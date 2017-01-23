using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
public class SilkNode {


    //Might use this to avoid naming conflicts instead of a generic Node datastructure
    public string nodeName;
    public string nodePassage;
    public Dictionary<string, string> links = new Dictionary<string, string>();
    public Dictionary<string, List<string>> tags = new Dictionary<string, List<string>>();
	
}
