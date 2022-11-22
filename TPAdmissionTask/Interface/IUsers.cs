using TPAdmissionTask.Models;

namespace TPAdmissionTask.Interface
{
    public interface IUsers
    {
        public List<UserModel>? GetUsers();
        public UserModel? GetUsers(int id);
        public void AddUser(UserModel record);
        public void UpdateUser(UserModel record);
        public UserModel? DeleteUser(int id);
        public bool CheckUser(int id);
        public UserModel? LoginUser(string email, string password);
    }
}
