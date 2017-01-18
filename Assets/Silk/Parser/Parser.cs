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
        public string[] tweeNodesToInterpret;
        string[] delim = new string[] { ":: " };
        void Start()
        {
            string promptContainer = "";
            nodeBuilder = new NodeBuilder();
            graphBuilder = new GraphBuilder();
            textToParse = testText.text;
            tweeNodesToInterpret = textToParse.Split(delim, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tweeNodesToInterpret.Length; i++)
            {
                AssignDataToNodes(tweeNodesToInterpret[i]);
                promptContainer = tweeNodesToInterpret[i].Replace(ReturnTitle(tweeNodesToInterpret[i]), string.Empty);
                //promptContainer = tweeNodesToInterpret[i].Replace(ReturnLinks)
                promptContainer = tweeNodesToInterpret[i].Replace(ReturnCustomTags(tweeNodesToInterpret[i]), string.Empty);
                promptContainer = tweeNodesToInterpret[i].Replace(ReturnPassageTags(tweeNodesToInterpret[i]), string.Empty);
                Debug.Log(promptContainer);
                
            }
            //for testing
            for(int j = 0; j < tweeNodesToInterpret.Length; j++)
            {
                Debug.Log("test " + tweeNodesToInterpret[j]);
            }

        }

        
        
        void AssignDataToNodes(string newTweeData)
        {
            SilkNode newNode = new SilkNode();
            newNode.nodeName = ReturnTitle(newTweeData);
            newNode.links = ReturnLinks(newTweeData);
            //add passage
            graphBuilder.AddToGraph(newNode.nodeName, newNode);
        }

        void AssignPassageToNodes(string newTweeData)
        {

        }

        string ReturnTitle(string inputToExtractTitleFrom)
        {
            string title = "";
            for(int i = 0;i < inputToExtractTitleFrom.Length; i++)
            {
                if(inputToExtractTitleFrom[i] == '\n' || inputToExtractTitleFrom[i] == '[')
                {
                    //title/graph parsing should go here
                    //
                    break;
                }
                else
                {
                    title += inputToExtractTitleFrom[i];
                    

                }

            }
            Debug.Log("title is " + title);
            return title.TrimStart(' ').TrimEnd(' ');
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
                                    break;
                                }
                                else
                                {
                                    newLinkValue += inputToExtractLinksFrom[k];
                                    if (inputToExtractLinksFrom[j] == ']')
                                    {

                                        newLinks.Add(newLink, newLink);
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

        //garbage fire
        string ReturnPassage(string inputToExtractPassageFrom)
        {
            Debug.Log("full text is " + inputToExtractPassageFrom);
            string passage = "";
             
            for(int i = 0; i < inputToExtractPassageFrom.Length; i++)
            {
                if(inputToExtractPassageFrom[i] == ':')
                {
                    for(int j = i; j < inputToExtractPassageFrom.Length; j++)
                    {

                    }
                }
                else if (inputToExtractPassageFrom[i] == '[' || inputToExtractPassageFrom[i] == '<')
                {

                }
                else
                {
                    passage += inputToExtractPassageFrom[i];
                }

            }
            Debug.Log("passage is " + passage);
            return passage;
        }

        
        

    }
}
