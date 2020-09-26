﻿
using UnityEngine;

public class Ability : MonoBehaviour
{
    public enum lifeTimeType { unlimited, limited }

    [Header("Ability")]
    [SerializeField] Attack _attack;
    [SerializeField] Lean.Pool.LeanGameObjectPool _pool;

    [Header("Lifetime")]
    [SerializeField] lifeTimeType _lifeType;
    [SerializeField] float _lifeTime;

    public Attack Attack { get { return _attack; } }
    public Lean.Pool.LeanGameObjectPool Pool { get { return _pool; } set { _pool = value; } }


    private void Awake()
    {
        SearchForAttack();
    }

    private void OnEnable()
    {
        if (_lifeType == lifeTimeType.limited)
            Invoke("Despawn", _lifeTime);
    }


    void SearchForAttack()
    {
        if (_attack == null)
            _attack = GetComponentInChildren<Attack>();
        _attack.Ability = GetComponent<Ability>();
    }


    public void Despawn()
    {
        if (_pool != null)
            _pool.Despawn(gameObject);
        else
            Destroy(gameObject);
    }
}
