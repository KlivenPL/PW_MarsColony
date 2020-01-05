using ExtentionMethods;
using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Items;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MarsColonyEngine.Colonizers {

    [Serializable]
    public class Explorer : Colonizer, ISerializable {
        public Explorer (string name, ColonizerStats stats, ColonyStats baseColonyStatsAffect, ColonyStats deltaDayColonyStatsAffect) : base(name, stats, baseColonyStatsAffect, deltaDayColonyStatsAffect) {
        }

        public override AvailableActions[] GetAvailableActions () {
            return base.GetAvailableActions().Concat(new AvailableActions[] {
                AvailableActions.Explore_Handler_User_Paramless,
            }).ToArray();
        }

        [ActionRequirement(AvailableActions.Explore_Handler_User_Paramless)]
        private bool ExploreRequirement (ref string res) {
            return IsAlive && this.Stats.Efficiency >= 100f;
        }

        [ActionProcedure(AvailableActions.Explore_Handler_User_Paramless, typeof(Explorer))]
        private Item ExploreProcedure (ref string res) {
            Stats = (ColonizerStats)Stats.Add(new ColonizerStats(0, -100, 0, 0, 0));
            var foundSomething = KRandom.Bool(40);
            if (foundSomething == false) {
                res = $"Explorer {Name} did not find anything.";
                return null;
            }

            if (KRandom.Bool(40)) {
                res = $"Explorer {Name} has found an Unexamined Sample. Research it in Scientist Station.";
                return new Item(AvailableItems.UnexaminedSample, 1);
            }
            var allExplorableItems = new List<AvailableItems>();

            // var allBlueprints = ((AvailableItems[])Enum.GetValues(typeof(AvailableItems))).Where(e => e.ToString().Contains("Blueprint")).ToList();
            var allMaps = ((AvailableItems[])Enum.GetValues(typeof(AvailableItems))).Where(e => e.ToString().Contains("Map")).ToList();

            //  allExplorableItems.AddRange(allBlueprints);
            allExplorableItems.AddRange(allMaps);

            var exploredItem = allExplorableItems[KRandom.Int(0, allExplorableItems.Count)];

            res = $"Explorer {Name} has found {exploredItem.ToString().SplitCamelCase()}.";
            return new Item(exploredItem, 1);
        }

        public Explorer (SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
