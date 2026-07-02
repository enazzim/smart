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
	/// ReceiptDisburse에 대한 요약 설명입니다.
	/// </summary>
	public class ReceiptDisburse : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Search;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				string [] hTexts = new string[]{"출고수량","단가","일자","업체명"}; // Sub-Column명이 들어간다. 합칠 Column의 갯수에 따라 이 부분도 수정되어야 한다.
				//string [] hTexts = new string[]{"출고수량","단가"}; // Sub-Column명이 들어간다. 합칠 Column의 갯수에 따라 이 부분도 수정되어야 한다.
				int len = hTexts.Length;
   
				Infragistics.WebUI.UltraWebGrid.UltraWebGrid grid = UltraWebGrid1;

				string thStyle = "font-family:Verdana;font-size:8pt;font-weight:normal;border-color:Gray; border-style:Solid; border-left-width:0px; border-right-width:0px; border-bottom-width:0px; border-top-width:0px; padding-left:0px;";
   
				grid.DisplayLayout.HeaderStyleDefault.Height = Unit.Pixel(40);
				grid.DisplayLayout.HeaderStyleDefault.Margin.Left = grid.DisplayLayout.HeaderStyleDefault.Margin.Right = Unit.Pixel(0);
				grid.DisplayLayout.HeaderStyleDefault.Padding.Left = grid.DisplayLayout.HeaderStyleDefault.Padding.Right = Unit.Pixel(0);
   
				for(int i=0; i<grid.Columns.Count; i+=len)
				{
					string mText = grid.Columns[i].HeaderText;

					if ((int)grid.Columns[i].Width.Value == 0)
						grid.Columns[i].HeaderStyle.Width = Unit.Pixel(100);
					else 
						grid.Columns[i].HeaderStyle.Width = grid.Columns[i].Width;
					int width = (int)grid.Columns[i].HeaderStyle.Width.Value * len;
					int one_width = (int)((double)width / len) ;
					
					string strHeader =
						"<table style='width:" + width + "px;height:100%;border-width:0px;padding-left:0px;' border='0' cellpadding='0' cellspacing='0'><thead><tr>"
						+ "<th style='" + thStyle + " width:100%;border-bottom-width:1px;' colspan='" + len + "'><b>" + mText + "</b></th></tr><tr>"
						+ "<th style='" + thStyle + " width:" + (one_width-1) + "px;border-right-width:0px;'>" + hTexts[0]+ "</th>";
					for(int j=1; j<len; j++)
					{
						strHeader += "<th style='" + thStyle + "width:" + one_width + "px;border-left-width:1px;'>" + hTexts[j] + "</th>";
					}
					strHeader += "</tr></thead></table>";
					grid.Columns[i].HeaderText = strHeader;               
				}
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
