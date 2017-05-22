using UnityEngine;

public class LevelGenerator
{
	private int levelWidth;
	private int levelHeight;
	private int levelSeed;
	private int [,] levelData;
	private Vector2 levelSpawn;

	public LevelGenerator(int levelWidth, int levelHeight, int levelSeed)
	{
		this.levelWidth = levelWidth;
		this.levelHeight = levelHeight;
		if ((this.levelSeed = levelSeed) == 0)
			this.levelSeed = SeedGenerator.GenerateSeed();
		levelData = new int[this.levelWidth, this.levelHeight];
	}

	public int GetLevelSeed
	{
		get { return levelSeed; }
	}

	public int[,] GetLevelData
	{
		get { return levelData; }
	}

	public Vector2 GetLevelSpawn
	{
		get { return levelSpawn; }
	}

	public void GenerateLevel()
	{
		Random.InitState(this.levelSeed);
		levelSpawn = new Vector2(Random.Range(1, levelWidth - 2), 0);
		GenerateLevelPath((int)levelSpawn.x, (int)levelSpawn.y, -1, 0);
	}

	private void GenerateLevelPath(int x, int y, int dirRoom, int nbRoom)
	{
		int topPath = 0;
		if (nbRoom == 0 || dirRoom == -1)
			dirRoom = Random.Range(0, 10);
		if (nbRoom >= levelWidth - 2)
			topPath = 5;
		else if (nbRoom > (levelWidth / 3) - 1)
			topPath = Random.Range(0, 10);
		if (nbRoom == 0 && y != 0)
			levelData[x, y] = 3;
		else
			levelData[x, y] = 1;
		if (topPath <= 4 && (dirRoom <= 4 && x != 0 || dirRoom >= 5 && x == levelWidth - 1 && nbRoom == 0))
			GenerateLevelPath(x - 1, y, 0, nbRoom + 1);
		else if (topPath <= 4 && (dirRoom >= 5 && x != levelWidth - 1 || dirRoom <= 4 && x == 0 && nbRoom == 0))
			GenerateLevelPath(x + 1, y, 5, nbRoom + 1);
		else if (y != levelHeight - 1)
		{
			levelData[x, y] = 2;
			GenerateLevelPath(x, y + 1, -1, 0);
		}
	}
}
