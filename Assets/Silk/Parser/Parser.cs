using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Silk;
namespace Silk
{
    public class Parser : MonoBehaviour
    {
        GraphBuilder graphBuilder;
        Importer importer;
        string textToParse;
        string[] tweeNodesToInterpret;
        string[] delim = new string[] { ":: " };
        void Start()
        {
            
            graphBuilder = new GraphBuilder();
            importer = GetComponent<Silk.Importer>();
            List<string> filenames = new List<string>();

            foreach (TextAsset currentTweeFile in importer.rawTweeFiles)
            {
                TextAsset tweeFile = currentTweeFile;
                string fileName = currentTweeFile.name;
                //this works for single file
                //textToParse = testText.text;
                
                textToParse = tweeFile.text;
                tweeNodesToInterpret = textToParse.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tweeNodesToInterpret.Length; i++)
                {
                    string storyTitle = "";
                    StringBuilder promptContainer = new StringBuilder(tweeNodesToInterpret[i]);

                    if (tweeNodesToInterpret[i].Contains("|"))
                    {
                        promptContainer.Replace("|", string.Empty);
                    }
                    if (tweeNodesToInterpret[i].Contains(ReturnTitle(tweeNodesToInterpret[i])))
                    {
                        //Debug.Log(ReturnTitle(tweeNodesToInterpret[i]));
                        string storyTitleCheck = ReturnTitle(tweeNodesToInterpret[i]).TrimStart().TrimEnd();
                        if (storyTitleCheck == "StoryTitle")
                        {

                            storyTitle = ReturnStoryTitle(tweeNodesToInterpret[i]);
                            Debug.Log("title");
                        }
                        else {
                            promptContainer.Replace(ReturnTitle(tweeNodesToInterpret[i]), string.Empty, 0, ReturnTitle(tweeNodesToInterpret[i]).Length);
                        }
                    }
                    foreach (KeyValuePair<string, string> entry in ReturnLinks(tweeNodesToInterpret[i]))
                    {
                        if (tweeNodesToInterpret[i].Contains("[[" + entry.Key) || tweeNodesToInterpret[i].Contains("[[" + entry.Value))
                        {

                            promptContainer.Replace("[[" + entry.Key, string.Empty).Replace(entry.Value + "]]", string.Empty);
                            promptContainer.Replace("]]", string.Empty);
                        }
                    }
                    SilkGraph newSilkGraph = new SilkGraph(storyTitle);
                    SilkNode newNode = new SilkNode();
                    AssignDataToNodes(newSilkGraph, newNode, tweeNodesToInterpret[i], promptContainer.ToString(), fileName);

                }
                graphBuilder.AddGraphToMother(fileName, graphBuilder.graph);
                
            }
            //Break This Out into its own method
            foreach(KeyValuePair<string, Dictionary<string, SilkNode>> silkStory in graphBuilder.motherGraph)
            {
                filenames.Add(silkStory.Key);
            }
            //have to search the mother to do it to ALL the graphs???
            foreach(KeyValuePair<string, Dictionary<string, SilkNode>> story in graphBuilder.motherGraph)
            {
                foreach(KeyValuePair<string, SilkNode> node in story.Value)
                {
                   
                    foreach (KeyValuePair<string, string> link in node.Value.links)
                     {
                        StringBuilder linkNameBuilder = new StringBuilder();
                        string linkName;
                        linkNameBuilder.Append(link.Value);
                        linkName = linkNameBuilder.ToString().TrimStart().TrimEnd();
                        foreach (KeyValuePair<string, SilkNode> linkedNode in story.Value)
                        {
                            string nodeName = "";
                            StringBuilder nodeNameBuilder = new StringBuilder();
                            for (int a = 0; a < filenames.Count; a++)
                            {

                                
                                if (linkedNode.Value.nodeName.Contains(filenames[a]))
                                {
                                    
                                    nodeNameBuilder.Append(linkedNode.Value.nodeName.Remove(0, filenames[a].Length + 1));
                                    nodeName = nodeNameBuilder.ToString().TrimEnd();

                                }
                                
                            }
                                                        
                            if (linkName.ToString() == nodeName)
                            {
                                
                                SilkLink newSilkLink = new SilkLink(node.Value, linkedNode.Value, link.Key);
                                node.Value.silkLinks.Add(newSilkLink);
                                Debug.Log(newSilkLink.LinkText);
                            }

                        }


                    }
                }
            }

            foreach(KeyValuePair<string, Dictionary<string, SilkNode>> graph in graphBuilder.motherGraph)
            {
                //for testing
                foreach(KeyValuePair<string, SilkNode> node in graph.Value)
                {
                    //for testing


                }
                

            }
            
        }


        //taking newNode out of here--lets see if it works
        void AssignDataToNodes(SilkGraph newSilkGraph, SilkNode newNode, string newTweeData, string newPassage, string graphTitle)
        {
            //SilkGraph newSilkGraph = new SilkGraph();
            newNode.nodeName = graphTitle + "_" + ReturnTitle(newTweeData).TrimEnd(' ');
            newNode.tags = ReturnCustomTags(newTweeData);
            //add passage
            newNode.nodePassage = newPassage;
            //add link names
            newNode.links = ReturnLinks(newTweeData);
            //Debug.Log(newNode.nodePassage);
            newSilkGraph.AddToGraph(newNode.nodeName, newNode);
            //maybe remove this??
            graphBuilder.AddToGraph(newNode.nodeName, newNode);
            

        }
        /*
        void AssignLinksToNodes(SilkNode newNode, List<SilkLink> silkLinkList, string newTweeData)
        {
           
        }
        */
        void AssignLinksToNodes(SilkMotherGraph mother)
        {
            foreach (KeyValuePair<string, SilkGraph> story in mother.MotherGraph)
            {
                foreach (KeyValuePair<string, SilkNode> node in story.Value.Story)
                {
                    foreach(KeyValuePair<string, string> linkName in node.Value.links)
                    {

                    }
                }
            }
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

        string ReturnStoryTitle(string nodeToInterpret)
        {
            
            string storyTitle = "";
            if (nodeToInterpret.Contains(ReturnTitle(nodeToInterpret))){
                storyTitle = nodeToInterpret.Replace(ReturnTitle(nodeToInterpret), string.Empty).TrimEnd().TrimStart();
            }
            //Debug.Log("title is >" + storyTitle + "<");
            return storyTitle;
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

        /*
        Dictionary<string, string> ReturnLinks(string inputToExtractLinksFrom)
         {
            List<SilkLink> newSilkLinks = new List<SilkLink>();
            Dictionary<string, string> newLinks = new Dictionary<string, string>();
            for (int i = 0; i < inputToExtractLinksFrom.Length; i++){
                if(inputToExtractLinksFrom[i] == '[' && inputToExtractLinksFrom[i + 1] == '[')
                {
                    
                    string newLink = "";
                    int linkLength;
                    //I might want to reevaluate how I deal with link text that is repeated.
                    //for now this should work
                    int linkCount = 0;
             
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
                                    linkCount += 1;
                                    break;
                                }
                                else
                                {
                                    newLinkValue += inputToExtractLinksFrom[k];
                                    if (inputToExtractLinksFrom[j] == ']')
                                    {

                                        newLinks.Add(newLink, newLink);
                                        //SilkLink newSilkLink = new SilkLink()
                                        linkCount += 1;
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
                                linkCount += 1;
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
        */


        
        Dictionary<string, string> ReturnLinks(string inputToExtractLinksFrom)
        {
            List<SilkLink> newSilkLinks = new List<SilkLink>();
            Dictionary<string, string> newLinks = new Dictionary<string, string>();
            for (int i = 0; i < inputToExtractLinksFrom.Length; i++)
            {
                if (inputToExtractLinksFrom[i] == '[' && inputToExtractLinksFrom[i + 1] == '[')
                {

                    string newLink = "";
                    int linkLength;
                    //I might want to reevaluate how I deal with link text that is repeated.
                    //for now this should work
                    int linkCount = 0;

                    for (int j = i + 2; j < inputToExtractLinksFrom.Length; j++)
                    {
                        if (inputToExtractLinksFrom[j] == '|')
                        {
                            string newLinkValue = "";
                            for (int k = j + 1; k < inputToExtractLinksFrom.Length; k++)
                            {
                                if (inputToExtractLinksFrom[k] == ']')
                                {

                                    newLinks.Add(newLink, newLinkValue);
                                    linkCount += 1;
                                    break;
                                }
                                else
                                {
                                    newLinkValue += inputToExtractLinksFrom[k];
                                    if (inputToExtractLinksFrom[j] == ']')
                                    {

                                        newLinks.Add(newLink, newLink);
                                        //SilkLink newSilkLink = new SilkLink()
                                        linkCount += 1;
                                        break;
                                    }
                                }
                            }
                        }
                        if (inputToExtractLinksFrom[j] == ']')
                        {
                            if (!newLink.Contains("|"))
                            {

                                newLinks.Add(newLink, newLink);
                                linkCount += 1;
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

        //overload for backref arg
        /*
        List<SilkLink> ReturnLinks(string inputToExtractLinksFrom, SilkNode backRef)
        {
            List<SilkLink> newSilkLinks = new List<SilkLink>();
            Dictionary<string, string> newLinks = new Dictionary<string, string>();
            for (int i = 0; i < inputToExtractLinksFrom.Length; i++)
            {
                if (inputToExtractLinksFrom[i] == '[' && inputToExtractLinksFrom[i + 1] == '[')
                {

                    string newLink = "";
                    int linkLength;
                    //I might want to reevaluate how I deal with link text that is repeated.
                    //for now this should work
                    int linkCount = 0;

                    for (int j = i + 2; j < inputToExtractLinksFrom.Length; j++)
                    {
                        if (inputToExtractLinksFrom[j] == '|')
                        {
                            string newLinkValue = "";
                            for (int k = j + 1; k < inputToExtractLinksFrom.Length; k++)
                            {
                                if (inputToExtractLinksFrom[k] == ']')
                                {
                                    SilkLink newSilkLink = new SilkLink(backRef, newLink, newLinkValue);
                                    newSilkLinks.Add(newSilkLink);
                                    newLinks.Add(newLink, newLinkValue);
                                    break;
                                }
                                else
                                {
                                    newLinkValue += inputToExtractLinksFrom[k];
                                    if (inputToExtractLinksFrom[j] == ']')
                                    {

                                        newLinks.Add(newLink, newLink);
                                        SilkLink newSilkLink = new SilkLink(backRef, newLink, newLink);
                                        newSilkLinks.Add(newSilkLink);
                                        linkCount += 1;
                                        break;
                                    }
                                }
                            }
                        }
                        if (inputToExtractLinksFrom[j] == ']')
                        {
                            if (!newLink.Contains("|"))
                            {
                                SilkLink newSilkLink = new SilkLink(backRef, newLink, newLink);
                                newSilkLinks.Add(newSilkLink);
                                newLinks.Add(newLink, newLink);
                                linkCount += 1;
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


            return newSilkLinks;
        }
        */

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
