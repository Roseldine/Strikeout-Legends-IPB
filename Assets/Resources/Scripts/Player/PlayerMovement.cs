
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    enum controllerType { mobile, axial }
    enum invertionType { normal, invert }

    [Header("Essential Variables")]
    [SerializeField] controllerType _controllerType;
    [SerializeField] invertionType _invertType;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;
    [SerializeField] float _speedMultiplier;
    [SerializeField] float _lookSpeed;

    [Header("Dash Variables")]
    [SerializeField] TrailRenderer _dashTrail;
    [SerializeField] float _dashMultiplier;
    [SerializeField] float _dashLength;
    bool _isDashing = false;

    [Header("Joystick")]
    [SerializeField] Joystick _joystick;

    float _horizontal, _vertical;
    Vector3 _direction;
    Quaternion _rotation;
    bool _isMoving;

    [Header("Debugging")]
    [SerializeField] bool _debug;

    public Vector3 Direction{ get { return _direction; } }
    public Quaternion Rotation { get { return _rotation; } }    
    public bool isMoving { get { return _isMoving; } }
    public TrailRenderer DashTrail { get { return _dashTrail; } set { _dashTrail = value; } }


    private void Start()
    {
        if (_dashTrail != null)
            _dashTrail.gameObject.SetActive(false);
    }


    // Update is called once per frame
    public void MovementUpdate()
    {
        if (PlayerManager.Instance != null)
        {
            UserInput();
            Move();
        }
    }



    //================================================== User Input
    public void UserInput()
    {
        switch (_controllerType)
        {
            case controllerType.mobile:
                JoystickInput();
                break;

            case controllerType.axial:
                AxialInput();
                if (Input.GetKeyDown(KeyCode.LeftShift) == true && _isDashing == false)
                    StartCoroutine(CR_Dash());
                break;
        }
    }

    void JoystickInput()
    {
        // joystick movement
        if (_joystick != null)
        {
            _horizontal = _joystick.Horizontal;
            _vertical = _joystick.Vertical;

            CalculateDirection();
        }
    }

    void AxialInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        CalculateDirection();
    }

    void CalculateDirection()
    {
        // direction
        _direction = new Vector3(_horizontal, 0, _vertical).normalized;
        if (_invertType == invertionType.invert) _direction *= -1;

        // rotation
        if (_direction != Vector3.zero)
        {
            _rotation = Quaternion.LookRotation(_direction);
            _isMoving = true;
        }

        else
            _isMoving = false;
    }



    //================================================== Movement
    void Move()
    {
        if (_isDashing == false)
            _rb.velocity = _direction * (_speed * _speedMultiplier);
    }

    IEnumerator CR_Dash()
    {
        _isDashing = true;
        _dashTrail.Clear();
        _dashTrail.gameObject.SetActive(true);
        float t = 0;

        while (t < _dashLength)
        {
            _rb.velocity = _direction * (_speed * _dashMultiplier * _speedMultiplier);

            t += Time.deltaTime;
            yield return null;
        }

        _isDashing = false;
        _dashTrail.gameObject.SetActive(false);
    }
}
