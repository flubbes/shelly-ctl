public class ShellyApi
{
    public int RetryCount { get; set; }

    public async Task<bool> Set(string device, string property, string value, string prefix = "")
    {
        Console.Write($"Setting {prefix}/{property}={value}");
        using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
        try
        {
            var url = $"http://{device}/settings{prefix}";
            var result = await client.PostAsync(url, new StringContent($"{property}={value}"));
            Console.WriteLine($" --- {result.StatusCode}");
            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--- {ex.Message}");
        }
        return false;
    }
}
