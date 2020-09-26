
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerCollisions : MonoBehaviour
{
    Attack _attack;

    private void OnTriggerEnter(Collider other)
    {
        _attack = other.GetComponent<Attack>();

        if (_attack != null)
        {
            if (_attack.Owner == Attack.attackOwner.enemyAttack)
            {
                if (PlayerManager.Instance != null)
                {
                    PlayerManager.Instance.Damage(_attack.Damage);
                    // damage
                    // cc
                    // buff
                    // debugg
                }
            }
            
        }
    }
}
