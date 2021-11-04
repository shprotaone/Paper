using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _hearts;
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Weapon _weapon;

    [SerializeField] private RectTransform _ammoView;
    [SerializeField] private Vector3 _offsetAmmoView;
    [SerializeField] private Camera _cam;

    [SerializeField] private TMP_Text _capacityAmmoText;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _scoreText;

    [SerializeField] private GameObject _inputNameField;
    [SerializeField] private HighscoreTable _highscoreTable;

    [SerializeField] private TMP_Text _errorText;

    private void Start()
    {
        OnEnable();
        OnDisable();
    }

    private void Update()
    {
        if (!_healthSystem.PlayerIsDeath)       //разобратся с уведомлениями о смерти
        {
            DrawAmmo();
            DrawTime();
            DrawScore();
        }
    }

    private void OnEnable()
    {
        _healthSystem.OnHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        _healthSystem.OnHealthChanged -= UpdateHealth;
    }

    private void DrawLifes(int value,bool activate)
    {
        if(value >= -1)
        {
            if (activate)
            {
                _hearts[value-1].SetActive(activate);
            }
            else
            {
                _hearts[value].SetActive(activate);
            }           
        }
    }

    private void DrawAmmo()
    {
        _capacityAmmoText.text = _weapon.CapacityAmmo.ToString();
        _ammoView.position = _healthSystem.transform.position + _offsetAmmoView;
    }

    private void UpdateHealth(int value,bool enable)
    {
        if (_healthSystem == null) return;
        
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
        if (_healthSystem.PlayerIsDeath)
        {
            _inputNameField.SetActive(true);
        }       
    }

    /// <summary>
    /// То что ниже перенсти в отдельный скрипт и повесить на InputField
    /// </summary>
    public void SaveScore()
    {
        bool correct;
        string currentName;

        currentName = CheckName(out correct);

        if(correct)
        {
            _gameManager.NameForRecord = currentName;
            _errorText.text = "";
            _highscoreTable.AddHighscoreEntry(_gameManager.Score, (int)_gameManager.InGameTime, _gameManager.NameForRecord);
            _inputNameField.SetActive(false);
            _highscoreTable.gameObject.SetActive(true);
        }
    }

    private string CheckName(out bool correct)
    {
        string result = _inputNameField.GetComponent<TMP_InputField>().text;

        if (result == "")
        {
            result = "Unknown";
            correct = true;
            return result;
        }
        else if (result.Length <= 8)
        {
            _errorText.text = "Good Name!";
            _errorText.color = Color.green;
            correct = true;            
            return result;
        }
        else
        {
            _errorText.text = "Name is very long, try again";
            _errorText.color = Color.red;
            correct = false;
            return null;
        }
    }
}
