using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Common
{
    public class SaveManager
    {
        /**
         * Saves the save data to the disk
         */
        public static void Save(string identifier, object obj)
        {
            var path = GetPath(identifier);

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(path);
            bf.Serialize(file, obj);
            file.Close();
        }

        /**
         * Loads the save data from the disk
         */
        public static T Load<T>(string identifier)
        {
            var path = GetPath(identifier);

            if (File.Exists(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(path, FileMode.Open);
                var obj = (T) bf.Deserialize(file);
                file.Close();
                return obj;
            } else
            {
                throw new Exception("no file saved");
            }
        }

        private static string GetPath(string identifier)
        {
            return string.Format("{0}/{1}.dat", Application.persistentDataPath, identifier);
        }
    }
}