using System.IO;
using UnityEngine;
public class FileHandler : MonoBehaviour
{
    /// <summary>
    ///  ������ ã�Ҵٸ� true, �������� �ʴٸ� false ��ȯ
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="dataToSave"></param>
    /// <returns></returns>
    public (bool,string) SaveOrLoadFile(string fileName, string dataToSave)
    {
        string path = GetPath(fileName);

        if (File.Exists(path))
        {
            // ������ �����ϸ� �ҷ�����
            string loadedData = LoadFromFile(fileName);
            Debug.Log("Loaded Data: " + loadedData);
            return (false, loadedData);
        }
        else
        {
            // ������ �������� ������ ����
            SaveToFile(fileName, dataToSave);
            Debug.Log("Saved Data: " + dataToSave);
            return (true,dataToSave);
        }
    }

    public void SaveToFile(string fileName, string data)
    {
        string path = GetPath(fileName);
        File.WriteAllText(path, data);
        Debug.Log("Data saved to: " + path);
    }
    public string LoadFromFile(string fileName)
    {
        string path = GetPath(fileName);

        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            Debug.Log("Data loaded from: " + path);
            return data;
        }
        else
        {
            Debug.LogWarning("File not found at: " + path);
            return null;
        }
    }
    private string GetPath(string fileName)
    {
        string path;

#if UNITY_EDITOR
        path = Application.dataPath + "/Resources/";
#elif UNITY_ANDROID
        path = Application.persistentDataPath + "/";
#else
        path = Application.persistentDataPath + "/";
#endif

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        return path + fileName;
    }
}
