using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    void Update()
    {
        float curTime = GameManager.instance.curGameTime;
        int minutes = Mathf.FloorToInt(curTime / 60f);
        int seconds = Mathf.FloorToInt(curTime % 60f);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
