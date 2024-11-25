using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    public Transform codeCubeTransform; // Code立方体的Transform引用
    public Transform xianTransform; // Xian立方体的Transform引用

    void Update()
    {
        if (codeCubeTransform == null || xianTransform == null)
        {
            return; // 如果任何一个引用没有设置，就不做任何操作
        }

        // 计算Code立方体的位置在62到134之间的标准化值
        float normalizedXPosition = Mathf.InverseLerp(62, 134, codeCubeTransform.position.x);

        // 计算Xian立方体应该旋转的角度，从0度到80度
        float rotationAngle = Mathf.Lerp(30, 70, normalizedXPosition);

        // 应用旋转，围绕X轴
        xianTransform.localEulerAngles = new Vector3(rotationAngle, 0, 0);
    }
}
