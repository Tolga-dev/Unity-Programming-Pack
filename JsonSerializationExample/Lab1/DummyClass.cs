using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Chest.ChestManagers;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace JsonSerializationExample.Lab1
{
    public class DummyClass : MonoBehaviour
    {
        private PlayerInformation _information = new PlayerInformation();
        
        private void Start()
        {

            _information.lvl = 1;
            _information.name = "hello world";
            _information.time = 5f;

            DummyItems item = new DummyItems()
            {
                ItemName = "hell",
                DummyEnumType = DummyEnum.Dum
            };
            DummyItems item2 = new DummyItems()
            {
                ItemName = "hell2",
                DummyEnumType = DummyEnum.Bum
            };
            
            _information.ItemsList.Add(item);
            _information.ItemsList.Add(item2);
            
            var json = JsonUtility.ToJson(_information);
            _information = JsonUtility.FromJson<PlayerInformation>(json);
            
            Debug.Log(_information);
            var fileName = "anan";
            Debug.Log(GetPath(ref fileName));
            Debug.Log(json);
            foreach (var dummyItem in _information.ItemsList)
            {
                Debug.Log($"{dummyItem.ItemName} , {dummyItem.DummyEnumType}");
            }
            
        }

        [SerializeField] InputField nameInput;
        [SerializeField] string Filename;
        List<InputEntry> entries = new List<InputEntry> ();
        
        public void ReadItemFromFile()
        {
            entries = FileHandler.ReadListFromJSON<InputEntry> (Filename);
        }

        public void AddNameToList ()
        {
            Random random = new Random();
            
            entries.Add (new InputEntry (nameInput.text, random.Next(0, 100)));
            nameInput.text = "";

            FileHandler.SaveToJSON<InputEntry> (entries, Filename);
        }

        public void SaveToJSON<T> (List<T> toSave,ref  string filename) {
            var message = GetPath (ref filename);
            Debug.Log (message);
            
            string content = JsonHelper.ToJson<T> (toSave.ToArray ());
            WriteFile (ref message, ref content);
        }

        public void SaveToJSON<T> (T toSave, ref string filename) {
            string content = JsonUtility.ToJson (toSave);
            var path = GetPath(ref filename);
            WriteFile (ref path, ref content);
        }
        
        private List<T> ReadListFromJSON<T>(ref string fileName)
        {
            var filePath = GetPath(ref fileName);
            var content = ReadFile(ref filePath);

            if (string.IsNullOrEmpty(content) || content == "{}")
                return new List<T>();

            List<T> res = JsonHelper.FromJson<T>(ref content).ToList();
            
            return res;

        }

        public T ReadFromJson<T>(ref string fileName)
        {
            var filePath = GetPath(ref fileName);
            var content = ReadFile(ref filePath);

            if (string.IsNullOrEmpty(content) || content == "{}")
                return default(T);

            T res = JsonUtility.FromJson<T>(content);
            
            return res;
        }

        private bool WriteFile(ref string path, ref string json)
        {
            var fileStream = new FileStream(path, FileMode.Create);

            using (var write = new StreamWriter(fileStream))
            {
                write.Write(json);
            }
            
            return true;
        }
        
        private string ReadFile(ref string filePath)
        {
            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    var content = reader.ReadToEnd();
                    return content;
                }
            }

            return "";
        }

        private string GetPath(ref string fileName)
        {
            var data = Application.persistentDataPath + "/" + fileName;
            Debug.Log($"{data}");
            return data;
        }
        
    }

    [Serializable]
    public class PlayerInformation
    {
        public int lvl;
        public float time;
        public string name;
        
        public List<DummyItems> ItemsList = new List<DummyItems>();
    }
    
    [Serializable]
    public class DummyItems
    {
        public string ItemName;
        public DummyEnum DummyEnumType;
    }
    [Serializable]
    public enum DummyEnum
    {
        Dum,
        Bum
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(ref string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T> (T[] array) {
        Wrapper<T> wrapper = new Wrapper<T> ();
        wrapper.Items = array;
        return JsonUtility.ToJson (wrapper);
    }

    public static string ToJson<T> (T[] array, bool prettyPrint) {
        Wrapper<T> wrapper = new Wrapper<T> ();
        wrapper.Items = array;
        return JsonUtility.ToJson (wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}


