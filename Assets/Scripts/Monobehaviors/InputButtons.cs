using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GrimDawnModdingTool
{
    public class InputButtons : MonoBehaviour
    {
        public void SetInputCommand(string s)
        {
            Save.Instance.InputCommand = s;
        }
    }
}