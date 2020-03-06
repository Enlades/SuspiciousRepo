using System.Collections.Generic;
using UnityEngine;

using ZyngaDemo.Extensions;

namespace ZyngaDemo.GameLogic
{
    public class GameTileDeck
    {
        ///<summary>
        /// If you change this, you need to modify multiple fields that contain
        /// modular operations and divisions that assumed the Deck size to be always 104
        ///</summary>
        public const int DECK_SIZE = 104;

        ///<summary>
        /// There is a cursor varible that sort of works like an iterator from c++
        /// It just points to the start of the Deck and incremented with Draw method.
        ///</summary>
        public bool IsDeckEmpty{
            get{
                return _deckCursor >= _deckTiles.Count;
            }
        }

        ///<summary>
        /// Unfinished code
        ///</summary>
        public GameTile OkeyTile{get; private set;}

        ///<summary>
        /// This is the part where 104 tiles come from,
        /// every other GameTile is copied from these guys
        ///</summary>
        private List<GameTile> _deckTiles;

        /// <summary>
        /// The cursor i told you about, an index starting from 0, incremented with each draw
        /// </summary>
        private int _deckCursor;

        public GameTileDeck(){
            _deckTiles = new List<GameTile>();

            PopulateDeck();
        }

        ///<summary>
        /// Attempts to draw a GameTile from the deck,
        /// that return default is not always handled by caller.
        ///</summary>
        public GameTile GetRandomTile(){
            if(IsDeckEmpty){
                Debug.LogWarning("Deck is empty");
                return default;
            }

            return _deckTiles[_deckCursor++];
        }

        ///<summary>
        /// The better approach here would be to reshuffle the deck
        /// and reset the cursor. Restructure PopulateDeck method
        ///</summary>
        public void Reset(){
            _deckCursor = 0;

            _deckTiles = new List<GameTile>();
            PopulateDeck();
        }

        ///<summary>
        /// Unfinished code, i've made a terrible attempt on OkeyTile implementation.
        ///</summary>
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