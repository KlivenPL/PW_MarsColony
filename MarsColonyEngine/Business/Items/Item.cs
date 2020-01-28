using ExtentionMethods;
using MarsColonyEngine.Logger;
using MarsColonyEngine.Technical;
using MarsColonyEngine.Technical.Misc;
using System;
using System.Runtime.Serialization;

namespace MarsColonyEngine.Business.Items {
    [Serializable]
    public class Item : Registrator, IDestructable, ISerializable {
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

        public void GetObjectData (SerializationInfo info, StreamingContext context) {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(Amount), Amount);
            info.AddValue(nameof(ItemEnum), ItemEnum);
        }

        public Item (SerializationInfo info, StreamingContext context) {
            Name = info.GetString(nameof(Name));
            Amount = (int)info.GetValue(nameof(Amount), typeof(int));
            ItemEnum = (AvailableItems)info.GetValue(nameof(ItemEnum), typeof(AvailableItems));
            Register();
        }
    }

    public enum AvailableItems {
        Aluminium,
        UnexaminedSample,
        AluminiumOreMap,
        ArmoredBedroomBlueprint,
        PotatoSeed,
        RepairStationBlueprint,
    }
}
