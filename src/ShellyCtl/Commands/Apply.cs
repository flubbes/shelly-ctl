using System.CommandLine;

public class Apply : Command
{
    public Apply() : base("apply", "Apply a shelly config file")
    {
        var fileConfig = new Option<FileInfo>("-f", "The path to the shelly config file");
        this.AddOption(fileConfig);

        fileConfig.AddValidator(
            (option) =>
            {
                var value = option.GetValueOrDefault<FileInfo>();
                if (value?.Exists == false)
                {
                    option.ErrorMessage = $"File '{value}' doesn't exist";
                }
            }
        );
        this.SetHandler(
            async (FileInfo configPath) =>
            {
                var config = ShellyConfig.FromFile(configPath.FullName);
                if (config.Inventory == null)
                {
                    Console.WriteLine("No inventory found");
                    return;
                }
                var shellyApi = new ShellyApi();
                foreach (var device in config.Inventory)
                {
                    Console.WriteLine($"Processing device {device}");
                    if (config?.Default?.Settings == null)
                    {
                        return;
                    }

                    async Task Handle(Dictionary<object, object> settings, string prefix = "")
                    {
                        foreach (var setting in settings)
                        {
                            if (setting.Value.GetType() == typeof(string))
                            {
                                await shellyApi.Set(
                                    device,
                                    (string)setting.Key,
                                    (string)setting.Value,
                                    prefix
                                );
                            }
                            else
                            {
                                var subObject = (Dictionary<object, object>)setting.Value;
                                await Handle(subObject, $"{prefix}/{setting.Key}");
                            }
                        }
                    }

                    await Handle(config.Default.Settings);
                    Console.WriteLine();
                }
            },
            fileConfig
        );
    }
}
