﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Silk;
namespace Silk
{
    public class Silky
    {
        GraphBuilder builder;
        //story => graph
        Dictionary<string, SilkNode> story;

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
            story = builder.motherGraph[storyName];
            return story;
        }



        public SilkNode GetNodeByName(string nodeName)
        {
            SilkNode node;
            node = story[nodeName];
            return node;
        }

        //this is gonna be bad for now
        public SilkNode GetNodeByLink(SilkNode curNode, string linkText)
        {
            SilkNode node;
            string link = curNode.links[linkText];
            node = story[link];
            return node;
        }

        public string GetNodePassage(SilkNode curNode)
        {
            return curNode.nodePassage;
        }

        public string GetNodeName(SilkNode curNode)
        {
            return curNode.nodeName;
        }



    }
}
