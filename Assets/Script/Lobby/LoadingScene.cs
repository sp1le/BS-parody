using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TMP_Text progressText;
    private void Start()
    {
        StartCoroutine(LoadSceneAsync("NewGameLobby"));
    }
    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            if (progressText != null)
            {
                progressText.text = Mathf.RoundToInt(progress * 100f) + "%";
              
            }
            yield return null;
        }
    }
}
