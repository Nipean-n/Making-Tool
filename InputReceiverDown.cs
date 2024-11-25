using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputReceiverDown : MonoBehaviour
{
    public TextMeshPro textMeshPro; // 文本组件
    private int number = 15; // 将初始数字设置为15
    private bool enableScrollInput = false; // 滚轮输入是否启用的标志

    void Start()
    {
        // 如果在Inspector中没有手动赋值，尝试自动获取TextMesh Pro组件
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshPro>();
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshPro component not found on " + gameObject.name);
            }
            else
            {
                // 初始显示
                UpdateDisplay();
            }
        }
    }

    public void ReceiveInput()
    {
        // 启用滚轮输入
        enableScrollInput = true;
    }

    void Update()
    {
        // 检测鼠标是否在 "ShuruUp" 上
        bool isOverShuruUp = IsMouseOverGameObject("ShuruDown");

        // 如果鼠标在 "ShuruUp" 上，启用滚轮输入
        if (isOverShuruUp && !enableScrollInput)
        {
            enableScrollInput = true;
        }
        // 如果鼠标不在 "ShuruUp" 上，禁用滚轮输入
        else if (!isOverShuruUp && enableScrollInput)
        {
            enableScrollInput = false;
        }

        // 只有当 enableScrollInput 为 true 时，滚轮事件才会被处理
        if (enableScrollInput)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            // 根据滚轮方向更新数字
            if (scrollInput > 0.09f) // 向上滚动
            {
                number = Mathf.Min(number + 1, 64);
                UpdateDisplay();
                enableScrollInput = false; // 一次更新后禁用滚轮输入
            }
            else if (scrollInput < -0.09f) // 向下滚动
            {
                number = Mathf.Max(number - 1, 3);
                UpdateDisplay();
                enableScrollInput = false; // 一次更新后禁用滚轮输入
            }
        }
    }

    private bool IsMouseOverGameObject(string gameObjectName)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject.name == gameObjectName;
        }
        return false;
    }

    private void UpdateDisplay()
    {
        // 更新TextMesh Pro组件的文本为当前数字
        textMeshPro.text = number.ToString();
        Debug.Log("Number updated to: " + number);
    }
}