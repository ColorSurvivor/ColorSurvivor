using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
