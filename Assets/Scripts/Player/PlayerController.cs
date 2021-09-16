using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject target;

    private CharacterController _characterController;
    private Weapon _currentWeapon;
    private Vector3 _mousePosition;

    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;
    private float directionY;
    
    #region properties

    public bool Stay { get; set; }
    public bool Run { get; set; }
    public bool Shoot { get; set; }
    public bool Reload { get; set;}
    public float InputX { get; set; }
    public float InputY { get; set; }

    #endregion

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _currentWeapon = GetComponent<Weapon>();
    }

    private void Update()
    {        
        Movement();
        Rotation();
        Shooting();
        Reloading();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        InputX = horizontal;
        float vertical = Input.GetAxisRaw("Vertical");
        InputY = vertical;
        print(horizontal);

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        //нужно переделать бег. Бегает строго по направлению курсора. 
        if(direction.magnitude >= 0.1f)
        {
            Run = true;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + transform.rotation.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDir * _speed * Time.deltaTime);
        }
        else
        {
            Run = false;
            Stay = true;
        }       
    }
    private void Rotation()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out RaycastHit raycastHit))
        {
            _mousePosition = raycastHit.point;

        }

        target.transform.position = _mousePosition;
    }
    private void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot = true;
            _currentWeapon.Shooting();
        }
        else
        {
            Shoot = false;
        }
    }
    private void Reloading()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload = true;
            _currentWeapon.Reloading();
        }
        else
        {
            Reload = false;
        }
    }


}
