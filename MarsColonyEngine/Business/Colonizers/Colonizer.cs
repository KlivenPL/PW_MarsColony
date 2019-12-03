using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Technical;
using MarsColonyEngine.Technical.Misc;
using System;

namespace MarsColonyEngine.Colonizers {
    public abstract class Colonizer : Registrator, IActionHandler, IColonyStatsAffector, IDestructable, IOnFirstTurnStartedRec {
        public ColonizerStats Stats { get; internal set; }

        public ColonyStats BaseColonyStatsAffect { get; internal set; }
        public ColonyStats DeltaDayColonyStatsAffect { get; internal set; }
        public bool IsAlive => Stats.HP > 0;
        public Action Destroy => () => {
            Unregister();
            ColonyContext.Current.Colonizers.Remove(this);
        };

        public virtual bool IsActive => Stats.HP > 0 && Stats.Efficiency > 0;

        public virtual AvailableActions[] GetAvailableActions () {
            return new AvailableActions[]{
                
            };
        }
        public void OnFirstTurnStarted () {
            Register();
        }
    }
}
