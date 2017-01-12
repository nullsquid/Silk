using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Silk;
namespace Silk
{
    public class GraphBuilder
    {

        //Silk.Graph graph = new Silk.Graph();
        //LinkedList<Node> graph = new LinkedList<Node>();
        public Dictionary<string, SilkNode> graph = new Dictionary<string, SilkNode>();
        public Dictionary<string, Dictionary<string, SilkNode>> motherGraph;
        public void AddToGraph(string newKey, SilkNode newNode)
        {
            graph.Add(newKey, newNode);
        }
        
        public void AddGraphToMother(Dictionary<string, SilkNode> graph)
        {

        }




        
    }
}
