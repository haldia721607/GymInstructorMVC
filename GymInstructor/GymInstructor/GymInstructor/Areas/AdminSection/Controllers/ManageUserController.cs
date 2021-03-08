using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymInstructor.Areas.AdminSection.Controllers
{
    public class ManageUserController : BaseAdminSectionController
    {
        // GET: AdminSection/ManageUser
        public ActionResult AllUsers(string SortOrder, string SortBy, string Page, string showRecords)
        {
            try
            {
                ViewBag.SortOrder = SortOrder;
                ViewBag.SortBy = SortBy;
                var GetUsers = adminArearBs.adminBs.GetAllUsers();
                switch (SortBy)
                {
                    case "Name":
                        switch (SortOrder)
                        {
                            case "Asc":
                                GetUsers = GetUsers.OrderBy(x => x.Name).ToList();
                                break;
                            case "Desc":
                                GetUsers = GetUsers.OrderByDescending(x => x.Name).ToList();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Email":
                        switch (SortOrder)
                        {
                            case "Asc":
                                GetUsers = GetUsers.OrderBy(x => x.Email).ToList();
                                break;
                            case "Desc":
                                GetUsers = GetUsers.OrderByDescending(x => x.Email).ToList();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Gender":
                        switch (SortOrder)
                        {
                            case "Asc":
                                GetUsers = GetUsers.OrderBy(x => x.Gender).ToList();
                                break;
                            case "Desc":
                                GetUsers = GetUsers.OrderByDescending(x => x.Gender).ToList();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Phone":
                        switch (SortOrder)
                        {
                            case "Asc":
                                GetUsers = GetUsers.OrderBy(x => x.Phone).ToList();
                                break;
                            case "Desc":
                                GetUsers = GetUsers.OrderByDescending(x => x.Phone).ToList();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "Status":
                        switch (SortOrder)
                        {
                            case "Asc":
                                GetUsers = GetUsers.OrderBy(x => x.Status).ToList();
                                break;
                            case "Desc":
                                GetUsers = GetUsers.OrderByDescending(x => x.Status).ToList();
                                break;
                            default:
                                break;
                        }
                        break;
                    case "IsBlock":
                        switch (SortOrder)
                        {
                            case "Asc":
                                GetUsers = GetUsers.OrderBy(x => x.IsBlock).ToList();
                                break;
                            case "Desc":
                                GetUsers = GetUsers.OrderByDescending(x => x.IsBlock).ToList();
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        GetUsers = GetUsers.OrderBy(x => x.Name).ToList();
                        break;
                }
                float NoOfRecords = 0;
                if (showRecords != null)
                {
                    NoOfRecords = 20;//float.Parse(showRecords); 
                    ViewBag.NoOfRecords = NoOfRecords;
                }
                else
                {
                    NoOfRecords = 10;
                }
                ViewBag.TotalPages = Math.Ceiling(adminArearBs.adminBs.GetAllUsers().Count() / NoOfRecords);
                int page = int.Parse(Page == null ? "1" : Page);
                ViewBag.Page = page;
                GetUsers = GetUsers.Skip((page - 1) * Convert.ToInt32(NoOfRecords)).Take(Convert.ToInt32(NoOfRecords)).ToList();
                return View(GetUsers);
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Something went worng." + ex;
                throw;
            }
        }

        [HttpPost]
        public ActionResult UnBlockAllUsers(List<int> unCheckedUserIds)
        {
            try
            {
                adminArearBs.adminBs.UnBlockAllUsers(unCheckedUserIds);
                TempData["Msg"] = "All selected users are un-blocked successfully.";
                var GetUsers = adminArearBs.adminBs.GetAllUsers();
                if (GetUsers != null)
                {
                    return PartialView("pv_AllUsers", GetUsers);
                }
                else
                {
                    return PartialView("pv_AllUsers", null);
                }
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Something went worng" + ex;
                return RedirectToAction("AllUsers", "ManageUser");
                throw;
            }
        }
        [HttpPost]
        public ActionResult BlockAllUsers(List<int> blockCheckedUserIds)
        {
            try
            {
                adminArearBs.adminBs.BlockAllUsers(blockCheckedUserIds);
                TempData["Msg"] = "All selected users are blocked successfully.";
                var GetUsers = adminArearBs.adminBs.GetAllUsers();
                if (GetUsers != null)
                {
                    return PartialView("pv_AllUsers", GetUsers);
                }
                else
                {
                    return PartialView("pv_AllUsers", null);
                }
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Something went worng" + ex;
                return RedirectToAction("AllUsers", "ManageUser");
                throw;
            }
        }
        [HttpPost]
        public ActionResult DeleteAllUers(List<int> Ids)
        {
            try
            {
                adminArearBs.adminBs.DeleteAllUsers(Ids);
                TempData["Msg"] = "All users are deleted successfully.";
                var GetUsers = adminArearBs.adminBs.GetAllUsers();
                if (GetUsers != null)
                {
                    return PartialView("pv_AllUsers", GetUsers);
                }
                else
                {
                    return PartialView("pv_AllUsers", null);
                }
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Something went wrong.Please contact to administrator." + ex;
                return RedirectToAction("AllUsers", "ManageUser");
                throw;
            }
        }
        [HttpPost]
        public ActionResult BlockUser(int blockId)
        {
            try
            {
                bool blockUser = adminArearBs.adminBs.BlockUser(blockId);
                if (blockUser == true)
                {
                    var GetUsers = adminArearBs.adminBs.GetAllUsers();
                    if (GetUsers.Count > 0)
                    {
                        //return Json(GetUsers, JsonRequestBehavior.AllowGet);
                        return PartialView("pv_AllUsers", GetUsers);
                    }
                    else
                    {
                        //return Json(null, JsonRequestBehavior.AllowGet);
                        return RedirectToAction("AllUsers", "ManageUser");
                    }
                }
                else
                {
                    TempData["Fail"] = "Something went wrong.Please contact to administrator.";
                    return RedirectToAction("AllUsers", "ManageUser");
                }
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Something went wrong.Please contact to administrator." + ex;
                return RedirectToAction("AllUsers", "ManageUser");
                throw;
            }
        }
        [HttpPost]
        public ActionResult DeleteUser(int deleteId)
        {
            try
            {
                bool deleteUser = adminArearBs.adminBs.DeleteUser(deleteId);
                if (deleteUser == true)
                {
                    TempData["Msg"] = "Usere deleted successfully";
                    var GetUsers = adminArearBs.adminBs.GetAllUsers();
                    if (GetUsers.Count > 0)
                    {
                        return PartialView("pv_AllUsers", GetUsers);
                    }
                    else
                    {
                        return RedirectToAction("AllUsers", "ManageUser");
                    }
                }
                else
                {
                    TempData["Fail"] = "Something went wrong.Please contact to administrator.";
                    return RedirectToAction("AllUsers", "ManageUser");
                }
            }
            catch (Exception ex)
            {
                TempData["Fail"] = "Something went wrong.Please contact to administrator." + ex;
                return RedirectToAction("AllUsers", "ManageUser");
                throw;
            }
        }
        public void DownloadExcel()
        {
            //var collection = adminArearBs.adminBs.GetAllUsers();


            //ExcelPackage Ep = new ExcelPackage();
            //ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            //Sheet.Cells["A1"].Value = "Name";
            //Sheet.Cells["B1"].Value = "Department";
            //Sheet.Cells["C1"].Value = "Address";
            //Sheet.Cells["D1"].Value = "City";
            //Sheet.Cells["E1"].Value = "Country";
            //int row = 2;
            //foreach (var item in collection)
            //{

            //    Sheet.Cells[string.Format("A{0}", row)].Value = item.Name;
            //    Sheet.Cells[string.Format("B{0}", row)].Value = item.Department;
            //    Sheet.Cells[string.Format("C{0}", row)].Value = item.Address;
            //    Sheet.Cells[string.Format("D{0}", row)].Value = item.City;
            //    Sheet.Cells[string.Format("E{0}", row)].Value = item.Country;
            //    row++;
            //}


            //Sheet.Cells["A:AZ"].AutoFitColumns();
            //Response.Clear();
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            //Response.BinaryWrite(Ep.GetAsByteArray());
            //Response.End();
        }
    }
}