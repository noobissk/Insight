using System;
using UnityEngine;


namespace MiguelTools
{
    public enum TileType
    {
        Empty,
        Wall,
        Goal,
        Player,
        Enemy,
    }
    public class Tile
    {
        public TileType type;
        public override string ToString()
        {
            return type.ToString();
        }

        public Tile() { }
        public Tile(TileType i_type)
        {
            type = i_type;
        }
    }

}