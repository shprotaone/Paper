using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class HighscoreTable : MonoBehaviour
{
    private const string _tableName = "HighscorePanel";   

    [SerializeField] private Transform _entryContainer;
    [SerializeField] private Transform _entryTemplate;

    private string _dataPath = Application.streamingAssetsPath + "/HighscoreData.json";

    private List<HighscoreEntry> _highscoreEntryList;
    private List<Transform> _highscoreEntryPosition;

    private void Awake()
    {
        if(base.name == _tableName)
        {
            _entryContainer = transform.Find("HighscoreContainer");
            _entryTemplate = _entryContainer.Find("HighscoreEntry");
            _entryTemplate.gameObject.SetActive(false);

            _highscoreEntryList = new List<HighscoreEntry>();

            Load();
            PrintTable();
        }
    }   

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry,Transform container,List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(_entryTemplate, container);
        Transform entryRectTransform = entryTransform.GetComponent<Transform>();

        entryRectTransform.localPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        entryTransform.Find("posText").GetComponent<TMP_Text>().text = rank.ToString();

        float score = highscoreEntry.Score;
        entryTransform.Find("posScore").GetComponent<TMP_Text>().text = score.ToString();   //здесь возможно нужно переделывать на текст меш про

        float time = highscoreEntry.Time;
        entryTransform.Find("posTime").GetComponent<TMP_Text>().text = time.ToString();

        string name = highscoreEntry.Name;
        entryTransform.Find("posName").GetComponent<TMP_Text>().text = name;

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(float score,float time,string name)     //был static, возможны проблемы
    {
        Load();
        HighscoreEntry highscoreEntry = new HighscoreEntry(score, time, name);
        _highscoreEntryList.Add(highscoreEntry);
        SortTable();
        Save();
    }

    private void SortTable()
    {
        for (int i = 0; i < _highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < _highscoreEntryList.Count; j++)
            {
                if (_highscoreEntryList[j].Score > _highscoreEntryList[i].Score)
                {
                    HighscoreEntry tmp = _highscoreEntryList[i];
                    _highscoreEntryList[i] = _highscoreEntryList[j];
                    _highscoreEntryList[j] = tmp;
                }
            }
        }

        if (_highscoreEntryList.Count > 10)
        {
            int deleteList = _highscoreEntryList.Count - 10;
            _highscoreEntryList.RemoveRange(10, deleteList);
            Debug.Log("Check");
        }
    }

    private void DefaultTable()
    {
        _highscoreEntryList = new List<HighscoreEntry>()
        {
            new HighscoreEntry(200,60,"Alex"),
            new HighscoreEntry(1000,60,"Alex"),
            new HighscoreEntry(400,60,"Alex"),
            new HighscoreEntry(1000,60,"Alex"),
            new HighscoreEntry(600,60,"Alex"),
            new HighscoreEntry(1000,60,"Alex"),
        };

        Save();
        
    }

    private void Save()     //проверить
    {
        string[] data = new string[_highscoreEntryList.Count];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = JsonUtility.ToJson(_highscoreEntryList[i]);
        }
        File.WriteAllLines(_dataPath, data);
    }

    private void Load()
    {
        string[] data = File.ReadAllLines(_dataPath);

        _highscoreEntryList = new List<HighscoreEntry>();
        HighscoreEntry highscoreEntry;

        for (int i = 0; i < data.Length; i++)
        {
            highscoreEntry = JsonUtility.FromJson<HighscoreEntry>(data[i]);
            _highscoreEntryList.Add(new HighscoreEntry(highscoreEntry.Score,highscoreEntry.Time,highscoreEntry.Name));
        }       
    }

    private void PrintTable()
    {
        _highscoreEntryPosition = new List<Transform>();

        foreach (HighscoreEntry higscoreEntry in _highscoreEntryList)
        {
            CreateHighscoreEntryTransform(higscoreEntry, _entryContainer, _highscoreEntryPosition);
        }
    }
}




