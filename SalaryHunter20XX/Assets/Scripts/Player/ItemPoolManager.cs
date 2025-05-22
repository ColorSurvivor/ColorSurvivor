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
            currentChoice[i].rarity = (WeaponGrade)Random.Range(0, 4);
            currentChoice[i].weaponcolor = (ColorType)Random.Range(1, 4);
        }
    }

    public void TakeItemToEmptySlot(int number)
    {
        GameManager.instance.player.GetWeapon(currentChoice[number]);
        // curWeapons.Add(currentChoice[number].weaponPrefab);
    }
    public void OverwriteItemToSlot(int number)
    {
        Destroy(curWeapons[number]);
        curWeapons.RemoveAt(number);
        GameManager.instance.player.GetWeapon(currentChoice[number]);
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
    public float[] MaxPenetrate = new float[4];
}
