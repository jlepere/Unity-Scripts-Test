using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Room : MonoBehaviour
{
	public LevelManager levelManager;
	public Vector2 roomPosition;

	private Tile[,] roomTiles;
	private MeshFilter meshFilter;
	private RoomGenerator roomGenerator;

	private void Awake()
	{
		meshFilter = gameObject.GetComponent<MeshFilter>();
	}

	private void Start()
	{
		roomGenerator = new RoomGenerator(levelManager);
		roomGenerator.GenerateWalls(this);
		roomGenerator.GenerateBorders(this);
		GenerateTiles(roomGenerator.GetRoomData);
		UpdateRoom();
	}

	public Tile GetTile(int x, int y)
	{
		return roomTiles[x, y];
	}

	private void GenerateTiles(int[,] roomData)
	{
		roomTiles = new Tile[levelManager.roomWidth, levelManager.roomHeight];
		for (int x = 0; x < levelManager.roomWidth; x++)
			for (int y = 0; y < levelManager.roomHeight; y++)
			{
				if (roomData[x, y] == 0)
					roomTiles[x, y] = new TileVoid();
				else
					roomTiles[x, y] = new Tile();
			}
	}

	private void UpdateRoom()
	{
		MeshData meshData = new MeshData();
		for (int x = 0; x < levelManager.roomWidth; x++)
			for (int y = 0; y < levelManager.roomHeight; y++)
				meshData = roomTiles[x, y].TileData(this, x, y, meshData);
		RenderMesh(meshData);
	}

	private void RenderMesh(MeshData meshData)
	{
		meshFilter.mesh.Clear();
		meshFilter.mesh.vertices = meshData.vertices.ToArray();
		meshFilter.mesh.triangles = meshData.triangles.ToArray();
		meshFilter.mesh.uv = meshData.uv.ToArray();
		meshFilter.mesh.RecalculateNormals();
	}
}
