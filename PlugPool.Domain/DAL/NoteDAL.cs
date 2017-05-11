using PlugPool.Domain.DAL.IDAL;
using PlugPool.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugPool.Domain.DAL
{
    public class NoteDAL : INoteDAL
    {
        private readonly PlugPoolContext _db;

        public NoteDAL(PlugPoolContext db)
        {
            _db = db;
        }

        public IEnumerable<Note> FetchAll()
        {
            return _db.Notes.ToList();
        }

        public Note FetchByID(int id)
        {
            return _db.Notes.FirstOrDefault(n => n.noteID == id);
        }

        public Note FetchByAccount(int id)
        {
            return _db.Notes.FirstOrDefault(n => n.accountID == id);
        }

        public void Create(Note note)
        {
            _db.Notes.Add(note);
            _db.SaveChanges();
        }

    }
}