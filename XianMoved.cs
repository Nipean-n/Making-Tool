using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XianMoved : MonoBehaviour
{
    public Transform code3; // Code3 立方体的引用
    public Transform xian; // Xian 立方体的引用
    public float centerPosition = -41f; // Xian 立方体的目标中心 X 轴位置
    public float range = 300f; // Xian 立方体的 X 轴位置范围

    void Update()
    {
        if (code3 != null && xian != null)
        {
            // 确保 code3 的位置在 -126 到 134 之间
            if (code3.position.x >= -126 && code3.position.x <= -46)
            {
                // 根据 Code3 的 X 轴位置计算 Xian 的 X 轴位置
                float xPosition = Mathf.Lerp(centerPosition - range, centerPosition + range, (code3.position.x + 126) / (-46 + 126));

                // 更新 Xian 立方体的 X 轴位置
                xian.position = new Vector3(xPosition, xian.position.y, xian.position.z);
            }
        }
    }
}