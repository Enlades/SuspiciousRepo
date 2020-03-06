using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.GameLogic{
    public class GameTileSameNumberArranger : GameTileArranger
    {
        public GameTileSameNumberArranger(GameTile p_okeyTile) : base(p_okeyTile) { }

        ///<summary>
        /// Same as GameTileSameColorArranger, just different comparisons
        /// The problem here is that, if there is a case for 7-7-7-7, it's included
        /// but 7-7-7 7 is not, it cannot find subgroups, so FindAll does not find every possibilty under this GroupMethod
        ///</summary>
        protected GameTileGroup GroupMethod(GameTile p_selectedTile, GameTileGroup p_gameTileGroup)
        {
            GameTileGroup result = new GameTileGroup();
            result.AddGameTile(p_selectedTile);

            for (int i = p_gameTileGroup.GameTiles.IndexOf(p_selectedTile); i < p_gameTileGroup.GameTileCount; i++)
            {
                if (p_gameTileGroup[i].CompareNumber(p_selectedTile))
                {
                    bool sameColorExists = false;
                    for (int j = 0; j < result.GameTileCount; j++)
                    {
                        if (result[j].CompareColor(p_gameTileGroup[i]))
                        {
                            sameColorExists = true;
                        }
                    }

                    if (!sameColorExists)
                    {
                        p_selectedTile = p_gameTileGroup[i];
                        result.AddGameTile(p_selectedTile);
                    }
                }
            }

            return result;
        }

        protected override GameTileGroup[] FindAll(GameTileGroup p_copiedGroup)
        {
            List<GameTileGroup> result = new List<GameTileGroup>();

            p_copiedGroup.SortByNumber();

            for (int i = 0; i < p_copiedGroup.GameTileCount; i++)
            {
                result.Add(GroupMethod(p_copiedGroup[i], p_copiedGroup));
            }

            return result.ToArray();
        }
    }
}