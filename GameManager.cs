using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float _playerHealth, _playerFood, _playerOxygen, _tank1OxygenLevel, _tank2OxygenLevel;
    public float timer, timer2;
    public int _speed;
    public bool _holdingMainTank, _holdingFood, _oxygenCharging;
    public bool _gear;
    public bool _holdingShovel, _insideDiggingHole;
    public bool _holdingSecondaryTank;
    public GameObject _item;
    public bool _disable;
    public float _timer;
    public int _easterEggCounter;
    public bool _canMove;
    public bool _insideHouse;
    public bool _holdingFlag;
    public int _dirt1, _dirt2, _dirt3;
    public float _musicVolume, _sfxVolume;
    public bool _firstTimeChangeTank;
    public int _FixedParts;
    public bool _canLeave;
    public int _skipFirstMision;
    public bool _openedMenu;
    public AudioSource _music;
    public bool _holdingRepairPart;
    public bool _replaceWithGoodTank;
    public bool _material;
    public bool _flagPlaced;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        Cursor.visible = false;
        _musicVolume = 0.4f;
        _sfxVolume = 50;
        _insideHouse = true;
        _canMove = true;
        _gear = false;
        _disable = false;
        timer = 0.5f;
        timer2 = 0.5f;
        _speed = 6;
        _tank1OxygenLevel = 100;
        _tank2OxygenLevel = 0;
        _playerHealth = 100;
        _playerFood = 100;
        _playerOxygen = 100;
        _holdingSecondaryTank = false;
        _holdingMainTank = true;
        _holdingShovel = false;
    }
    public void Update()
    {
        _music.volume = _musicVolume;
    }


}
