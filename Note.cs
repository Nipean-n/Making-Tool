using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public GameObject prefab; // 第一个要生成的预制体
    public GameObject secondPrefab; // 第二个要生成的预制体
    private GameObject spawnedObject; // 生成的第一个预制体实例

    void Update()
    {
        // 检查鼠标左键是否被按下
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标下方的游戏对象
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject) // 确保射线检测到的是挂载此脚本的物体
                {
                    // 实例化第一个预制体
                    spawnedObject = Instantiate(prefab, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
                }
            }
        }
        // 检查鼠标左键是否被释放
        else if (Input.GetMouseButtonUp(0) && spawnedObject != null)
        {
            // 删除生成的第一个预制体
            Destroy(spawnedObject);
            spawnedObject = null; // 重置生成的第一个预制体实例引用

            // 在指定坐标处实例化第二个预制体
            Vector3 secondPrefabPosition = new Vector3(96.5f, -14.6f, -3f);
            GameObject secondSpawnedObject = Instantiate(secondPrefab, secondPrefabPosition, Quaternion.identity);
        }
    }
}