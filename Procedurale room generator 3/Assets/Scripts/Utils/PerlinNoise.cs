using UnityEngine;

public class PerlinNoise
{
    private int levelSeed;

    public PerlinNoise(int levelSeed)
    {
        this.levelSeed = levelSeed;
    }

    private int RandomNoise(int x, int range)
    {
        return (int)(((x * levelSeed) ^ 5) % range);
    }

    public int GetNoise(int x, int range)
    {
        range /= 2;
        float noise = 0;
        int size = 16;
        while (size > 0)
        {
            int index = x / size;
            float prog = (x % size) / (float)size;
            float leftRandom = RandomNoise(index, range);
            float rightRandom = RandomNoise(index + 1, range);
            noise += (1 - prog) * leftRandom + prog * rightRandom;
            range = Mathf.Max(1, range / 2);
            size /= 2;
        }
        return Mathf.RoundToInt(noise);
    }
}
