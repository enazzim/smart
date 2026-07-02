using System;
using System.Configuration;
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
using Infragistics.WebUI;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BusinessManagement.PopUp
{
	/// <summary>
	/// Search에 대한 요약 설명입니다.
	/// </summary>
	public class Search : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			//string str = "Select * From RO_HT Where ItemName like @name and BusinessRegistrationNum like @com and (ReceivingOrderDate >= @begin and ReceivingOrderDate <= @end)";
			string str = "Select * From RO_HT Where ItemName like @name and CompanyName like @com and (ReceivingOrderDate >= @begin and ReceivingOrderDate <= @end)";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = "%"+Request.QueryString["Item"].ToString().Trim() + "%";
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = "%"+Request.QueryString["Com"].ToString().Trim() + "%";
			comm.Parameters.Add("@begin",SqlDbType.VarChar).Value = Request.QueryString["From"].ToString().Trim();
			comm.Parameters.Add("@end",SqlDbType.VarChar).Value = Request.QueryString["To"].ToString().Trim();
			SqlDataReader dr = comm.ExecuteReader();
			UltraWebGrid1.DataSource = dr;
			UltraWebGrid1.DataBind();

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
