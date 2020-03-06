using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.Unity{

    ///<summary>
    /// View for the slots on the board, GameTileView finds it's way on the screen using the transform of this mono,
    /// also IsFilled is used by PlayerInputController
    ///</summary>
    public class BoardSlotView : MonoBehaviour
    {
        public bool IsFilled{get; private set;}
        
        private void Awake(){
            IsFilled = false;
        }

        public void Fill(){
            IsFilled = true;
        }

        public void UnFill(){
            IsFilled = false;
        }
    }
}