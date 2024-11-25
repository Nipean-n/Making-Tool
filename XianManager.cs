using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class XianManager : MonoBehaviour
{
    public TextMeshPro shuru2TextMeshPro; // Shuru2 的 TextMeshPro 组件的引用
    public GameObject smallXianPrefab; // SmallXian 预制体的引用
    private List<GameObject> smallXians = new List<GameObject>(); // 存储生成的 SmallXian 预制体实例
    private List<GameObject> objectPool = new List<GameObject>(); // 对象池，存储超出Y轴的对象
    private int previousNumber = -1; // 用于存储之前的数字，以便比较是否有变化
    public List<GameObject> GetSmallXians()
    {
        return smallXians;
    }
    void Start()
    {
        // 初始化 SmallXian 预制体，使用 Shuru2 文本组件的当前值
        UpdateSmallXians();
    }

    void Update()
    {
        // 每次更新时检查 Shuru2 文字组件的值是否有变化
        if (CheckForNumberChange())
        {
            // 如果数字变化了，重新生成 SmallXian 实例
            UpdateSmallXians();
        }

        // 管理 SmallXian 实例，超出范围的放入对象池，进入范围的从对象池取出
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
                previousNumber = shuru2Value; // 更新 previousNumber 为当前值
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
            DestroyAllSmallXians(); // 销毁之前生成的所有实例

            int totalXians = (shuru2Value * 499) + 1; // 计算总共需要生成的实例数量
            float startPosY = 0f;
            float endPosY = 64870f;
            float interval = (endPosY - startPosY) / (totalXians - 1); // 计算间隔

            // 生成所有 SmallXian 实例
            for (int i = 0; i < totalXians; i++)
            {
                float yPos = startPosY + i * interval;
                Vector3 position = transform.TransformPoint(new Vector3(0, yPos, 0)); // 使用父物体的当前位置和旋转
                Quaternion rotation = transform.rotation;
                GameObject smallXianInstance = Instantiate(smallXianPrefab, position, rotation, transform);
                smallXians.Add(smallXianInstance);
            }

            // 删除特定索引的实例
            int deleteInterval = shuru2Value - 1; // 删除间隔起始值
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
                // 如果 SmallXian 实例超出范围，放入对象池
                RecycleObject(smallXian);
            }
            else if (!smallXian.activeSelf && (-200 <= smallXian.transform.position.y && smallXian.transform.position.y <= 200))
            {
                // 如果 SmallXian 实例回到范围内，从对象池取出并激活
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