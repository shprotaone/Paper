using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _scoreText;

    [SerializeField] private GameObject _inputNameField;
    [SerializeField] private HighscoreTable _highscoreTable;

    private bool _recordWindow = false;

    private void Start()
    {
        OnEnable();
        OnDisable();
    }

    private void Update()
    {
        if (!_playerController.PlayerIsDeath)       //разобратся с уведомлениями о смерти
        {
            DrawAmmo();
            DrawTime();
            DrawScore();
        }
    }

    private void OnEnable()
    {
        _playerController.OnHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        _playerController.OnHealthChanged -= UpdateHealth;
    }

    private void DrawLifes(int value,bool enable)
    {
        if(value >= -1)
        {
            if (enable)
            {
                _hearts[value-1].SetActive(enable);
            }
            else
            {
                _hearts[value].SetActive(enable);
            }           
        }
    }

    private void DrawAmmo()
    {
        Vector3 pos = Input.mousePosition + _offsetAmmoView;
        _ammoView.position = _cam.ScreenToWorldPoint(pos);

        _capacityAmmoText.text = _weapon.CapacityAmmo.ToString();
    }

    private void UpdateHealth(int value,bool enable)
    {
        if (_playerController == null) return;
        DrawLifes(value,enable);
        SubmitRecord();
    }

    private void DrawTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(_gameManager.InGameTime);
        _timerText.text = time.ToString(@"mm\:ss");
    }

    private void DrawScore()
    {
        _scoreText.text = _gameManager.Score.ToString();
    }

    private void SubmitRecord()
    {
        if (_playerController.PlayerIsDeath)
        {
            _inputNameField.SetActive(true);
        }       
    }

    public void SaveScore()
    {
        _gameManager.NameForRecord = _inputNameField.GetComponent<TMP_InputField>().text;
        _highscoreTable.AddHighscoreEntry(_gameManager.Score, (int)_gameManager.InGameTime, _gameManager.NameForRecord);
        _inputNameField.SetActive(false);
        _highscoreTable.gameObject.SetActive(true);
    }
}
