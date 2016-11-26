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
                //BuildNodes(nodesToInterpret[i]);
                SplitTokens(nodesToInterpret[i]);
            }
        }

        void BuildNodes(string newText)
        {
            string[] responseDelim = new string[]{ "[[" };
            Silk.Node newNode = new Silk.Node();
            StringBuilder newKey = new StringBuilder();
            //List<string> newResponseList = new List<string>(); 
            string[] newResponses;

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

        void SplitTokens(string newText)
        {
            StringBuilder tempKey = new StringBuilder();
            StringBuilder newKey = new StringBuilder();

            StringBuilder testLink = new StringBuilder();
            StringBuilder prompt = new StringBuilder();
            for (int i = 0; i < newText.Length; i++)
            {
                if (newKey.ToString() == "")
                {
                    if (newText[i] != '\n')
                    {
                        tempKey.Append(newText[i]);
                    }
                    else
                    {
                        newKey = tempKey;
                    }
                }
                else if(newText[i] == '[' && newText[i + 1] == '[')
                {
                    List<string> newLinks = new List<string>();
                    StringBuilder newLink = new StringBuilder();
                    for (int j = i; j < newText.IndexOf("]]"); j++)
                    {
                        newLink.Append(newText[j]);
                    }
                    if (newLink.ToString() != "")
                    {
                        newLinks.Add(newLink.ToString().TrimStart('['));
                    }
                    foreach(string link in newLinks)
                    {
                        
                        Debug.Log("link is " + i + " " + link);
                    }

                }
                else if(newText[i] == '<' && newText[i + 1] == '<')
                {
                    List<string> newCustomTags = new List<string>();
                    //Custom Tags
                }
                else
                {
                    prompt.Append(newText[i]);
                }
                
                
            }
            

        }

    }
}
