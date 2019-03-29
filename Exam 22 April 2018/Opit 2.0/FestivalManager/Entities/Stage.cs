namespace FestivalManager.Entities
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    public class Stage : IStage
    {
    
       private readonly List<ISet> sets;
       private readonly List<ISong> songs;
       private readonly List<IPerformer> performers;

        public IReadOnlyCollection<ISet> Sets => sets;

        public IReadOnlyCollection<ISong> Songs => songs;

        public IReadOnlyCollection<IPerformer> Performers => performers;

        public Stage()
        {
            sets = new List<ISet>();
            songs = new List<ISong>();
            performers = new List<IPerformer>();
        }

        public IPerformer GetPerformer(string name)
        {
            return performers.FirstOrDefault(x => x.Name == name);
        }

        public ISong GetSong(string name)
        {
            return songs.FirstOrDefault(x => x.Name == name);
        }

        public ISet GetSet(string name)
        {
            return sets.FirstOrDefault(x => x.Name == name);
        }

        public void AddPerformer(IPerformer performer)
        {
            performers.Add(performer);
        }

        public void AddSong(ISong song)
        {
            songs.Add(song);
        }

        public void AddSet(ISet set)
        {
            sets.Add(set);
        }

        public bool HasPerformer(string name)
        {
            return Performers.Any(x => x.Name == name);
        }

        public bool HasSong(string name)
        {
            return Songs.Any(x => x.Name == name);
        }

        public bool HasSet(string name)
        {
            return Sets.Any(x => x.Name == name);
        }
    }
}