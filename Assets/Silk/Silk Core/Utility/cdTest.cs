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
		words = cd.result as string;
		Debug.Log("Result is " + words);
    }

    IEnumerator GetString() {
        yield return new WaitForSeconds(1.0f);
        factory = new TagFactory();
        Debug.Log("i");
        yield return Silk.Silky.Instance.mother.GetNodeByName("Sample","Start").silkLinks[0].LinkText;
        factory.SetTag(_name, _args);
        yield return factory.Tag;
    }
}
