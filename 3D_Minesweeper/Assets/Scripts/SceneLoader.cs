using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;

    public void LoadLevel(string sceneName)
    {
      /*  Debug.Log(sceneName);
        if (sceneName != null && sceneName == "")
        {*/
            StartCoroutine(LoadAsynchronously(sceneName));
      /*  }
        else
        {
            Debug.LogError("Scene name must be assigned!!!");
        }*/
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            // loadingScreen.SetActive(true);
            yield return null;
        }
    }
}
