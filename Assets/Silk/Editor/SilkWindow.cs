using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Silk.Editor {
    public class SilkWindow : MonoBehaviour {
        public static string silkName = "Silk_0.5.0";

        [MenuItem("Silk/Create New Silk")]
        private static void CreateNewSilk()
        {
            
            if (!GameObject.Find("Silk")) {
                GameObject newSilkInstance;
                
                newSilkInstance = new GameObject(silkName);
                newSilkInstance.AddComponent<Silky>();
                newSilkInstance.AddComponent<Importer>();
            }
            else {
                Debug.LogError("There is already one Silk in the scene");
            }
        }
        [MenuItem("Silk/Remove Silk")]
        private static void DestroySilk() {
            if (GameObject.Find(silkName)) {
                DestroyImmediate(GameObject.Find(silkName));
            }
        }
    }
}
