using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Technical;
using MarsColonyEngine.Technical.Misc;
using System;

namespace MarsColonyEngine.Colonizers {
    public abstract class Colonizer : Registrator, IActionHandler, IColonyStatsAffector, IDestructable {
        public ColonizerStats Stats { get; private set; }

        public ColonyStats BaseColonyStatsAffect { get; private set; }
        public ColonyStats DeltaDayColonyStatsAffect { get; private set; }
        public bool IsAlive => Stats.HP > 0;
        public Action Destroy => () => {
            Unregister();
            ColonyContext.Current.Colonizers.Remove(this);
        };

        protected Colonizer () {
            Register();
            ColonyContext.Current.Colonizers.Add(this);

            BaseColonyStatsAffect = new ColonyStats {
                Population = 1
            };
            DeltaDayColonyStatsAffect = new ColonyStats {
                Oxygen = -1,
                Food = -1
            };
        }
        ~Colonizer () {
            ColonyContext.Current.Colonizers.Remove(this);
        }

        public virtual bool IsActive => Stats.HP > 0 && Stats.Efficiency > 0;

        public virtual AvailableActions[] GetAvailableActions () {
            return new AvailableActions[]{

            };
        }
    }
}
