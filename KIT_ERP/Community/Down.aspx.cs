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
using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.Community
{
	/// <summary>
	/// Down에 대한 요약 설명입니다.
	/// </summary>
	public class Down : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Con.Open();
				string Division = Request.QueryString["Division"].ToString();
				switch(Division)
				{
					case "Notice"://공지(T_Notice)
						Cmd.CommandText = "SELECT * FROM T_Notice WHERE seq = @seq";
						break;
					case "Development"://레이저 바인딩(T_Development)
						Cmd	.CommandText = "SELECT * FROM T_Development WHERE seq = @seq";
						break;
					case "Product"://생산자재 바인딩(T_Product)
						Cmd	.CommandText = "SELECT * FROM T_Product WHERE seq = @seq";
						break;
					case "Materials"://영업QC 바인딩(T_Materials)
						Cmd	.CommandText = "SELECT * FROM T_Materials WHERE seq = @seq";
						break;
					case "WorkDailyBoard"://업무일지 바인딩(WorkReport)
						Cmd	.CommandText = "SELECT * FROM T_WorkDailyBoard WHERE seq = @seq";
						break;
					case "Institute"://연구소 바인딩(T_Institute)
						Cmd	.CommandText = "SELECT * FROM T_Institute WHERE seq = @seq";
						break;
					case "Loader"://로더생산현황 바인딩 (T_Loader)
						Cmd	.CommandText = "SELECT * FROM T_Loader WHERE seq = @seq";
						break;
					case "PresidentNotice"://대표 공지 바인딩(T_PresidentNotice)
						Cmd.CommandText = "SELECT * FROM T_PresidentNotice WHERE seq = @seq";
						break;
					case "Pipe"://파이프 생산관리(T_Pipe)
						Cmd.CommandText = "SELECT * FROM T_Pipe WHERE seq = @seq";
						break;
				}
				Cmd.Parameters.Add("@seq", SqlDbType.Int);
				Cmd.Parameters["@seq"].Value = Request.QueryString["seq"];
				
				SqlDataReader reader = Cmd.ExecuteReader();
				reader.Read();

				Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlPathEncode(reader["FileName"].ToString()));
				Response.ContentType = reader["FileType"].ToString();
				Response.OutputStream.Write((byte[])reader["AppendFile"], 0, (int)reader["FileSize"]);
				Response.End();

				reader.Close();
				Con.Close();
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
