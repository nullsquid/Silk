using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineDataWrapper {
    public Coroutine coroutine { get; private set; }
    public object result;
    IEnumerator target;

    public CoroutineDataWrapper(MonoBehaviour owner, IEnumerator target) {
        this.target = target;
        coroutine = owner.StartCoroutine(Run());
    }

    IEnumerator Run() {
        while (target.MoveNext()) {
            result = target.Current;
            yield return result;
        }
    }
}
