using UnityEngine;
using System;
using System.Text;
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
            for (int i = 0; i < nodesToInterpret.Length; i++)
            {
                BuildNodes(nodesToInterpret[i]);
            }
        }

        void BuildNodes(string newText)
        {
            Silk.Node newNode = new Silk.Node();
            StringBuilder newKey = new StringBuilder();

            for (int i = 0; i < newText.Length; i++)
            {
                if (newText[i] != '\n')
                {
                    newKey.Append(newText[i]);
                }
                else
                {
                    break;
                }
            }
            for(int j = 0; j < newText.Length; j++)
            {

            }
            newNode.Key = newKey.ToString();
            Debug.Log("Key is " + newNode.Key);
        }
        
    }
}
