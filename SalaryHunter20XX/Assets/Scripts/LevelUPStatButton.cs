using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LevelUPStatButton : MonoBehaviour
{
    public Image image;//UI 프리펩 오브젝트
    public TextMeshProUGUI WeaponName;//UI 프리펩 오브젝트
    public TextMeshProUGUI WeaponValue;//UI 프리펩 오브젝트

    
    // public void Select()
    // {
    //     ManagetObject = transform.parent.gameObject;
    //     GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SetStat(What, How);
    //     Time.timeScale = 1;
    //     Destroy(ManagetObject);//UI는 역할을 다했으니 제거
    // }

    public void Init(string newName, Sprite newImage, float newValue)
    {
        image.sprite = newImage;
        WeaponName.text = newName;
        WeaponValue.text = newValue.ToString();
    }
}
