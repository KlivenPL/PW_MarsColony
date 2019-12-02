using MarsColonyEngine.Business.Structures;
using MarsColonyEngine.ColonyActions;
using System;
using System.Linq;

namespace MarsColonyEngine.Colonizers {
    public class Engineer : Colonizer {
        public override AvailableActions[] GetAvailableActions () {
            return base.GetAvailableActions().Concat(new AvailableActions[] {
                AvailableActions.BuildStructureSimpleShelter_Handler_User,
            }).ToArray();
        }

        [ActionRequirement(AvailableActions.BuildStructureSimpleShelter_Handler_User)]
        private bool BuildStructureSimpleShelterRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 25f;
        }

        [ActionProcedure(AvailableActions.BuildStructureSimpleShelter_Handler_User, typeof(Engineer))]
        private Structure BuildStructureSimpleShelterProcedure (ref string res) {
            return new Structure {
                Name = "SimpleShelter",
                BaseColonizerStatsAffect = new Business.Colonizers.ColonizerStats(0, 0, 0, 0, 10f),
                DeltaDayColonizerStatsAffect = default,
                BaseColonyStatsAffect = new Business.Stats.ColonyStats(0, 0, 1, 0),
                DeltaDayColonyStatsAffect = default,
                Stats = new StructureStats(50, 100)
            };
        }
    }
}
