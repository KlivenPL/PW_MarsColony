using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Technical;
using MarsColonyEngine.Technical.Misc;
using System;

namespace MarsColonyEngine.Business.Structures {
    public class Structure : Registrator, IDestructable, IColonizerStatsAffector, IColonyStatsAffector, IOnFirstTurnStartedRec {
        public bool IsAlive { get { return Stats.HP > 0; } }
        public Action Destroy => () => {
            Unregister();
        };
        public StructureStats Stats { get; internal set; }

        public ColonizerStats BaseColonizerStatsAffect { get; internal set; }

        public ColonizerStats DeltaDayColonizerStatsAffect { get; internal set; }

        public ColonyStats BaseColonyStatsAffect { get; internal set; }

        public ColonyStats DeltaDayColonyStatsAffect { get; internal set; }

        public Structure (Structure pattern) {
            Name = pattern.Name;
            Stats = pattern.Stats;
            BaseColonyStatsAffect = pattern.BaseColonyStatsAffect;
            DeltaDayColonyStatsAffect = pattern.DeltaDayColonyStatsAffect;
            BaseColonizerStatsAffect = pattern.BaseColonizerStatsAffect;
            DeltaDayColonizerStatsAffect = pattern.DeltaDayColonizerStatsAffect;
        }

        internal Structure (string name, StructureStats stats, ColonyStats baseColonyStatsAffect, ColonyStats deltaDayColonyStatsAffect, ColonizerStats baseColonizerStatsAffect, ColonizerStats deltaDayColonizerStatsAffect) {
            Name = name;
            Stats = stats;
            BaseColonyStatsAffect = baseColonyStatsAffect;
            DeltaDayColonyStatsAffect = deltaDayColonyStatsAffect;
            BaseColonizerStatsAffect = baseColonizerStatsAffect;
            DeltaDayColonizerStatsAffect = deltaDayColonizerStatsAffect;
        }

        public Structure () {
        }

        public void OnFirstTurnStarted () {
            Register();
        }
    }
}
