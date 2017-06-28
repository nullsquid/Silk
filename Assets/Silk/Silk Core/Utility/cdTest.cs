using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cdTest : MonoBehaviour {

    private void Start() {
        StartCoroutine(PrintWords());
    }
    IEnumerator PrintWords() {
        CoroutineDataWrapper cd = new CoroutineDataWrapper(this, GetString());
        yield return cd.coroutine;
        Debug.Log("Result is " + cd.result);
    }

    IEnumerator GetString() {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("i");
        yield return Silk.Silky.Instance.mother.GetNodeByName("Sample","Start").silkLinks[0].LinkText;
    }
}
