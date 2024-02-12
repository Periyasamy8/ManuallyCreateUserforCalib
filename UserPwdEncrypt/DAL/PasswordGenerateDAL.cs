using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserPwdEncrypt.DAL
{
    public class PasswordGenerateDAL: DataComponent
    {
        public DataSet GetUserDetails()
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("Asp_GetUsersEmailforCal", con);
            cmd.CommandType = CommandType.StoredProcedure;
            return SelectCmd(cmd);
        }

        public void StorePassword(string Encryptpwd, int mode)
        {
            SqlCommand sqlCmd = new SqlCommand("Asp_UpdateMultipleuser", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Encryptpwd", Encryptpwd);
            sqlCmd.Parameters.AddWithValue("@Mode", mode);
            bool beResult = ExecuteCmd(sqlCmd);
        }
    }
}
