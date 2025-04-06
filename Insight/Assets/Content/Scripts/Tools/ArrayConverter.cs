using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;


namespace MiguelTools
{

    [System.Serializable]
    public class MapData
    {
        public int width;
        public int height;
        public int[] tiles; // Flattened int[,]
    }

    public static class ArrayConverter
    {
        // private static Tilemap _tilemap;
        public static TileBase[] tilePalette;

        // public static Vector2Int size; // how big the area is
        // public static Vector3Int origin;

        public static int[] Flatten(int[,] map)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            int[] flat = new int[width * height];

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                flat[y * width + x] = map[x, y];

            return flat;
        }
        public static int[,] Unflatten(int[] flat, int width, int height)
        {
            int[,] map = new int[width, height];
            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                map[x, y] = flat[y * width + x];
            return map;
        }
    }
}
