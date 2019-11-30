using MarsColonyEngine.ColonyActions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MarsColonyEngine.Colonizers {
    public class Explorer : Colonizer {
        public override AvailableActions[] GetAvailableActions () {
            return base.GetAvailableActions().Concat(new AvailableActions[] {
                
            }).ToArray();
        }
    }
}
