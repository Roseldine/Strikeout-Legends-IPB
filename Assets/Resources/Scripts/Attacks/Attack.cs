
using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum attackOwner { playerAttack, enemyAttack }
    

    [Header("Owner")]
    [SerializeField] protected attackOwner _owner;
    [SerializeField] Ability _ability;

    [Header("Attack Parameters")]
    [SerializeField] EffectEnum.statusEffect _effect;
    [SerializeField] int _damage;

    public int Damage { get { return _damage; } }
    public attackOwner Owner { get { return _owner; } }
    public Ability Ability { get { return _ability; } set { _ability = value; } }
}
