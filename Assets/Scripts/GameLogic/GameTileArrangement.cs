using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.GameLogic{
    public class GameTileArrangement
    {
        public int Score{
            get{
                return RemainderGroup.GameTileCount;
            }
        }

        public List<GameTileGroup> ArrangedGroups{get; private set;}
        public GameTileGroup RemainderGroup {get; private set;}

        public GameTileArrangement(){
            ArrangedGroups = new List<GameTileGroup>();
            RemainderGroup = new GameTileGroup();
        }

        public void AddGroup(GameTileGroup p_gameTileGroup)
        {
            if (p_gameTileGroup.GameTileCount < 3)
            {
                RemainderGroup.AddGroup(p_gameTileGroup);
                return;
            }
            ArrangedGroups.Add(p_gameTileGroup);
        }

        public void AddTile(GameTile p_gameTile)
        {
            RemainderGroup.AddGameTile(p_gameTile);
        }

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