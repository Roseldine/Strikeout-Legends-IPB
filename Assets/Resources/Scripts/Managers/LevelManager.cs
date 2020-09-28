
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public enum levelState { waiting, playing, won, lost, restarting, paused }

    public static LevelManager Instance;

    [Header("Win Condition")]
    [SerializeField] levelState _levelState;

    [Header("UI Elements")]
    [Tooltip("start, win, lose")]
    [SerializeField] GameObject[] _uiElements;

    [Header("Music")]
    [SerializeField] AudioClip _music;


    private void Awake()
    {
        Instance = GetComponent<LevelManager>();
        _levelState = levelState.waiting;
        
    }

    private void OnEnable()
    {
        Invoke("StartLevel", .3f);
        AudioManager.Instance.PlayMusic(_music);
    }


    //====================================================== State Management
    public void ChangeLevelState(levelState levelState)
    {
        _levelState = levelState;
    }

    public void UpdateState()
    {
        // check if won
        if (EnemyManager.Instance.EnemiesDespawned >= EnemyManager.Instance.EnemiesSpawned)
        {
            ChangeLevelState(levelState.won);
            EnableUI(1);
            StopLevel();
        }

        // check if lost
        if (PlayerManager.Instance.PlayerHealth <= 0)
        {
            ChangeLevelState(levelState.lost);
            EnableUI(2);
            StopLevel();
        }
    }




    //====================================================== Start & Restart
    public void StartLevel()
    {
        ChangeLevelState(levelState.playing);
        EnemyManager.Instance.StartEnemies();
        PlayerManager.Instance.StartPlayer();
        DisableUI();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        ChangeLevelState(levelState.restarting);
        PlayerManager.Instance.RestartPlayer();
        EnemyManager.Instance.RestartEnemies();
        DisableUI();
        Invoke("StartLevel", .3f);
    }

    public void StopLevel()
    {
        PlayerManager.Instance.StopPlayer();
        EnemyManager.Instance.DespawnAll();
    }

    public void PauseLevel()
    {
        ChangeLevelState(levelState.paused);
        EnableUI(3);
        Time.timeScale = 0;
    }

    public void ResumeLevel()
    {
        ChangeLevelState(levelState.playing);
        DisableUI();
        Time.timeScale = 1;
    }





    //====================================================== UI Management
    void DisableUI()
    {
        foreach (GameObject g in _uiElements)
            g.SetActive(false);
    }

    /// <summary>
    /// start, win, lose
    /// </summary>
    void EnableUI(int id)
    {
        DisableUI();
        _uiElements[id].SetActive(true);
    }
}
