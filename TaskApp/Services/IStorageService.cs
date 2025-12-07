using System.Collections.Generic;

namespace TaskApp.Services
{
    public interface IStorageService
    {
        List<T> Load<T>(string filePath);
        void Save<T>(string filePath, List<T> data);
    }
}
