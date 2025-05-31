using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(true);
        StartCoroutine(Timer(60f));
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
