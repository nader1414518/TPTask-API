using TPAdmissionTask.Models;
using TPAdmissionTask.Interface;
using Microsoft.EntityFrameworkCore;

namespace TPAdmissionTask.Repository
{
    public class RecordRepository : IRecords
    {
        readonly DatabaseContext _db = new DatabaseContext();

        public RecordRepository(DatabaseContext db)
        {
            _db = db;
        }

        public void AddRecord(RecordModel record)
        {
            try
            {
                _db.Records?.Add(record);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckRecord(int id)
        {
            if (_db.Records == null)
                return false;

            return _db.Records.Any(e => e.RecordId == id);
        }

        public RecordModel? DeleteRecord(int id)
        {
            try
            {
                if (_db.Records == null)
                    return null;

                RecordModel? record = _db.Records.Find(id);

                if (record == null)
                    return null;

                _db.Records.Remove(record);
                _db.SaveChanges();
                return record;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public RecordModel? GetRecord(int id)
        {
            try
            {
                RecordModel? record = _db.Records?.Find(id);
                
                return record;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<RecordModel> GetRecords()
        {
            try
            {
                if (_db.Records == null)
                    return new List<RecordModel>();
                else
                    return _db.Records.ToList();
            }
            catch (Exception)
            {
                return new List<RecordModel>();
            }
        }

        public void UpdateRecord(RecordModel record)
        {
            try
            {
                _db.Entry(record).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
