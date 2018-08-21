using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour {

    public GameObject PauseMenu;
    public GameObject UI;
    public GameObject FinishMenu;

    private void OnEnable()
    {
        GameManager.instance.OnPauseGame += OnPause;
        GameManager.instance.OnResumeGame += OnResume;
        GameManager.instance.OnStartGame += OnStart;
        GameManager.instance.OnFinishGame += OnFinish;
    }


    private void OnStart()
    {
        ShowIGMenu();
    }

    private void OnPause()
    {
        HideIGMenu();
        ShowPauseMenu();
    }

    private void OnResume()
    {
        HidePauseMenu();
        ShowIGMenu();
    }

    private void OnFinish()
    {
        HideIGMenu();
        HidePauseMenu();
        ShowFinishMenu();
    }

    void ShowIGMenu()
    {
        UI.SetActive(true);
    }

    void HideIGMenu()
    {
        UI.SetActive(false);
    }

    void ShowPauseMenu()
    {
        PauseMenu.SetActive(true);
    }

    void ShowFinishMenu()
    {
        FinishMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        PauseMenu.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.instance.OnResumeGame -= OnResume;
        GameManager.instance.OnPauseGame -= OnPause;
        GameManager.instance.OnFinishGame -= OnFinish;
        GameManager.instance.OnStartGame -= OnStart;
    }
}
