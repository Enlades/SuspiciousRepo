using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.GameLogic{

    ///<summary>
    /// Here comes the Frankenstein's monster
    ///</summary>
    public class GameTileSmartArranger : GameTileArranger
    {
        public GameTileSmartArranger(GameTile p_okeyTile) : base(p_okeyTile) { }

        ///<summary>
        /// Unfinished code, i was thinking of removing OkeyTile first,
        /// find every possibilty, add OkeyTile to a single possiblity and try to generate ne Groups later to be generated as arrrangements
        ///</summary>
        protected override void OkeyTreatment(GameTileGroup p_copiedGroup){
            p_copiedGroup.RemoveTile(_okeyTile);
        }

        ///<summary>
        /// Yep, so smart it contains duplicate code
        ///</summary>
        protected GameTileGroup SameNumberGroupMethod(GameTile p_selectedTile, GameTileGroup p_gameTileGroup)
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

        ///<summary>
        /// Duplicate code
        ///</summary>
        protected GameTileGroup SameColorGroupMethod(GameTile p_selectedTile, GameTileGroup p_gameTileGroup)
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
        /// This is NOT working fine
        ///</summary>
        protected override GameTileGroup[] FindAll(GameTileGroup p_copiedGroup)
        {
            List<GameTileGroup> result = new List<GameTileGroup>();

            for (int i = 0; i < p_copiedGroup.GameTileCount; i++)
            {
                result.Add(SameNumberGroupMethod(p_copiedGroup[i], p_copiedGroup));
            }

            p_copiedGroup.SortByNumber();

            for (int i = 0; i < p_copiedGroup.GameTileCount; i++)
            {
                result.Add(SameColorGroupMethod(p_copiedGroup[i], p_copiedGroup));
            }

            return result.ToArray();
        }
    }

}