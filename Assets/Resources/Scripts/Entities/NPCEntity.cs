
using UnityEngine;

public class NPCEntity : LivingEntity, IDamagable
{
    enum npcNature { friendly, neutral, hostile }
    enum npcMovement { idle, waypoints, random }

    [Header("NPC Components")]
    [SerializeField] npcNature _nature;
    [SerializeField] npcMovement _movement;


    public override void Damage(int ammount)
    {

    }

    public override void Heal(int ammount)
    {

    }

    public override void Death()
    {

    }
}
