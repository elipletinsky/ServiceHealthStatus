using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel.Model
{
    public class Service : ServiceItemBase
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

        public static IEnumerable<Service> LoadFrom(string fileName)
        {
            try
            {
                return JsonSerializer.Deserialize<Service[]>(fileName, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public Environment[] Environments { get; set; }
    }


}
