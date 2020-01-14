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
                AvailableActions.BuildStructureOxygenGenerator_Handler_User_Paramless,
                AvailableActions.BuildStructureResearchStation_Handler_User_Paramless,
                AvailableActions.RepairStructure_Handler_User_Args,
                AvailableActions.BuildStructureArmoredBedroom_Handler_User_Paramless,
                AvailableActions.BuildStructureRepairStation_Handler_User_Paramless,
            }).ToArray();
        }

        [ActionRequirement(AvailableActions.BuildStructureSimpleShelter_Handler_User_Paramless)]
        private bool BuildStructureSimpleShelterRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f && ColonyContext.Current.Turn.CountItems(AvailableItems.Aluminium) > 50;
        }

        [ActionProcedure(AvailableActions.BuildStructureSimpleShelter_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildStructureSimpleShelterProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, 0, -Stats.Efficiency, 0, 0, 0));
            res = $"Engineer {Name} has built Simple Shelter.";
            new Item(AvailableItems.Aluminium, -50);
            return StructuresBuilder.SimpleShelter();
        }

        [ActionRequirement(AvailableActions.BuildStructurePotatoFarm_Handler_User_Paramless)]
        private bool BuildStructurePotatoFarmRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f && ColonyContext.Current.Turn.CountItems(AvailableItems.Aluminium) >= 25 && ColonyContext.Current.Turn.CountItems(AvailableItems.PotatoSeed) >= 5;
        }

        [ActionProcedure(AvailableActions.BuildStructurePotatoFarm_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildStructurePotatoFarmProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, 0, -Stats.Efficiency, 0, 0, 0));
            res = $"Engineer {Name} has built Potato Farm.";
            new Item(AvailableItems.Aluminium, -25);
            new Item(AvailableItems.PotatoSeed, -5);
            return StructuresBuilder.PotatoFarm();
        }

        [ActionRequirement(AvailableActions.BuildStructureAluminiumMine_Handler_User_Paramless)]
        private bool BuildStructureAluminiumMineRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f && ColonyContext.Current.Turn.CountItems(AvailableItems.AluminiumOreMap) > 0;
        }

        [ActionProcedure(AvailableActions.BuildStructureAluminiumMine_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildStructureAluminiumMineProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, 0, -Stats.Efficiency, 0, 0, 0));
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
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, 0, -Stats.Efficiency, 0, 0, 0));
            res = $"Engineer {Name} has built Oxygen Generator.";
            new Item(AvailableItems.Aluminium, -25);
            return StructuresBuilder.OxygenGenerator();
        }

        [ActionRequirement(AvailableActions.BuildStructureResearchStation_Handler_User_Paramless)]
        private bool BuildResearchStationRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f && ColonyContext.Current.Turn.CountItems(AvailableItems.Aluminium) >= 10
                && ColonyContext.Current.Structures.Any(e => e.StructureEnum == AvailableStructures.ResearchStation) == false;
        }

        [ActionProcedure(AvailableActions.BuildStructureResearchStation_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildResearchStationProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, 0, -Stats.Efficiency, 0, 0, 0));
            res = $"Engineer {Name} has built ResearchStation.";
            new Item(AvailableItems.Aluminium, -10);
            var researchStation = StructuresBuilder.ResearchStation();
            Simulation.Simulator.Current.NextTurn();
            return researchStation;
        }

        [ActionRequirement(AvailableActions.BuildStructureArmoredBedroom_Handler_User_Paramless)]
        private bool BuildArmoredBedroomRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f && ColonyContext.Current.Turn.CountItems(AvailableItems.Aluminium) >= 75
                && ColonyContext.Current.Turn.CountItems(AvailableItems.ArmoredBedroomBlueprint) > 0;
        }

        [ActionProcedure(AvailableActions.BuildStructureArmoredBedroom_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildArmoredBedroomProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, 0, -Stats.Efficiency, 0, 0, 0));
            res = $"Engineer {Name} has built Armored Bedroom.";
            new Item(AvailableItems.Aluminium, -75);
            new Item(AvailableItems.ArmoredBedroomBlueprint, -1);
            var armoredBedroom = StructuresBuilder.ArmoredBedroom();
            return armoredBedroom;
        }

        [ActionRequirement(AvailableActions.BuildStructureRepairStation_Handler_User_Paramless)]
        private bool BuildRepairStationRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f && ColonyContext.Current.Turn.CountItems(AvailableItems.Aluminium) >= 50
                && ColonyContext.Current.Turn.CountItems(AvailableItems.RepairStationBlueprint) > 0
                && ColonyContext.Current.Structures.Any(e => e.StructureEnum == AvailableStructures.RepairStation) == false;
        }

        [ActionProcedure(AvailableActions.BuildStructureRepairStation_Handler_User_Paramless, typeof(Engineer))]
        private Structure BuildRepairStationProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, 0, -Stats.Efficiency, 0, 0, 0));
            res = $"Engineer {Name} has built Repair Station";
            new Item(AvailableItems.Aluminium, -50);
            new Item(AvailableItems.RepairStationBlueprint, -1);
            var repairStation = StructuresBuilder.RepairStation();
            Simulation.Simulator.Current.NextTurn();
            return repairStation;
        }

        [ActionRequirement(AvailableActions.RepairStructure_Handler_User_Args)]
        private bool RepairStructureRequirement (ref string res) {
            if (ColonyContext.Current.Structures.Any(e => e.StructureEnum == AvailableStructures.RepairStation)) {
                if (this.Stats.Efficiency < 50f)
                    return false;
            } else {
                if (this.Stats.Efficiency < 100f)
                    return false;
            }
            return IsAlive && ColonyContext.Current.Turn.CountItems(AvailableItems.Aluminium) > 20
                && ColonyContext.Current.Structures.Any() && ColonyContext.Current.Structures.Any(e => e.Stats.HP != e.Stats.MaxHP);
        }

        [ActionProcedure(AvailableActions.RepairStructure_Handler_User_Args, typeof(Engineer))]
        private void RepairStructureProcedure (ref string res, Structure structure) {
            if (ColonyContext.Current.Structures.Any(e => e.StructureEnum == AvailableStructures.RepairStation)) {
                Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, 0, -50f, 0, 0, 0));
            } else {
                Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, 0, -Stats.Efficiency, 0, 0, 0));
            }
            var cost = (int)(20f * (1 - structure.Stats.HP / structure.Stats.MaxHP));
            res = $"Engineer {Name} has repaired {structure.Name}. It took {cost}x Aluminium.";
            new Item(AvailableItems.Aluminium, -cost);
            structure.AffectHp(structure.Stats.MaxHP - structure.Stats.HP);
        }

        public Engineer (SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
