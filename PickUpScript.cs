using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PickUpScript : MonoBehaviour
{
    [SerializeField] private GameObject _pickUpSlot, _pickUpSlot2, _UiManager;
    public static bool _isHolding, _timerRuning;
    public static float timer, timer2;
    private GameObject _itemTem;
    private PlayerInputActions playerInputActions;
    public Animator _playerAnim;

    void Start()
    {
        timer2 = 0.5f;
        timer = 0f;
        _isHolding = false;
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerMov.Interact.started += Interact_started;
        playerInputActions.PlayerMov.Enable();
    }

    private void Interact_started(InputAction.CallbackContext context)
    {
        //Check if menu is open so u cant grab/drop with menu open.
        if (!GameManager.Instance._openedMenu)
        {
            if (_itemTem != null)
            {
                //Grab
                if (!_isHolding && timer <= 0 && !GameManager.Instance._oxygenCharging)
                {
                    if (!GameManager.Instance._holdingSecondaryTank && !_itemTem.CompareTag("Oxigeno2"))
                    {
                        StartCoroutine(GrabC());
                    }
                }
                //Drop
                if (_isHolding && timer2 <= 0 && !GameManager.Instance._insideDiggingHole)
                {
                    if (_itemTem.CompareTag("Oxigeno"))
                    {
                        GameManager.Instance._holdingFood = false;
                    }
                    _itemTem = null;
                    GameManager.Instance._item = _itemTem;
                    _isHolding = false;
                    GameManager.Instance._holdingRepairPart = false;
                    timer = 0.5f;
                    GameManager.Instance._speed = 6;
                    _timerRuning = true;
                }
            }

        }
        
    }

    void Update()
    {
        //Set the position of the object depending on its tag.
        if (_isHolding)
        {
            _playerAnim.SetBool("Grabing", true);
            string _tag;
            _tag = GameManager.Instance._item.tag;
            switch (_tag)
            {
                case "RepairPart1":
                    GameManager.Instance._holdingRepairPart = true;
                    GameManager.Instance._item.transform.position = new Vector3(_pickUpSlot.transform.position.x, _pickUpSlot.transform.position.y + 2, _pickUpSlot.transform.position.z);
                    break;
                case "RepairPart2":
                    GameManager.Instance._holdingRepairPart = true;
                    GameManager.Instance._item.transform.position = _pickUpSlot.transform.position;
                    GameManager.Instance._item.transform.rotation = _pickUpSlot.transform.rotation;
                    break;
                case "RepairPart3":
                    GameManager.Instance._holdingRepairPart = true;
                    GameManager.Instance._item.transform.position = new Vector3(_pickUpSlot2.transform.position.x, _pickUpSlot2.transform.position.y + 2, _pickUpSlot2.transform.position.z);
                    break;
                case "Shovel":
                    GameManager.Instance._holdingRepairPart = false;
                    GameManager.Instance._item.transform.position = new Vector3(_pickUpSlot.transform.position.x, _pickUpSlot.transform.position.y, _pickUpSlot.transform.position.z);
                    break;
                case "Flag":
                    GameManager.Instance._holdingRepairPart = false;
                    GameManager.Instance._item.transform.position = _pickUpSlot.transform.position;
                    break;
                default:
                    GameManager.Instance._holdingRepairPart = false;
                    GameManager.Instance._item.transform.position = _pickUpSlot.transform.position;
                    GameManager.Instance._item.transform.rotation = _pickUpSlot.transform.rotation;
                    break;
            }
            timer2 -= Time.deltaTime;
        }else _playerAnim.SetBool("Grabing", false);
        if (_timerRuning) timer -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && !_isHolding)
        {
            _itemTem = other.gameObject;
        }
        if (other.gameObject.CompareTag("Oxigeno") && !GameManager.Instance._holdingMainTank && !_isHolding)
        {
            _itemTem = other.gameObject;
        }
        if (other.gameObject.CompareTag("Oxigeno2") && !GameManager.Instance._holdingSecondaryTank && !_isHolding)
        {
            _itemTem = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6 && !_isHolding)
        {
            _itemTem = null;
        }
        if (other.gameObject.CompareTag("Oxigeno") && !GameManager.Instance._holdingMainTank && !_isHolding)
        {
            _itemTem = null;
        }
        if (other.gameObject.CompareTag("Oxigeno2") && !GameManager.Instance._holdingSecondaryTank && !_isHolding)
        {
            _itemTem = null;
        }
    }
    IEnumerator GrabC()
    {
        yield return null;
        if (!GameManager.Instance._disable)
        {
            _timerRuning = false;
            GameManager.Instance._item = _itemTem;
            _isHolding = true;
            timer2 = 0.5f;
            GameManager.Instance._speed = 4;
        }
    }

}
