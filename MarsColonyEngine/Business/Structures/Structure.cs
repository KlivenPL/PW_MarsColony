using ExtentionMethods;
using MarsColonyEngine.Business.Colonizers;
using MarsColonyEngine.Business.Items;
using MarsColonyEngine.Business.Simulation;
using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Logger;
using MarsColonyEngine.Technical;
using MarsColonyEngine.Technical.Misc;
using System;
using System.Runtime.Serialization;

namespace MarsColonyEngine.Business.Structures {
    [Serializable]
    public class Structure : Registrator, IDestructable, IColonizerStatsAffector, IColonyStatsAffector, IOnFirstTurnStartedRec, IOnTurnStartedRec, ISerializable {
        public AvailableStructures StructureEnum { get; private set; }
        public bool IsAlive { get { return Stats.HP > 0; } }
        public void AffectHp (float signeDeltaHp) {
            if (signeDeltaHp == 0)
                return;
            Stats = (StructureStats)Stats.Add(new StructureStats(signeDeltaHp + Stats.HP >= 0 ? signeDeltaHp : -Stats.HP, 0));
            KLogger.Log.Message("Structure " + Name + " HP affected: " + signeDeltaHp + ", current value: " + Stats.HP);
            if (IsAlive == false) {
                Destroy();
                KLogger.Log.Message("Structure " + Name + " has been destroyed!");
            }
        }
        public Action Destroy => () => {
            Unregister();
        };
        public StructureStats Stats { get; private set; } = new StructureStats();

        public ColonizerStats BaseColonizerStatsAffect { get; private set; } = new ColonizerStats();

        public ColonizerStats DeltaDayColonizerStatsAffect { get; private set; } = new ColonizerStats();

        public ColonyStats BaseColonyStatsAffect { get; private set; } = new ColonyStats();

        public ColonyStats DeltaDayColonyStatsAffect { get; private set; } = new ColonyStats();

        //public Structure (Structure pattern) {
        //    Name = pattern.Name;
        //    Stats = pattern.Stats;
        //    BaseColonyStatsAffect = pattern.BaseColonyStatsAffect;
        //    DeltaDayColonyStatsAffect = pattern.DeltaDayColonyStatsAffect;
        //    BaseColonizerStatsAffect = pattern.BaseColonizerStatsAffect;
        //    DeltaDayColonizerStatsAffect = pattern.DeltaDayColonizerStatsAffect;
        //}

        internal Structure (AvailableStructures structureEnum, StructureStats stats, ColonyStats baseColonyStatsAffect, ColonyStats deltaDayColonyStatsAffect, ColonizerStats baseColonizerStatsAffect, ColonizerStats deltaDayColonizerStatsAffect) {
            StructureEnum = structureEnum;
            Name = structureEnum.ToString().SplitCamelCase();
            Stats = stats;
            BaseColonyStatsAffect = baseColonyStatsAffect;
            DeltaDayColonyStatsAffect = deltaDayColonyStatsAffect;
            BaseColonizerStatsAffect = baseColonizerStatsAffect;
            DeltaDayColonizerStatsAffect = deltaDayColonizerStatsAffect;
        }

        public void OnFirstTurnStarted () {
            Register();
        }

        public void OnTurnStarted () {
            switch (StructureEnum) {
                case AvailableStructures.AluminiumMine:
                    new Item(AvailableItems.Aluminium, 5);
                    break;
                default:
                    break;
            }
        }

        public void GetObjectData (SerializationInfo info, StreamingContext context) {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(StructureEnum), StructureEnum);
            info.AddValue(nameof(Stats), Stats);
            info.AddValue(nameof(BaseColonizerStatsAffect), BaseColonizerStatsAffect);
            info.AddValue(nameof(DeltaDayColonizerStatsAffect), DeltaDayColonizerStatsAffect);
            info.AddValue(nameof(BaseColonyStatsAffect), BaseColonyStatsAffect);
            info.AddValue(nameof(DeltaDayColonyStatsAffect), DeltaDayColonyStatsAffect);
        }

        public Structure (SerializationInfo info, StreamingContext context) {
            Name = info.GetString(nameof(Name));
            StructureEnum = (AvailableStructures)info.GetValue(nameof(StructureEnum), typeof(AvailableStructures));
            Stats = (StructureStats)info.GetValue(nameof(Stats), typeof(StructureStats));
            BaseColonizerStatsAffect = (ColonizerStats)info.GetValue(nameof(BaseColonizerStatsAffect), typeof(ColonizerStats));
            DeltaDayColonizerStatsAffect = (ColonizerStats)info.GetValue(nameof(DeltaDayColonizerStatsAffect), typeof(ColonizerStats));
            BaseColonyStatsAffect = (ColonyStats)info.GetValue(nameof(BaseColonyStatsAffect), typeof(ColonyStats));
            DeltaDayColonyStatsAffect = (ColonyStats)info.GetValue(nameof(DeltaDayColonyStatsAffect), typeof(ColonyStats));
        }
    }
}
