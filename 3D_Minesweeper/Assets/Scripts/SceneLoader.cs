using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;

    public void LoadLevel(string sceneName)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        Debug.Log("LOAD");
        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
