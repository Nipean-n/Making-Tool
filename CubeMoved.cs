using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMoved : MonoBehaviour
{
    public Transform code1; // Code1 立方体的引用
    public Transform xian; // Xian 立方体的引用
    public float minY = -8000f; // Xian 立方体的最小 Y 轴位置
    public float maxY = 0f; // Xian 立方体的最大 Y 轴位置

    void Update()
    {
        if (code1 != null && xian != null)
        {
            // 确保 code1 的位置在 62 到 134 之间
            if (code1.position.x >= 62 && code1.position.x <= 134)
            {
                // 根据 Code1 的 X 轴位置计算 Xian 的 Y 轴位置
                float yPosition = Mathf.Lerp(minY, maxY, (code1.position.x - 62) / (134 - 62));

                // 更新 Xian 立方体的 Y 轴位置
                xian.position = new Vector3(xian.position.x, yPosition, xian.position.z);
            }
        }
    }
}