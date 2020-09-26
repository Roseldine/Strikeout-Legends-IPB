

using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Graphic & Animation")]
    [SerializeField] Transform _graphic;
    [SerializeField] Animator _animator;
    [SerializeField] float _lookSpeed;
    [SerializeField] float _attackTimeStamp;
    [SerializeField] float _attackStop;
    [SerializeField] string[] _animBools;
    [SerializeField] string[] _animTags;

    public void AnimationsUpdate()
    {
        if (PlayerManager.Instance != null)
        {
            RotateGraphic();
            AnimationStateMachine();
        }
    }


    //================================================== Animation
    void AnimationStateMachine()
    {
        //Debug.Log(PlayerManager.Instance.isMoving);

        // idle/running
        switch (PlayerManager.Instance.isMoving)
        {
            case false:
                CheckAndSetAnim(0, _animBools[0]);
                break;

            case true:
                CheckAndSetAnim(0, _animBools[1]);
                break;
        }

        // charging
        switch (PlayerManager.Instance.isCharging)
        {
            case false:
                SetChargeAnimation(false);
                break;

            case true:
                SetChargeAnimation(true);
                break;
        }

        if (PlayerManager.Instance.isAttacking)
        {
            var _animInfo =_animator.GetCurrentAnimatorStateInfo(0);
            bool isInAttack = _animInfo.IsTag(_animTags[2]);

            // spawn attack
            if (isInAttack && _animInfo.normalizedTime > _attackTimeStamp)
                PlayerManager.Instance.Attack();

            // stop attack
            if (isInAttack && _animInfo.normalizedTime > _attackStop)
                PlayerManager.Instance.AttackStop();
        }
    }

    /// <summary>
    /// idle/moving, charge, attack
    /// </summary>
    void CheckAndSetAnim(int id, string animBool)
    {
        switch (id)
        {
            case 0: // idle/moving
                if (IsAnimTrue(animBool) == false)
                    SetMovingAnimation(animBool);
                break;

            case 1: // charging
                if (IsAnimTrue(animBool) == false)
                    SetChargeAnimation(!IsAnimTrue(animBool));
                break;

            case 2: // attacking
                if (IsAnimTrue(animBool) == false)
                    SetAttackAnimation(!IsAnimTrue(animBool));
                break;

            default:
                Debug.Log("Something wrong with anim Check");
                break;
        }
    }

    void SetMovingAnimation(string animBool)
    {
        for (int i = 0; i < 2; i++)
            _animator.SetBool(_animBools[i], false);

        _animator.SetBool(animBool, true);
    }

    public void SetChargeAnimation(bool val) => _animator.SetBool(_animBools[2], val);
    public void SetAttackAnimation(bool val) => _animator.SetBool(_animBools[3], val);
    bool IsAnimTrue(string animBool) => _animator.GetBool(animBool);



    //================================================== Rotation
    void RotateGraphic()
    {
        bool _isMoving = PlayerManager.Instance.isMoving;
        bool _isAttacking = PlayerManager.Instance.isAttacking;
        Quaternion _rot = PlayerManager.Instance.PlayerRotation;

        if (_isMoving && _isAttacking == false)
            _graphic.rotation = Quaternion.Slerp(_graphic.rotation, _rot, _lookSpeed * Time.deltaTime);
    
        else
        {
            if (_isAttacking)
            {
                var _dir = PlayerManager.Instance.AimTarget - transform.position;
                var rot = Quaternion.LookRotation(_dir);
                _graphic.rotation = Quaternion.Slerp(_graphic.rotation, rot, 2 * _lookSpeed * Time.deltaTime);
            }
        }
    }





    //================================================== Setters
    public void SetCharacter(Transform character)
    {
        _graphic = character;
        _animator = _graphic.GetComponent<Animator>();
    }
}
