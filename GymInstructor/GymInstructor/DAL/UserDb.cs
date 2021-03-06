using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class UserDb
    {
        private MVCDemoEntities mVCDemoEntities;
        public UserDb()
        {
            mVCDemoEntities = new MVCDemoEntities();
        }
        public void Save()
        {
            mVCDemoEntities.SaveChanges();
        }

        public User GetUserByEmail(string userEmail)
        {
            try
            {
                User user = new User();
                var getUser = (from objUser in mVCDemoEntities.Users where objUser.Email == userEmail && objUser.IsActive == true && objUser.IsBlockByAdmin == false select objUser).FirstOrDefault();
                if (getUser != null)
                {
                    user.UserId = getUser.UserId;
                    user.Name = getUser.Name;
                    user.Email = getUser.Email;
                }
                else
                {
                    user = null;
                }
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CommanDetails GetUserProfileImage(int userId)
        {
            try
            {
                CommanDetails profileImage = new CommanDetails();
                var getUserProfileImage = (from o in mVCDemoEntities.UserDetails
                                           where o.UserId == userId
                                           select new
                                           {
                                               o.Image,
                                               o.ImageName,
                                               o.ImageType
                                           }).FirstOrDefault();
                var getUser = (from o in mVCDemoEntities.Users
                               where o.UserId == userId
                               select new
                               {
                                   o.Name,
                                   o.Email
                               }).FirstOrDefault();
                if (getUser != null)
                {
                    profileImage.Name = getUser.Name;
                    profileImage.Email = getUser.Email;
                }
                if (getUserProfileImage != null)
                {
                    profileImage.Image = getUserProfileImage.Image;
                    profileImage.ImageName = getUserProfileImage.ImageName;
                    profileImage.ImageType = getUserProfileImage.ImageType;
                }
                else
                {
                    profileImage = null;
                }
                return profileImage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ChangePassword(string currentPassword, string password, string confirmPassword, int userId)
        {
            try
            {
                bool IsSuccess = false;
                mVCDemoEntities.Configuration.ValidateOnSaveEnabled = false;
                var checkPassword = (from o in mVCDemoEntities.Users where o.UserId == userId select o).FirstOrDefault();
                if (checkPassword != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(currentPassword, checkPassword.Password))
                    {
                        checkPassword.Password = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
                        checkPassword.ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(confirmPassword, BCrypt.Net.BCrypt.GenerateSalt());
                        checkPassword.ModifyDate = DateTime.Now;
                        mVCDemoEntities.Entry(checkPassword).State = EntityState.Modified;
                        Save();
                        IsSuccess = true;
                    }
                }
                return IsSuccess;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Gender> GenderList()
        {
            try
            {
                List<Gender> genders = new List<Gender>();
                var getGender = (from o in mVCDemoEntities.Genders where o.Status == true select o).ToList();
                if (getGender.Count > 0)
                {
                    foreach (var item in getGender)
                    {
                        Gender gender = new Gender();
                        gender.GenderName = item.GenderName;
                        gender.GenderId = item.GenderId;
                        genders.Add(gender);
                    }
                }
                else
                {
                    genders = null;
                }
                return genders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ShowUserDetails GetUserDetailById(int userId)
        {
            try
            {
                ShowUserDetails showUserDetails = new ShowUserDetails();
                var getUser = (from o in mVCDemoEntities.Users
                               where o.UserId == userId
                               select new
                               {
                                   o.Email,
                                   o.Name
                               }).FirstOrDefault();
                if (getUser != null)
                {
                    UpdateUser upUser = new UpdateUser();
                    upUser.Name = getUser.Name;
                    upUser.Email = getUser.Email;
                    //Add User Name and email
                    showUserDetails.updateUser = upUser;
                }
                else
                {
                    showUserDetails.updateUser = null;
                }
                var getUserDetail = (from o in mVCDemoEntities.UserDetails
                                     where o.UserId == userId
                                     select new
                                     {
                                         o.Address,
                                         o.LandMark,
                                         o.GenderId,
                                         o.CountryId,
                                         o.StateId,
                                         o.CityId,
                                         o.Phone,
                                         o.PostalCode,
                                         o.DOB
                                     }).FirstOrDefault();
                if (getUserDetail != null)
                {
                    UpdateUserDetail upUserDetail = new UpdateUserDetail();
                    upUserDetail.Address = getUserDetail.Address;
                    upUserDetail.LandMark = getUserDetail.LandMark;
                    upUserDetail.GenderId = getUserDetail.GenderId;
                    upUserDetail.CountryId = getUserDetail.CountryId;
                    upUserDetail.StateId = getUserDetail.StateId;
                    upUserDetail.CityId = getUserDetail.CityId;
                    upUserDetail.Phone = getUserDetail.Phone;
                    upUserDetail.PostalCode = getUserDetail.PostalCode;
                    string dob = Convert.ToString(getUserDetail.DOB);
                    string dd = dob.Substring(0, 2);
                    string mm = dob.Substring(3, 2);
                    string yyyy = dob.Substring(6, 4);
                    upUserDetail.DOB = getUserDetail.DOB;//Convert.ToDateTime(dd + "-" + mm + "-" + yyyy);
                    upUserDetail.IsEdit = true;
                    showUserDetails.updateUserDetail = upUserDetail;
                }
                else
                {
                    showUserDetails = null;
                }
                return showUserDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int checkUserIdExist(int userId)
        {
            try
            {
                int userIdExist = 0;
                var getUserId = (from o in mVCDemoEntities.UserDetails where o.UserId == userId select o.UserId).FirstOrDefault();
                if (getUserId != 0)
                {
                    userIdExist = getUserId;
                }
                return userIdExist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateUserDetail(int userId, UpdateUserDetail userDetail, UpdateUser updateUsers, HttpPostedFileBase postedFile)
        {
            try
            {
                bool IsUpdateSuccess = false;
                mVCDemoEntities.Configuration.ValidateOnSaveEnabled = false;
                var updateUser = (from o in mVCDemoEntities.Users where o.UserId == userId select o).FirstOrDefault();
                var updateUserDetail = (from o in mVCDemoEntities.UserDetails where o.UserId == userId select o).FirstOrDefault();
                if (updateUser != null)
                {
                    updateUser.Name = updateUsers.Name;
                    updateUser.Email = updateUsers.Email;
                    updateUser.ModifyDate = DateTime.Now;
                }
                if (updateUserDetail != null)
                {
                    byte[] bytes;
                    if (postedFile != null)
                    {
                        if (postedFile.ContentLength > 0)
                        {
                            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                            {
                                bytes = br.ReadBytes(postedFile.ContentLength);
                            }
                            updateUserDetail.Image = bytes;
                            updateUserDetail.ImageName = Path.GetFileName(postedFile.FileName);
                            updateUserDetail.ImageType = postedFile.ContentType;
                        }
                    }
                    updateUserDetail.Address = userDetail.Address;
                    updateUserDetail.LandMark = userDetail.LandMark;
                    updateUserDetail.GenderId = userDetail.GenderId;
                    updateUserDetail.CountryId = userDetail.CountryId;
                    updateUserDetail.StateId = userDetail.StateId;
                    updateUserDetail.CityId = userDetail.CityId;
                    updateUserDetail.Phone = userDetail.Phone;
                    updateUserDetail.PostalCode = userDetail.PostalCode;
                    updateUserDetail.DOB = Convert.ToDateTime(userDetail.DOB);
                    updateUserDetail.ModifyDate = DateTime.Now;
                    mVCDemoEntities.Entry(updateUser).State = EntityState.Modified;
                    mVCDemoEntities.Entry(updateUserDetail).State = EntityState.Modified;
                    Save();
                    IsUpdateSuccess = true;
                }
                return IsUpdateSuccess;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool InsertUserDetail(int userId, UpdateUserDetail userDetail, UpdateUser updateUserTbl, HttpPostedFileBase postedFile)
        {
            try
            {
                bool IsInsertUserDetailSuccess = false;
                var updateUser = (from o in mVCDemoEntities.Users where o.UserId == userId select o).FirstOrDefault();
                if (updateUser != null)
                {
                    mVCDemoEntities.Configuration.ValidateOnSaveEnabled = false;
                    updateUser.Name = updateUserTbl.Name;
                    updateUser.Email = updateUserTbl.Email;
                    updateUser.ModifyDate = DateTime.Now;
                    mVCDemoEntities.Entry(updateUser).State = EntityState.Modified;
                    Save();
                    mVCDemoEntities.Configuration.ValidateOnSaveEnabled = true;
                }
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                UserDetail InsertUserDetail = new UserDetail();
                InsertUserDetail.ImageName = Path.GetFileName(postedFile.FileName);
                InsertUserDetail.ImageType = postedFile.ContentType;
                InsertUserDetail.Image = bytes;
                InsertUserDetail.UserId = userId;
                InsertUserDetail.Address = userDetail.Address;
                InsertUserDetail.LandMark = userDetail.LandMark;
                InsertUserDetail.GenderId = userDetail.GenderId;
                InsertUserDetail.CountryId = userDetail.CountryId;
                InsertUserDetail.StateId = userDetail.StateId;
                InsertUserDetail.CityId = userDetail.CityId;
                InsertUserDetail.PostalCode = userDetail.PostalCode;
                InsertUserDetail.CreatedDate = DateTime.Now;
                InsertUserDetail.DOB = Convert.ToDateTime(userDetail.DOB);
                mVCDemoEntities.UserDetails.Add(InsertUserDetail);
                Save();
                IsInsertUserDetailSuccess = true;
                return IsInsertUserDetailSuccess;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Country> CountryList()
        {
            try
            {
                List<Country> CountryList = new List<Country>();
                var getCountry = (from o in mVCDemoEntities.Countries select o).ToList();
                if (getCountry.Count > 0)
                {
                    foreach (var item in getCountry)
                    {
                        Country country = new Country();
                        country.ID = item.ID;
                        country.Name = item.Name;
                        CountryList.Add(country);
                    }
                }
                else
                {
                    CountryList = null;
                }
                return CountryList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<State> StateList(int countryId)
        {
            try
            {
                List<State> StateList = new List<State>();
                var getState = (from o in mVCDemoEntities.States where o.CountryID == countryId select o).ToList();
                if (getState.Count > 0)
                {
                    foreach (var item in getState)
                    {
                        State state = new State();
                        state.ID = item.ID;
                        state.Name = item.Name;
                        StateList.Add(state);
                    }
                }
                else
                {
                    StateList = null;
                }
                return StateList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<City> CityList(int stateId)
        {
            try
            {
                List<City> CityList = new List<City>();
                var getCity = (from o in mVCDemoEntities.Cities where o.StateID == stateId select o).ToList();
                if (getCity.Count > 0)
                {
                    foreach (var item in getCity)
                    {
                        City city = new City();
                        city.ID = item.ID;
                        city.Name = item.Name;
                        CityList.Add(city);
                    }
                }
                else
                {
                    CityList = null;
                }
                return CityList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateUserPassword(string password, string confirmPassword, int forgetPassworduserId)
        {
            try
            {
                bool IsUpdateSuccessflly = false;
                var updateUserPassword = (from o in mVCDemoEntities.Users where o.UserId == forgetPassworduserId select o).FirstOrDefault();
                if (updateUserPassword != null)
                {
                    mVCDemoEntities.Configuration.ValidateOnSaveEnabled = false;
                    string newPassword = BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
                    updateUserPassword.Password = newPassword;
                    updateUserPassword.ConfirmPassword = newPassword;
                    updateUserPassword.ModifyDate = DateTime.Now;
                    mVCDemoEntities.Entry(updateUserPassword).State = EntityState.Modified;
                    Save();
                    IsUpdateSuccessflly = true;
                }
                return IsUpdateSuccessflly;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GetUserDetail GetUserDetails(int userId)
        {
            GetUserDetail userDetail = new GetUserDetail();
            var getUserDetail = (from o in mVCDemoEntities.UserDetails
                                 join g in mVCDemoEntities.Genders on o.GenderId equals g.GenderId
                                 join c in mVCDemoEntities.Countries on o.CountryId equals c.ID
                                 join s in mVCDemoEntities.States on o.StateId equals s.ID
                                 join ci in mVCDemoEntities.Cities on o.CityId equals ci.ID
                                 where o.UserId == userId
                                 select new
                                 {
                                     o.UserId,
                                     o.Address,
                                     o.DOB,
                                     g.GenderName,
                                     o.Phone,
                                     CountryName = c.Name != null ? c.Name : null,
                                     o.CountryId,
                                     StateName = s.Name != null ? s.Name : null,
                                     o.StateId,
                                     CityName = ci.Name != null ? ci.Name : null,
                                     o.CityId,
                                     o.PostalCode,
                                     o.LandMark,
                                     o.Image,
                                     o.ImageName,
                                     o.ImageType
                                 }).FirstOrDefault();
            if (getUserDetail != null)
            {
                userDetail.UserId = getUserDetail.UserId;
                userDetail.Address = getUserDetail.Address;
                userDetail.Image = getUserDetail.Image;
                userDetail.ImageName = getUserDetail.ImageName;
                userDetail.ImageType = getUserDetail.ImageType;
                userDetail.DOB = getUserDetail.DOB;
                userDetail.GenderName = getUserDetail.GenderName;
                userDetail.PostalCode = getUserDetail.PostalCode;
                userDetail.Phone = getUserDetail.Phone;
                userDetail.CountryName = getUserDetail.CountryName;
                userDetail.StateName = getUserDetail.StateName;
                userDetail.CityName = getUserDetail.CityName;
                userDetail.LandMark = getUserDetail.LandMark;
            }
            else
            {
                userDetail = null;
            }
            return userDetail;
        }

        public User UserIsActive(string email)
        {
            try
            {
                User user = new User();
                var checkUserIsActive = (from o in mVCDemoEntities.Users where o.Email == email select o).FirstOrDefault();
                if (checkUserIsActive != null)
                {
                    user.UserId = checkUserIsActive.UserId;
                    user.Name = checkUserIsActive.Name;
                    user.Email = checkUserIsActive.Email;
                    user.IsActive = checkUserIsActive.IsActive;
                    user.IsBlockByAdmin = checkUserIsActive.IsBlockByAdmin;
                }
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User FindUsersByEmail(string email)
        {
            User user = new User();
            var resEmail = (from o in mVCDemoEntities.Users where o.Email == email && o.IsBlockByAdmin == false select o).FirstOrDefault();
            if (resEmail != null)
            {
                user.UserId = resEmail.UserId;
                user.Name = resEmail.Name;
                user.Email = resEmail.Email;
            }
            else
            {
                user = null;
            }
            return user;
        }

        public bool UpdateUserIsActive(string userId)
        {
            bool IsSuccessfully = false;
            int Id = Convert.ToInt32(userId);
            var updateUserIsActive = (from o in mVCDemoEntities.Users where o.UserId == Id select o).FirstOrDefault();
            if (updateUserIsActive != null)
            {
                updateUserIsActive.IsActive = true;
                updateUserIsActive.ModifyDate = DateTime.Now;
                mVCDemoEntities.Entry(updateUserIsActive).State = EntityState.Modified;
                mVCDemoEntities.Configuration.ValidateOnSaveEnabled = false;
                Save();
                IsSuccessfully = true;
            }
            return IsSuccessfully;
        }

        public bool GetUserIsActiveOrNot(string userId)
        {
            bool IsActive = false;
            int Id = Convert.ToInt32(userId);
            var getUserIsActive = (from o in mVCDemoEntities.Users where o.UserId == Id select o).FirstOrDefault();
            if (getUserIsActive != null)
            {
                IsActive = getUserIsActive.IsActive;
            }
            return IsActive;
        }

        public int NewUser(User user)
        {
            try
            {
                int userId = 0;
                User newUser = new User();
                newUser.Name = user.Name;
                newUser.Email = user.Email;
                string password = BCrypt.Net.BCrypt.HashPassword(user.Password, BCrypt.Net.BCrypt.GenerateSalt());
                newUser.Password = password;
                newUser.ConfirmPassword = password;
                newUser.IsActive = user.IsActive;
                newUser.IsBlockByAdmin = false;
                newUser.CreatedDate = user.CreatedDate;
                mVCDemoEntities.Users.Add(newUser);
                Save();
                return userId = newUser.UserId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ValidateUser(string username, string password)
        {
            try
            {
                bool IsValid = false;
                var checkUser = (from o in mVCDemoEntities.Users where o.Email == username && o.IsActive == true && o.IsBlockByAdmin == false select o).FirstOrDefault();
                if (checkUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, checkUser.Password))
                    {
                        IsValid = true;
                    }
                }
                return IsValid;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateUserIfDoesNotExist(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
