using System;
using UnityEngine;


namespace MiguelGrid
{
    public enum TileType
    {
        Empty,
        Wall,
        Player,
        Enemy,
        Goal,
    }
    public class Tile
    {
        public TileType type;
        public override string ToString()
        {
            return type.ToString();
        }
    }

}