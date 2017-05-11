using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL.IDAL
{
    public interface IJobDAL
    {
        IEnumerable<Job> FetchAll();
        Job FetchByID(int id);
        Job FetchByName(string name);
        void Create(Job job);
        void Update(Job job);
        void Delete(int id);
    }
}
