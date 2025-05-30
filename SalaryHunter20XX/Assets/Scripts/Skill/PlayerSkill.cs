using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float DamageMul = 1; // 데미지 배율
    public float ATKSpeedMul = 1; // 공격속도 배율
    public bool ShotLock = false; // 공격 봉인 여부
    public bool GGGSkillOn = false; // GGG 스킬 사용 여부
    public GameObject[] SkillPrefabs; // 스킬 프리팹 배열
    public int SkillColor = 1; // 스킬 색상 (1: Red, 2: Green, 3: Blue)
    int R = 0, G = 0, B = 0; // 스킬 색상 카운트 변수
    public void SkillColorChange()
    {
        SkillColor++;
        if (SkillColor > 3)
            SkillColor = 1;
    }

    public void UseSkill()
    {
        R = 0;
        G = 0;
        B = 0;
        StartCoroutine(SkillOn()); // 스킬 활성화
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
        // 1개씩 모두 있는 조합
        else if (R == 1 && G == 1 && B == 1)
            RGBSkill();
        // 아무것도 없는 경우
        else if (R == 0 && G == 0 && B == 0)
            NoneSkill();
        // 위 조합은 아닌데 뭐라도 있는경우
        else
            NormalSkill();

        
    }

    void RRRSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[0], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }

    void BBBSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[1], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }

    void GGGSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[2], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정        
    }

    void RGBSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[3], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }
    void NormalSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[4], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
        temp.GetComponent<NormalSkills>().SkillOn(R, G, B); // NormalSkills 스크립트의 SkillOn 메서드 호출
    }

    void NoneSkill()
    {
        GameObject temp = Instantiate(SkillPrefabs[5], transform);
        temp.transform.position = transform.position; // 스킬 오브젝트 위치 지정
    }

    IEnumerator SkillOn()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<BaseGun>().IsSkillActive = true; // 모든 총의 스킬 활성화
        }
        yield return new WaitForSeconds(10f); // 스킬 지속 시간
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<BaseGun>().IsSkillActive = false; // 모든 총의 스킬 비활성화
        }
    }
}
