using System;
using System.Collections;
using System.Collections.Generic;

namespace LobbyingMadeSimple.Interfaces
{
    public interface IRepoBase<T> where T: class
    {
        T Find(int id);
        List<T> GetAll();
    }
}
