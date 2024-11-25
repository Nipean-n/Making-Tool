using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class XianManager : MonoBehaviour
{
    public TextMeshPro shuru2TextMeshPro; // Shuru2 �� TextMeshPro ���������
    public GameObject smallXianPrefab; // SmallXian Ԥ���������
    private List<GameObject> smallXians = new List<GameObject>(); // �洢���ɵ� SmallXian Ԥ����ʵ��
    private List<GameObject> objectPool = new List<GameObject>(); // ����أ��洢����Y��Ķ���
    private int previousNumber = -1; // ���ڴ洢֮ǰ�����֣��Ա�Ƚ��Ƿ��б仯
    public List<GameObject> GetSmallXians()
    {
        return smallXians;
    }
    void Start()
    {
        // ��ʼ�� SmallXian Ԥ���壬ʹ�� Shuru2 �ı�����ĵ�ǰֵ
        UpdateSmallXians();
    }

    void Update()
    {
        // ÿ�θ���ʱ��� Shuru2 ���������ֵ�Ƿ��б仯
        if (CheckForNumberChange())
        {
            // ������ֱ仯�ˣ��������� SmallXian ʵ��
            UpdateSmallXians();
        }

        // ���� SmallXian ʵ����������Χ�ķ������أ����뷶Χ�ĴӶ����ȡ��
        ManageSmallXians();
    }

    bool CheckForNumberChange()
    {
        if (shuru2TextMeshPro == null)
        {
            Debug.LogError("Shuru2 TextMeshPro component not found.");
            return false;
        }

        int shuru2Value;
        if (int.TryParse(shuru2TextMeshPro.text, out shuru2Value))
        {
            if (previousNumber != shuru2Value)
            {
                previousNumber = shuru2Value; // ���� previousNumber Ϊ��ǰֵ
                return true;
            }
        }
        else
        {
            Debug.LogError("Failed to parse Shuru2 text as integer.");
        }
        return false;
    }

    void UpdateSmallXians()
    {
        int shuru2Value;
        if (int.TryParse(shuru2TextMeshPro.text, out shuru2Value))
        {
            DestroyAllSmallXians(); // ����֮ǰ���ɵ�����ʵ��

            int totalXians = (shuru2Value * 499) + 1; // �����ܹ���Ҫ���ɵ�ʵ������
            float startPosY = 0f;
            float endPosY = 64870f;
            float interval = (endPosY - startPosY) / (totalXians - 1); // ������

            // �������� SmallXian ʵ��
            for (int i = 0; i < totalXians; i++)
            {
                float yPos = startPosY + i * interval;
                Vector3 position = transform.TransformPoint(new Vector3(0, yPos, 0)); // ʹ�ø�����ĵ�ǰλ�ú���ת
                Quaternion rotation = transform.rotation;
                GameObject smallXianInstance = Instantiate(smallXianPrefab, position, rotation, transform);
                smallXians.Add(smallXianInstance);
            }

            // ɾ���ض�������ʵ��
            int deleteInterval = shuru2Value - 1; // ɾ�������ʼֵ
            for (int i = 0; i < smallXians.Count; i += deleteInterval)
            {
                if (i < smallXians.Count)
                {
                    RecycleObject(smallXians[i]);
                    smallXians.RemoveAt(i);
                }
            }
        }
    }

    void ManageSmallXians()
    {
        foreach (var smallXian in smallXians)
        {
            if (smallXian.activeSelf && (smallXian.transform.position.y > 200 || smallXian.transform.position.y < -200))
            {
                // ��� SmallXian ʵ��������Χ����������
                RecycleObject(smallXian);
            }
            else if (!smallXian.activeSelf && (-200 <= smallXian.transform.position.y && smallXian.transform.position.y <= 200))
            {
                // ��� SmallXian ʵ���ص���Χ�ڣ��Ӷ����ȡ��������
                smallXian.SetActive(true);
            }
        }
    }

    void RecycleObject(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Add(obj);
    }

    GameObject GetObjectFromPool()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool[0];
            objectPool.RemoveAt(0);
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    void DestroyAllSmallXians()
    {
        foreach (var smallXian in smallXians)
        {
            Destroy(smallXian);
        }
        smallXians.Clear();
    }
}