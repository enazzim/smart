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
	/// Top에 대한 요약 설명입니다.
	/// </summary>
	public class Top : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.WebControls.LinkButton LinkButton3;
		protected System.Web.UI.WebControls.LinkButton LinkButton4;
		protected System.Web.UI.WebControls.LinkButton LinkButton5;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton LinkButton6;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["Day"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = "SELECT CompanyName FROM UCI_T";
			Con.Open();
			if(!Convert.IsDBNull(Cmd.ExecuteScalar()))
                Label1.Text = Convert.ToString(Cmd.ExecuteScalar());			
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
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.LinkButton3.Click += new System.EventHandler(this.LinkButton3_Click);
			this.LinkButton4.Click += new System.EventHandler(this.LinkButton4_Click);
			this.LinkButton5.Click += new System.EventHandler(this.LinkButton5_Click);
			this.LinkButton6.Click += new System.EventHandler(this.LinkButton6_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		// 회사소개 버튼...
		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'CompanyIntroduce.aspx';</script>");
		}


		// 사업/제품개요 버튼...
		private void LinkButton3_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'BusinessProduct.aspx';</script>");
		}


		// 자유게시판 버튼..
		private void LinkButton4_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'FreeBoard.aspx';</script>");
		}


		// 자료실 버튼...
		private void LinkButton5_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'PDS.aspx';</script>");
		}


		// 방명록 버튼...
		private void LinkButton6_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'GuestBook.aspx';</script>");
		}



		// 홈으로...
		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>document.parentWindow.parent.Contents.location = 'Contents.aspx';</script>");
		}
	}
}
