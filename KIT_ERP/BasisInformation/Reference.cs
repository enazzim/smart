using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// Reference에 대한 요약 설명입니다.
	/// </summary>
	public class Reference
	{
		private string m_Table;		//넘어온 테이블 담는 변수
		private int m_Index;		//레코드 인덱스값 저장


		public Reference(string m_RequireTable, int m_RequireIndex)
		{
			m_Table = m_RequireTable.ToString();
			m_Index = m_RequireIndex;
		}

		public DataSet reference()
		{				
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "SELECT * From Relation WHERE (StandardinfoTableID = @table) AND (SITIndex = @index)";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@table", SqlDbType.VarChar).Value = m_Table.ToString();
			comm.Parameters.Add("@index", SqlDbType.VarChar).Value = m_Index;
			
			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(ds);

			return ds;

		}


	}
}
