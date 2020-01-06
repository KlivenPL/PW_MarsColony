using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Logger;
using System;

namespace MarsColonyEngine.Business.Colonizers {
    [Serializable]
    public struct ColonizerStats : IStats {
        public float HP { get; private set; }
        public float MaxHP { get; private set; }
        public float Efficiency { get; private set; }
        public float Hunger { get; private set; }
        public float OxygenUsage { get; private set; }
        public float Comfort { get; private set; }

        public ColonizerStats (float hP, float maxHp, float efficiency, float hunger, float oxygenUsage, float comfort) {
            HP = hP;
            MaxHP = maxHp;
            Efficiency = efficiency;
            Hunger = hunger;
            OxygenUsage = oxygenUsage;
            Comfort = comfort;
        }

        public IStats Add (IStats other) {
            if (other is ColonizerStats == false) {
                KLogger.Log.Error($"Cannot add StructureStats to {other.GetType()}");
                return default;
            }
            var otherColonizerStats = (ColonizerStats)other;
            return new ColonizerStats(
                hP: this.HP + otherColonizerStats.HP,
                maxHp: this.MaxHP + otherColonizerStats.MaxHP,
                efficiency: this.Efficiency + otherColonizerStats.Efficiency,
                hunger: this.Hunger + otherColonizerStats.Hunger,
                oxygenUsage: this.OxygenUsage + otherColonizerStats.OxygenUsage,
                comfort: this.Comfort + otherColonizerStats.Comfort
            );
        }

        public override string ToString () {
            return $"HP: {HP} / {MaxHP} pts\nEfficiency: {Efficiency}%\nHunger: {Hunger} kcal/day\nOxygen usage: {OxygenUsage} kg/day\nComfort {Comfort}%";
        }
    }
}
