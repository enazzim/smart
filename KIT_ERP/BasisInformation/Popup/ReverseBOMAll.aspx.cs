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

namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// ReverseBOMAll에 대한 요약 설명입니다.
	/// </summary>
	public class ReverseBOMAll : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected KIT_ERP.BasisInformation.Popup.ReversalItem.ItemSearch ItemSearch1;
		protected System.Web.UI.WebControls.Button Button1;

		private string ChildItemNum;
		private string ChildItemDrawNum;
		private string ChildItemName;

		private Stack st = new Stack(); 
		private Stack st1 = new Stack(); 
		public struct St_Item 
		{
			public string Name;
			public decimal Need;
		}

		private DataTable dt = new DataTable();
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		private DataColumn dc;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearch1.ChildICommodity = false;
			ItemSearch1.ChildHalfFinishedProducts=true;
			ItemSearch1.ChildProducts = true;
			ItemSearch1.ChildRawMaterials = true;
			ItemSearch1.ChildPhantom = false;
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				UltraWebGrid1.Visible= true;				
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
