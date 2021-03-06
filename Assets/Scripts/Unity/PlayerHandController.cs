﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZyngaDemo.GameLogic;

namespace ZyngaDemo.Unity{
    public class PlayerHandController : MonoBehaviour
    {
        public BoardView PlayerBoard;

        // Every GameTileView also has reference to their GameTile
        public GameTileGroup PlayerHand{get; private set;}
        public List<GameTileView> PlayerHandViews{get; private set;}

        private void Awake(){
            PlayerHand = new GameTileGroup();
            PlayerHandViews = new List<GameTileView>();
        }

        public void AddTile(GameTileView p_gameTileView){
            PlayerHandViews.Add(p_gameTileView);
            PlayerHand.AddGameTile(p_gameTileView.GameTile);
        }

        ///<summary>
        /// GameManager should call this when the time is right
        ///</summary>
        public void DealHand(GameTileArranger p_arranger)
        {
            PlayerBoard.Reset();

            GameTileGroup[] result = p_arranger.Arrange(PlayerHand);
            GameTileView currentView = null;

            for (int i = 0; i < result.Length; i++)
            {
                for (int j = 0; j < result[i].GameTileCount; j++)
                {
                    currentView = FindView(result[i][j]);
                    currentView.TranslateToPosition(PlayerBoard.GetNextPosition(), null);
                    currentView.TurnFaceUp();
                }
                PlayerBoard.SkipPosition();
            }
        }

        public void Reset(){
            PlayerHand = new GameTileGroup();
            PlayerHandViews = new List<GameTileView>();

            PlayerBoard.Reset();
        }

        ///<summary>
        /// Ah yes, since the sorter returns me GameTiles, there is this method to find the View that actually uses it.
        /// There should be another layer above Sorter and this controller
        ///</summary>
        private GameTileView FindView(GameTile p_gameTile){
            for(int i = 0; i < PlayerHandViews.Count; i++){
                if(PlayerHandViews[i].GameTile.TileIndex == p_gameTile.TileIndex){
                    return PlayerHandViews[i];
                }
            }

            return null;
        }
    }
}