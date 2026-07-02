using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// IDCheck에 대한 요약 설명입니다.
	/// </summary>
	public class IDCheck : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Request.QueryString["param"]==null)
				return;
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.CommandText = "Select count(ID) From UI_MT where RecodingState = 1 and [ID] = @id";
			comm.Parameters.Add("@id",Request.QueryString["param"]);
			con.Open();
			if(int.Parse(comm.ExecuteScalar().ToString()) == 0)
				if(Request.QueryString["param"].Trim() =="")
					Response.Write("null");
				else
					Response.Write("true");
			else
				Response.Write("false");//HOONS는 사용하고 있는 아이디로 가정한다.
			con.Close();
			Response.Flush();

			Response.End();
		}

		#region Web Form 디자이너에서 생성한 코드
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 이 호출은 ASP.NET Web Form 디자이너에 필요합니다.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
