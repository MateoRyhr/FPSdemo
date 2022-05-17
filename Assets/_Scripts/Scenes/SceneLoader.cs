using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _progressBar;
    public UnityEvent SceneLoaded;

    public void LoadScene(string sceneName){
        StartCoroutine("Loading",sceneName);
    }

    public IEnumerator Loading(string sceneName){
        _loadingScreen.gameObject.SetActive(true);
        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while(!scene.isDone){
            _progressBar.value = scene.progress*110;
            yield return null;
        }
        if(sceneName != "EmptyScene"){
            SceneLoaded?.Invoke();
        }
        _loadingScreen.gameObject.SetActive(false);
    }

    public void ExitScene(float delay){
        SceneManager.LoadScene("EmptyScene");
    }
}
