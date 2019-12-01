using MarsColonyEngine.Logger;

namespace MarsColonyEngine.Business.Stats {
    public struct ColonyStats : IStats {
        public int Oxygen { get; set; }

        public int Population { get; set; }

        public int PopulationLimit { get; set; }

        public int Food { get; set; }

        public ColonyStats (int oxygen, int population, int populationLimit, int food) {
            Oxygen = oxygen;
            Population = population;
            PopulationLimit = populationLimit;
            Food = food;
        }

        public IStats Add (IStats other) {
            if (other.GetType().IsEquivalentTo(typeof(ColonyStats)) == false) {
                KLogger.Log.Error($"Cannot add ColonyStats to {other.GetType()}");
                return null;
            }
            var otherColStats = (ColonyStats)other;
            return new ColonyStats {
                Oxygen = Oxygen + otherColStats.Oxygen,
                Population = Population + otherColStats.Population,
                PopulationLimit = PopulationLimit + otherColStats.PopulationLimit,
                Food = Food + otherColStats.Food,
            };
        }
        public static ColonyStats operator + (ColonyStats a, ColonyStats b) {
            var add = a.Add(b);
            return (ColonyStats)add;
        }
    }
}
