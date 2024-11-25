using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ShuManager : MonoBehaviour
{
    public GameObject shuPrefab; // Shu Ԥ����
    public TextMeshPro shuru2TextMeshPro; // Shuru2 �� TextMeshPro ���
    public Transform targetTransform; // �û�ѡ��������
    public float spacing = 600f; // ��������Shuʵ��֮��ľ���

    private List<GameObject> shuInstances = new List<GameObject>(); // �洢���ɵ� Shu Ԥ����ʵ��
    public List<GameObject> GetShuInstances()
    {
        return shuInstances;
    }
    void Start()
    {
        // ��ʼ������Ԥ����
        InitializeShuInstances();
    }

    void Update()
    {
        // ��� Shuru2 �ı��Ƿ�仯
        if (!string.IsNullOrEmpty(shuru2TextMeshPro.text))
        {
            int shuru2Value;
            // ���Խ����ı�Ϊ����
            if (int.TryParse(shuru2TextMeshPro.text, out shuru2Value))
            {
                // ɾ���������е� Shu Ԥ����
                DestroyAllShuInstances();
                // �����µ� Shu Ԥ����
                InitializeShuInstances(shuru2Value);
            }
        }

        // ÿһ֡������ Shu ʵ����λ��
        UpdateShuInstancesPosition();
    }

    void UpdateShuInstancesPosition()
    {
        for (int i = 0; i < shuInstances.Count; i++)
        {
            // ����ÿ�� Shu ʵ����ƫ���������û�ѡ�������x����Ϊ��׼
            float offset = targetTransform.position.x + (i - (shuInstances.Count - 1) / 2f) * (spacing / (shuInstances.Count - 1));
            // ���� Shu ʵ����λ�ã�������û�ѡ�������λ��
            shuInstances[i].transform.localPosition = new Vector3(offset - targetTransform.position.x, 0, 0);
            // �̳��û�ѡ���������ת
            shuInstances[i].transform.localRotation = Quaternion.identity;
        }
    }

    void InitializeShuInstances(int count = 0)
    {
        // ɾ�����е�Ԥ����
        DestroyAllShuInstances();

        // ���� Shuru2 �ı����������������Ӧ������ Shu Ԥ����
        for (int i = 0; i < count; i++)
        {
            // ����ÿ�� Shu ʵ����ƫ���������û�ѡ�������x����Ϊ��׼
            float offset = targetTransform.position.x + (i - (count - 1) / 2f) * (spacing / (count - 1));
            // ʵ���� Shu Ԥ���岢�����丸����Ϊ�û�ѡ������
            InstantiateShu(new Vector3(offset - targetTransform.position.x, 0, 0), targetTransform);
        }
    }

    void InstantiateShu(Vector3 localPosition, Transform parent)
    {
        // ʵ���� Shu Ԥ����
        GameObject instance = Instantiate(shuPrefab, localPosition, Quaternion.identity, parent);
        // ��ʵ����ӵ��б���
        shuInstances.Add(instance);
    }

    void DestroyAllShuInstances()
    {
        // ɾ������ Shu Ԥ����
        foreach (var instance in shuInstances)
        {
            Destroy(instance);
        }
        // ����б�
        shuInstances.Clear();
    }
}