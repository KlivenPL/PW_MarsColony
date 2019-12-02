using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Colonizers;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Misc;
using MarsColonyEngine.Simulation;

namespace MarsColonyEngine.Technical {
    public abstract class Registrator : IIdentifiable {
        private static int instanceCounter = 0;

        internal Registrator () {
            if (this is IOnFirstTurnStartedRec) {
                Simulator.Current.OnFirstTurnStarted += ((IOnFirstTurnStartedRec)this).OnFirstTurnStarted;
            }
        }
        ~Registrator () {
            Unregister();
        }

        public int Id { get; set; }
        public string Name { get; set; }

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
            if (this is IOnNextTurnStartedRec) {
                Simulator.Current.OnNextTurnStarted -= ((IOnNextTurnStartedRec)this).OnNextTurnStarted;
            }
        }
    }
}
