using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawTag {

    string _tagName;
    List<string> _tagArgs = new List<string>();

    
    public string RawTagName
    {
        get
        {
            return _tagName;
        }
        set
        {
            _tagName = value;
        }

    }

    public List<string> TagArgs
    {
        get
        {
            return _tagArgs;
        }
    }

    public void AddArgument(string arg) {
        _tagArgs.Add(arg);
    }


        

}
