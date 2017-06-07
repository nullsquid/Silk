using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Silk.Editor {
    public class SilkWindow : MonoBehaviour {
        [MenuItem("Silk/Create New Silk")]
        
        private static void CreateNewSilk()
        {
            if (!GameObject.Find("Silk")) {
                GameObject newSilkInstance;
                newSilkInstance = new GameObject("Silk");
                newSilkInstance.AddComponent<Parser>();
                newSilkInstance.AddComponent<Importer>();
            }
            else {
                Debug.LogError("There is already one Silk in the scene");
            }
        }
    }
}
