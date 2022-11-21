using TPAdmissionTask.Models;

namespace TPAdmissionTask.Interface
{
    public interface IRecords
    {
        public List<RecordModel> GetRecords();
        public RecordModel? GetRecord(int id);
        public void AddRecord(RecordModel record);
        public void UpdateRecord(RecordModel record);
        public RecordModel? DeleteRecord(int id);
        public bool CheckRecord(int id);
    }
}
