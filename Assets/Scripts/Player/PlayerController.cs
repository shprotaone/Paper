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

    private CharacterController _characterController;
    private RigBuilder _rigBuilder;
    private Weapon _currentWeapon;
    private int _health = 3;

    #region properties

    public Vector3 ShootDirection { get; private set; }
    public bool DeathPlayer { get; set; }
    public float InputX { get; set; }
    public float InputY { get; set; }
    public int Health { get { return _health; } }
    
    #endregion

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _currentWeapon = GetComponentInChildren<Weapon>();
        _rigBuilder = GetComponent<RigBuilder>();
    }

    private void Update()
    {
        if (!DeathPlayer)
        {
            Movement();
            Aimining();
        }
        Death();

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

        transform.Translate(direction * _speed * Time.deltaTime);        ///ПОЧИНИТЬ ПОВОРОТ
    }

    private void Aimining()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);  //высчитывает точку
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);        //луч для точки

        float hitDist = 0f;

        if (playerPlane.Raycast(ray, out hitDist))  //отдает дистанцию до точки
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);    //координаты куда идти
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);  //координаты к повороту к точке к которой нужно идти
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);  //изменение координат поворота
            _target.transform.position = targetPoint + new Vector3(0, 1.3f, 0);           
        }
        Debug.DrawRay(ray.origin, ray.direction, Color.red,1000f);
    }

    private void Death()    ///??Куда то надо перенести
    {
        if (Health<=0)
        {
            DeathPlayer = true;
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
