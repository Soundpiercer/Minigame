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
            new Phase(5, 30, 0.6f, 0.095f)
        };
    }
}