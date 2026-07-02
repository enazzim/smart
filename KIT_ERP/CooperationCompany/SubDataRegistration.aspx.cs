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

namespace KIT_ERP.CooperationCompany
{
	/// <summary>
	/// SubDataRegistration에 대한 요약 설명입니다.
	/// </summary>
	public class SubDataRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox RegistrationPerson;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox Title;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox Content;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Button btnApply;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;

		private static byte [] Files1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 유효성 검사후 스크립트 실행...
			string orginValidateCode;
			orginValidateCode = "if(typeof(Page_ClientValidate) == 'function')";
			orginValidateCode += "if(!Page_ClientValidate()) return false;";

			string MyScriptCode;
			MyScriptCode = "if(!Registration_Check()) return false;";

			this.btnApply.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			btnCancel.Attributes.Add("onclick", "return Cancel_Check();");

			if(!IsPostBack)
			{
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Cmd.CommandText = "SELECT Name FROM UI_MT WHERE Id = 'admin'";
				Con.Open();
				string name = Cmd.ExecuteScalar().ToString();
				RegistrationPerson.Text = name.ToString();
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

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			//Response.Redirect("GeneralData.aspx");
			Response.Redirect("SubData.aspx");
		}

		private void btnApply_Click(object sender, System.EventArgs e)
		{
			int FileSize = (int)File1.PostedFile.InputStream.Length;
			string ContentType = File1.PostedFile.ContentType;
			string FileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);

			Files1 = new byte[FileSize];
			File1.PostedFile.InputStream.Read(Files1, 0, FileSize);

			
			
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "INSERT INTO SD_T(Title, Contents, AppendFile, FileName, FileSize, FileType, RegistrationPerson) VALUES(@Title, @Contents, @AppendFile, @FileName, @FileSize, @FileType, @RegistrationPerson)";

			Cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = Title.Text;
			Cmd.Parameters.Add("@Contents", SqlDbType.VarChar).Value = Content.Text;
			Cmd.Parameters.Add("@AppendFile", SqlDbType.Image).Value = Files1;
			Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName.ToString();
			Cmd.Parameters.Add("@FileSize", SqlDbType.Int).Value = FileSize;
			Cmd.Parameters.Add("@FileType", SqlDbType.VarChar).Value = ContentType.ToString();
			Cmd.Parameters.Add("@RegistrationPerson", SqlDbType.VarChar).Value = RegistrationPerson.Text;
			
			Con.Open();
			Cmd.ExecuteNonQuery();
			Con.Close();

			Response.Cookies["SD_T"]["PageNumber"] = "0";
			Response.Redirect("SubData.aspx");
		}
	}
}
