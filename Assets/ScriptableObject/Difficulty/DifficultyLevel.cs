using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyLevel", menuName = "DifficultyLevel", order = 1)]
public class DifficultyLevel : ScriptableObject
{
    [SerializeField]
    private float _nextWaveTimer;

    [SerializeField]
    private float _enemyOnScreen;

    public float nextWaveTimer { get { return _nextWaveTimer; } }
    public float enemyOnScreen { get { return _enemyOnScreen; } }
}
