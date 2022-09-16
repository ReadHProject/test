using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinalTestMVC
{
    public class DBConnectionClass
    {
        public SqlConnection DBConnect()
        {
            SqlConnection scon = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            scon.Open();
            return scon;
        }
        
        public SqlDataReader ExecuteReaderSP(string query,SqlParameter[] param)
        {
            var con = DBConnect();
            SqlCommand scom = new SqlCommand(query, con);
            scom.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < param.Length; i++)
            {
                if (param[i] != null)
                {
                    scom.Parameters.AddWithValue(param[i].ParameterName, param[i].Value);
                }
                
            }
            SqlDataReader sdr = scom.ExecuteReader();
            return sdr;
        }

        public int ExecuteNonQuerySP(string query, SqlParameter[] param)
        {
            var con = DBConnect();
            SqlCommand scom = new SqlCommand(query, con);
            scom.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < param.Length; i++)
            {
                if (param[i] != null)
                {
                    scom.Parameters.AddWithValue(param[i].ParameterName, param[i].Value);
                }
            }
            int val = scom.ExecuteNonQuery();
            return val;
        }

        public DataSet ExecuteDataSetSP(string query, SqlParameter[] param)
        {
            var con = DBConnect();
            SqlCommand scom = new SqlCommand(query, con);
            scom.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < param.Length; i++)
            {
                if (param[i] != null)
                {
                    scom.Parameters.AddWithValue(param[i].ParameterName, param[i].Value);
                }
            }
            SqlDataAdapter sda = new SqlDataAdapter(scom);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
    }
}