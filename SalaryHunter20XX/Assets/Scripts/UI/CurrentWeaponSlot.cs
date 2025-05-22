using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponSlot : MonoBehaviour
{
    public Image itemSprite;
    public Image weaponColor;
    public int slotNumber;

    void Select()
    {
        transform.parent.GetComponent<LevelUpButtonOb>().selectedSlotNumber = slotNumber;
        transform.parent.GetComponent<LevelUpButtonOb>().setArrowPos();
        //선택됐을 경우 할 행동.
    }

    public void Init(Sprite weaponSprite)
    {
        itemSprite.GetComponent<Image>().sprite = weaponSprite;
        weaponColor.GetComponent<Image>().gameObject.SetActive(true);
        weaponColor.GetComponent<Image>().color = Color.blue;
    }

    public void Clear()
    {
        itemSprite.GetComponent<Image>().sprite = null;
        weaponColor.GetComponent<Image>().gameObject.SetActive(false);
    }
}
