using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector3> ClickedOnSpawnPlace;
    public event Action<Base> ClickedOnBase;

    private Input _playerInput;

    private void Awake()
    {
        _playerInput = new Input();

        _playerInput.Player.Click.performed += OnClick;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 mouseClickPosition = _playerInput.Player.MouseMove.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(mouseClickPosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.TryGetComponent(out SpawnPlace spawnPlace))
                {
                    ClickedOnSpawnPlace?.Invoke(hitInfo.point);
                }

                if (hitInfo.collider.TryGetComponent(out Base unitBase))
                {
                    ClickedOnBase?.Invoke(unitBase);
                }
            }
        }
    }
}