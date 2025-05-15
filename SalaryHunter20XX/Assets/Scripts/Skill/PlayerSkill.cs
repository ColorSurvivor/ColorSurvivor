using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public void UseSkill()
    {
        int R = 0, G = 0, B = 0;

        for (int i = 0; i < transform.childCount; i++)
        {
            ColorType CT = transform.GetChild(i).GetComponent<BaseGun>().WeaponColor;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void RRRSkill()
    {

    }
    void RRBSkill()
    {

    }
    void RRGSkill()
    {

    }

    void BBBSkill()
    {
        
    }
    void BBRSkill()
    {

    }
    void BBGSkill()
    {

    }

    void GGGSkill()
    {
        
    }
    void GGRSkill()
    {

    }
    void GGBSkill()
    {

    }

    void RGBSkill()
    {

    }

    void RGSkill()
    {
        
    }
    void RBSkill()
    {

    }
    void GBSkill()
    {

    }
    void RSkill()
    {

    }
    void GSkill()
    {

    }
    void BSkill()
    {

    }
    void NoneSkill()
    {

    }
}
