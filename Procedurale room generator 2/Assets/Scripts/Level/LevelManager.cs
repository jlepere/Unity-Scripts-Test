using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	private const string roomNameFormat = "Room [{0}, {1}]";

	public int levelSeed = 0;
	public int levelWidth = 6;
	public int levelHeight = 8;
	public int roomWidth = 30;
	public int roomHeight = 15;
	public GameObject roomPrefab;
	public PerlinNoise perlinNoise;
	public Text textLevelData;
	public Text textLevelSeed;
	public Text textLevelSpawn;

	private Room[,] levelRooms;
	private LevelGenerator levelGenerator;

	private void Awake()
	{
		levelRooms = new Room[levelWidth, levelHeight];
		levelGenerator = new LevelGenerator(levelWidth, levelHeight, levelSeed);
		perlinNoise = new PerlinNoise(levelGenerator.GetLevelSeed);
	}

	private void Start()
	{
		GenerateLevel();
		GenerateDebugText();
	}

	private void GenerateLevel()
	{
		levelGenerator.GenerateLevel();
		/*for (int i = x - 1; i <= x + 1; i++)
			CreateRoom(i, y);*/
		for (int y1 = 0; y1 < levelHeight; y1++)
		for (int x1 = 0; x1 < levelWidth; x1++)
			CreateRoom(x1, y1);
	}

	private void GenerateDebugText()
	{
		textLevelSeed.text = string.Format("Seed : {0}", levelGenerator.GetLevelSeed);
		textLevelSpawn.text = string.Format("Spawn : {0} {1}", levelGenerator.GetLevelSpawn.x, levelGenerator.GetLevelSpawn.y);
		for (int y = levelHeight - 1; y >= 0; y--)
		{
			for (int x = 0; x < levelWidth; x++)
				textLevelData.text += string.Format(" {0}", levelGenerator.GetLevelData[x, y]);
			textLevelData.text += "\n";
		}
	}

	private void CreateRoom(int x, int y)
	{
		GameObject roomObject = Instantiate(roomPrefab, new Vector3(x * roomWidth, y * roomHeight, 5), Quaternion.Euler(Vector3.zero));
		roomObject.name = string.Format(roomNameFormat, x, y);
		roomObject.transform.parent = this.transform;
		levelRooms[x, y] = roomObject.GetComponent<Room>();
		levelRooms[x, y].levelManager = this;
		levelRooms[x, y].roomPosition = new Vector2(x, y);
	}

	public void DestroyRoom(int x, int y)
	{
		if (levelRooms[x, y] != null)
			Destroy(levelRooms[x, y].gameObject);
	}

	public void CleanRoom()
	{
		for (int x = 0; x < levelWidth; x++)
			for (int y = 0; y < levelHeight; y++)
				DestroyRoom(x, y);
	}
}
