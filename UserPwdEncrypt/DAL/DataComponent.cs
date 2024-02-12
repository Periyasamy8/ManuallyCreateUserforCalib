using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using UserPwdEncrypt.JsonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace UserPwdEncrypt.DAL
{
    public class DataComponent
    {

        public DataComponent()
        {
            var configuration = GetConfiguration();
            con = new SqlConnection(configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value);
        }
        public IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }



        public static SqlConnection con1 = new SqlConnection();
        public static SqlConnection con = new SqlConnection();
        //public static SqlConnection con = new SqlConnection(ConnectionString.CName);

        public void conn()
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
            }
            catch (Exception)
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                    con.Open();
                }
            }
        }
        public static bool ExecuteCmd(SqlCommand sqlCmd)
        {
            bool _wasSuccessful = false;
            LogFile Log = new LogFile();
            try
            {

                SqlConnection sqlConn = con;
                sqlCmd.Connection = sqlConn;
                try
                {
                    sqlConn.Open();
                    sqlCmd.ExecuteNonQuery();
                    _wasSuccessful = true;
                }
                catch (SqlException ex)
                {
                    Log.writelog(ex.Message);

                }
                finally
                {
                    sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                Log.writelog(ex.Message);
            }
            return _wasSuccessful;
        }

        public static DataSet SelectCmd(SqlCommand sqlCmd)
        {
            DataSet dsResults = new DataSet();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            LogFile Log = new LogFile();
            try
            {
                SqlConnection sqlConn = con;
                sqlCmd.Connection = sqlConn;
                try
                {
                    sqlCmd.CommandTimeout = 2000;
                    sqlAdapter.SelectCommand = sqlCmd;
                    sqlAdapter.Fill(dsResults);
                }
                catch (SqlException ex)
                {
                    Log.writelog(ex.Message);

                }

                finally
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlCmd.Dispose();
                }

            }
            catch (Exception ex)
            {
                Log.writelog(ex.Message);
            }
            return dsResults;
        }
    }

    public class LogFile
    {
        public void writelog(string message)
        {
            var path = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["Path"];
            string sTemp = path + "_" + DateTime.Now.ToString("dd_MM") + ".txt";
            using (FileStream Fs = new FileStream(sTemp, FileMode.OpenOrCreate | FileMode.Append))
            {
                StreamWriter st = new StreamWriter(Fs);
                string dttemp = DateTime.Now.ToString("[dd:MM:yyyy] [HH:mm:ss:ffff]");
                st.WriteLine(dttemp + "\t" + message);
                st.Close();
            }    
        }
    }

}
