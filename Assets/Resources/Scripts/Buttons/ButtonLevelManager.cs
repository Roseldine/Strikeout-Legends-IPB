
using UnityEngine;

public class ButtonLevelManager : MonoBehaviour
{
    enum state { start, restart, mainMenu, pause, resume }
    [SerializeField] UnityEngine.UI.Button _button;
    [SerializeField] state _state;
    [SerializeField] AudioClip _soundClip;

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

        else if (_state == state.pause)
            _button.onClick.AddListener(() => LevelManager.Instance.PauseLevel());

        else if (_state == state.resume)
            _button.onClick.AddListener(() => LevelManager.Instance.ResumeLevel());

        // sound
        if (_soundClip != null)
            _button.onClick.AddListener(() => AudioManager.Instance.PlayUISound(_soundClip));
    }
}
