﻿using UnityEngine;
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
        Importer importer;
        string textToParse;
        public string[] tweeNodesToInterpret;
        string[] delim = new string[] { ":: " };
        void Start()
        {
            
            nodeBuilder = new NodeBuilder();
            graphBuilder = new GraphBuilder();
            importer = GetComponent<Silk.Importer>();
            
            foreach (TextAsset currentTweeFile in importer.rawTweeFiles)
            {
                TextAsset tweeFile = currentTweeFile;
                string fileName = currentTweeFile.name;
                Debug.Log(tweeFile.text);
                //this works for single file
                //textToParse = testText.text;
                
                //hopefully this works
                textToParse = tweeFile.text;
                tweeNodesToInterpret = textToParse.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tweeNodesToInterpret.Length; i++)
                {
                    StringBuilder promptContainer = new StringBuilder(tweeNodesToInterpret[i]);

                    if (tweeNodesToInterpret[i].Contains("|"))
                    {
                        promptContainer.Replace("|", string.Empty);
                    }
                    if (tweeNodesToInterpret[i].Contains(ReturnTitle(tweeNodesToInterpret[i])))
                    {
                        promptContainer.Replace(ReturnTitle(tweeNodesToInterpret[i]), string.Empty, 0, ReturnTitle(tweeNodesToInterpret[i]).Length);
                    }
                    foreach (KeyValuePair<string, string> entry in ReturnLinks(tweeNodesToInterpret[i]))
                    {
                        if (tweeNodesToInterpret[i].Contains("[[" + entry.Key) || tweeNodesToInterpret[i].Contains("[[" + entry.Value))
                        {

                            promptContainer.Replace("[[" + entry.Key, string.Empty).Replace(entry.Value + "]]", string.Empty);
                        }
                    }

                    AssignDataToNodes(tweeNodesToInterpret[i], promptContainer.ToString());

                }
                graphBuilder.AddGraphToMother(fileName, graphBuilder.graph);
            }
            foreach(KeyValuePair<string, SilkNode> node in graphBuilder.graph)
            {
                //for testing
            }
            



        }


        
        void AssignDataToNodes(string newTweeData, string newPassage)
        {
            SilkNode newNode = new SilkNode();
            newNode.nodeName = ReturnTitle(newTweeData);
            newNode.links = ReturnLinks(newTweeData);
            newNode.tags = ReturnCustomTags(newTweeData);
            //add passage
            newNode.nodePassage = newPassage;
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
            return title.TrimStart(' ').TrimEnd(' ');
        }

        

        string ReturnPassageTags(string inputToExtractTagsFrom)
        {
            string newTag = "";
            return newTag;
        }

        Dictionary<string, string[]> ReturnCustomTags(string inputToExtractTagsFrom)
        {
            Dictionary<string, string[]> tags = new Dictionary<string, string[]>();
            List<string> rawTags = new List<string>();
            int iterations = 0;
            for (int i = 0; i < inputToExtractTagsFrom.Length; i++)
            {
                string rawTag = "";
                //to find each custom tag
                if (inputToExtractTagsFrom[i] == '<' && inputToExtractTagsFrom[i + 1] == '<')
                {
                    //to get data out of each tag
                    for (int j = i + 2; j < inputToExtractTagsFrom.Length; j++)
                    {
                        if(inputToExtractTagsFrom[j] == '>' && inputToExtractTagsFrom[j + 1] == '>')
                        {
                            rawTags.Add(rawTag);
                            break;
                        }
                        else
                        {
                            rawTag += inputToExtractTagsFrom[j];
                        }
                    }
                }
            }
            foreach(string tag in rawTags)
            {
                string tagName = "";
                string[] tagArgs = null;
                
                for(int i = 0; i < tag.Length; i++)
                {

                    if(tag[i] == '=')
                    {
                        tagArgs = tag.Substring(i + 1).Split(',');
                        break;
                    }
                    else
                    {
                        tagName += tag[i];
                        
                    }
                }
                if (tagArgs != null)
                {
                    
                    tags.Add(tagName + "_" + iterations, tagArgs);
                    
                }
                else
                {
                    tags.Add(tagName, null);
                }
                iterations += 1;
            }
            foreach(KeyValuePair<string, string[]> tag in tags)
            {
                //Debug.Log(tag.Key);
            }
            return tags;

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
