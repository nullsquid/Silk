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
        GraphBuilder graphBuilder;
        string textToParse;
        //List<string> nodesToInterpret = new List<string>();
        public string[] tweeNodesToInterpret;
        string[] delim = new string[] { ":: " };
        void Start()
        {
            nodeBuilder = new NodeBuilder();
            graphBuilder = new GraphBuilder();
            textToParse = testText.text;
            tweeNodesToInterpret = textToParse.Split(delim, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tweeNodesToInterpret.Length; i++)
            {
                AssignDataToNodes(tweeNodesToInterpret[i]);
            }
        }

        
        
        void AssignDataToNodes(string newTweeData)
        {
            SilkNode newNode = new SilkNode();
            newNode.nodeName = ReturnTitle(newTweeData);
            newNode.links = ReturnLinks(newTweeData);
            graphBuilder.AddToGraph(newNode.nodeName, newNode);
        }

        /*string ReturnGraphTitle(string newTweeData)
        {
            string newGraphTitle;

        }*/

        string ReturnTitle(string inputToExtractTitleFrom)
        {
            string title = "";
            for(int i = 0;i < inputToExtractTitleFrom.Length; i++)
            {
                if(inputToExtractTitleFrom[i] == '\n' || inputToExtractTitleFrom[i] == '[')
                {
                    if(title == "StoryTitle")
                    {
                        //
                    }
                    break;
                }
                else
                {
                    title += inputToExtractTitleFrom[i];
                }

                //put StoryTitle check in this function
            }
            Debug.Log("title is " + title);
            return title;
        }

        string ReturnPassageTags(string inputToExtractTagsFrom)
        {
            string newTag = "";
            return newTag;
        }

        string ReturnCustomTags(string inputToExtractTagsFrom)
        {
            return null;
        }

        Dictionary<string, string> ReturnLinks(string inputToExtractLinksFrom)
        {
            Dictionary<string, string> newLinks = new Dictionary<string, string>();
            for (int i = 0; i < inputToExtractLinksFrom.Length; i++){
                if(inputToExtractLinksFrom[i] == '[' && inputToExtractLinksFrom[i + 1] == '[')
                {
                    
                    string newLink = "";
                    int linkLength;
             
                    for(int j = i + 2; j < inputToExtractLinksFrom.Length; j++)
                    {
                        if(inputToExtractLinksFrom[j] == '|')
                        {
                            string newLinkValue = "";
                            for(int k = j + 1; k < inputToExtractLinksFrom.Length; k++)
                            {
                                if(inputToExtractLinksFrom[k] == ']')
                                {
                                    newLinks.Add(newLink, newLinkValue);
                                    Debug.Log("new link value is " + newLinkValue);
                                    break;
                                }
                                else
                                {
                                    newLinkValue += inputToExtractLinksFrom[k];
                                    if (inputToExtractLinksFrom[j] == ']')
                                    {

                                        newLinks.Add(newLink, newLink);
                                        Debug.Log("new link is " + newLink);
                                        break;
                                    }
                                }
                            }
                        }
                        if(inputToExtractLinksFrom[j] == ']')
                        {
                            if (!newLink.Contains("|"))
                            {
                                newLinks.Add(newLink, newLink);
                                Debug.Log("new link is " + newLink);
                                break;
                            }
                        }
                        else
                        {
                            newLink += inputToExtractLinksFrom[j];

                        }
                    }

                }
            }
            
            
            return newLinks;
        }

        
        /*
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
        */

    }
}
