using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LevelUPStatButton : MonoBehaviour
{
    public Image itemSprite;// 아이템 이미지
    public TextMeshProUGUI itemName;// 아이템 이름 
    public TextMeshProUGUI itemSort;// 아이템 분류 무기/패시브 
    public TextMeshProUGUI weaponDamage;// 무기 공격력
    public TextMeshProUGUI weaponSpeed;// 무기 공격속도 
    public TextMeshProUGUI weaponPenetration;// 무기 적 관통 수
    public int slotNumber = 0;

    public void Select() //클릭되면 실행 
    {
        GameManager.instance.itemPool.TakeItem(slotNumber);
        Time.timeScale = 1;
        Destroy(transform.parent.gameObject);//UI는 역할을 다했으니 제거
    }

    public void Init(WeaponData itemStats, bool isGun, int grade)
    {
        itemSprite.sprite = itemStats.weaponSprite;
        itemName.text = itemStats.itemName;

        switch(grade){
            case 1:
                GetComponent<Image>().color = new Color(0.4f, 0.4f, 1f);
                break;
            case 2:
                GetComponent<Image>().color = new Color(0.4f, 1f, 0.4f);
                break;
            case 3:
                GetComponent<Image>().color = new Color(1f, 0.25f, 0.25f);
                break;
            default:
                break;
        }

        if(isGun)
        {
            itemSort.text = "Weapon";

            weaponDamage.text = "Damage: " + itemStats.bulletDamage[grade];
            weaponSpeed.text = "Speed: " + itemStats.bulletSpeed[grade];
            weaponPenetration.text = "Penetration: " + itemStats.MaxPenetrate[grade];
        }
        else
        {
            itemSort.text = "Item";

            weaponDamage.text = "";
            weaponSpeed.text = "";
            weaponPenetration.text = "";
        }
    }
}
