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

using Microsoft.Win32;
using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.ProductionManagement.PopupWindows
{
	/// <summary>
	/// RowMaterialMessage에 대한 요약 설명입니다.
	/// </summary>
	public class RowMaterialMessage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.CheckBox chkPresentRowMaterialUsed;
		protected System.Web.UI.WebControls.CheckBox chkSafeyRowMaterialUsed;
		protected System.Web.UI.WebControls.CheckBox chkOrderNonInStorehouseUsed;
		protected System.Web.UI.WebControls.CheckBox chkOrderGapInStorehouseUsed;
		protected System.Web.UI.WebControls.CheckBox chkMinimumGapUsed;
		protected System.Web.UI.WebControls.CheckBox chkOrderRequestStandbyUsed;
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Web.UI.WebControls.Button btnCancle;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 버튼에 onclick속성 추가
			btnOK.Attributes.Add("onclick", "return Close('OK');");
			btnCancle.Attributes.Add("onclick", "return Close('Cancle');");
			
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				// 시스템정보의 셋팅(자재소요량 산출관련)값을 가져옴.
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();
				
				string strSQL = @"SELECT PresentRowMaterialUsed, SafeyRowMaterialUsed, OrderNonInStorehouseUsed, 
								  OrderGapInStorehouseUsed, MinimumGapUsed,OrderRequestStandbyUsed FROM SCS_T";

				SqlCommand comm = new SqlCommand(strSQL, conn);

				SqlDataReader reader = comm.ExecuteReader();

				while(reader.Read())
				{
					// 현재고량
					if(bool.Parse(reader["PresentRowMaterialUsed"].ToString()))
						chkPresentRowMaterialUsed.Checked = true;
					else
						chkPresentRowMaterialUsed.Checked = false;

					// 안전재고량
					if(bool.Parse(reader["SafeyRowMaterialUsed"].ToString()))
						chkSafeyRowMaterialUsed.Checked = true;
					else
						chkSafeyRowMaterialUsed.Checked = false;

					// 발주미입고량
					if(bool.Parse(reader["OrderNonInStorehouseUsed"].ToString()))
						chkOrderNonInStorehouseUsed.Checked = true;
					else
						chkOrderNonInStorehouseUsed.Checked = false;

					// 발주간격수량
					if(bool.Parse(reader["OrderGapInStorehouseUsed"].ToString()))
						chkOrderGapInStorehouseUsed.Checked = true;
					else
						chkOrderGapInStorehouseUsed.Checked = false;

					// 최소발주량
					if(bool.Parse(reader["MinimumGapUsed"].ToString()))
						chkMinimumGapUsed.Checked = true;
					else
						chkMinimumGapUsed.Checked = false;

					// 발주의뢰대기량
					if(bool.Parse(reader["OrderRequestStandbyUsed"].ToString()))
						chkOrderRequestStandbyUsed.Checked = true;
					else
						chkOrderRequestStandbyUsed.Checked = false;
				}
				reader.Close();
				conn.Close();
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
