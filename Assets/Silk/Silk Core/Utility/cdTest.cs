using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Silk;
public class cdTest : MonoBehaviour {

	string words = "";
    //below is just for testing
    string _name = "name";
    string[] _args = { "C" };
    
    //
    TagFactory factory;
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
        yield return Random.Range(-100, 100);
        //yield return factory.Tag;
    }
}
