using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Context;
using MarsColonyEngine.Logger;
using System;
using System.Collections.Generic;

namespace MarsColonyEngine.Simulation {
    public partial class Simulator {
        internal Simulator () { }
        public static Simulator _current;
        public static Simulator Current {
            get {
                if (_current == null) {
                    KLogger.Log.Error("Simulator cannot be used: ColonyContext does not exist. Use ColonyContext.Load() or ColonyContext.Create() first.");
                    return null;
                }
                return _current;
            }
        }

        internal Action OnNextTurnStarted { get; set; }

        private List<IStatsAffector> statsAffectors = new List<IStatsAffector>();

        internal void RegisterStatsAffector (IStatsAffector affector) {
            statsAffectors.Add(affector);
        }
        internal void UnregisterStatsAffector (IStatsAffector affector) {
            statsAffectors.Remove(affector);
        }

        IStats GetTotalStats<T> (out IStats baseStats, out IStats deltaDayStats) where T : IStatsAffector {
            var statsAffectorType = typeof(T);

            baseStats = default;
            deltaDayStats = default;

            foreach (var affector in statsAffectors) {
                if (affector.GetType().IsEquivalentTo(statsAffectorType)) {
                    if (deltaDayStats == null) {
                        baseStats = affector.BaseStats;
                        deltaDayStats = affector.DayDeltaStats;
                        continue;
                    }
                    baseStats = baseStats.Add(affector.BaseStats);
                    deltaDayStats = deltaDayStats.Add(affector.DayDeltaStats);
                }
            }
            return baseStats.Add(deltaDayStats);
        }


        public void NextTurn () {
            var currTurn = ColonyContext.Current.Turn;
            GetTotalStats<ColonyStatsAffector>(out IStats baseStats, out IStats deltaDayStats);
            var newTurn = new Turn(
                currTurn.Day + 1,
                (ColonyStats)baseStats,
                currTurn.DeltaDayColonyStats + (ColonyStats)deltaDayStats
            );
            ColonyContext.Current.Turn = newTurn;
            OnNextTurnStarted();
        }
    }
}
