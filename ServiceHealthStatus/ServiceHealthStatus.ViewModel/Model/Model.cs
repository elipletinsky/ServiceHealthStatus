using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel.Model
{
    public static class Model
    {
        public static async Task<IEnumerable<Service>> Load(string fileName)
        {
            try
            {
                using (var reader = File.OpenText(fileName))
                {
                    var json = await reader.ReadToEndAsync();
                    return JsonSerializer.Deserialize<Service[]>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public static IEnumerable<Service> LoadFrom(string fileContent)
        {
            try
            {
                return JsonSerializer.Deserialize<Service[]>(fileContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
