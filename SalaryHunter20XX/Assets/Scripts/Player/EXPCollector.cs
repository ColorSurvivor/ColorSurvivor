using UnityEngine;

public class EXPCollector : MonoBehaviour
{
    Player playerData;
    void Awake()
    {
        playerData = GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("EXP"))
        {
            AudioManager.instance.PlayExpCollect();
            playerData.getEXP(other.gameObject.GetComponent<EXP>().GetEXP());
            Destroy(other.gameObject);
        }
    }
}
