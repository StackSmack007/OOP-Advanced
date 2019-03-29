namespace FestivalManager.Core.Controllers
{
    using Contracts;
    using Entities.Contracts;
    using FestivalManager.Entities.Factories;
    using FestivalManager.Entities.Factories.Contracts;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class FestivalController : IFestivalController
    {
        private const string TimeFormatLong = "{0:2D}:{1:2D}";
        private const string timeFormat = "mm\\:ss";
        private ISetFactory setFactory;
        private IInstrumentFactory instrumentFactory;
        private IPerformerFactory performerFactory;
        private ISongFactory songFactory;

        private readonly IStage stage;

        public FestivalController(IStage stage)
        {
            this.stage = stage;
            setFactory = new SetFactory();
            instrumentFactory = new InstrumentFactory();
            performerFactory = new PerformerFactory();
            songFactory = new SongFactory();
        }

        public string ProduceReport()
        {
            StringBuilder sb = new StringBuilder();

            var totalFestivalLength = new TimeSpan(this.stage.Sets.Sum(s => s.ActualDuration.Ticks));

            sb.AppendLine($"Festival length: {FormatTimeSpan(totalFestivalLength)}");

            foreach (var set in this.stage.Sets)
            {
                sb.AppendLine( ($"--{set.Name} ({FormatTimeSpan(set.ActualDuration)}):"));

                var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);
                foreach (var performer in performersOrderedDescendingByAge)
                {
                    var instruments = string.Join(", ", performer.Instruments
                        .OrderByDescending(i => i.Wear));

                    sb.AppendLine(($"---{performer} ({instruments})"));
                }

                if (!set.Songs.Any())
                    sb.AppendLine(("--No songs played") );
                else
                {
                    sb.AppendLine(("--Songs played:") );
                    foreach (var song in set.Songs)
                    {
                        sb.AppendLine($"----{song}");
                    }
                }
            }

            return sb.ToString().Trim();
        }


        public string RegisterSet(string[] args)
        {
            string name = args[0];
            string durationType = args[1];
            ISet newSet = setFactory.CreateSet(name, durationType);
            stage.AddSet(newSet);
            return $"Registered {durationType} set";
        }

        public string SignUpPerformer(string[] args)
        {
            var name = args[0];
            var age = int.Parse(args[1]);

            var instrumentNames = args.Skip(2).ToArray();

            IInstrument[] instruments = instrumentNames
                .Select(i => this.instrumentFactory.CreateInstrument(i))
                .ToArray();

            IPerformer performer = this.performerFactory.CreatePerformer(name, age);

            foreach (var instrument in instruments)
            {
                performer.AddInstrument(instrument);
            }

            this.stage.AddPerformer(performer);

            return $"Registered performer {performer.Name}";
        }

        public string RegisterSong(string[] args)
        {
            string songName = args[0];
            TimeSpan duration = TimeSpan.ParseExact(args[1], timeFormat, CultureInfo.InvariantCulture);
            ISong song = songFactory.CreateSong(songName, duration);
            stage.AddSong(song);
            return $"Registered song {song.Name} ({args[1]})";
        }

        public string AddSongToSet(string[] args)//previous name SongRegistration
        {
            var songName = args[0];
            var setName = args[1];

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            if (!this.stage.HasSong(songName))
            {
                throw new InvalidOperationException("Invalid song provided");
            }

            ISet set = this.stage.GetSet(setName);
            ISong song = this.stage.GetSong(songName);

            set.AddSong(song);

            return $"Added {song} to {set.Name}";
        }

        public string AddPerformerToSet(string[] args)
        {
            var performerName = args[0];
            var setName = args[1];

            if (!this.stage.HasPerformer(performerName))
            {
                throw new InvalidOperationException("Invalid performer provided");
            }

            if (!this.stage.HasSet(setName))
            {
                throw new InvalidOperationException("Invalid set provided");
            }

            IPerformer performer = this.stage.GetPerformer(performerName);
            ISet set = this.stage.GetSet(setName);

            set.AddPerformer(performer);

            return $"Added {performer.Name} to {set.Name}";
        }

        public string RepairInstruments(string[] args)
        {
            IInstrument[] instrumentsToRepair = stage.Performers
                .SelectMany(p => p.Instruments)
                .Where(i => i.Wear < 100)
                .ToArray();

            foreach (var instrument in instrumentsToRepair)
            {
                instrument.Repair();
            }

            return $"Repaired {instrumentsToRepair.Length} instruments";
        }

        private static string FormatTimeSpan(TimeSpan timeSpan)
        {
            var formatted = string.Format("{0:D2}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            return formatted;
        }
    }
}