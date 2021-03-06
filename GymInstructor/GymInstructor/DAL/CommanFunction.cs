using BOL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class CommanFunction
    {
        public static bool SendEmail(MailMessage mailMessage)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailPort"]);
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SenderEmail"], ConfigurationManager.AppSettings["SenderPassword"]);
                mailMessage.Sender = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"], ConfigurationManager.AppSettings["AppName"]);
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["SenderEmail"], ConfigurationManager.AppSettings["AppName"]);
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                mailMessage.IsBodyHtml = true;
                smtp.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message.ToString());
                return false;
            }
        }
        public byte[] CNUserImage { get; set; }
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj !=null)
            {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, obj);
                CNUserImage= ms.ToArray();
            }
            else
            {
                CNUserImage = null;
            }
            return CNUserImage;
        }
        public static bool ValidateEmail(string strEmail)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(strEmail);
            return match.Success;
        }

        public static MailMessage AddEmailToAddress(MailMessage objMail, string email)
        {
            try
            {
                string[] StrEmailTo = email.Split(';');

                if (StrEmailTo.Count() > 0)
                {
                    foreach (var item in StrEmailTo)
                    {
                        if (!string.IsNullOrEmpty(item) && !string.IsNullOrWhiteSpace(item) && ValidateEmail(item))
                        {
                            objMail.To.Add(item);
                        }
                    }
                }
                return objMail;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// For Get MEMEBER CODE
        /// </summary>
        /// <returns></returns>
        public static string GetNextEmployeeCode()
        {
            try
            {
                string Code = ConfigurationManager.AppSettings["EmployeeCode"];
                int EmployeeCount = 0;
                List<string> EmpCodeList = new List<string>();
                string NewEmpCode = null;
                using (MVCDemoEntities mVCDemoEntities = new MVCDemoEntities())
                {
                    var resUser = (from objUser in mVCDemoEntities.Users
                                   select objUser).ToList();
                    EmployeeCount = resUser.Count + 1;

                    for (int i = 1; i <= EmployeeCount; i++)
                    {

                        string p1 = Code + i;
                        string i2 = i.ToString();
                        int h = i2.Length;
                        int u = Code.Length - h;
                        string y = p1.Substring(0, u);
                        p1 = y + i;
                        EmpCodeList.Add(p1);
                    }

                    var GetDetail = (from obj in EmpCodeList
                                     orderby obj descending
                                     select obj).FirstOrDefault();
                    if (GetDetail != null)
                    {
                        NewEmpCode = GetDetail;
                    }
                }
                return NewEmpCode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Encrypt Query String Items
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        public static string Encrypt(string inputText)
        {
            string encryptionkey = "SAUW193BX628TD57";
            byte[] keybytes = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] plainText = Encoding.Unicode.GetBytes(inputText);
            PasswordDeriveBytes pwdbytes = new PasswordDeriveBytes(encryptionkey, keybytes);
            using (ICryptoTransform encryptrans = rijndaelCipher.CreateEncryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16)))
            {
                using (MemoryStream mstrm = new MemoryStream())
                {
                    using (CryptoStream cryptstm = new CryptoStream(mstrm, encryptrans, CryptoStreamMode.Write))
                    {
                        cryptstm.Write(plainText, 0, plainText.Length);
                        cryptstm.Close();
                        return Convert.ToBase64String(mstrm.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string encryptText)
        {
            string encryptionkey = "SAUW193BX628TD57";
            byte[] keybytes = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            byte[] encryptedData = Convert.FromBase64String(encryptText.Replace(" ", "+"));
            PasswordDeriveBytes pwdbytes = new PasswordDeriveBytes(encryptionkey, keybytes);
            using (ICryptoTransform decryptrans = rijndaelCipher.CreateDecryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16)))
            {
                using (MemoryStream mstrm = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptstm = new CryptoStream(mstrm, decryptrans, CryptoStreamMode.Read))
                    {
                        byte[] plainText = new byte[encryptedData.Length];
                        int decryptedCount = cryptstm.Read(plainText, 0, plainText.Length);
                        return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                    }
                }
            }
        }

        /// <summary>
        /// Create Random Password
        /// </summary>
        /// <param name="PasswordLength"></param>
        /// <returns></returns>
        public static string CreateRandomPassword(int PasswordLength)
        {
            try
            {
                string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
                Random randNum = new Random();
                char[] chars = new char[PasswordLength];
                int allowedCharCount = _allowedChars.Length;
                for (int i = 0; i < PasswordLength; i++)
                {
                    chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
                }
                return new string(chars);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
