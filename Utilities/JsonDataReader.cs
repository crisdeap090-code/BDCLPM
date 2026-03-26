
using Newtonsoft.Json;

namespace SeleniumParaBankTest.Utilities;

public static class JsonDataReader
{
    public static List<T> ReadList<T>(string path)
    {
        if (!File.Exists(path))
        {
            return new List<T>();
        }

        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
    }

    public static T? FindByDataSetId<T>(string path, string dataSetId) where T : class
    {
        var items = ReadList<T>(path);
        var prop = typeof(T).GetProperty("DataSetId");
        if (prop == null) return null;

        return items.FirstOrDefault(x =>
        {
            var value = prop.GetValue(x)?.ToString() ?? "";
            return string.Equals(value, dataSetId, StringComparison.OrdinalIgnoreCase);
        });
    }
}
