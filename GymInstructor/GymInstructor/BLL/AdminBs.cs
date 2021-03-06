using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class AdminBs
    {
        private AdminDb adminDb;
        public AdminBs()
        {
            adminDb = new AdminDb();
        }
        public Admin GetAdminByEmail(string email)
        {
            return adminDb.GetAdminByEmail(email);
        }

        public List<UserDetails> GetAllUsers()
        {
            return adminDb.GetAllUsers();
        }

        public UpdateAdmin GetAdminDetailById(int adminId)
        {
            return adminDb.GetAdminDetailById(adminId);
        }

        public bool UpdateAdminProfile(int adminId, UpdateAdminDetail updateAdminDetail, HttpPostedFileBase httpPostedFileBase)
        {
            return adminDb.UpdateAdminProfile(adminId, updateAdminDetail, httpPostedFileBase);
        }

        public void UnBlockAllUsers(List<int> unCheckedUserIds)
        {
            adminDb.UnBlockAllUsers(unCheckedUserIds);
        }

        public void BlockAllUsers(List<int> checkedIds)
        {
            adminDb.BlockAllUsers(checkedIds);
        }

        public CommanDetails GetProfileImage(int adminId)
        {
            return adminDb.GetProfileImage(adminId);
        }

        public void DeleteAllUsers(List<int> ids)
        {
            adminDb.DeleteAllUsers(ids);
        }

        public bool BlockUser(int id)
        {
            return adminDb.BlockUser(id);
        }

        public bool DeleteUser(int id)
        {
            return adminDb.DeleteUser(id);
        }
    }
}
