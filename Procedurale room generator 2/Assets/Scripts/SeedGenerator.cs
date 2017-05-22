using UnityEngine;

public class SeedGenerator
{
	private static Random.State seedGenerator;
	private static bool seedGeneratorInitialized = false;

	public static int GenerateSeed()
	{
		var tmp = Random.state;
		if (!seedGeneratorInitialized)
		{
			Random.InitState((int)System.DateTime.Now.Ticks);
			seedGenerator = Random.state;
			seedGeneratorInitialized = true;
		}
		Random.state = seedGenerator;
		int generatedSeed = Random.Range(int.MinValue, int.MaxValue);
		seedGenerator = Random.state;
		Random.state = tmp;
		return generatedSeed;
	}
}
