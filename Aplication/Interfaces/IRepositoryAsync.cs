using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    //implementamos de ardalis
    //escritura
    public interface IRepositoryAsync<T>:IRepositoryBase<T> where T : class
    {

    }
    //lectura
    public interface IReadRepositoryAsync<T> : IReadRepositoryBase<T> where T :class 
    { 
    
    }
}
