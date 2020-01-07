using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Helpers;

namespace MarsColonyEngine.Colonizers {
    internal class ColonizerSpawner {

        public static Explorer SpawnExplorer () {
            return new Explorer(
                    baseColonyStatsAffect: new ColonyStats(0, 1, 0, 0),
                    deltaDayColonyStatsAffect: new ColonyStats(),
                    name: KRandom.Name(),
                    stats: new ColonizerStats(KRandom.Float(80, 110), 110, KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(0.9f, 1.1f), KRandom.Float(30, 70))
                );
        }
        public static Engineer SpawnEngineer () {
            return new Engineer(
                    baseColonyStatsAffect: new ColonyStats(0, 1, 0, 0),
                    deltaDayColonyStatsAffect: new ColonyStats(), // those are managed on every turn in Colonizer class.
                    name: KRandom.Name(),
                    stats: new ColonizerStats(KRandom.Float(80, 110), 110, KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(0.9f, 1.1f), KRandom.Float(30, 70))
                );
        }
        public static Scientist SpawnScientist () {
            return new Scientist(
                    baseColonyStatsAffect: new ColonyStats(0, 1, 0, 0),
                    deltaDayColonyStatsAffect: new ColonyStats(),
                    name: KRandom.Name(),
                    stats: new ColonizerStats(KRandom.Float(80, 110), 110, KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(0.9f, 1.1f), KRandom.Float(30, 70))
                );
        }

        [ActionRequirement(AvailableActions.SpawnColonizer_Static_Simulation_Args)]
        private static bool SpawnColonizerRequirement (ref string res) {
            return ColonyContext.Current.World != null && ColonyContext.Current.Turn.TotalColonyStats.PopulationLimit - ColonyContext.Current.Turn.TotalColonyStats.Population > 0;
        }

        [ActionProcedure(AvailableActions.SpawnColonizer_Static_Simulation_Args, null)]
        private static Colonizer SpawnColonizerProcedure (ref string res, string type) {
            Colonizer spawnedColonizer = null;
            if (type.ToLower().Contains("engineer")) {
                spawnedColonizer = SpawnEngineer();
                res = $"Engineer {spawnedColonizer.Name} has been spawned.";
            } else if (type.ToLower().Contains("scientist")) {
                spawnedColonizer = SpawnScientist();
                res = $"Scientist {spawnedColonizer.Name} has been spawned.";
            } else if (type.ToLower().Contains("explorer")) {
                spawnedColonizer = SpawnExplorer();
                res = $"Explorer {spawnedColonizer.Name} has been spawned.";
            } else
            if (spawnedColonizer == null)
                res = "Invalid Colonizer class was given.";

            Simulation.Simulator.Current.NextTurn();
            return spawnedColonizer;
        }
    }
}
