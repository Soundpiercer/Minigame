using System;
using UnityEngine;

namespace RandomDice
{
    public enum ShooterDiceType
    {
        Blue = 0,
        Red,
    }

    [Serializable]
    public class ShooterDiceProperty
    {
        public ShooterDiceProperty(ShooterDiceType shooterDiceType, int power, float attackInterval, Color bodyColor, Color beamColor)
        {
            this.shooterDiceType = shooterDiceType;
            this.power = power;
            this.attackInterval = attackInterval;
            this.bodyColor = bodyColor;
            this.beamColor = beamColor;
        }

        public ShooterDiceType shooterDiceType;
        public int power;
        public float attackInterval;
        public Color bodyColor;
        public Color beamColor;
    }
}