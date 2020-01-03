﻿using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Helpers;
using System;

namespace MarsColonyEngine.Colonizers {
    internal class ColonizerActions {

        [ActionRequirement(AvailableActions.SpawnColonizer_Static_Simulation_Args)]
        private static bool SpawnColonizerRequirement (ref string res) {
            return ColonyContext.Current.World != null && ColonyContext.Current.Turn.TotalColonyStats.PopulationLimit - ColonyContext.Current.Turn.TotalColonyStats.Population > 0;
        }

        [ActionProcedure(AvailableActions.SpawnColonizer_Static_Simulation_Args, null)]
        private static Colonizer SpawnColonizerProcedure (ref string res, Type type) {
            Colonizer spawnedColonizer = null;
            if (type == typeof(Engineer)) {
                spawnedColonizer = new Engineer(
                    baseColonyStatsAffect: new ColonyStats(0, 1, 0, 0),
                    deltaDayColonyStatsAffect: new ColonyStats(), // those are managed on every turn in Colonizer class.
                    name: KRandom.Name(),
                    stats: new ColonizerStats(KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(30, 70))
                );
            } else if (type == typeof(Scientist)) {
                spawnedColonizer = new Scientist(
                    baseColonyStatsAffect: new ColonyStats(0, 1, 0, 0),
                    deltaDayColonyStatsAffect: new ColonyStats(),
                    name: KRandom.Name(),
                    stats: new ColonizerStats(KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(30, 70))
                );
            } else if (type == typeof(Explorer)) {
                spawnedColonizer = new Explorer(
                    baseColonyStatsAffect: new ColonyStats(0, 1, 0, 0),
                    deltaDayColonyStatsAffect: new ColonyStats(),
                    name: KRandom.Name(),
                    stats: new ColonizerStats(KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(80, 110), KRandom.Float(30, 70))
                );
            } else
            if (spawnedColonizer == null)
                res = "Invalid Colonizer class was given.";

            return spawnedColonizer;
        }
    }
}
