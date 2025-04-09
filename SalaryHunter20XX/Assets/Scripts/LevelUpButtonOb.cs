using System.Collections.Generic;
using UnityEngine;

public class LevelUpButtonOb : MonoBehaviour
{
    public List<GameObject> Buttons = new List<GameObject>();
    public GameObject[] Pos = new GameObject[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int[] Nums = {-1,-1,-1};
        for(int i = 0;i<3;i++)
        {
            bool Isin = true;
            while(Isin)
            {
                Nums[i] = Random.Range(0,Buttons.Count);
                Isin = false;
                for(int j = 0;j<3;j++)
                {
                    if(j==i)
                        continue;
                    if(Nums[i]==Nums[j])
                        Isin = true;
                }
            }
            GameObject temp = Instantiate(Buttons[Nums[i]]);
            temp.transform.parent = transform;
            temp.transform.position = Pos[i].transform.position;
        }
    }

    // Update is called once per frame
    
}
