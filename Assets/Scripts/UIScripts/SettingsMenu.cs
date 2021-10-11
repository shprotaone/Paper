using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    private const string musicName = "Music";
    private const string sfxName = "SFX";

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private Resolution[] _resolutions;

    private void Start()
    {
        AddResolutions();
    }
    public void SetMusic(float volume)
    {
        _audioMixer.SetFloat(musicName, volume);
    }

    public void SetSFX(float volume)
    {
        _audioMixer.SetFloat(sfxName, volume);
    }

    public void SetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void AddResolutions()
    {
        _resolutions = new Resolution[3]        //здесь сделал согласно ТЗ, 3 разрешения
        {
            NewResolution(1280,720),
            NewResolution(1920,1080),
            NewResolution(2000,1080)
        };

        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            options.Add(option);

            if(_resolutions[i].width == Screen.currentResolution.width &&
               _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Конструктор разрешения
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    private Resolution NewResolution(int width,int height)
    {
        Resolution current = new Resolution();
        current.width = width;
        current.height = height;

        return current;
    }
}
