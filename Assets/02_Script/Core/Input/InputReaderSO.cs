using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/InputReader")]
public class InputReaderSO : ScriptableObject, IPlayerActions
{
    public Vector2 MoveDirection { get; private set; }
    public event Action DashEvent;
    public event Action InterectionEvent;
    public event Action FocusPlayerEvent;
    public event Action FocusCoreEvent;

    private Controls _controls;

    private void OnEnable()
    {
        if(_controls == null)
        {
            _controls = new Controls();
        }

        _controls.Player.SetCallbacks(this);
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
        if(context.canceled) MoveDirection = Vector2.zero;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        DashEvent?.Invoke();
    }

    public void OnInterection(InputAction.CallbackContext context)
    {
        InterectionEvent?.Invoke();
    }

    public void OnFocusPlayer(InputAction.CallbackContext context)
    {
        FocusPlayerEvent?.Invoke();
    }

    public void OnFocusCore(InputAction.CallbackContext context)
    {
        FocusCoreEvent?.Invoke();
    }
}
