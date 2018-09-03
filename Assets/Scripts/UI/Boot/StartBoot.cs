using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartBoot : MonoBehaviour {
    public Button startMenuButton;
    AsyncOperation sceneLoaded;

    // Use this for initialization
    void Start () {
        GameManager.instance.OnScenePreloadWaitingReady += ActivateButton;
        GameManager.instance.PreloadScene("MainMenu", false);
    }

    void ActivateButton(AsyncOperation scene, string name)
    {
        sceneLoaded = scene;
        startMenuButton.enabled = true;
    }

	public void OnEndAnim ()
    {
        sceneLoaded.allowSceneActivation = true;
	}
}
