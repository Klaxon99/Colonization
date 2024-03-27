using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Input _playerInput;

    private void Awake()
    {
        _playerInput = new Input();

        _playerInput.Player.Click.performed += OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log(1);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}