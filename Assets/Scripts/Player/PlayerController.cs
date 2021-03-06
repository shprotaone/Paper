using System;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class PlayerController : MonoCache
{
    [SerializeField] private float _speed;    
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _mainTransform;
    [SerializeField] private LayerMask _floorMask;
    [SerializeField] private GameStats _gameStats;

    private Camera _camera;
    private CharacterController _characterController;
    private RigBuilder _rigBuilder;
    private Weapon _currentWeapon;

    private float _horizontal;
    private float _vertical;

    #region properties

    public Vector3 ShootDirection { get; private set; }
    public float Horizontal { get { return _horizontal; } }
    public float Vertical { get { return _vertical; } }
    public bool PlayerIsDeath { get { return _gameStats.PlayerIsDeath; } }
    #endregion

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _currentWeapon = GetComponentInChildren<Weapon>();
        _rigBuilder = GetComponent<RigBuilder>();      
        _camera = Camera.main;
    }

    public override void OnTick()
    {
        if (!PlayerIsDeath && !_gameStats.GameInPause)
        {
            Movement();
            Aimining();
            Shooting();
        } 
        else if(PlayerIsDeath)
        {
            Death();
        }      
        ShootDirection = _target.transform.position;
    }

    private void Movement()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(_horizontal, 0, _vertical).normalized;
        _characterController.Move(movement * _speed * Time.deltaTime);
    }

    private void Shooting()
    {
        if(Input.GetMouseButton(0))
        _currentWeapon.Shooting();

        if (Input.GetKeyDown(KeyCode.R))
        _currentWeapon.ReloadWeapon();
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
        _rigBuilder.enabled = false;

        _currentWeapon.ReleasingWeapon();
    }
}
