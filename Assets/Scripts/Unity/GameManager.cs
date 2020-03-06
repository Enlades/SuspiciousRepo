using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZyngaDemo.GameLogic;

namespace ZyngaDemo.Unity{
    ///<summary>
    /// The hardworker
    ///</summary>
    public class GameManager : MonoBehaviour
    {
        public GameDeckController GameDeckController;
        public UIController UIController;
        public PlayerHandController PlayerHandController;
        public PlayerInputController PlayerInputController;

        public PrefabManager PrefabManager;

        ///<summary>
        /// Player hand is kept as a reference on GameManager
        ///</summary>
        private GameTileGroup _playerHand;

        private void Awake(){
            _playerHand = new GameTileGroup();
        }

        private void Start(){
            GameDeckController.Init();
            PlayerInputController.Init(PlayerHandController.PlayerBoard);
            UIController.Init(() => { ColorSort(); }, () => { NumberSort(); }, () => { SmartSort(); }, () => { Reset(); });

            GameDeckController.CreateGameTiles(PrefabManager.GameTileViewPrefab, ()=>{DeckRevealFinished();});
        }

        ///<summary>
        /// Callback for the Deck reveal
        ///</summary>
        private void DeckRevealFinished(){
            UIController.ShowGamePanel();

            DrawPlayerHand();
        }

        private void DrawPlayerHand(){
            for (int i = 0; i < 14; i++)
            {
                PlayerHandController.AddTile(GameDeckController.DrawTile());
            }

            PlayerHandController.DealHand(new GameTileSmartArranger(new GameTile(50, true)));
        }

        private void NumberSort(){
            PlayerHandController.DealHand(new GameTileSameNumberArranger(new GameTile(50, true)));
        }

        private void ColorSort(){
            PlayerHandController.DealHand(new GameTileSameColorArranger(new GameTile(50, true)));
        }

        private void SmartSort(){
            PlayerHandController.DealHand(new GameTileSmartArranger(new GameTile(50, true)));
        }

        private void Reset(){
            PlayerHandController.Reset();
            GameDeckController.Reset();
            UIController.DisableButtons();

            StartCoroutine(UnOrganizedDelay());
        }

        ///<summary>
        /// After deck reset, there is no callback, we just wait manually
        ///</summary>
        private IEnumerator UnOrganizedDelay(){
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 14; i++)
            {
                PlayerHandController.AddTile(GameDeckController.DrawTile());
            }

            PlayerHandController.DealHand(new GameTileSmartArranger(new GameTile(50, true)));

            yield return new WaitForSeconds(0.4f);

            UIController.EnableButtons();
        }
    }
}