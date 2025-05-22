using UnityEngine;

public class EXPCollector : MonoBehaviour
{
    Player playerData;
    Collider2D coll;
    float magnetRadius;
    void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        playerData = GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("EXP"))
        {
            AudioManager.instance.PlayExpCollect();
            playerData.getEXP(other.gameObject.GetComponent<EXP>().expAmount);
            Destroy(other.gameObject);
        }
    }
}
