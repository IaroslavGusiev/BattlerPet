namespace Code.Services.JSONSaver
{
    public interface ISaver
    {
        bool SaveData<T>(string relativePath, T data);
        T LoadData<T>(string relativePath);
    }
}