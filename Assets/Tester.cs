using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Silk;
public class Tester : MonoBehaviour {

	void Start(){

		StartCoroutine (PrintLinks ());
	}

	IEnumerator PrintLinks(){
		yield return new WaitForEndOfFrame ();
		Debug.Log ("working");
		foreach (KeyValuePair<string, SilkStory> story in Silky.Instance.mother.MotherStory) {
			foreach (KeyValuePair<string, SilkNode> node in story.Value.Story) {
				foreach (SilkLink silkLink in node.Value.silkLinks) {
					
					Debug.Log ("LYNX " +silkLink.LinkText);
				}
			}
		}
	}

	
}
