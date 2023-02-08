using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This Component is used only in one off scene, to load in persistance objects.
public class InstantLoadToDifferentScene : MonoBehaviour
{
    public bool _goInstant = true;
    public string _sceneName;
    public FadeinOutController _fadeControler;
    // Start is called before the first frame update
    void Start()
    {
        if (_goInstant) LoadSceneInstant(_sceneName);
    }

    public void LoadSceneInstant(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneFade(string sceneName)
    {
        StartCoroutine(LoadSceneSequence(sceneName));
    }

    public IEnumerator LoadSceneSequence(string sceneName)
    {
        if (_fadeControler != null)
        {
            _fadeControler.FadeOut();
            yield return new WaitForSeconds(1 / _fadeControler.Speed);
        }
        SceneManager.LoadScene(sceneName);
    }
}
