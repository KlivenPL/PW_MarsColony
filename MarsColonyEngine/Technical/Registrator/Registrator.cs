using MarsColonyEngine.Business.Items;
using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Business.Structures;
using MarsColonyEngine.Colonizers;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Misc;
using MarsColonyEngine.Simulation;
using System.Linq;

namespace MarsColonyEngine.Technical {
    public abstract class Registrator : IIdentifiable {
        private static int instanceCounter = 0;

        //protected Registrator (string name) {
        //    Name = name;
        //}

        internal Registrator () {
            if (this is IOnFirstTurnStartedRec) {
                Simulator.Current.RegisterIOnFirstTurnStartedReciever(((IOnFirstTurnStartedRec)this).OnFirstTurnStarted);
            }
        }
        //~Registrator () {
        //    Unregister();
        //}

        public int Id { get; protected set; }
        public string Name { get; protected set; }

        protected void Register () {
            Id = instanceCounter++;
            if (this is Colonizer) {
                ColonyContext.Current.Colonizers.Add((Colonizer)this);
            }
            if (this is Structure) {
                ColonyContext.Current.Structures.Add((Structure)this);
            }
            if (this is Item) {
                var item = (Item)this;
                if (ColonyContext.Current.Turn.CountItems(item.ItemEnum) == 0)
                    ColonyContext.Current.Items.Add(item);
                else
                    ColonyContext.Current.Items.Single(e => e.ItemEnum == item.ItemEnum).AffectHp(item.Amount);
            }
            if (this is IActionHandler) {
                ColonyActions.ColonyActions.RegisterHandler((IActionHandler)this);
            }
            if (this is IColonyStatsAffector) {
                Simulator.Current.affectorsManager.RegisterColonyStatsAffector((IColonyStatsAffector)this);
            }
            if (this is IOnTurnStartedRec) {
                Simulator.Current.RegisterIOnNextTurnStartedReciever(((IOnTurnStartedRec)this).OnTurnStarted);
            }
            if (this is IOnTurnFinishedRec) {
                Simulator.Current.RegisterOnTurnFinishedReviever(((IOnTurnFinishedRec)this).OnTurnFinished);
            }
        }

        protected void Unregister () {
            if (this is Colonizer) {
                ColonyContext.Current.Colonizers.Remove((Colonizer)this);
            }
            if (this is Structure) {
                ColonyContext.Current.Structures.Remove((Structure)this);
            }
            if (this is Item) {
                ColonyContext.Current.Items.Remove((Item)this);
            }
            if (this is IActionHandler) {
                ColonyActions.ColonyActions.UnregisterHandler((IActionHandler)this);
            }
            if (this is IColonyStatsAffector) {
                Simulator.Current.affectorsManager.UnregisterColonyStatsAffector((IColonyStatsAffector)this);
            }
            if (this is IOnTurnStartedRec) {
                Simulator.Current.UnregisterIOnNextTurnStartedReciever(((IOnTurnStartedRec)this).OnTurnStarted);
            }
            if (this is IOnTurnFinishedRec) {
                Simulator.Current.UnregisterOnTurnFinishedReviever(((IOnTurnFinishedRec)this).OnTurnFinished);
            }
        }
    }
}
