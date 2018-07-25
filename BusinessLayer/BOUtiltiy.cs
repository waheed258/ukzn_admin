using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using DataManager;
using System.Globalization;
using System.Xml.Serialization;
using System.Net.Mail;


namespace BusinessManager
{
    public class BOUtiltiyUKZN
    {
        public static string[] formats = { "MM/dd/yyyy", "M/d/yyyy", "dd/MM/yyyy", "yyyy-MM-dd'T'HH:mm:ss'Z'", "dd-MM-yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "yyyy/MM/d", "yyyy/M/d", "yyyy/M/dd", "M/dd/yyyy", "d/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "MMMM dd, yyyy" };
        private DOUtility _objDOUtility = new DOUtility();
      

        public string Encrypt(string toEncrypt)
        {
            //byte[] keyArray;
            //byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            ////System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //// Get the key from config file
            //string key = "dinoosys@!@#";
            ////System.Windows.Forms.MessageBox.Show(key);
            //if (useHashing)
            //{
            //    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //    hashmd5.Clear();
            //}
            //else
            //    keyArray = UTF8Encoding.UTF8.GetBytes(key);

            //TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //tdes.Key = keyArray;
            //tdes.Mode = CipherMode.ECB;
            //tdes.Padding = PaddingMode.PKCS7;

            //ICryptoTransform cTransform = tdes.CreateEncryptor();
            //byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            //tdes.Clear();
            //return Convert.ToBase64String(resultArray, 0, resultArray.Length);

            string ResultString = string.Empty;
            try
            {
                byte[] encData_byte = new byte[toEncrypt.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(toEncrypt);
                ResultString = Convert.ToBase64String(encData_byte);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to encrypt.");
            }
            return ResultString;
        }
        public string Decrypt(string cipherString)
        {
            //byte[] keyArray;
            //byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            ////System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            ////Get your key from config file to open the lock!
            //string key = "dinoosys@!@#";

            //if (useHashing)
            //{
            //    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            //    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //    hashmd5.Clear();
            //}
            //else
            //    keyArray = UTF8Encoding.UTF8.GetBytes(key);

            //TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //tdes.Key = keyArray;
            //tdes.Mode = CipherMode.ECB;
            //tdes.Padding = PaddingMode.PKCS7;

            //ICryptoTransform cTransform = tdes.CreateDecryptor();
            //byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            //tdes.Clear();
            //return UTF8Encoding.UTF8.GetString(resultArray);
            string ResultString = string.Empty;
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(cipherString);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                ResultString = new String(decoded_char);
            }
            catch (Exception ex)
            {
                throw new Exception("invalid result.");
            }
            return ResultString;
        }
      
        public string ShowMessage(string sOperation, string TitleMessage, string sMessage)
        {
            StringBuilder sbMessage = new StringBuilder();
            // operations class 1.success,2.danger,3.info4.warning
            sbMessage.Append(" <div class='alert alert-" + sOperation + " fade in'>");
            sbMessage.Append("<a href='#' class='close' data-dismiss='alert'>&times;</a>   <strong>" + TitleMessage + "!</strong>" + sMessage + "</div>");
            return sbMessage.ToString();

        }
        public string GetSubstringByString(string a, string b, string c)
        {
            if (c.IndexOf(a) > 0 && c.IndexOf(b) > 0)
                return c.Substring((c.IndexOf(a) + a.Length), (c.IndexOf(b) - c.IndexOf(a) - a.Length));
            return c;
        }

        public string ConvertDateFormat(string DateString)
        {
            try
            {
                if (DateString != "")
                {
                    if (IsValidDateTime(DateString))
                    {
                        string[] dateString = DateString.Split(' ');
                        return DateTime.ParseExact(dateString[0], formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.Message + " " + DateString;
            }
        }
        public string ReverseConvertDateFormat(string DateString)
        {
            try
            {
                string ReturnDate = string.Empty;
                if (DateString != "")
                {
                    if (IsValidDateTime(DateString))
                    {
                        string[] dateString = DateString.Split(' ');
                        ReturnDate = DateTime.ParseExact(dateString[0], formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MM-yyyy");

                    }
                }
                return ReturnDate;
            }
            catch (Exception ex)
            {
                return ex.Message + " " + DateString;
            }
        }
        public string ReverseConvertDateFormat(string DateString, string DateFormat)
        {
            try
            {
                string ReturnDate = string.Empty;

                if (DateString != "")
                {
                    if (IsValidDateTime(DateString))
                    {
                        string[] dateString = DateString.Split(' ');

                        ReturnDate = DateTime.ParseExact(dateString[0], formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString(DateFormat);
                    }
                }
                return ReturnDate;
            }
            catch (Exception ex)
            {
                return ex.Message + " " + DateString;
            }
        }


        public bool SendEmail(string SmtpHost, int SmtpPort, string MailFrom, string DisplayNameFrom, string FromPassword, string MailTo, string DisplayNameTo, string MailCc, string DisplayNameCc, string MailBcc, string Subject, string MailText, string Attachment)
        {
            MailMessage myMessage = new MailMessage();
            bool IsSucces = false;
            try
            {
                myMessage.From = new MailAddress(MailFrom, DisplayNameFrom);
                if (MailTo != "")
                    myMessage.To.Add(new MailAddress(MailTo, DisplayNameTo));
                if (MailCc != "")
                {
                    string[] arrayMailCC = MailCc.Split(',');
                    if (arrayMailCC.Length > 0)
                    {
                        foreach (string inMailCC in arrayMailCC)
                        {
                            myMessage.CC.Add(new MailAddress(inMailCC));
                        }

                    }
                    else
                    {
                        myMessage.CC.Add(new MailAddress(MailCc));
                    }

                }

                if (MailBcc != "")
                    myMessage.Bcc.Add(MailBcc);

                myMessage.Subject = Subject;
                myMessage.IsBodyHtml = true;
                myMessage.Body = MailText;
                if (Attachment != "")
                {
                    Attachment a = new Attachment(Attachment);
                    myMessage.Attachments.Add(a);
                }
                SmtpClient mySmtpClient = new SmtpClient(SmtpHost, SmtpPort);
                mySmtpClient.Credentials = new System.Net.NetworkCredential(MailFrom, FromPassword);
                mySmtpClient.EnableSsl = true;
                mySmtpClient.Send(myMessage);
                IsSucces = true;

            }
            catch (Exception ex)
            {
                IsSucces = false;
            }
            finally
            {

                myMessage.Dispose();
            }
            return IsSucces;
        }

        public string ReverseConvertDateFormatByDate(string DateString, string DateFormat)
        {
            try
            {
                string ReturnDate = string.Empty;

                if (DateString != "")
                {
                    if (IsValidDateTime(DateString))
                    {
                        string[] dateString = DateString.Split(' ');
                        string[] DateFomat = dateString[0].Split('-');
                        ReturnDate = DateFomat[2] + "-" + DateFomat[1] + "-" + DateFomat[0];
                        //ReturnDate = DateTime.ParseExact(dateString[0], formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString(DateFormat);
                    }
                }
                return ReturnDate;
            }
            catch (Exception ex)
            {
                return ex.Message + " " + DateString;
            }
        }
        public static bool IsValidDateTime(string dateTime)
        {
            string[] dateString = dateTime.Split(' ');


            DateTime parsedDateTime;
            return DateTime.TryParseExact(dateString[0], formats, CultureInfo.InvariantCulture,
                                           DateTimeStyles.None, out parsedDateTime);
        }
        public string CurrencyId()
        {
            return "15";
        }
        public string Currencycode()
        {
            return "R";
        }
 
        public string LogoUrl(string Logo)
        {
            return "http://salesdemo.dinoosystech.com/pdfimages/" + Logo;
        }
        public string LogoUrl()
        {
            return "http://salesdemo.dinoosystech.com/";
        }

        #region DataBaseRelatedMethods
        public DataSet GetMenus(string RoleId, int CompanyId)
        {
            return _objDOUtility.GetMenus(RoleId, CompanyId);
        }
        #endregion DataBaseRelatedMethods

    }




}
