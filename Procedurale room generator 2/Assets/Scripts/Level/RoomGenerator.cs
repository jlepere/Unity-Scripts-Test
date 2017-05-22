using UnityEngine;

public class RoomGenerator
{
	private LevelManager levelManager;
	private int[,] roomData;

	public RoomGenerator(LevelManager levelManager)
	{
		this.levelManager = levelManager;
		roomData = new int[this.levelManager.roomWidth, this.levelManager.roomHeight];
	}

	public int[,] GetRoomData
	{
		get { return roomData; }
	}

	public void GenerateWalls(Room room)
	{
		for (int x = 0; x < levelManager.roomWidth; x++)
		{
			int floorHeight = levelManager.perlinNoise.GetNoise((int)((room.roomPosition.x + levelManager.levelWidth * room.roomPosition.y) * levelManager.roomWidth + x), 10);
			int ceilHeight = 1 + levelManager.perlinNoise.GetNoise((int)((room.roomPosition.x + levelManager.levelWidth * room.roomPosition.y) * levelManager.roomWidth + x), 7);
			//Debug.Log(string.Format("x : {0} floorHeight : {1}", (room.roomPosition.x + 6 * room.roomPosition.y) * 30 + x, floorHeight));
			for (int y = 0; y < levelManager.roomHeight; y++)
			{
				if (y <= floorHeight || y >= levelManager.roomHeight - ceilHeight)
					roomData[x, y] = 1;
				else
					roomData[x, y] = 0;
			}
		}
	}

	public void GenerateBorders(Room room)
	{
		if (room.roomPosition.x == 0)
			GenerateLeftBorder(room);// && room.roomPosition.x != levelManager.levelWidth - 1)
		else if (room.roomPosition.x == levelManager.levelWidth - 1)
			GenerateRightBorder(room);
	}

	private void GenerateLeftBorder(Room room)
	{
		for (int y = 0; y < levelManager.roomHeight; y++)
		{
			int wallWidth = 1 + levelManager.perlinNoise.GetNoise((int)((room.roomPosition.y + levelManager.levelHeight * room.roomPosition.x) * levelManager.roomHeight + y), 8);
			for (int x = 0; x < 6; x++)
				if (x <= wallWidth)
					roomData[x, y] = 1;
		}
	}

	private void GenerateRightBorder(Room room)
	{
		for (int y = 0; y < levelManager.roomHeight; y++)
		{
			int wallWidth = 1 + levelManager.perlinNoise.GetNoise((int)((room.roomPosition.y + levelManager.levelHeight * room.roomPosition.x) * levelManager.roomHeight + y), 8);
			for (int x = levelManager.roomWidth - 6; x < levelManager.roomWidth; x++)
				if (x >= levelManager.roomWidth - wallWidth)
					roomData[x, y] = 1;
		}
	}
}
