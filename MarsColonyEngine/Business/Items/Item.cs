using ExtentionMethods;
using MarsColonyEngine.Logger;
using MarsColonyEngine.Technical;
using MarsColonyEngine.Technical.Misc;
using System;

namespace MarsColonyEngine.Business.Items {
    public class Item : Registrator, IDestructable {
        public AvailableItems ItemEnum { get; private set; }
        public int Amount { get; private set; }

        public Item (AvailableItems itemEnum, int amount) {
            ItemEnum = itemEnum;
            Name = itemEnum.ToString().SplitCamelCase();
            Amount = amount;
            Register();
        }
        private Item () { }

        public bool IsAlive => Amount > 0;

        public Action Destroy => () => {
            Unregister();
        };

        public void AffectHp (float signeDeltaHp) {
            int amountToAffect = (int)signeDeltaHp;
            if (amountToAffect < 0 && amountToAffect + Amount < 0) {
                KLogger.Log.Error($"Cannot remove more items ({Name}, {amountToAffect}) than exist ({Amount})");
                return;
            }
            Amount += amountToAffect;

            if (IsAlive == false)
                Destroy();
        }
    }

    public enum AvailableItems {
        Aluminium,
        UnexaminedSample,
        AluminiumOreMap,
        ArmoredBedroomBlueprint,

    }
}
