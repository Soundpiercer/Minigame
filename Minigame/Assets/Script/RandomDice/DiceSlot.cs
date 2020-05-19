using System;
using UnityEngine;

public class DiceSlot
{
    public DiceSlot()
    {
    }

    public DiceSlot(Vector3 position, bool isOccupied)
    {
        this.position = position;
        this.isOccupied = isOccupied;
    }

    public Vector3 position;
    public bool isOccupied;
}
