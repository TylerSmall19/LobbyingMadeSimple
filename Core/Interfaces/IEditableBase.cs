namespace LobbyingMadeSimple.Core.Interfaces
{
    public interface IEditableBase<T> where T: class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
