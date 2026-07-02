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

namespace KIT_ERP.EtcPresentCondition
{
	/// <summary>
	/// ItemState에 대한 요약 설명입니다.
	/// </summary>
	public class ItemState : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList ddlDivision;
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlMon;
		protected System.Web.UI.WebControls.Button btComSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox txtDeliveryQuantity;
		protected System.Web.UI.WebControls.TextBox txtCost;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist1;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		protected System.Web.UI.WebControls.TextBox txtOrderQuantity;
		protected System.Web.UI.WebControls.TextBox txtCashQuantity;
		protected System.Web.UI.WebControls.TextBox txtRemainQuantity;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryDate;
		protected System.Web.UI.WebControls.Button btnRegistration;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdLotNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden IndexNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden HistoryIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden UnitCost;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
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
