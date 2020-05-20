using System;

namespace RandomDice
{
    [Serializable]
    public class Phase
    {
        public Phase()
        {
        }

        public Phase(int phaseNumber, int spawnAmount, float spawnInterval, float offsetPerSecond)
        {
            this.phaseNumber = phaseNumber;
            this.spawnAmount = spawnAmount;
            this.spawnInterval = spawnInterval;
            this.offsetPerSecond = offsetPerSecond;
        }

        public int phaseNumber;
        public int spawnAmount;
        public float spawnInterval;
        public float offsetPerSecond;
    }
}