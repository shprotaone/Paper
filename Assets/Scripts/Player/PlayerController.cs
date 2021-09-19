using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject mainTransform;
    [SerializeField] private LayerMask ignoreMask;
    
    private RigBuilder _rigBuilder;
    private Weapon _currentWeapon;
    private float _health;
       
    #region properties

    public bool Shoot { get; set; }
    public bool Reload { get; set;}
    public bool DeathPlayer { get; set; }
    public float InputX { get; set; }
    public float InputY { get; set; }

    public float Health { get { return _health; } }
    
    #endregion

    private void Start()
    {
        _currentWeapon = GetComponentInChildren<Weapon>();
        _rigBuilder = GetComponent<RigBuilder>();
    }

    private void Update()
    {
        if (!DeathPlayer)
        {
            Movement();
            Aimining();
            Shooting();
            Reloading();
        }
        Death();
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        InputY = horizontal;
        float vertical = Input.GetAxisRaw("Vertical");
        InputX = vertical;

        Vector3 direction = new Vector3(horizontal, 0f, vertical);       
        transform.Translate(direction * _speed * Time.deltaTime);        
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
            target.transform.position = targetPoint + new Vector3(0, 1.3f, 0);       
        }
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
            _currentWeapon.Reloading();
        }
    }

    private void Death()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DeathPlayer = true;
            _rigBuilder.enabled = false;
            _currentWeapon.transform.SetParent(mainTransform.transform);
            _currentWeapon.ReleasingWeapon();
        }
    }
}
