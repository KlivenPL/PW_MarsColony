using ExtentionMethods;
using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Items;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Business.Structures;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Context;
using MarsColonyEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MarsColonyEngine.Colonizers {
    [Serializable]
    public class Scientist : Colonizer, ISerializable {
        public Scientist (string name, ColonizerStats stats, ColonyStats baseColonyStatsAffect, ColonyStats deltaDayColonyStatsAffect) : base(name, stats, baseColonyStatsAffect, deltaDayColonyStatsAffect) {
        }

        public override AvailableActions[] GetAvailableActions () {
            return base.GetAvailableActions().Concat(new AvailableActions[] {
                AvailableActions.DoResearch_Handler_User_Paramless,
            }).ToArray();
        }

        [ActionRequirement(AvailableActions.DoResearch_Handler_User_Paramless)]
        private bool DoResearchRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f
                && ColonyContext.Current.Structures.Any(e => e.StructureEnum == AvailableStructures.ResearchStation)
                && ColonyContext.Current.Turn.CountItems(Business.Items.AvailableItems.UnexaminedSample) > 0;
        }

        [ActionProcedure(AvailableActions.DoResearch_Handler_User_Paramless, typeof(Scientist))]
        private Item DoResearchProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, 0, -100, 0, 0, 0));
            new Item(AvailableItems.UnexaminedSample, -1);

            var foundSomething = KRandom.Bool(45);
            if (foundSomething == false) {
                res = $"Scientist {Name} did not discover anything.";
                return null;
            }
            var explorableItems = new List<AvailableItems>();

            var blueprints = ((AvailableItems[])Enum.GetValues(typeof(AvailableItems))).Where(e => e.ToString().Contains("Blueprint")).ToList();

            explorableItems.AddRange(blueprints);

            var discoveredItem = explorableItems[KRandom.Int(0, explorableItems.Count)];

            res = $"Scientist {Name} has discovered {discoveredItem.ToString().SplitCamelCase()}.";
            return new Item(discoveredItem, 1);
        }

        public Scientist (SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
