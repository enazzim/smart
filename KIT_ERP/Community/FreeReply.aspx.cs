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
	/// FreeReply에 대한 요약 설명입니다.
	/// </summary>
	public class FreeReply : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox RegistrationPerson;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox Title;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox Content;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Button btnApply;
		private string CFreeBoardIndex = string.Empty;
		private string depth = string.Empty;
	
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
			MyScriptCode = "if(!Registration_Check()) return false;";

			this.btnApply.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			btnCancel.Attributes.Add("onclick", "return Cancel_Check();");

			if(!IsPostBack)
			{
				if (Request.Params["CFreeBoardIndex"] != null)
					CFreeBoardIndex = Request.Params["CFreeBoardIndex"].ToString();
				if (Request.Params["depth"] != null)
					depth = Request.Params["depth"].ToString();			
				
				// 답글쓰기 페이지에서 현재 로그인된 사용자 이름을 작성자란에 자동기입...
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


			Cmd.CommandText = "SELECT * FROM CFB_T WHERE CFreeBoardIndex = @CFreeBoardIndex";
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

			if(reader.Read())
			{
				Title.Text = "[RE] " + reader["Title"].ToString();
//				Content.Text = reader["Contents"].ToString();
			}			
			reader.Close();						
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
			Response.Redirect("FreeBoard.aspx");
		}

		
		// 등록버튼...
		private void btnApply_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			
			string sp;
			sp = "UP_INSERT_FREEREPLY";
			
			SqlCommand Cmd = new SqlCommand(sp, Con);
			Cmd.CommandType = CommandType.StoredProcedure;

			Cmd.Parameters.Add("@ParentCFreeBoardIndex", SqlDbType.Int);
			Cmd.Parameters.Add("@PrevCFreeBoardIndex", SqlDbType.Int);
			Cmd.Parameters.Add("@depth", SqlDbType.Int);

			Cmd.Parameters.Add("@RegistrationPerson", SqlDbType.VarChar, 20);						
			Cmd.Parameters.Add("@Title", SqlDbType.VarChar, 100);
			Cmd.Parameters.Add("@Contents", SqlDbType.VarChar, 7000);

			
			int CFreeBoardIndex2 = int.Parse(CFreeBoardIndex = Request.Params["CFreeBoardIndex"].ToString());
			int depth2 = int.Parse(depth = Request.Params["depth"].ToString());
			int prevCFreeBoardIndex2 = (CFreeBoardIndex2-1)/1000 * 1000;


			Cmd.Parameters["@ParentCFreeBoardIndex"].Value = CFreeBoardIndex2;
			Cmd.Parameters["@PrevCFreeBoardIndex"].Value = prevCFreeBoardIndex2;
			Cmd.Parameters["@depth"].Value = depth2 + 1;

			Cmd.Parameters["@RegistrationPerson"].Value = RegistrationPerson.Text;						
			Cmd.Parameters["@Title"].Value = Title.Text;			
			Cmd.Parameters["@Contents"].Value = Content.Text;

			Con.Open();
			Cmd.ExecuteNonQuery();
			Con.Close();

			Response.Redirect("FreeBoard.aspx");


			if(Con.State == ConnectionState.Open)
				Con.Close();

			Cmd = null;
			Con = null;
			
		}
	}
}
