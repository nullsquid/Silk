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
        NodeBuilder nodeBuilder;
        string textToParse;
        //List<string> nodesToInterpret = new List<string>();
        public string[] tweeNodesToInterpret;
        string[] delim = new string[] { ":: " };
        void Start()
        {
            nodeBuilder = new NodeBuilder();
            textToParse = testText.text;
            tweeNodesToInterpret = textToParse.Split(delim, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tweeNodesToInterpret.Length; i++)
            {
                //BuildNodes(nodesToInterpret[i]);
                //SplitTokens(tweeNodesToInterpret[i]);
                
            }
        }

        void AssignDataToNodes(string newTweeData)
        {
            SilkNode newNode = new SilkNode();
            newNode.nodeName = ReturnTitle(newTweeData);
            for(int i = 0; i < newTweeData.Length; i++)
            {
                
            }
        }

        string ReturnTitle(string inputToExtractTitleFrom)
        {
            string title = "";
            for(int i = 0;i < inputToExtractTitleFrom.Length; i++)
            {
                if(inputToExtractTitleFrom[i] == '\n' || inputToExtractTitleFrom[i] == '[')
                {
                    break;
                }
                else
                {
                    title += inputToExtractTitleFrom[i];
                }
            }
            return title;
        }
        /*string ReturnTitle(inputTweeText)
        {

        }*/

        

        void SplitTokens(string newText)
        {
            StringBuilder tempKey = new StringBuilder();
            StringBuilder newKey = new StringBuilder();

            StringBuilder prompt = new StringBuilder();
            for (int i = 0; i < newText.Length; i++)
            {
                List<string> newLinks = new List<string>();
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
                    
                    StringBuilder newLink = new StringBuilder();
                    for (int j = i; j < newText.IndexOf("]]"); j++)
                    {
                        newLink.Append(newText[j]);
                    }
                    if (newLink.ToString() != "")
                    {
                        newLinks.Add(newLink.ToString().TrimStart('['));
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
                
                //nodeBuilder.BuildNode(newKey.ToString(), prompt.ToString(), newLinks);
                //might need to do it in a new for loop
            }
            

        }

    }
}
