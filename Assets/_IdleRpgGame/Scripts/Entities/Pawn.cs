using UnityEngine;
using IdleGame.StateMachine;

public class Pawn : MonoBehaviour
{
    [SerializeField] private Weapon _startWeapon;
    [SerializeField] private Armor _startArmor;
    [SerializeField] private PawnConfiguration _configuration;
    [SerializeField] internal protected Animator _fightIndicatorAnimator;
    [SerializeField] internal protected Animator _pawnAnimator;
    [SerializeField] internal protected GameObject _prepareAttackSprite;
    [SerializeField] internal protected GameObject _switchWeaponSprite;
    [SerializeField] internal protected GameObject _attackSprite;
    internal protected StateMachine _stateMachine;
    internal protected PrepareAttackState _prepareAttackState;
    internal protected SwitchWeaponState _switchWeaponState;
    internal protected AttackState _attackState;
    internal protected EntryState _entryState;
    public Transform _pawnTransform;

    public PawnConfiguration PawnConfiguration
    {
        get
        {
            return _configuration;
        }

        set
        {
            _configuration = value;
        }
    }

    private void OnDisable()
    {
        _stateMachine.ChangeState(_entryState);
        SetupConfiguration();
    }

    private void Awake()
    {
        _prepareAttackSprite.gameObject.SetActive(false);
        _attackSprite.gameObject.SetActive(false);
        _switchWeaponSprite.gameObject.SetActive(false);
        SetupConfiguration();
        SetupStateMachine();
    }

    private void Update() => _stateMachine.CurrentState.Update();
    private void FixedUpdate() => _stateMachine.CurrentState.FixedUpdate();
    private void LateUpdate() => _stateMachine.CurrentState.LateUpdate();
    private void SetupStateMachine()
    {
        _stateMachine = new StateMachine();
        _prepareAttackState = new PrepareAttackState(this, _stateMachine);
        _attackState = new AttackState(this, _stateMachine);
        _entryState = new EntryState(this, _stateMachine);
        _switchWeaponState = new SwitchWeaponState(this, _stateMachine);
        _stateMachine.Initialize(_entryState);
    }
    private void SetupConfiguration()
    {
        PawnConfiguration.MeleeAttack = true;
        PawnConfiguration._currentArmor = _startArmor;
        PawnConfiguration._currentWeapon = _startWeapon;
        PawnConfiguration.CurrentHealthValue = PawnConfiguration.MaxHealthValue;
        PawnConfiguration.AttackTime = PawnConfiguration._currentWeapon.AttackSpeed;
        PawnConfiguration.CurrentAttackDamage = PawnConfiguration._currentWeapon.DamageValue + PawnConfiguration.PawnDamage;
        PawnConfiguration.CurrentHealthValue = PawnConfiguration.StartHealthValue;
    }
}