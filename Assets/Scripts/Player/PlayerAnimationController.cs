using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;

    private int _moveX = Animator.StringToHash("MoveX");
    private int _moveY = Animator.StringToHash("MoveY");
    private int _death = Animator.StringToHash("Death");

    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        _animator.SetFloat(_moveX,_playerController.InputX);
        _animator.SetFloat(_moveY, _playerController.InputY);
        _animator.SetBool(_death, _playerController.DeathPlayer);
        
    }
}
