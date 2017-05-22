using UnityEngine;

public class Level : MonoBehaviour
{
	public int levelSeed = -1;
	public int levelWidth = 6;
	public int levelHeight = 8;
	public int roomWidth = 30;
	public int roomHeight = 15;
	public GameObject roomPrefab;
	public GameObject gameCamera;
	public PerlinNoise levelNoise;

	private int[,] levelData;
	private Vector2 levelSpawn;
	private Room[,] levelRooms;

	private void Awake()
	{
		LevelGenerator levelGenerator = new LevelGenerator(levelWidth, levelHeight, levelSeed);
		levelNoise = new PerlinNoise(levelSeed);
		levelSpawn = levelGenerator.GetLevelSpawn;
		levelData = levelGenerator.GetLevelData;
	}

	private void Start()
	{
		levelRooms = new Room[levelWidth, levelHeight];
		for (int x = 0; x < levelWidth; x++)
			for (int y = 0; y < levelHeight; y++)
				CreateRoom(x, y);
		LoadSpawn();
	}

	private void CreateRoom(int x, int y)
	{
		GameObject newRoomObject = Instantiate(roomPrefab, new Vector3(x * roomWidth, y * roomHeight, 5), Quaternion.Euler(Vector3.zero)) as GameObject;
		newRoomObject.name = string.Format("Room [{0}, {1}]", x, y);
		newRoomObject.transform.parent = this.transform;
		Room newRoom = newRoomObject.GetComponent<Room>();
		newRoom.level = this;
		newRoom.levelPos = new Vector2(x, y);
		levelRooms[x, y] = newRoom;
	}

	private void LoadSpawn()
	{
		Room spawnRoom = levelRooms[(int)levelSpawn.x, (int)levelSpawn.y];
		gameCamera.transform.position = new Vector3(spawnRoom.transform.position.x, spawnRoom.transform.position.y, -10);
		spawnRoom.LoadRoom();
		levelRooms[(int)levelSpawn.x - 1, (int)levelSpawn.y].LoadRoom();
		levelRooms[(int)levelSpawn.x + 1, (int)levelSpawn.y].LoadRoom();
	}

	private void DestroyRoom(int x, int y)
	{
		Room room = null;
		if ((room = levelRooms[x, y]) != null)
			Destroy(room.gameObject);
	}

	private void CleanRoom()
	{
		for (int x = 0; x < levelWidth; x++)
			for (int y = 0; y < levelHeight; y++)
				DestroyRoom(x, y);
	}
}
