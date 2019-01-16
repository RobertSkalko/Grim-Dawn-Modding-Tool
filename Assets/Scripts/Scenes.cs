using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GrimDawnModdingTool
{
    public class Scenes : MonoBehaviour
    {
        public GameObject ScenePrefab;

        public static List<string> GetAll()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            var scenes = new List<string>();
            for (int i = 0; i < sceneCount; i++) {
                scenes.Add(Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
            }
            return scenes;
        }

        private void Start()
        {
            foreach (var scene in GetAll()) {
                if (!scene.Contains(Path.GetFileNameWithoutExtension(SceneManager.GetActiveScene().name))) { // if not current scene lol
                    var obj = Instantiate(ScenePrefab, transform);
                    obj.GetComponent<SceneObject>().Init(scene);
                }
            }
        }
    }
}