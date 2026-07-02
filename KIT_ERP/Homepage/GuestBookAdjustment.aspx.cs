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
	/// GustBookAdjustment에 대한 요약 설명입니다.
	/// </summary>
	public class GustBookAdjustment : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblRegistrationPerson;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label lblRegistrationDate;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label lblHits;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox Title;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox Content;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.HyperLink HyperLink3;
		protected System.Web.UI.WebControls.HyperLink HyperLink4;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button1;

		private string HGuestIndex = "";
		private string updateHits = "0";		
		private string name = "";
		private string Password = "";
	
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

			if(!IsPostBack)
			{
				// 현재 클릭된 글의 HGuestIndex컬럼값을 Request개체로 얻어서 그 값을 저장한다.
				HGuestIndex = Request.QueryString["HGuestIndex"];
				// HGuestIndex값이 null이면 사용자가 GuestBook.aspx 페이지에서 특정 제목을 클릭하여 GuestBookAdjustment.aspx 페이지로
				// 이동해 온 것이 아니라, 사용자가 브라우저에 직접적으로 GuestBookAdjustment.aspx 페이지를 요청하여 접근한 것이므로
				// Response.Redirect 메서드를 사용하여 GuestBook.aspx 페이지로 이동시킨다.
				if(HGuestIndex == null)
					Response.Redirect("GuestBook.aspx");

				if(Request.Cookies["HG_T"] != null)
					updateHits = Request.Cookies["HG_T"]["UpdateHits"];

			
				if((Session["ID"] != null) && (Session["ID"].ToString() == "admin"))
				{
					// 현재 로그인된 ID의 사용자 이름을 name에 저장한다
					string id = Session["ID"].ToString();
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					SqlCommand Cmd = new SqlCommand();
					Cmd.Connection = Con;				
					Cmd.CommandText = "SELECT Name FROM UI_MT WHERE ID = @id";
					Cmd.Parameters.Add("@id", SqlDbType.VarChar, 10);
					Cmd.Parameters["@id"].Value = id;
					Con.Open();
					name = Cmd.ExecuteScalar().ToString();
					Con.Close();
				}
				LoadData();
				Label1.Text = "◀ 방 명 록 ▶";				
			}
		}
		
        

		private void LoadData()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;

			Con.Open();
			Cmd.Parameters.Add("@HGuestIndex", SqlDbType.Int);
			Cmd.Parameters["@HGuestIndex"].Value = HGuestIndex;

			if(updateHits == "0")
			{
				Cmd.CommandText = "UPDATE HG_T SET Hits = Hits + 1 WHERE HGuestIndex = @HGuestIndex";				
				Cmd.ExecuteNonQuery();
				Response.Cookies["HG_T"]["UpdateHits"] = "1";
			}

			Cmd.CommandText = "SELECT HGuestIndex, Title, Contents, RegistrationPerson, Convert(varchar, HG_T.RegistrationDate, 111)RegistrationDate, Hits, Depth, Password FROM HG_T WHERE HGuestIndex = @HGuestIndex";
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

			if(reader.Read())
			{
				
				lblRegistrationPerson.Text = reader["RegistrationPerson"].ToString();
				lblRegistrationDate.Text = reader["RegistrationDate"].ToString();
				lblHits.Text = reader["Hits"].ToString();
				Title.Text = reader["Title"].ToString();
				Content.Text = reader["Contents"].ToString();				
			}
			

			ViewState["HGuestIndex"] = reader["HGuestIndex"].ToString();
			ViewState["depth"] = reader["depth"].ToString();
			reader.Close();	
			Con.Close();


			// 이전글, 다음글 보기...
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
						
			int Pre = 0, Next = 0;
			SqlCommand comm = new SqlCommand("SELECT TOP 1 HGuestIndex FROM HG_T WHERE HGuestIndex < @HGuestIndex ORDER BY HGuestIndex DESC", conn);
			comm.Parameters.Add("@HGuestIndex", SqlDbType.Int).Value = Request.QueryString["HGuestIndex"];
			if(comm.ExecuteScalar() == null)
			{
				HyperLink4.Visible = false;				
			}
			else
			{
				Next = int.Parse(comm.ExecuteScalar().ToString());
			}

			comm = new SqlCommand("SELECT TOP 1 HGuestIndex FROM HG_T WHERE HGuestIndex > @HGuestIndex ORDER BY HGuestIndex ASC", conn);
			comm.Parameters.Add("@HGuestIndex", SqlDbType.Int).Value = Request.QueryString["HGuestIndex"];
			if(comm.ExecuteScalar() == null)
			{
				HyperLink3.Visible = false;
			}
			else
			{
				Pre = int.Parse(comm.ExecuteScalar().ToString()); 
			}
			
			HyperLink3.NavigateUrl = "GuestBookAdjustment.aspx?HGuestIndex=" + Pre.ToString();
			HyperLink4.NavigateUrl = "GuestBookAdjustment.aspx?HGuestIndex=" + Next.ToString();
						
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
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
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
			SqlCommand Cmd = new SqlCommand();
			SqlTransaction trans = Con.BeginTransaction();
			Cmd.Connection = Con;
			Cmd.Transaction = trans;

			Cmd.CommandText = "SELECT Password FROM HG_T WHERE HGuestIndex = @HGuestIndex";
			Cmd.Parameters.Add("@HGuestIndex", SqlDbType.Int).Value = Request.QueryString["HGuestIndex"];			
			Password = Cmd.ExecuteScalar().ToString();


			if((Session["ID"] != null) && (Session["ID"].ToString() == "admin"))
			{
				try
				{				
					Cmd.CommandText = "DELETE FROM HG_T WHERE HGuestIndex <= @HGIndex AND HGuestIndex > (@HGIndex-1000)";
					Cmd.Parameters.Add("@HGIndex", SqlDbType.Int);
					Cmd.Parameters["@HGIndex"].Value = Request.QueryString["HGuestIndex"];
			
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
					Response.Write("<script>alert('삭제 되었습니다.'); location.href='../Homepage/GuestBook.aspx?HGuestIndex=" + HGuestIndex + "';; </script>");
				}
			}
			else if(TextBox1.Text == Password)
			{
				try
				{
					Cmd.CommandText = "DELETE FROM HG_T WHERE HGuestIndex <= @HIndex AND HGuestIndex > (@HIndex-1000)";
					Cmd.Parameters.Add("@HIndex", SqlDbType.Int);
					Cmd.Parameters["@HIndex"].Value = Request.QueryString["HGuestIndex"];
			
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
					Response.Write("<script>alert('삭제 되었습니다.'); location.href='../Homepage/GuestBook.aspx?HGuestIndex=" + HGuestIndex + "';; </script>");
				}
			}
			else
				Response.Write("<script>alert('비밀번호가 일치하지 않습니다.');</script>");		
		}



		// 수정버튼...
		private void Button2_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlCommand Cmd = new SqlCommand(ConnectStr);
			SqlTransaction trans = Con.BeginTransaction();
			Cmd.Connection = Con;
			Cmd.Transaction = trans;

			Cmd.CommandText = "SELECT Password FROM HG_T WHERE HGuestIndex = @HGuestIndex";
			Cmd.Parameters.Add("@HGuestIndex", SqlDbType.Int).Value = Request.QueryString["HGuestIndex"];			
			Password = Cmd.ExecuteScalar().ToString();


			if((Session["ID"] != null) && (Session["ID"].ToString() == "admin"))
			{
				try
				{
					Cmd.CommandText = "UPDATE HG_T SET Title=@Title, Contents=@Contents, RegistrationDate=@RegistrationDate WHERE HGuestIndex=@HGIndex";

					Cmd.Parameters.Add("@Title", SqlDbType.VarChar, 100);
					Cmd.Parameters.Add("@Contents", SqlDbType.VarChar, 7000);
					Cmd.Parameters.Add("@HGIndex", SqlDbType.Int);
					Cmd.Parameters.Add("@RegistrationDate", SqlDbType.SmallDateTime);

					Cmd.Parameters["@Title"].Value = Title.Text;
					Cmd.Parameters["@Contents"].Value = Content.Text;
					Cmd.Parameters["@HGIndex"].Value = Request.QueryString["HGuestIndex"];
					Cmd.Parameters["@RegistrationDate"].Value = System.DateTime.Now.ToString();
			
					Cmd.ExecuteNonQuery();
					trans.Commit();
				}
				catch(Exception ex)
				{
					Response.Write(ex.Message);
				}
				finally
				{					
					Con.Close();
					Response.Write("<script>alert('수정 되었습니다.'); location.href='../Homepage/GuestBook.aspx?HGuestIndex=" + HGuestIndex + "';; </script>");
				}
			}
			else if(TextBox1.Text == Password)
			{
				try
				{
					Cmd.CommandText = "UPDATE HG_T SET Title=@Title, Contents=@Contents, RegistrationDate=@RegistrationDate WHERE HGuestIndex=@HIndex";
				
					Cmd.Parameters.Add("@Title", SqlDbType.VarChar, 100);
					Cmd.Parameters.Add("@Contents", SqlDbType.VarChar, 7000);
					Cmd.Parameters.Add("@HIndex", SqlDbType.Int);
					Cmd.Parameters.Add("@RegistrationDate", SqlDbType.SmallDateTime);
				
					Cmd.Parameters["@Title"].Value = Title.Text;
					Cmd.Parameters["@Contents"].Value = Content.Text;
					Cmd.Parameters["@HIndex"].Value = Request.QueryString["HGuestIndex"];
					Cmd.Parameters["@RegistrationDate"].Value = System.DateTime.Now.ToString();
							
					Cmd.ExecuteNonQuery();
					trans.Commit();
				}
				catch(Exception ex)
				{
					Response.Write(ex.Message);
				}
				finally
				{			
					Con.Close();
					Response.Write("<script>alert('수정 되었습니다.'); location.href='../Homepage/GuestBook.aspx?HGuestIndex=" + HGuestIndex + "';; </script>");
				}
			}
			else
				Response.Write("<script>alert('비밀번호가 일치하지 않습니다.');</script>");			
		}



		// 목록으로...
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("GuestBook.aspx");
		}



		// 답변버튼...
		private void Button3_Click(object sender, System.EventArgs e)
		{
			string HGuestIndex = ViewState["HGuestIndex"].ToString();
			string depth = ViewState["depth"].ToString();			

			Response.Redirect("GuestBookReply.aspx?HGuestIndex=" + HGuestIndex + "&depth=" + depth);	
		}
	}
}
