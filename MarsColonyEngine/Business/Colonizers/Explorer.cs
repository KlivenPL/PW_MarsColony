using MarsColonyEngine.ColonyActions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;

namespace MarsColonyEngine.Colonizers {
    public class Explorer : Colonizer {
        public Explorer (string name, ColonizerStats stats, ColonyStats baseColonyStatsAffect, ColonyStats deltaDayColonyStatsAffect) : base(name, stats, baseColonyStatsAffect, deltaDayColonyStatsAffect) {
        }

        public override AvailableActions[] GetAvailableActions () {
            return base.GetAvailableActions().Concat(new AvailableActions[] {
                
            }).ToArray();
        }
    }
}
