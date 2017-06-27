using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace Silk {
    public class Coroutine<T> {

        private T returnVal;
        public Coroutine coroutine;

        public IEnumerator InternalRoutine(IEnumerator coroutine) {
            while (true) {
                if (!coroutine.MoveNext()) {
                    yield break;
                }
                object yielded = coroutine.Current;
                if (yielded != null && yielded.GetType() == typeof(T)) {
                    returnVal = (T)yielded;
                    yield break;
                }
                else {
                    yield return coroutine.Current;
                }
            }
        }
    }
//}
