using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ShuManager : MonoBehaviour
{
    public GameObject shuPrefab; // Shu 预制体
    public TextMeshPro shuru2TextMeshPro; // Shuru2 的 TextMeshPro 组件
    public Transform targetTransform; // 用户选定的物体
    public float spacing = 600f; // 两个相邻Shu实例之间的距离

    private List<GameObject> shuInstances = new List<GameObject>(); // 存储生成的 Shu 预制体实例
    public List<GameObject> GetShuInstances()
    {
        return shuInstances;
    }
    void Start()
    {
        // 初始化生成预制体
        InitializeShuInstances();
    }

    void Update()
    {
        // 检查 Shuru2 文本是否变化
        if (!string.IsNullOrEmpty(shuru2TextMeshPro.text))
        {
            int shuru2Value;
            // 尝试解析文本为整数
            if (int.TryParse(shuru2TextMeshPro.text, out shuru2Value))
            {
                // 删除所有现有的 Shu 预制体
                DestroyAllShuInstances();
                // 生成新的 Shu 预制体
                InitializeShuInstances(shuru2Value);
            }
        }

        // 每一帧都更新 Shu 实例的位置
        UpdateShuInstancesPosition();
    }

    void UpdateShuInstancesPosition()
    {
        for (int i = 0; i < shuInstances.Count; i++)
        {
            // 计算每个 Shu 实例的偏移量，以用户选定物体的x坐标为基准
            float offset = targetTransform.position.x + (i - (shuInstances.Count - 1) / 2f) * (spacing / (shuInstances.Count - 1));
            // 设置 Shu 实例的位置，相对于用户选定物体的位置
            shuInstances[i].transform.localPosition = new Vector3(offset - targetTransform.position.x, 0, 0);
            // 继承用户选定物体的旋转
            shuInstances[i].transform.localRotation = Quaternion.identity;
        }
    }

    void InitializeShuInstances(int count = 0)
    {
        // 删除现有的预制体
        DestroyAllShuInstances();

        // 根据 Shuru2 文本组件的数字生成相应数量的 Shu 预制体
        for (int i = 0; i < count; i++)
        {
            // 计算每个 Shu 实例的偏移量，以用户选定物体的x坐标为基准
            float offset = targetTransform.position.x + (i - (count - 1) / 2f) * (spacing / (count - 1));
            // 实例化 Shu 预制体并设置其父物体为用户选定物体
            InstantiateShu(new Vector3(offset - targetTransform.position.x, 0, 0), targetTransform);
        }
    }

    void InstantiateShu(Vector3 localPosition, Transform parent)
    {
        // 实例化 Shu 预制体
        GameObject instance = Instantiate(shuPrefab, localPosition, Quaternion.identity, parent);
        // 将实例添加到列表中
        shuInstances.Add(instance);
    }

    void DestroyAllShuInstances()
    {
        // 删除所有 Shu 预制体
        foreach (var instance in shuInstances)
        {
            Destroy(instance);
        }
        // 清空列表
        shuInstances.Clear();
    }
}