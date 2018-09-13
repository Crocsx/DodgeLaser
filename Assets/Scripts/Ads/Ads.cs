using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour {
    public string id;

    public static Ads instance = null;

    int adsCounter = 3;
    int adsCounterRemaning;

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

    // Use this for initialization
    void Start () {
        GameManager.instance.OnLoadScene += Counter;
        Advertisement.Initialize(id);

    }
	
	// Update is called once per frame
	public void ShowAd () {
        Advertisement.Show();
    }

    public void Counter(string scene)
    {
        adsCounterRemaning++;
        if (adsCounterRemaning == adsCounter)
            ShowAd();
    }
}
