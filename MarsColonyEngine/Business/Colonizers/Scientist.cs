using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.ColonyActions;
using System.Linq;

namespace MarsColonyEngine.Colonizers {
    public class Scientist : Colonizer {
        public Scientist (string name, ColonizerStats stats, ColonyStats baseColonyStatsAffect, ColonyStats deltaDayColonyStatsAffect) : base(name, stats, baseColonyStatsAffect, deltaDayColonyStatsAffect) {
        }

        public override AvailableActions[] GetAvailableActions () {
            return base.GetAvailableActions().Concat(new AvailableActions[] {

            }).ToArray();
        }
    }
}
