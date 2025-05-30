using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : MonoBehaviour
{
    public WeaponData[] weaponList; // 모든 무기정보
    public List<GameObject> curWeapons; // 장비하고 있는 무기 (슬롯별)
    public WeaponData[] currentChoice = new WeaponData[3]; // 선택지
    public WeaponSlotUI[] hudSlots = new WeaponSlotUI[3];  // HUD 슬롯(에디터에서 드래그!)

    void Start()
    {
        weaponList[0].rarity = WeaponGrade.Common;
        weaponList[0].weaponcolor = (ColorType)Random.Range(1, 4);

        GameManager.instance.player.GetWeapon(weaponList[0]); //TODO
        UpdateHUDSlot(0); // 게임 시작시 HUD 0번 슬롯 동기화
    }

    public void MakeNewChoices()
    {
        List<int> availables = new List<int>();
        for(int i=0; i<weaponList.Length; i++) availables.Add(i); //가능한 모든 숫자 리스트.

        for (int i = 0; i < 3; i++)
        {
            int n = Random.Range(0, availables.Count);
            currentChoice[i] = weaponList[availables[n]];
            currentChoice[i].rarity = GetWeaponGrade();
            currentChoice[i].weaponcolor = (ColorType)Random.Range(1, 4);
            availables.RemoveAt(n);
        }
    }

    public WeaponGrade GetWeaponGrade()
    {
        int rand = Random.Range(0, 100);
        int lv = GameManager.instance.player.GetLV();

        if (lv <= 9) return WeaponGrade.Common; //10레벨까지는 일반만.


        else if (lv <= 14) //10~14은 희귀/일반 15/85
        {
            if (rand < 15) return WeaponGrade.Rare;
            else return WeaponGrade.Common;
        }
        else if (lv <= 19) //15~19은 희귀/일반 65/35
        {
            if (rand < 75) return WeaponGrade.Rare;
            else return WeaponGrade.Common;
        }


        else if (lv <= 24) //20~24은 에픽/희귀/일반 15/55/30
        {
            if (rand < 15) return WeaponGrade.Epic;
            else if (rand < 70) return WeaponGrade.Rare;
            else return WeaponGrade.Common;
        }
        else if (lv <= 29) //25~29은 에픽/희귀/일반 50/45/5
        {
            if (rand < 65) return WeaponGrade.Epic;
            else return WeaponGrade.Rare;
        }


        else if (lv <= 34) //24 ~ 32 전설/에펙/희귀 15/65/20
        {
            if (rand < 15) return WeaponGrade.Legendary;
            else if (rand < 80) return WeaponGrade.Epic;
            else return WeaponGrade.Rare;
        }
        else if (lv <= 39) //24 ~ 32 전설/에펙 50/50
        {
            if (rand < 50) return WeaponGrade.Legendary;
            else return WeaponGrade.Epic;
        }

        else //40~ 전설
        {
            return WeaponGrade.Legendary;
        }
    }

    public void AssignWeaponToSlot(int slotN, int choiceN)
    {
        WeaponData chosenWeapon = currentChoice[choiceN];

        // 기존 무기 삭제(슬롯에 이미 무기가 있다면)
        if (curWeapons.Count > slotN && curWeapons[slotN] != null)
        {
            Destroy(curWeapons[slotN]);
            curWeapons[slotN] = null;
        }

        // 새 무기 생성 및 슬롯에 넣기
        GameObject newWpn = Instantiate(chosenWeapon.weaponPrefab);
        var gun = newWpn.GetComponent<BaseGun>();
        gun.Init(chosenWeapon);

        newWpn.transform.SetParent(GameManager.instance.player.weaponHanger.transform, false);
        newWpn.transform.position = GameManager.instance.player.transform.position;

        if (curWeapons.Count > slotN)
            curWeapons[slotN] = newWpn;
        else
        {
            while (curWeapons.Count < slotN)
                curWeapons.Add(null);
            curWeapons.Add(newWpn);
        }

        // HUD 해당 슬롯 갱신
        UpdateHUDSlot(slotN);
    }
    
     // HUD 개별 슬롯 업데이트 (curWeapons → BaseGun)
    public void UpdateHUDSlot(int slotN)
    {
        if (hudSlots != null && slotN < hudSlots.Length && hudSlots[slotN] != null)
        {
            BaseGun gun = (curWeapons.Count > slotN && curWeapons[slotN] != null)
                ? curWeapons[slotN].GetComponent<BaseGun>()
                : null;
            hudSlots[slotN].UpdateSlot(gun);
        }
    }

    // 전체 HUD를 한 번에 갱신하고 싶을 때
    public void UpdateAllHUDSlots()
    {
        for (int i = 0; i < hudSlots.Length; i++)
            UpdateHUDSlot(i);
    }

    public bool IsHaveEmptySlot()
    {
        return curWeapons.Count < 3;
    }
}

[System.Serializable]
public class WeaponData
{
    public GameObject weaponPrefab;
    public Sprite weaponSprite;
    public WeaponGrade rarity;
    public ColorType weaponcolor;
    public string itemName;
    public string description;
    public float[] bulletDamage = new float[4];
    public float[] bulletSpeed = new float[4];
    public float[] ShootingSpeed = new float[4];
}
