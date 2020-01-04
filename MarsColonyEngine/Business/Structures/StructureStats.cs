using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Logger;

namespace MarsColonyEngine.Business.Structures {
    public struct StructureStats : IStats {
        public float HP { get; private set; }
        public float Efficiency { get; private set; }

        public IStats Add (IStats other) {
            if (other is StructureStats == false) {
                KLogger.Log.Error($"Cannot add StructureStats to {other.GetType()}");
                return default;
            }
            var otherStructureStats = (StructureStats)other;
            return new StructureStats() {
                Efficiency = this.Efficiency + otherStructureStats.Efficiency,
                HP = this.HP + otherStructureStats.HP,
            };
        }

        public StructureStats (float HP, float Efficiency) {
            this.HP = HP;
            this.Efficiency = Efficiency;
        }

        public override string ToString () {
            return $"HP: {HP} pts\nEfficiency: {Efficiency}";
        }
    }
}
