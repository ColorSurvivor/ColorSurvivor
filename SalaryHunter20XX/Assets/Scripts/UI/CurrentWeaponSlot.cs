using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponSlot : MonoBehaviour
{
    public Image itemSprite;
    public Image weaponColor;
    public int slotNumber;

    public void Select()
    {
        transform.parent.GetComponent<LevelUpButtonOb>().CurrentItemSelected(slotNumber);
    }

    public void Init(Sprite weaponSprite, WeaponGrade rarity, ColorType color)
    {
        itemSprite.GetComponent<Image>().sprite = weaponSprite;
        weaponColor.GetComponent<Image>().gameObject.SetActive(true);

        switch (rarity)
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

        switch (color)
        {
            case ColorType.None:
                weaponColor.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
                break;
            case ColorType.Red:
                weaponColor.GetComponent<Image>().color = Color.red;
                break;
            case ColorType.Blue:
                weaponColor.GetComponent<Image>().color = Color.blue;
                break;
            case ColorType.Green:
                weaponColor.GetComponent<Image>().color = Color.green;
                break;
        }
    }

    public void Clear()
    {
        itemSprite.GetComponent<Image>().sprite = null;
        weaponColor.GetComponent<Image>().gameObject.SetActive(false);
    }
}
