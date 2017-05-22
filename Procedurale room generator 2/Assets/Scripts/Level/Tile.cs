using UnityEngine;

public class Tile
{
	private const float textureSize = 0.5f;
	public Vector2 texturePos;

	public virtual bool IsSolid
	{
		get { return true; }
	}

	public virtual Vector2[] TileUVs()
	{
		Vector2[] tileUVs = new Vector2[4];
		tileUVs[0] = new Vector2(textureSize * texturePos.x, textureSize * texturePos.y);
		tileUVs[1] = new Vector2(textureSize * texturePos.x, textureSize * texturePos.y + textureSize);
		tileUVs[2] = new Vector2(textureSize * texturePos.x + textureSize, textureSize * texturePos.y + textureSize);
		tileUVs[3] = new Vector2(textureSize * texturePos.x + textureSize, textureSize * texturePos.y);
		return tileUVs;
	}

	public virtual MeshData TileData(Room room, int x, int y, MeshData meshData)
	{
		if (!room.GetTile(x, y).IsSolid)
			return meshData;
		meshData.vertices.Add(new Vector3(x - (room.levelManager.roomWidth / 2), y - 0.5f - (room.levelManager.roomHeight / 2), 0.5f));
		meshData.vertices.Add(new Vector3(x - (room.levelManager.roomWidth / 2), y + 0.5f - (room.levelManager.roomHeight / 2), 0.5f));
		meshData.vertices.Add(new Vector3(x + 1 - (room.levelManager.roomWidth / 2), y + 0.5f - (room.levelManager.roomHeight / 2), 0.5f));
		meshData.vertices.Add(new Vector3(x + 1 - (room.levelManager.roomWidth / 2), y - 0.5f - (room.levelManager.roomHeight / 2), 0.5f));
		meshData.AddQuadTriangles();
		meshData.uv.AddRange(TileUVs());
		return meshData;
	}
}
