using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Silk;
public class NameTag : SilkTagBase {

	public NameTag(List<string> args){
		//dummy, this will actually
		if (args.Count == 1) {
			//Will really be something like Value = CharacterManager.GetCharacterName(args[0]);
			Value = "MEMM";
		} else {
			Value = "NOPE";
		}
	}

}
