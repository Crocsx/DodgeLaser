using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject Home;
    public GameObject Option;
    public GameObject Credits;

    GameObject currentPanel;

    private void Start()
    {
        ShowPanel("Home");
    }

    public void ShowPanel(string name)
    {
        DeactivateCurrent();
        currentPanel = GetPanel(name);
        Activate();
    }

    public void LoadGame()
    {
        DeactivateCurrent();
        StartGame();
    }

    public void ShowAchievement()
    {
        Achievements.instance.ShowAchievement();
    }

    public void Quit()
    {
        Application.Quit();
    }

    void StartGame()
    {
        SceneManager.LoadScene("Stage01");
    }

    // Update is called once per frame
    void DeactivateCurrent()
    {
        if (currentPanel == null)
            return;

        currentPanel.SetActive(false);
        currentPanel = null;
    }

    void Activate()
    {
        currentPanel.SetActive(true);
    }

    GameObject GetPanel(string name)
    {
        GameObject panel;
        switch (name)
        {
            case "Options":
                panel = Option;
                break;
            case "Credits":
                panel = Credits;
                break;
            case "Home":
            default:
                panel = Home;
                break;
        }
        return panel;
    }
}