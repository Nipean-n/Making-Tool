using System.IO;
using UnityEngine;

public class LevelCreative : MonoBehaviour
{
    private void Start()
    {
        // ��ȡ��ǰ�ű��ļ���Ŀ¼·��
        string scriptDirectory = Application.dataPath;

        // ����data.json�ļ�������·��
        string jsonFilePath = Path.Combine(scriptDirectory, "Cup", "data.json");

        // ���JSON�ļ��Ƿ����
        if (File.Exists(jsonFilePath))
        {
            // ��ȡJSON�ļ�����
            string jsonContent = File.ReadAllText(jsonFilePath);

            // ����JSON�ļ���ȡ�㼶����
            LevelData levelData = JsonUtility.FromJson<LevelData>(jsonContent);

            // ���㼶�����Ƿ�Ϊ1
            if (levelData.level == 1)
            {
                // ��ָ��λ������Ԥ����
                InstantiatePrefab(new Vector3(62.52003f, 37.69924f, -14f));
            }
        }
    }

    private void InstantiatePrefab(Vector3 position)
    {
        // ������Ϊ"RightNote"��Ԥ����
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

// JSON�ļ���C#��ӳ��
[System.Serializable]
public class LevelData
{
    public int level;
}