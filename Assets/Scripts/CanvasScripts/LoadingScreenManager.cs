using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenManager: MonoBehaviour
{

    public static LoadingScreenManager loadingScreenInstance;
    private float progress;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Slider loadingBar;

    void Awake()
    {
        if (loadingScreenInstance == null)
        {
            loadingScreenInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadNextScene(string sceneName)
    {
        progress = 0;
        loadingBar.value = 0;

        var nextScene = SceneManager.LoadSceneAsync(sceneName);
        nextScene.allowSceneActivation = false;
        loadingPanel.SetActive(true);

        do
        {
            await System.Threading.Tasks.Task.Delay(100);
            progress = nextScene.progress;
        } while (nextScene.progress < 0.9f);

        await System.Threading.Tasks.Task.Delay(1000); //jangan lupa hapus

        nextScene.allowSceneActivation = true;
        loadingPanel.SetActive(false);
    }
    void Update()
    {
        loadingBar.value = Mathf.MoveTowards(loadingBar.value, progress, 3 * Time.deltaTime);
    }

}
