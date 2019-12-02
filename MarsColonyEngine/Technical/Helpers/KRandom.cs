using System;

namespace MarsColonyEngine.Helpers {
    public static class KRandom {
        private static Random _rand = null;
        private static Random Rand {
            get {
                return _rand != null ? _rand : (_rand = new Random());
            }
        }
        public static void Seed (int seed) {
            _rand = new Random(seed);
        }
        public static int Int (int min = int.MinValue, int max = int.MaxValue) {
            return Rand.Next(min, max);
        }
        public static float Float (float min = float.MinValue, float max = float.MaxValue) {
            return (float)Rand.NextDouble() * (max - min) + min;
        }

        public static float Float01 () {
            return (float)Rand.NextDouble();
        }

        public static bool Bool (int chancesPercent = 50) {
            return Float01() <= chancesPercent / 100f ? true : false;
        }

        public static string[] nameList = new string[] {
            "Cristal Swopes","Remedios Pyatt","Nakisha Vandenbosch","Ilse Carrillo","Lea Holloman","Foster Viger","Camelia Coss","Keira Stiefel","Ji Rochford","Susana Schwindt","Christoper March","Bambi Rozzell","Magdalene Helbert","Grayce Hashimoto","Kristeen Giordano","Muoi Scouten","Basilia Qualls","Latashia Plewa","Ivana Gordon","Oscar Hayne","Li Wieck","Chantelle Nice","Lizzette Whalley","Phyliss Labree","Arvilla Luiz","Marlyn Genna","Wendi Bills","Karly Millner","Alton Guillermo","Carlene Tomasi","Nakesha Real","Marvis Hochmuth","Tomasa Hanford","Curtis Lehman","Vickey Shuman","Stephaine Cuesta","Tracee Warnick","Kia Haigler","Janna Palin","Tania Huwe","Lesha Hegwood","Caron Rabe","Les Raab","Kymberly Meese","Andreas Barthel","Marni Mcintosh","Brenda Orum","Fay Rich","Dania Minchew","Darby Bohling"
        };

        public static string Name () {
            return nameList[Int(0, nameList.Length - 1)];
        }
    }
}
