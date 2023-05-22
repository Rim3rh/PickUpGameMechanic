using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class CharacterController : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerInputActions playerInputActions;
    public Transform orientation;
    private Vector3 _moveDir;
    private Vector2 _moveInput;
    public Animator _playerAnim;
    public GameObject _optionsMenu, _resumeButton;

    void Awake()
    {
        Cursor.visible = false;
        _optionsMenu.SetActive(false);
        _rb = GetComponent<Rigidbody>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMov.Enable();
        _rb.drag = 6f;
    }
    void Update()
    {
        Animations();
        //Get new input system inputs
        _moveInput = playerInputActions.PlayerMov.Movement.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
       Movement();
    }
    private void Animations()
    {
        if (_moveInput != Vector2.zero) _playerAnim.SetBool("Moving", true);
        else _playerAnim.SetBool("Moving", false);
    }
    private void Movement()
    {
        if (GameManager.Instance._canMove)
        {
            this.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            _moveDir = orientation.forward * _moveInput.y + orientation.right * _moveInput.x;
            _rb.velocity = _moveDir.normalized * GameManager.Instance._speed;
            //Make Character stop if there is no input;
            if (_moveInput == Vector2.zero) _rb.velocity = Vector3.zero;
        }
    }

}