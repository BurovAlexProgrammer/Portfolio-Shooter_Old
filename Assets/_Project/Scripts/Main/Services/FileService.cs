using System.IO;
using _Project.Scripts.Main.AppServices.Base;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Main.AppServices
{
    [UsedImplicitly]
    public class FileService : IFileService
    {
        private const string StoredFolder = "StoredData";
        
        public string StorageFolder => Application.platform switch
        {
            RuntimePlatform.IPhonePlayer => Path.Combine(Application.persistentDataPath, StoredFolder),
            RuntimePlatform.Android => Path.Combine(Application.temporaryCachePath, StoredFolder),
            _ => Path.Combine(Application.dataPath, StoredFolder)
        };
    }
}