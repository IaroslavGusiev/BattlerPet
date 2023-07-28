using System;
using System.IO;
using Code.Data;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;

namespace Code.Services.JSONSaver
{
    public class JsonSaver : ISaver
    {
        public async UniTask SaveData<T>(string relativePath, T data)
        {
            string path = GetPath(relativePath);
            try
            {
                if (File.Exists(path))
                    File.Delete(path);

                await using FileStream stream = File.Create(path);
                stream.Close();
                await File.WriteAllTextAsync(path, JsonConvert.SerializeObject(data));
            }
            catch (Exception e)
            {
                Debug.LogError($"Enable to save data due to: {e.Message} {e.StackTrace}");
            }
        }

        public async UniTask<T> LoadData<T>(string relativePath)
        {
            string path = GetPath(relativePath);
            if (!File.Exists(path))
            {
                Debug.Log($"<color=red> Cannot load file at {path}. File doesn't exist. </color>");
                return default;
            }
            try
            {
                string data = await File.ReadAllTextAsync(path).AsUniTask();
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception e)
            {
                Debug.Log($"<color=red>Failed to load data due to: {e.Message} {e.StackTrace} </color>");
                return default;
            }
        }

        private string GetPath(string relativePath) => 
            Application.persistentDataPath + relativePath;

        public static void ClearAllData()
        {
            foreach (string path in SavedKeysData.AllKeys
                    .Select(key => Application.persistentDataPath + key)
                    .Where(File.Exists))
            {
                File.Delete(path);
                Debug.Log($"<color=green> Delete file at path { path } </color>");
            }
        }
    }
}