using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Image skillImage;  // 스킬 아이콘 이미지
    public Image skillColorImage;  // 스킬컬러 아이콘 이미지
    public Image cooldownImage;  // 쿨타임 표시용 원형 이미지
    public float Skillcooltime = 10f;  // 스킬의 전체 쿨타임 시간
    private float currentCooldownTime = 0f;  // 현재 쿨타임 시간
    bool isSkillReady = true; // 스킬 사용 가능 여부

    void Start()
    {
        currentCooldownTime = 0f;
        skillColorImage.color = new Color32(255, 0, 0, 100);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isSkillReady)
        {
            isSkillReady = false; // 스킬 사용 불가 상태로 변경
            AudioManager.instance.PlaySkillUse();
            UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            AudioManager.instance.PlayChangingColor();
            GameObject.Find("WeaponHanger").GetComponent<PlayerSkill>().SkillColorChange();
            switch (GameObject.Find("WeaponHanger").GetComponent<PlayerSkill>().SkillColor)
            {
                case 1:
                    skillColorImage.color = new Color32(255, 0, 0, 100); // 빨간색
                    break;
                case 2:
                    skillColorImage.color = new Color32(0, 255, 0, 100); // 초록색
                    break;
                case 3:
                    skillColorImage.color = new Color32(0, 0, 255, 100); // 파란색
                    break;
            }
        }
    }

    // 스킬 사용 시 호출
    public void UseSkill()
    {
        GameObject.Find("WeaponHanger").GetComponent<PlayerSkill>().UseSkill();
    }
    public void OnSkill(float Skilltime, float DebuffTime)
    {
        StartCoroutine(CoolDown(Skilltime, DebuffTime, Skillcooltime));
    }
    IEnumerator CoolDown(float Skilltime, float DebuffTime, float Cooldown)
    {
        currentCooldownTime = 0;
        while (currentCooldownTime < Skilltime)
        {
            currentCooldownTime += Time.deltaTime;
            cooldownImage.fillClockwise = true; // 원형 이미지가 시계 방향으로 채워짐
            cooldownImage.fillAmount = currentCooldownTime / Skilltime;
            yield return new WaitForEndOfFrame();
        }
        currentCooldownTime = 0; // 이제 디버프 시간 계산
        while (currentCooldownTime < DebuffTime)
        {
            currentCooldownTime += Time.deltaTime;
            skillImage.color = new Color32(255, 0,0, 100); // 스킬 시뻘겋게
            cooldownImage.fillAmount = currentCooldownTime / DebuffTime;
            yield return new WaitForEndOfFrame();
        }
        skillImage.color = new Color32(255, 255, 255, 100); // 스킬 복구
        currentCooldownTime = 0; // 이제 쿨탐 계산
        while (currentCooldownTime < Cooldown)
        {
            currentCooldownTime += Time.deltaTime;
            cooldownImage.fillClockwise = false; // 원형 이미지가 반시계 방향으로 채워짐
            cooldownImage.fillAmount = 1 - (currentCooldownTime / Skillcooltime);
            yield return new WaitForEndOfFrame();
        }
        isSkillReady = true; // 스킬 사용 가능 상태로 변경
        cooldownImage.fillAmount = 0; // 쿨타임 이미지 초기화
        AudioManager.instance.PlaySkillReady();
    }
}
