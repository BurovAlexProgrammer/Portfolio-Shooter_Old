using System.IO;
using JetBrains.Annotations;
using UnityEngine;

namespace Main.Services
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