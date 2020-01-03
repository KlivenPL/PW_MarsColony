using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Business.Structures;
using MarsColonyEngine.ColonyActions;
using System.Linq;

namespace MarsColonyEngine.Colonizers {
    public class Engineer : Colonizer {
        public Engineer (string name, ColonizerStats stats, ColonyStats baseColonyStatsAffect, ColonyStats deltaDayColonyStatsAffect) : base(name, stats, baseColonyStatsAffect, deltaDayColonyStatsAffect) {
        }

        public override AvailableActions[] GetAvailableActions () {
            return base.GetAvailableActions().Concat(new AvailableActions[] {
                AvailableActions.BuildStructureSimpleShelter_Handler_User_Paramless,
            }).ToArray();
        }

        [ActionRequirement(AvailableActions.BuildStructureSimpleShelter_Handler_User_Paramless)]
        private bool BuildStructureSimpleShelterRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 25f;
        }

        [ActionProcedure(AvailableActions.BuildStructureSimpleShelter_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildStructureSimpleShelterProcedure (ref string res) {
            return StructuresBuilder.SimpleShelter();
        }
    }
}
