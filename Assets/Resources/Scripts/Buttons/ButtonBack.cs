
using UnityEngine;

public class ButtonBack : MonoBehaviour
{
    [SerializeField] UIManager.menuActivate _backTo;
    [SerializeField] UnityEngine.UI.Button _button;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<UnityEngine.UI.Button>();

        _button.onClick.AddListener(() => UIManager.Instance.ChangeMenu((int)_backTo));
    }
}
