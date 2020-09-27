
using UnityEngine;

public class ButtonMainMenu : MonoBehaviour
{
    [SerializeField] UIManager.menuActivate _target;
    [SerializeField] UnityEngine.UI.Button _button;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<UnityEngine.UI.Button>();

        _button.onClick.AddListener(() => UIManager.Instance.ChangeMenu((int)_target));
    }
}
