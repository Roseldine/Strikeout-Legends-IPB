
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    Enemy _enemy;
    Attack _attack;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _attack = other.GetComponent<Attack>();

        if (_attack != null)
        {
            if (_attack.Owner == Attack.attackOwner.playerAttack)
            {
                _enemy.Damage(_attack.Damage);
                // damage
                // cc
                // buff
                // debugg
            }
        }
    }
}
