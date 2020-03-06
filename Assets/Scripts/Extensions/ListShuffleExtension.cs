using UnityEngine;
using System.Collections.Generic;

namespace ZyngaDemo.Extensions{
    public static class ListShuffleExtension{
        ///<summary>
        /// Shuffle algorithm "Durstenfeld's"
        ///</summary>
        public static void Shuffle<T>(this IList<T> list){
            int n = list.Count;
            int randomIndex = -1;
            T temp = default;
            for(int i = 0; i < n - 1; i++){
                randomIndex = Random.Range(i, n);

                temp = list[randomIndex];
                list[randomIndex] = list[i];
                list[i] = temp;
            }
        }
    }
}