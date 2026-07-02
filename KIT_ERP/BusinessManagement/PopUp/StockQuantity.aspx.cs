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

namespace KIT_ERP.BusinessManagement.PopUp
{
	/// <summary>
	/// StockQuantity에 대한 요약 설명입니다.
	/// </summary>
	public class StockQuantity : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		private string ItemNum = "";

	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemNum = Session["ItemNum"].ToString();
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			if(!Page.IsPostBack)
			{

				string str = "StockItemBOM";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.CommandType = CommandType.StoredProcedure;
				comm.Parameters.Add("@ItemNum",ItemNum);
				comm.Parameters.Add("@Quantity",1);
				comm.Parameters.Add("@Tree",1);

				DataSet dsItem = new DataSet();
				SqlDataAdapter daItem = new SqlDataAdapter(comm);
				daItem.Fill(dsItem);
				comm.Parameters.Clear();

				UltraWebGrid1.DataSource = dsItem;
				UltraWebGrid1.DataBind();

			}	

			conn.Close();
			Session.Remove("ItemNum");
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
