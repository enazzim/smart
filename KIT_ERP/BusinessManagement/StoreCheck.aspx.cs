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
namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// StoreCheck에 대한 요약 설명입니다.
	/// </summary>
	public class StoreCheck : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Request.QueryString["param"]==null)
				return;
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.CommandText = "Select StockQuantity"+DateTime.Now.Month+ " as Quantity From BS_MT where BusinessStorehouseNum = 1 and RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
			comm.Parameters.Add("@id",Request.QueryString["param"]);
			comm.Parameters.Add("@year",DateTime.Now.Year);
			decimal Quantity  = 0;
			con.Open();
			if(int.Parse(comm.ExecuteScalar().ToString()) != 0)
			{
				Quantity = decimal.Parse(comm.ExecuteScalar().ToString());
				Response.Write(Quantity.ToString());
			}
			con.Close();
			Response.Flush();

			Response.End();
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
