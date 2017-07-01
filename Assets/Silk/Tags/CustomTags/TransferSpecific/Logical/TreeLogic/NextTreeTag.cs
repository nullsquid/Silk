using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Silk;
public class NextTreeTag : SilkTagBase {
	public NextTreeTag(List<string> args){
		if (args.Count == 1) {
			Value = "";
			//SetNextTree(args[0]
		} else {
			Value = "TOO MANY ARGS FOR TREE TAG";
		}
	}

}
