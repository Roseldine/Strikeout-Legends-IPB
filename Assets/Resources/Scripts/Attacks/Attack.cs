
using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum attackOwner { playerAttack, enemyAttack }
    public enum attackType { none, basic, special }

    [Header("Owner")]
    [SerializeField] protected attackOwner _owner;
    [SerializeField] protected attackType _attackType;
    [SerializeField] Ability _ability;

    [Header("Attack Parameters")]
    [SerializeField] EffectEnum.statusEffect _effect;
    [SerializeField] int _damage;

    public int Damage { get { return _damage; } }
    public attackOwner Owner { get { return _owner; } }
    public Ability Ability { get { return _ability; } set { _ability = value; } }




    private void OnTriggerEnter(Collider other)
    {
        string _tag = other.tag;

        if (_tag == "Wall" && _attackType != attackType.special)
        {
            _ability.Despawn();
        }


        if (_owner == attackOwner.playerAttack)
        {
            if (_tag == "Enemy" && _attackType == attackType.basic)
            {
                ParticleManager.Instance.SpawnHitWall(transform.position);
                _ability.Despawn();
            }
        }

        else if (_owner == attackOwner.enemyAttack)
        {
            if (_tag == "Player")
            {
                ParticleManager.Instance.SpawnHitEnemy(other.transform.position);
                _ability.Despawn();
            }
        }
    }
}
