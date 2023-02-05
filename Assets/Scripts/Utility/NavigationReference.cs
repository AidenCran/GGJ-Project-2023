using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationReference : MonoBehaviour
{
    Navigation _navigation;

    void Start()
    {
        try { _navigation = Navigation.instance; }
        catch { }
    }

    public void OpenURL(string URL)
    {
        Application.OpenURL(URL);
    }

    public void ChangeScene(string str)
    {
        _canvasGroup.alpha = 1;
        StartCoroutine(LoadSceneAsync(sceneName));
    }


    public void Quit() => _navigation.Quit();
}
