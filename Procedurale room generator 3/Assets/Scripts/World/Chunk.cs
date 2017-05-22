using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class Chunk : MonoBehaviour
{
    public const string chunkNameFormat = "Chunk [{0}, {1}]";
    public const int chunkSize = 16;
    private Tile[,] chunkTiles;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    private void Awake()
    {
        meshFilter = gameObject.GetComponent<MeshFilter>();
        meshCollider = gameObject.GetComponent<MeshCollider>();
    }

    public Tile GetTile(int x, int y)
    {
        return chunkTiles[x, y];
    }

    public void SetChunkData(int xChunk, int yChunk, int[,] levelData)
    {
        chunkTiles = new Tile[chunkSize, chunkSize];
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                if (levelData[xChunk * chunkSize + x, yChunk * chunkSize + chunkSize - y - 1] == 1)
                    chunkTiles[x, y] = new Tile();
                else
                    chunkTiles[x, y] = new TileVoid();
            }
        }
        UpdateChunk();
    }

    private void UpdateChunk()
    {
        MeshData meshData = new MeshData();
        for (int x = 0; x < chunkSize; x++)
            for (int y = 0; y < chunkSize; y++)
                meshData = chunkTiles[x, y].TileData(this, x, y, meshData);
        RenderMesh(meshData);
    }

    private void RenderMesh(MeshData meshData)
    {
        meshFilter.mesh.Clear();
        meshFilter.mesh.vertices = meshData.vertices.ToArray();
        meshFilter.mesh.triangles = meshData.triangles.ToArray();
        meshFilter.mesh.uv = meshData.uv.ToArray();
        meshFilter.mesh.RecalculateNormals();

        Mesh mesh = new Mesh();
        meshCollider.sharedMesh = null;
        mesh.vertices = meshData.colVertices.ToArray();
        mesh.triangles = meshData.colTriangles.ToArray();
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
}
