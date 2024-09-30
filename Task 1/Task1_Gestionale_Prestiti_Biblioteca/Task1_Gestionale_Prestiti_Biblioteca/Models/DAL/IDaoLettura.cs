using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_Gestionale_Prestiti_Biblioteca.Models.DAL
{
    internal interface IDaoLettura<T>
    {
        List<T> GetAll();
        T GetById(int id);
    }
}
