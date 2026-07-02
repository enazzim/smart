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


namespace KIT_ERP.Homepage
{
	/// <summary>
	/// Contents에 대한 요약 설명입니다.
	/// </summary>
	public class Contents : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Image Image1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				//string id = Session["ID"].ToString();
//				Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'Contents.aspx';</script>");

//				string ConnecStr = ConfigurationSettings.AppSettings["DSN"].ToString();
//				SqlConnection Con = new SqlConnection(ConnecStr);
//				SqlCommand Cmd = new SqlCommand();
//				Cmd.Connection = Con;
//				Cmd.CommandText = "SELECT Name FROM UI_MT WHERE ID = @id";
//				Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
//				Con.Open();
//				Session["UserName"] = Cmd.ExecuteScalar().ToString();
//				Label1.Text = Session["UserName"].ToString();	
//				Con.Close();
				
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ImageButton7_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["ID"] = null;
			Session.Abandon();
			Response.Redirect("Login.aspx");
		}
	}
}
