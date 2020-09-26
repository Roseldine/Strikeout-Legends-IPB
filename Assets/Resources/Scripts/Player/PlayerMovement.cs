
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
    [SerializeField] float _dashSpeed;
    [SerializeField] float _dashLength;

    [Header("Joystick")]
    [SerializeField] Joystick _joystick;

    float _horizontal, _vertical;
    Vector3 _direction;
    Quaternion _rotation;
    bool _isMoving;

    public Vector3 Direction{ get { return _direction; } }
    public Quaternion Rotation { get { return _rotation; } }    
    public bool isMoving { get { return _isMoving; } }


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
    void Move() => _rb.velocity = _direction * (_speed * _speedMultiplier);    
}
