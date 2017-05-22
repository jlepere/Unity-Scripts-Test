using System;

[Serializable]
public class IntRange
{
    public int rangeMin, rangeMax;

    public IntRange(int rangeMin, int rangeMax)
    {
        this.rangeMin = rangeMin;
        this.rangeMax = rangeMax;
    }

    public int Random
    {
        get { return UnityEngine.Random.Range(rangeMin, rangeMax); }
    }
}
