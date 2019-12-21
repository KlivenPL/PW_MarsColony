using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Business.Structures;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
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
            return new Structure(
                name: "SimpleShelter",
                baseColonizerStatsAffect: new Business.Colonizers.ColonizerStats(0, 0, 0, 0, 10f),
                deltaDayColonizerStatsAffect: default,
                baseColonyStatsAffect: new Business.Stats.ColonyStats(0, 0, 1, 0),
                deltaDayColonyStatsAffect: default,
                stats: new StructureStats(50, 100)
                );
        }

        [ActionRequirement(AvailableActions.BuildStructureChurch_Handler_User_Paramless)]
        private bool BuildStructureChurchRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 50f && ColonyContext.Current.Turn.TotalColonyStats.Population == 3;
        }

        [ActionProcedure(AvailableActions.BuildStructureChurch_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildStructureChurchProcedure (ref string res) {
            return new Structure(
                name: "Church",
                baseColonizerStatsAffect: new Business.Colonizers.ColonizerStats(0, 5, 0, 0, 5f),
                deltaDayColonizerStatsAffect: default,
                baseColonyStatsAffect: new Business.Stats.ColonyStats(0, 0, 1, 0),
                deltaDayColonyStatsAffect: default,
                stats: new StructureStats(70, 100)
                );
        }
    }
}
