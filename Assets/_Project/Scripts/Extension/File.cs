using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Object = System.Object;

namespace _Project.Scripts.Extension
{
    public static partial class Common
    {
        public static void SaveFile(this Object data, string path)
        {
            using FileStream file = new FileStream(path, FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(file, data);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
        }

        public static T LoadFile<T>(string path) where T : new()
        {
            T result = new T();
            
            using FileStream file = new FileStream(path, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                result = (T)formatter.Deserialize(file);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }

            return result;
        }
        
        public static void SaveFile2(this Object data, string path)
        {
            if (System.IO.File.Exists(path) == false)
            {
                System.IO.File.Create(path);
            } 
            
            using StreamWriter writer = new StreamWriter(path);
            try
            {
                var json = JsonUtility.ToJson(data);
                writer.Write(json);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
        }

        public static T LoadFile2<T>(string path) where T : new()
        {
            T result = new T();
            try
            {
                var json = System.IO.File.ReadAllText(path);
                result = JsonUtility.FromJson<T>(json);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }

            return result;
        }
    }
}