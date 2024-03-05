using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
#if UNITY_EDITOR




#else

#endif
    [SerializeField] int round;
    [SerializeField] int round1;
    [SerializeField] int round2;
    [SerializeField] int round3;

    [ContextMenu("save")]
    public void Save()
    {
        Debug.Log("Save");
        if(!Directory.Exists($"{Application.dataPath}/Data"))
        {
            Debug.Log("Data 폴더가 없으니 생성합니다.");
            Directory.CreateDirectory($"{Application.dataPath}/Data");
        }

        string path = Path.Combine(Application.dataPath,"Data/Test.text");
        Debug.Log(path);
        string a = JsonUtility.ToJson(gameObject);
        Debug.Log(a);

    }
    [ContextMenu("Load")]
    public void Load()
    {
        Debug.Log("Load");
        string path = Path.Combine(Application.dataPath, "Data/Test.text");

        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);
            Debug.Log(text);
        }

        else
        {
            Debug.Log("Save File not Exist");
        }
    }
}
