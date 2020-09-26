
using UnityEngine;
using Lean.Pool;
using System.Collections;

public class PlayerAttackController : MonoBehaviour
{
    public enum specialSpawnPlace { spawner, player, aimPosition }

    [SerializeField] specialSpawnPlace _spawnPlace;

    [SerializeField] LeanGameObjectPool _poolBasic;
    [SerializeField] LeanGameObjectPool _poolSpecial;
    [SerializeField] float _cooldown;
    bool _canAttack = true;

    Transform _attackSpawner;
    Vector3 _direction;


    public specialSpawnPlace SpecialSpawnPlace { set { _spawnPlace = value; } }


    private void Start()
    {
        _attackSpawner = PlayerManager.Instance.AttackSpawner;
    }



    //========================================================== Spawn
    public void SpawnAttack()
    {
        if (_canAttack == true)
        {
            // Basic Ability
            if (PlayerManager.Instance.isSpecial == false)
            {
                _direction = PlayerManager.Instance.AimPosition - _attackSpawner.position;
                Ability _ability = _poolBasic.Spawn(_attackSpawner.position, GetRotation(_direction)).GetComponent<Ability>();

                if (_ability.Pool == null)
                    _ability.Pool = _poolBasic;
            }


            // Special Ability
            else if (PlayerManager.Instance.isSpecial == true)
            {
                Ability _ability = null;

                switch (_spawnPlace)
                {
                    case specialSpawnPlace.spawner:
                        _direction = PlayerManager.Instance.AimPosition - _attackSpawner.position;
                        _ability = _poolSpecial.Spawn(_attackSpawner.position, GetRotation(_direction)).GetComponent<Ability>();
                        break;

                    case specialSpawnPlace.player:
                        _direction = PlayerManager.Instance.AimPosition - PlayerManager.Instance.Player.position;
                        _ability = _poolSpecial.Spawn(PlayerManager.Instance.Player.position, GetRotation(_direction)).GetComponent<Ability>();
                        break;

                    case specialSpawnPlace.aimPosition:
                        _direction = Vector3.zero;
                        _ability = _poolSpecial.Spawn(PlayerManager.Instance.AimPosition, GetRotation(_direction)).GetComponent<Ability>();
                        break;
                }

                if (_ability.Pool == null)
                    _ability.Pool = _poolSpecial;
            }

            StartCoroutine(CR_AttackCD());
            _direction = Vector3.zero;
        }
    }


    IEnumerator CR_AttackCD()
    {
        Debug.Log("<color=yellow>Attack: Spawned</color>");
        _canAttack = false;
        yield return new WaitForSeconds(_cooldown);
        _canAttack = true;
    }


    Quaternion GetRotation(Vector3 direction) => Quaternion.LookRotation(direction);



    

    //========================================================== Start Up
    public void SetAttackPools(GameObject basic, GameObject special)
    {
        _poolBasic = CreatePool(basic, 10);
        _poolSpecial = CreatePool(special, 3);
    }


    LeanGameObjectPool CreatePool(GameObject prefab, int maxCopies)
    {
        LeanGameObjectPool _pool = PlayerManager.Instance.Player.gameObject.AddComponent<LeanGameObjectPool>();
        _pool.Prefab = prefab;
        _pool.Recycle = true;
        _pool.Capacity = maxCopies;

        return _pool;
    }
}
