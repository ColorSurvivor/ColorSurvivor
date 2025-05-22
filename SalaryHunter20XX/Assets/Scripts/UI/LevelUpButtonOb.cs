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
            GameObject temp = Instantiate(weaponPickUp);
            temp.GetComponent<LevelUPStatButton>().Init(GameManager.instance.itemPool.currentChoice[i], true);
            temp.GetComponent<LevelUPStatButton>().slotNumber = i;
            temp.transform.SetParent(transform);
            temp.transform.position = Pos[i];
        }

        for (int i = 0; i < 3; i++)
        {
            GameObject temp = Instantiate(currentWeaponSlot);
            temp.GetComponent<CurrentWeaponSlot>().Clear();

            temp.GetComponent<CurrentWeaponSlot>().slotNumber = i;
            temp.transform.SetParent(transform);
            temp.transform.position = Pos[i+3];

            if (i< GameManager.instance.itemPool.curWeapons.Count)
            {
                GameObject weaponObj = GameManager.instance.itemPool.curWeapons[i];
                temp.GetComponent<CurrentWeaponSlot>().Init(
                    weaponObj.GetComponent<BaseGun>().weaponSprite,
                    weaponObj.GetComponent<BaseGun>().rarity,
                    weaponObj.GetComponent<BaseGun>().weaponColor);
            }
        }
    }
    public void NewItemSelected(int slotN)
    {
        if (GameManager.instance.itemPool.IsHaveEmptySlot())
        {
            GameManager.instance.itemPool.TakeItemToEmptySlot(slotN); //슬롯에 빈칸이 있으면 바로 획득
            Time.timeScale = 1;
            Destroy(gameObject); //UI 제거
        }
        else
        {
            selectedSlotNumber = slotN; //보상 슬롯 중 몇번 째 슬롯이 선택됨.
            //소지중인 슬롯에서 선택해야 한다는 시각적 피드백.
        }
    }

    public void CurrentItemSelected(int slotN)
    {
        if (selectedSlotNumber == -1)//보상슬롯을 선택하지 않았을 경우
        {
            setArrowPos(slotN);
        }
        else
        {
            GameManager.instance.itemPool.OverwriteItemToSlot(slotN);
            Time.timeScale = 1;
            Destroy(gameObject); //UI 제거
        }
    }
    public void setArrowPos(int n)
    {
        slotArrow.gameObject.SetActive(true);
        slotArrow.transform.position = new Vector2(1385, 790 - 260 * n);
    }
}
