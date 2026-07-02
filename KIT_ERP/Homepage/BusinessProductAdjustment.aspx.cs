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
	/// BusinessProductAdjustment에 대한 요약 설명입니다.
	/// </summary>
	public class BusinessProductAdjustment : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblRegistrationPerson;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label lblRegistrationDate;
		protected System.Web.UI.WebControls.Label lblHits;		
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.HyperLink HyperLink3;
		protected System.Web.UI.WebControls.HyperLink HyperLink4;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;		
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.TextBox Content;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.TextBox Title;		

		private string HBizProductIntroIndex="";
		private string updateHits = "0";
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;		
		private static byte [] Files1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 유효성검사후 스크립트 실행...
			string orginValidateCode;
			orginValidateCode = "if(typeof(Page_ClientValidate) == 'function') ";
			orginValidateCode += "if(!Page_ClientValidate()) return false;";

			string MyScriptCode;
			MyScriptCode = "if(!Edit()) return false;";

			this.Button2.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			Button1.Attributes.Add("onclick", "return Delete()");

			HBizProductIntroIndex = Request.QueryString["HBizProductIntroIndex"];

			if(HBizProductIntroIndex == null)
				Response.Redirect("BusinessProduct.aspx");

			if(Request.Cookies["HBP_T"] != null)
				updateHits = Request.Cookies["HBP_T"]["UpdateHits"];

			
			if(!IsPostBack)
			{
				Label1.Text = "◀ 사업 / 제품개요 ▶";
				
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Con.Open();
				Cmd.CommandText = "SELECT Category FROM CCG_T WHERE Classify = 0 order by Category";
				SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
            
				DropDownList1.DataSource = reader;
				DropDownList1.DataTextField = "Category";
				DropDownList1.DataBind();
				Con.Close();
				LoadData();			
			}			


			if((Session["ID"] != null) && (Session["ID"].ToString() == "admin"))
			{					
				Button1.Enabled = true;
				Button2.Enabled = true;
				Title.ReadOnly = false;
				Content.ReadOnly = false;
				DropDownList1.Enabled = true;
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
			Cmd.Parameters.Add("@HBizProductIntroIndex", SqlDbType.Int);
			Cmd.Parameters["@HBizProductIntroIndex"].Value = HBizProductIntroIndex;

			if(updateHits == "0")
			{
				Cmd.CommandText = "UPDATE HBP_T SET Hits = Hits + 1 WHERE HBizProductIntroIndex = @HBizProductIntroIndex";				
				Cmd.ExecuteNonQuery();
				Response.Cookies["HBP_T"]["UpdateHits"] = "1";
			}

			Cmd.CommandText = "SELECT * FROM HBP_T WHERE HBizProductIntroIndex = @HBizProductIntroIndex";
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

			if(reader.Read())
			{
				DropDownList1.SelectedItem.Text = reader["Category"].ToString();
				lblRegistrationPerson.Text = reader["RegistrationPerson"].ToString();				
				lblRegistrationDate.Text = reader["RegistrationDate"].ToString();
				lblHits.Text = reader["Hits"].ToString();				
				Title.Text = reader["Title"].ToString();
				Content.Text = reader["Contents"].ToString();
				HyperLink1.Text = reader["FileName"].ToString();
				HyperLink1.NavigateUrl = "BpDownload.aspx?HBizProductIntroIndex=" + Request.QueryString["HBizProductIntroIndex"] + "&filename=AppendFile";
				
				if(reader["FileSize"].ToString() != "0")
					Label6.Text = "(" + reader["FileSize"].ToString() + ")";
				else
					Label6.Text = "<font color='red'><b>첨부된 파일없음</b></font>";					

			}
			reader.Close();			
			Con.Close();
		
		
			// 이전글, 다음글 보기...
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
						
			int Pre = 0, Next = 0;
			SqlCommand comm = new SqlCommand("SELECT TOP 1 HBizProductIntroIndex FROM HBP_T WHERE HBizProductIntroIndex < @HBizProductIntroIndex AND Distinction = 0 ORDER BY HBizProductIntroIndex DESC", conn);
			comm.Parameters.Add("@HBizProductIntroIndex", SqlDbType.Int).Value = Request.QueryString["HBizProductIntroIndex"];
			if(comm.ExecuteScalar() == null)
			{
				HyperLink4.Visible = false;				
			}
			else
			{
				Next = int.Parse(comm.ExecuteScalar().ToString());
			}

			comm = new SqlCommand("SELECT TOP 1 HBizProductIntroIndex FROM HBP_T WHERE HBizProductIntroIndex > @HBizProductIntroIndex AND Distinction = 0 ORDER BY HBizProductIntroIndex ASC", conn);
			comm.Parameters.Add("@HBizProductIntroIndex", SqlDbType.Int).Value = Request.QueryString["HBizProductIntroIndex"];
			if(comm.ExecuteScalar() == null)
			{
				HyperLink3.Visible = false;				
			}
			else
			{
				Pre = int.Parse(comm.ExecuteScalar().ToString()); 
			}
			
			HyperLink3.NavigateUrl = "BusinessProductAdjustment.aspx?HBizProductIntroIndex=" + Pre.ToString();
			HyperLink4.NavigateUrl = "BusinessProductAdjustment.aspx?HBizProductIntroIndex=" + Next.ToString();
									
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
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// 삭제버튼...
		private void Button1_Click(object sender, System.EventArgs e)
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
				Cmd.CommandText = "DELETE FROM HBP_T WHERE HBizProductIntroIndex = @HBizProductIntroIndex";							
				Cmd.Parameters.Add("@HBizProductIntroIndex", SqlDbType.Int);
				Cmd.Parameters["@HBizProductIntroIndex"].Value = HBizProductIntroIndex;
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
				Response.Redirect("BusinessProduct.aspx");
			}
		}



		// 수정버튼...
		private void Button2_Click(object sender, System.EventArgs e)
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
				if(File1.Value=="")
				{
					Cmd.CommandText = "UPDATE HBP_T SET Title=@Title, Contents=@Contents, RegistrationDate=@RegistrationDate, Category=@Category WHERE HBizProductIntroIndex=@HBizProductIntroIndex";
				}
				else
                    Cmd.CommandText = "UPDATE HBP_T SET Title=@Title, Contents=@Contents, AppendFile=@AppendFile, FileName=@FileName, FileSize=@FileSize, FileType=@FileType, RegistrationDate=@RegistrationDate, Category=@Category WHERE HBizProductIntroIndex=@HBizProductIntroIndex";

				Cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = Title.Text;
				Cmd.Parameters.Add("@Contents", SqlDbType.VarChar).Value = Content.Text;
				Cmd.Parameters.Add("@AppendFile", SqlDbType.Image).Value = Files1;
				Cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = FileName.ToString();
				Cmd.Parameters.Add("@FileSize", SqlDbType.Int).Value = FileSize;
				Cmd.Parameters.Add("@FileType", SqlDbType.VarChar).Value = ContentType;
				Cmd.Parameters.Add("@HBizProductIntroIndex", SqlDbType.Int).Value = HBizProductIntroIndex;
				Cmd.Parameters.Add("@RegistrationDate", SqlDbType.SmallDateTime).Value = System.DateTime.Now.ToString();
				Cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
				
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
				Response.Redirect("BusinessProduct.aspx");
				Con.Close();
			}
		}



		// 목록으로...
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("BusinessProduct.aspx");
		}
	}
}


