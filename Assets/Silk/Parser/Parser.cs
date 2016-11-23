using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Silk
{
    public class Parser : MonoBehaviour
    {
        public TextAsset testText;

        string textToParse;
        //List<string> nodesToInterpret = new List<string>();
        string[] nodesToInterpret;
        string[] delim = new string[] { ":: " };
        void Start()
        {
            textToParse = testText.text;
            nodesToInterpret = textToParse.Split(delim, StringSplitOptions.RemoveEmptyEntries);
        }

        void BuildNodes(string newText)
        {
            //newNode = 
        }
        
    }
}
