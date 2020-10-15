
using UnityEngine;

public class ButtonMainMenu : MonoBehaviour
{
    [SerializeField] UIManager.menuActivate _target;
    [SerializeField] UnityEngine.UI.Button _button;
    [SerializeField] AudioClip _soundClip;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<UnityEngine.UI.Button>();

        _button.onClick.AddListener(() => UIManager.Instance.ChangeMenu((int)_target));

        if (_soundClip != null)
            _button.onClick.AddListener(() => AudioManager.Instance.PlayUISound(_soundClip));
    }
}
