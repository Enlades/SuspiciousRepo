using System.Collections.Generic;
using UnityEngine;

using ZyngaDemo.Extensions;

namespace ZyngaDemo.GameLogic
{
    public class GameTileDeck
    {
        public const int DECK_SIZE = 104;

        public bool IsDeckEmpty{
            get{
                return _deckCursor >= _deckTiles.Count;
            }
        }

        public GameTile OkeyTile{get; private set;}

        private List<GameTile> _deckTiles;

        /// <summary>
        /// An index starting from 0, incremented with each draw
        /// </summary>
        private int _deckCursor;

        public GameTileDeck(){
            _deckTiles = new List<GameTile>();

            PopulateDeck();
        }

        public GameTile GetRandomTile(){
            if(IsDeckEmpty){
                Debug.LogWarning("Deck is empty");
                return default;
            }

            return _deckTiles[_deckCursor++];
        }

        public void Reset(){
            _deckCursor = 0;

            _deckTiles = new List<GameTile>();
            PopulateDeck();
        }

        private void PopulateDeck(){
            int okeyIndex = Random.Range(0, DECK_SIZE);

            OkeyTile = new GameTile(okeyIndex, true);

            for(int i = 0; i < DECK_SIZE; i++){
                _deckTiles.Add(new GameTile(i, i == (okeyIndex % (DECK_SIZE / 2))));
            }

            _deckTiles.Shuffle();

            _deckCursor = 0;
        }
    }
}