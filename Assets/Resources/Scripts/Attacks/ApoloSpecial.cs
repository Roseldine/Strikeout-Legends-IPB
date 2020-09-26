
using UnityEngine;

public class ApoloSpecial : Ability
{
    [Header("Parameters")]
    [SerializeField] Transform _pivot;
    [Tooltip("x = Distance, y = Scale Ammount")]
    [SerializeField] Vector2 _ratio;
    [SerializeField] float _lookSpeed;
    [SerializeField] float _minScale;
    [SerializeField] float _maxScale;

    [SerializeField] AnimationCurve _curve;

    [Header("Debug")]
    [SerializeField] bool _lookAtMouse;

    float _distance, _scale;
    Quaternion _rotation;
    Transform _player;


    private void Update()
    {
        transform.position = PlayerManager.Instance.Player.position;
        ActivateLookAtMosue();

        if (_lookAtMouse)
        {
            LookAtMosue();
            CalculateScale();
        }
    }



    void CalculateScale()
    {
        _distance = Vector3.Distance(_pivot.position, PlayerManager.Instance.AimPosition);
        _scale = _distance * _ratio.y / _ratio.x;
        _scale = Mathf.Clamp(_scale, _minScale, _maxScale);

        _pivot.localScale = new Vector3(1, 1, _scale);
    }

    void LookAtMosue()
    {
        Vector3 _dir = PlayerManager.Instance.AimPosition - transform.position;

        if (_dir != Vector3.zero)
            _rotation = Quaternion.LookRotation(_dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, _lookSpeed * Time.deltaTime);
    }

    void ActivateLookAtMosue()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _lookAtMouse = !_lookAtMouse;
    }
}
