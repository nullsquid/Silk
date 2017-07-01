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

			switch (tagName) {
			case "name":
				NameTag newName = new NameTag (args);
				return newName;
			case "speaker":
				SpeakerTag newSpeaker = new SpeakerTag (args);
				return newSpeaker;
			case "nexttree":
				NextTreeTag newTreeTag = new NextTreeTag (args);
				return newTreeTag;
			case "state":
				StateTag newState = new StateTag (args);
				return newState;
			case "unload":
				UnloadTag newUnload = new UnloadTag ();
				return newUnload;
			default:
				DummyTag newDummy = new DummyTag (tagName, args);
				return newDummy;

			}




			/*
			if (tagName == "DummyTag") {
				DummyTag newDummyTag = new DummyTag (tagName, args);
				return newDummyTag;
			} else if (tagName == "name") {
				if (args.Count == 1) {
					NameTag newName = new NameTag (args [0]);
					return newName;
				} else {
					Debug.LogWarning ("name tag has too many arguments");
					NameTag newName = new NameTag (args [0]);
					return newName;
				}
			} else if (tagName == "speaker") {

			}
            else {
				Debug.LogWarning ("Tag not found");
				DummyTag newDummyTag = new DummyTag (tagName, args);
				return newDummyTag;
            }
			*/
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

