using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButtonOb : MonoBehaviour
{
    public GameObject weaponPickUp; //선택 창 프리팹
    public GameObject currentWeaponSlot; //현재 소지중인 무기 슬롯 프리팹

    public Image slotArrow; //교체할 무기를 가르키는 화살표
    public Vector2[] Pos; //선택 창 위치

    public int selectedSlotNumber = -1;

    void Start()
    {
        slotArrow.gameObject.SetActive(false);
        GameManager.instance.itemPool.MakeNewChoices();

        for (int i = 0; i < 3; i++)
        {
            int rarity = Random.Range(0, 4);
            int weaponColor = Random.Range(0, 3);

            GameObject temp = Instantiate(weaponPickUp);
            temp.GetComponent<LevelUPStatButton>().Init(GameManager.instance.itemPool.currentChoice[i], true, rarity, weaponColor);
            temp.GetComponent<LevelUPStatButton>().slotNumber = i;
            temp.transform.SetParent(transform);
            temp.transform.position = Pos[i];
        }

        for (int i = 3; i < 6; i++)
        {
            GameObject temp = Instantiate(currentWeaponSlot);
            temp.GetComponent<CurrentWeaponSlot>().Clear();

            GameObject weaponObj = GameManager.instance.itemPool.curWeapons[i - 3];
            if (weaponObj != null)
            {
                // temp.GetComponent<CurrentWeaponSlot>().Init(weaponObj.GetComponent<BaseGun>().);
                // basegun에 무기 스프라이트가 없음.        
            }

            temp.GetComponent<CurrentWeaponSlot>().slotNumber = i - 3;
            temp.transform.SetParent(transform);
            temp.transform.position = Pos[i];
        }
    }
    public void setArrowPos()
    {
        slotArrow.gameObject.SetActive(true);
        slotArrow.transform.position = new Vector2(1385, 790 - 260 * selectedSlotNumber);
    }
}
