using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

using ZyngaDemo.GameLogic;

namespace ZyngaDemo.Unity{
    public class GameTileView : MonoBehaviour
    {
        public TextMeshPro TileText;
        public GameObject ThatHole;

        public GameTile GameTile{get; private set;}

        public BoardSlotView BoardSlotView;

        private Coroutine _translationCoroutine;

        private bool _translationInProgress;

        public void Init(GameTile p_gameTile){
            GameTile = p_gameTile;

            TileText.text = p_gameTile.TileNumber.ToString();
            TileText.color = GetTileColor(p_gameTile.TileColor);
            TileText.faceColor = GetTileColor(p_gameTile.TileColor);

            gameObject.name = "GameTile_" + p_gameTile.TileColor + "_" + p_gameTile.TileNumber;
        }

        public void SetPosition(Vector3 p_targetPosition){
            transform.position = p_targetPosition;
        }

        public void TurnFaceUp(){
            TileText.enabled = true;
            ThatHole.SetActive(true);
        }

        public void TurnFaceDown()
        {
            TileText.enabled = false;
            ThatHole.SetActive(false);
        }

        public void TranslateToPosition(Vector3 p_position)
        {
            if (_translationInProgress)
            {
                StopCoroutine(_translationCoroutine);
            }

            _translationCoroutine = StartCoroutine(SmoothMovement(p_position, null));
        }

        public void TranslateToPosition(BoardSlotView p_boardSlotView, Action p_callBack)
        {
            BoardSlotView = p_boardSlotView;

            if (_translationInProgress)
            {
                StopCoroutine(_translationCoroutine);
            }

            _translationCoroutine = StartCoroutine(SmoothMovement(BoardSlotView.transform.position, p_callBack));
        }

        public void TranslateToPosition(Vector3 p_targetPosition, Action p_callBack)
        {
            if (_translationInProgress)
            {
                StopCoroutine(_translationCoroutine);
            }

            _translationCoroutine = StartCoroutine(SmoothMovement(p_targetPosition, p_callBack));
        }

        private IEnumerator SmoothMovement(Vector3 p_targetPosition, Action p_callBack){
            Vector3 startPosition = transform.position;

            float timer = 0.3f;
            float maxTimer = timer;

            _translationInProgress = true;

            while(timer >= 0f){

                transform.position = Vector3.Lerp(startPosition, p_targetPosition, (maxTimer - timer) / maxTimer);

                timer -= Time.deltaTime;

                yield return null;
            }

            _translationInProgress = false;

            if(p_callBack != null){
                p_callBack.Invoke();
            }

            SetPosition(p_targetPosition);
        }

        private Color GetTileColor(int p_tileColor){
            switch(p_tileColor){
                case 0:{
                    return Color.red;
                }
                case 1:{
                    return Color.blue;
                }
                case 2:{
                    return Color.black;
                }
                case 3:{
                    return new Color(249f / 255f, 225f / 255f, 0f);
                }
                default:{
                    return Color.cyan;
                }
            }
        }
    }
}