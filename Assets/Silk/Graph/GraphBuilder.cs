﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Silk;
namespace Silk
{
    public class GraphBuilder
    {

        
        public Dictionary<string, SilkNode> graph = new Dictionary<string, SilkNode>();
        public Dictionary<string, Dictionary<string, SilkNode>> motherGraph = new Dictionary<string, Dictionary<string, SilkNode>>();
        public void AddToGraph(string newKey, SilkNode newNode)
        {
            graph.Add(newKey, newNode);
        }
        
        public void AddGraphToMother(string name, Dictionary<string, SilkNode> graph)
        {
            motherGraph.Add(name, graph);
        }




        
    }
}
