using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float DamageMul = 1; // 데미지 배율
    public float ATKSpeedMul = 1; // 공격속도 배율
    public bool ShotLock = false; // 공격 봉인 여부
    public GameObject[] SkillPrefabs; // 스킬 프리팹 배열

    public void UseSkill()
    {
        int R = 0, G = 0, B = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            ColorType CT = transform.GetChild(i).GetComponent<BaseGun>().weaponColor;
            switch (CT)
            {
                case ColorType.Red:
                    R++;
                    break;
                case ColorType.Blue:
                    B++;
                    break;
                case ColorType.Green:
                    G++;
                    break;
            }
        }

        // 3개짜리 조합
        if (R == 3)
            RRRSkill();
        else if (G == 3)
            GGGSkill();
        else if (B == 3)
            BBBSkill();
        // 3개인데 2개인 조합
        else if (R == 2 && B == 1)
            RRBSkill();
        else if (R == 2 && G == 1)
            RRGSkill();
        else if (B == 2 && R == 1)
            BBRSkill();
        else if (B == 2 && G == 1)
            BBGSkill();
        else if (G == 2 && R == 1)
            GGRSkill();
        else if (G == 2 && B == 1)
            GGBSkill();
        // 1개씩 모두 있는 조합
        else if (R == 1 && G == 1 && B == 1)
            RGBSkill();
        // 2개짜리 조합
        else if (R == 1 && G == 1 && B == 0)
            RGSkill();
        else if (R == 1 && B == 1 && G == 0)
            RBSkill();
        else if (G == 1 && B == 1 && R == 0)
            GBSkill();
        // 1개짜리 조합
        else if (R == 1 && G == 0 && B == 0)
            RSkill();
        else if (G == 1 && R == 0 && B == 0)
            GSkill();
        else if (B == 1 && R == 0 && G == 0)
            BSkill();
        // 아무것도 없는 경우
        else
            NoneSkill();
    }

    void RRRSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[0], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
    void RRBSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[1], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
    void RRGSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[2], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }

    void BBBSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[3], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
    void BBRSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[4], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
    void BBGSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[5], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }

    void GGGSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[6], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정        
    }
    void GGRSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[7], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
    void GGBSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[8], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }

    void RGBSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[9], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }

    void RGSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[10], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정       
    }
    void RBSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[11], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
    void GBSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[12], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
    void RSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[13], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
    void GSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[14], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
    void BSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[15], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
    void NoneSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[16], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
}
