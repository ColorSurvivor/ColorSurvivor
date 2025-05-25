using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Cursor.visible = false; // 기본 커서 숨기기
    }

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            Input.mousePosition, null, out pos);
        rectTransform.localPosition = pos;
    }
}
