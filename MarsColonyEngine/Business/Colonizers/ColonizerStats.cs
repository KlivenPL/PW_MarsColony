using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Logger;

namespace MarsColonyEngine.Business.Colonizers {
    public struct ColonizerStats : IStats {
        public float HP { get; private set; }
        public float Efficiency { get; private set; }
        public float Hunger { get; private set; }
        public float Space { get; private set; }
        public float Comfort { get; private set; }

        public ColonizerStats (float hP, float efficiency, float hunger, float space, float comfort) {
            HP = hP;
            Efficiency = efficiency;
            Hunger = hunger;
            Space = space;
            Comfort = comfort;
        }

        public IStats Add (IStats other) {
            if (other is ColonizerStats == false) {
                KLogger.Log.Error($"Cannot add StructureStats to {other.GetType()}");
                return default;
            }
            var otherColonizerStats = (ColonizerStats)other;
            return new ColonizerStats() {
                Efficiency = this.Efficiency + otherColonizerStats.Efficiency,
                HP = this.HP + otherColonizerStats.HP,
                Hunger = this.Hunger + otherColonizerStats.Hunger,
                Space = this.Space + otherColonizerStats.Space,
                Comfort = this.Comfort + otherColonizerStats.Comfort,
            };
        }

        public override string ToString () {
            return $"HP: {HP} pts\nEfficiency: {Efficiency}%\nHunger: {Hunger} kcal\nSpace: {Space}%\nComfort {Comfort}%";
        }
    }
}
