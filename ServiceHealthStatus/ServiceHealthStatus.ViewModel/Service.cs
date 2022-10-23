using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceHealthStatus.ViewModel
{
    public class Service : ServiceItemBase
    {
        public static async Task<IEnumerable<Service>> Load(string fileName)
        {
            try
            {
                var json = await File.ReadAllTextAsync(fileName);
                return JsonSerializer.Deserialize<Service[]>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception e)
            {

                throw;
            }
        }
           

        public Environment[] Environments { get; set; }
    }

    
}
