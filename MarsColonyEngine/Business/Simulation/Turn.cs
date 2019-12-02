using MarsColonyEngine.Business.Stats;

namespace MarsColonyEngine.Simulation {
    public class Turn {
        public ColonyStats PrevDeltaDayColonyStats { get; set; }
        public ColonyStats BaseColonyStats {
            get {
                Simulator.Current.affectorsManager.GetTotalColonyStats(out ColonyStats baseStats, out ColonyStats deltaDayStats);
                return baseStats;
            }
        }
        public ColonyStats DeltaDayColonyStats {
            get {
                Simulator.Current.affectorsManager.GetTotalColonyStats(out ColonyStats baseStats, out ColonyStats deltaDayStats);
                return deltaDayStats + PrevDeltaDayColonyStats;
            }
        }
        public ColonyStats TotalColonyStats => DeltaDayColonyStats + BaseColonyStats;

        public int Day { get; internal set; }

        //public 
        public int AvailableMoves {
            get {
                return ColonyActions.ColonyActions.GetAvailableActions().Length;
            }
        }


    }
}
