using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class InputButtons : MonoBehaviour
    {
        public void SetGamePathName(string s)
        {
            Save.Instance.GamePath = s;
        }

        public void SetFilesToEditPathName(string s)
        {
            Save.Instance.FilesToEditPath = s;
        }

        public void SetDataPath(string s)
        {
            Save.Instance.DataPath = s;
        }

        public void SetOutputPath(string s)
        {
            Save.Instance.OutputPath = s;
        }

        public void SetInputCommand(string s)
        {
            Save.Instance.InputCommand = s;
        }
    }
}