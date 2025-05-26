using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : MonoBehaviour
{
    public WeaponData[] weaponList; //모든 무기정보가 들어있는 배열.
    public List<GameObject> curWeapons; //장비하고 있는 무기.
    public WeaponData[] currentChoice = new WeaponData[3]; //선택지를 담는 배열.

    void Start()
    {
        weaponList[0].rarity = WeaponGrade.Common;
        weaponList[0].weaponcolor = ColorType.None;

        GameManager.instance.player.GetWeapon(weaponList[0]);
    }

    public void MakeNewChoices()
    {
        List<int> availables = new List<int>();
        for(int i=0; i<weaponList.Length; i++) availables.Add(i); //가능한 모든 숫자 리스트.

        List<int> randoms = new List<int>(); //그 중에서 선택지가 될 3개를 뽑음.
        for (int i = 0; i < 3; i++)
        {
            int n = Random.Range(0, availables.Count);
            randoms.Add(availables[n]);
            availables.Remove(availables[n]);

            currentChoice[i] = weaponList[randoms[i]]; //숫자 3개로 선택지를 고름.
            currentChoice[i].rarity = GetWeaponGrade();
            currentChoice[i].weaponcolor = (ColorType)Random.Range(1, 4);
        }
    }

    public WeaponGrade GetWeaponGrade()
    {
        int rand = Random.Range(0, 100);

        if (GameManager.instance.player.GetLV() <= 3) return WeaponGrade.Common; //3레벨까지는 일반만.
        else if (GameManager.instance.player.GetLV() <= 8) //4~8은 희귀/일반 25/75
        {
            if (rand < 25) return WeaponGrade.Rare;
            else return WeaponGrade.Common;
        }
        else if (GameManager.instance.player.GetLV() <= 18) //11~18은 에픽/희귀/일반 20/70/10
        {
            if (rand < 20) return WeaponGrade.Epic;
            else if (rand < 70) return WeaponGrade.Rare;
            else return WeaponGrade.Common;
        }
        else if (GameManager.instance.player.GetLV() <= 30) //19 ~ 30 전설/에펙/희귀 25/50/25
        {
            if (rand < 25) return WeaponGrade.Legendary;
            else if (rand < 70) return WeaponGrade.Epic;
            else return WeaponGrade.Rare;
        }
        else //31~ 전설/에픽 60/40
        {
            if (rand < 60) return WeaponGrade.Legendary;
            else return WeaponGrade.Epic;
        }
    }

    public void TakeItemToEmptySlot(int number)
    {
        GameManager.instance.player.GetWeapon(currentChoice[number]);
        // curWeapons.Add(currentChoice[number].weaponPrefab);
    }
    public void OverwriteItemToSlot(int curslot, int rewardN)
    {
        GameObject temp = curWeapons[curslot]; //삭제할 무기 임시 저장
        curWeapons.RemoveAt(curslot);
        GameManager.instance.player.GetWeapon(currentChoice[rewardN]);

        Destroy(temp);
        // curWeapons[number] = currentChoice[number].weaponPrefab;
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
    public float[] bulletDamage = new float[4];
    public float[] bulletSpeed = new float[4];
    public float[] ShootingSpeed = new float[4];
    public int[] MaxPenetration = new int[4];
}
