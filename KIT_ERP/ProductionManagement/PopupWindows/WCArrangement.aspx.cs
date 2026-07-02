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

using System.Configuration;
using System.Data.SqlClient;

namespace KIT_ERP.ProductionManagement.PopupWindows
{
	/// <summary>
	/// WCArrangement에 대한 요약 설명입니다.
	/// </summary>
	public class WCArrangement : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButton RadioButton1;
		protected System.Web.UI.WebControls.RadioButton RadioButton2;
		protected System.Web.UI.WebControls.RadioButton Radiobutton3;
		protected System.Web.UI.WebControls.RadioButton Radiobutton4;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();
				string str = "Select * From SCS_T";
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet ds = new DataSet();
				da.Fill(ds);
				if(ds.Tables[0].Rows[0]["DailyWorkProgress"].ToString() == "True")
					RadioButton1.Checked = true;
				else 
					RadioButton2.Checked = true;
				if(ds.Tables[0].Rows[0]["ContinuityWorkProgress"].ToString() == "True")
					Radiobutton3.Checked = true;
				else 
					Radiobutton4.Checked = true;


			}

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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();

				string str = "Update SCS_T Set DailyWorkProgress = @Daily,ContinuityWorkProgress = @Continue ";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Parameters.Add("@Daily",SqlDbType.Bit).Value = RadioButton1.Checked;
				comm.Parameters.Add("@Continue",SqlDbType.Bit).Value = Radiobutton3.Checked;
				comm.ExecuteNonQuery();

				RegisterStartupScript("","<script>alert('수정하였습니다.');self.close();</script>");
//				Response.Write("<script language='JavaScript'>");
//				Response.Write("alert('수정하였습니다.');");
//				Response.Write("self.close();");
//				Response.Write("</script>");
//				Response.End();
			}
			catch(Exception ee)
			{
				Response.Write("<script language='JavaScript'>");
				Response.Write("alert('"+ee.Message+"');");
				Response.Write("</script>");
			}
		}
	}
}
