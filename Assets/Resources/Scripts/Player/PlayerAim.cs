
using System.Collections;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    enum controllerType { mobile, pc, axial }

    [Header("Essential Variables")]
    [SerializeField] controllerType _controllerType;
    [SerializeField] LayerMask _mask;
    [SerializeField] float _chargeTime;
    [SerializeField] Transform[] _waypoints;

    Camera _camera;

    Vector3 _aimPos, _aimDirection, _aimTarget;

    Ray _ray;
    RaycastHit _hit;

    bool _isAttacking;
    bool _isCharging;
    bool _isSpecial;
    bool _input;

    // Variable Properties
    public Vector3 AimPosition { get { return _aimPos; } }
    public Vector3 AimDirection { get { return _aimDirection; } }
    public Vector3 AimTarget { get { return _aimTarget; } }
    public bool isCharging { get { return _isCharging; } }
    public bool isSpecial { get { return _isSpecial; } }
    public bool isAttacking { get { return _isAttacking; } }



    private void Start()
    {
        _camera = Camera.main;
    }

    public void AimUpdate()
    {
        if (PlayerManager.Instance != null)
        {
            MouseRaycast();
            CalculateDirection();
            AimInput();
        }
    }


    //================================================== Raycast
    void MouseRaycast()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, 100f, _mask))
            _aimPos = _hit.point + Vector3.up;
    }

    void CalculateDirection()
    {
        _aimDirection = _aimPos - transform.position;
        _waypoints[2].position = _aimPos;
    }



    //================================================== Charging
    void AimInput()
    {
        _input = Input.GetButton("Fire1");

        if (_input == true && _isCharging == false)
        {
            StartCoroutine(CR_Charge());
        }

        else if (_input == false && _isCharging == true)
        {
            PlayerManager.Instance.AttackStart();
            PlayerManager.Instance.EnergyChargeStop();
            _aimTarget = _aimPos;
            _isAttacking = true;
            _isCharging = false;
        }
    }

    IEnumerator CR_Charge()
    {
        float t = 0;
        _isSpecial = false;
        _isCharging = true;

        while (_isCharging)
        {
            if (t >= _chargeTime)
            {
                _isSpecial = true;
                Debug.Log(_isSpecial);
                break;
            }

            //Debug.Log(t.ToString("f2"));

            PlayerManager.Instance.EnergyCharge(t / _chargeTime);
            t += Time.deltaTime;
            yield return null;
        }
    }

    public void ResetCharge() => _isAttacking = false;
}
