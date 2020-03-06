using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZyngaDemo.Unity{

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