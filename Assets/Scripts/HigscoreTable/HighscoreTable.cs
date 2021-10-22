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
    [SerializeField] private GameObject _newRecordUI;

    private string _dataPath = Application.streamingAssetsPath + "/HighscoreData.json";

    private List<HighscoreEntry> _highscoreEntryList;
    private List<Transform> _highscoreEntryPosition;

    private Color _newEntryColor = Color.yellow;

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

    /// <summary>
    /// Инициализация таблицы
    /// </summary>
    /// <param name="highscoreEntry"></param>
    /// <param name="container"></param>
    /// <param name="transformList"></param>
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry,Transform container,List<Transform> transformList)
    {        
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(_entryTemplate, container);
        Transform entryRectTransform = entryTransform.GetComponent<Transform>();
       
        entryRectTransform.localPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        float score = highscoreEntry.Score;
        float time = highscoreEntry.Time;
        string name = highscoreEntry.Name;

        TMP_Text[] fieldArray = FillLine(entryTransform);

        fieldArray[0].text = rank.ToString();
        fieldArray[1].text = score.ToString();
        fieldArray[2].text = time.ToString();
        fieldArray[3].text = name;

        CheckNewEntry(highscoreEntry.newEntry, fieldArray);

        transformList.Add(entryTransform);
    }

    /// <summary>
    /// Добавляет новую запись
    /// </summary>
    /// <param name="score"></param>
    /// <param name="time"></param>
    /// <param name="name"></param>
    public void AddHighscoreEntry(float score,float time,string name)
    {
        Load();
        HighscoreEntry highscoreEntry = new HighscoreEntry(score, time, name,true);
        _highscoreEntryList.Add(highscoreEntry);
        SortTable();

        if(_highscoreEntryList[0] == highscoreEntry && highscoreEntry.newEntry)
        {
            _newRecordUI.SetActive(true);
            
            Debug.Log("newRecord");
        }
        else
        {
            _newRecordUI.SetActive(false);
        }

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

    private void Save()
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
            _highscoreEntryList.Add(new HighscoreEntry(highscoreEntry.Score,highscoreEntry.Time,highscoreEntry.Name,highscoreEntry.newEntry));
        }

        
    }

    /// <summary>
    /// Вывод таблицы
    /// </summary>
    private void PrintTable()
    {
        _highscoreEntryPosition = new List<Transform>();

        foreach (HighscoreEntry higscoreEntry in _highscoreEntryList)
        {
            CreateHighscoreEntryTransform(higscoreEntry, _entryContainer, _highscoreEntryPosition);
        }

        ReturnColorEntry();
        Save();
    }

    /// <summary>
    /// Заменяет цвет на белый, если рекорд не новый
    /// </summary>
    private void ReturnColorEntry()
    {
        for (int i = 0; i < _highscoreEntryList.Count; i++)
        {
            HighscoreEntry tmp = _highscoreEntryList[i];
            tmp.newEntry = false;            
        }
    }
    
    /// <summary>
    /// Заполняет линию для окрашивания
    /// </summary>
    /// <param name="entryTransform"></param>
    /// <returns></returns>
    private TMP_Text[] FillLine(Transform entryTransform)
    {
        TMP_Text[] fieldArray =
        {
            entryTransform.Find("posText").GetComponent<TMP_Text>(),
            entryTransform.Find("posScore").GetComponent<TMP_Text>(),
            entryTransform.Find("posTime").GetComponent<TMP_Text>(),
            entryTransform.Find("posName").GetComponent<TMP_Text>()
        };

        return fieldArray;
    }
    
    /// <summary>
    /// Окрашивает новую запись в таблице
    /// </summary>
    /// <param name="isNew"></param>
    /// <param name="fieldArray"></param>
    private void CheckNewEntry(bool isNew, TMP_Text [] fieldArray)
    {
        if (isNew == true)
        {
            foreach (TMP_Text text in fieldArray)
            {
                text.color = _newEntryColor;
            }
        }
        else
        {
            foreach (TMP_Text text in fieldArray)
            {
                text.color = Color.white;
            }
        }
    }
}




