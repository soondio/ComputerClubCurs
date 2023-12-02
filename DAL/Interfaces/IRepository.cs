﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T:class
    {
        List<T> GetList(); // получение всех объектов
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);

    }
}
