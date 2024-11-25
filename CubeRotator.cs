using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotator : MonoBehaviour
{
    public Transform codeCubeTransform; // Code�������Transform����
    public Transform xianTransform; // Xian�������Transform����

    void Update()
    {
        if (codeCubeTransform == null || xianTransform == null)
        {
            return; // ����κ�һ������û�����ã��Ͳ����κβ���
        }

        // ����Code�������λ����62��134֮��ı�׼��ֵ
        float normalizedXPosition = Mathf.InverseLerp(62, 134, codeCubeTransform.position.x);

        // ����Xian������Ӧ����ת�ĽǶȣ���0�ȵ�80��
        float rotationAngle = Mathf.Lerp(30, 70, normalizedXPosition);

        // Ӧ����ת��Χ��X��
        xianTransform.localEulerAngles = new Vector3(rotationAngle, 0, 0);
    }
}
