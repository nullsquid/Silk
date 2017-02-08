using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Silk
{
    public class SilkLink
    {
        
        private SilkNode _backRefNode;
        private string _linkedNodeTitle;
        private string _linkText;

        public SilkNode BackRefNode
        {
            get
            {
                return _backRefNode;
            }

        }

        public string LinkedNode
        {
            get
            {
                return _linkedNodeTitle;
            }

        }

        public string LinkText
        {
            get
            {
                return _linkText;
            }

        }

        public SilkLink(SilkNode thisNode, string linkedNodeTitle, string text)
        {
            _backRefNode = thisNode;
            _linkedNodeTitle = linkedNodeTitle;
            _linkText = text;
        }
    }
}
