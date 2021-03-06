﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.Unity{
    ///<summary>
    /// Kinda hardcoded, but works
    ///</summary>
    public class PlayerInputController : MonoBehaviour
    {
        // This field is used during touch up to stop user from messing up the Coroutines
        public bool CanTakeInput;

        // Used during search for BoardSlotView
        private BoardView _playerBoard;

        private GameTileView _selectedView;

        // The tiles in the middle of the screen can also be interacted with,
        // The Vector3 field is there to return them back
        private BoardSlotView _lastBoardSlot;
        private Vector3 _lastPosition;

        private void Awake(){
            CanTakeInput = true;

            _selectedView = null;
        }

        public void Init(BoardView p_boardView){
            _playerBoard = p_boardView;
        }

        private void Update(){
            if(!CanTakeInput){
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
                if(rayHit.transform != null){
                    _selectedView = rayHit.transform.gameObject.GetComponent<GameTileView>();
                    if (_selectedView != null)
                    {
                        _lastBoardSlot = _selectedView.BoardSlotView;
                        _lastPosition = _selectedView.transform.position;
                    }
                }
            }else if(Input.GetMouseButton(0)){
                if(_selectedView != null){
                    MoveViewToWorldPosition(Input.mousePosition);
                }
            }else if(Input.GetMouseButtonUp(0)){
                if(_selectedView != null){
                    CanTakeInput = false;
                    BoardSlotView targetBoardSlot = null;
                    FindPosition(out targetBoardSlot);
                    if(_lastBoardSlot != null){
                        _lastBoardSlot.UnFill();
                        targetBoardSlot.Fill();

                        _selectedView.TranslateToPosition(targetBoardSlot, () => { ViewMoveFinished(); });
                    }else{
                        _selectedView.TranslateToPosition(_lastPosition, () => { ViewMoveFinished(); });
                    }

                    _selectedView = null;
                }
            }
        }

        ///<summary>
        /// Contains wierd double if becuse of the last day rush inside BoardView
        ///</summary>
        private bool FindPosition(out BoardSlotView p_boardView){
            for(int i = 0; i < _playerBoard.UpperPositions.Length; i++){
                if (Vector2.Distance(_playerBoard.UpperPositions[i].transform.position, _selectedView.transform.position) < 1f
                && !_playerBoard.UpperPositions[i].IsFilled)
                {
                    p_boardView = _playerBoard.UpperPositions[i];
                    return true;
                }

                if (Vector2.Distance(_playerBoard.LowerPositions[i].transform.position, _selectedView.transform.position) < 1f
                && !_playerBoard.LowerPositions[i].IsFilled)
                {
                    p_boardView = _playerBoard.LowerPositions[i];
                    return true;
                }
            }

            p_boardView = _lastBoardSlot;
            return false;
        }

        ///<summary>
        /// Used during touch and drag
        ///</summary>
        private void MoveViewToWorldPosition(Vector2 p_screenPosition){
            Vector3 targetPosition = Vector3.zero;
            targetPosition = Camera.main.ScreenToWorldPoint(p_screenPosition);
            targetPosition.z = _selectedView.transform.position.z;

            _selectedView.transform.position = targetPosition;
        }

        private void ViewMoveFinished(){
            CanTakeInput = true;
        }
    }
}