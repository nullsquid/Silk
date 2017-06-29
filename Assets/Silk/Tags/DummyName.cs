using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Silk;
public class DummyName : SilkTagBase {
    private string _name;
    private string tagToReplace;
    private string _newName = "MEMM";
	public string Name {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
	public DummyName(string[] args, int _priority) {
        DefineArguments(args);
		priority = _priority;
        if(args.Length == 1) {
            //I want to be able to roll the actual string.replace method into this constructor if at all possible
            ReplaceTagWithName(args[0]);
        }
        else {
            Debug.LogWarning("Too many name arguments");
        }

        //_tagName = tagName;
    }

    public override void SilkTagDefinition() {
        
    }

    public string ReplaceTagWithName(string id) {
        //realistically would take id and figure out the name based on it
        return _newName;
    }
}
