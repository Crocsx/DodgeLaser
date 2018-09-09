using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{



    public GameObject pStart;
    public GameObject pPause;
    public GameObject pUI;
    public GameObject pEnd;

    GameObject currentPanel;

    public static InGameMenu instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.LogWarning("IGMenu.Awake() - instance already exists!");
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        GameManager.instance.OnInitGame += OnInit;
        GameManager.instance.OnPauseGame += OnPause;
        GameManager.instance.OnResumeGame += OnResume;
        GameManager.instance.OnStartGame += OnStart;
        GameManager.instance.OnFinishGame += OnFinish;
    }

    void Start()
    {
        currentPanel = null;
        ShowPanel("Start");
    }

    public void ShowPanel(string name)
    {
        DeactivateCurrent();
        Activate(GetPanel(name));
    }

    void DeactivateCurrent()
    {
        if (currentPanel == null)
            return;

        currentPanel.SetActive(false);
        currentPanel = null;
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ReloadStage(string name)
    {
        DeactivateCurrent();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnInit()
    {
        ShowPanel("Start");
    }

    private void OnStart()
    {
        ShowPanel("UI");
    }

    private void OnPause()
    {
        ShowPanel("Pause");
    }

    private void OnResume()
    {
        ShowPanel("UI");
    }

    private void OnFinish()
    {
        ShowPanel("Finish");
    }

    void Activate(GameObject panel)
    {
        currentPanel = panel;
        currentPanel.SetActive(true);
    }

    GameObject GetPanel(string name)
    {
        GameObject panel;
        switch (name)
        {
            case "Start":
                panel = pStart;
                break;
            case "Finish":
                panel = pEnd;
                break;
            case "UI":
                panel = pUI;
                break;
            case "Pause":
            default:
                panel = pPause;
                break;
        }
        return panel;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnResumeGame -= OnResume;
        GameManager.instance.OnPauseGame -= OnPause;
        GameManager.instance.OnFinishGame -= OnFinish;
        GameManager.instance.OnStartGame -= OnStart;
        currentPanel = null;
    }
}
