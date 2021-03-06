using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BOL;
using System.Data.Entity;
using System.Activities.Statements;

namespace DAL
{
    public class AdminDb
    {
        private MVCDemoEntities dc;
        public AdminDb()
        {
            dc = new MVCDemoEntities();
        }
        public void Save()
        {
            dc.SaveChanges();
        }

        public bool ValidateUser(string username, string password)
        {
            try
            {
                bool IsValid = false;
                var checkAdminUser = (from o in dc.Admins where o.Email == username && o.Status == true select o).FirstOrDefault();
                if (checkAdminUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, checkAdminUser.Password))
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

        public void UnBlockAllUsers(List<int> unCheckedUserIds)
        {
            try
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                if (unCheckedUserIds.Count > 0)
                {
                    foreach (var item in unCheckedUserIds)
                    {
                        var resBlockAllUsers = (from user in dc.Users where user.UserId == item select user).FirstOrDefault();
                        if (resBlockAllUsers != null)
                        {
                            resBlockAllUsers.IsBlockByAdmin = false;
                            resBlockAllUsers.ModifyDate = DateTime.Now;
                            dc.Entry(resBlockAllUsers).State = EntityState.Modified;
                            Save();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void BlockAllUsers(List<int> checkedIds)
        {
            try
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                if (checkedIds.Count>0)
                {
                    foreach (var item in checkedIds)
                    {
                        var resBlockAllUsers = (from user in dc.Users where user.UserId == item select user).FirstOrDefault();
                        if (resBlockAllUsers != null)
                        {
                            resBlockAllUsers.IsBlockByAdmin = true;
                            resBlockAllUsers.ModifyDate = DateTime.Now;
                            dc.Entry(resBlockAllUsers).State = EntityState.Modified;
                            Save();
                        }
                    }
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                bool IsUserDelete = false;
                dc.Configuration.ValidateOnSaveEnabled = false;
                var deleteUserDetail = (from userDetail in dc.UserDetails where userDetail.UserId == id select userDetail).FirstOrDefault();
                if (deleteUserDetail != null)
                {
                    dc.UserDetails.Remove(deleteUserDetail);
                    dc.Entry(deleteUserDetail).State = EntityState.Deleted;
                }
                var deleteUser = (from user in dc.Users where user.UserId == id select user).FirstOrDefault();
                if (deleteUser != null)
                {
                    dc.Users.Remove(deleteUser);
                    dc.Entry(deleteUser).State = EntityState.Deleted;
                    IsUserDelete = true;
                }
                Save();
                return IsUserDelete;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool BlockUser(int id)
        {
            try
            {
                bool IsBlock = false;
                dc.Configuration.ValidateOnSaveEnabled = false;
                var blockUser = (from user in dc.Users where user.UserId == id select user).FirstOrDefault();
                if (blockUser != null)
                {
                    if (blockUser.IsBlockByAdmin == true)
                    {
                        blockUser.IsBlockByAdmin = false;
                    }
                    else
                    {
                        blockUser.IsBlockByAdmin = true;
                    }
                    blockUser.ModifyDate = DateTime.Now;
                    dc.Entry(blockUser).State = EntityState.Modified;
                    Save();
                    IsBlock = true;
                }
                return IsBlock;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteAllUsers(List<int> Ids)
        {
            try
            {
                dc.Configuration.ValidateOnSaveEnabled = false;
                if (Ids.Count>0)
                {
                    foreach (var item in Ids)
                    {
                        var deleteUserDetail = (from userDetail in dc.UserDetails where userDetail.UserId == item select userDetail).FirstOrDefault();
                        if (deleteUserDetail != null)
                        {
                            dc.UserDetails.Remove(deleteUserDetail);
                            dc.Entry(deleteUserDetail).State = EntityState.Deleted;
                        }
                        var deleteUser = (from user in dc.Users where user.UserId == item select user).FirstOrDefault();
                        if (deleteUser != null)
                        {
                            dc.Users.Remove(deleteUser);
                            dc.Entry(deleteUser).State = EntityState.Deleted;
                        }
                        Save();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<UserDetails> GetAllUsers()
        {
            try
            {
                List<UserDetails> userDetails = new List<UserDetails>();
                var getAllUsers = (from user in dc.Users
                                   join userDetail in dc.UserDetails on user.UserId equals userDetail.UserId
                                   join gender in dc.Genders on userDetail.GenderId equals gender.GenderId
                                   select new
                                   {
                                       user.UserId,
                                       user.Name,
                                       user.Email,
                                       Status = user.IsActive == true ? "Active" : "Not Active",
                                       IsBlockByAdminStatus = user.IsBlockByAdmin == true ? "Blocked" : "Not Blocked",
                                       user.IsBlockByAdmin,
                                       gender.GenderName,
                                       userDetail.Phone
                                   }).ToList();
                if (getAllUsers.Count > 0)
                {
                    foreach (var item in getAllUsers)
                    {
                        UserDetails usDetails = new UserDetails();
                        usDetails.UserId = item.UserId;
                        usDetails.Name = item.Name;
                        usDetails.Email = item.Email;
                        usDetails.Phone = item.Phone;
                        usDetails.Gender = item.GenderName;
                        usDetails.Status = item.Status;
                        usDetails.IsBlock = item.IsBlockByAdminStatus;
                        usDetails.IsBlockByAdmin = item.IsBlockByAdmin;
                        userDetails.Add(usDetails);
                    }
                }
                else
                {
                    userDetails = null;
                }
                return userDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CommanDetails GetProfileImage(int adminId)
        {
            try
            {
                CommanDetails commanDetails = new CommanDetails();
                var getAdminProfileImage = (from o in dc.Admins
                                            where o.AdminId == adminId
                                            select new
                                            {
                                                o.Name,
                                                o.Email,
                                                o.Image,
                                                o.ImageName,
                                                o.ImageType
                                            }).FirstOrDefault();
                if (getAdminProfileImage != null)
                {
                    commanDetails.Name = getAdminProfileImage.Name;
                    commanDetails.Email = getAdminProfileImage.Email;
                    commanDetails.Image = getAdminProfileImage.Image;
                    commanDetails.ImageName = getAdminProfileImage.ImageName;
                    commanDetails.ImageType = getAdminProfileImage.ImageType;
                }
                else
                {
                    commanDetails = null;
                }
                return commanDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateAdminProfile(int adminId, UpdateAdminDetail updateAdminDetail, HttpPostedFileBase httpPostedFileBase)
        {
            try
            {
                bool IsUpdateSuccessfuly = false;
                dc.Configuration.ValidateOnSaveEnabled = false;
                var updateAdmin = (from o in dc.Admins where o.AdminId == adminId select o).FirstOrDefault();
                if (updateAdmin != null)
                {
                    byte[] bytes;
                    if (httpPostedFileBase != null)
                    {
                        if (httpPostedFileBase.ContentLength > 0)
                        {
                            using (BinaryReader br = new BinaryReader(httpPostedFileBase.InputStream))
                            {
                                bytes = br.ReadBytes(httpPostedFileBase.ContentLength);
                            }
                            updateAdmin.Image = bytes;
                            updateAdmin.ImageName = Path.GetFileName(httpPostedFileBase.FileName);
                            updateAdmin.ImageType = httpPostedFileBase.ContentType;
                        }
                    }
                    updateAdmin.Name = updateAdminDetail.Name;
                    updateAdmin.Email = updateAdminDetail.Email;
                    updateAdmin.Phone = updateAdminDetail.Phone;
                    updateAdmin.DOB = Convert.ToDateTime(updateAdminDetail.DOB);
                    updateAdmin.Gender = updateAdminDetail.Gender;
                    dc.Entry(updateAdmin).State = EntityState.Modified;
                    Save();
                    IsUpdateSuccessfuly = true;
                }
                return IsUpdateSuccessfuly;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UpdateAdmin GetAdminDetailById(int adminId)
        {
            try
            {
                UpdateAdmin updateAdmin = new UpdateAdmin();
                var getAdminDetail = (from o in dc.Admins where o.AdminId == adminId select o).FirstOrDefault();
                if (getAdminDetail != null)
                {
                    UpdateAdminDetail upAdminDetail = new UpdateAdminDetail();
                    upAdminDetail.Name = getAdminDetail.Name;
                    upAdminDetail.Email = getAdminDetail.Email;
                    upAdminDetail.Phone = getAdminDetail.Phone;
                    string dob = Convert.ToString(getAdminDetail.DOB);
                    string dd = dob.Substring(0, 2);
                    string mm = dob.Substring(3, 2);
                    string yyyy = dob.Substring(6, 4);
                    upAdminDetail.DOB = Convert.ToDateTime(dd + "-" + mm + "-" + yyyy);
                    upAdminDetail.Gender = getAdminDetail.Gender;
                    upAdminDetail.IsEditAllow = true;
                    updateAdmin.updateAdminDetail = upAdminDetail;
                }
                else
                {
                    updateAdmin.updateAdminDetail = null;
                }
                return updateAdmin;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Admin GetAdminByEmail(string email)
        {
            try
            {
                Admin admin = new Admin();
                var getAdminDetails = (from o in dc.Admins where o.Email == email select o).FirstOrDefault();
                if (getAdminDetails != null)
                {
                    admin.AdminId = getAdminDetails.AdminId;
                    admin.Name = getAdminDetails.Name;
                    admin.Email = getAdminDetails.Email;
                    admin.Image = getAdminDetails.Image;
                    admin.ImageName = getAdminDetails.ImageName;
                    admin.ImageType = getAdminDetails.ImageType;
                }
                else
                {
                    admin = null;
                }
                return admin;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
