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
using System.Data.SqlClient;


namespace KIT_ERP
{
	/// <summary>
	/// Top에 대한 요약 설명입니다.
	/// </summary>
	public class Top : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden helpUrl;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.LinkButton LinkButton3;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = 'Login.aspx';</script>");
			}
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			Session.Abandon();
			Page.RegisterClientScriptBlock("SEND", "<script>LogOut();</script>");
		}

		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language=javascript>");
			Response.Write("window.open('Help/프레임.htm','팝업창','location=no,directories=no,resizable=0,status=no,toolbar=no,menubar=no,width=718,height=570,scrollbars=no');");
			Response.Write("</script>");  
		}
		
	}
}
