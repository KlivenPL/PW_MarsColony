﻿using MarsColonyEngine.ColonyActions;
using MarsColonyEngine.Misc;

namespace MarsColonyEngine.Colonizers {
    public abstract class Colonizer : ActionHandlerBase, IBasicParameters {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public float HP { get; private set; }
        public float Efficiency { get; private set; }

        public float Hunger { get; private set; }
        public float Space { get; private set; }
        public float Comfort { get; private set; }


        public override bool IsActive => HP > 0 && Efficiency > 0;
        public override AvailableActions[] GetAvailableActions () {
            return new AvailableActions[]{

            };
        }
    }
}
