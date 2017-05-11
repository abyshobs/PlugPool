using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL.IDAL
{
    public interface INoteDAL
    {
        void Create(Note note);
        Note FetchByAccount(int id);
        Note FetchByID(int id);
        IEnumerable<Note> FetchAll();
        
    }
}
