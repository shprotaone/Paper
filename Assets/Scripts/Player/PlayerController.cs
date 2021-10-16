using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<int,bool> OnHealthChanged;

    [SerializeField] private float _speed;    
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _mainTransform;
    [SerializeField] private LayerMask _floorMask;

    private Camera _camera;
    private CharacterController _characterController;
    private RigBuilder _rigBuilder;
    private Weapon _currentWeapon;
    private GameManager _gameManager;
    private int _health = 3;
    private bool _playerIsDeath;

    private Vector3 _camForward;
    private Vector3 _move;
    private Vector3 _moveInput;

    private float _horizontal;
    private float _vertical;
    private float _forwardAmount;
    private float _turnAmount;

    #region properties

    public Vector3 ShootDirection { get; private set; }
    public float InputX { get { return _forwardAmount; } }
    public float InputY { get { return _turnAmount; } }
    public int Health { get { return _health; } }
    public bool PlayerIsDeath { get { return _playerIsDeath; } }

    #endregion

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _currentWeapon = GetComponentInChildren<Weapon>();
        _rigBuilder = GetComponent<RigBuilder>();
        _camera = Camera.main;

        if(_gameManager == null)
        {
            _gameManager = GameManager.instance;
        }
    }

    private void Update()
    {
        if (!PlayerIsDeath)
        {
            Movement();
            Aimining();
        }

        ShootDirection = _target.transform.position;
    }

    private void Movement()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
      
        MoveDirectionDetector(_move);

        Vector3 movement = new Vector3(_horizontal, 0, _vertical).normalized;
        _characterController.Move(movement * _speed * Time.deltaTime);
    }

    /// <summary>
    /// определение направления движения для анимации
    /// </summary>
    /// <param name="move"></param>
    private void MoveDirectionDetector(Vector3 move)
    {
        if (_camera.transform != null)
        {
            _camForward = Vector3.Scale(_camera.transform.up, new Vector3(1, 0, 1)).normalized;
            _move = _vertical * _camForward + _horizontal * _camera.transform.right;
        }
        else
        {
            _move = _vertical * Vector3.forward + _horizontal * Vector3.right;
        }

        if (_move.magnitude > 1)
        {
            move.Normalize();
        }

        this._moveInput = move;

        InverseAnimationInput();
    }

    /// <summary>
    /// инверсия данных для анимации
    /// </summary>
    private void InverseAnimationInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(_moveInput);
        _turnAmount = localMove.x;

        _forwardAmount = localMove.z;
    }

    private void Aimining()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(ray, out floorHit, 100f, _floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation,7f*Time.deltaTime);
        }
    }

    private void Death()
    {
        if (Health<=0)
        {
            _playerIsDeath = true;
            _gameManager.PlayerIsDeath = _playerIsDeath;
            _rigBuilder.enabled = false;
            _currentWeapon.transform.SetParent(_mainTransform.transform);
            _currentWeapon.ReleasingWeapon();
        }
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        if(OnHealthChanged != null)
        {
            Death();
            OnHealthChanged.Invoke(_health,false);
        }       
    }

    public void Healing(int heal)
    {
        if (_health < 3)
        {
            _health += heal;
            if (OnHealthChanged != null)
            {
                OnHealthChanged.Invoke(_health,true);
            }
        }
    }
}
