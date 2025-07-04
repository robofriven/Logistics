using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameWorld gameWorld;
    public WorldVisualizer worldVisualizer;

    private int tick = 0;
    private int tickRate = 1; // How often to update the game world in ticks/second
    private float tickTimer = 0f;

    private bool isRunning = false;

    void Start()
    {
        if (gameWorld == null || worldVisualizer == null)
        {
            Debug.LogError("Something not assigned in Editor");
            return;
        }

        gameWorld.InitializeWorld();
        Debug.Log("Game World Initialized with dimensions: " + gameWorld.width + " " + gameWorld.height);
        worldVisualizer.Initialize(gameWorld);
        Debug.Log("World Visualizer Initialized");

        Debug.Log("Making some other tiles for testing");
        gameWorld.GetTile(new Vector2Int(0, 0)).SetType(TileType.Right);
        gameWorld.GetTile(new Vector2Int(1, 0)).SetType(TileType.Left);
        gameWorld.GetTile(new Vector2Int(2, 0)).SetType(TileType.Up);
        gameWorld.GetTile(new Vector2Int(3, 0)).SetType(TileType.Down);

        worldVisualizer.UpdateThings();
        Debug.Log("World Visualizer Updated");

    }

    void Update()
    {
        if (isRunning)
        {
            tickTimer += Time.deltaTime;
            float tickInterval = 1f / tickRate;

            while (tickTimer >= tickInterval)
            {
                tick++;
                Tick();
                tickTimer -= tickInterval;
            }
        }
    }

    private void Tick()
    {
        // this is where the tick happens

        // Cycle through all tiles
        // If thing is on a right left up down tile, then move thing that direction and update the tiles (previous and current)
        // This will be slow as hell, but it should work for now. Yay prototyping!
        CheckTiles();
    }

    private void CheckTiles()
    {
        for (int x = 0; x < gameWorld.width; x++)
        {
            for (int y = 0; y < gameWorld.height; y++)
            {
                Tile tile = gameWorld.GetTile(new Vector2Int(x, y));
                if (tile != null)
                {
                    // Check if the tile has a thing and process it
                    // This is where you would handle the logic for things on the tile
                    // For example, if a thing is on this tile, move it or perform some action
                }
            }
        }
    }
}
