using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.ColonyActions;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace MarsColonyEngine.Colonizers {
    [Serializable]
    public class Scientist : Colonizer, ISerializable {
        public Scientist (string name, ColonizerStats stats, ColonyStats baseColonyStatsAffect, ColonyStats deltaDayColonyStatsAffect) : base(name, stats, baseColonyStatsAffect, deltaDayColonyStatsAffect) {
        }

        public override AvailableActions[] GetAvailableActions () {
            return base.GetAvailableActions().Concat(new AvailableActions[] {

            }).ToArray();
        }

        public Scientist (SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
