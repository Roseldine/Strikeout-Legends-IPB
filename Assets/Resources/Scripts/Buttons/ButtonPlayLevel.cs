
using UnityEngine;

public class ButtonPlayLevel : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Button _button;
    [SerializeField] int _sceneId;
    [SerializeField] AudioClip _soundClip;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<UnityEngine.UI.Button>();

        if (_soundClip != null)
            _button.onClick.AddListener(() => AudioManager.Instance.PlayUISound(_soundClip));

        _button.onClick.AddListener(() => GameManager.Instance.LoadScene(_sceneId));
    }
}
