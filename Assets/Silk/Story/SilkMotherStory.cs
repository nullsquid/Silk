﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Silk
{
    public class SilkMotherStory
    {

        #region Data
        private Dictionary<string, SilkStory> motherStory = new Dictionary<string, SilkStory>();

        public Dictionary<string, SilkStory> MotherStory
        {
            get
            {
                return motherStory;
            }
        }
        #endregion

        #region Data Manipulation Methods
        public void AddToMother(string storyName, SilkStory story)
        {
            motherStory.Add(storyName, story);
        }
        #endregion

        #region Accessor Methods
        public SilkNode GetNodeByName(string storyName, string nodeName) {
            foreach(KeyValuePair<string, SilkStory> story in MotherStory) {
                if (story.Key == storyName) {
                    foreach (KeyValuePair<string, SilkNode> node in story.Value.Story) {
                        if (node.Value.GetNodeName() == nodeName) {
                            return node.Value;
                        }
                    }
                }
            }
            return null;
        }
        #endregion
        //public loadStory
    }
}
