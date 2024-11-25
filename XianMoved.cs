using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XianMoved : MonoBehaviour
{
    public Transform code3; // Code3 �����������
    public Transform xian; // Xian �����������
    public float centerPosition = -41f; // Xian �������Ŀ������ X ��λ��
    public float range = 300f; // Xian ������� X ��λ�÷�Χ

    void Update()
    {
        if (code3 != null && xian != null)
        {
            // ȷ�� code3 ��λ���� -126 �� 134 ֮��
            if (code3.position.x >= -126 && code3.position.x <= -46)
            {
                // ���� Code3 �� X ��λ�ü��� Xian �� X ��λ��
                float xPosition = Mathf.Lerp(centerPosition - range, centerPosition + range, (code3.position.x + 126) / (-46 + 126));

                // ���� Xian ������� X ��λ��
                xian.position = new Vector3(xPosition, xian.position.y, xian.position.z);
            }
        }
    }
}