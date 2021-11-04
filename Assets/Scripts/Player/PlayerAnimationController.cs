using UnityEngine;

public class PlayerAnimationController : MonoCache
{
    private Animator _animator;
    private PlayerController _playerController;
    private AudioSource _stepSound;

    private Camera _camera;
    private float _forwardAmount;
    private float _turnAmount;

    private Vector3 _camForward;
    private Vector3 _move;
    private Vector3 _moveInput;

    private int _moveX = Animator.StringToHash("MoveX");
    private int _moveY = Animator.StringToHash("MoveY");
    private int _death = Animator.StringToHash("Death");

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerController = GetComponent<PlayerController>();
        _stepSound = GetComponent<AudioSource>();
        _camera = Camera.main;
    }

    public override void OnTick()
    {
        _animator.SetFloat(_moveX, _forwardAmount);
        _animator.SetFloat(_moveY, _turnAmount);
        _animator.SetBool(_death, _playerController.PlayerIsDeath);
        MoveDirectionDetector(_move);
    }

    private void Step(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5)
        {
            _stepSound.PlayOneShot(_stepSound.clip);
        }
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
            _move = _playerController.Vertical * _camForward + _playerController.Horizontal * _camera.transform.right;
        }
        else
        {
            _move = _playerController.Vertical * Vector3.forward + _playerController.Horizontal * Vector3.right;
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
}
