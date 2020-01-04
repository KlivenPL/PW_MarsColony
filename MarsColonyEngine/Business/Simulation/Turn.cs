using MarsColonyEngine.Business.Items;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Context;
using System.Linq;

namespace MarsColonyEngine.Simulation {
    public class Turn {
        public ColonyStats PrevDeltaDayColonyStats { get; private set; } = new ColonyStats();
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

        public int Day { get; private set; }

        public void SetNextTurn (ColonyStats prevDeltaDayColonyStats) {
            Day++;
            PrevDeltaDayColonyStats = prevDeltaDayColonyStats;
            if (TotalColonyStats.Food < 0) {
                PrevDeltaDayColonyStats = PrevDeltaDayColonyStats + new ColonyStats(0, 0, 0, -PrevDeltaDayColonyStats.Food);
            }
            if (TotalColonyStats.Oxygen < 0) {
                PrevDeltaDayColonyStats = PrevDeltaDayColonyStats + new ColonyStats(-PrevDeltaDayColonyStats.Oxygen, 0, 0, 0);
            }
        }

        public int CountItems (AvailableItems item) {
            return ColonyContext.Current.Items.Where(e => e.ItemEnum == item).Sum(e => e.Amount);
        }

        public int AvailableMoves {
            get {
                return ColonyActions.ColonyActions.GetAvailableActions().Length;
            }
        }


    }
}
