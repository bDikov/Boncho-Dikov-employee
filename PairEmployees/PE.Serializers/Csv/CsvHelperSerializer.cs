using CsvHelper;
using PE.Common.Interfaces;
using CsvHelper.Configuration;
using System.Globalization;

namespace PE.Serializers.Csv
{
    public class CsvHelperSerializer : ICsvSerializer
    {
        public IEnumerable<T> DeserializeAll<T>(Stream stream)
        {
            var data = new List<T>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                NewLine = Environment.NewLine
            };

            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, config))
            {
                // return new InternalResult<T>(csv.GetRecords<T>());
                data = csv.GetRecords<T>().ToList();
            }
            return data;
        }
    }
}