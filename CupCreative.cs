using System.IO;
using System.Text;
using UnityEngine;

public class CupCreative : MonoBehaviour
{
    private void Start()
    {
        // ��ȡ��ǰ�ű��ļ���Ŀ¼·��
        string scriptDirectory = Application.dataPath;

        // ����Cup�ļ��е�����·��
        string cupFolderPath = Path.Combine(scriptDirectory, "Cup");

        // ����ļ����Ƿ���ڣ�����������򴴽�
        if (!Directory.Exists(cupFolderPath))
        {
            Directory.CreateDirectory(cupFolderPath);
        }

        // ����data.json�ļ�������·��
        string jsonFilePath = Path.Combine(cupFolderPath, "data.json");

        // ���JSON�ļ��Ƿ���ڣ�����������򴴽�
        if (!File.Exists(jsonFilePath))
        {
            // ����һ������"level"����JSON�ļ����㼶����Ϊ1
            string jsonContent = "{\"level\": 1}";
            File.WriteAllText(jsonFilePath, jsonContent, Encoding.UTF8);
        }
    }
}