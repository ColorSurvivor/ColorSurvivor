using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    Player playerScript; 
    Slider expBar;
    public TextMeshProUGUI levelText;
    void Start()
    {
        playerScript = GameManager.instance.player.GetComponent<Player>(); 
        expBar = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        expBar.value = playerScript.GetCurEXP() / playerScript.GetMaxEXP(); //플레이어 스크립트에서 경험치를 받아 바를 업데이트.
        levelText.text = "LV: " + playerScript.GetLV();
    }
}
