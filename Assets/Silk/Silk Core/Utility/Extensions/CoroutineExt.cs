using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Silk;

public static class CoroutineExt {
    public static Coroutine<T> StartCoroutine<T>(this MonoBehaviour obj, IEnumerator coroutine) {
        Coroutine<T> coroutineObject = new Coroutine<T>();
        coroutineObject.coroutine = obj.StartCoroutine(coroutineObject.InternalRoutine(coroutine));
        return coroutineObject;
    }
}

