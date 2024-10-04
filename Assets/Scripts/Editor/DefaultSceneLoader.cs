#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

//[InitializeOnLoad]
//public class DefaultSceneLoader : MonoBehaviour
//{
//    static DefaultSceneLoader()
//    {
//        var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
//        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
//        EditorSceneManager.playModeStartScene = sceneAsset;
//        Debug.Log(pathOfFirstScene + " was set as default play mode scene");
//    }
//}
#endif