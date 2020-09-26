
using System.Collections;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [Header("Attack Prefab & Spawner")]
    public Lean.Pool.LeanGameObjectPool _pool;
    [SerializeField] Transform _attack;
    [SerializeField] Transform _spawner;
    [SerializeField] float _attackCD;

    Transform _player;
    bool _canAttack = true;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Attack()
    {
        if (_canAttack == true)
        {
            CreateAttackPrefab();

            StartCoroutine(CR_AttackCD());
        }
    }

    void CreateAttackPrefab()
    {
        Vector3 _dir = _player.position - transform.position;
        Quaternion _rot = Quaternion.LookRotation(_dir);

        var _inst = Instantiate(_attack, _spawner.position, _rot);
        if (_pool != null)
            _inst.GetComponent<LifeTime>().Pool = _pool;

        if (_inst.GetComponent<AttackEnemy>() == true)
        {
            var _attck = _inst.GetComponent<AttackEnemy>();
            int _id = (int)_attck._attackType;

            // bomb
            if (_id == 1)
                _attck.SetTarget(_player.position);
        }
    }

    IEnumerator CR_AttackCD()
    {
        _canAttack = false;
        yield return new WaitForSeconds(_attackCD);
        _canAttack = true;
    }
}
