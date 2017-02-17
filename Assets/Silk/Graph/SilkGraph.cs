﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Silk
{
    public class SilkGraph
    {
        #region Constructor
        //need to find where I get the graph's name
        
        public SilkGraph(string name)
        {
            storyName = name;
        }
        
        #endregion

        #region Data
        private string storyName;
        private Dictionary<string, SilkNode> story = new Dictionary<string, SilkNode>();
        public Dictionary<string, SilkNode> Story
        {
            get
            {
                return story;
            }
        }
        #endregion

        #region Methods for Manipulating Data
        public void AddToGraph(string nodeName, SilkNode node)
        {
            story.Add(nodeName, node);
        }

        public int NumberOfNodes()
        {
            return story.Count;
        }
        #endregion

        #region Accessor Methods
        public SilkNode GetNodeByName(string nodeName)
        {
            return story[nodeName];
        }

        public SilkNode GetNodeByLink(SilkLink link)
        {
            return link.LinkedNode;
        }

        public string StoryName
        {
            get
            {
                return storyName;
            }
        }
        /*public string GetStoryName()
        {
            
        }
        */
        /*
        public SilkNode GetNodeByLink(string linkText)
        {
            
        }
        */

        //this needs some work
        /*
        public SilkNode GetNodeByLink(SilkLink link)
        {
            foreach(KeyValuePair<string, SilkNode> node in story)
            {
                if(node.Value.nodeName == link.LinkedNode)
                {
                    return node.Value;
                }
                
            }
            return null;
        }
        */

        public string GetNodePassage(string nodeName)
        {
            return story[nodeName].nodePassage;
        }

        public string[] GetLinkText(string nodeName)
        {
            SilkNode curNode = story[nodeName];
            string[] linkKeys = curNode.links.Keys.ToArray();
            return linkKeys;
        }

        public string GetNodeName(string nodeName)
        {
            return story[nodeName].nodeName;
        }

        #endregion


    }
}
