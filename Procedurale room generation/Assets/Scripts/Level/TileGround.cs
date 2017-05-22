using UnityEngine;

public class TileGround : Tile
{
	public TileGround()
	{
		tileTexture = new Vector2(0, 0);
	}

	public override bool IsSolid()
	{
		return true;
	}
}
