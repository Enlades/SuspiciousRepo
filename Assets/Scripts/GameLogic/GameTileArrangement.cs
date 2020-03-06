using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.GameLogic{
    public class GameTileArrangement
    {
        ///<summary>
        /// An arrangement contains a possbile case for a giving GameTileGroup,
        // Score is calculated by the number of tiles that are not in a Group of 3 or more.
        // So less score is better
        ///</summary>
        public int Score{
            get{
                return RemainderGroup.GameTileCount;
            }
        }

        ///<summary>
        /// Every GameTileGroup that contains 3 or more Tiles
        ///</summary>
        public List<GameTileGroup> ArrangedGroups{get; private set;}
        ///<summary>
        /// Every tile that is left out of 14
        ///</summary>
        public GameTileGroup RemainderGroup {get; private set;}

        public GameTileArrangement(){
            ArrangedGroups = new List<GameTileGroup>();
            RemainderGroup = new GameTileGroup();
        }

        ///<summary>
        /// Arrangement algorithm sorts it's Groups and adds them one by one to the Arrangement.
        /// In here they either go into ArrangedGroups or RemainderGroup
        ///</summary>
        public void AddGroup(GameTileGroup p_gameTileGroup)
        {
            if (p_gameTileGroup.GameTileCount < 3)
            {
                RemainderGroup.AddGroup(p_gameTileGroup);
                return;
            }
            ArrangedGroups.Add(p_gameTileGroup);
        }

        ///<summary>
        /// For some reason sorting algorithm leaves some ungroups single tiles,
        /// that's a bug, this is a brute fix
        ///</summary>
        public void AddTile(GameTile p_gameTile)
        {
            RemainderGroup.AddGameTile(p_gameTile);
        }

        ///<summary>
        /// This is used during collision search between arrangements,
        /// since every arragnment is created with duplicate tiles.
        ///</summary>
        public bool HasDuplicateTile(GameTile p_gameTile)
        {
            for (int i = 0; i < ArrangedGroups.Count; i++)
            {
                if (ArrangedGroups[i].HasDuplicate(p_gameTile))
                {
                    return true;
                }
            }

            if (RemainderGroup != null && RemainderGroup.HasDuplicate(p_gameTile))
            {
                return true;
            }

            return false;
        }

        ///<summary>
        /// Modular code
        ///</summary>
        public bool HasDuplicateTileGroup(GameTileGroup p_gameTileGroup)
        {
            for (int i = 0; i < ArrangedGroups.Count; i++)
            {
                if (ArrangedGroups[i].HasDuplicateGroup(p_gameTileGroup))
                {
                    return true;
                }
            }

            if (RemainderGroup != null && RemainderGroup.HasDuplicateGroup(p_gameTileGroup))
            {
                return true;
            }

            return false;
        }
    }
}