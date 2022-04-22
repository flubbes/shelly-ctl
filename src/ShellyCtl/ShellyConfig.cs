using System.Collections.Generic;
using System.IO;
using YamlDotNet;
using YamlDotNet.Serialization;

public class ShellyConfig
{
    public static ShellyConfig FromFile(string path)
    {
        var content = File.ReadAllText(path);
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(
                YamlDotNet.Serialization.NamingConventions.CamelCaseNamingConvention.Instance
            )
            .Build();
        return deserializer.Deserialize<ShellyConfig>(content);
    }

    public string[]? Inventory { get; set; }

    public ShellySettings? Default { get; set; }
}

public class ShellySettings
{
    public Dictionary<object, object>? Settings { get; set; }
}
