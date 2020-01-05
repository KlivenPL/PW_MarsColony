using MarsColonyEngine.Business.Items;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Context;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace MarsColonyEngine.Simulation {
    [Serializable]
    public class Turn : ISerializable {
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
                // PrevDeltaDayColonyStats = PrevDeltaDayColonyStats + new ColonyStats(0, 0, 0, -PrevDeltaDayColonyStats.Food);
                PrevDeltaDayColonyStats = new ColonyStats(
                    PrevDeltaDayColonyStats.Oxygen,
                    PrevDeltaDayColonyStats.Population,
                    PrevDeltaDayColonyStats.PopulationLimit,
                    -BaseColonyStats.Food
                );
            }
            if (TotalColonyStats.Oxygen < 0) {
                PrevDeltaDayColonyStats = new ColonyStats(
                    -BaseColonyStats.Oxygen,
                    PrevDeltaDayColonyStats.Population,
                    PrevDeltaDayColonyStats.PopulationLimit,
                    PrevDeltaDayColonyStats.Food
                );
            }
        }

        public int CountItems (AvailableItems item) {
            return ColonyContext.Current.Items.Where(e => e.ItemEnum == item).Sum(e => e.Amount);
        }
        public Turn () { }

        public void GetObjectData (SerializationInfo info, StreamingContext context) {
            info.AddValue(nameof(PrevDeltaDayColonyStats), PrevDeltaDayColonyStats);
            info.AddValue(nameof(Day), Day);
        }
        public Turn (SerializationInfo info, StreamingContext context) {
            PrevDeltaDayColonyStats = (ColonyStats)info.GetValue(nameof(PrevDeltaDayColonyStats), typeof(ColonyStats));
            Day = (int)info.GetValue(nameof(Day), typeof(int));
        }

        public int AvailableMoves {
            get {
                return ColonyActions.ColonyActions.GetAvailableActions().Length;
            }
        }


    }
}
