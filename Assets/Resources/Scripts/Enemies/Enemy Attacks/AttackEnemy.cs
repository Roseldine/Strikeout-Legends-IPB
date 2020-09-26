
using System.Collections;
using TMPro;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    enum attackOwner { archer, bomber, jumper, cannioneer }
    public enum attackType { arrow, bomb, cannonBall, multiArrow }

    [Header("Attack Essentials")]
    [SerializeField] Collider _collider;
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;
    [SerializeField] int _damage;
    [SerializeField] Lean.Pool.LeanGameObjectPool _pool;

    [Header("Attack Type")]
    [SerializeField] attackOwner _attackOwner;
    public attackType _attackType;
    [SerializeField] EffectEnum.statusEffect _statusEffect;

    [Header("Graphics")]
    [SerializeField] Transform _graphic;
    [SerializeField] Transform _vfxHit;

    [Header("In case of bomb")]
    [SerializeField] Transform _bomb;
    [SerializeField] float _explosionScale;
    [SerializeField] float _explosionTime;
    [Tooltip("x = units, y = time to cover units, z = curve height")]
    [SerializeField] Vector3 _durUnitTime;
    [SerializeField] AnimationCurve _curveArc;
    [SerializeField] AnimationCurve _curveScale;


    // internal vairables
    Vector3 _targetPos;
    bool _isMoving;

    // properties
    #region Properties
    public int AttackDamage { get { return _damage; } }
    public EffectEnum.statusEffect StatusEffect { get { return _statusEffect; } }

    #endregion


    private void OnEnable()
    {
        _isMoving = false;
        if (_bomb != null)
            _bomb.localScale = Vector3.one * .5f;
    }    

    private void Update()
    {
        Move();
    }


    //================================================ Setters
    public void SetTarget(Vector3 targetPos)
    {
        Vector3 _dir = targetPos - transform.position;
        transform.rotation = Quaternion.LookRotation(_dir);

        _targetPos = targetPos;
        _targetPos.y = 1;
    }



    //================================================ Movement
    void Move()
    {
        switch(_attackType)
        {
            case attackType.arrow:
                MoveArrow();
                break;

            case attackType.bomb:
                MoveBomb();
                break;

            case attackType.cannonBall:
                MoveCannionBall();
                break;
        }
    }



    void MoveArrow()
    {
        if (_isMoving == false && _rb != null)
        {
            _rb.velocity = transform.forward.normalized * _speed;
            _isMoving = true;
        }
    }


    void MoveBomb()
    {
        if (_isMoving == false)
            StartCoroutine(CR_MoveBomb());
    }

    IEnumerator CR_MoveBomb()
    {
        // calculate flight duration
        float t = 0;
        float _dist = Vector3.Distance(_targetPos, transform.position);
        float _dur = _dist * _durUnitTime.y / _durUnitTime.x;

        // disable collider
        _collider.enabled = false;

        // create arc
        Vector3 _curPos = transform.position;
        var _keys = _curveArc.keys;
        _keys[0].value = _curPos.y;
        _keys[1].time = _dur / 2;
        _keys[1].value = _curPos.y + _durUnitTime.z;
        _keys[2].time = _dur;
        _curveArc.keys = _keys;

        _isMoving = true;

        while(t < _dur)
        {
            transform.position = Vector3.Lerp(_curPos, _targetPos, t/_dur);

            if (_bomb != null)
                _bomb.localPosition = new Vector3(0, _curveArc.Evaluate(t), 0);

            t += Time.deltaTime;
            yield return null;
        }

        // enable collider
        _collider.enabled = true;

        // edit arc
        _keys = _curveScale.keys;
        _keys[0].value = 0;
        _keys[1].value = _explosionScale;
        _keys[1].time = _explosionTime / 2;
        _keys[2].value = 0;
        _keys[2].time = _explosionTime;
        _curveScale.keys = _keys;

        t = 0;
        while (t < _explosionTime)
        {
            if (_bomb != null)
                _bomb.localScale = Vector3.one * _curveScale.Evaluate(t);

            t += Time.deltaTime;
            yield return null;
        }

        if (_bomb != null)
            _bomb.localScale = Vector3.zero;
    }


    void MoveCannionBall()
    {

    }



    //================================================ Lifetime
    IEnumerator CR_Lifetime()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
