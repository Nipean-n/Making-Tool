using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public GameObject targetObject; // 要移动的目标物体
    public float movementAmount = 1.0f; // 每次滚动时物体移动的量
    public float minX = 62; // 限制的最小X坐标
    public float maxX = 134; // 限制的最大X坐标

    private bool isMouseOver = false; // 鼠标是否悬停在物体上

    void Update()
    {
        // 检查鼠标是否悬停在物体上
        isMouseOver = IsMouseOverObject();

        if (isMouseOver && targetObject != null)
        {
            // 根据鼠标滚轮输入移动物体
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scrollInput) >= 0.09f) // 如果滚动输入大于或等于0.09
            {
                // 根据滚轮方向和设置的移动量移动物体
                Vector3 movement = new Vector3((scrollInput > 0 ? 1 : -1) * movementAmount, 0, 0);
                Vector3 newPosition = targetObject.transform.position + movement;

                // 限制物体的X坐标在指定范围内
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

                targetObject.transform.position = newPosition;
            }
        }
    }

    // 检查鼠标是否悬停在物体上
    bool IsMouseOverObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }
}