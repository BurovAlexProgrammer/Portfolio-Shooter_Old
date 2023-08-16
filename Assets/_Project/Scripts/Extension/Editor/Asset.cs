using System;
using System.Linq;
using UnityEditor;

namespace Main.Extension.Editor
{
    public static partial class Common
    {
        public static T LoadSingleAsset<T>() where T : class
        {
            var tName = typeof(T).Name;
            var settingGUIDs = AssetDatabase.FindAssets("t:" + tName);

            if (settingGUIDs.Length > 1)
            {
                throw new Exception("More than one asset of type " + typeof(T).Name);
            }

            var settingGuid = settingGUIDs.Single();
            return AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(settingGuid)) as T;
        }
    }
}