using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObject/InputReader")]
public class GameInputReaderSO : ScriptableObject, GameInput.IInputActions
{
    private GameInput _gameInput;

    public event Action ClickEvent;
    public event Action TouchEvent;
    public void Initialization()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();
            _gameInput.Input.SetCallbacks(this);
            SetGameplay();
        }
        Debug.Log($"{this} is Initialized");
    }

    private void SetGameplay() => _gameInput.Input.Enable();

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            ClickEvent?.Invoke();
        }
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            TouchEvent?.Invoke();
        }
    }
}
