using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _hearts;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Weapon _weapon;

    [SerializeField] private RectTransform _ammoView;
    [SerializeField] private Vector3 _offsetAmmoView;
    [SerializeField] private Camera _cam;
    [SerializeField] private Text _capacityAmmoText;

    private void Update()
    {
        DrawLifes();
        DrawAmmo();
    }

    private void DrawLifes()
    {
        if (_playerController.Health < 1)
        {
            _hearts[0].SetActive(false);
        }
        else if (_playerController.Health < 2)
        {
            _hearts[1].SetActive(false);
        }
        else if (_playerController.Health < 3)
        {
            _hearts[2].SetActive(false);
        }
    }

    private void DrawAmmo()
    {
        Vector3 pos = Input.mousePosition + _offsetAmmoView;
        _ammoView.position = _cam.ScreenToWorldPoint(pos);

        _capacityAmmoText.text = _weapon.CapacityAmmo.ToString();
    }

    //private void OnGUI()
    //{
    //    Vector3 point = new Vector3();
    //    Event currentEvent = Event.current;
    //    Vector2 mousePos = new Vector2();

    //    mousePos.x = currentEvent.mousePosition.x;
    //    mousePos.y = _cam.pixelHeight - currentEvent.mousePosition.y;

    //    point = _cam.ScreenToViewportPoint(new Vector3(mousePos.x, mousePos.y, _cam.nearClipPlane));

    //    GUILayout.BeginArea(new Rect(20, 20, 250, 120));
    //    GUILayout.Label("Screen pixels: " + _cam.pixelWidth + ":" + _cam.pixelHeight);
    //    GUILayout.Label("Mouse position: " + mousePos);
    //    GUILayout.Label("World position: " + point.ToString("F3"));
    //    GUILayout.EndArea();
    //}
}
