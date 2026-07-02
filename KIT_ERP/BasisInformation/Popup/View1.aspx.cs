using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;


namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// View1에 대한 요약 설명입니다.
	/// </summary>
	public class View1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lbContents;
		protected System.Web.UI.WebControls.Label lbWriter;
		protected System.Web.UI.WebControls.Label lbDate;
		protected System.Web.UI.WebControls.Label lbTitle;
		protected System.Web.UI.WebControls.Label lbSize;
		protected System.Web.UI.WebControls.Button bt_Close;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
			
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				string str = "select * From "+Request.QueryString["table"]+" where seq = "+Request.QueryString["index"];
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataReader dr = comm.ExecuteReader();

				while(dr.Read())
				{
					lbWriter.Text = dr["writer"].ToString();
					lbDate.Text = dr["transdate"].ToString();
					lbTitle.Text = dr["Title"].ToString();
					lbContents.Text = ReplaceBR(dr["Content"].ToString());
					HyperLink1.Text = dr["FileName"].ToString();
					HyperLink1.NavigateUrl = "Down.aspx?index=" + Request.QueryString["index"] + "&table="+Request.QueryString["table"]+"&filename=AppendFile";
				
					if(dr["FileSize"].ToString() != "0")
						lbSize.Text = "(" + dr["FileSize"].ToString() + ")";
					else
						lbSize.Text = "첨부된 파일없음";

				}
				conn.Close();

			
			}
		}

		private string ReplaceBR(string s)
		{
			string a =  s.Replace("\n", "<BR>");
			return a.Replace(" ", "&nbsp;");
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
			this.bt_Close.Click += new System.EventHandler(this.bt_Close_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Close_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script language='javascript'>");
			Response.Write("window.close();");
			Response.Write("</script>");
			Response.End();
		}
	}
}
