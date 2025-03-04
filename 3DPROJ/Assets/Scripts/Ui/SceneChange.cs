using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    static SceneChange instance;
    public static SceneChange Instance
    {
        get;
        private set;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void TitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void GameStageScene()
    {
        SceneManager.LoadScene("STAGE");
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene($"{sceneName}");
    }
}
