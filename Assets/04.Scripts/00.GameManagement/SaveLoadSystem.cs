using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SaveDataVC = SaveDataV3;

public static class SaveLoadSystem
{
    public static int SaveDataVersion { get; } = 3;
    public static string SaveDirectory
    {
        get
        {
            return $"{Application.persistentDataPath}/save";
        }
    }

    public static void Save(SaveData data, string fileName)
    {
        if(!Directory.Exists(SaveDirectory)) 
        {
            Directory.CreateDirectory(SaveDirectory);
        }

        var path = Path.Combine(SaveDirectory, fileName);
        using (var writer = new JsonTextWriter(new StreamWriter(path)))
        {
            var serialize = new JsonSerializer();
            //serialize.Converters.Add();            
            serialize.Serialize(writer, data);
        }
    }

    public static SaveData Load(string fileName)
    {
        var path = Path.Combine(SaveDirectory, fileName);
        if(!File.Exists(path)) 
            return null;

        SaveData data = null;
        int version = 0;

        var json = File.ReadAllText(path);
        using (var reader = new JsonTextReader(new StringReader(json)))
        {
            var jObj = JObject.Load(reader);
            version = jObj["Version"].Value<int>();
        }
        using (var reader = new JsonTextReader(new StringReader(json)))
        { 
            var deserialize = new JsonSerializer();
            deserialize.Converters.Add(new Vector3Converters());
            deserialize.Converters.Add(new QuaternionConverters());
            switch (version)
            {
                case 1:                
                    data = deserialize.Deserialize<SaveDataV1>(reader);
                    break;
                case 2:
                    data = deserialize.Deserialize<SaveDataV2>(reader);
                    break;
                case 3:
                    data = deserialize.Deserialize<SaveDataV3>(reader);
                    break;
            }

            while(data.Version < SaveDataVersion) 
            {
                var oldVer = data;
                data = data.VersionUp();
                version++;
            }
        }
        return data;
    }
}
