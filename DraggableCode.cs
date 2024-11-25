using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableCode : MonoBehaviour
{
    public float minX; // X轴最小值
    public float maxX; // X轴最大值

    private bool isDragging = false; // 是否正在拖动
    private Vector3 clickOffset; // 鼠标点击位置与物体的偏移量
    private Vector3 initialPosition; // 初始位置

    void Start()
    {
        // 记录初始位置
        initialPosition = transform.position;
    }

    public void StartDragging()
    {
        isDragging = true;
        // 计算鼠标点击位置与物体的偏移量
        Vector3 mousePosition = Input.mousePosition;
        // 将鼠标的屏幕坐标转换为世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        clickOffset = transform.position - worldPosition;
        Debug.Log("射线击中了Code");
    }

    void Update()
    {
        if (isDragging)
        {
            // 更新物体位置
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
            Vector3 newPosition = worldPosition + clickOffset;
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX); // 限制X轴
            newPosition.y = initialPosition.y; // Y轴坐标固定为初始Y轴坐标
            newPosition.z = initialPosition.z; // Z轴坐标固定为初始Z轴坐标
            transform.position = newPosition;
        }

        if (Input.GetMouseButtonUp(0)) // 检测鼠标左键松开
        {
            isDragging = false;
        }
    }
}