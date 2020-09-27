
using UnityEngine;

public class ButtonLevelManager : MonoBehaviour
{
    enum state { start, restart, mainMenu }
    [SerializeField] UnityEngine.UI.Button _button;
    [SerializeField] state _state;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<UnityEngine.UI.Button>();

        if (_state == state.start)
            _button.onClick.AddListener(() => LevelManager.Instance.StartLevel());

        else if (_state == state.restart)
            _button.onClick.AddListener(() => LevelManager.Instance.RestartLevel());

        else if (_state == state.mainMenu)
            _button.onClick.AddListener(() => GameManager.Instance.LoadScene(1));
    }
}
