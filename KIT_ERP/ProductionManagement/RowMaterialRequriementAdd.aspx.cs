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

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// RowMaterialRequriementAdd에 대한 요약 설명입니다.
	/// </summary>
	public class RowMaterialRequriementAdd : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgBR_HT;
		protected System.Web.UI.WebControls.DropDownList ddlRequestPost;
		protected System.Web.UI.WebControls.Button btnClear;
		protected System.Web.UI.WebControls.Button btnUpDate;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.DropDownList ddlBuyingRequestSource;
		protected System.Web.UI.WebControls.Label searchTitle;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFirstDeliveryDemandDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcSecondDeliveryDemandDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcThirdDeliveryDemandDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFourthDeliveryDemandDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFifthDeliveryDemandDate;
		protected System.Web.UI.HtmlControls.HtmlInputText txtFirstDeliveryDemandQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputText txtSecondDeliveryDemandQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputText txtThirdDeliveryDemandQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputText txtFourthDeliveryDemandQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputText txtFifthDeliveryDemandQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputText txtOrderQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputText txtTotalCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden applyUnitCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtItemDrawNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtHistoryIndex;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		private ArrayList rowMaterialRequriementAdd;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.RawMaterials=true;
			ItemSearchControl1.IsDoPost = true;

			if(!Page.IsPostBack)
			{
				string strSQL = "";

				// MainTitle Frame에 해당 페이지 Title출력
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 생산관리 > 원자재 구매의뢰 추가';</script>");

				// 추가버튼에 대하여 onclick 속성 추가
				//btnAdd.Attributes.Add("onclick", "return Add('선택한 품목을 추가하시겠습니까?');");
				// 삭제버튼에 대하여 onclick 속성 추가
				//btnDelete.Attributes.Add("onclick", "return Confirm('선택한 품목을 삭제하시겠습니까?');");
				// 수정버튼에 대하여 onclick 속성 추가
				btnUpDate.Attributes.Add("onclick", "return Confirm('선택한 품목을 수정하시겠습니까?');");
				
				// 품목명 선택을 위한 WebComBo버튼에 해당(제품, 상품) Data를 바인딩
//				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
//				SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionString").ToString());
//				conn.Open();
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

//				string strSQL = @"SELECT ItemNum, ItemDrawNum, ItemName, StandardUnitCost
//									FROM II_MT WHERE PropertyClassification = '원자재' AND RecodingState = '1'";
//
//				SqlCommand comm = new SqlCommand(strSQL, conn);
//				
//				SqlDataReader reader = comm.ExecuteReader();
//
//				wcbItemName.DataSource = reader;
//				wcbItemName.DataTextField = "ItemName";
//				wcbItemName.DataValueField = "ItemNum";
//				wcbItemName.DataBind();
//				reader.Close();

				// 구매의뢰부서선택을 위한 DropDownList에 해당 Data를 바인딩
				strSQL = @"SELECT SmallClassificationCode, SmallClassificationName FROM PUC_MT 
								  WHERE LargeClassificationCode LIKE '0800%' AND RecodingState = '1'";
				SqlCommand comm = new SqlCommand(strSQL, conn);
				SqlDataReader reader = comm.ExecuteReader();
				ddlRequestPost.DataSource = reader;
				ddlRequestPost.DataTextField = "SmallClassificationName";
				ddlRequestPost.DataValueField = "SmallClassificationCode";
				ddlRequestPost.DataBind();
				
				reader.Close();


				// 구매의뢰원천선택을 위한 DropDownList에 해당 Data를 바인딩
				strSQL = @"SELECT SmallClassificationCode, SmallClassificationName FROM PUC_MT 
							  WHERE LargeClassificationCode LIKE '0330%' AND RecodingState = '1' AND SmallClassificationName != '정상'";
				comm = new SqlCommand(strSQL, conn);
				reader = comm.ExecuteReader();
				ddlBuyingRequestSource.DataSource = reader;
				ddlBuyingRequestSource.DataTextField = "SmallClassificationName";
				ddlBuyingRequestSource.DataValueField = "SmallClassificationCode";
				ddlBuyingRequestSource.DataBind();
				
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
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			this.btnUpDate.Click += new System.EventHandler(this.btnUpDate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		// TODO : 초기화버튼 클릭시
		private void btnClear_Click(object sender, System.EventArgs e)
		{
			//wcbItemName.DataValue = "";
			ItemSearchControl1.ClearTextBox();
			ddlBuyingRequestSource.SelectedIndex = 0;
			ddlRequestPost.SelectedIndex = 0;
			txtFirstDeliveryDemandQuantity.Value = null;
			txtSecondDeliveryDemandQuantity.Value = null;
			txtThirdDeliveryDemandQuantity.Value = null;
			txtFourthDeliveryDemandQuantity.Value = null;
			txtFifthDeliveryDemandQuantity.Value = null;
			txtOrderQuantity.Value = null;
			txtTotalCost.Value = null;
			wdcFirstDeliveryDemandDate.Value = null;
			wdcSecondDeliveryDemandDate.Value = null;
			wdcThirdDeliveryDemandDate.Value = null;
			wdcFourthDeliveryDemandDate.Value = null;
			wdcFifthDeliveryDemandDate.Value = null;
			applyUnitCost.Value = null;
		}


		// TODO : 추가버튼 클릭시
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			InputData();

			KIT_ERP.Registration registrBR_HT = new KIT_ERP.Registration("RowMaterialRequriementAdd", Session["ID"].ToString(), "", rowMaterialRequriementAdd);

			uwgBR_HT.DataSource = registrBR_HT.MainTableRegistration();
			uwgBR_HT.DataBind();
		}

		// TODO : 현재 각 컨트롤의 값을 ArrayList의 저장
		private void InputData()
		{
			rowMaterialRequriementAdd = new ArrayList();
			
//			//rowMaterialRequriementAdd.Add(wcbItemName.DataValue);
//			rowMaterialRequriementAdd.Add(ItemSearchControl1.ItemName);
//			
//			rowMaterialRequriementAdd.Add(txtItemDrawNum.Value);
//			
//			//rowMaterialRequriementAdd.Add(wcbItemName.DisplayValue);


			rowMaterialRequriementAdd.Add(ItemSearchControl1.ItemNum);
			rowMaterialRequriementAdd.Add(ItemSearchControl1.ItemDrawNum);
			rowMaterialRequriementAdd.Add(ItemSearchControl1.ItemName);
			
			
			rowMaterialRequriementAdd.Add("원자재");
			rowMaterialRequriementAdd.Add(ddlBuyingRequestSource.SelectedItem.Value);
			rowMaterialRequriementAdd.Add(ddlBuyingRequestSource.SelectedItem.Text);
			rowMaterialRequriementAdd.Add(txtFirstDeliveryDemandQuantity.Value);
			rowMaterialRequriementAdd.Add(wdcFirstDeliveryDemandDate.Text);
			rowMaterialRequriementAdd.Add(txtSecondDeliveryDemandQuantity.Value);
			rowMaterialRequriementAdd.Add(wdcSecondDeliveryDemandDate.Text);
			rowMaterialRequriementAdd.Add(txtThirdDeliveryDemandQuantity.Value);
			rowMaterialRequriementAdd.Add(wdcThirdDeliveryDemandDate.Text);
			rowMaterialRequriementAdd.Add(txtFourthDeliveryDemandQuantity.Value);
			rowMaterialRequriementAdd.Add(wdcFourthDeliveryDemandDate.Text);
			rowMaterialRequriementAdd.Add(txtFifthDeliveryDemandQuantity.Value);
			rowMaterialRequriementAdd.Add(wdcFifthDeliveryDemandDate.Text);
			rowMaterialRequriementAdd.Add(txtOrderQuantity.Value);
			rowMaterialRequriementAdd.Add(applyUnitCost.Value);
			rowMaterialRequriementAdd.Add(txtTotalCost.Value);
			rowMaterialRequriementAdd.Add(ddlRequestPost.SelectedItem.Value);
			rowMaterialRequriementAdd.Add(ddlRequestPost.SelectedItem.Text);
		}

		// TODO : 수정버튼 클릭시
		private void btnUpDate_Click(object sender, System.EventArgs e)
		{
			InputData();

			KIT_ERP.Registration registrBR_HT = new KIT_ERP.Registration("RowMaterialRequriementAdd", Session["ID"].ToString(), txtHistoryIndex.Value, rowMaterialRequriementAdd);

			uwgBR_HT.DataSource = registrBR_HT.MainTableUpdate();
			uwgBR_HT.DataBind();
		}

		// TODO : 삭제버튼 클릭시
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			KIT_ERP.Registration registrBR_HT = new KIT_ERP.Registration("RowMaterialRequriementAdd", txtHistoryIndex.Value);
			
			uwgBR_HT.DataSource = registrBR_HT.MainTableDelete();
			uwgBR_HT.DataBind();
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			applyUnitCost.Value =  Convert.ToString( GetSTDUnitCost() );
		}

		private decimal GetSTDUnitCost()
		{
			string strItemNum = ItemSearchControl1.ItemNum;
			string strItemDrawNum = ItemSearchControl1.ItemDrawNum;
			string strItemName = ItemSearchControl1.ItemName;
			decimal stdUnitCost = 0.00m;  

			string strSQL = @"select StandardUnitCost from ii_mt 
								where 
								(
									case when @ItemNum <> ''  then ItemNum else '' end
									=
									case when @ItemNum <> ''  then @ItemNum else '' end
								)
								and
								(
									case when @ItemDrawNum <> ''  then ItemDrawNum else '' end
									=
									case when @ItemDrawNum <> ''  then @ItemDrawNum else '' end
								)
								and
								(
									case when @ItemName <> ''  then ItemName else '' end
									=
									case when @ItemName <> ''  then @ItemName else '' end
								)
								and recodingstate = 1" ;
			
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand cmd = new SqlCommand(strSQL, con);

			cmd.Parameters.Add("@ItemNum", SqlDbType.VarChar, 50).Value = strItemNum;
			cmd.Parameters.Add("@ItemDrawNum", SqlDbType.VarChar, 80).Value = strItemDrawNum;
			cmd.Parameters.Add("@ItemName", SqlDbType.VarChar, 50).Value = strItemName;

			con.Open(); 
			stdUnitCost = Convert.ToDecimal( cmd.ExecuteScalar() );
			con.Close();

			return stdUnitCost;
		}

	}
}
