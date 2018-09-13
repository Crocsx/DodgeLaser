using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GooglePlayLoader : MonoBehaviour {

    public void Start()
    {
        // Create client configuration
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        // END THE CODE TO PASTE INTO START

        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
    }

    // Update is called once per frame
    public void SignInCallback(bool success)
    {
        if (success)
        {
            Debug.Log(" Signed in!");

        }
        else
        {
            Debug.Log(" Sign-in failed...");
        }
    }
}
