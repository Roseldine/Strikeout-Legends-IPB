
using System.Runtime.InteropServices;
using UnityEngine;

public class ButtonCharPreview : MonoBehaviour
{
    enum charId { apollo, ares, poseidon, zeus }

    [SerializeField] UnityEngine.UI.Button _button;
    [SerializeField] charId _char;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<UnityEngine.UI.Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(() => UIManager.Instance.ChangeCharacterPreview((int)_char));
    }
}
