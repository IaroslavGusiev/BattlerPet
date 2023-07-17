﻿using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Code.Services.JSONSaver
{
    public class JsonSaver : IJsonSaver
    {
        public bool SaveData<T>(string relativePath, T data)
        {
            string path = Application.persistentDataPath + relativePath;
            try
            {
                if (File.Exists(path))
                    File.Delete(path);

                using FileStream stream = File.Create(path);
                stream.Close();
                File.WriteAllText(path, JsonConvert.SerializeObject(data));
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Enable to save data due to: {e.Message} {e.StackTrace}");
                return false;
            }
        }

        public T LoadData<T>(string relativePath)
        {
            string path = Application.persistentDataPath + relativePath;
            if (!File.Exists(path))
            {
                Debug.Log($"<color=red> Cannot load file at {path}. File doesn't exist. </color>");
                return default;
            }
            
            try
            {
                var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return data;
            }
            catch (Exception e)
            {
                Debug.Log($"<color=red>Failed to load data due to: {e.Message} {e.StackTrace} </color>");
                return default;
            }
        }
    }
}