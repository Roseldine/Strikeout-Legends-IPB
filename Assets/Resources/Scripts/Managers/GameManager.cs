
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager Instance;

    [SerializeField] Object[] _scenes;

    [Header("Characters")]
    public SOCharacterDictionary characterDictionary;
    public int charId;



    private void Awake()
    {
        Instance = GetComponent<GameManager>();
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void SetCharacterId(int id)
    {
        charId = id;
        UIManager.Instance.ChangeCharacterPreview(id);
    }


    /// <summary>
    /// Start, Menu, Game
    /// </summary>
    public void LoadScene(int id)
    {
        SceneManager.LoadSceneAsync(id);
        Time.timeScale = 1;
    }
}
