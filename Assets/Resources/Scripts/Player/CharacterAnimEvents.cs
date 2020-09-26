using UnityEngine;

public class CharacterAnimEvents : MonoBehaviour
{
    // Variables
    public Transform projectileSpawner;

    public void Attack()
    {
        // tell manager player wants to attack
        PlayerManager.Instance.Attack();
    }


    public void StopAttack()
    {
        // tell manager player needs to stop attacking

    }
}
