using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _progressText;

    public void LoadLevel(int sceneIndex)
    {       
        StartCoroutine(LoadAsync(sceneIndex));       
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        _loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            _slider.value = progress;
            _progressText.text = (int)(progress * 100) + "%";

            yield return null;
        }
    }
}
