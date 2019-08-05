using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class Saving : MonoBehaviour
    {
        private void Start()
        {
            Save.TryLoadStateFromFile();
            Save.Instance.CreateFoldersIfEmpty();
        }

        private void OnApplicationQuit()
        {
            Save.SaveStateToFile();
        }
    }
}