using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Room : MonoBehaviour
{
	public Level level;
	public Vector2 levelPos;

	private Tile[,] roomTiles;
	private MeshFilter meshFilter;

	private void Awake()
	{
		meshFilter = gameObject.GetComponent<MeshFilter>();
	}

	private void Start()
	{
		LoadRoom();
	}

	public void LoadRoom()
	{
		roomTiles = new Tile[level.roomWidth, level.roomHeight];
		for (int x = 0; x < level.roomWidth; x++)
		{
			int columnHeight = level.levelNoise.GetNoise(((int)levelPos.x * level.roomWidth * ((int)levelPos.y + 1)) + x, 10);
			int columnTop = 1 + level.levelNoise.GetNoise(((int)levelPos.x * level.roomWidth * ((int)levelPos.y + 1)) + x, 7);
			for (int y = 0; y < level.roomHeight; y++)
			{
				if (y == columnHeight || y == level.roomHeight - columnTop)
					roomTiles[x, y] = new TileGrass();
				else if (y <= columnHeight || y >= level.roomHeight - columnTop)
					roomTiles[x, y] = new TileGround();
				else
					roomTiles[x, y] = new TileVoid();
			}
		}
		UpdateRoom();
	}

	public Tile GetTile(int x, int y)
	{
		return roomTiles[x, y];
	}

	private void UpdateRoom()
	{
		MeshData meshData = new MeshData();
		for (int x = 0; x < level.roomWidth; x++)
			for (int y = 0; y < level.roomHeight; y++)
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
