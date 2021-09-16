using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _minAngle = 5.0f;
    [SerializeField] private float _maxAngle = 45.0f;
    [SerializeField] private float _minDistance = 5.0f;
    [SerializeField] private float _maxDistance = 45.0f;

    //Тестовый скрипт на камеру
    public static CameraController Instance { get; set; }

    public Camera gameplayCamera;

    public CinemachineVirtualCamera Camera { get; protected set; }

    protected float m_currentDistance = 1.0f;
    protected CinemachineFramingTransposer m_FramingTransporer;

    private void Awake()
    {
        Instance = this;
        Camera = GetComponent<CinemachineVirtualCamera>();
        m_FramingTransporer = Camera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    ///есть зум, возможно добавить позже
}
