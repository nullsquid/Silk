using UnityEngine;
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

        //public loadStory
    }
}
