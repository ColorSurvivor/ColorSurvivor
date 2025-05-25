using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OptionMenu : MonoBehaviour
{
    [Header("UI References")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] allowedResolutions = new Resolution[]
    {
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 2560, height = 1440 },
        new Resolution { width = 3840, height = 2160 }
    };

    void Start()
    {
        // 볼륨 초기값
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 전체화면 토글 초기값
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // 해상도 드롭다운 설정
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < allowedResolutions.Length; i++)
        {
            string option = allowedResolutions[i].width + " x " + allowedResolutions[i].height;
            options.Add(option);

            if (Screen.currentResolution.width == allowedResolutions[i].width &&
                Screen.currentResolution.height == allowedResolutions[i].height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int index)
    {
        Resolution res = allowedResolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void CloseOptionPanel()
    {
        gameObject.SetActive(false); // 패널 비활성화
    }
}
