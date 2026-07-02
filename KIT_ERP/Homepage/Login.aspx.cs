using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace KIT_ERP.Homepage
{
	/// <summary>
	/// Login에 대한 요약 설명입니다.
	/// </summary>
	public class Login : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				if(Session["ID"] == null || Session["ID"].ToString() == "")
				{
				
				}
				else
				{
					Response.Redirect("Login_OK.aspx");
				}
				
				Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'Contents.aspx';</script>");
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
			this.ImageButton1.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		private bool Authenticate(string id, string pwd)
		{
			bool bExist = false;

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "SELECT UserInfoIndex FROM UI_MT WHERE id = @ID AND Password = @pwd and RecodingState = 1";

			Cmd.Parameters.Add("@ID", SqlDbType.VarChar);
			Cmd.Parameters.Add("@pwd", SqlDbType.VarChar);
			Cmd.Parameters["@ID"].Value = id;
			Cmd.Parameters["@pwd"].Value = pwd;
			try
			{
				Con.Open();
				if(Cmd.ExecuteScalar() != null)
				{
					bExist = true;
					Session["ID"] = id.ToLower();
				}
				else
				{
					Label1.Text = "비밀번호가 일치하지 않습니다!";	
				}
			}
			catch(Exception ex)
			{
				Label1.Text = ex.Message;
			}
			finally
			{
				Con.Close();				
					if(Session["ID"] != null)
									
					Response.Redirect("Login_OK.aspx");				
			}

			return bExist;
		}

		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string id = TextBox1.Text;
			string pwd = TextBox2.Text;

			if(Authenticate(id, pwd))
			{				
				FormsAuthentication.SetAuthCookie(id, false);
				string url = FormsAuthentication.GetRedirectUrl(id, false);
				Response.Redirect(url);				
			}				
		}
	}
}
