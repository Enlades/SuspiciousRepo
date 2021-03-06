﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.GameLogic{

    public class GameTileGroup
    {
        ///<summary>
        /// Oh i love using this. Shortcut for accessing GameTiles
        ///</summary>
        public GameTile this[int index]{
            get{
                return GameTiles[index];
            }
        }

        ///<summary>
        /// Shortcut
        ///</summary>
        public int GameTileCount{
            get{
                return GameTiles.Count;
            }
        }

        ///<summary>
        /// The GameTiles
        ///</summary>
        public List<GameTile> GameTiles {get; private set;}

        ///<summary>
        /// Here are those static people, used for Comparisons
        ///</summary>
        private static GameTileNumberComparer _numberComparer = new GameTileNumberComparer();
        private static GameTileColorComparer _colorComparer = new GameTileColorComparer();

        public GameTileGroup(){
            GameTiles = new List<GameTile>();
        }

        public GameTileGroup(GameTile[] p_GameTileIndexes) : this(){
            GameTiles.AddRange(p_GameTileIndexes);
        }

        public GameTileGroup(GameTileGroup p_gameTileGroup) : this(p_gameTileGroup.GameTiles.ToArray()){}


        ///<summary>
        /// The warning inside is for Debugging purposes,
        /// that should never be called, if so the Sorting algorithm messed up
        ///</summary>
        public void AddGameTile(GameTile p_gameTile)
        {
            if (HasDuplicate(p_gameTile))
            {
                Debug.LogWarning("Adding same tile");
            }

            GameTiles.Add(p_gameTile);
        }

        ///<summary>
        /// The warning inside is for Debugging purposes,
        /// that should never be called, if so the Sorting algorithm messed up
        ///</summary>
        public void AddGroup(GameTileGroup p_gameTileGroup)
        {
            if (HasDuplicateGroup(p_gameTileGroup))
            {
                Debug.LogWarning("Adding group with same tile");
            }

            GameTiles.AddRange(p_gameTileGroup.GameTiles);
        }

        public void SortByNumber(){
            GameTiles.Sort(_numberComparer);
        }

        public void SortByColor(){
            GameTiles.Sort(_colorComparer);
        }

        public bool RemoveTile(GameTile p_gameTile)
        {
            for(int i = 0; i < GameTileCount; i++){
                if(GameTiles[i].TileColor == p_gameTile.TileColor && GameTiles[i].TileNumber == p_gameTile.TileNumber){
                    GameTiles.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool RemoveGroup(GameTileGroup p_gameTileGroup){
            bool result = true;
            for(int i = 0; i < p_gameTileGroup.GameTileCount; i++){
                result = RemoveTile(p_gameTileGroup[i]);

                if(!result){
                    Debug.LogWarning("Trying to remove a wrong tile");
                    return result;
                }
            }

            return result;
        }

        ///<summary>
        /// Unfinished code
        ///</summary>
        public bool ContainsOkey(GameTile p_okeyTile){            
            for(int i = 0; i < GameTileCount; i++){
                if(GameTiles[i].CompareTile(p_okeyTile)){
                    return true;
                }
            }

            return false;
        }

        ///<summary>
        /// Used during sorting algorithms, for collision control
        ///</summary>
        public bool HasDuplicate(GameTile p_gameTile)
        {
            for (int i = 0; i < GameTileCount; i++)
            {
                if (GameTiles[i].IsDuplicateOf(p_gameTile))
                {
                    return true;
                }
            }

            return false;
        }

        ///<summary>
        /// Used during sorting algorithms, for collision control
        ///</summary>
        public bool HasDuplicateGroup(GameTileGroup p_gameTileGroup)
        {
            for (int i = 0; i < p_gameTileGroup.GameTileCount; i++)
            {
                if (HasDuplicate(p_gameTileGroup[i]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}