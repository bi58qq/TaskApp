using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TaskApp.Services
{
    public class JsonStorageService : IStorageService
    {
        public List<T> Load<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return new List<T>();

                string json = File.ReadAllText(filePath);

                return JsonConvert.DeserializeObject<List<T>>(json)
                       ?? new List<T>();
            }
            catch (JsonException)
            {
                Console.WriteLine("JSON file corrupted! Creating backup...");
                File.Copy(filePath, filePath + ".bak", overwrite: true);
                return new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
                return new List<T>();
            }
        }

        public void Save<T>(string filePath, List<T> data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data: " + ex.Message);
            }
        }
    }
}
