using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DraShopDAO
{
    public class DataHelper
    {
        SqlConnection con;
        public DataHelper()
        {
            //con = new SqlConnection("Data Source=DESKTOP-T7M1CD1;Initial Catalog=Learn_MVC;Integrated Security=True");
            string strcon = ConfigurationManager.ConnectionStrings["strconnect"].ConnectionString;
            con = new SqlConnection(strcon);
        }
        public DataHelper(string stcon)
        {
            con = new SqlConnection(stcon);
        }

        public void Open()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        public void Close()
        {
            if (con.State != ConnectionState.Closed)
                con.Close();
        }

        public void ExcuteNonQuery(string stsql)
        {
            Open();
            SqlCommand cm = new SqlCommand(stsql, con);
            cm.ExecuteNonQuery();
            Close();
        }

        public DataTable GetDataTable(string sqlSelect)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlSelect, con);
            da.Fill(dt);
            return dt;
        }

        public SqlDataReader StoreReader(string nameStore, params object[] values)
        {
            Open();
            SqlCommand cm;
            try
            {
                cm = new SqlCommand(nameStore, con);
                cm.CommandType = CommandType.StoredProcedure;
                SqlCommandBuilder.DeriveParameters(cm);
                for(int i = 1; i < cm.Parameters.Count; i++)
                {
                    cm.Parameters[i].Value = values[i - 1];
                }
                SqlDataReader dr = cm.ExecuteReader();
                return dr;
            }
            catch
            {
                return null;
            }
        }
    }
}
