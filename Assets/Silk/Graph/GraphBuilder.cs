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
        Dictionary<string, SilkNode> graph = new Dictionary<string, SilkNode>();
        public void AddToGraph(string newKey, SilkNode newNode)
        {
            graph.Add(newKey, newNode);
        }
        public void AddToGraph(Node newNode)
        {
            if (graph != null)
            {
                //graph.AddLast(newNode);
            }
        }

        
    }
}
