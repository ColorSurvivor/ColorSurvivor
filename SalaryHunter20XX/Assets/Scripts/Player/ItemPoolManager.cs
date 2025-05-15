using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : MonoBehaviour
{
    public GameObject startWeapon; //시작무기. Inspector에서 지정.
    public WeaponData[] weaponList; //모든 무기정보가 들어있는 배열.
    [SerializeField]
    BaseGun[] curWeapons = new BaseGun[3];
    public WeaponData[] currentChoice = new WeaponData[3];
    int weaponIndex = 0;

    void Start()
    {
        GameManager.instance.player.GetWeapon(startWeapon);
    }

    public void MakeNewChoices()
    {
        List<int> availables = new List<int>();
        for(int i=0; i<weaponList.Length; i++) availables.Add(i); //가능한 모든 숫자 리스트.

        List<int> randoms = new List<int>(); //그 중에서 선택지가 될 3개를 뽑음.
        for(int i=0; i<3; i++)
        {
            int n = Random.Range(0, availables.Count);
            randoms.Add(availables[n]);
            availables.Remove(availables[n]);
        }
        
        for(int i=0; i<3; i++) //숫자 3개로 선택지를 고름.
        {
            currentChoice[i] = weaponList[randoms[i]];
        }
    }

    public void TakeItem(int number)
    {
        GameManager.instance.player.GetWeapon(currentChoice[number].weaponPrefab);
    }

    public int GetWeaponCount()
    {
        return weaponIndex;
    }

    public void AddWeaponToEmptySlot(GameObject newWeaponData)
    {
        // weapons[weaponIndex] = newWeaponData;
        if(weaponIndex < 2) weaponIndex += 1;
    }
}

[System.Serializable]
public class WeaponData
{
    public GameObject weaponPrefab;
    public Sprite weaponSprite;
    public string itemName;
    public float[] bulletDamage = new float[4];
    public float[] bulletSpeed = new float[4];
    public float[] ShootingSpeed = new float[4];
    public float[] MaxPenetrate = new float[4];
}
