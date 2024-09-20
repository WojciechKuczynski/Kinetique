namespace Kinetique.Shared.Model.Storage;

public interface IResponseStorage
{
    void Set(string obj, long id);
    long Get(string obj);
}