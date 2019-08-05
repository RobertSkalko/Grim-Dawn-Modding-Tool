using System.Collections;
using System.Collections.Generic;
using GrimDawnModdingTool;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class OpenFolder : MonoBehaviour
    {
        public void OpenInExplorer()
        {
            System.Diagnostics.Process.Start(Save.Instance.GetFolder());
        }
    }
}