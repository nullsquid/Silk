using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace Silk
{
    public class SilkNode
    {


        public string nodeName;
        public string nodePassage;
        public Dictionary<string, string> links = new Dictionary<string, string>();
        public Dictionary<string, string[]> tags = new Dictionary<string, string[]>();
        public List<SilkLink> silkLinks = new List<SilkLink>();
        public List<SilkTagBase> silkTags = new List<SilkTagBase>();



    }
}
