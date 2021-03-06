﻿using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Silk;
namespace Silk {
    public class Silky : MonoBehaviour {

        #region Public Variables
        public SilkMotherStory mother;
        #endregion

        #region Private Variables
        TagFactory tagFactory;
        Importer importer;
        string textToParse;
        string[] tweeNodesToInterpret;
        string[] delim = new string[] { ":: " };
        #endregion

        #region Testing Methods
        void LogNodes() {
            foreach (KeyValuePair<string, SilkStory> story in mother.MotherStory) {
                foreach (KeyValuePair<string, SilkNode> node in story.Value.Story) {
                    Debug.Log("NODE >>" + node.Value.GetNodeName());
                }
            }
        }
        #endregion

        #region Singleton
        public static Silky Instance { get; private set; }
        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        #endregion

        //TODO sort out all of this nonsense, break into other methods, etc
        #region Initialization
        void ImportText() {
            //All of the "Getting Text Files to parse" code
        }
        void InitializeTagFactory() {

        }
        void InitializeSilk() {
            tagFactory = new TagFactory();
            importer = GetComponent<Silk.Importer>();
            List<string> filenames = new List<string>();
            mother = new SilkMotherStory();

            foreach (TextAsset currentTweeFile in importer.rawTweeFiles) {
                SilkStory newSilkStory = new SilkStory();
                TextAsset tweeFile = currentTweeFile;
                string fileName = currentTweeFile.name;
                //this works for single file
                //textToParse = testText.text;

                textToParse = tweeFile.text;
                tweeNodesToInterpret = textToParse.Split(delim, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tweeNodesToInterpret.Length; i++) {
                    string storyTitle = "";
                    StringBuilder promptContainer = new StringBuilder(tweeNodesToInterpret[i]);

                    if (tweeNodesToInterpret[i].Contains("|")) {
                        promptContainer.Replace("|", string.Empty);
                    }
                    if (tweeNodesToInterpret[i].Contains(ReturnTitle(tweeNodesToInterpret[i]))) {
                        string storyTitleCheck = ReturnTitle(tweeNodesToInterpret[i]).TrimStart().TrimEnd();
                        if (storyTitleCheck == "StoryTitle") {

                            newSilkStory.SetStoryName(ReturnStoryTitle(tweeNodesToInterpret[i]));
                        }
                        else {
                            promptContainer.Replace(ReturnTitle(tweeNodesToInterpret[i]), string.Empty, 0, ReturnTitle(tweeNodesToInterpret[i]).Length);
                        }
                    }
                    foreach (KeyValuePair<string, string> entry in ReturnLinks(tweeNodesToInterpret[i])) {
                        if (tweeNodesToInterpret[i].Contains("[[" + entry.Key) || tweeNodesToInterpret[i].Contains("[[" + entry.Value)) {
                            promptContainer.Replace("[[" + entry.Key, string.Empty).Replace(entry.Value + "]]", string.Empty);
                            promptContainer.Replace("]]", string.Empty);
                        }
                    }
                    SilkNode newNode = new SilkNode();
                    //TODO Switch type from SilkG--- to SilkStory in AssignDataToNodes
                    AssignDataToNodes(newSilkStory, newNode, tweeNodesToInterpret[i], promptContainer.ToString(), fileName);
                    //Debug.Log(newNode.nodeName);
                }
                mother.AddToMother(fileName, newSilkStory);
                foreach (KeyValuePair<string, SilkStory> story in mother.MotherStory) {
                    foreach (KeyValuePair<string, SilkNode> node in story.Value.Story) {
                        //for testing
                        //Debug.Log("ON NODE: " + node.)

                    }
                }



            }
            //TODO Break This Out into its own method
            foreach (KeyValuePair<string, SilkStory> silkStory in mother.MotherStory) {
                filenames.Add(silkStory.Key);
            }
            //


            //have to search the mother to do it to ALL the gr---???
            //TODO in mother or in story, make a method that allows for easier searching
            //TODO Make this its own method
            foreach (KeyValuePair<string, SilkStory> story in mother.MotherStory) {
                foreach (KeyValuePair<string, SilkNode> node in story.Value.Story) {

                    foreach (KeyValuePair<string, string> link in node.Value.links) {
                        StringBuilder linkNameBuilder = new StringBuilder();
                        string linkName;
                        linkNameBuilder.Append(link.Value);
                        linkName = linkNameBuilder.ToString().TrimStart().TrimEnd();
                        foreach (KeyValuePair<string, SilkNode> linkedNode in story.Value.Story) {
                            string nodeName = "";
                            StringBuilder nodeNameBuilder = new StringBuilder();
                            for (int a = 0; a < filenames.Count; a++) {


                                if (linkedNode.Value.nodeName.Contains(filenames[a])) {

                                    nodeNameBuilder.Append(linkedNode.Value.nodeName.Remove(0, filenames[a].Length + 1));
                                    nodeName = nodeNameBuilder.ToString().TrimEnd();

                                }

                            }

                            if (linkName.ToString() == nodeName) {

                                SilkLink newSilkLink = new SilkLink(node.Value, linkedNode.Value, link.Key);
                                node.Value.silkLinks.Add(newSilkLink);
                                Debug.Log("SilkLink " + newSilkLink.LinkText);
                            }

                        }


                    }

                }
            }
            //TODO break this into its own method (TESTING)
            foreach (KeyValuePair<string, SilkStory> story in mother.MotherStory) {
                //for testing
                foreach (KeyValuePair<string, SilkNode> node in story.Value.Story) {
                    //for testing
                    Debug.Log("NODE IS CALLED " + node.Value.nodeName);
                    //Debug.Log(node.Value.silkTags[0]);
                    //Debug.Log(node.Value.silkTags.Count);
                    foreach (KeyValuePair<string, string[]> tagName in node.Value.tags) {
                        //Debug.Log(tagName.Key);

                    }
                    foreach (SilkTagBase _tag in node.Value.silkTags) {
                        Debug.Log(_tag.TagName);

                    }
                    foreach (SilkLink _link in node.Value.silkLinks) {
                        Debug.Log(node.Value.nodeName + " " + " " + _link.LinkText);
                    }
                }
            }
        }

        private void Start() {
            InitializeSilk();
            //Debug.Log("NODE TEST " + LogNodes().GetNodeName());
            LogNodes();
            
        }

        #endregion

        void AssignDataToNodes(SilkStory newSilkStory, SilkNode newNode, string newTweeData, string newPassage, string storyTitle) {
            //TODO figure out where this g----Title is started
            newNode.nodeName = storyTitle + "_" + ReturnTitle(newTweeData).TrimEnd(' ');
            //only to remove it when required in GetNodeName
            newNode.StoryName = storyTitle;
            //add custom tag names
            newNode.tags = ReturnCustomTags(newTweeData);

            //add custom tags
            foreach (KeyValuePair<string, string[]> tagName in newNode.tags) {

                string newTagName = "";
                //Right now, you can't use underscores in your custom tag names
                if (tagName.Key.Contains("_")) {
                    newTagName = tagName.Key.Remove(tagName.Key.IndexOf('_')).TrimStart().TrimEnd();
                }
                if (tagFactory.SetTag(newTagName, tagName.Value) != null) {
                    newNode.silkTags.Add(tagFactory.SetTag(newTagName, tagName.Value));
                }
                else {
                    Debug.LogError(newTagName + " is not a recognized tag. Check your TagFactory");
                }
            }
            //Debug.Log(newNode.silkTags[0].TagName);

            //add passage
            newNode.nodePassage = newPassage;
            //TODO Add the correct amount of links to the list
            //add link names
            newNode.links = ReturnLinks(newTweeData);

            newSilkStory.AddToStory(newNode.nodeName, newNode);
        }

        //finish this method
        void AssignLinksToNodes(SilkMotherStory mother) {
            foreach (KeyValuePair<string, SilkStory> story in mother.MotherStory) {
                foreach (KeyValuePair<string, SilkNode> node in story.Value.Story) {
                    foreach (SilkTagBase tag in node.Value.silkTags) {

                    }
                }
            }
        }
        void AssignPassageToNodes(string newTweeData) {

        }

        string ReturnTitle(string inputToExtractTitleFrom) {
            string title = "";
            for (int i = 0; i < inputToExtractTitleFrom.Length; i++) {
                if (inputToExtractTitleFrom[i] == '\n' || inputToExtractTitleFrom[i] == '[') {
                    break;
                }

                else {
                    title += inputToExtractTitleFrom[i];
                }
            }
            return title.TrimStart(' ').TrimEnd(' ');
        }

        string ReturnStoryTitle(string nodeToInterpret) {
            string storyTitle = "";
            if (nodeToInterpret.Contains(ReturnTitle(nodeToInterpret))) {
                storyTitle = nodeToInterpret.Replace(ReturnTitle(nodeToInterpret), string.Empty).TrimEnd().TrimStart();
            }
            return storyTitle;
        }

        //finish this method
        string ReturnPassageTags(string inputToExtractTagsFrom) {
            string newTag = "";
            return newTag;
        }

        Dictionary<string, string[]> ReturnCustomTags(string inputToExtractTagsFrom) {
            Dictionary<string, string[]> tags = new Dictionary<string, string[]>();
            List<string> rawTags = new List<string>();
            int iterations = 0;
            for (int i = 0; i < inputToExtractTagsFrom.Length; i++) {
                string rawTag = "";
                //to find each custom tag
                if (inputToExtractTagsFrom[i] == '<' && inputToExtractTagsFrom[i + 1] == '<') {
                    //to get data out of each tag
                    for (int j = i + 2; j < inputToExtractTagsFrom.Length; j++) {
                        if (inputToExtractTagsFrom[j] == '>' && inputToExtractTagsFrom[j + 1] == '>') {
                            rawTags.Add(rawTag);
                            break;
                        }
                        else {
                            rawTag += inputToExtractTagsFrom[j];
                        }
                    }
                }
            }
            foreach (string tag in rawTags) {
                string tagName = "";
                string[] tagArgs = null;

                for (int i = 0; i < tag.Length; i++) {

                    if (tag[i] == '=') {
                        tagArgs = tag.Substring(i + 1).Split(',');
                        break;
                    }
                    else {
                        tagName += tag[i];

                    }
                }
                if (tagArgs != null) {

                    tags.Add(tagName + "_" + iterations, tagArgs);

                }
                else {
                    tags.Add(tagName, null);
                }
                iterations += 1;
            }
            foreach (KeyValuePair<string, string[]> tag in tags) {
                //Debug.Log(tag.Key);
            }
            return tags;

        }

        //TODO clean this code up and remove unused variables
        //TODO replace all of the inputToExtractLinksFrom variables with inputCopy
        //TODO remove the link substring from inputCopy once it's been added to the list
        Dictionary<string, string> ReturnLinks(string inputToExtractLinksFrom) {
            StringBuilder inputCopy = new StringBuilder();
            inputCopy.Append(inputToExtractLinksFrom);
            List<SilkLink> newSilkLinks = new List<SilkLink>();
            Dictionary<string, string> newLinks = new Dictionary<string, string>();
            for (int i = 0; i < inputCopy.Length; i++) {
                if (inputCopy[i] == '[' && inputCopy[i + 1] == '[') {

                    string newLink = "";
                    //I might want to reevaluate how I deal with link text that is repeated.
                    //for now this should work
                    for (int j = i + 2; j < inputCopy.Length; j++) {
                        //bug might be in here
                        //make sure that it breaks if there is no |
                        if (inputCopy[j] == '|') {
                            string newLinkValue = "";
                            for (int k = j + 1; k < inputCopy.Length; k++) {
                                if (inputCopy[k] == ']' && inputCopy[k + 1] == ']') {

                                    newLinks.Add(newLink, newLinkValue);
                                    if (newLinkValue.Length > 0) {
                                        inputCopy.Replace(newLinkValue, "");
                                    }
                                    //Debug.Log("NEW LINK IS " + newLink);

                                    //Debug.Log(inputCopy);
                                    break;
                                }
                                else {
                                    newLinkValue += inputCopy[k];
                                    if (inputCopy[j] == ']' && inputCopy[j + 1] == ']') {
                                        //TODO test if this works
                                        inputCopy.Replace(newLink, "");
                                        newLinks.Add(newLink, newLink);
                                        break;
                                    }
                                }
                            }
                        }
                        if (inputCopy[j] == ']' && inputCopy[j + 1] == ']') {
                            //newLinks.Add(newLink, newLink);
                            inputCopy.Replace(newLink, "");
                            //break;
                            //Debug.Log("NEW LINK IS " + newLink);
                            if (!newLink.Contains("|")) {

                                newLinks.Add(newLink, newLink);
                                break;
                            }
                            break;

                        }
                        else {
                            newLink += inputCopy[j];


                        }
                    }
                }

            }

            //Debug.Log(newLinks.ContainsValue("2"));
            int linkNum = 0;
            foreach (KeyValuePair<string, string> link in newLinks) {
                linkNum++;
            }
            //Debug.Log(linkNum + " is the number of links");
            return newLinks;
        }


        //garbage fire
        //TODO bring Passage Setting code from start method down here
        string ReturnPassage(string inputToExtractPassageFrom) {
            Debug.Log("full text is " + inputToExtractPassageFrom);
            string passage = "";

            for (int i = 0; i < inputToExtractPassageFrom.Length; i++) {
                if (inputToExtractPassageFrom[i] == ':') {
                    for (int j = i; j < inputToExtractPassageFrom.Length; j++) {

                    }
                }
                else if (inputToExtractPassageFrom[i] == '[' || inputToExtractPassageFrom[i] == '<') {

                }
                else {
                    passage += inputToExtractPassageFrom[i];
                }
            }
            Debug.Log("passage is " + passage);
            return passage;
        }
    }
}
