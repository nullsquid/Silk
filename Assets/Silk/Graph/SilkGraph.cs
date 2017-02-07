using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Silk
{
    public class SilkGraph
    {
        #region Data
        private Dictionary<string, SilkNode> story = new Dictionary<string, SilkNode>();
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
        /*
        public SilkNode GetNodeByLink(string linkText)
        {
            
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
