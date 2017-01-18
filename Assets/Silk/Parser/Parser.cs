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
            
            nodeBuilder = new NodeBuilder();
            graphBuilder = new GraphBuilder();
            textToParse = testText.text;
            tweeNodesToInterpret = textToParse.Split(delim, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tweeNodesToInterpret.Length; i++)
            {
                StringBuilder promptContainer = new StringBuilder(tweeNodesToInterpret[i]);
                AssignDataToNodes(tweeNodesToInterpret[i]);
                if (tweeNodesToInterpret[i].Contains("|"))
                {
                    promptContainer.Replace("|", string.Empty);
                }
                //if contains passagetag replace passagetag
                if (tweeNodesToInterpret[i].Contains(ReturnTitle(tweeNodesToInterpret[i])))
                {
                    //need to also deal with if there's a tag in the name
                    
                    promptContainer.Replace(ReturnTitle(tweeNodesToInterpret[i]), string.Empty, 0, ReturnTitle(tweeNodesToInterpret[i]).Length);
                }
                foreach(KeyValuePair<string, string> entry in ReturnLinks(tweeNodesToInterpret[i]))
                {
                    if(tweeNodesToInterpret[i].Contains("[["+entry.Key) || tweeNodesToInterpret[i].Contains("[[" + entry.Value))
                    {

                        promptContainer.Replace("[[" + entry.Key, string.Empty).Replace(entry.Value + "]]", string.Empty);
                    }
                }
                
                
                Debug.Log("container is " + promptContainer);
                
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
            //Debug.Log("title is " + title);
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
