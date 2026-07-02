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
	/// NoAdjustment에 대한 요약 설명입니다.
	/// </summary>
	public class NoAdjustment : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.Button btnDelete;		
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblRegistrationPerson;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblRegistrationDate;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label lblHits;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox txtContents;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.LinkButton lbkList;
		protected System.Web.UI.WebControls.HyperLink hlnkPreContent;
		protected System.Web.UI.WebControls.HyperLink hlnkNextContent;

		private string CNoticeIndex = "";    // 현재 선택된 글의 번호
		private string updateHits = "0";     // 현재글의 조회수
		private string name = "";            // 현재 로그인된 사용자 이름...
			
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Homepage/default.htm';</script>");
			}
			// 유효성 검사후 스크립트 실행...
			string orginValidateCode;
			orginValidateCode = "if(typeof(Page_ClientValidate) == 'function')";
			orginValidateCode += "if(!Page_ClientValidate()) return false;";

			string MyScriptCode;
			MyScriptCode = "if(!Edit_Check()) return false;";

			this.btnEdit.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			btnDelete.Attributes.Add("onclick", "return Delete_Check();");

			// 현재 클릭된 글의 CNoticeIndex컬럼값을 Request개체로 얻어서 그 값을 저장한다.
			CNoticeIndex = Request.QueryString["CNoticeIndex"];
			// CNoticeIndex값이 null이면 사용자가 Notice.aspx 페이지에서 특정 제목을 클릭하여 NoAdjustment.aspx 페이지로
			// 이동해 온 것이 아니라, 사용자가 브라우저에 직접적으로 NoAdjustment.aspx 페이지를 요청하여 접근한 것이므로
			// Response.Redirect 메서드를 사용하여 Notice.aspx 페이지로 이동시킨다.
			if(CNoticeIndex == null)
				Response.Redirect("Notice.aspx");

			if(Request.Cookies["CN_T"] != null)
				updateHits = Request.Cookies["CN_T"]["UpdateHits"];

			if(!IsPostBack)
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
				LoadData();
			}
		}
        

		// 내용보기...
		private void LoadData()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;

			Con.Open();
			Cmd.Parameters.Add("@CNoticeIndex", SqlDbType.Int);
			Cmd.Parameters["@CNoticeIndex"].Value = CNoticeIndex;

			// 조회수 증가
			if(updateHits == "0")
			{
				Cmd.CommandText = "UPDATE CN_T SET Hits = Hits + 1 WHERE CNoticeIndex = @CNoticeIndex";				
				Cmd.ExecuteNonQuery();
				Response.Cookies["CN_T"]["UpdateHits"] = "1";
			}

			Cmd.CommandText = "SELECT * FROM CN_T WHERE CNoticeIndex = @CNoticeIndex";
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

			if(reader.Read())
			{
				lblRegistrationPerson.Text = reader["RegistrationPerson"].ToString();
				lblRegistrationDate.Text = reader["RegistrationDate"].ToString();
				lblHits.Text = reader["Hits"].ToString();
				txtTitle.Text = reader["Title"].ToString();
				txtContents.Text = reader["Contents"].ToString();				
			}

			// 현재글을 보는이가 admin일 경우에만 텍스트박스, 수정(삭제)버튼 활성화...
			if(Session["ID"].ToString() == "admin")
			{							
				btnDelete.Enabled = true;				
			}

			if(name == lblRegistrationPerson.Text)
			{
				txtTitle.ReadOnly = false;
				txtContents.ReadOnly = false;
				btnEdit.Enabled = true;
				btnDelete.Enabled = true;				
			}

			reader.Close();			
            Con.Close();

			
			// 이전글, 다음글 보기...
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
						
			int Pre = 0, Next = 0;
			SqlCommand comm = new SqlCommand("SELECT TOP 1 CNoticeIndex FROM CN_T WHERE CNoticeIndex < @CNoticeIndex ORDER BY CNoticeIndex DESC", conn);
			comm.Parameters.Add("@CNoticeIndex", SqlDbType.Int).Value = Request.QueryString["CNoticeIndex"];
			if(comm.ExecuteScalar() == null)
			{
				hlnkNextContent.Visible = false;
			}
			else
			{
				Next = int.Parse(comm.ExecuteScalar().ToString());
			}

			comm = new SqlCommand("SELECT TOP 1 CNoticeIndex FROM CN_T WHERE CNoticeIndex > @CNoticeIndex ORDER BY CNoticeIndex ASC", conn);
			comm.Parameters.Add("@CNoticeIndex", SqlDbType.Int).Value = Request.QueryString["CNoticeIndex"];
			if(comm.ExecuteScalar() == null)
			{
				hlnkPreContent.Visible = false;
			}
			else
			{
				Pre = int.Parse(comm.ExecuteScalar().ToString()); 
			}
			
			hlnkPreContent.NavigateUrl = "NoAdjustment.aspx?CNoticeIndex=" + Pre.ToString();
			hlnkNextContent.NavigateUrl = "NoAdjustment.aspx?CNoticeIndex=" + Next.ToString();
						
			conn.Close();
		}


		// 삭제...
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;

			Con.Open();

			Cmd.CommandText = "DELETE FROM CN_T WHERE CNoticeIndex = @CNoticeIndex";
			Cmd.Parameters.Add("@CNoticeIndex", SqlDbType.Int);
			Cmd.Parameters["@CNoticeIndex"].Value = CNoticeIndex;

			Cmd.ExecuteNonQuery();
			Con.Close();
			Response.Redirect("Notice.aspx");
		}


		// 수정...
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand(ConnectStr);
			Cmd.Connection = Con;
			Cmd.CommandText = "UPDATE CN_T SET Title=@Title, Contents=@Contents, RegistrationDate=@RegistrationDate WHERE CNoticeIndex=@CNoticeIndex";

			Cmd.Parameters.Add("@Title", SqlDbType.VarChar, 100);
			Cmd.Parameters.Add("@Contents", SqlDbType.VarChar, 7000);
			Cmd.Parameters.Add("@CNoticeIndex", SqlDbType.Int);
			Cmd.Parameters.Add("@RegistrationDate", SqlDbType.SmallDateTime);

			Cmd.Parameters["@Title"].Value = txtTitle.Text;
			Cmd.Parameters["@Contents"].Value = txtContents.Text;
			Cmd.Parameters["@CNoticeIndex"].Value = CNoticeIndex;
			Cmd.Parameters["@RegistrationDate"].Value = System.DateTime.Now.ToString();

			Con.Open();
			Cmd.ExecuteNonQuery();
			Response.Redirect("Notice.aspx");
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
			this.lbkList.Click += new System.EventHandler(this.lbkList_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion	

		// 목록...
		private void lbkList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Notice.aspx");
		}

		private void lnkPreContent_Click(object sender, System.EventArgs e)
		{
		
		}
		
	}
}
