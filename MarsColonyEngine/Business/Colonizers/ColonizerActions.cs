using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Helpers;
using System;

namespace MarsColonyEngine.Colonizers {
    internal class ColonizerActions {

        [ActionRequirement(AvailableActions.SpawnColonizer_Static_Simulation_Args)]
        private static bool SpawnColonizerRequirement (ref string res) {
            return ColonyContext.Current.World != null;
        }

        [ActionProcedure(AvailableActions.SpawnColonizer_Static_Simulation_Args, null)]
        private static Colonizer SpawnColonizerProcedure (ref string res, Type type) {
            Colonizer spawnedColonizer = null;
            if (type == typeof(Engineer)) {
                spawnedColonizer = new Engineer(
                    baseColonyStatsAffect: new Business.Stats.ColonyStats(0, 1, 0, 0),
                    deltaDayColonyStatsAffect: default,
                    name: KRandom.Name(),
                    stats: new Business.Colonizers.ColonizerStats(KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(30, 70))
                );
            } else if (type == typeof(Scientist)) {
                spawnedColonizer = new Scientist(
                    baseColonyStatsAffect: new Business.Stats.ColonyStats(0, 1, 0, 0),
                    deltaDayColonyStatsAffect: default,
                    name: KRandom.Name(),
                    stats: new Business.Colonizers.ColonizerStats(KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(30, 70))
                );
            } else if (type == typeof(Explorer)) {
                spawnedColonizer = new Explorer(
                    baseColonyStatsAffect: new Business.Stats.ColonyStats(0, 1, 0, 0),
                    deltaDayColonyStatsAffect: default,
                    name: KRandom.Name(),
                    stats: new Business.Colonizers.ColonizerStats(KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(30, 70))
                );
            } else
            if (spawnedColonizer == null)
                res = "Invalid Colonizer class was given.";

            return spawnedColonizer;
        }
    }
}
