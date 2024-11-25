using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XianRotator : MonoBehaviour
{
    public Transform codeCubeTransform; // Code立方体的Transform引用
    public Transform xianTransform; // Xian立方体的Transform引用

    void Update()
    {
        // 如果引用没有设置，则不执行任何操作
        if (codeCubeTransform == null || xianTransform == null)
        {
            return;
        }

        // 计算codeCubeTransform的X坐标在-36到44之间的归一化值
        float normalizedXPosition = Mathf.InverseLerp(-36, 44, codeCubeTransform.position.x);

        // 根据归一化值计算旋转角度，范围从0到50度
        float rotationAngle = Mathf.Lerp(30, 80, normalizedXPosition);

        // 应用旋转，围绕Y轴
        xianTransform.localEulerAngles = new Vector3(0, rotationAngle, 0);
    }
}
