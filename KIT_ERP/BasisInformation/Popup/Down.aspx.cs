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

namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// Down에 대한 요약 설명입니다.
	/// </summary>
	public class Down : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
//			string index;
//			if(Request.QueryString["table"].ToString() == "T_Product")
//				index = "CCompanyStandardIndex";
//			else if(Request.QueryString["table"].ToString() == "T_Development")
//				index = "CGeneralDataIndex";
//			else
//				index = "CTechnologyDataIndex";

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand();
				Cmd.Connection = Con;
				Con.Open();
				
				Cmd.CommandText = "SELECT * FROM "+Request.QueryString["table"]+ " WHERE seq = @Index";
				Cmd.Parameters.Add("@Index", SqlDbType.Int);
				Cmd.Parameters["@Index"].Value = Request.QueryString["index"];
				
				SqlDataReader reader = Cmd.ExecuteReader();
				reader.Read();

				Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(reader["FileName"].ToString()));
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
