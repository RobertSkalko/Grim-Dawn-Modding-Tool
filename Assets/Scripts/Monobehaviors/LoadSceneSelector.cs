using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GrimDawnModdingTool
{
    public class LoadSceneSelector : MonoBehaviour
    {
        private void Start()
        {
            Button butt = this.gameObject.GetComponent<Button>();

            butt.onClick.AddListener(() => {
                UnityEngine.SceneManagement.SceneManager.LoadScene("SceneSelector");
            });
        }
    }
}