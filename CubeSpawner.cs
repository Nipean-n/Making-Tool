using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeSpawner : MonoBehaviour
{
    public GameObject xiansonPrefab; // "Xianson" Ԥ����
    public Transform xianParent; // "Xian" �����������
    public Transform code; // Code �����������
    public int totalCubes = 500; // Ҫ���ɵ�Ԥ��������

    private float distanceBetweenCubes = 130f; // Ԥ����֮��ĳ�ʼ����

    void Start()
    {
        // ���δָ�� xianParent�������ڳ������ҵ���Ϊ "Xian" ������
        if (xianParent == null)
        {
            xianParent = GameObject.Find("Xian").transform;
            if (xianParent == null)
            {
                Debug.LogError("δ�ҵ���Ϊ 'Xian' �ĸ�����");
                return;
            }
        }

        // ���δָ�� code�������ڳ������ҵ���Ϊ "Code" ������
        if (code == null)
        {
            code = GameObject.Find("Code").transform;
            if (code == null)
            {
                Debug.LogError("δ�ҵ���Ϊ 'Code' ��������");
                return;
            }
        }

        // ����ָ�������� "Xianson" Ԥ����ʵ��
        for (int i = 0; i < totalCubes; i++)
        {
            // ����ÿ��Ԥ����� Y ��λ��
            float yPos = i * distanceBetweenCubes;
            // ʵ����Ԥ���岢�����丸����
            GameObject instance = Instantiate(xiansonPrefab, new Vector3(0, yPos, 0), Quaternion.identity, xianParent);
            // ���� Zi �������ϵ� TextMesh Pro ������ı�
            SetZiText(instance, i);
        }
    }

    void Update()
    {
        // ����Ԥ����֮��ľ���
        UpdateDistanceBetweenCubes();
    }

    void UpdateDistanceBetweenCubes()
    {
        if (code != null)
        {
            // ȷ�� Code ��ָ���� X �᷶Χ��
            float normalizedXPosition = Mathf.InverseLerp(62, 134, code.position.x);
            // ���� Code �� X ��λ�ø��� distanceBetweenCubes
            distanceBetweenCubes = Mathf.Lerp(10, 130, normalizedXPosition);
        }
    }

    private void SetZiText(GameObject instance, int index)
    {
        // ���� Zi ������
        Transform ziTransform = instance.transform.Find("Zi");
        if (ziTransform != null)
        {
            // ��ȡ Zi �������ϵ� TextMesh Pro ���
            TextMeshPro textMeshPro = ziTransform.GetComponent<TextMeshPro>();
            if (textMeshPro != null)
            {
                // ���� Zi �������ϵ� TextMesh Pro ������ı�
                textMeshPro.text = index.ToString();
            }
            else
            {
                Debug.LogError("δ�� Zi ���������ҵ� TextMesh Pro ���", instance);
            }
        }
        else
        {
            Debug.LogError("δ�ҵ� Zi ������", instance);
        }
    }
}