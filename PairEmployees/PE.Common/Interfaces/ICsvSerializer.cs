using Common.Models;

namespace PE.Common.Interfaces
{
    public interface ICsvSerializer
    {
        IEnumerable<T> DeserializeAll<T>(Stream stream);
    }
}