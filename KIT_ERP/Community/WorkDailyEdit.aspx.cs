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

namespace KIT_ERP.Community
{
	/// <summary>
	/// WorkDailyEdit에 대한 요약 설명입니다.
	/// </summary>
	public class WorkDailyEdit : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.HyperLink hlnkNextContent;
		protected System.Web.UI.WebControls.HyperLink hlnkPreContent;
		protected System.Web.UI.WebControls.LinkButton lbkList;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.TextBox txtContents;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btnReply;
		private string seq;
		private static byte [] Files1;        
		private string Nowname = "";
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label lbWriter;
		protected System.Web.UI.WebControls.Label lblRegistrationDate;
		private string Writer = "";
	
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
			MyScriptCode = "if(!Edit_Check()) return false;";

			this.btnEdit.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			btnDelete.Attributes.Add("onclick", "return ContentDelete_Check();");

			if(!IsPostBack)
			{
				if(Request.Params["seq"] != null)
					seq = Request.Params["seq"].ToString();

				if(seq == string.Empty)
				{
					Response.Redirect("WorkDailyList.aspx");
					Response.End();
				}

				string id = Session["ID"].ToString();
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;				
				Cmd.CommandText = "SELECT Name FROM UI_MT WHERE ID = @id";
				Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;				
				Con.Open();
				Nowname = Cmd.ExecuteScalar().ToString();
				Con.Close();
				LoadData();

				CheckReferrer(); 
			}	
			else
			{
				seq = ViewState["seq"].ToString();
			}
		}

		private void CheckReferrer()
		{
			if(Request.UrlReferrer == null)
				Response.Redirect("WorkDailyList.aspx");
			else
			{
				string UrlReferrer = Request.UrlReferrer.ToString();
				string ServerUrl = Request.Url.ToString();
				ServerUrl = ServerUrl.Substring(0, ServerUrl.LastIndexOf("/"));
				if(UrlReferrer.IndexOf(ServerUrl) == -1)
					Response.Redirect("WorkDailyList.aspx");
			}
		} 

		private void LoadData()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;

			Con.Open();
			Cmd.Parameters.Add("@seq", SqlDbType.Int);
			Cmd.Parameters["@seq"].Value = seq;
			
			Cmd.CommandText = "SELECT * FROM T_WorkDailyBoard WHERE seq = @seq";
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);			

			if(reader.Read())
			{
				ViewState["seq"] = reader["seq"].ToString();
				ViewState["thread"] = reader["thread"].ToString();
				ViewState["depth"] = reader["depth"].ToString();

				Writer = reader["writer"].ToString();
				lbWriter.Text = reader["writer"].ToString();
				lblRegistrationDate.Text = DateTime.Parse(reader["transdate"].ToString()).ToShortDateString();
				txtTitle.Text = reader["title"].ToString();
				txtContents.Text = reader["content"].ToString();
				HyperLink1.Text = reader["FileName"].ToString();
				HyperLink1.NavigateUrl = "WorkDailyReportDown.aspx?seq=" + Request.QueryString["seq"] + "&filename=AppendFile";
				
				if(reader["FileSize"].ToString() == "0")
					Label1.Text = "첨부된 파일없음";

			}			

			if(Nowname == Writer)
			{
				txtTitle.ReadOnly = false;
				txtContents.ReadOnly = false;
				btnEdit.Enabled = true;
				btnDelete.Enabled = true;				
			}
			else if(Session["ID"].ToString().ToLower() == "admin")
			{
				btnDelete.Enabled = true;				
			}
			reader.Close();			
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
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			int FileSize = (int)File1.PostedFile.InputStream.Length;
			string ContentType = File1.PostedFile.ContentType;
			string FileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);

			Files1 = new byte[FileSize];
			File1.PostedFile.InputStream.Read(Files1, 0, FileSize);
			
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand(ConnectStr);
			Con.Open();
			SqlTransaction trans = Con.BeginTransaction();
			Cmd.Connection = Con;
			Cmd.Transaction = trans;

			try
			{
				if(File1.Value.Trim() == "")
					Cmd.CommandText = "UPDATE T_WorkDailyBoard SET Title=@Title, content=@content, transdate=@transdate WHERE seq=@seq ";
				else
				{
					if(FileSize == 0)
						throw new Exception("파일크기가 0보다 커야 합니다!");
					Cmd.CommandText = "UPDATE T_WorkDailyBoard SET Title=@Title, content=@content, AppendFile=@AppendFile, FileName=@FileName, FileSize=@FileSize, FileType=@FileType, transdate=@transdate WHERE seq=@seq";
				}
				Cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = txtTitle.Text;
				Cmd.Parameters.Add("@content", SqlDbType.VarChar).Value = txtContents.Text;
				Cmd.Parameters.Add("@AppendFile", SqlDbType.Image).Value = Files1;
				Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName.ToString();
				Cmd.Parameters.Add("@FileSize", SqlDbType.Int).Value = FileSize;
				Cmd.Parameters.Add("@FileType", SqlDbType.VarChar).Value = ContentType;
				Cmd.Parameters.Add("@seq", SqlDbType.Int).Value = seq;
				Cmd.Parameters.Add("@transdate", SqlDbType.SmallDateTime).Value = System.DateTime.Now.ToString();
				Cmd.ExecuteNonQuery();
				
				Cmd.Parameters.Clear();

				trans.Commit();
			}
			catch(Exception ex)
			{
				Response.Write(ex.Message);
				trans.Rollback();
			}
			finally
			{				
				Response.Redirect("WorkDailyList.aspx");
				Con.Close();
			}
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("WorkDailyList.aspx");
		}
	}
}
