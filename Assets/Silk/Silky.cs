using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Silk;
namespace Silk
{
    public class Silky
    {
        GraphBuilder builder;
        //public Dictionary<string, >
        //I might want to make the SilkGraph class do something first
        //for now I'll just have it return a dictionary I guess.... :/

        /*
        public SilkGraph LoadStory(string storyName)
        {
            SilkGraph story;
            builder = new GraphBuilder();
            //story = builder.motherGraph[storyName];
            return null;
        }*/

        public Dictionary<string, SilkNode> LoadStory(string storyName)
        {
            Dictionary<string, SilkNode> story;
            story = builder.motherGraph[storyName];
            return story;
        }



        public SilkNode GetNodeByName(string nodeName)
        {
            return null;
        }

        public SilkNode GetNodeByLink(string linkText)
        {
            return null;
        }

        public string GetNodePassage(SilkNode node)
        {
            return node.nodePassage;
        }

        public string GetNodeName(SilkNode node)
        {
            return node.nodeName;
        }



        

        



    }
}
