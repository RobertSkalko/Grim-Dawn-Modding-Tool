using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GrimDawnModdingTool
{
    public class InputCommandSaving : MonoBehaviour
    {
        public string Saved => Save.Instance.InputCommand;

        public bool Updated = false;

        public void Update()
        {
            if (Saved != null && Saved.Length > 0 && !Updated) {
                this.gameObject.GetComponentInChildren<Text>().text = Saved;
                Updated = true;
            }
        }
    }
}