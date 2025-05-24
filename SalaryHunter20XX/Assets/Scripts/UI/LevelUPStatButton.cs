using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LevelUPStatButton : MonoBehaviour
{
    public Image itemSprite;// 아이템 이미지
    public TextMeshProUGUI itemName;// 아이템 이름 
    public Image weaponColor; //무기 색 표시.
    public TextMeshProUGUI itemSort;// 아이템 분류 무기/패시브 
    public TextMeshProUGUI weaponDamage;// 무기 공격력
    public TextMeshProUGUI weaponSpeed;// 무기 공격속도 
    public TextMeshProUGUI weaponPenetration;// 무기 적 관통 수
    public int slotNumber = 0;

    public void Select() //클릭되면 실행 
    {
        transform.parent.GetComponent<LevelUpButtonOb>().NewItemSelected(slotNumber);
    }

    public void Init(WeaponData itemStats, bool isGun)
    {
        itemSprite.sprite = itemStats.weaponSprite;
        itemName.text = itemStats.itemName;

        switch (itemStats.rarity)
        {
            case WeaponGrade.Common:
                break;
            case WeaponGrade.Rare:
                GetComponent<Image>().color = new Color(0.3f, 0.3f, 1f); //희귀 등급 하늘색 배경
                break;
            case WeaponGrade.Epic:
                GetComponent<Image>().color = new Color(0.8f, 0f, 1f); //레어 등급 보라색 배경
                break;
            case WeaponGrade.Legendary:
                GetComponent<Image>().color = new Color(1f, 0.4f, 0f); //전설? 등급 붉은색 배경
                break;
        }

        switch (itemStats.weaponcolor)
        {
            case ColorType.Red:
                weaponColor.GetComponent<Image>().color = new Color(1f, 0.25f, 0.25f);
                break;
            case ColorType.Green:
                weaponColor.GetComponent<Image>().color = new Color(0.4f, 1f, 0.4f);
                break;
            case ColorType.Blue:
                weaponColor.GetComponent<Image>().color = new Color(0.4f, 0.4f, 1f);
                break;
        }

        if(isGun)
        {
            itemSort.text = "Weapon";

            weaponDamage.text = "Damage: " + itemStats.bulletDamage[(int)itemStats.rarity];
            weaponSpeed.text = "Speed: " + itemStats.bulletSpeed[(int)itemStats.rarity];
            weaponPenetration.text = "Penetration: " + itemStats.MaxPenetration[(int)itemStats.rarity];
        }
        else
        {
            itemSort.text = "Item";

            weaponDamage.text = "";
            weaponSpeed.text = "";
            weaponPenetration.text = "";
        }
    }
}
