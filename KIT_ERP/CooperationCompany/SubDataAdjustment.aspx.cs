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
	/// SubDataAdjustment에 대한 요약 설명입니다.
	/// </summary>
	public class SubDataAdjustment : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblRegistrationPerson;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblRegistrationDate;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label lblHits;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtContents;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.LinkButton lbkList;
		protected System.Web.UI.WebControls.HyperLink hlnkPreContent;
		protected System.Web.UI.WebControls.HyperLink hlnkNextContent;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.Button btnDelete;		

		private string SubDataIndex = "";		
		private string updateHits = "0";		
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
			MyScriptCode = "if(!Edit_Check()) return false;";

			this.btnEdit.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			btnDelete.Attributes.Add("onclick", "return ContentDelete_Check();");

			
			SubDataIndex = Request.QueryString["SubDataIndex"];
			
			if(SubDataIndex == null)
				Response.Redirect("SubData.aspx");

			if(Request.Cookies["SD_T"] != null)
				updateHits = Request.Cookies["SD_T"]["UpdateHits"];
            
			if(Session["ID"].ToString() != "admin")
				File1.Visible = false;

			if(!IsPostBack)
			{
				LoadData();				
			}			
		}


		
		// 해당글 내용보기...
		private void LoadData()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;

			Con.Open();
			Cmd.Parameters.Add("@SubDataIndex", SqlDbType.Int).Value = SubDataIndex;		

			if(updateHits == "0")
			{
				Cmd.CommandText = "UPDATE SD_T SET Hits = Hits + 1 WHERE SubDataIndex = @SubDataIndex";				
				Cmd.ExecuteNonQuery();
				Response.Cookies["SD_T"]["UpdateHits"] = "1";
			}

			Cmd.CommandText = "SELECT * FROM SD_T WHERE SubDataIndex = @SubDataIndex";
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

			if(reader.Read())
			{
				lblRegistrationPerson.Text = reader["RegistrationPerson"].ToString();
				lblRegistrationDate.Text = reader["RegistrationDate"].ToString();
				lblHits.Text = reader["Hits"].ToString();
				txtTitle.Text = reader["Title"].ToString();
				txtContents.Text = reader["Contents"].ToString();
				HyperLink1.Text = reader["FileName"].ToString();
				HyperLink1.NavigateUrl = "SubDataDown.aspx?SubDataIndex=" + Request.QueryString["SubDataIndex"] + "&filename=AppendFile";
				
				if(reader["FileSize"].ToString() != "0")
					Label7.Text = "(" + reader["FileSize"].ToString() + ")";
				else
					Label7.Text = "첨부된 파일없음";

			}
			reader.Close();			
			Con.Close();

			// 현재 로그인된 사용자가 admin일 경우에만 텍스트박스, 수정(삭제)버튼 활성화...
			if(Session["ID"].ToString() == "admin")
			{				
				txtTitle.ReadOnly = false;
				txtContents.ReadOnly = false;
				btnEdit.Enabled = true;
				btnDelete.Enabled = true;			
				
				File1.Visible = true;			
			}

			// 이전글, 다음글 보기...
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
						
			int Pre = 0, Next = 0;
			SqlCommand comm = new SqlCommand("SELECT TOP 1 SubDataIndex FROM SD_T WHERE SubDataIndex < @SubDataIndex ORDER BY SubDataIndex DESC", conn);
			comm.Parameters.Add("@SubDataIndex", SqlDbType.Int).Value = Request.QueryString["SubDataIndex"];
			if(comm.ExecuteScalar() == null)
			{
				hlnkNextContent.Visible = false;
			}
			else
			{
				Next = int.Parse(comm.ExecuteScalar().ToString());
			}

			comm = new SqlCommand("SELECT TOP 1 SubDataIndex FROM SD_T WHERE SubDataIndex > @SubDataIndex ORDER BY SubDataIndex ASC", conn);
			comm.Parameters.Add("@SubDataIndex", SqlDbType.Int).Value = Request.QueryString["SubDataIndex"];
			if(comm.ExecuteScalar() == null)
			{
				hlnkPreContent.Visible = false;
			}
			else
			{
				Pre = int.Parse(comm.ExecuteScalar().ToString()); 
			}
			
			hlnkPreContent.NavigateUrl = "SubDataAdjustment.aspx?SubDataIndex=" + Pre.ToString();
			hlnkNextContent.NavigateUrl = "SubDataAdjustment.aspx?SubDataIndex=" + Next.ToString();
						
			conn.Close();
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
			this.lbkList.Click += new System.EventHandler(this.lbkList_Click);
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
				Cmd.CommandText = "UPDATE SD_T SET Title=@Title, Contents=@Contents, AppendFile=@AppendFile, FileName=@FileName, FileSize=@FileSize, FileType=@FileType, RegistrationDate=@RegistrationDate WHERE SubDataIndex=@SubDataIndex";

				Cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = txtTitle.Text;
				Cmd.Parameters.Add("@Contents", SqlDbType.VarChar).Value = txtContents.Text;
				Cmd.Parameters.Add("@AppendFile", SqlDbType.Image).Value = Files1;
				Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName.ToString();
				Cmd.Parameters.Add("@FileSize", SqlDbType.Int).Value = FileSize;
				Cmd.Parameters.Add("@FileType", SqlDbType.VarChar).Value = ContentType;
				Cmd.Parameters.Add("@SubDataIndex", SqlDbType.Int).Value = SubDataIndex;
				Cmd.Parameters.Add("@RegistrationDate", SqlDbType.SmallDateTime).Value = System.DateTime.Now.ToString();
				
				Cmd.ExecuteNonQuery();
				trans.Commit();
			}
			catch(Exception ex)
			{
				Response.Write(ex.Message);
				trans.Rollback();
			}
			finally
			{				
				Response.Redirect("SubData.aspx");
				Con.Close();
			}
		}

		private void lbkList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("SubData.aspx");
		}

		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlTransaction trans = Con.BeginTransaction();
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.Transaction = trans;			
			
			try
			{
				Cmd.CommandText = "DELETE FROM SD_T WHERE SubDataIndex = @SubDataIndex";							
				Cmd.Parameters.Add("@SubDataIndex", SqlDbType.Int);
				Cmd.Parameters["@SubDataIndex"].Value = SubDataIndex;
				Cmd.ExecuteNonQuery();				
				trans.Commit();
			}
			catch(Exception ex)
			{
				Response.Write(ex.Message);
				trans.Rollback();
			}
			finally
			{
				Con.Close();
				Response.Redirect("SubData.aspx");
			}
		}
	}
}
