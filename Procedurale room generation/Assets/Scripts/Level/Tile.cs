using UnityEngine;

public class Tile
{
	public Vector2 tileTexture;
	private const float textureSize = 0.5f;

	public Tile()
	{
		tileTexture = new Vector2(1, 0);
	}

	public virtual bool IsSolid()
	{
		return true;
	}

	public virtual Vector2[] TileUVs()
	{
		Vector2[] tileUVs = new Vector2[4];
		tileUVs[0] = new Vector2(textureSize * tileTexture.x, textureSize * tileTexture.y);
		tileUVs[1] = new Vector2(textureSize * tileTexture.x, textureSize * tileTexture.y + textureSize);
		tileUVs[2] = new Vector2(textureSize * tileTexture.x + textureSize, textureSize * tileTexture.y + textureSize);
		tileUVs[3] = new Vector2(textureSize * tileTexture.x + textureSize, textureSize * tileTexture.y);
		return tileUVs;
	}

	public virtual MeshData TileData(Room room, int x, int y, MeshData meshData)
	{
		if (!room.GetTile(x, y).IsSolid())
			return meshData;
		meshData.vertices.Add(new Vector3(x - (room.level.roomWidth / 2), y - 0.5f - (room.level.roomHeight / 2), 0.5f));
		meshData.vertices.Add(new Vector3(x - (room.level.roomWidth / 2), y + 0.5f - (room.level.roomHeight / 2), 0.5f));
		meshData.vertices.Add(new Vector3(x + 1 - (room.level.roomWidth / 2), y + 0.5f - (room.level.roomHeight / 2), 0.5f));
		meshData.vertices.Add(new Vector3(x + 1 - (room.level.roomWidth / 2), y - 0.5f - (room.level.roomHeight / 2), 0.5f));
		meshData.AddQuadTriangles();
		meshData.uv.AddRange(TileUVs());
		return meshData;
	}
}
