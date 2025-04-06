using UnityEngine;
using MiguelTools;
using MyBox;
using System.IO;
using System;


public class GridController : MonoBehaviour
{
    public static GridController Singleton;
    public string mapFileName = "tilemap.json";
    public LayerMask wallMask;
    public Vector2Int gridSize = new Vector2Int(30, 26);
    public float gridScale;
    public Grid_Base<Tile> grid;
    public Vector2Int playerPosition;
    public Vector2Int goalPosition;


    int[,] _map;

    void Awake()
    {
        if (Singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        Singleton = this;

        if (!File.Exists(Path.Combine(Application.persistentDataPath, mapFileName)))
        {
            // grid = new Grid_Base<Tile>(transform, Vector3.zero, gridSize, gridScale, false);
            SaveTilemap();
        }

        _map = LoadMap(mapFileName);
        grid = new Grid_Base<Tile>(transform, Vector3.zero, gridSize, gridScale, true, CreateTile);
    }

    private Tile CreateTile(int x, int y)
    {
        TileType t = (TileType)_map[x, y];
        switch (t)
        {
            case TileType.Goal:
                goalPosition = new Vector2Int(x, y);
                break;
            case TileType.Player:
                playerPosition = new Vector2Int(x, y);
                break;
            default:
                break;
        }
        

        return new Tile(t);
    }

    void OnDrawGizmosSelected()
    {
        if (grid == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(
            gridSize.x * gridScale, 
            gridSize.y * gridScale,
            0.1f
        ));
    }

    [ButtonMethod]
    public void SaveTilemap()
    {
        int[,] map = new int[gridSize.x, gridSize.y];
        RaycastHit2D hit;
        for (int y = 0; y < gridSize.y; y++)
        for (int x = 0; x < gridSize.x; x++)
        {
            hit = Physics2D.Raycast(grid.GetWorldPosition(x, y), Vector2.up, 0.1f, wallMask);

            if (hit.collider == null)
                continue;
            map[x, y] = LayerMask.LayerToName(hit.collider.gameObject.layer) switch
            {
                "Wall"   => 1,
                "Goal"   => 2,
                "Player" => 3,
                "Enemy"  => 4,
                _        => 0,
            };
        }

        MapData mapData = new MapData();
        mapData.width = map.GetLength(0);
        mapData.height = map.GetLength(1);
        mapData.tiles = ArrayConverter.Flatten(map);

        string json = JsonUtility.ToJson(mapData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, mapFileName), json);
    }

    public int[,] LoadMap(string i_mapName)
    {
        MapData data = JsonUtility.FromJson<MapData>(File.ReadAllText(Path.Combine(Application.persistentDataPath, i_mapName)));
        return ArrayConverter.Unflatten(data.tiles, data.width, data.height);
    }
}
