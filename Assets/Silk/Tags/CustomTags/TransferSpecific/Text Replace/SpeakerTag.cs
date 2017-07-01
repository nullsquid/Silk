using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Silk;
public class SpeakerTag : SilkTagBase {

	public SpeakerTag(List<string> args){
		if (args.Count == 1) {
			//check arg[0] against the name list and replace with the appropriate name
			//Value = CharacterManager.GetName(args [0]); --probably
		} else {
			Value = "YA DOOFED UP";
		}
	}
}
