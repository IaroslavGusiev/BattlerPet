namespace Code.Services.JSONSaver
{
    public interface IJsonSaver
    {
        bool SaveData<T>(string relativePath, T data);
        T LoadData<T>(string relativePath);
    }
}