using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;

    private int _idle = Animator.StringToHash("Idle");
    private int _shootingIdle = Animator.StringToHash("ShootingIdle");
    private int _reloadingIdle = Animator.StringToHash("ReloadIdle");
    private int _moveX = Animator.StringToHash("MoveX");
    private int _moveZ = Animator.StringToHash("MoveZ");
    private int _shootingRun = Animator.StringToHash("ShootingRun");
    private int _reloadRun = Animator.StringToHash("ReloadRun");

    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        _animator.SetFloat(_moveX, _playerController.InputX);
        _animator.SetFloat(_moveZ, _playerController.InputY);
    }
}
