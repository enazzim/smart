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
using System.Web.Security;
using System.Data.SqlClient;

namespace KIT_ERP
{
	/// <summary>
	/// Login에 대한 요약 설명입니다.
	/// </summary>
	public class Login : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.TextBox TextBox2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			

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
			this.ImageButton1.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			string id = TextBox1.Text;
			string pwd = TextBox2.Text;

			if(id.Trim() == "")
				Response.Write("<script>alert('아이디를 입력하세요!');</script>");
			else if(pwd.Trim() == "")
				Response.Write("<script>alert('비밀번호를 입력하세요!');</script>");
			else if(Authenticate(id, pwd))
				Response.Write("<script>top.location.href='Main.htm';</script>");
		}

		//인증 함수
		private bool Authenticate(string id, string pwd)
		{
			bool bExist = false;

			SqlConnection Con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"].ToString());
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "SELECT COUNT(ID) FROM UI_MT WHERE id = @ID AND Password = @pwd and RecodingState = 1";
			Cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = id.ToLower().Trim();
			Cmd.Parameters.Add("@pwd", SqlDbType.VarChar).Value = pwd;
			try
			{
				Con.Open();
				if(int.Parse(Cmd.ExecuteScalar().ToString()) != 0)
				{
					bExist = true;
					Session["ID"] = id;
					UserName(id);
				}
				else
				{
					TextBox2.Text = "";
					throw new Exception("비밀번호가 일치하지 않습니다!");	
					
				}
			}
			catch(Exception ex)
			{
				RegisterStartupScript("","<script>alert('"+ex.Message+"');</script>");
				bExist = false;
			}
			finally
			{
				Con.Close();	
			}

			return bExist;
		}

		private void UserName(string ID)
		{
			Session["UserName"] = "";

			SqlConnection Con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"].ToString());
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "SELECT Name FROM UI_MT WHERE id = @ID and RecodingState = 1";
			Cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
			Con.Open();
			SqlDataReader dr = Cmd.ExecuteReader();

			if(dr.Read())
			{
				Session["UserName"] = dr["Name"].ToString();
			}
			dr.Close();
			Con.Close();

			if(Session["UserName"].ToString() == "")
				throw new Exception(ID+"에 해당하는 이름이 존재하지 않습니다!");

		}
	}
}
