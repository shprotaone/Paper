using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<int,bool> OnHealthChanged;

    [SerializeField] private float _speed;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _mainTransform;
    [SerializeField] private LayerMask _ignoreMask;
    [SerializeField] private LayerMask _floorMask;

    private CharacterController _characterController;
    private RigBuilder _rigBuilder;
    private Weapon _currentWeapon;
    private GameManager _gameManager;
    private int _health = 3;
    private bool _playerIsDeath;

    #region properties

    public Vector3 ShootDirection { get; private set; }
    public float InputX { get; set; }
    public float InputY { get; set; }
    public int Health { get { return _health; } }
    public bool PlayerIsDeath { get { return _playerIsDeath; } }

    #endregion

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _currentWeapon = GetComponentInChildren<Weapon>();
        _rigBuilder = GetComponent<RigBuilder>();

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
        float horizontal = Input.GetAxisRaw("Horizontal");
        InputY = horizontal;
        float vertical = Input.GetAxisRaw("Vertical");
        InputX = vertical;

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDir = transform.rotation * direction;
            _characterController.Move(moveDir * _speed * Time.deltaTime);
        }

        //transform.Translate(direction * _speed * Time.deltaTime);        ///ПОЧИНИТЬ ПОВОРОТ
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
