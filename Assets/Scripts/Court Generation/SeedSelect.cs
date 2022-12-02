using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSelect : MonoBehaviour
{
    public string seedString;
    public int seed;
    public bool useRandomSeed = true;

    private void Awake()
    {
        UpdateSeed();
    }

    public void UpdateSeed()
    {
        if (useRandomSeed) RandomizeSeed();
        else SeedFromString();
    }

    private void RandomizeSeed()
    {
        seed = UnityEngine.Random.Range(0, 99999);
        UnityEngine.Random.InitState(seed);
    }

    private void SeedFromString()
    {
        bool integerValueInString = Int32.TryParse(seedString, out seed);
        if (integerValueInString == false) seed = seedString.GetHashCode();
        UnityEngine.Random.InitState(seed);
    }
}
