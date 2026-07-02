using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// Update에 대한 요약 설명입니다.
	/// </summary>
	public class Update
	{
		private string UserName;
		private string UserID;

		public Update()
		{
			//
			// TODO: 여기에 생성자 논리를 추가합니다.
			//
		}

		
		
		public Update(string user)
		{
			UserID = user;

			// 사용자 이름을 입력
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = UserID.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				UserName = dr["Name"].ToString();
			}
			conn.Close();


		}
		
		/// <summary>
		/// 클레임원인
		/// </summary>
		/// <returns></returns>
		public DataSet ClaimCause()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='1010'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("Cause");
			da.Fill(ds.Tables["Cause"]);
			conn.Close();
			
			return ds;
		}

		/// <summary>
		/// 클레임현상
		/// </summary>
		/// <returns></returns>
		public DataSet ClaimStatus()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='1000'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("Status");
			da.Fill(ds.Tables["Status"]);
			conn.Close();
			
			return ds;
		}

		public DataSet dsBank()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='0920'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("bank");
			da.Fill(ds.Tables["bank"]);
			DataRow row = table.NewRow();
			row["SmallClassificationCode"]="";
			row["SmallClassificationName"]="-선 택-";
			table.Rows.InsertAt(row,0);
			
			conn.Close();
			
			return ds;
		}


		public DataSet dsDecision()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "select * from PUC_MT where SmallClassificationName != '' and Recodingstate = 1 and LargeClassificationCode = '0910'";
			SqlCommand comm = new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("Decision");
			da.Fill(ds.Tables["Decision"]);
			DataRow row = table.NewRow();
			row["SmallClassificationCode"]="";
			row["SmallClassificationName"]="-선 택-";
			table.Rows.InsertAt(row,0);
			conn.Close();

			return ds;
		}
	}
}
