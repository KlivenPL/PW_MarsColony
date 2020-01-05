using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Items;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Business.Structures;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace MarsColonyEngine.Colonizers {
    [Serializable]
    public class Engineer : Colonizer, ISerializable {
        public Engineer (string name, ColonizerStats stats, ColonyStats baseColonyStatsAffect, ColonyStats deltaDayColonyStatsAffect) : base(name, stats, baseColonyStatsAffect, deltaDayColonyStatsAffect) {
        }

        public override AvailableActions[] GetAvailableActions () {
            return base.GetAvailableActions().Concat(new AvailableActions[] {
                AvailableActions.BuildStructureSimpleShelter_Handler_User_Paramless,
                AvailableActions.BuildStructurePotatoFarm_Handler_User_Paramless,
                AvailableActions.BuildStructureAluminiumMine_Handler_User_Paramless,
            }).ToArray();
        }

        [ActionRequirement(AvailableActions.BuildStructureSimpleShelter_Handler_User_Paramless)]
        private bool BuildStructureSimpleShelterRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f && ColonyContext.Current.Turn.CountItems(AvailableItems.Aluminium) > 50;
        }

        [ActionProcedure(AvailableActions.BuildStructureSimpleShelter_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildStructureSimpleShelterProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, -Stats.Efficiency, 0, 0, 0));
            res = $"Engineer {Name} has built Simple Shelter.";
            new Item(AvailableItems.Aluminium, -50);
            return StructuresBuilder.SimpleShelter();
        }

        [ActionRequirement(AvailableActions.BuildStructurePotatoFarm_Handler_User_Paramless)]
        private bool BuildStructurePotatoFarmRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f && ColonyContext.Current.Turn.CountItems(AvailableItems.Aluminium) > 25;
        }

        [ActionProcedure(AvailableActions.BuildStructurePotatoFarm_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildStructurePotatoFarmProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, -Stats.Efficiency, 0, 0, 0));
            res = $"Engineer {Name} has built Potato Farm.";
            new Item(AvailableItems.Aluminium, -25);
            return StructuresBuilder.PotatoFarm();
        }

        [ActionRequirement(AvailableActions.BuildStructureAluminiumMine_Handler_User_Paramless)]
        private bool BuildStructureAluminiumMineRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f && ColonyContext.Current.Turn.CountItems(AvailableItems.AluminiumOreMap) > 0;
        }

        [ActionProcedure(AvailableActions.BuildStructureAluminiumMine_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildStructureAluminiumMineProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, -Stats.Efficiency, 0, 0, 0));
            res = $"Engineer {Name} has built Aluminium Mine.";
            new Item(AvailableItems.AluminiumOreMap, -1);
            return StructuresBuilder.AluminiumMine();
        }

        [ActionRequirement(AvailableActions.BuildStructureOxygenGenerator_Handler_User_Paramless)]
        private bool BuildStructureOxygenGeneratorRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f && ColonyContext.Current.Turn.CountItems(AvailableItems.Aluminium) > 25;
        }

        [ActionProcedure(AvailableActions.BuildStructureOxygenGenerator_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildStructureOxygenGeneratorProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, -Stats.Efficiency, 0, 0, 0));
            res = $"Engineer {Name} has built Oxygen Generator.";
            new Item(AvailableItems.Aluminium, -25);
            return StructuresBuilder.OxygenGenerator();
        }

        public Engineer (SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
