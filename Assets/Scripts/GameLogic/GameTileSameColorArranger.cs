using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.GameLogic{
    public class GameTileSameColorArranger : GameTileArranger
    {
        public GameTileSameColorArranger(GameTile p_okeyTile) : base(p_okeyTile) { }

        ///<summary>
        /// Grouping method that works on o(n^2) since GameTileGroup is iterated for every GameTile inside the group
        ///</summary>
        protected GameTileGroup GroupMethod(GameTile p_selectedTile, GameTileGroup p_gameTileGroup)
        {
            GameTileGroup result = new GameTileGroup();
            result.AddGameTile(p_selectedTile);

            for (int i = 0; i < p_gameTileGroup.GameTileCount; i++)
            {
                if (p_gameTileGroup[i].CompareColor(p_selectedTile) && p_gameTileGroup[i].IsNextOf(p_selectedTile))
                {
                    p_selectedTile = p_gameTileGroup[i];
                    result.AddGameTile(p_selectedTile);
                    i = -1;
                }
            }

            return result;
        }

        ///<summary>
        /// Works fine for itself
        ///</summary>
        protected override GameTileGroup[] FindAll(GameTileGroup p_copiedGroup)
        {
            List<GameTileGroup> result = new List<GameTileGroup>();

            //p_copiedGroup.SortByNumber();

            for (int i = 0; i < p_copiedGroup.GameTileCount; i++)
            {
                result.Add(GroupMethod(p_copiedGroup[i], p_copiedGroup));
            }

            return result.ToArray();
        }
    }
}