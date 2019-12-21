using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Colonizers;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Misc;
using MarsColonyEngine.Simulation;
using System;

namespace MarsColonyEngine.Technical {
    public abstract class Registrator : IIdentifiable {
        private static int instanceCounter = 0;

        protected Registrator (string name) {
            Name = name;
        }

        internal Registrator () {
            if (this is IOnFirstTurnStartedRec) {
                Simulator.Current.RegisterIOnFirstTurnStartedReciever(((IOnFirstTurnStartedRec)this).OnFirstTurnStarted);
            }
        }
        ~Registrator () {
            Unregister();
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }

        protected void Register () {
            Id = instanceCounter++;
            if (this is Colonizer) {
                ColonyContext.Current.Colonizers.Add((Colonizer)this);
            }
            if (this is IActionHandler) {
                ColonyActions.ColonyActions.RegisterHandler((IActionHandler)this);
            }
            if (this is IColonyStatsAffector) {
                Simulator.Current?.affectorsManager.RegisterColonyStatsAffector((IColonyStatsAffector)this);
            }
            if (this is IOnTurnStartedRec) {
                Simulator.Current.RegisterIOnNextTurnStartedReciever(((IOnTurnStartedRec)this).OnTurnStarted);
            }
            if (this is IOnTurnFinishedRec) {
                Simulator.Current.RegisterOnTurnFinishedReviever(((IOnTurnStartedRec)this).OnTurnStarted);
            }
        }

        protected void Unregister () {
            if (this is Colonizer) {
                ColonyContext.Current.Colonizers.Remove((Colonizer)this);
            }
            if (this is IActionHandler) {
                ColonyActions.ColonyActions.UnregisterHandler((IActionHandler)this);
            }
            if (this is IColonyStatsAffector) {
                Simulator.Current?.affectorsManager.UnregisterColonyStatsAffector((IColonyStatsAffector)this);
            }
            if (this is IOnTurnStartedRec) {
                throw new NotImplementedException();
            }
            if (this is IOnTurnFinishedRec) {
                throw new NotImplementedException();
            }
        }
    }
}
