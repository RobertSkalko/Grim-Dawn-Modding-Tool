using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GrimDawnModdingTool
{
    public class Quit : MonoBehaviour
    {
        private void Start()
        {
            Button butt = this.gameObject.GetComponent<Button>();

            butt.onClick.AddListener(() => {
                Application.Quit();
            });
        }
    }
}