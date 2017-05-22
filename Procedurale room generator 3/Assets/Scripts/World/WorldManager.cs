using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private int levelWidth = 6;

    [SerializeField]
    private int levelHeight = 8;

    [SerializeField]
    private int levelGroundOffset = 12;

    [SerializeField]
    private IntRange numRoomsRange = new IntRange(8, 12);

    [SerializeField]
    private IntRange roomWidthRange = new IntRange(16, 20);

    [SerializeField]
    private IntRange roomHeightRange = new IntRange(8, 12);

    [SerializeField]
    private GameObject chunkPrefab;

    private LevelGenerator levelGenerator;
    private Chunk[,] levelChunks;

    private void Awake()
    {
        levelGenerator = new LevelGenerator(levelWidth * Chunk.chunkSize, levelHeight * Chunk.chunkSize);
    }

    private void Start()
    {
        GenerateWorld();
    }

    private void OnDestroy()
    {
        CleanChunks();
    }

    public void GenerateWorld()
    {
        CleanChunks();
        GenerateLevel();
        GenerateChunks();
    }

    private void GenerateLevel()
    {
        levelGenerator.GenerateWorldSeed();
        levelGenerator.GenerateLevelData(levelGroundOffset);
        levelGenerator.GenerateLevelRooms(levelGroundOffset, numRoomsRange.Random, roomWidthRange, roomHeightRange);
    }

    private void GenerateChunks()
    {
        levelChunks = new Chunk[levelWidth, levelHeight];
        for (int x = 0; x < levelWidth; x++)
            for (int y = 0; y < levelHeight; y++)
                CreateChunk(x, y);
    }

    private void CreateChunk(int x, int y)
    {
        GameObject chunkObject = Instantiate(chunkPrefab, new Vector3(x * Chunk.chunkSize, -y * Chunk.chunkSize, 5), Quaternion.Euler(Vector3.zero));
        chunkObject.name = string.Format(Chunk.chunkNameFormat, x, y);
        chunkObject.transform.parent = this.transform;
        levelChunks[x, y] = chunkObject.GetComponent<Chunk>();
        levelChunks[x, y].SetChunkData(x, y, levelGenerator.GetLevelData);
    }

    private void DestroyChunk(int x, int y)
    {
        if (levelChunks != null && levelChunks[x, y] != null)
            Destroy(levelChunks[x, y].gameObject);
    }

    public void CleanChunks()
    {
        if (levelChunks == null)
            return;
        for (int x = 0; x < levelWidth; x++)
            for (int y = 0; y < levelHeight; y++)
                DestroyChunk(x, y);
    }
}
