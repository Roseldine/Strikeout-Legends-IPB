
using UnityEngine;

public class ButtonPlayLevel : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Button _button;
    [SerializeField] int _sceneId;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<UnityEngine.UI.Button>();

        _button.onClick.AddListener(() => GameManager.Instance.LoadScene(_sceneId));
    }
}
