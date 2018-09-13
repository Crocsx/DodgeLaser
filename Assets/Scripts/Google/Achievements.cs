using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class Achievements : MonoBehaviour {

    public static Achievements instance = null;
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

    private void Start()
    {
        ScoreManager.instance.OnNewHighScore += CheckScoreAchievement;
        UnlockNewPlayerAchievement();
    }

    // Update is called once per frame
    public void ShowAchievement()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
    }

    void UnlockNewPlayerAchievement()
    {
        PlayGamesPlatform.Instance.ReportProgress(
            GPGSIds.achievement_new_comer,
            100.0f, (bool success) => {
            });
    }

    public void UpgradeJump()
    {
        PlayGamesPlatform.Instance.ReportProgress(
            GPGSIds.achievement_jumper_bumper,
            1, (bool success) => {
            });
        PlayGamesPlatform.Instance.ReportProgress(
            GPGSIds.achievement_the_mystical_bird,
            1, (bool success) => {
            });
    }

    void CheckScoreAchievement(int score)
    {
        if(score > 30)
        {
            PlayGamesPlatform.Instance.ReportProgress(
                GPGSIds.achievement_the_night_fox,
                100.0f, (bool success) => {
                });
        }
        if (score > 10)
        {
            PlayGamesPlatform.Instance.ReportProgress(
                GPGSIds.achievement_dancer,
                100.0f, (bool success) =>
                {
                });
        }
    }

}
