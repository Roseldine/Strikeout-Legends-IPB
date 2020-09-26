

using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Movement))]
public class LivingEntity : Entity, IDamagable
{
    [Header("Health Components")]
    [SerializeField] protected Health _health;
    [SerializeField] protected bool _isDead;

    [Header("Graphical Components")]
    [SerializeField] Collider _collider;
    [SerializeField] Transform _graphic;

    [Header("Movement Components")]
    [SerializeField] Movement _movement;


    // cc status effects (stunned, slowed, etc)
    // movement patterns
    // attack patterns

    private void Start()
    {
        if (_health == null)
            _health = GetComponent<Health>();
    }



    //================================================ Movement




    //================================================ Damage & Healing
    public virtual void Damage(int ammount)
    {
        
    }

    public virtual void Heal(int ammount)
    {

    }

    public virtual void Death()
    {

    }
}
