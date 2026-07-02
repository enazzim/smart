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
	/// FreeAdjustment에 대한 요약 설명입니다.
	/// </summary>
	public class FreeAdjustment : System.Web.UI.Page
	{
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
		protected System.Web.UI.WebControls.Button btnEdit;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button btnReply;
		protected System.Web.UI.WebControls.LinkButton lbkList;
		protected System.Web.UI.WebControls.HyperLink hlnkPreContent;
		protected System.Web.UI.WebControls.HyperLink hlnkNextContent;
		
		private string CFreeBoardIndex = "";
		private string updateHits = "0";
		private string name = "";
	
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
			btnDelete.Attributes.Add("onclick", "return Delete_Check();");			
			
			
			// 현재 클릭된 글의 CFreeBoardIndex컬럼값을 Request개체로 얻어서 그 값을 저장한다.
			CFreeBoardIndex = Request.QueryString["CFreeBoardIndex"];
			// CFreeBoardIndex값이 null이면 사용자가 Notice.aspx 페이지에서 특정 제목을 클릭하여 FreeAdjustment.aspx 페이지로
			// 이동해 온 것이 아니라, 사용자가 브라우저에 직접적으로 NoAdjustment.aspx 페이지를 요청하여 접근한 것이므로
			// Response.Redirect 메서드를 사용하여 FreeBoard.aspx 페이지로 이동시킨다.
			if(CFreeBoardIndex == null)
				Response.Redirect("FreeBoard.aspx");

			if(Request.Cookies["CFB_T"] != null)
				updateHits = Request.Cookies["CFB_T"]["UpdateHits"];

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
        

		private void LoadData()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;

			Con.Open();
			Cmd.Parameters.Add("@CFreeBoardIndex", SqlDbType.Int);
			Cmd.Parameters["@CFreeBoardIndex"].Value = CFreeBoardIndex;

			if(updateHits == "0")
			{
				Cmd.CommandText = "UPDATE CFB_T SET Hits = Hits + 1 WHERE CFreeBoardIndex = @CFreeBoardIndex";				
				Cmd.ExecuteNonQuery();
				Response.Cookies["CFB_T"]["UpdateHits"] = "1";
			}

			Cmd.CommandText = "SELECT CFreeBoardIndex, Title, Contents, RegistrationPerson, Convert(varchar, CFB_T.RegistrationDate, 111)RegistrationDate, Hits, Depth FROM CFB_T WHERE CFreeBoardIndex = @CFreeBoardIndex";
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

			if(reader.Read())
			{
				lblRegistrationPerson.Text = reader["RegistrationPerson"].ToString();
				lblRegistrationDate.Text = reader["RegistrationDate"].ToString();
				lblHits.Text = reader["Hits"].ToString();
				txtTitle.Text = reader["Title"].ToString();
				txtContents.Text = reader["Contents"].ToString();				
			}

			if((Session["ID"].ToString() == "admin") || (name == reader["RegistrationPerson"].ToString()))
			{
				txtTitle.ReadOnly = false;
				txtContents.ReadOnly = false;
				btnEdit.Enabled = true;
				btnDelete.Enabled = true;
			}

			if((Session["ID"].ToString() == "admin") && (reader["RegistrationPerson"].ToString() != "관리자"))
			{
				txtTitle.ReadOnly = true;
				txtContents.ReadOnly = true;
			}

			
			ViewState["CFreeBoardIndex"] = reader["CFreeboardIndex"].ToString();
			ViewState["depth"] = reader["depth"].ToString();
			reader.Close();	
			Con.Close();


			// 이전글, 다음글 보기...
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
						
			int Pre = 0, Next = 0;
			SqlCommand comm = new SqlCommand("SELECT TOP 1 CFreeBoardIndex FROM CFB_T WHERE CFreeBoardIndex < @CFreeBoardIndex ORDER BY CFreeBoardIndex DESC", conn);
			comm.Parameters.Add("@CFreeBoardIndex", SqlDbType.Int).Value = Request.QueryString["CFreeBoardIndex"];
			if(comm.ExecuteScalar() == null)
			{
				hlnkNextContent.Visible = false;
			}
			else
			{
				Next = int.Parse(comm.ExecuteScalar().ToString());
			}

			comm = new SqlCommand("SELECT TOP 1 CFreeBoardIndex FROM CFB_T WHERE CFreeBoardIndex > @CFreeBoardIndex ORDER BY CFreeBoardIndex ASC", conn);
			comm.Parameters.Add("@CFreeBoardIndex", SqlDbType.Int).Value = Request.QueryString["CFreeBoardIndex"];
			if(comm.ExecuteScalar() == null)
			{
				hlnkPreContent.Visible = false;
			}
			else
			{
				Pre = int.Parse(comm.ExecuteScalar().ToString()); 
			}
			
			hlnkPreContent.NavigateUrl = "FreeAdjustment.aspx?CFreeBoardIndex=" + Pre.ToString();
			hlnkNextContent.NavigateUrl = "FreeAdjustment.aspx?CFreeBoardIndex=" + Next.ToString();
						
			conn.Close();
		}


		// 삭제하기...
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			
			Con.Open();
			
			Cmd.CommandText = "DELETE FROM CFB_T WHERE CFreeBoardIndex <= @CFreeBoardIndex AND CFreeBoardIndex > (@CFreeBoardIndex-1000)";
			Cmd.Parameters.Add("@CFreeBoardIndex", SqlDbType.Int);
			Cmd.Parameters["@CFreeBoardIndex"].Value = CFreeBoardIndex;
			
			Cmd.ExecuteNonQuery();
			Con.Close();
			Response.Redirect("FreeBoard.aspx");
		}




		// 수정하기...
		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand(ConnectStr);
			Cmd.Connection = Con;
			Cmd.CommandText = "UPDATE CFB_T SET Title=@Title, Contents=@Contents, RegistrationDate=@RegistrationDate WHERE CFreeBoardIndex=@CFreeBoardIndex";

			Cmd.Parameters.Add("@Title", SqlDbType.VarChar, 100);
			Cmd.Parameters.Add("@Contents", SqlDbType.VarChar, 7000);
			Cmd.Parameters.Add("@CFreeBoardIndex", SqlDbType.Int);
			Cmd.Parameters.Add("@RegistrationDate", SqlDbType.SmallDateTime);

			Cmd.Parameters["@Title"].Value = txtTitle.Text;
			Cmd.Parameters["@Contents"].Value = txtContents.Text;
			Cmd.Parameters["@CFreeBoardIndex"].Value = CFreeBoardIndex;
			Cmd.Parameters["@RegistrationDate"].Value = System.DateTime.Now.ToString();

			Con.Open();
			Cmd.ExecuteNonQuery();
			Response.Redirect("FreeBoard.aspx");
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
			this.btnReply.Click += new System.EventHandler(this.btnReply_Click);
			this.lbkList.Click += new System.EventHandler(this.lbkList_Click);
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion	

		// 목록...
		private void lbkList_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("FreeBoard.aspx");
		}

		
		// 답변...
		private void btnReply_Click(object sender, System.EventArgs e)
		{
			string CFreeBoardIndex = ViewState["CFreeBoardIndex"].ToString();
			string depth = ViewState["depth"].ToString();			

			Response.Redirect("FreeReply.aspx?CFreeBoardIndex=" + CFreeBoardIndex + "&depth=" + depth);		
		}		
	}
}