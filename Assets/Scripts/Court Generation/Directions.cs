using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions
{
    public bool positiveX;
    public bool negativeX;
    public bool positiveZ;
    public bool negativeZ;

    public Directions()
    {
        positiveX = false;
        negativeX = false;
        positiveZ = false;
        negativeZ = false;
    }

    public Directions(bool positiveZ, bool positiveX, bool negativeZ, bool negativeX)
    {
        this.positiveZ = positiveZ;
        this.positiveX = positiveX;
        this.negativeZ = negativeZ;
        this.negativeX = negativeX;
    }

    public int GetNumberOfActive()
    {
        return (positiveZ ? 1 : 0) + (positiveX ? 1 : 0) + (negativeZ ? 1 : 0)  + (negativeX ? 1 : 0);
    }
}
