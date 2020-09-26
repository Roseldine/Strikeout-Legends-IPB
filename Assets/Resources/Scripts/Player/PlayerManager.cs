
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(PlayerAim))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCollisions))]
public class PlayerManager : MonoBehaviour
{
    [Header("Singleton")]
    public static PlayerManager Instance;    


    [Header("Character Database")]
    [SerializeField] SOCharacterDictionary _characters;
    [SerializeField] SOCharacter _char;
    [Range(0, 3)] [SerializeField] int _charId;
    [SerializeField] Transform _player;
    Transform _playerGraphic;
    Transform _attackSpawner;

    [Header("Player Components")]
    [SerializeField] Health _health;
    [SerializeField] Energy _energy;

    [SerializeField] PlayerMovement _movement;
    [SerializeField] PlayerAnimationController _animations;
    [SerializeField] PlayerAttackController _attacks;
    [SerializeField] PlayerAim _aim;

    [SerializeField] EffectEnum.statusEffect _statusEffects;


    //===== Variable Properties
    #region Variable Properties
    public int PlayerHealth { get { return _health.PlayerHealth; } }
    public int PlayerEnergy { get { return _energy.PlayerEnergy; } }

    public bool isMoving { get { return _movement.isMoving; } }
    public bool isCharging { get { return _aim.isCharging; } }
    public bool isSpecial { get { return _aim.isSpecial; } }
    public bool isAttacking { get { return _aim.isAttacking; } }

    public Vector3 PlayerDirection { get { return _movement.Direction; } }
    public Quaternion PlayerRotation { get { return _movement.Rotation; } }
    public Vector3 AimPosition { get { return _aim.AimPosition; } }
    public Vector3 AimDirection { get { return _aim.AimDirection; } }
    public Vector3 AimTarget { get { return _aim.AimTarget; } }

    public Transform Player { get { return _player; } }
    public Transform PlayerGraphic { get { return _playerGraphic; } }
    public Transform AttackSpawner { get { return _attackSpawner; } }

    #endregion



    private void Awake()
    {
        Instance = GetComponent<PlayerManager>();
        _char = _characters.characters[_charId];
        SpawnPlayer();
    }


    private void Update()
    {
        PlayerUpdate();
    }

    public void PlayerUpdate()
    {
        _movement.MovementUpdate();
        _animations.AnimationsUpdate();
        _aim.AimUpdate();
    }


    //================================================== Movement & Aiming
    public void Movement()
    {

    }


    //================================================== Attacks
    public void AttackStart()
    {
        _animations.SetAttackAnimation(true);
        Debug.Log("<color=green>Attack: Started</color>");
    }

    public void AttackStop()
    {
        _aim.ResetCharge();
        _animations.SetAttackAnimation(false);
        Debug.Log("<color=red>Attack: Stopped</color>");
    }

    public void Attack()
    {
        _attacks.SpawnAttack();        
    }


    //================================================== Health & Energy
    public void Damage(int ammount)
    {
        _health.Damage(ammount);
    }

    public void EnergyUse(int ammount)
    {
        
    }


    public void EnergyCharge(float value) => _energy.EnergyCharge.fillAmount = value;
    public void EnergyChargeStop() => _energy.ChargeStop();



    //================================================== Spawn Player
    void SpawnPlayer()
    {
        _playerGraphic = Instantiate(_char.characterModel, transform).transform;
        _health.SetHealth(_char.health);
        _energy.SetEnergy(_char.energy);

        _animations.SetCharacter(_playerGraphic);

        var _basic = _char.basicAttack;
        //_basic.GetComponent<PlayerAttack>().Damage = _char.basicDamage;

        var _special = _char.specialAttack;
        //_special.GetComponent<PlayerAttack>().Damage = _char.basicDamage;

        _attacks.SetAttackPools(_basic, _special);
        _attackSpawner = _playerGraphic.GetComponent<CharacterAnimEvents>().projectileSpawner;
        _attacks.SpecialSpawnPlace = _char.specialSpawnPlace;
    }
}
