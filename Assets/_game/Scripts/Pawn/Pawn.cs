using UnityEngine;
using _game.StateMachine;
public class Pawn : MonoBehaviour
{
    [SerializeField] private Configuration _configuration;
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


    public Configuration Configuration
    {
        get
        {
            return _configuration;
        }

        set
        {
            if (value == null)
            {
                return;
            }
            else
            {
                _configuration = value;
            }
        }
    }

    private void Awake()
    {
        _prepareAttackSprite.gameObject.SetActive(false);
        _attackSprite.gameObject.SetActive(false);
        _switchWeaponSprite.gameObject.SetActive(false);

        Configuration.MeleeAttack = true;
        Configuration.SwitchWeaponTime = 2f;

        _stateMachine = new StateMachine();
        _prepareAttackState = new PrepareAttackState(this, _stateMachine);
        _attackState = new AttackState(this, _stateMachine);
        _entryState = new EntryState(this, _stateMachine);
        _switchWeaponState = new SwitchWeaponState(this, _stateMachine);
    }

    private void Start() => _stateMachine.Initialize(_entryState);
    private void Update() => _stateMachine.CurrentState.Update();
    private void FixedUpdate() => _stateMachine.CurrentState.FixedUpdate();
    private void LateUpdate() => _stateMachine.CurrentState.LateUpdate();
}