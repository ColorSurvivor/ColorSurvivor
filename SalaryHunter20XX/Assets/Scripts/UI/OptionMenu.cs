using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [Header("UI References")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    private int targetWidth = 1920;
    private int targetHeight = 1080;

    void Start()
    {
        // 볼륨 초기값
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // 전체화면 토글 초기값
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // 무조건 FHD로 고정
        Screen.SetResolution(targetWidth, targetHeight, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        // 전체화면 상태 바꿨을 때도 해상도 유지
        Screen.SetResolution(targetWidth, targetHeight, isFullscreen);
    }

    public void CloseOptionPanel()
    {
        gameObject.SetActive(false);
    }

    public void TogglePanel(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
}
