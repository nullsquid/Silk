using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silk{
    public class TagFactory
    {

        string SetRawTag(string tagName, string[] args) {
            string rawTag = "<<" + tagName + '=';
            for (int i = 0; i < args.Length; i++) {
                rawTag += "\"" + args[i] + "\"";
            }
            rawTag += ">>";
            return rawTag;
        }
        public SilkTagBase SetTag(string tagName, List<string> args)
        {
            if(tagName == "DummyTag") {
                return null;
            }
            else {
                return null;
            }
            //TODO sort out what each tag needs to do upon creation
            /*
            if(tagName == "DummyTag")
            {
                if (tagName.Contains("_"))
                {
                    Debug.LogWarning("Tag cannot contain underscores");
                }

                DummyTag newDummyTag = new DummyTag(tagName, args);
				newDummyTag.RawTag = SetRawTag (tagName, args);
                return newDummyTag;

            }
            if(tagName == "DummyName") {
                //TODO make this actually replace the text that is coming into it
                DummyName newDummyName = new DummyName(args, 0);
				newDummyName.RawTag = SetRawTag (tagName, args);
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
            */
        } 
    }
}

