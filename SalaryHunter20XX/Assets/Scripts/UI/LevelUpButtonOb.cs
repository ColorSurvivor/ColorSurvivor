using System.Collections.Generic;
using UnityEngine;

public class LevelUpButtonOb : MonoBehaviour
{
    public GunStats[] gunStats;
    public GameObject WeaponPickUp;
    public GameObject[] Pos = new GameObject[3];
    
    void Start()
    {
        int[] Nums = {-1,-1,-1};
        for(int i = 0;i<3;i++)
        {
            bool Isin = true;
            while(Isin)
            {
                Nums[i] = Random.Range(0,gunStats.Length);
                Isin = false;
                for(int j = 0;j<3;j++)
                {
                    if(j==i)
                        continue;
                    if(Nums[i]==Nums[j])
                        Isin = true;
                }
            }
            GameObject temp = Instantiate(WeaponPickUp);
            temp.GetComponent<LevelUPStatButton>().Init(gunStats[Nums[i]].GunName, gunStats[Nums[i]].WeaponSprite, gunStats[Nums[i]].bulletDamage[0]);
            temp.transform.SetParent(transform);
            temp.transform.position = Pos[i].transform.position;
        }
    }
}

[System.Serializable]
public class GunStats
{
    public string GunName;
    public Sprite WeaponSprite;
    public float[] bulletDamage;
    public float[] bulletSpeed;
    public float[] ShootingSpeed;
    public float[] MaxPenetrate;
}
