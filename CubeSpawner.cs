using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeSpawner : MonoBehaviour
{
    public GameObject xiansonPrefab; // "Xianson" 预制体
    public Transform xianParent; // "Xian" 父物体的引用
    public Transform code; // Code 立方体的引用
    public int totalCubes = 500; // 要生成的预制体总数

    private float distanceBetweenCubes = 130f; // 预制体之间的初始距离

    void Start()
    {
        // 如果未指定 xianParent，尝试在场景中找到名为 "Xian" 的物体
        if (xianParent == null)
        {
            xianParent = GameObject.Find("Xian").transform;
            if (xianParent == null)
            {
                Debug.LogError("未找到名为 'Xian' 的父物体");
                return;
            }
        }

        // 如果未指定 code，尝试在场景中找到名为 "Code" 的物体
        if (code == null)
        {
            code = GameObject.Find("Code").transform;
            if (code == null)
            {
                Debug.LogError("未找到名为 'Code' 的立方体");
                return;
            }
        }

        // 生成指定数量的 "Xianson" 预制体实例
        for (int i = 0; i < totalCubes; i++)
        {
            // 计算每个预制体的 Y 轴位置
            float yPos = i * distanceBetweenCubes;
            // 实例化预制体并设置其父物体
            GameObject instance = Instantiate(xiansonPrefab, new Vector3(0, yPos, 0), Quaternion.identity, xianParent);
            // 设置 Zi 子物体上的 TextMesh Pro 组件的文本
            SetZiText(instance, i);
        }
    }

    void Update()
    {
        // 更新预制体之间的距离
        UpdateDistanceBetweenCubes();
    }

    void UpdateDistanceBetweenCubes()
    {
        if (code != null)
        {
            // 确保 Code 在指定的 X 轴范围内
            float normalizedXPosition = Mathf.InverseLerp(62, 134, code.position.x);
            // 根据 Code 的 X 轴位置更新 distanceBetweenCubes
            distanceBetweenCubes = Mathf.Lerp(10, 130, normalizedXPosition);
        }
    }

    private void SetZiText(GameObject instance, int index)
    {
        // 查找 Zi 子物体
        Transform ziTransform = instance.transform.Find("Zi");
        if (ziTransform != null)
        {
            // 获取 Zi 子物体上的 TextMesh Pro 组件
            TextMeshPro textMeshPro = ziTransform.GetComponent<TextMeshPro>();
            if (textMeshPro != null)
            {
                // 设置 Zi 子物体上的 TextMesh Pro 组件的文本
                textMeshPro.text = index.ToString();
            }
            else
            {
                Debug.LogError("未在 Zi 子物体上找到 TextMesh Pro 组件", instance);
            }
        }
        else
        {
            Debug.LogError("未找到 Zi 子物体", instance);
        }
    }
}