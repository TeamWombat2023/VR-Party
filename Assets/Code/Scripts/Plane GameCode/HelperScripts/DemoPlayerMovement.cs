using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Because of this requirement, adding the script adds the component as well!!
[RequireComponent(typeof(CharacterController))]
public class DemoPlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerControls;
    private InputAction _movement;
    private CharacterController _characterController;

    private Vector3 _moveVector;

    [SerializeField] private float _speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        //Ilgılı action mapi yerlestirdikten sonra gerekli ayarları böle getiririz.
        var gameplayActionMap = _playerControls.FindActionMap("Default");
        _movement = gameplayActionMap.FindAction("Move");

        //Every time an update happens on the movement, its subscribed events will be called.
        _movement.performed += OnMovementChanged;
        _movement.canceled += OnMovementChanged;
        //Callbackleri cagirabilmesi icin.
        _movement.Enable();
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _characterController.Move(_moveVector * (_speed * Time.fixedDeltaTime));

    }

    public void OnMovementChanged(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        _moveVector = new Vector3(direction.x, 0, direction.y);


    }
}
