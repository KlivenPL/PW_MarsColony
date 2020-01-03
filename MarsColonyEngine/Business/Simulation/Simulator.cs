using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Context;
using MarsColonyEngine.Logger;
using System;
namespace MarsColonyEngine.Simulation {
    public class Simulator {
        internal Simulator () { }
        private static Simulator _current;
        internal AffectorsManager affectorsManager = new AffectorsManager();
        public static Simulator Current {
            get {
                if (ColonyContext.Current == null) {
                    KLogger.Log.Error("Simulator cannot be used: ColonyContext does not exist. Use ColonyContext.Load() or ColonyContext.Create() first.");
                    return null;
                }
                return _current ?? (_current = new Simulator());
            }
        }

        internal Action OnTurnFinished { get; private set; }
        internal Action OnTurnStarted { get; private set; }
        internal Action OnFirstTurnStarted { get; private set; }

        internal void RegisterIOnNextTurnStartedReciever (Action action) {
            OnTurnStarted += action;
        }
        internal void RegisterIOnFirstTurnStartedReciever (Action action) {
            OnFirstTurnStarted += action;
        }
        internal void RegisterOnTurnFinishedReviever (Action action) {
            OnTurnFinished += action;
        }
        public void NextTurn () {
            OnTurnFinished?.Invoke();
            var currTurn = ColonyContext.Current.Turn;
            affectorsManager.GetTotalColonyStats(out ColonyStats baseStats, out ColonyStats deltaDayStats);
            currTurn.SetNextTurn(currTurn.DeltaDayColonyStats);
            OnTurnStarted?.Invoke();
            OnFirstTurnStarted?.Invoke();
            OnFirstTurnStarted = null;
        }
    }
}
