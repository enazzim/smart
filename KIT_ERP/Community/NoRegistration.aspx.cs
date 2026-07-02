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

namespace KIT_ERP.Community
{
	/// <summary>
	/// NoRegistration에 대한 요약 설명입니다.
	/// </summary>
	public class NoRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.TextBox Title;
		protected System.Web.UI.WebControls.TextBox Content;
		protected System.Web.UI.WebControls.Button btnApply;
		protected System.Web.UI.WebControls.TextBox RegistrationPerson;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
			
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Homepage/default.htm';</script>");
			}
			// 유효성 검사후 스크립트 실행...
			string orginValidateCode;
			string MyScriptCode;
			orginValidateCode = "if(typeof(Page_ClientValidate) == 'function')";
			orginValidateCode += "if(!Page_ClientValidate()) return false;";			
			MyScriptCode = "if(!Registration_Check()) return false;";

			this.btnApply.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			btnCancel.Attributes.Add("onclick", "return Cancel_Check();");
			
			// 현재 로그인된 사용자 이름을 작성자란에 자동기입...
			if(!IsPostBack)
			{
				string id = Session["ID"].ToString();
				string ConnecStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnecStr);
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Cmd.CommandText = "SELECT Name FROM UI_MT WHERE ID = @id";
				Cmd.Parameters.Add("@id", SqlDbType.VarChar, 10);
				Cmd.Parameters["@id"].Value = id;
				Con.Open();
				RegistrationPerson.Text = Cmd.ExecuteScalar().ToString();
				Con.Close();
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
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// 취소버튼...
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Notice.aspx");
		}


		// 등록버튼...
		private void btnApply_Click(object sender, System.EventArgs e)
		{

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "INSERT INTO CN_T(Title, Contents, RegistrationPerson) VALUES(@Title, @Contents, @RegistrationPerson)";

			Cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = Title.Text;
			Cmd.Parameters.Add("@Contents", SqlDbType.VarChar).Value = Content.Text;
			Cmd.Parameters.Add("@RegistrationPerson", SqlDbType.VarChar).Value = RegistrationPerson.Text;			
			

			Con.Open();
			Cmd.ExecuteNonQuery();
			Con.Close();

			// 새글작성 등록시 첫목록페이지로 가기위해 Cookie PageNumber를 "0"으로...
			Response.Cookies["CN_T"]["PageNumber"] = "0";
			Response.Redirect("Notice.aspx");
		}		
	}
}