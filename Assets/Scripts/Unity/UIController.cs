using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace ZyngaDemo.Unity{
    public class UIController : MonoBehaviour
    {
        public GameObject GamePanel;

        private Button[] _panelButtons;

        private Action _colorSortAction, _numberSortAction, _smartSortAction, _resetAction;

        private void Awake(){
            HideGamePanel();

            _panelButtons = GamePanel.GetComponentsInChildren<Button>();
        }

        public void Init(Action p_colorSort, Action p_numberSort, Action p_smartSort, Action p_reset){
            _colorSortAction = p_colorSort;
            _numberSortAction = p_numberSort;
            _smartSortAction = p_smartSort;
            _resetAction = p_reset;
        }

        public void ShowGamePanel(){
            GamePanel.SetActive(true);
            StartCoroutine(SmoothReveal());
        }

        public void HideGamePanel(){
            GamePanel.SetActive(false);
        }

        public void ColorSortButton(){
            if (_colorSortAction != null)
            {
                _colorSortAction.Invoke();
            }
        }

        public void NumberSortButton(){
            if (_numberSortAction != null)
            {
                _numberSortAction.Invoke();
            }
        }

        public void SmartSortButton(){
            if (_smartSortAction != null)
            {
                _smartSortAction.Invoke();
            }
        }

        public void ResetButton(){
            if (_resetAction != null)
            {
                _resetAction.Invoke();
            }
        }

        public void DisableButtons(){
            for (int i = 0; i < _panelButtons.Length; i++)
            {
                _panelButtons[i].interactable = false;
            }
        }

        public void EnableButtons(){
            for (int i = 0; i < _panelButtons.Length; i++)
            {
                _panelButtons[i].interactable = true;
            }
        }

        private IEnumerator SmoothReveal(){
            Graphic[] everyGraphic = GamePanel.GetComponentsInChildren<Graphic>();
            Color[] startColors = new Color[everyGraphic.Length];
            Color[] targetColors = new Color[everyGraphic.Length];

            for(int i = 0; i < everyGraphic.Length; i++){
                targetColors[i] = everyGraphic[i].color;
                startColors[i] = targetColors[i];
                startColors[i].a = 0f;
            }

            float timer = 0.5f;
            float maxTimer = timer;

            while(timer >= 0f){

                for(int i = 0; i < everyGraphic.Length; i++){
                    everyGraphic[i].color = Color.Lerp(startColors[i], targetColors[i], (maxTimer - timer) / maxTimer);
                }

                timer -= Time.deltaTime;

                yield return null;
            }
        }
    }
}