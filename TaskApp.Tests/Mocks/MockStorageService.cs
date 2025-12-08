using System.Collections.Generic;
using TaskApp.Services;

public class MockStorageService : IStorageService
{
    // Store fake in-memory JSON data
    public List<object> FakeData = new List<object>();

    public List<T> Load<T>(string filePath)
    {
        // Convert stored objects to requested type (T)
        var typedList = new List<T>();

        foreach (var item in FakeData)
        {
            if (item is T typedItem)
                typedList.Add(typedItem);
        }

        return typedList;
    }

    public void Save<T>(string filePath, List<T> data)
    {
        FakeData.Clear();

        foreach (var item in data)
            FakeData.Add(item);
    }
}
