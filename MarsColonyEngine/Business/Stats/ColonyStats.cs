using MarsColonyEngine.Logger;

namespace MarsColonyEngine.Business.Stats {
    public struct ColonyStats : IStats {
        /// <summary>
        /// KG
        /// </summary>
        public float Oxygen { get; private set; }

        public int Population { get; private set; }

        public int PopulationLimit { get; private set; }

        /// <summary>
        /// KCal
        /// </summary>
        public float Food { get; private set; }

        // public ColonyStats () { }
        public ColonyStats (float oxygen, int population, int populationLimit, float food) {
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
            return new ColonyStats(
                oxygen: Oxygen + otherColStats.Oxygen,
                population: Population + otherColStats.Population,
                populationLimit: PopulationLimit + otherColStats.PopulationLimit,
                food: Food + otherColStats.Food
            );
        }
        public static ColonyStats operator + (ColonyStats a, ColonyStats b) {
            var add = a.Add(b);
            return (ColonyStats)add;
        }

        public override string ToString () {
            return $"Oxygen: {Oxygen} kg\nPopulation: {Population}\nPopulation limit: {PopulationLimit}\nFood: {Food} kcal";
        }
    }
}
