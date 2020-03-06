using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ZyngaDemo.GameLogic;

namespace ZyngaDemo.Unity{
    ///<summary>
    /// View for the tiles in the middle of the screen representing our deck.
    ///</summary>
    public class GameDeckView : MonoBehaviour
    {
        public Transform StackPositionsParent;
        public Transform OkeyPosition;

        public Transform[] StackPositions{get; private set;}

        private void Awake(){
            StackPositions = new Transform[StackPositionsParent.childCount];

            for(int i = 0; i < StackPositions.Length; i++){
                StackPositions[i] = StackPositionsParent.GetChild(i);
            }
        }
    }
}