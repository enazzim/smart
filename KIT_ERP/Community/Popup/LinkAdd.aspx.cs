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

namespace KIT_ERP.Community.Popup
{
	/// <summary>
	/// LinkAdd에 대한 요약 설명입니다.
	/// </summary>
	public class LinkAdd : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox txtSiteName;
		protected System.Web.UI.WebControls.TextBox txtLink;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator4;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
	
		private string id = "";

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Con.Open();
		
				id = Session["ID"].ToString();
				Cmd.CommandText = "SELECT Category FROM CCG_T WHERE RegistrationPersonID = @id OR (Category = 'Category' AND Classify = '2')";
				Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
				SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
            
				DropDownList1.DataSource = reader;
				DropDownList1.DataTextField = "Category";
				DropDownList1.DataBind();
			}
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
		private void Button2_Click(object sender, System.EventArgs e)
		{
            Response.Write("<script>window.close();</script>");		
		}



		// 등록버튼...
		private void Button1_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			Con.Open();
			SqlCommand Cmd = new SqlCommand();
			SqlTransaction trans = Con.BeginTransaction();
			Cmd.Connection = Con;	
			Cmd.Transaction = trans;
						
			id = Session["ID"].ToString();
			//if(DropDownList1.SelectedIndex == 0)
				//Response.Write("<script>alert('Category를 선택해 주십시오');</script>");

			//else
			//{
				try
				{
					Cmd.CommandText = "INSERT INTO CL_T(Classification, SiteName, Link, RegistrationPersonID) VALUES(@Classification, @SiteName, @Link, @id)";

					Cmd.Parameters.Add("@Classification", SqlDbType.VarChar).Value = DropDownList1.SelectedItem.Text;
					Cmd.Parameters.Add("@SiteName", SqlDbType.VarChar).Value = txtSiteName.Text;
					Cmd.Parameters.Add("@Link", SqlDbType.VarChar).Value = "http://" + txtLink.Text;
					Cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
				
					Cmd.ExecuteNonQuery();
					trans.Commit();
					Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../Link.aspx'; self.close();</script>");
				}

				catch(Exception ex)
				{
					Response.Write(ex.Message);
					trans.Rollback();
				}

				finally
				{
					Con.Close();
				}			
			//}
		}
	}
}
