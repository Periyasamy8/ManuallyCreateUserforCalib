using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPwdEncrypt.DAL;
using UserPwdEncrypt.JsonClass;

namespace UserPwdEncrypt.BAL
{
    public class PasswordGenerateBAL
    {
        
        #region

        public void UserDetails()
        {
            LogFile log = new LogFile();
            try
            {
                DataSet dsResult = new DataSet();
                PasswordGenerateDAL _obj = new PasswordGenerateDAL();
                dsResult = _obj.GetUserDetails();

                StringBuilder sb = new StringBuilder();
                DEncrypt Encrypt = new DEncrypt();
                string pass = "Password@123";
                if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    sb.Append("<rows>");

                    for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                    {
                        sb.Append("<row>");
                        sb.Append("<Email>" + Convert.ToString(dsResult.Tables[0].Rows[i]["Email"]) + "</Email>");
                        sb.Append("<Password>" + Encrypt.Encrypt(pass, Convert.ToString(dsResult.Tables[0].Rows[i]["Email"])) + "</Password>");
                        sb.Append("</row>");
                       
                    }
                    
                }
              
                sb.Append("</rows>");
                PasswordGenerateDAL _object = new PasswordGenerateDAL();
             _object.StorePassword(sb.ToString(), 0); //Mode - 1 for fetching data and 0 for updating 
            }
            catch (Exception ex)
            {
                log.writelog(ex.Message);
            }
        }


        #endregion
    }
}
