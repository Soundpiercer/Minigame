using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RandomDice
{
    public static class RandomDiceData
    {
        public static Phase[] phases =
        {
            new Phase(1, 10, 1.0f, 0.075f),
            new Phase(2, 15, 0.9f, 0.08f),
            new Phase(3, 20, 0.8f, 0.085f),
            new Phase(4, 25, 0.7f, 0.09f),
            new Phase(5, 30, 0.6f, 0.095f),
            new Phase(6, 32, 0.56f, 0.105f),
            new Phase(7, 35, 0.53f, 0.115f),
            new Phase(8, 36, 0.5f, 0.125f),
            new Phase(9, 40, 0.48f, 0.135f),
            new Phase(10, 45, 0.45f, 0.15f)
        };

        public static ShooterDiceProperty[] shooterDiceProperties =
        {
            new ShooterDiceProperty(ShooterDiceType.Blue, 1, 1, 60, 0.6f, new Color(0, 0, 1, 1), new Color(1, 1, 0, 0)),
            new ShooterDiceProperty(ShooterDiceType.Blue, 1, 2, 600, 0.6f, new Color(0, 0, 1, 1), new Color(1, 1, 0, 0)),
            new ShooterDiceProperty(ShooterDiceType.Blue, 1, 3, 6000, 0.6f, new Color(0, 0, 1, 1), new Color(1, 1, 0, 0)),
            new ShooterDiceProperty(ShooterDiceType.Red, 1, 1, 30, 0.3f, new Color(1, 0, 0, 1), new Color(0, 1, 0, 0)),
            new ShooterDiceProperty(ShooterDiceType.Red, 1, 2, 30, 0.3f, new Color(1, 0, 0, 1), new Color(0, 1, 0, 0)),
            new ShooterDiceProperty(ShooterDiceType.Red, 1, 3, 30, 0.3f, new Color(1, 0, 0, 1), new Color(0, 1, 0, 0))
        };
    }
}