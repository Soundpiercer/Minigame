using System;
using UnityEngine;

namespace RandomDice
{
    public enum ShooterDiceType
    {
        Blue = 0,
        Red = 1,
    }

    [Serializable]
    public class ShooterDiceProperty
    {
        public ShooterDiceProperty(ShooterDiceType shooterDiceType, int tier, int level, int power, float attackInterval, Color bodyColor, Color beamColor)
        {
            this.shooterDiceType = shooterDiceType;
            this.tier = tier;
            this.level = level;
            this.power = power;
            this.attackInterval = attackInterval;
            this.bodyColor = bodyColor;
            this.beamColor = beamColor;
        }

        public ShooterDiceType shooterDiceType;
        public int tier;
        public int level;
        public int power;
        public float attackInterval;
        public Color bodyColor;
        public Color beamColor;
    }
}