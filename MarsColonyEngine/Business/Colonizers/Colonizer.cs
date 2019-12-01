using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Misc;
using MarsColonyEngine.Technical;

namespace MarsColonyEngine.Colonizers {
    public abstract class Colonizer : Registrator, IActionHandler, IColonyStatsAffector, IBasicParameters {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public float HP { get; private set; }
        public float Efficiency { get; private set; }

        public float Hunger { get; private set; }
        public float Space { get; private set; }
        public float Comfort { get; private set; }

        internal ColonyStats BaseColonyStats { get; private set; }
        internal ColonyStats DayDeltaColonyStats { get; private set; }

        public IStats BaseStats => BaseColonyStats;
        public IStats DeltaDayStats => DayDeltaColonyStats;

        //ColonyStats IColonyStatsAffector.BaseColonyStats => BaseColonyStats;
        //ColonyStats IColonyStatsAffector.DayDeltaColonyStats => DayDeltaColonyStats;
        /*public virtual IStats BaseStats => BaseColonyStats;
        public virtual IStats DayDeltaStats => DeltaDayColonyStats;*/

        protected Colonizer () {
            ColonyContext.Current.Colonizers.Add(this);

            BaseColonyStats = new ColonyStats {
                Polulation = 1
            };
            DayDeltaColonyStats = new ColonyStats {
                OxygenLevel = -1
            };
        }
        ~Colonizer () {
            ColonyContext.Current.Colonizers.Remove(this);
        }

        public virtual bool IsActive => HP > 0 && Efficiency > 0;

        public virtual AvailableActions[] GetAvailableActions () {
            return new AvailableActions[]{

            };
        }
    }
}
