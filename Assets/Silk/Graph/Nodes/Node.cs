using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Silk
{
    public class Node
    {

        string _key;
        string _prompt;
        public List<string> Responses = new List<string>();
        //
        Dictionary<string, Silk.Node> _neighbors = new Dictionary<string, Silk.Node>();
        
        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        public string Prompt
        {
            get
            {
                return _prompt;
            }

            set
            {
                _prompt = value;
            }
        }
    }
}
