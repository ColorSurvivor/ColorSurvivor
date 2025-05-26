using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    public Image gradeBackground;
    public Image weaponIconImage;
    public Image colorIndicator;

    public void UpdateSlot(BaseGun gun)
    {
        if (gun == null)
        {
            weaponIconImage.sprite = null;
            gradeBackground.color = Color.clear;
            colorIndicator.color = Color.clear;
            return;
        }

        weaponIconImage.sprite = gun.weaponSprite;

        switch (gun.rarity)
        {
            case WeaponGrade.Common:
                gradeBackground.color = Color.white;
                break;
            case WeaponGrade.Rare:
                gradeBackground.color = new Color(0.3f, 0.3f, 1f);
                break;
            case WeaponGrade.Epic:
                gradeBackground.color = new Color(0.8f, 0f, 1f);
                break;
            case WeaponGrade.Legendary:
                gradeBackground.color = new Color(1f, 0.4f, 0f);
                break;
        }

        switch (gun.weaponColor)
        {
            case ColorType.Red:
                colorIndicator.color = Color.red;
                break;
            case ColorType.Green:
                colorIndicator.color = Color.green;
                break;
            case ColorType.Blue:
                colorIndicator.color = Color.blue;
                break;
            default:
                colorIndicator.color = Color.clear;
                break;
        }
    }
}
