using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Silk;
namespace Silk
{
    public class NodeBuilder
    {
        Node node;
        public void BuildNode(string nodeKey, List<string> nodeResponses)
        {
            node = new Node();
            node.Key = nodeKey;
            node.Responses = nodeResponses;
        }

        public void BuildNode(string nodeKey, List<string> nodeResponses, List<string> nodeCustomTags)
        {

        }
    }
}
