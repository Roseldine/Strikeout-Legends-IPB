
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class Movement : MonoBehaviour, ICrowdControl
{
    [Header("Movement Components")]
    [Tooltip("NPC Idle, NPC Waypoints, NPC Random, Player")]
    [SerializeField] EntityEnums.movementType _moveType;
    [Tooltip("Rigidbody, Transform, NavMeshAgent")]
    [SerializeField] EntityEnums.movementController _moveController;

    [Header("Speed & Crowd Controll")]
    [SerializeField] EntityEnums.ccStatus _ccStatus;
    [SerializeField] float _speed;
    [SerializeField] float _speedMultiplier;

    [Header("Controller Types")]
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Transform _transform;
    [SerializeField] NavMeshAgent _agent;

    [Header("Controller Variables")]
    [SerializeField] float _randomRadius;
    [SerializeField] float _breakTime;
    [SerializeField] Transform[] _waypoints;

    // random agent navigation
    Vector3 _randPos;


    private void Update()
    {
        MoveType();
    }


    //======================================================== Movement
    void MoveType()
    {
        switch (_moveType)
        {
            case EntityEnums.movementType.npcIdle:
                NPC_Idle();
                break;

            case EntityEnums.movementType.npcRandom:
                NPC_Random();
                break;

            case EntityEnums.movementType.npcWaypoints:
                NPC_Waypoints();
                break;
        }
    }

    void NPC_Idle()
    {
        if (_agent != null && _agent.isStopped == false)
        {
            _agent.isStopped = true;
            _agent.enabled = false;
        }
    }

    void NPC_Random()
    {
        if (_agent != null)
        {

        }

        else
            Debug.Log("<color=blue>AI Warning:</color> no NavmeshAgent attached!");
    }

    void NPC_Waypoints()
    {

    }




    //======================================================== Crowd Control
    public void CC_KnockedBack(float duration)
    {
        throw new System.NotImplementedException();
    }

    public void CC_KnockedDown(float duration)
    {
        throw new System.NotImplementedException();
    }

    public void CC_KnockedUp(float duration)
    {
        throw new System.NotImplementedException();
    }

    public void CC_Pinned(float duration, Transform pinningObject)
    {
        throw new System.NotImplementedException();
    }

    public void CC_Slowed(float duration, float ammount)
    {
        throw new System.NotImplementedException();
    }

    public void CC_Stunned(float duration)
    {
        throw new System.NotImplementedException();
    }
}
