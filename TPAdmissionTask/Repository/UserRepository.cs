using TPAdmissionTask.Models;
using TPAdmissionTask.Interface;
using Microsoft.EntityFrameworkCore;

namespace TPAdmissionTask.Repository
{
    public class UserRepository : IUsers
    {
        readonly DatabaseContext _db = new DatabaseContext();

        public UserRepository(DatabaseContext db)
        {
            _db = db;
        }

        public void AddUser(UserModel user)
        {
            try
            {
                _db.Users?.Add(user);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckUser(int id)
        {
            if (_db.Users == null)
                return false;

            return _db.Users.Any(e => e.UserId == id);
        }

        public UserModel? DeleteUser(int id)
        {
            try
            {
                if (_db.Users == null)
                    return null;

                UserModel? user = _db.Users.Find(id);

                if (user == null)
                    return null;

                _db.Users.Remove(user);
                _db.SaveChanges();
                return user;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserModel? GetUsers(int id)
        {
            try
            {
                UserModel? user = _db.Users?.Find(id);
                
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<UserModel>? GetUsers()
        {
            try
            {
                if (_db.Users == null)
                    return null;
                else
                    return _db.Users.ToList();
            }
            catch (Exception ex)
            {
                //System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public UserModel? LoginUser(string email, string password)
        {
            try
            {
                email = email.Trim().ToLower();
                password = password.Trim();
                var user = _db.Users!.ToList().Where(x => x.Email == email && x.Password == password);
                return user.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void UpdateUser(UserModel user)
        {
            try
            {
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
