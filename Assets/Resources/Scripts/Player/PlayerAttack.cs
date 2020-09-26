
using TMPro;
using UnityEngine;

[RequireComponent(typeof(LifeTime))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerAttack : MonoBehaviour
{
    enum attackMovement { none, straight, arc, boomerang }

    [Header("Essential Parameters")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] LifeTime _lifetime;
    [SerializeField] EffectEnum.statusEffect _statusEffect;
    [SerializeField] attackMovement _movement;
    [SerializeField] int _damage = 20;
    [SerializeField] float _speed = 10;

    TrailRenderer[] _trails;

    public int Damage { get { return _damage; } set { _damage = value; } }
    public Lean.Pool.LeanGameObjectPool Pool { get { return _lifetime.Pool; } set { _lifetime.Pool = value; } }


    private void Awake()
    {
        if (_lifetime == null)
            _lifetime = GetComponent<LifeTime>();
        if (_rb == null)
            _rb = GetComponent<Rigidbody>();

        if (tag != "Player_Attack")
            tag = "Player_Attack";

        _trails = GetComponentsInChildren<TrailRenderer>();
    }


    private void OnEnable()
    {
        if (_movement == attackMovement.straight)
            _rb.velocity = transform.forward * _speed;

        if (_trails.Length > 0)
            foreach (TrailRenderer t in _trails)
                t.Clear();
    }
}
