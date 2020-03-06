using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.GameLogic{
    public class GameTileHand
    {
        public GameTileGroup EveryTile;
        public List<GameTileGroup> ArrangedTiles;
        public GameTileGroup RemainderTiles;

        public GameTileHand()
        {
            ArrangedTiles = new List<GameTileGroup>();
        }

        public GameTileHand(GameTileGroup p_gameTileGroup) : this()
        {
            EveryTile = p_gameTileGroup;
        }
    }
}