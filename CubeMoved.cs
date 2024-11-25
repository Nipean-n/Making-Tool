using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMoved : MonoBehaviour
{
    public Transform code1; // Code1 �����������
    public Transform xian; // Xian �����������
    public float minY = -8000f; // Xian ���������С Y ��λ��
    public float maxY = 0f; // Xian ���������� Y ��λ��

    void Update()
    {
        if (code1 != null && xian != null)
        {
            // ȷ�� code1 ��λ���� 62 �� 134 ֮��
            if (code1.position.x >= 62 && code1.position.x <= 134)
            {
                // ���� Code1 �� X ��λ�ü��� Xian �� Y ��λ��
                float yPosition = Mathf.Lerp(minY, maxY, (code1.position.x - 62) / (134 - 62));

                // ���� Xian ������� Y ��λ��
                xian.position = new Vector3(xian.position.x, yPosition, xian.position.z);
            }
        }
    }
}