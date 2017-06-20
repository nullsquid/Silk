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
                newSilkInstance = new GameObject("Silk_4.0b");
                newSilkInstance.AddComponent<Silky>();
                newSilkInstance.AddComponent<Importer>();
            }
            else {
                Debug.LogError("There is already one Silk in the scene");
            }
        }
        [MenuItem("Silk/Remove Silk")]
        private static void DestroySilk() {
            if (GameObject.Find("Silk")) {
                DestroyImmediate(GameObject.Find("Silk"));
            }
        }
    }
}
