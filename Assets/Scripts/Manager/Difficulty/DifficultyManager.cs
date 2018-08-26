using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    #region Variables
    public enum DifficultyAvailable { Easy, Medium, Hard };
    public DifficultyLevel Easy;
    public DifficultyLevel Medium;
    public DifficultyLevel Hard;

    public DifficultyAvailable difficultySelected { get { return _difficultySelected; } }
    DifficultyAvailable _difficultySelected;
    public DifficultyLevel difficultySelectedSettings { get { return _difficultySelectedSettings; } }
    DifficultyLevel _difficultySelectedSettings;

    public static DifficultyManager instance = null;
    #endregion

    #region Singleton Initialization 
    void Awake()
    {

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        _difficultySelectedSettings = Easy;
        _difficultySelected = DifficultyAvailable.Easy;
    }

    public void ChangeDifficulty(DifficultyAvailable chosenDifficulty)
    {
        if(chosenDifficulty == DifficultyAvailable.Easy)
            _difficultySelectedSettings = Easy;
        if (chosenDifficulty == DifficultyAvailable.Medium)
            _difficultySelectedSettings = Medium;
        if (chosenDifficulty == DifficultyAvailable.Hard)
            _difficultySelectedSettings = Hard;

        _difficultySelected = chosenDifficulty;
    }
}