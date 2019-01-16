using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneObject : MonoBehaviour
{
    private string Name;

    public Text NameObj;

    public void Init(string name)
    {
        Name = name;
        NameObj.text = name;

        Button butt = this.gameObject.AddComponent<Button>();

        butt.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Name);
        });
    }
}