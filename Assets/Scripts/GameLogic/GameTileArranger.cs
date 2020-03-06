using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.GameLogic{
    public abstract class GameTileArranger
    {
        ///<summary>
        /// Unfinished code
        ///</summary>
        protected GameTile _okeyTile;

        public GameTileArranger(GameTile p_okeyTile)
        {
            _okeyTile = p_okeyTile;
        }

        ///<summary>
        /// This is the function that findsevery possible grouping that is possible with given GameTileGroup
        /// GameTileSmartArranger, GameTileSameNumberArrnger and GameTileSameColorArranger implement this in their own way/
        ///</summary>
        protected abstract GameTileGroup[] FindAll(GameTileGroup p_copiedGroup);

        ///<summary>
        /// Unfinished code
        ///</summary>
        protected virtual void OkeyTreatment(GameTileGroup p_copiedGroup){

        }

        ///<summary>
        /// No matter the algorithm, the GameTileGroup is treated with same sequence.
        /// Some of the methods inside are implemented seperately.
        ///</summary>
        public GameTileGroup[] Arrange(GameTileGroup p_originalGroup){
            List<GameTileGroup> result = new List<GameTileGroup>();

            GameTileGroup copiedGroup = new GameTileGroup(p_originalGroup);

            if(copiedGroup.ContainsOkey(_okeyTile)){
                OkeyTreatment(copiedGroup);
            }

            GameTileGroup[] everyPossibility = FindAll(copiedGroup);
            GameTileArrangement[] feasbileArrangements = FindFeasibles(everyPossibility);

            // Couldn't find the bug, brute fix incoming
            FindMissings(p_originalGroup, feasbileArrangements);

            int bestArrangementScore = int.MaxValue;
            int bestArrangementIndex = 0;
            for(int i = 0; i < feasbileArrangements.Length; i++){
                if(feasbileArrangements[i].Score < bestArrangementScore){
                    bestArrangementScore = feasbileArrangements[i].Score;
                    bestArrangementIndex = i;
                }
            }

            List<GameTileGroup> arrangementResult = new List<GameTileGroup>();
            arrangementResult.AddRange(feasbileArrangements[bestArrangementIndex].ArrangedGroups);
            arrangementResult.Add(feasbileArrangements[bestArrangementIndex].RemainderGroup);

            result.AddRange(everyPossibility);

            return arrangementResult.ToArray();
        }

        ///<summary>
        /// Input parameter contains every possible GameTileGroup, with duplicates.
        /// First GameTileGroups are sorted by Score, then o(n^2) to find every arrengement with no collisions.
        /// This function worked great for SameNumber/SameColor sort, not so great for SmartSort.
        ///</summary>
        protected virtual GameTileArrangement[] FindFeasibles(GameTileGroup[] p_possibilities)
        {
            p_possibilities = SortByScore(p_possibilities);

            List<GameTileArrangement> feasbileArrangements = new List<GameTileArrangement>();
            GameTileArrangement currentArrangement = null;

            for (int i = 0; i < p_possibilities.Length; i++)
            {
                currentArrangement = new GameTileArrangement();
                currentArrangement.AddGroup(p_possibilities[i]);
                for (int j = 0; j < p_possibilities.Length; j++)
                {
                    if (!currentArrangement.HasDuplicateTileGroup(p_possibilities[j]))
                    {
                        currentArrangement.AddGroup(p_possibilities[j]);
                    }
                }
                feasbileArrangements.Add(currentArrangement);
            }

            return feasbileArrangements.ToArray();
        }

        ///<summary>
        /// Somewhere between FindAll nd FindFeasibles, some GameTile's are lost depending on GameTileGroup that ha our 14 original tiles.
        /// This only happens for SmartSort, this function adds the lost Tiles which will be added as remainder to the arrengement.
        ///</summary>
        private void FindMissings(GameTileGroup p_everyTile, GameTileArrangement[] p_arrangements){
            for(int i = 0; i < p_arrangements.Length; i++){
                for(int j = 0; j < p_everyTile.GameTileCount; j++){
                    if(!p_arrangements[i].HasDuplicateTile(p_everyTile[j])){
                        p_arrangements[i].AddTile(p_everyTile[j]);
                    }
                }
            }
        }

        ///<summary>
        /// This shouldn't be here but yeah, sorts the array by score.
        ///</summary>
        protected GameTileGroup[] SortByScore(GameTileGroup[] p_possibilities){
            GameTileGroup swapTemp = null;
            int highestScore = -1;
            int highestIndex = -1;
            for(int i = 0; i < p_possibilities.Length; i++){
                highestIndex = i;
                highestScore = -1;
                for(int j = i; j < p_possibilities.Length; j++){
                    if(p_possibilities[j].GameTileCount > highestScore){
                        highestScore = p_possibilities[j].GameTileCount;
                        highestIndex = j;
                    }
                }

                swapTemp = p_possibilities[i];
                p_possibilities[i] = p_possibilities[highestIndex];
                p_possibilities[highestIndex] = swapTemp;
            }

            return p_possibilities;
        }
    }
}