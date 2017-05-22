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
		int size = 16;
		float noise = 0;
		range /= 2;
		while (size > 0)
		{
			int index = x / size;
			float prog = (x % size) / (size * 1f);
			float leftRandom = RandomNoise(index, range);
			float rightRandom = RandomNoise(index + 1, range);
			noise += (1 - prog) * leftRandom + prog * rightRandom;
			size /= 2;
			range /= 2;
			range = Mathf.Max(1, range);
		}
		return (int)Mathf.Round(noise);
	}
}
