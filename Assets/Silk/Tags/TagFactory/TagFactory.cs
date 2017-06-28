using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silk{
    public class TagFactory
    {
        private string _tag;
        public string Tag
        {
            get
            {
                return _tag;
            }
            private set
            {
                _tag = value;
            }
        }
        string SetRawTag(string tagName, string[] args) {
            string rawTag = "<<" + tagName + '=';
            for (int i = 0; i < args.Length; i++) {
                rawTag += "\"" + args[i] + "\"";
            }
            rawTag += ">>";
            return rawTag;
        }
        public SilkTagBase SetTag(string tagName, string[] args)
        {
            Tag = SetRawTag(tagName, args);
            
            if(tagName == "DummyTag")
            {
                if (tagName.Contains("_"))
                {
                    Debug.LogWarning("Tag cannot contain underscores");
                }

                DummyTag newDummyTag = new DummyTag(tagName, args);
                return newDummyTag;

            }
            if(tagName == "DummyName") {
                //TODO make this actually replace the text that is coming into it
                DummyName newDummyName = new DummyName(args);
                return newDummyName;
            }

            
            //else if(tagName == "..."){
            //
            //note::tag names at present cannot include underscores
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

