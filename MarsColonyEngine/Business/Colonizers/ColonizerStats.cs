using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Logger;

namespace MarsColonyEngine.Business.Colonizers {
    public class ColonizerStats : IStats {
        public float HP { get; private set; }
        public float Efficiency { get; private set; }
        public float Hunger { get; private set; }
        public float Space { get; private set; }
        public float Comfort { get; private set; }

        public ColonizerStats () { }
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
            return new ColonizerStats(
                hP: this.HP + otherColonizerStats.HP,
                efficiency: this.Efficiency + otherColonizerStats.Efficiency,
                hunger: this.Hunger + otherColonizerStats.Hunger,
                space: this.Space + otherColonizerStats.Space,
                comfort: this.Comfort + otherColonizerStats.Comfort
            );
        }

        public override string ToString () {
            return $"HP: {HP} pts\nEfficiency: {Efficiency}%\nHunger: {Hunger} kcal\nSpace: {Space}%\nComfort {Comfort}%";
        }
    }
}
