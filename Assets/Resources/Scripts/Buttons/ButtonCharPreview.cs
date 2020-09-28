
using System.Runtime.InteropServices;
using UnityEngine;

public class ButtonCharPreview : MonoBehaviour
{
    enum charId { apollo, ares, poseidon, zeus }

    [SerializeField] UnityEngine.UI.Button _button;
    [SerializeField] charId _char;
    [SerializeField] AudioClip _soundClip;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<UnityEngine.UI.Button>();

        if (_soundClip != null)
            _button.onClick.AddListener(() => AudioManager.Instance.PlayUISound(_soundClip));
    }

    private void Start()
    {
        _button.onClick.AddListener(() => UIManager.Instance.ChangeCharacterPreview((int)_char));
    }
}
