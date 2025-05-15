using System.Collections.Generic;
using UnityEngine;

public class LevelUpButtonOb : MonoBehaviour
{
    public GameObject WeaponPickUp; //선택 창 프리팹
    public GameObject[] Pos = new GameObject[3]; //선택 창 위치
    
    void Start()
    {
        GameManager.instance.itemPool.MakeNewChoices();

        for(int i=0; i<3; i++)
        {
            int rarity = Random.Range(0, 4);
    
            GameObject temp = Instantiate(WeaponPickUp);
            temp.GetComponent<LevelUPStatButton>().Init(GameManager.instance.itemPool.currentChoice[i], true, rarity);
            temp.GetComponent<LevelUPStatButton>().slotNumber = i;
            temp.transform.SetParent(transform);
            temp.transform.position = Pos[i].transform.position;
        }
    }
}
