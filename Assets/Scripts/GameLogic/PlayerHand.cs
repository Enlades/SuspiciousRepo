using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.GameLogic{

    public class PlayerHand{

        public GameTileGroup EveryTile;
        public List<GameTileGroup> OrderedTiles;
        public GameTileGroup RemainderTiles;

        public PlayerHand()
        {
            OrderedTiles = new List<GameTileGroup>();
        }

        public PlayerHand(GameTileGroup p_gameTileGroup) : this()
        {
            EveryTile = p_gameTileGroup;
        }

        public void AddTile(GameTile p_gameTile)
        {
            EveryTile.AddGameTile(p_gameTile);
        }
    }
}
