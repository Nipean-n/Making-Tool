using System.IO;
using UnityEngine;

public class LevelCreative : MonoBehaviour
{
    private void Start()
    {
        // 获取当前脚本文件的目录路径
        string scriptDirectory = Application.dataPath;

        // 构建data.json文件的完整路径
        string jsonFilePath = Path.Combine(scriptDirectory, "Cup", "data.json");

        // 检查JSON文件是否存在
        if (File.Exists(jsonFilePath))
        {
            // 读取JSON文件内容
            string jsonContent = File.ReadAllText(jsonFilePath);

            // 解析JSON文件获取层级名称
            LevelData levelData = JsonUtility.FromJson<LevelData>(jsonContent);

            // 检测层级名称是否为1
            if (levelData.level == 1)
            {
                // 在指定位置生成预制体
                InstantiatePrefab(new Vector3(62.52003f, 37.69924f, -14f));
            }
        }
    }

    private void InstantiatePrefab(Vector3 position)
    {
        // 加载名为"RightNote"的预制体
        GameObject prefab = Resources.Load<GameObject>("RightNote");
        if (prefab != null)
        {
            Instantiate(prefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab 'RightNote' not found!");
        }
    }
}

// JSON文件的C#类映射
[System.Serializable]
public class LevelData
{
    public int level;
}