using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL
{
    public class JobDAL : IJobDAL
    {
        private readonly PlugPoolContext _db;

        public JobDAL (PlugPoolContext db)
        {
            _db = db;
        }

        public IEnumerable<Job> FetchAll()
        {
            return _db.Jobs.ToList();
        }

        public Job FetchByID(int id)
        {
            return _db.Jobs.FirstOrDefault(j => j.jobID == id);
        }

        public Job FetchByName(string name)
        {
            return _db.Jobs.FirstOrDefault(j => j.name == name);
        }

        //creates a job and adds it to the database
        public void Create(Job job)
        {
            _db.Jobs.Add(job);
            _db.SaveChanges();
        }

        public void Update(Job job)
        {
            Job originalJob = _db.Jobs.Find(job.jobID);
            originalJob.jobID = job.jobID;
            originalJob.name = job.name;
            originalJob.updateDate = DateTime.Now;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Job job = _db.Jobs.Find(id);
            _db.Jobs.Remove(job);
            _db.SaveChanges();
        }
    }
}
