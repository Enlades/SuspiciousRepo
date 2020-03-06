using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.Unity{
    public class BoardView : MonoBehaviour
    {
        // Last day rush 
        public Transform UpperPositionsParent;
        public Transform LowerPositionsParent;

        public BoardSlotView[] UpperPositions{get; private set;}
        public BoardSlotView[] LowerPositions{get; private set;}

        ///<summary>
        /// This cursor iterates between BoardSlotViews
        ///</summary>
        private int PositionCursor
        {
            get{
                return _positionCursor;
            }
            set{
                _positionCursor = value;

                if(_positionCursor >= 14){
                    _positionCursor = 0;
                    _isUpper = false;
                }
            }
        }
        private int _positionCursor;

        private bool _isUpper;

        private void Awake(){
            UpperPositions = new BoardSlotView[14];
            LowerPositions = new BoardSlotView[14];

            PositionCursor = 0;
            _isUpper = true;

            BoardSlotView tempRef = null;
            GameObject tempObj = null;

            // Behold my greatest creation
            for (int i = 0; i < 14; i++)
            {
                tempObj = new GameObject("Upper_" + i);
                tempRef = tempObj.AddComponent<BoardSlotView>();

                tempObj.transform.position = UpperPositionsParent.position + Vector3.left * 6.5f + Vector3.right * i;
                tempObj.transform.SetParent(UpperPositionsParent);

                UpperPositions[i] = tempRef;

                tempObj = new GameObject("Lower_" + i);
                tempRef = tempObj.AddComponent<BoardSlotView>();

                tempObj.transform.position = LowerPositionsParent.position + Vector3.left * 6.5f + Vector3.right * i;
                tempObj.transform.SetParent(LowerPositionsParent);

                LowerPositions[i] = tempRef;
            }
        }

        ///<summary>
        /// PlayerHandController calls this after drawing a tile to hand to find positions to it
        ///</summary>
        public BoardSlotView GetNextPosition(){
            if(_isUpper){
                UpperPositions[PositionCursor].Fill();
                return UpperPositions[PositionCursor++];
            }else{
                LowerPositions[PositionCursor].Fill();
                return LowerPositions[PositionCursor++];
            }
        }
        
        public void Reset(){
            for (int i = 0; i < 14; i++)
            {
                UpperPositions[i].UnFill();
                LowerPositions[i].UnFill();
            }

            PositionCursor = 0;
            _isUpper = true;
        }

        ///<summary>
        /// PlayerHandContoller calls this after finishing a group
        ///</summary>
        public void SkipPosition(){
            PositionCursor++;
        }
    }
}