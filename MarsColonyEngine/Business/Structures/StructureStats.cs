﻿using MarsColonyEngine.Business.Stats;
using MarsColonyEngine.Logger;
using System;

namespace MarsColonyEngine.Business.Structures {
    [Serializable]
    public struct StructureStats : IStats {

        public float MaxHP { get; private set; }
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
                MaxHP = this.MaxHP + otherStructureStats.MaxHP
            };
        }

        public StructureStats (float HP, float MaxHP, float Efficiency) {
            this.HP = HP;
            this.MaxHP = MaxHP;
            this.Efficiency = Efficiency;
        }

        public override string ToString () {
            return $"HP: {HP} / {MaxHP} pts\nEfficiency: {Efficiency}";
        }
    }
}
