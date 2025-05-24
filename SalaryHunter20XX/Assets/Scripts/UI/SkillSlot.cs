using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public Image skillImage;  // 스킬 아이콘 이미지
    public Image cooldownImage;  // 쿨타임 표시용 원형 이미지
    public float Skillcooltime = 5f;  // 스킬의 전체 쿨타임 시간
    private float currentCooldownTime = 0f;  // 현재 쿨타임 시간

    void Start()
    {
        currentCooldownTime = 0f;
    }

    void Update()
    {
        if (currentCooldownTime < Skillcooltime) //아직 쿨다운 중
        {
            currentCooldownTime += Time.deltaTime;
            cooldownImage.fillAmount = 1f - currentCooldownTime / Skillcooltime; //1에서 0으로 감소
        }
        else //사용가능하면 0으로 고정.
        {
            cooldownImage.fillAmount = 0f;
        }

         if (Input.GetKeyDown(KeyCode.Space) && (currentCooldownTime > Skillcooltime))
         {
            UseSkill();
         }
    }

    // 스킬 사용 시 호출
    public void UseSkill()
    {
        GameManager.instance.playerSkill.UseSkill();
    }
}
