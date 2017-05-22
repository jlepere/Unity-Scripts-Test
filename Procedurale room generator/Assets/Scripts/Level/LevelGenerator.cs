using UnityEngine;

public class LevelGenerator
{
	private int levelWidth;
	private int levelHeight;
	private int[,] levelData;
	private Vector2 levelSpawn;

	public LevelGenerator(int levelWidth, int levelHeight, int levelSeed)
	{
		this.levelWidth = levelWidth;
		this.levelHeight = levelHeight;
		if (levelSeed != -1)
			Random.InitState(levelSeed);
		Debug.Log("Level seed : " + levelSeed);
		levelSpawn = new Vector2(Random.Range(1, levelWidth - 2), 0);
		Debug.Log("Level spawn : " + levelSpawn.x + ":" + levelSpawn.y);
		levelData = new int[levelWidth, levelHeight];
		GenExitPath((int)levelSpawn.x, (int)levelSpawn.y, -1, 0);
		ViewData();
	}

	public int[,] GetLevelData
	{
		get { return levelData; }
	}

	public Vector2 GetLevelSpawn
	{
		get { return levelSpawn; }
	}

	private void GenExitPath(int x, int y, int dirRoom, int nbRoom)
	{
		int top = 0;
		if (nbRoom == 0 || dirRoom == -1)
			dirRoom = Random.Range(0, 10);
		if (nbRoom > levelWidth - 2)
			top = 5;
		else if (nbRoom > (levelWidth / 3) - 1)
			top = Random.Range(0, 10);
		if (nbRoom == 0 && y != 0)
			levelData[x, y] = 3;
		else
			levelData[x, y] = 1;
		if (top <= 4 && (dirRoom <= 4 && x != 0 || dirRoom >= 5 && x == levelWidth - 1 && nbRoom == 0))
			GenExitPath(x - 1, y, 0, nbRoom + 1);
		else if (top <= 4 && (dirRoom >= 5 && x != levelWidth - 1 || dirRoom <= 4 && x == 0 && nbRoom == 0))
			GenExitPath(x + 1, y, 5, nbRoom + 1);
		else if (y != levelHeight - 1)
		{
			levelData[x, y] = 2;
			GenExitPath(x, y + 1, -1, 0);
		}
	}

	private void ViewData()
	{
		int c = levelHeight - 1;
		while (c >= 0)
		{
			Debug.Log(levelData[0, c] + " " + levelData[1, c] + " " + levelData[2, c] + " " + levelData[3, c] + " " + levelData[4, c] + " " + levelData[5, c]);
			c--;
		}
	}
}
