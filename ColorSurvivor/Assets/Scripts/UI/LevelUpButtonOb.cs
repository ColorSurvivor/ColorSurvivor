using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButtonOb : MonoBehaviour
{
    public GameObject weaponPickUp; //무기 선택 프리팹
    public GameObject currentWeaponSlot; //현재 무기 슬롯 프리팹
    public Image slotArrow; //슬롯 선택 강조 표시
    public Vector2[] Pos; //UI 위치

    public int selectedChoiceNumber = -1; // 어떤 보상을 골랐는지

    void Start()
    {
        slotArrow.gameObject.SetActive(false);
        GameManager.instance.itemPool.MakeNewChoices();

        // 무기 보상 3개 버튼 생성
        for (int i = 0; i < 3; i++)
        {
            GameObject temp = Instantiate(weaponPickUp);
            temp.GetComponent<LevelUPStatButton>().Init(GameManager.instance.itemPool.currentChoice[i]);
            temp.GetComponent<LevelUPStatButton>().slotNumber = i;
            temp.transform.SetParent(transform);
            temp.transform.position = Pos[i];
        }

        // 현재 보유 무기 슬롯 3개 버튼 생성
        for (int i = 0; i < 3; i++)
        {
            GameObject temp = Instantiate(currentWeaponSlot);
            temp.GetComponent<CurrentWeaponSlot>().Clear();
            temp.GetComponent<CurrentWeaponSlot>().slotNumber = i;
            temp.transform.SetParent(transform);
            temp.transform.position = Pos[i + 3];

            if (i < GameManager.instance.itemPool.curWeapons.Count && GameManager.instance.itemPool.curWeapons[i] != null)
            {
                GameObject weaponObj = GameManager.instance.itemPool.curWeapons[i];
                temp.GetComponent<CurrentWeaponSlot>().Init(
                    weaponObj.GetComponent<BaseGun>().weaponSprite,
                    weaponObj.GetComponent<BaseGun>().rarity,
                    weaponObj.GetComponent<BaseGun>().weaponColor);
            }
        }
    }

    // 무기(보상) 선택 시 반드시 슬롯 선택을 유도만 하고, 바로 장착 안함
    public void NewItemSelected(int choiceN)
    {
        selectedChoiceNumber = choiceN; // 선택한 무기 인덱스 저장
        slotArrow.gameObject.SetActive(true);
        // 추가: "슬롯을 선택하세요" 안내 메시지 등을 띄우면 UX 좋음
    }

    // 슬롯 선택 시, 반드시 무기가 먼저 선택됐어야만 실제 장착/교체
    public void CurrentItemSelected(int slotN)
    {
        if (selectedChoiceNumber == -1)
        {
            // 아직 무기를 안 골랐다면 안내
            // 예: "먼저 보상 무기를 선택하세요!"
            return;
        }
        else
        {
            // 무기와 슬롯 모두 선택됨 → 장착/교체 실행
            GameManager.instance.itemPool.AssignWeaponToSlot(slotN, selectedChoiceNumber);
            Time.timeScale = 1;
            Destroy(gameObject); // UI 닫기
            GameManager.instance.player.CheckLevelup();
        }
    }

    public void SkipChoice()
    {
        Time.timeScale = 1;
        Destroy(gameObject); // UI 닫기
        GameManager.instance.player.CheckLevelup();
    }
}
