using System.Collections;
using UnityEngine;

public class Skills : MonoBehaviour
{
    protected PlayerSkill weaponHanger;

    public virtual void Awake()
    {
        weaponHanger = transform.parent.GetComponent<PlayerSkill>();
        StartCoroutine(Skill());
    }

    public virtual IEnumerator Skill()
    {
        print("RRR");
        yield return new WaitForSeconds(0.5f);
    }
}
