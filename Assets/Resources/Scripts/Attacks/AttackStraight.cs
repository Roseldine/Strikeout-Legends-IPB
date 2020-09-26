
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AttackStraight : Attack
{
    [Header("Straight Parameters")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speed;

    TrailRenderer[] _trails;

    private void Awake()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody>();
        StartTrailArray();
    }


    private void OnEnable()
    {
        AddForce();
    }

    private void OnDisable()
    {
        ResetTrail();
    }




    void AddForce()
    {
        _rb.velocity = Vector3.zero;
        _rb.velocity = transform.forward * _speed;
    }


    void StartTrailArray()
    {
        var _trailParent = GetComponentsInParent<TrailRenderer>();
        var _trailChildren = GetComponentsInChildren<TrailRenderer>();

        if (_trailChildren != null)
        {
            _trails = _trailChildren;

            if (_trailParent != null)
                _trails.Concat(_trailParent).ToArray();
        }
    }


    void ResetTrail()
    {
        if (_trails.Length > 0)
            foreach (TrailRenderer t in _trails)
                t.Clear();
    }
}