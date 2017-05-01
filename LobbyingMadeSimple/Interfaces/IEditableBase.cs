using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LobbyingMadeSimple.Interfaces
{
    public interface IEditableBase<T> where T: class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
