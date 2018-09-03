using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    #region GameState
    public enum GameState
    {
        MENU, LOADING, PAUSE, PLAY, END
    }
    private static GameState gameState;
    #endregion

    #region Event
    #region Game 
    public delegate void onInitGame();
    public event onInitGame OnInitGame;
    public delegate void onStartGame();
    public event onStartGame OnStartGame;
    public delegate void onPauseGame();
    public event onPauseGame OnPauseGame;
    public delegate void onResumeGame();
    public event onResumeGame OnResumeGame;
    public delegate void onFinishGame();
    public event onFinishGame OnFinishGame;
    public delegate void onEndGame();
    public event onEndGame OnEndGame;
    #endregion

    #region Scene Event
    public delegate void onLoadScene(string sceneName);
    public event onLoadScene OnLoadScene;
    public delegate void onReloadScene(string sceneName);
    public event onReloadScene OnReloadScene;
    public delegate void onScenePreloadWaitingReady(AsyncOperation sceneLoaded, string sceneName);
    public event onScenePreloadWaitingReady OnScenePreloadWaitingReady;
    public delegate void onScenePreloadComplete(AsyncOperation sceneLoaded, string sceneName);
    public event onScenePreloadComplete OnScenePreloadComplete;
    public delegate void onSceneLoaded(Scene scene, LoadSceneMode sceneMode);
    public event onSceneLoaded OnSceneLoaded;
    #endregion
    #endregion

    #region Singleton Initialization 
    void Awake()
    {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(transform.gameObject);
    }
    #endregion

    #region Dispatchers

    #region Game State
    void d_InitGame()
    {
        if (OnInitGame != null)
            OnInitGame();
    }

    void d_StartGame()
    {
        if (OnStartGame != null)
            OnStartGame();
    }

    void d_PauseGame()
    {
        if (OnPauseGame != null)
            OnPauseGame();
    }

    void d_ResumeGame()
    {
        if (OnResumeGame != null)
            OnResumeGame();
    }

    void d_FinishGame()
    {
        if (OnFinishGame != null)
            OnFinishGame();
    }

    void d_EndGame()
    {
        if (OnEndGame != null)
            OnEndGame();
    }
    #endregion

    #region Scene Loading Dispatchers
    void d_LoadedScene(Scene scene, LoadSceneMode sceneMode)
    {
        SceneManager.sceneLoaded -= d_LoadedScene;
        if (OnSceneLoaded != null)
            OnSceneLoaded(scene, sceneMode);
    }

    void d_LoadScene(string LevelName)
    {
        if (OnLoadScene != null)
            OnLoadScene(LevelName);
    }

    void d_PreLoadScene(string LevelName, bool allowSceneActivation)
    {
        StartCoroutine(PreloadMenu(LevelName, allowSceneActivation));
    }

    IEnumerator PreloadMenu(string LevelName, bool allowSceneActivation)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(LevelName);
        asyncLoad.allowSceneActivation = allowSceneActivation;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                if (OnScenePreloadWaitingReady != null)
                    OnScenePreloadWaitingReady(asyncLoad, LevelName);
            }
            yield return null;
        }

        if(OnScenePreloadComplete != null)
            OnScenePreloadComplete(asyncLoad, LevelName);
    }
    #endregion
    #endregion

    #region Methods
    #region Game State
    public void InitGame()
    {
        setState(GameState.LOADING);
        d_InitGame();
    }

    public void StartGame()
    {
        setState(GameState.PLAY);
        d_StartGame();
    }

    public void PauseGame()
    {
        setState(GameState.PAUSE);
        d_PauseGame();
    }

    public void ResumeGame()
    {
        setState(GameState.PLAY);
        d_ResumeGame();
    }

    public void FinishGame()
    {
        setState(GameState.MENU);
        d_FinishGame();
    }

    public void EndGame()
    {
        setState(GameState.MENU);
        d_EndGame();
    }
#endregion

    #region Scene
    public void ReloadScene()
    {
        setState(GameState.LOADING);
        string SceneName = SceneManager.GetActiveScene().name;
        if (OnReloadScene != null)
            OnReloadScene(SceneName);
        LoadScene(SceneName);
    }

    public void LoadScene(string LevelName)
    {
        setState(GameState.LOADING);
        SceneManager.LoadScene(LevelName);
        SceneManager.sceneLoaded += d_LoadedScene;
        d_LoadScene(LevelName);
    }

    public void PreloadScene(string LevelName, bool allowSceneActivation)
    {
        d_PreLoadScene(LevelName, allowSceneActivation);
    }
    #endregion

        #region GameState
        private static void setState(GameState state)
    {
        gameState = state;
    }
    #endregion
    #endregion
}
