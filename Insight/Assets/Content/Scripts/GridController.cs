using UnityEngine;
using MiguelTools;
using MyBox;
using System.IO;
using System;


public class GridController : MonoBehaviour
{
    public string mapFileName = "tilemap.json";
    public LayerMask wallMask;
    public Vector2Int gridSize = new Vector2Int(30, 26);
    public float gridScale;
    public Grid_Base<Tile> grid;


    int[,] _map;

    void Start()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, mapFileName)))
        {
            grid = new Grid_Base<Tile>(transform, Vector3.zero, gridSize, gridScale, true);
            return;
            // SaveTilemap();
        }

        _map = LoadMap("Level_1");
        grid = new Grid_Base<Tile>(transform, Vector3.zero, gridSize, gridScale, true, CreateTile);
    }

    private Tile CreateTile(int x, int y)
    {
        return new Tile((TileType)_map[x, y]);
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
