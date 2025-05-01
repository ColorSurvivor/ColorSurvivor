using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Player playerScript;
    Slider hpBar;
    void Start()
    {
        playerScript = GameManager.instance.player.GetComponent<Player>();
        hpBar = GetComponent<Slider>();

    }

    void LateUpdate()
    {
        hpBar.value = playerScript.GetCurHP() / playerScript.GetMaxHP();
    }
}
