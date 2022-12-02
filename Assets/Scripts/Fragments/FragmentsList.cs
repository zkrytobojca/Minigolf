using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FragmentsList
{
    public List<GameObject> deadEnds;
    public List<GameObject> straightLines;
    public List<GameObject> corners;
    public List<GameObject> Tshaped;
    public List<GameObject> Xshaped;
    public List<GameObject> detached;

    public FragmentsList()
    {
        deadEnds = new List<GameObject>();
        straightLines = new List<GameObject>();
        corners = new List<GameObject>();
        Tshaped = new List<GameObject>();
        Xshaped = new List<GameObject>();
        detached = new List<GameObject>();
    }

    public List<GameObject> GetFragmentsBasedOnDirections(Directions directions)
    {
        switch(directions.GetNumberOfActive())
        {
            case 0:
                return detached;
            case 1:
                return deadEnds;
            case 2:
                if ((directions.positiveX ? 1 : 0) + (directions.negativeX ? 1 : 0) == 2 || (directions.positiveZ ? 1 : 0) + (directions.negativeZ ? 1 : 0) == 2) return straightLines;
                else return corners;
            case 3:
                return Tshaped;
            case 4:
                return Xshaped;
            default:
                return new List<GameObject>();
        }
    }

    public Quaternion GetQuaternionBasedOnDirections(Directions directions)
    {
        switch (directions.GetNumberOfActive())
        {
            case 1:
                //deadEnds
                if (directions.positiveZ) return Quaternion.identity;
                if (directions.positiveX) return Quaternion.Euler(0, 90, 0);
                if (directions.negativeZ) return Quaternion.Euler(0, 180, 0);
                if (directions.negativeX) return Quaternion.Euler(0, 270, 0);
                break;
            case 2:
                //straight
                if (directions.positiveZ && directions.negativeZ) return Quaternion.identity;
                if (directions.positiveX && directions.negativeX) return Quaternion.Euler(0, 90, 0);
                //corners
                if (directions.positiveZ && directions.positiveX) return Quaternion.identity;
                if (directions.positiveX && directions.negativeZ) return Quaternion.Euler(0, 90, 0);
                if (directions.negativeZ && directions.negativeX) return Quaternion.Euler(0, 180, 0);
                if (directions.negativeX && directions.positiveZ) return Quaternion.Euler(0, 270, 0);

                break;
            case 3:
                //Tshaped
                if (!directions.negativeX) return Quaternion.identity;
                if (!directions.positiveZ) return Quaternion.Euler(0, 90, 0);
                if (!directions.positiveX) return Quaternion.Euler(0, 180, 0);
                if (!directions.negativeZ) return Quaternion.Euler(0, 270, 0);
                break;
            case 4:
                //Xshaped
                return Quaternion.identity;
            default:
                return Quaternion.identity;
        }
        return Quaternion.identity;
    }
}
