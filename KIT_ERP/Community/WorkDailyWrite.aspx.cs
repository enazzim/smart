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

namespace KIT_ERP.Community
{
	/// <summary>
	/// WorkDailyWrite에 대한 요약 설명입니다.
	/// </summary>
	public class WorkDailyWrite : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnApply;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.TextBox Content;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.TextBox Title;
		protected System.Web.UI.WebControls.TextBox RegistrationPerson;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		private static byte [] Files1;

		private bool ReplyFlag = false;
		private string thread = string.Empty;
		private string depth = string.Empty;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string orginValidateCode;
			orginValidateCode = "if(typeof(Page_ClientValidate) == 'function')";
			orginValidateCode += "if(!Page_ClientValidate()) return false;";

			string MyScriptCode;
			MyScriptCode = "if(!Registration_Check()) return false;";

			this.btnApply.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			btnCancel.Attributes.Add("onclick", "return Cancel_Check();");

			if (Request.Params["thread"] != null)
				thread = Request.Params["thread"].ToString();
			if (Request.Params["depth"] != null)
				depth = Request.Params["depth"].ToString();

			if(thread != string.Empty && depth != string.Empty)
			{
				ReplyFlag = true;
			}


			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				if(Session["ID"] != null)
				{
					WriteName(Session["ID"].ToString());
				}
			}			
		}

		private void WriteName(string id)
		{
			string ConnecStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnecStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "SELECT Name FROM UI_MT WHERE ID = @id and RecodingState = 1";
			Cmd.Parameters.Add("@id", id);
			Con.Open();
			RegistrationPerson.Text = Cmd.ExecuteScalar().ToString();
			RegistrationPerson.ReadOnly = true;
			Con.Close();
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

		private void btnApply_Click(object sender, System.EventArgs e)
		{
			bool truefalse = false;
			int FileSize = (int)File1.PostedFile.InputStream.Length;
			string ContentType = File1.PostedFile.ContentType;
			string FileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);

			Files1 = new byte[FileSize];
			File1.PostedFile.InputStream.Read(Files1, 0, FileSize);

			

			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);


			string sp;
			if(ReplyFlag)
				sp = "UP_INSERT_REPLY";
			else
				sp = "UP_INSERT_BOARDNEW";

			SqlCommand Cmd = new SqlCommand(sp, Con);
			Cmd.CommandType = CommandType.StoredProcedure;

			if(ReplyFlag)
			{
				int thread = int.Parse(this.thread);
				int depth = int.Parse(this.depth);
				int prevThread = (thread-1)/1000 * 1000;

				Cmd.Parameters.Add("@ParentThread", thread);
				Cmd.Parameters.Add("@PrevThread", prevThread);
				Cmd.Parameters.Add("@depth", depth + 1);
			}

			Cmd.Parameters.Add("@writer", SqlDbType.VarChar).Value = RegistrationPerson.Text;
			Cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = Title.Text;			
			Cmd.Parameters.Add("@AppendFile", SqlDbType.Image).Value = Files1;
			Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName.ToString();
			Cmd.Parameters.Add("@FileSize", SqlDbType.Int).Value = FileSize;
			Cmd.Parameters.Add("@FileType", SqlDbType.VarChar).Value = ContentType.ToString();
			Cmd.Parameters.Add("@content", SqlDbType.VarChar).Value = Content.Text;
			Cmd.Parameters.Add("@Division","업무일지");
Con.Open();
			try
			{
				Cmd.ExecuteNonQuery();
				truefalse = true;
			}
			catch(Exception ee)
			{
				Response.Write("<script>alert('"+ee.Message+"');</script>");
			}
			finally
			{
				Con.Close();

				if(truefalse)
				{
					string script = "<script>alert('저장되었습니다');</script>";
					Page.RegisterClientScriptBlock("done", script);
					Response.Redirect("WorkDailyList.aspx");
				}

			}
			
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("WorkDailyList.aspx");
		}
	}
}
