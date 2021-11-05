using UnityEngine;
using TMPro;

public class RecordConfirmer : MonoBehaviour
{
    [SerializeField] private GameObject _inputNameField;
    [SerializeField] private HighscoreTable _highscoreTable;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private GameStats _gameStats;

    private string _nameForRecord;

    private void Start()
    {
        _gameStats.OnDeath += RecordFieldActivated;
    }

    private void OnDisable()
    {
        _gameStats.OnDeath -= RecordFieldActivated;
    }

    private void RecordFieldActivated(bool condition)
    {
        if (condition)
        {
            _inputNameField.SetActive(true);
        }
    }

    public void SaveScore()
    {
        bool correct;
        string currentName;

        currentName = CheckName(out correct);

        if (correct)
        {
            _nameForRecord = currentName;
            _errorText.text = "";
            _highscoreTable.AddHighscoreEntry(_gameStats.Score, (int)_gameStats.InGameTime, _nameForRecord);
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
