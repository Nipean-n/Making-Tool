using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XianRotator : MonoBehaviour
{
    public Transform codeCubeTransform; // Code�������Transform����
    public Transform xianTransform; // Xian�������Transform����

    void Update()
    {
        // �������û�����ã���ִ���κβ���
        if (codeCubeTransform == null || xianTransform == null)
        {
            return;
        }

        // ����codeCubeTransform��X������-36��44֮��Ĺ�һ��ֵ
        float normalizedXPosition = Mathf.InverseLerp(-36, 44, codeCubeTransform.position.x);

        // ���ݹ�һ��ֵ������ת�Ƕȣ���Χ��0��50��
        float rotationAngle = Mathf.Lerp(30, 80, normalizedXPosition);

        // Ӧ����ת��Χ��Y��
        xianTransform.localEulerAngles = new Vector3(0, rotationAngle, 0);
    }
}
