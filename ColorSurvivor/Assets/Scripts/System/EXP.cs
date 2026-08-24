using UnityEngine;

public class EXP : MonoBehaviour
{
    public float baseEXP;
    float finalEXP;

    public void SetFinalEXP()
    {
        finalEXP =  baseEXP + baseEXP * (GameManager.instance.curGameTime / 100f); //100초마다 1배씩 추가로 제공 ex)5분이면 3배(15)
    }
    public float GetEXP()
    {
        return finalEXP;
    }
}
