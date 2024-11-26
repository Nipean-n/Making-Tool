using System.IO;
using System.Text;
using UnityEngine;

public class CupCreative : MonoBehaviour
{
    private void Start()
    {
        // 获取当前脚本文件的目录路径
        string scriptDirectory = Application.dataPath;

        // 构建Cup文件夹的完整路径
        string cupFolderPath = Path.Combine(scriptDirectory, "Cup");

        // 检查文件夹是否存在，如果不存在则创建
        if (!Directory.Exists(cupFolderPath))
        {
            Directory.CreateDirectory(cupFolderPath);
        }

        // 构建data.json文件的完整路径
        string jsonFilePath = Path.Combine(cupFolderPath, "data.json");

        // 检查JSON文件是否存在，如果不存在则创建
        if (!File.Exists(jsonFilePath))
        {
            // 创建一个包含"level"键的JSON文件，层级名称为1
            string jsonContent = "{\"level\": 1}";
            File.WriteAllText(jsonFilePath, jsonContent, Encoding.UTF8);
        }
    }
}