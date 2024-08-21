using UnityEngine;
using _game.StateMachine;
public class Pawn : MonoBehaviour
{
    [SerializeField] private Configuration _configuration;
    [SerializeField] internal protected Animator _fightIndicatorAnimator;
    [SerializeField] internal protected Animator _pawnAnimator;
    internal protected StateMachine _stateMachine;
    internal protected PrepareAttackState _prepareAttackState;
    internal protected AttackState _attackState;
    internal protected EntryState _entryState;
    public GameObject _prepareAttackSprite;
    public GameObject _attackSprite;
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
        _stateMachine = new StateMachine();
        _prepareAttackState = new PrepareAttackState(this, _stateMachine);
        _attackState = new AttackState(this, _stateMachine);
        _entryState = new EntryState(this, _stateMachine);
    }

    private void Start() => _stateMachine.Initialize(_entryState);
    private void Update() => _stateMachine.CurrentState.Update();
    private void FixedUpdate() => _stateMachine.CurrentState.FixedUpdate();
    private void LateUpdate() => _stateMachine.CurrentState.LateUpdate();
}