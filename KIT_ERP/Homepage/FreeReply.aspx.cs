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
	/// FreeReply에 대한 요약 설명입니다.
	/// </summary>
	public class FreeReply : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox RegistrationPerson;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.TextBox Password;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox Title;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator3;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox Content;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator4;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;

		private string HFreeBoardIndex = string.Empty;
		private string depth = string.Empty;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// 유효성검사후 스크립트 실행...
			string orginValidateCode;
			orginValidateCode = "if(typeof(Page_ClientValidate) == 'function') ";
			orginValidateCode += "if(!Page_ClientValidate()) return false;";

			string MyScriptCode;
			MyScriptCode = "if(!Registration_Check()) return false;";

			this.Button2.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			Button1.Attributes.Add("onclick", "return Cancel_Check()");

			if(!IsPostBack)
			{
				if (Request.Params["HFreeBoardIndex"] != null)
					HFreeBoardIndex = Request.Params["HFreeBoardIndex"].ToString();
				if (Request.Params["depth"] != null)
					depth = Request.Params["depth"].ToString();			
				
				// 답글쓰기 페이지에서 현재 로그인된 사용자 이름을 작성자란에 자동기입...
				if(Session["ID"] != null)
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
					RegistrationPerson.ReadOnly = true;
					Con.Close();
				}

				LoadData();
				Label1.Text = "◀ 자 유 게 시 판 ▶";				
			}
		}


		private void LoadData()
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;

			Con.Open();
			Cmd.Parameters.Add("@HFreeBoardIndex", SqlDbType.Int);
			Cmd.Parameters["@HFreeBoardIndex"].Value = HFreeBoardIndex;


			Cmd.CommandText = "SELECT * FROM HFB_T WHERE HFreeBoardIndex = @HFreeBoardIndex";
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

			if(reader.Read())
			{
				Title.Text = reader["Title"].ToString();
				//Content.Text = reader["Contents"].ToString();
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		// 취소버튼...
		private void Button1_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("FreeBoard.aspx");
		}



		// 등록버튼...
		private void Button2_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			
			string sp;
			sp = "UP_INSERT_HFREEREPLY";
			
			SqlCommand Cmd = new SqlCommand(sp, Con);
			Cmd.CommandType = CommandType.StoredProcedure;

			Cmd.Parameters.Add("@ParentHFreeBoardIndex", SqlDbType.Int);
			Cmd.Parameters.Add("@PrevHFreeBoardIndex", SqlDbType.Int);
			Cmd.Parameters.Add("@depth", SqlDbType.Int);
			Cmd.Parameters.Add("@RegistrationPerson", SqlDbType.VarChar);						
			Cmd.Parameters.Add("@Title", SqlDbType.VarChar);
			Cmd.Parameters.Add("@Contents", SqlDbType.VarChar);
			Cmd.Parameters.Add("@Password", SqlDbType.VarChar);

			
			int HFreeBoardIndex2 = int.Parse(HFreeBoardIndex = Request.Params["HFreeBoardIndex"].ToString());
			int depth2 = int.Parse(depth = Request.Params["depth"].ToString());
			int prevHFreeBoardIndex2 = (HFreeBoardIndex2-1)/1000 * 1000;


			Cmd.Parameters["@ParentHFreeBoardIndex"].Value = HFreeBoardIndex2;
			Cmd.Parameters["@PrevHFreeBoardIndex"].Value = prevHFreeBoardIndex2;
			Cmd.Parameters["@depth"].Value = depth2 + 1;
			Cmd.Parameters["@RegistrationPerson"].Value = RegistrationPerson.Text;						
			Cmd.Parameters["@Title"].Value = Title.Text;			
			Cmd.Parameters["@Contents"].Value = Content.Text;
			Cmd.Parameters["@Password"].Value = Password.Text;

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
