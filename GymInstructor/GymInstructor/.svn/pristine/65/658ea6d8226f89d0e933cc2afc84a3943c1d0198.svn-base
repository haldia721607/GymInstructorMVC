using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BOL;
using System.Web;

namespace BLL
{
    public class UserBs
    {
        private UserDb userDb;
        public UserBs()
        {
            userDb = new UserDb();
        }
        public User GetUserByEmail(string userEmail)
        {
            return userDb.GetUserByEmail(userEmail);
        }

        public List<Country> CountryList()
        {
            return userDb.CountryList();
        }
        public List<State> StateList(int countryId)
        {
            return userDb.StateList(countryId);
        }
        public List<City> CityList(int stateId)
        {
            return userDb.CityList(stateId);
        }
        public List<Gender> GenderList()
        {
            return userDb.GenderList();
        }

        public CommanDetails GetUserProfileImage(int userId)
        {
            return userDb.GetUserProfileImage(userId);
        }

        public int Newuser(User user)
        {
           return userDb.NewUser(user);
        }

        public bool GetUserIsActiveOrNot(string userId)
        {
            return userDb.GetUserIsActiveOrNot(userId);
        }

        public bool UpdateUserIsActive(string userId)
        {
            return userDb.UpdateUserIsActive(userId);
        }

        public User FindUsersByEmail(string email)
        {
            return userDb.FindUsersByEmail(email);
        }

        public bool UpdateUserPassword(string password, string confirmPassword, int forgetPassworduserId)
        {
            return userDb.UpdateUserPassword(password,confirmPassword,forgetPassworduserId);
        }

        public bool ChangePassword(string currentPassword,string password,string confirmPassword, int userId)
        {
            return userDb.ChangePassword(currentPassword, password, confirmPassword, userId);
        }

        public GetUserDetail GetUserDetails(int userId)
        {
            return userDb.GetUserDetails(userId);
        }

        public ShowUserDetails GetUserDetailById(int userId)
        {
            return userDb.GetUserDetailById(userId);
        }

        public User UserIsActive(string email)
        {
            return userDb.UserIsActive(email);
        }

        public bool UpdateUserDetail(int userId,UpdateUserDetail userDetail,UpdateUser updateUser, HttpPostedFileBase postedFile)
        {
            return userDb.UpdateUserDetail(userId,userDetail, updateUser, postedFile);
        }

        public int checkUserIdExist(int userId)
        {
            return userDb.checkUserIdExist(userId);
        }

        public bool InsertUserDetail(int userId, UpdateUserDetail userDetail, UpdateUser updateUser, HttpPostedFileBase postedFile)
        {
            return userDb.InsertUserDetail(userId, userDetail, updateUser, postedFile);
        }
    }
}
