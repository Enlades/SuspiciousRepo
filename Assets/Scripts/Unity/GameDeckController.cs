using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using ZyngaDemo.GameLogic;

namespace ZyngaDemo.Unity{
    ///<summary>
    /// Works similar to the GameTileDeck
    ///</summary>
    public class GameDeckController : MonoBehaviour
    {
        public GameDeckView GameDeckView;

        public GameTileDeck GameTileDeck{get; private set;}
        public GameTileView[] GameTiles{get; private set;}

        private WaitForSeconds _tileDelay;

        private GameObject _gameTileViewsParent;

        ///<summary>
        /// Unfinished code
        ///</summary>
        private GameTileView _dummyOkeyTile;

        private int _deckCursor;

        public void Init(){
            _deckCursor = 0;

            GameTileDeck = new GameTileDeck();
            _tileDelay = new WaitForSeconds(0.01f);

            _gameTileViewsParent = new GameObject("GameTileViewsParent");
        }

        ///<summary>
        /// Callback is managed by GameManager
        ///</summary>
        public void CreateGameTiles(GameTileView p_gameTilePrefab, Action p_callback){
            GameTiles = new GameTileView[GameTileDeck.DECK_SIZE];

            for (int i = 0; i < GameTileDeck.DECK_SIZE; i++)
            {
                GameTiles[i] = Instantiate(p_gameTilePrefab);
                GameTiles[i].transform.SetParent(_gameTileViewsParent.transform);
                GameTiles[i].TurnFaceDown();
            }

            StartCoroutine(SmoothDeckReveal(p_callback));
        }

        public GameTileView DrawTile(){
            GameTiles[_deckCursor].Init(GameTileDeck.GetRandomTile());

            return GameTiles[_deckCursor++];
        }

        public void Reset(){
            for (int i = 0; i < GameTileDeck.DECK_SIZE; i++)
            {
                GameTiles[i].TurnFaceDown();
                GameTiles[i].TranslateToPosition(GameDeckView.StackPositions[i / 26].position);
            }

            _dummyOkeyTile.TurnFaceDown();

            _deckCursor = 0;
            GameTileDeck.Reset();

            StartCoroutine(DummyOkeyHardCode());
        }

        private IEnumerator SmoothDeckReveal(Action p_callback){

            for (int i = 0; i < GameTileDeck.DECK_SIZE; i++)
            {
                yield return _tileDelay;

                GameTiles[i].TranslateToPosition(GameDeckView.StackPositions[i / 26].position);
            }

            if(p_callback != null){
                p_callback.Invoke();
            }

            _dummyOkeyTile = DrawTile();
            _dummyOkeyTile.TurnFaceUp();
            _dummyOkeyTile.TranslateToPosition(GameDeckView.OkeyPosition.position);
        }

        private IEnumerator DummyOkeyHardCode(){
            yield return new WaitForSeconds(1f);

            _dummyOkeyTile = DrawTile();
            _dummyOkeyTile.TurnFaceUp();
            _dummyOkeyTile.TranslateToPosition(GameDeckView.OkeyPosition.position);
        }
    }
}
