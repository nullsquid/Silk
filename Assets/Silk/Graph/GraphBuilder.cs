using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Silk;
namespace Silk
{
    public class GraphBuilder
    {
        //Silk.Graph graph = new Silk.Graph();
        LinkedList<Node> graph = new LinkedList<Node>();
        Silk.Node node;
        public void AddToGraph(Node newNode)
        {
            if (graph != null)
            {
                graph.AddLast(newNode);
            }
        }

        public void BuildNode(string nodeKey, string nodePrompt, List<string> nodeResponses)
        {
            node = new Silk.Node();
            node.Key = nodeKey;
            node.Prompt = nodePrompt;
            node.Responses = nodeResponses;
            AddToGraph(node);
            Debug.Log(node);


        }

        public void BuildNode(string nodeKey, string nodePrompt, List<string> nodeResponses, List<string> nodeCustomTags)
        {

        }
    }
}
