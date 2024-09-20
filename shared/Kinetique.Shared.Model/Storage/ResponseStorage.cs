namespace Kinetique.Shared.Model.Storage;

public class ResponseStorage : IResponseStorage
{
    private readonly Dictionary<string, long> _idStorage = new();

    public void Set(string obj, long id)
    {
        if (!_idStorage.ContainsKey(obj))
        {
            _idStorage.Add(obj,id);
            return;
        }

        _idStorage[obj] = id;
    }

    public long Get(string obj)
    {
        if (_idStorage.ContainsKey(obj))
        {
            return _idStorage[obj];
        }
        throw new Exception($"id not generated for object {obj}");
    }
}