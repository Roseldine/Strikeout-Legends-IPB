

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyAttackController))]
public class Enemy : MonoBehaviour, IDamagable
{
    public enum enemyState { idle, wonder, aim, attack, dead }

    [Header("Enemy Components")]
    [SerializeField] Collider _collider;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] Health _health;

    [Header("Enemy Variables")]
    [SerializeField] float _speed;
    [SerializeField] float _speedMultiplier;
    [SerializeField] float _lookSpeed;
    [SerializeField] Transform _player;
    [SerializeField] Lean.Pool.LeanGameObjectPool _pool;
    Vector3 _targetPos;

    [Header("Attack Variables")]
    [SerializeField] EnemyAttackController _attackController;

    [Header("Graphics & Animation")]
    [SerializeField] Transform _graphic;
    [SerializeField] Animator _animator;
    [Tooltip("Idle, Walking, Attacking")]
    [SerializeField] string[] _animatorBools;

    [Header("AI variables")]
    [SerializeField] UnityEngine.AI.NavMeshAgent _agent;
    [SerializeField] float _randomRadius;
    [SerializeField] float _minDistance;

    [Header("States")]
    [SerializeField] enemyState _state;
    [Tooltip("Idle, Wonder, Aim, Attack, Death")]
    public EnemyState[] _enemyStates;

    public Lean.Pool.LeanGameObjectPool Pool { get { return _pool; } set { _pool = value; } }


    private void OnEnable()
    {
        _state = enemyState.idle;
        _health.ResetHealth();
    }


    void Start()
    {
        _agent.speed = _speed * _speedMultiplier;
        _agent.updateRotation = false;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        EnemyStateMachine();
    }



    //================================================ States
    #region States
    public virtual void EnemyStateMachine()
    {
        switch(_state)
        {
            case enemyState.idle:
                StateIdle();
                break;

            case enemyState.wonder:
                StateWonder();
                break;

            case enemyState.aim:
                StateAim();
                break;

            case enemyState.attack:
                StateAttack();
                break;
        }
    }


    //--------------------------- State Methods
    public virtual void StateIdle()
    {
        if (_animator != null && GetAnimation(_animatorBools[0]) == false)
        {
            StopAgent();
            SetAnimation(_animatorBools[0]);
        }
    }


    public virtual void StateWonder()
    {
        // if agent not walking, make him walk
        if (_agent.enabled == false)
        {
            if (_animator != null && GetAnimation(_animatorBools[1]) == false)
            {
                StartAgent(RandomWonderPos());
                SetAnimation(_animatorBools[1]);
                StartCoroutine(CR_Wonder());
            }
        }        

        // if agent reached his destination, then aim to attack
        else
        {
            if (_agent.hasPath == true)
                AgentLookAtPath();

            else
                _state = enemyState.aim;
        }
    }

    IEnumerator CR_Wonder()
    {
        float t = 0;

        while (t < _enemyStates[1]._duration)
        {
            
            t += Time.deltaTime;
            yield return null;
        }

        _state = enemyState.aim;
    }



    public virtual void StateAim()
    {
        // play aim animation
        if (_animator != null && GetAnimation(_animatorBools[0]) == false)
        {
            SetAnimation(_animatorBools[0]);
            StopAgent();
            StartCoroutine(CR_Aim());
        }
    }

    IEnumerator CR_Aim()
    {
        float t = 0;

        while(t < _enemyStates[2]._duration)
        {
            SmoothLookAt(_graphic, _player.position);
            t += Time.deltaTime;
            yield return null;
        }

        _state = enemyState.attack;
    }



    public virtual void StateAttack()
    {
        if (_animator != null && GetAnimation(_animatorBools[2]) == false)
        {
            SetAnimation(_animatorBools[2]);
            //StartCoroutine(CR_Attack());
        }

        SmoothLookAt(_graphic, _player.position);

        if (_animator != null)
        {
            AnimatorStateInfo _animStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (_animStateInfo.IsTag("attack"))
            {
                if (_animStateInfo.normalizedTime > .45f && _animStateInfo.normalizedTime < .5f)
                    _attackController.Attack();

                if (_animStateInfo.normalizedTime >= 1)
                    _state = enemyState.wonder;
            }
        }
    }

    IEnumerator CR_Attack()
    {
        float t = 0;

        while (t < _enemyStates[3]._duration)
        {
            
            t += Time.deltaTime;
            yield return null;
        }
        Debug.Log("<color=orange>Attack Warnign: </color> Enemy has Attacked!");

        _state = enemyState.wonder;
    }

    public void ChangeState(enemyState state) => _state = state;





    //================================================ State Helper Methods
    #region State Methods
    //--------------------------- Navmesh Agent
    protected void StopAgent()
    {
        if (_agent != null && _agent.enabled)
        {
            _agent.isStopped = true;
            _agent.velocity = Vector3.zero;
            _agent.ResetPath();
            _agent.enabled = false;
        }
    }

    protected void StartAgent(Vector3 targetPos)
    {
        if (_agent != null)
        {
            if (_agent.enabled == false)
                _agent.enabled = true;

            _agent.isStopped = false;

            if (_agent.hasPath == false)
                _agent.SetDestination(targetPos);
        }
    }


    protected Vector3 RandomWonderPos()
    {
        float _dist = 0;

        do
        {
            float x = Random.Range(2f, 8f) * -1;
            float z = Random.Range(-7f, 7.25f);
            _targetPos = new Vector3(x, 0, z);
            _dist = Vector3.Distance(transform.position, _targetPos);

        } while (_dist < _minDistance);

        
        return _targetPos;
    }



    //--------------------------- Graphic Look Rotation
    protected void AgentLookAtPath()
    {
        if (_agent.velocity != Vector3.zero)
        {
            Quaternion _rot = Quaternion.LookRotation(_agent.velocity.normalized);
            _graphic.rotation = Quaternion.Slerp(_graphic.rotation, _rot, Time.deltaTime * _lookSpeed);
        }
    }

    protected void SmoothLookAt(Transform objectToRotate, Vector3 target)
    {
        Vector3 _dir = (target - objectToRotate.position).normalized;
        Quaternion _rot = Quaternion.LookRotation(_dir);
        objectToRotate.rotation = Quaternion.Slerp(objectToRotate.rotation, _rot, Time.deltaTime * _lookSpeed);
    }



    //--------------------------- Animation
    protected void SetAnimation(string animBool)
    {
        for (int i = 0; i < _animatorBools.Length; i++)
            _animator.SetBool(_animatorBools[i], false);

        _animator.SetBool(animBool, true);
    }

    protected bool GetAnimation(string animBool) => _animator.GetBool(animBool);
    #endregion // State methods

    #endregion // States





    //================================================ Health, Damage, Healing & Death
    #region Health, Damage, Healing & Death
    void StartHealth()
    {
        
    }

    public void Damage(int ammount)
    {
        _health.Damage(ammount);
        if (_health.EntityHealth <= 0)
            EnemyManager.Instance.DespawnEnemy(GetComponent<Enemy>());
    }

    public void Heal(int ammount)
    {

    }

    public void Death()
    {

    }

    #endregion





    //================================================ CC effects
    #region CC efects
    public void CC_Pinned()
    {
        ChangeState(enemyState.idle);
        StopAgent();
    }

    public void CC_KockUp()
    {
        ChangeState(enemyState.idle);
        StopAgent();
    }

    #endregion


    //================================================ Debuggin
    #region Debug
    private void OnDrawGizmosSelected()
    {
        // Display Random Wonder Radius

        if (_agent.enabled == true && _agent.hasPath)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(_agent.pathEndPosition, .5f);

            UnityEditor.Handles.Label(transform.position + Vector3.left, _agent.remainingDistance.ToString());
        }
    }

    #endregion
}
