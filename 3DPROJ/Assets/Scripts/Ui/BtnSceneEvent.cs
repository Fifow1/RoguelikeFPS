using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSceneEvent : MonoBehaviour
{
    SceneChange sceneManager;
    public string sceneName;
    Button btn;
    private void Start()
    {
        btn = GetComponent<Button>();
        sceneManager = FindAnyObjectByType<SceneChange>();
        if (sceneManager != null)
        {
            btn.onClick.AddListener(()=>sceneManager.LoadScene(sceneName));
        }
    }
}
