using UnityEngine;

public class TileGrass : Tile
{
	public TileGrass()
	{
		tileTexture = new Vector2(1, 1);
	}

	public override bool IsSolid()
	{
		return true;
	}
}
