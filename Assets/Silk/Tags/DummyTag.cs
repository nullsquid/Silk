using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Silk;
public class DummyTag : SilkTagBase {

	public DummyTag(string tagName, List<string> args)
    {
        DefineArguments(args);
		Value = "BOOP!";
        _tagName = tagName;
    }

    public override void SilkTagDefinition()
    {
        base.SilkTagDefinition();
    }

}
