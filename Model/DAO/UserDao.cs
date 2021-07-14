using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class UserDao
    {
        PetStoreDbContext db = null;
        public UserDao()
        {
            db = new PetStoreDbContext();
        }    
        //Insert
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        //getById
        public User getByUserName(string username)
        {
            return db.Users.SingleOrDefault(x => x.Username == username);
        }
        public User getUserById(int id)
        {
            return db.Users.Find(id);
        }
        //Update
        public int Update(User entity)
        {
            User userUpdate = db.Users.SingleOrDefault(x => x.Id == entity.Id);
            if(userUpdate != null)
            {
                db.Users.AddOrUpdate(entity);
                db.SaveChanges();
            }
            return entity.Id;
        }
        //Delete
        //
        public int Login(string userName, string passWord, int role)
        {
            var result = db.Users.SingleOrDefault(x => x.Username == userName && x.Role==role);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                    return -1;
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }    
            }    
        }
        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.Username == userName && x.Role!=4);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Status == false)
                    return -1;
                else
                {
                    if (result.Password == passWord)
                        return 1;
                    else
                        return -2;
                }
            }

        }
    }
}
