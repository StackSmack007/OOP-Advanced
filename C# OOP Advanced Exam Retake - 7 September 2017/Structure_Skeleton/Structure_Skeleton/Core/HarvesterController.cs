using System;
using System.Collections.Generic;
using System.Linq;

public class HarvesterController : IHarvesterController
{
    private Mode currentMode;
    private IList<IHarvester> harvesters;
    private IHarvesterFactory harvesterFactory;
    private IEnergyRepository energyRepository;
    public HarvesterController(IEnergyRepository energyRepository)
    {
        currentMode = (Mode)Enum.Parse(typeof(Mode), Constants.DefaultMode, true);
        harvesters = new List<IHarvester>();
        harvesterFactory = new HarvesterFactory();
        OreProduced = 0;
        this.energyRepository = energyRepository;
    }

    public double OreProduced { get; private set; }

    public string ChangeMode(string mode)
    {
        if (!Enum.TryParse<Mode>(mode, out currentMode))
        {
            throw new ArgumentException("Wrong ModeType NSH-exception");
        }

        var brokenHarvesters = new List<IHarvester>();
        foreach (var harvester in harvesters)
        {
            try
            {
                harvester.Broke();
            }
            catch (ArgumentException)
            {
                brokenHarvesters.Add(harvester);
            }
        }
        foreach (var broken in brokenHarvesters)
        {
            harvesters.Remove(broken);
        }
        return $"Mode changed to {mode}!";
    }

    public IEntity GetById(int id)
    {
        return harvesters.FirstOrDefault(x=>x.ID==id);
    }

    public string Produce()
    {
        double modeKoef = (int)currentMode / 100.0;
        double EnergyRequired = modeKoef * harvesters.Sum(x => x.EnergyRequirement);

        double oreProducedThisDay = 0;
        if (energyRepository.EnergyStored >= EnergyRequired)
        {
            energyRepository.TakeEnergy(EnergyRequired);
            oreProducedThisDay += modeKoef * harvesters.Sum(x => x.Produce());
            OreProduced += oreProducedThisDay;
        }
        return string.Format(Constants.OreOutputToday, oreProducedThisDay);
    }

    public string Register(IList<string> args)
    {
        var newHarvester = harvesterFactory.GenerateHarvester(args);
        harvesters.Add(newHarvester);
        return string.Format(Constants.SuccessfullRegistration, newHarvester.GetType().Name);
    }
}