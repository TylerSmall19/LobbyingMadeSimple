using System.Collections.Generic;

namespace LobbyingMadeSimple.Core.Interfaces
{
    public interface IRepoBase<T> where T: class
    {
        T Find(int id);
        List<T> GetAll();
    }
}
