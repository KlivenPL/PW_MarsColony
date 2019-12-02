using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Context;
using MarsColonyEngine.Logger;
using System;
namespace MarsColonyEngine.Simulation {
    public class Simulator {
        internal Simulator () { }
        internal static Simulator _current;
        internal AffectorsManager affectorsManager = new AffectorsManager();
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
        internal Action OnFirstTurnStarted { get; set; }

        public void NextTurn () {
            var currTurn = ColonyContext.Current.Turn;
            affectorsManager.GetTotalColonyStats(out ColonyStats baseStats, out ColonyStats deltaDayStats);
            currTurn.PrevDeltaDayColonyStats = currTurn.DeltaDayColonyStats;
            currTurn.Day++;
            OnNextTurnStarted?.Invoke();
            OnFirstTurnStarted?.Invoke();
            OnFirstTurnStarted = null;
        }
    }
}
