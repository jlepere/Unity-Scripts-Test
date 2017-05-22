using UnityEngine;

public class LevelGenerator
{
    private int levelWidth, levelHeight, levelSeed;
    private int[,] levelData;
    private PerlinNoise perlinNoise;

    public LevelGenerator(int levelWidth, int levelHeight)
    {
        this.levelWidth = levelWidth;
        this.levelHeight = levelHeight;
    }

    public int[,] GetLevelData
    {
        get { return levelData; }
    }

    public void GenerateWorldSeed()
    {
        levelSeed = SeedGenerator.GenerateSeed();
        perlinNoise = new PerlinNoise(levelSeed);
        Random.InitState(levelSeed);
        Debug.Log("levelSeed = " + levelSeed);
    }

    public void GenerateLevelData(int levelGroundOffset)
    {
        levelData = new int[levelWidth, levelHeight];
        for (int x = 0; x < levelWidth; x++)
        {
            int groundNoise = levelGroundOffset - perlinNoise.GetNoise((int)x / 2, 10);
            for (int y = 0; y < levelHeight; y++)
                if (y >= groundNoise)
                    levelData[x, y] = 1;
        }
    }

    public void GenerateLevelRooms(int levelGroundOffset, int numRooms, IntRange roomWidthRange, IntRange roomHeightRange)
    {
        while (numRooms > 0)
        {
            int roomWidth = roomWidthRange.Random;
            int roomHeight = roomHeightRange.Random;
            int roomX = Random.Range(1, levelWidth - roomWidth - 1);
            int roomY = Random.Range(levelGroundOffset + 5, levelHeight - roomHeight - 1);
            Debug.Log(string.Format("roomX {0} roomY {1} roomWidth {2} roomHeight {3}", roomX, roomY, roomWidth, roomHeight));
            if (CheckRectToGrid(roomX - 1, roomY - 1, roomWidth + 2, roomHeight + 2))
            {
                AddRectToGrid(roomX, roomY, roomWidth, roomHeight);
                numRooms--;
            }
        }
    }

    private bool CheckRectToGrid(int rectX, int rectY, int rectWidth, int rectHeight)
    {
        for (int x = rectX; x < rectX + rectWidth; x++)
            for (int y = rectY; y < rectY + rectHeight; y++)
                if (levelData[x, y] == 0)
                    return false;
        return true;
    }

    private void AddRectToGrid(int rectX, int rectY, int rectWidth, int rectHeight)
    {
        for (int x = rectX; x < rectX + rectWidth; x++)
            for (int y = rectY; y < rectY + rectHeight; y++)
                levelData[x, y] = 0;
    }
}
