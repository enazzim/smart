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

namespace KIT_ERP.ManagementInfomation
{
	/// <summary>
	/// WebForm1에 대한 요약 설명입니다.
	/// </summary>
	public class WebForm1 : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid3;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid4;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.Button btnSearch;
	
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
			this.DropDownList1.SelectedIndexChanged += new System.EventHandler(this.DropDownList1_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void DropDownList1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(DropDownList1.SelectedItem.Value == "원자재입고대장")
			{
				UltraWebGrid1.Visible = true;
				UltraWebGrid2.Visible = false;
				UltraWebGrid3.Visible = false;
				UltraWebGrid4.Visible = false;
			}
			else if(DropDownList1.SelectedItem.Value == "제품출고대장")
			{
				UltraWebGrid1.Visible = false;
				UltraWebGrid2.Visible = true;
				UltraWebGrid3.Visible = false;
				UltraWebGrid4.Visible = false;
			}
			else
			{
				UltraWebGrid1.Visible = false;
				UltraWebGrid2.Visible = false;
				UltraWebGrid3.Visible = true;
				UltraWebGrid4.Visible = true;
			}

		}
	}
}
