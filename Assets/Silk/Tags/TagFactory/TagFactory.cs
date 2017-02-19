using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silk{
    public class TagFactory
    {
        public TagBase SetTag(string tagName, string[] args)
        {
            if(tagName == "DummyTag")
            {
                DummyTag newDummyTag = new DummyTag(tagName, args);
                return newDummyTag;

            }

            //else if(tagName == "..."){
            //
            //
            //}

            else
            {
                return null;
            }
        } 
    }
}

