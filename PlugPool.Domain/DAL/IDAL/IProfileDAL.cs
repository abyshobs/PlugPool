using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL.IDAL
{
    public interface IProfileDAL
    {
        Profile fetchByAccountID(int accountID);
        Profile fetchByJob(int jobID);
        void Create(Profile profile);
        void Update(Profile profile);
        Profile FetchByUsername(string username);
    }
}
