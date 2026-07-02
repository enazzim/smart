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
using System.IO;
using System.Data.OleDb;
using Microsoft.Win32;

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// ItemInfo에 대한 요약 설명입니다.
	/// </summary>
	public class ItemInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.Label Label34;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.Label Label36;
		protected System.Web.UI.WebControls.Label Label37;
		protected System.Web.UI.WebControls.Label Label38;
		protected System.Web.UI.WebControls.Label Label39;
		protected System.Web.UI.WebControls.Label Label40;
		protected System.Web.UI.WebControls.Label Label41;
		protected System.Web.UI.WebControls.Label Label43;
		protected System.Web.UI.WebControls.Label Label42;
		protected System.Web.UI.WebControls.Label Label44;
		protected System.Web.UI.WebControls.Label Label45;
		protected System.Web.UI.WebControls.Label Label46;
		protected System.Web.UI.WebControls.Label Label47;
		protected System.Web.UI.WebControls.Label Label48;
		protected System.Web.UI.WebControls.Label Label49;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Reference;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Label Label51;
		protected System.Web.UI.WebControls.TextBox tb_Standard;
		protected System.Web.UI.WebControls.DropDownList dl_Texture;
		protected System.Web.UI.WebControls.DropDownList dl_StockUnit;
		protected System.Web.UI.WebControls.DropDownList dl_BOMUnit;
		protected System.Web.UI.WebControls.DropDownList dl_PurchaseUnit;
		protected System.Web.UI.WebControls.DropDownList dl_SaleUnit;
		protected System.Web.UI.WebControls.DropDownList dl_IOChackable;
		protected System.Web.UI.WebControls.DropDownList dl_StockManagable;
		protected System.Web.UI.WebControls.DropDownList dl_CheckDistinction;
		protected System.Web.UI.WebControls.DropDownList dl_OrderPlan;
		protected System.Web.UI.WebControls.DropDownList dl_ItemType;
		protected System.Web.UI.WebControls.DropDownList dl_ItemState;
		protected System.Web.UI.WebControls.TextBox tb_Maker;
		protected System.Web.UI.WebControls.DropDownList dl_ItemClassification1;
		protected System.Web.UI.WebControls.DropDownList dl_ItemClassification2;
		protected System.Web.UI.WebControls.DropDownList dl_ItemClassification3;
		protected System.Web.UI.WebControls.DropDownList dl_ItemClassification4;
		protected System.Web.UI.WebControls.TextBox tb_Standard1;
		protected System.Web.UI.WebControls.TextBox tb_Standard2;
		protected System.Web.UI.WebControls.DropDownList dl_Unit1;
		protected System.Web.UI.WebControls.DropDownList dl_Unit2;
		protected System.Web.UI.WebControls.TextBox tb_Standard3;
		protected System.Web.UI.WebControls.DropDownList dl_Unit3;
		protected System.Web.UI.WebControls.TextBox tb_Standard4;
		protected System.Web.UI.WebControls.DropDownList dl_Unit4;
		protected System.Web.UI.WebControls.TextBox tb_ProductWeight;
		protected System.Web.UI.WebControls.TextBox tb_MaterialWeight;
		protected System.Web.UI.WebControls.TextBox tb_SupplyTerm;
		protected System.Web.UI.WebControls.TextBox tb_StiffenDeep;
		protected System.Web.UI.WebControls.TextBox tb_ProductHeatTreatmentDescription;
		protected System.Web.UI.WebControls.TextBox tb_MaterialHeatTreatmentDescription;
		protected System.Web.UI.WebControls.TextBox tb_MaterialHeatTreatmentRequestDegree;
		protected System.Web.UI.WebControls.TextBox tb_CuttingSpace;
		protected System.Web.UI.WebControls.TextBox tb_MainPurchaseCompany;
		protected System.Web.UI.WebControls.TextBox tb_MainOutSideOrderCompany;
		protected System.Web.UI.WebControls.TextBox tb_MainSaleCompany;
		protected System.Web.UI.WebControls.DropDownList dl_ChargePerson;
		protected System.Web.UI.WebControls.DropDownList dl_PropertyClassification;
		protected System.Web.UI.WebControls.DropDownList dl_Unit;
		protected System.Web.UI.WebControls.Label lb_Index;
		protected System.Web.UI.WebControls.Label lb_ItemChoice;
		protected System.Web.UI.WebControls.Label lb_ItemIndex;
		protected System.Web.UI.WebControls.DropDownList dl_MateralQuality;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.DropDownList dl_DomesticImportDistinction;
		protected System.Web.UI.WebControls.Label Label50;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Property;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ItemIndex;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.WebControls.TextBox tb_SupplementaryValueTaxRate;
		protected System.Web.UI.WebControls.TextBox tb_SafetyStockQuantity;
		protected System.Web.UI.WebControls.TextBox tb_OrderIntervalQuantity;
		protected System.Web.UI.WebControls.TextBox tb_MinOrderQuantity;
		protected System.Web.UI.WebControls.TextBox tb_TariffRate;
		protected System.Web.UI.WebControls.TextBox tb_StandardUnitCost;
		protected System.Web.UI.WebControls.Button bt_Registration;
		//protected object aaa;
		protected System.Web.UI.WebControls.Button tbTotalUpdate;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		private string m_UserName = "";
		protected System.Web.UI.WebControls.TextBox txtLeadTime;
		private DataSet dataset = new DataSet();


	
		private void Page_Load(object sender, System.EventArgs e)
		{
//			if(Session["ID"] == null)
//			{	
//				Session.Abandon();
//				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
//			}

			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.RawMaterials = true; //원자재 바인딩	
			ItemSearchControl1.Phantom = true; //팬텀 바인딩
			ItemSearchControl1.SubMaterials = true;
			ItemSearchControl1.HalfFinishedProducts =  true; //반제품 바인딩	
			ItemSearchControl1.Products = true; //제품 바인딩	

			ItemSearchControl1.IsDoPost = true;
			
			if(!Page.IsPostBack)
			{
				////Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 품목정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");


				tb_SupplementaryValueTaxRate.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_SupplementaryValueTaxRate.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				
				tb_SafetyStockQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_SafetyStockQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_OrderIntervalQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_OrderIntervalQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_MinOrderQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_MinOrderQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_TariffRate.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_TariffRate.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				tb_StandardUnitCost.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_StandardUnitCost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

				bt_Clear.Attributes.Add("onClick", "ResetTextBox()");
				
				page_Load();
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
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.bt_Reference.Click += new System.EventHandler(this.bt_Reference_Click);
			this.tbTotalUpdate.Click += new System.EventHandler(this.tbTotalUpdate_Click);
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.UltraWebGrid1.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.UltraWebGrid1_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void page_Load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			
			//	*************************************************
			//	**  단위 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_Unit = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0400'" ;
			SqlCommand comm_Unit = new SqlCommand(str_Unit,conn);
			SqlDataAdapter da_Unit = new SqlDataAdapter(comm_Unit) ;
			DataSet ds_Unit = new DataSet() ;
			da_Unit.Fill(ds_Unit);
				
			dl_Unit.DataSource = ds_Unit;
			dl_Unit.DataTextField = ds_Unit.Tables[0].Columns[0].ToString();
			dl_Unit.DataValueField = ds_Unit.Tables[0].Columns[1].ToString();
			dl_Unit.DataBind();
			dl_Unit.Items.Insert(0, "-선택-") ;
			dl_Unit.Items[0].Value = "";

			//	*************************************************
			//	**  단위1 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_Unit1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0410'" ;
			SqlCommand comm_Unit1 = new SqlCommand(str_Unit1,conn);
			SqlDataAdapter da_Unit1 = new SqlDataAdapter(comm_Unit1) ;
			DataSet ds_Unit1 = new DataSet() ;
			da_Unit1.Fill(ds_Unit1);
				
			dl_Unit1.DataSource = ds_Unit1;
			dl_Unit1.DataTextField = ds_Unit1.Tables[0].Columns[0].ToString();
			dl_Unit1.DataValueField = ds_Unit1.Tables[0].Columns[1].ToString();
			dl_Unit1.DataBind();
			dl_Unit1.Items.Insert(0, "-선택-") ;
			dl_Unit1.Items[0].Value = "";

			//	*************************************************
			//	**  단위2 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_Unit2 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0420'" ;
			SqlCommand comm_Unit2 = new SqlCommand(str_Unit2,conn);
			SqlDataAdapter da_Unit2 = new SqlDataAdapter(comm_Unit2) ;
			DataSet ds_Unit2 = new DataSet() ;
			da_Unit2.Fill(ds_Unit2);
				
			dl_Unit2.DataSource = ds_Unit2;
			dl_Unit2.DataTextField = ds_Unit2.Tables[0].Columns[0].ToString();
			dl_Unit2.DataValueField = ds_Unit2.Tables[0].Columns[1].ToString();
			dl_Unit2.DataBind();
			dl_Unit2.Items.Insert(0, "-선택-") ;
			dl_Unit2.Items[0].Value = "";

			//	*************************************************
			//	**  단위3 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_Unit3 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0430'" ;
			SqlCommand comm_Unit3 = new SqlCommand(str_Unit3,conn);
			SqlDataAdapter da_Unit3 = new SqlDataAdapter(comm_Unit3) ;
			DataSet ds_Unit3 = new DataSet() ;
			da_Unit3.Fill(ds_Unit3);
				
			dl_Unit3.DataSource = ds_Unit3;
			dl_Unit3.DataTextField = ds_Unit3.Tables[0].Columns[0].ToString();
			dl_Unit3.DataValueField = ds_Unit3.Tables[0].Columns[1].ToString();
			dl_Unit3.DataBind();
			dl_Unit3.Items.Insert(0, "-선택-") ;
			dl_Unit3.Items[0].Value = "";

			//	*************************************************
			//	**  단위4 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
//			string str_Unit4 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0440'" ;
//			SqlCommand comm_Unit4 = new SqlCommand(str_Unit4,conn);
//			SqlDataAdapter da_Unit4 = new SqlDataAdapter(comm_Unit4) ;
//			DataSet ds_Unit4 = new DataSet() ;
//			da_Unit4.Fill(ds_Unit4);
//				
//			dl_Unit4.DataSource = ds_Unit4;
//			dl_Unit4.DataTextField = ds_Unit4.Tables[0].Columns[0].ToString();
//			dl_Unit4.DataValueField = ds_Unit4.Tables[0].Columns[1].ToString();
//			dl_Unit4.DataBind();
//			dl_Unit4.Items.Insert(0, "-선택-") ;
//			dl_Unit4.Items[0].Value = "";

			//	*************************************************
			//	**  재고단위 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_StockUnit = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0510'" ;
			SqlCommand comm_StockUnit = new SqlCommand(str_StockUnit,conn);
			SqlDataAdapter da_StockUnit = new SqlDataAdapter(comm_StockUnit) ;
			DataSet ds_StockUnit = new DataSet() ;
			da_StockUnit.Fill(ds_StockUnit);
				
			dl_StockUnit.DataSource = ds_StockUnit;
			dl_StockUnit.DataTextField = ds_StockUnit.Tables[0].Columns[0].ToString();
			dl_StockUnit.DataValueField = ds_StockUnit.Tables[0].Columns[1].ToString();
			dl_StockUnit.DataBind();
			dl_StockUnit.Items.Insert(0, "-선택-") ;
			dl_StockUnit.Items[0].Value = "";


			//	*************************************************
			//	**  BOM단위 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_BOMUnit = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0520'" ;
			SqlCommand comm_BOMUnit = new SqlCommand(str_BOMUnit,conn);
			SqlDataAdapter da_BOMUnit = new SqlDataAdapter(comm_BOMUnit) ;
			DataSet ds_BOMUnit = new DataSet() ;
			da_BOMUnit.Fill(ds_BOMUnit);
				
			dl_BOMUnit.DataSource = ds_BOMUnit;
			dl_BOMUnit.DataTextField = ds_BOMUnit.Tables[0].Columns[0].ToString();
			dl_BOMUnit.DataValueField = ds_BOMUnit.Tables[0].Columns[1].ToString();
			dl_BOMUnit.DataBind();
			dl_BOMUnit.Items.Insert(0, "-선택-") ;
			dl_BOMUnit.Items[0].Value = "";


			//	*************************************************
			//	**  구매단위 드롭다운리스트... 데이타바인딩... **			
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_PurchaseUnit = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0530'" ;
			SqlCommand comm_PurchaseUnit = new SqlCommand(str_PurchaseUnit,conn);
			SqlDataAdapter da_PurchaseUnit = new SqlDataAdapter(comm_PurchaseUnit) ;
			DataSet ds_PurchaseUnit = new DataSet() ;
			da_PurchaseUnit.Fill(ds_PurchaseUnit);
				
			dl_PurchaseUnit.DataSource = ds_PurchaseUnit;
			dl_PurchaseUnit.DataTextField = ds_PurchaseUnit.Tables[0].Columns[0].ToString();
			dl_PurchaseUnit.DataValueField = ds_PurchaseUnit.Tables[0].Columns[1].ToString();
			dl_PurchaseUnit.DataBind();
			dl_PurchaseUnit.Items.Insert(0, "-선택-") ;
			dl_PurchaseUnit.Items[0].Value = "";
			




			//	*************************************************
			//	**  판매단위 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_SaleUnit = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0540'" ;
			SqlCommand comm_SaleUnit = new SqlCommand(str_SaleUnit,conn);
			SqlDataAdapter da_SaleUnit = new SqlDataAdapter(comm_SaleUnit) ;
			DataSet ds_SaleUnit = new DataSet() ;
			da_SaleUnit.Fill(ds_SaleUnit);
				
			dl_SaleUnit.DataSource = ds_SaleUnit;
			dl_SaleUnit.DataTextField = ds_SaleUnit.Tables[0].Columns[0].ToString();
			dl_SaleUnit.DataValueField = ds_SaleUnit.Tables[0].Columns[1].ToString();
			dl_SaleUnit.DataBind();
			dl_SaleUnit.Items.Insert(0, "-선택-") ;
			dl_SaleUnit.Items[0].Value = "";



			//	*************************************************
			//	**  담당자 드롭다운리스트... 데이타바인딩... **		dl_ChargePerson
			//	*************************************************
			//코드분류표에서 대분류명이 단위인 소분류명을 가지고 옴.
			string str_Man = "Select Name, ID  from UI_MT where RecodingState = 1" ;
			SqlCommand comm_Man = new SqlCommand(str_Man,conn);
			SqlDataAdapter da_Man = new SqlDataAdapter(comm_Man) ;
			DataSet ds_Man = new DataSet() ;
			da_Man.Fill(ds_Man);
				
			dl_ChargePerson.DataSource = ds_Man;
			dl_ChargePerson.DataTextField = ds_Man.Tables[0].Columns[0].ToString();
			dl_ChargePerson.DataValueField = ds_Man.Tables[0].Columns[1].ToString();
			dl_ChargePerson.DataBind();
			dl_ChargePerson.Items.Insert(0, "-선택-") ;
			dl_ChargePerson.Items[0].Value = "";



			//	*************************************************
			//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
			string str_ItemClassification1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0210'" ;
			SqlCommand comm_ItemClassification1 = new SqlCommand(str_ItemClassification1,conn);
			SqlDataAdapter da_ItemClassification1 = new SqlDataAdapter(comm_ItemClassification1) ;
			DataSet ds_ItemClassification1 = new DataSet() ;
			da_ItemClassification1.Fill(ds_ItemClassification1);
				
			dl_ItemClassification1.DataSource = ds_ItemClassification1;
			dl_ItemClassification1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
			dl_ItemClassification1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
			dl_ItemClassification1.DataBind();
			dl_ItemClassification1.Items.Insert(0, "-선택-") ;
			dl_ItemClassification1.Items[0].Value = "";



			


			//	*************************************************
			//	**  품목분류2 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류2인 소분류명을 가지고 옴.
			string str_ItemClassification2 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0220'" ;
			SqlCommand comm_ItemClassification2 = new SqlCommand(str_ItemClassification2,conn);
			SqlDataAdapter da_ItemClassification2 = new SqlDataAdapter(comm_ItemClassification2) ;
			DataSet ds_ItemClassification2 = new DataSet() ;
			da_ItemClassification2.Fill(ds_ItemClassification2);
				
			dl_ItemClassification2.DataSource = ds_ItemClassification2;
			dl_ItemClassification2.DataTextField = ds_ItemClassification2.Tables[0].Columns[0].ToString();
			dl_ItemClassification2.DataValueField = ds_ItemClassification2.Tables[0].Columns[1].ToString();
			dl_ItemClassification2.DataBind();
			dl_ItemClassification2.Items.Insert(0, "-선택-") ;
			dl_ItemClassification2.Items[0].Value = "";



			//	*************************************************
			//	**  품목분류3 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류3인 소분류명을 가지고 옴.
			string str_ItemClassification3 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0230'" ;
			SqlCommand comm_ItemClassification3 = new SqlCommand(str_ItemClassification3,conn);
			SqlDataAdapter da_ItemClassification3 = new SqlDataAdapter(comm_ItemClassification3) ;
			DataSet ds_ItemClassification3 = new DataSet() ;
			da_ItemClassification3.Fill(ds_ItemClassification3);
				
			dl_ItemClassification3.DataSource = ds_ItemClassification3;
			dl_ItemClassification3.DataTextField = ds_ItemClassification3.Tables[0].Columns[0].ToString();
			dl_ItemClassification3.DataValueField = ds_ItemClassification3.Tables[0].Columns[1].ToString();
			dl_ItemClassification3.DataBind();
			dl_ItemClassification3.Items.Insert(0, "-선택-") ;
			dl_ItemClassification3.Items[0].Value = "";

			


			//	*************************************************
			//	**  품목분류4 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
			string str_ItemClassification4 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0240'" ;
			SqlCommand comm_ItemClassification4 = new SqlCommand(str_ItemClassification4,conn);
			SqlDataAdapter da_ItemClassification4 = new SqlDataAdapter(comm_ItemClassification4) ;
			DataSet ds_ItemClassification4 = new DataSet() ;
			da_ItemClassification4.Fill(ds_ItemClassification4);
				
			dl_ItemClassification4.DataSource = ds_ItemClassification4;
			dl_ItemClassification4.DataTextField = ds_ItemClassification4.Tables[0].Columns[0].ToString();
			dl_ItemClassification4.DataValueField = ds_ItemClassification4.Tables[0].Columns[1].ToString();
			dl_ItemClassification4.DataBind();
			dl_ItemClassification4.Items.Insert(0, "-선택-") ;
			dl_ItemClassification4.Items[0].Value = "";


			//	*************************************************
			//	**  품목타입 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
			string str_ItemType = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0610'" ;
			SqlCommand comm_ItemType = new SqlCommand(str_ItemType,conn);
			SqlDataAdapter da_ItemType = new SqlDataAdapter(comm_ItemType) ;
			DataSet ds_ItemType = new DataSet() ;
			da_ItemType.Fill(ds_ItemType);
				
			dl_ItemType.DataSource = ds_ItemType;
			dl_ItemType.DataTextField = ds_ItemType.Tables[0].Columns[0].ToString();
			dl_ItemType.DataValueField = ds_ItemType.Tables[0].Columns[1].ToString();
			dl_ItemType.DataBind();
			dl_ItemType.Items.Insert(0, "-선택-") ;
			dl_ItemType.Items[0].Value = "";


			//	*************************************************
			//	**  품목재질 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목재질인 소분류명을 가지고 옴.
			string str_MateralQuality = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0620'" ;
			SqlCommand comm_MateralQuality = new SqlCommand(str_MateralQuality,conn);
			SqlDataAdapter da_MateralQuality = new SqlDataAdapter(comm_MateralQuality) ;
			DataSet ds_MateralQuality = new DataSet() ;
			da_MateralQuality.Fill(ds_MateralQuality);
				
			dl_MateralQuality.DataSource = ds_MateralQuality;
			dl_MateralQuality.DataTextField = ds_MateralQuality.Tables[0].Columns[0].ToString();
			dl_MateralQuality.DataValueField = ds_MateralQuality.Tables[0].Columns[1].ToString();
			dl_MateralQuality.DataBind();
			dl_MateralQuality.Items.Insert(0, "-선택-") ;
			dl_MateralQuality.Items[0].Value = "";

			//	*************************************************
			//	**  품목상태 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목상태인 소분류명을 가지고 옴.
			string str_ItemState = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0630'" ;
			SqlCommand comm_ItemState = new SqlCommand(str_ItemState,conn);
			SqlDataAdapter da_ItemState = new SqlDataAdapter(comm_ItemState) ;
			DataSet ds_ItemState = new DataSet() ;
			da_ItemState.Fill(ds_ItemState);
				
			dl_ItemState.DataSource = ds_ItemState;
			dl_ItemState.DataTextField = ds_ItemState.Tables[0].Columns[0].ToString();
			dl_ItemState.DataValueField = ds_ItemState.Tables[0].Columns[1].ToString();
			dl_ItemState.DataBind();
			dl_ItemState.Items.Insert(0, "-선택-") ;
			dl_ItemState.Items[0].Value = "";


			conn.Close();
		}


		/// <summary>
		/// 등록버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
		
			string m_RequirePage = "ItemInfo";
			string m_RequireAction = "Registration";
			string m_TableName = "II_MT";
			int m_Idx = (int)PageName.ItemInfo;
			string m_User = Session["ID"].ToString();
			
			ArrayList InputList = new ArrayList();



			if(dl_ItemState.SelectedIndex == 0)
				RegisterStartupScript("","<script>alert('품목상태를 선택해 주세요');</script>");
			else if(dl_ItemClassification1.SelectedIndex == 0)
				RegisterStartupScript("","<script>alert('품목분류1을 선택해 주세요');</script>");
			else if(dl_ItemClassification2.SelectedIndex == 0)
				RegisterStartupScript("","<script>alert('품목분류2를 선택해 주세요');</script>");
			else if(dl_ItemClassification3.SelectedIndex == 0)
				RegisterStartupScript("","<script>alert('품목분류3를 선택해 주세요');</script>");
			else
			{



				InputList.Add(ItemSearchControl1.ItemNum.ToString().Trim());
				InputList.Add(ItemSearchControl1.ItemDrawNum.ToString().Trim());
				InputList.Add(ItemSearchControl1.ItemName.ToString().Trim());
				InputList.Add(dl_PropertyClassification.SelectedItem.Value);
				InputList.Add(dl_Unit.SelectedItem.Value);
				InputList.Add(tb_Standard.Text);
				InputList.Add(dl_Texture.SelectedItem.Value);
				InputList.Add(float.Parse(tb_SupplementaryValueTaxRate.Text));
				InputList.Add(dl_StockUnit.SelectedItem.Value);
				InputList.Add(dl_BOMUnit.SelectedItem.Value);
				InputList.Add(dl_PurchaseUnit.SelectedItem.Value);
				InputList.Add(dl_SaleUnit.SelectedItem.Value);
				if(dl_PropertyClassification.SelectedItem.Value.Trim() == "부자재")
					InputList.Add(0);
				else
					InputList.Add(int.Parse(dl_IOChackable.SelectedItem.Value));
				InputList.Add(int.Parse(dl_StockManagable.SelectedItem.Value));
				if(dl_PropertyClassification.SelectedItem.Value.Trim() == "부자재")
					InputList.Add(0);
				else
					InputList.Add(int.Parse(dl_CheckDistinction.SelectedItem.Value));

				InputList.Add(dl_OrderPlan.SelectedItem.Value);
				InputList.Add(dl_ItemType.SelectedItem.Value);
				InputList.Add(dl_MateralQuality.SelectedItem.Value);
				InputList.Add(dl_ItemState.SelectedItem.Value);
				InputList.Add(tb_Maker.Text);
				InputList.Add(dl_ItemClassification1.SelectedItem.Value);
				InputList.Add(dl_ItemClassification2.SelectedItem.Value);
				InputList.Add(dl_ItemClassification3.SelectedItem.Value);
				InputList.Add(dl_ItemClassification4.SelectedItem.Value);
				InputList.Add(tb_Standard1.Text);
				InputList.Add(dl_Unit1.SelectedItem.Value);
				InputList.Add(tb_Standard2.Text);
				InputList.Add(dl_Unit2.SelectedItem.Value);
				InputList.Add(tb_Standard3.Text);
				InputList.Add(dl_Unit3.SelectedItem.Value);
				InputList.Add(tb_Standard4.Text);
				InputList.Add(dl_Unit4.SelectedItem.Value);
				InputList.Add(tb_ProductWeight.Text);
				InputList.Add(tb_MaterialWeight.Text);
				InputList.Add(tb_SupplyTerm.Text);
				InputList.Add(txtLeadTime.Text);
				InputList.Add(dl_DomesticImportDistinction.SelectedItem.Value);
				InputList.Add(tb_SafetyStockQuantity.Text);
				InputList.Add(tb_OrderIntervalQuantity.Text);
				InputList.Add(tb_MinOrderQuantity.Text);
				InputList.Add(tb_StiffenDeep.Text);
				InputList.Add(tb_ProductHeatTreatmentDescription.Text);
				InputList.Add(tb_MaterialHeatTreatmentDescription.Text);
				InputList.Add(tb_MaterialHeatTreatmentRequestDegree.Text);
				InputList.Add(tb_CuttingSpace.Text);
				InputList.Add(tb_TariffRate.Text);
				InputList.Add(tb_StandardUnitCost.Text);
				InputList.Add(tb_MainPurchaseCompany.Text);
				InputList.Add(tb_MainOutSideOrderCompany.Text);
				InputList.Add(tb_MainSaleCompany.Text);
				InputList.Add(dl_ChargePerson.SelectedItem.Value);

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
				rud.ActivateRUD();

				bt_Delete.Enabled = false;
				bt_Update.Enabled = false;
				bt_Registration.Enabled = true;
			}
		}


		/// <summary>
		/// 초기화 버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{	
			this.clear();

			bt_Delete.Enabled = false;
			bt_Update.Enabled = false;
			bt_Registration.Enabled = true;
			dl_PropertyClassification.Enabled = true;

		}

		/// <summary>
		/// 참고보기 버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Reference_Click(object sender, System.EventArgs e)
		{
			if(lb_ItemIndex.Text == "")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 버튼을 눌러주세요');</script>");
			}
			else
			{
				string m_TableName = "품목정보";
				int m_RecodeIdx = int.Parse(lb_ItemIndex.Text);
			

				Reference rf = new Reference(m_TableName, m_RecodeIdx);
				UltraWebGrid1.DataSource = rf.reference();
				UltraWebGrid1.DataBind();
			}
		}

		/// <summary>
		/// 수정버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(Page.IsValid == true)
			{
				if(lb_ItemIndex.Text == "")
				{
					RegisterStartupScript("","<script>alert('등록후 바로 수정은 불가능 합니다!');</script>");
				}
				else
				{
					string m_RequirePage = "ItemInfo";
					string m_RequireAction = "Update";
					string m_TableName = "II_MT";
					int m_Idx = (int)PageName.ItemInfo;
					string m_User = Session["ID"].ToString();
				
					if(dl_ItemState.SelectedIndex == 0)
						RegisterStartupScript("","<script>alert('품목상태를 선택해 주세요');</script>");
					else if(dl_ItemClassification1.SelectedIndex == 0)
						RegisterStartupScript("","<script>alert('품목분류1을 선택해 주세요');</script>");
					else if(dl_ItemClassification2.SelectedIndex == 0)
						RegisterStartupScript("","<script>alert('품목분류2를 선택해 주세요');</script>");
					else if(dl_ItemClassification3.SelectedIndex == 0)
						RegisterStartupScript("","<script>alert('품목분류3를 선택해 주세요');</script>");
					else
					{

			
						ArrayList InputList = new ArrayList();

						InputList.Add(ItemSearchControl1.ItemNum.ToString());
						InputList.Add(ItemSearchControl1.ItemDrawNum.ToString());
						InputList.Add(ItemSearchControl1.ItemName.ToString());
						InputList.Add(dl_PropertyClassification.SelectedItem.Value);
						InputList.Add(dl_Unit.SelectedItem.Value);
						InputList.Add(tb_Standard.Text);
						InputList.Add(dl_Texture.SelectedItem.Value);
						InputList.Add(tb_SupplementaryValueTaxRate.Text);
						InputList.Add(dl_StockUnit.SelectedItem.Value);
						InputList.Add(dl_BOMUnit.SelectedItem.Value);
						InputList.Add(dl_PurchaseUnit.SelectedItem.Value);
						InputList.Add(dl_SaleUnit.SelectedItem.Value);
						InputList.Add(dl_IOChackable.SelectedItem.Value);
						InputList.Add(dl_StockManagable.SelectedItem.Value);
						InputList.Add(dl_CheckDistinction.SelectedItem.Value);
						InputList.Add(dl_OrderPlan.SelectedItem.Value);
						InputList.Add(dl_ItemType.SelectedItem.Value);
						InputList.Add(dl_MateralQuality.SelectedItem.Value);
						InputList.Add(dl_ItemState.SelectedItem.Value);
						InputList.Add(tb_Maker.Text);
						InputList.Add(dl_ItemClassification1.SelectedItem.Value);
						InputList.Add(dl_ItemClassification2.SelectedItem.Value);
						InputList.Add(dl_ItemClassification3.SelectedItem.Value);
						InputList.Add(dl_ItemClassification4.SelectedItem.Value);
						InputList.Add(tb_Standard1.Text);
						InputList.Add(dl_Unit1.SelectedItem.Value);
						InputList.Add(tb_Standard2.Text);
						InputList.Add(dl_Unit2.SelectedItem.Value);
						InputList.Add(tb_Standard3.Text);
						InputList.Add(dl_Unit3.SelectedItem.Value);
						InputList.Add(tb_Standard4.Text);
						InputList.Add(dl_Unit4.SelectedItem.Value);
						InputList.Add(tb_ProductWeight.Text);
						InputList.Add(tb_MaterialWeight.Text);
						InputList.Add(tb_SupplyTerm.Text);
						InputList.Add(txtLeadTime.Text);
						InputList.Add(dl_DomesticImportDistinction.SelectedItem.Value);
						InputList.Add(tb_SafetyStockQuantity.Text);
						InputList.Add(tb_OrderIntervalQuantity.Text);
						InputList.Add(tb_MinOrderQuantity.Text);
						InputList.Add(tb_StiffenDeep.Text);
						InputList.Add(tb_ProductHeatTreatmentDescription.Text);
						InputList.Add(tb_MaterialHeatTreatmentDescription.Text);
						InputList.Add(tb_MaterialHeatTreatmentRequestDegree.Text);
						InputList.Add(tb_CuttingSpace.Text);
						InputList.Add(tb_TariffRate.Text);
						InputList.Add(tb_StandardUnitCost.Text);
						InputList.Add(tb_MainPurchaseCompany.Text);
						InputList.Add(tb_MainOutSideOrderCompany.Text);
						InputList.Add(tb_MainSaleCompany.Text);
						InputList.Add(dl_ChargePerson.SelectedItem.Value);
						InputList.Add(lb_ItemIndex.Text);


						MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User,InputList,"낱개입력");
						rud.ActivateRUD();

						bt_Delete.Enabled = false;
						bt_Update.Enabled = false;
						bt_Registration.Enabled = true;
						dl_PropertyClassification.Enabled = true;
					}
				}
			}

			lb_Property.Value = dl_PropertyClassification.SelectedItem.Text;
		}

		/// <summary>
		/// 삭제버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			if(Page.IsValid == true)
			{
				if(lb_ItemIndex.Text == "")
				{
					RegisterStartupScript("","<script>alert('등록후 바로 삭제는 불가능 합니다!');</script>");
				}
				else
				{
					string m_RequirePage = "ItemInfo";
					string m_RequireAction = "Delete";
					string m_TableName = "II_MT";
					int m_Idx = (int)PageName.ItemInfo;
					string m_User = Session["ID"].ToString();
				
			
					ArrayList InputList = new ArrayList();

					InputList.Add(ItemSearchControl1.ItemNum.ToString());
					InputList.Add(ItemSearchControl1.ItemDrawNum.ToString());
					InputList.Add(ItemSearchControl1.ItemName.ToString());
					InputList.Add(dl_PropertyClassification.SelectedItem.Value);
					InputList.Add(dl_Unit.SelectedItem.Value);
					InputList.Add(tb_Standard.Text);
					InputList.Add(dl_Texture.SelectedItem.Value);
					InputList.Add(tb_SupplementaryValueTaxRate.Text);
					InputList.Add(dl_StockUnit.SelectedItem.Value);
					InputList.Add(dl_BOMUnit.SelectedItem.Value);
					InputList.Add(dl_PurchaseUnit.SelectedItem.Value);
					InputList.Add(dl_SaleUnit.SelectedItem.Value);
					InputList.Add(dl_IOChackable.SelectedItem.Value);
					InputList.Add(dl_StockManagable.SelectedItem.Value);
					InputList.Add(dl_CheckDistinction.SelectedItem.Value);
					InputList.Add(dl_OrderPlan.SelectedItem.Value);
					InputList.Add(dl_ItemType.SelectedItem.Value);
					InputList.Add(dl_MateralQuality.SelectedItem.Value);
					InputList.Add(dl_ItemState.SelectedItem.Value);
					InputList.Add(tb_Maker.Text);
					InputList.Add(dl_ItemClassification1.SelectedItem.Value);
					InputList.Add(dl_ItemClassification2.SelectedItem.Value);
					InputList.Add(dl_ItemClassification3.SelectedItem.Value);
					InputList.Add(dl_ItemClassification4.SelectedItem.Value);
					InputList.Add(tb_Standard1.Text);
					InputList.Add(dl_Unit1.SelectedItem.Value);
					InputList.Add(tb_Standard2.Text);
					InputList.Add(dl_Unit2.SelectedItem.Value);
					InputList.Add(tb_Standard3.Text);
					InputList.Add(dl_Unit3.SelectedItem.Value);
					InputList.Add(tb_Standard4.Text);
					InputList.Add(dl_Unit4.SelectedItem.Value);
					InputList.Add(tb_ProductWeight.Text);
					InputList.Add(tb_MaterialWeight.Text);
					InputList.Add(tb_SupplyTerm.Text);
					InputList.Add(txtLeadTime.Text);
					InputList.Add(dl_DomesticImportDistinction.SelectedItem.Value);
					InputList.Add(tb_SafetyStockQuantity.Text);
					InputList.Add(tb_OrderIntervalQuantity.Text);
					InputList.Add(tb_MinOrderQuantity.Text);
					InputList.Add(tb_StiffenDeep.Text);
					InputList.Add(tb_ProductHeatTreatmentDescription.Text);
					InputList.Add(tb_MaterialHeatTreatmentDescription.Text);
					InputList.Add(tb_MaterialHeatTreatmentRequestDegree.Text);
					InputList.Add(tb_CuttingSpace.Text);
					InputList.Add(tb_TariffRate.Text);
					InputList.Add(tb_StandardUnitCost.Text);
					InputList.Add(tb_MainPurchaseCompany.Text);
					InputList.Add(tb_MainOutSideOrderCompany.Text);
					InputList.Add(tb_MainSaleCompany.Text);
					InputList.Add(dl_ChargePerson.SelectedItem.Value);
					InputList.Add(lb_ItemIndex.Text);


					MasterInfoRecordRUD rud = new MasterInfoRecordRUD(m_RequirePage, m_RequireAction, m_TableName, m_Idx, m_User, InputList,"낱개입력");
					rud.ActivateRUD();
					this.clear();
					ItemSearchControl1.ItemNum = "";
					ItemSearchControl1.ItemDrawNum = "";
					ItemSearchControl1.ItemName = "";

					dl_PropertyClassification.Enabled = true;


				}
			}
		}



		// 클리어함수
		private void clear()
		{
			dl_PropertyClassification.SelectedIndex = 0;
			dl_Texture.SelectedIndex = 0;
			tb_SupplementaryValueTaxRate.Text = "10";
			dl_Unit.SelectedIndex = 0;
			dl_Unit1.SelectedIndex = 0;
			dl_Unit2.SelectedIndex = 0;
			dl_Unit3.SelectedIndex = 0;
			dl_Unit4.SelectedIndex = 0;
			tb_Standard.Text = "";
			tb_Standard1.Text = "";
			tb_Standard2.Text = "";
			tb_Standard3.Text = "";
			tb_Standard4.Text = "";
			dl_StockUnit.SelectedIndex = 0;
			dl_BOMUnit.SelectedIndex = 0;
			dl_PurchaseUnit.SelectedIndex = 0;
			dl_SaleUnit.SelectedIndex = 0;
			dl_IOChackable.SelectedIndex = 0;
			dl_StockManagable.SelectedIndex = 0;
			dl_CheckDistinction.SelectedIndex = 0;
			dl_OrderPlan.SelectedIndex = 0;
			dl_ItemType.SelectedIndex = 0;
			dl_MateralQuality.SelectedIndex = 0;
			dl_ItemState.SelectedIndex = 0;
			tb_Maker.Text = "";
			dl_ItemClassification1.SelectedIndex = 0;
			dl_ItemClassification2.SelectedIndex = 0;
			dl_ItemClassification3.SelectedIndex = 0;
			dl_ItemClassification4.SelectedIndex = 0;
			tb_ProductWeight.Text = "";
			tb_MaterialWeight.Text = "";
			tb_SupplyTerm.Text = "";
			dl_DomesticImportDistinction.SelectedIndex = 0;
			tb_SafetyStockQuantity.Text = "0";
			tb_OrderIntervalQuantity.Text = "0";
			tb_MinOrderQuantity.Text = "0";
			tb_StiffenDeep.Text = "";
			tb_ProductHeatTreatmentDescription.Text ="";
			tb_MaterialHeatTreatmentDescription.Text = "";
			tb_MaterialHeatTreatmentRequestDegree.Text = "";
			tb_CuttingSpace.Text = "";
			tb_TariffRate.Text = "0";
			tb_StandardUnitCost.Text = "0";
			tb_MainPurchaseCompany.Text = "";
			tb_MainOutSideOrderCompany.Text = "";
			tb_MainSaleCompany.Text = "";
			dl_ChargePerson.SelectedIndex = 0;	
			txtLeadTime.Text = "0";
			lb_ItemIndex.Text = "";
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			lb_Property.Value = dl_PropertyClassification.SelectedItem.Text;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select * From II_MT Where RecodingState = 1 and ItemNum = @num ";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.NVarChar).Value = ItemSearchControl1.ItemNum;

			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);

			ItemSearchControl1.ItemNum = ds.Tables[0].Rows[0]["ItemNum"].ToString();
			ItemSearchControl1.ItemDrawNum = ds.Tables[0].Rows[0]["ItemDrawNum"].ToString();
			ItemSearchControl1.ItemName = ds.Tables[0].Rows[0]["ItemName"].ToString();


			for (int i=0 ; i < dl_PropertyClassification.Items.Count ; i++)
			{
				if( dl_PropertyClassification.Items[i].Value == ds.Tables[0].Rows[0]["PropertyClassification"].ToString())
				{
					dl_PropertyClassification.SelectedIndex = i;				
				}
			}
				
			for (int i=0 ; i < dl_Unit.Items.Count ; i++)
			{
				if( dl_Unit.Items[i].Value == ds.Tables[0].Rows[0]["Unit"].ToString())
				{
					dl_Unit.SelectedIndex = i;				
				}
			}				
			
			tb_Standard.Text = ds.Tables[0].Rows[0]["Standard"].ToString();

			if(bool.Parse(ds.Tables[0].Rows[0]["Texture"].ToString()))
				dl_Texture.SelectedIndex = 0;
			else
				dl_Texture.SelectedIndex = 1;
			
			tb_SupplementaryValueTaxRate.Text = ds.Tables[0].Rows[0]["SupplementaryValueTaxRate"].ToString();
				
				
			for (int i=0 ; i < dl_StockUnit.Items.Count ; i++)
			{
				if( dl_StockUnit.Items[i].Value == ds.Tables[0].Rows[0]["StockUnit"].ToString())
				{
					dl_StockUnit.SelectedIndex = i;				
				}
			}
			for (int i=0 ; i < dl_BOMUnit.Items.Count ; i++)
			{
				if( dl_BOMUnit.Items[i].Value == ds.Tables[0].Rows[0]["BOMUnit"].ToString())
				{
					dl_BOMUnit.SelectedIndex = i;				
				}
			}
			for (int i=0 ; i < dl_PurchaseUnit.Items.Count ; i++)
			{
				if( dl_PurchaseUnit.Items[i].Value == ds.Tables[0].Rows[0]["PurchaseUnit"].ToString())
				{
					dl_PurchaseUnit.SelectedIndex = i;				
				}
			}
			for (int i=0 ; i < dl_SaleUnit.Items.Count ; i++)
			{
				if( dl_SaleUnit.Items[i].Value == ds.Tables[0].Rows[0]["SaleUnit"].ToString())
				{
					dl_SaleUnit.SelectedIndex = i;				
				}
			}
			if(bool.Parse(ds.Tables[0].Rows[0]["IOChackable"].ToString()))
				dl_IOChackable.SelectedIndex = 0;				
			else
				dl_IOChackable.SelectedIndex = 1;

			if(bool.Parse(ds.Tables[0].Rows[0]["StockManagable"].ToString()))
				dl_StockManagable.SelectedIndex = 0;
			else
				dl_StockManagable.SelectedIndex = 1;

			if(bool.Parse(ds.Tables[0].Rows[0]["CheckDistinction"].ToString()))
				dl_CheckDistinction.SelectedIndex = 0;
			else
				dl_CheckDistinction.SelectedIndex = 1;
			
			if(bool.Parse(ds.Tables[0].Rows[0]["OrderPlan"].ToString()))
				dl_OrderPlan.SelectedIndex = 0;
			else
				dl_OrderPlan.SelectedIndex = 1;

			for (int i=0 ; i < dl_ItemType.Items.Count ; i++)
			{
				if( dl_ItemType.Items[i].Value == ds.Tables[0].Rows[0]["ItemType"].ToString())
				{
					dl_ItemType.SelectedIndex = i;				
				}
			}
			for (int i=0 ; i < dl_MateralQuality.Items.Count ; i++)
			{
				if( dl_MateralQuality.Items[i].Value == ds.Tables[0].Rows[0]["MateralQuality"].ToString())
				{
					dl_MateralQuality.SelectedIndex = i;				
				}
			}
			for (int i=0 ; i < dl_ItemState.Items.Count ; i++)
			{
				if( dl_ItemState.Items[i].Value == ds.Tables[0].Rows[0]["ItemState"].ToString())
				{
					dl_ItemState.SelectedIndex = i;				
				}
			}
			
			tb_Maker.Text = ds.Tables[0].Rows[0]["Maker"].ToString();

			for (int i=0 ; i < dl_ItemClassification1.Items.Count ; i++)
			{
				if( dl_ItemClassification1.Items[i].Value == ds.Tables[0].Rows[0]["ItemClassification1"].ToString())
				{
					dl_ItemClassification1.SelectedIndex = i;				
				}
			}
			for (int i=0 ; i < dl_ItemClassification2.Items.Count ; i++)
			{
				if( dl_ItemClassification2.Items[i].Value == ds.Tables[0].Rows[0]["ItemClassification2"].ToString())
				{
					dl_ItemClassification2.SelectedIndex = i;				
				}
			}
			for (int i=0 ; i < dl_ItemClassification3.Items.Count ; i++)
			{
				if( dl_ItemClassification3.Items[i].Value == ds.Tables[0].Rows[0]["ItemClassification3"].ToString())
				{
					dl_ItemClassification3.SelectedIndex = i;				
				}
			}
			for (int i=0 ; i < dl_ItemClassification4.Items.Count ; i++)
			{
				if( dl_ItemClassification4.Items[i].Value == ds.Tables[0].Rows[0]["ItemClassification4"].ToString())
				{
					dl_ItemClassification4.SelectedIndex = i;				
				}
			}
			
			tb_Standard1.Text = ds.Tables[0].Rows[0]["Standard1"].ToString();				
			tb_Standard2.Text = ds.Tables[0].Rows[0]["Standard2"].ToString();
			tb_Standard3.Text = ds.Tables[0].Rows[0]["Standard3"].ToString();
			tb_Standard4.Text = ds.Tables[0].Rows[0]["Standard4"].ToString();

			for (int i=0 ; i < dl_Unit1.Items.Count ; i++)
			{
				if( dl_Unit1.Items[i].Value == ds.Tables[0].Rows[0]["Unit1"].ToString())
				{
					dl_Unit1.SelectedIndex = i;				
				}
			}
			for (int i=0 ; i < dl_Unit2.Items.Count ; i++)
			{
				if( dl_Unit2.Items[i].Value == ds.Tables[0].Rows[0]["Unit2"].ToString())
				{
					dl_Unit2.SelectedIndex = i;				
				}
			}
			for (int i=0 ; i < dl_Unit3.Items.Count ; i++)
			{
				if( dl_Unit3.Items[i].Value == ds.Tables[0].Rows[0]["Unit3"].ToString())
				{
					dl_Unit3.SelectedIndex = i;				
				}
			}
			for (int i=0 ; i < dl_Unit4.Items.Count ; i++)
			{
				if( dl_Unit4.Items[i].Value == ds.Tables[0].Rows[0]["Unit4"].ToString())
				{
					dl_Unit4.SelectedIndex = i;				
				}
			}
			

			tb_ProductWeight.Text = ds.Tables[0].Rows[0]["ProductWeight"].ToString();
			tb_MaterialWeight.Text = ds.Tables[0].Rows[0]["MaterialWeight"].ToString();
			tb_SupplyTerm.Text = ds.Tables[0].Rows[0]["SupplyTerm"].ToString();
			txtLeadTime.Text = ds.Tables[0].Rows[0]["CompleteLeadTime"].ToString();
			if(bool.Parse(ds.Tables[0].Rows[0]["DomesticImportDistinction"].ToString()))
				dl_DomesticImportDistinction.SelectedIndex = 0;
			else
                dl_DomesticImportDistinction.SelectedIndex = 1;
			
			tb_SafetyStockQuantity.Text = ds.Tables[0].Rows[0]["SafetyStockQuantity"].ToString();
			tb_OrderIntervalQuantity.Text = ds.Tables[0].Rows[0]["OrderIntervalQuantity"].ToString();
			tb_MinOrderQuantity.Text = ds.Tables[0].Rows[0]["MinOrderQuantity"].ToString();
			tb_StiffenDeep.Text = ds.Tables[0].Rows[0]["StiffenDeep"].ToString();
			tb_ProductHeatTreatmentDescription.Text = ds.Tables[0].Rows[0]["ProductHeatTreatmentDescription"].ToString();
			tb_MaterialHeatTreatmentDescription.Text = ds.Tables[0].Rows[0]["MaterialHeatTreatmentDescription"].ToString();
			tb_MaterialHeatTreatmentRequestDegree.Text = ds.Tables[0].Rows[0]["MaterialHeatTreatmentRequestDegree"].ToString();
			tb_CuttingSpace.Text = ds.Tables[0].Rows[0]["CuttingSpace"].ToString();
			tb_TariffRate.Text = ds.Tables[0].Rows[0]["TariffRate"].ToString();
			tb_StandardUnitCost.Text = ds.Tables[0].Rows[0]["StandardUnitCost"].ToString();
			tb_MainPurchaseCompany.Text = ds.Tables[0].Rows[0]["MainPurchaseCompany"].ToString();
			tb_MainOutSideOrderCompany.Text = ds.Tables[0].Rows[0]["MainOutSideOrderCompany"].ToString();
			tb_MainSaleCompany.Text = ds.Tables[0].Rows[0]["MainSaleCompany"].ToString();
				

			for (int i=0 ; i < dl_ChargePerson.Items.Count ; i++)
			{
				if( dl_ChargePerson.Items[i].Value == ds.Tables[0].Rows[0]["ChargePerson"].ToString())
				{
					dl_ChargePerson.SelectedIndex = i;				
				}
			}
			
			lb_ItemIndex.Text = ds.Tables[0].Rows[0]["ItemInfoIndex"].ToString();

			dl_PropertyClassification.Enabled = false;

			bt_Delete.Enabled = true;
			bt_Update.Enabled = true;
			bt_Registration.Enabled = true;
			
			conn.Close();
		
		}

//		private void pop_Click(object sender, System.EventArgs e)
//		{
//			string txt = TextBox1.Text ;
//			string formID = "1" ;
//			string txtID = "ItemIndex";
//			RegisterStartupScript("","<script language=javascript>window.open('./Popup/Iteminfo.aspx?form=" + formID + "&txtID=" + txtID +  "&txt=" + txt + "','Item'," + "'width=800" + "," + "height=400px');</script>");
//		}

		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language=javascript>window.open('./Popup/ItemAll.aspx','ItemAll','width=830,height=500px');</script>");
		}

		private void UltraWebGrid1_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			string table = lb_ReferenceTable.Value;
			string index = lb_ReferenceIndex.Value;

			if(table == "생산품질")
				table = "T_Product";
			else if(table =="개발실")
				table = "T_Development";
			else
				table = "T_Materials";
			//Response.Write("<script language=javascript>window.open('./Popup/View.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=510" + "," + "height=435px');</script>");
			RegisterStartupScript("","<script>window.open('./Popup/View1.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=818" + "," + "height=610px');</script>");
		
		}

		private void tbTotalUpdate_Click(object sender, System.EventArgs e)
		{
			SqlConnection con1 = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			con1.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,con1);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = Session["ID"].ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			con1.Close();

			if(File1.Value != "")
			{
				string FileName = System.IO.Path.GetDirectoryName(File1.PostedFile.FileName) + System.IO.Path.GetFileName(File1.PostedFile.FileName);
			
				if(FileName.ToString().Substring(FileName.ToString().Length - 3) == "xls" || FileName.ToString().Substring(FileName.ToString().Length - 3) == "XLS")
				{

					string upLoadFile = Path.GetTempFileName();
					File1.PostedFile.SaveAs(upLoadFile);

					OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + upLoadFile + "';Extended Properties=Excel 8.0;");
			
					con.Open();
			
					OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", con);
						
					adapter.Fill(dataset);
					con.Close();

					SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
					conn.Open();
					SqlTransaction tr = conn.BeginTransaction();

					try
					{

						UnitCodeInfo(conn,tr);
						
						Response.Write("<script language=javascript>");
						Response.Write("alert('수정하였습니다.');");
						Response.Write("</script>");
						tr.Commit();
					}
					catch(Exception ee)
					{
						Response.Write("<script language=javascript>");
						Response.Write("alert('"+ee.Message+"');");
						Response.Write("</script>");

						tr.Rollback();
					}
					finally
					{
						File.Delete(upLoadFile);
						conn.Close();
					}
				}
				else
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('엑셀 양식의 파일이 아닙니다.');");
					Response.Write("</script>");
				}
			}
			else
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('입력할 엑셀자료가 없습니다.');");
				Response.Write("</script>");
			}
		}


		private void UnitCodeInfo(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			string str = "";

			
			foreach(DataRow row in dataset.Tables[0].Rows)
			{
				
				str = @"Update II_MT Set 
						Unit = @Unit, Standard = @Standard, Texture= @Texture, SupplementaryValueTaxRate = @SupplementaryValueTaxRate, 
						StockUnit = @StockUnit, BOMUnit = @BOMUnit, PurchaseUnit = @PurchaseUnit, SaleUnit = @SaleUnit, IOChackable = @IOChackable, 
						StockManagable = @StockManagable, CheckDistinction = @CheckDistinction, OrderPlan = @OrderPlan, ItemType = @ItemType, 
						MateralQuality = @MateralQuality, ItemState = @ItemState, Maker = @Maker, ItemClassification1 = @ItemClassification1, 
						ItemClassification2 = @ItemClassification2, ItemClassification3 = @ItemClassification3, ItemClassification4 = @ItemClassification4, 
						Standard1 = @Standard1, Unit1 = @Unit1, Standard2 = @Standard2, Unit2 = @Unit2, Standard3 = @Standard3, Unit3 = @Unit3, Standard4 = @Standard4, 
						Unit4 = @Unit4, ProductWeight = @ProductWeight, MaterialWeight = @MaterialWeight, SupplyTerm = @SupplyTerm, CompleteLeadTime = @CompleteLeadTime,
						DomesticImportDistinction = @DomesticImportDistinction, SafetyStockQuantity = @SafetyStockQuantity, OrderIntervalQuantity = @OrderIntervalQuantity, MinOrderQuantity = @MinOrderQuantity, StiffenDeep = @StiffenDeep, 
						ProductHeatTreatmentDescription = @ProductHeatTreatmentDescription, MaterialHeatTreatmentDescription = @MaterialHeatTreatmentDescription, MaterialHeatTreatmentRequestDegree = @MaterialHeatTreatmentRequestDegree, 
						CuttingSpace = @CuttingSpace, TariffRate = @TariffRate, StandardUnitCost = @StandardUnitCost, MainPurchaseCompany = @MainPurchaseCompany, 
						MainOutSideOrderCompany = @MainOutSideOrderCompany, MainSaleCompany = @MainSaleCompany, ChargePerson = @ChargePerson, UpdatingPerson = @UpdatingPerson, UpdatingPersonID = @UpdatingPersonID, UpdatingDate = @UpdatingDate
						Where ItemNum = @ItemNum and RecodingState = 1";

				comm.Parameters.Add("@Unit",UnitCode(row["단위"].ToString().Trim(),con, trans,1));
				comm.Parameters.Add("@Standard",row["규격"].ToString().Trim());

				if(row["과세여부"].ToString().Trim() == "")
					comm.Parameters.Add("@Texture","1");
				else
					comm.Parameters.Add("@Texture",row["과세여부"].ToString().Trim());

				if(row["부가세율"].ToString().Trim() == "")
					comm.Parameters.Add("@SupplementaryValueTaxRate","10");
				else
					comm.Parameters.Add("@SupplementaryValueTaxRate",row["부가세율"].ToString().Trim());


				comm.Parameters.Add("@StockUnit",UnitCode(row["재고단위"].ToString().Trim(),con,trans,2));
				comm.Parameters.Add("@BOMUnit",UnitCode(row["BOM단위"].ToString().Trim(),con,trans,3));
				comm.Parameters.Add("@PurchaseUnit",UnitCode(row["구매단위"].ToString().Trim(),con,trans,4));
				comm.Parameters.Add("@SaleUnit",UnitCode(row["판매단위"].ToString().Trim(),con,trans,5));



				if(row["자재산출여부"].ToString().Trim() == "" || row["자재산출여부"].ToString().Trim() == "예")
					comm.Parameters.Add("@IOChackable","1");
				else
					comm.Parameters.Add("@IOChackable","0");
				
				if(row["재고관리여부"].ToString().Trim() == "" || row["재고관리여부"].ToString().Trim() == "예")
					comm.Parameters.Add("@StockManagable","1");
				else
					comm.Parameters.Add("@StockManagable","0");
				
				if(row["검사구분"].ToString().Trim() == "" || row["검사구분"].ToString().Trim() == "예")
					comm.Parameters.Add("@CheckDistinction","1");
				else
					comm.Parameters.Add("@CheckDistinction","0");
				
				if(row["발주방침"].ToString().Trim() == "" || row["발주방침"].ToString().Trim() == "계산발주")
					comm.Parameters.Add("@OrderPlan","1");
				else
					comm.Parameters.Add("@OrderPlan","0");
				
				comm.Parameters.Add("@ItemType",UnitCode(row["품목타입"].ToString().Trim(),con,trans,6));
				comm.Parameters.Add("@MateralQuality",UnitCode(row["재질"].ToString().Trim(),con,trans,7));

				if(row["품목상태"].ToString().Trim() == "")
					comm.Parameters.Add("@ItemState","06300010");//양산
				else
					comm.Parameters.Add("@ItemState",UnitCode(row["품목상태"].ToString().Trim(),con,trans,8));
				
				


				comm.Parameters.Add("@Maker",row["메이커"].ToString().Trim());
				comm.Parameters.Add("@ItemClassification1",UnitCode(row["품목분류1"].ToString().Trim(),con,trans,9));
				comm.Parameters.Add("@ItemClassification2",UnitCode(row["품목분류2"].ToString().Trim(),con,trans,10));
				comm.Parameters.Add("@ItemClassification3",UnitCode(row["품목분류3"].ToString().Trim(),con,trans,11));
				comm.Parameters.Add("@ItemClassification4",UnitCode(row["품목분류4"].ToString().Trim(),con,trans,12));
				comm.Parameters.Add("@Unit1",UnitCode(row["단위1"].ToString().Trim(),con,trans,13));
				comm.Parameters.Add("@Standard1",row["규격1"].ToString().Trim());
				comm.Parameters.Add("@Unit2",UnitCode(row["단위2"].ToString().Trim(),con,trans,14));
				comm.Parameters.Add("@Standard2",row["규격2"].ToString().Trim());
				comm.Parameters.Add("@Unit3",UnitCode(row["단위3"].ToString().Trim(),con,trans,15));
				comm.Parameters.Add("@Standard3",row["규격3"].ToString().Trim());
				comm.Parameters.Add("@Unit4",UnitCode(row["단위4"].ToString().Trim(),con,trans,16));
				comm.Parameters.Add("@Standard4",row["규격4"].ToString().Trim());

				comm.Parameters.Add("@ProductWeight",row["제품중량"].ToString().Trim());
				comm.Parameters.Add("@MaterialWeight",row["소재중량"].ToString().Trim());
				comm.Parameters.Add("@SupplyTerm",row["조달기간"].ToString().Trim());
				if(row["완성리드타임"].ToString().Trim() == "")
					comm.Parameters.Add("@CompleteLeadTime","1");
				else
					comm.Parameters.Add("@CompleteLeadTime",row["완성리드타임"].ToString().Trim());
				
				if(row["내외자구분"].ToString().Trim() == "" || row["내외자구분"].ToString().Trim() =="내자")
					comm.Parameters.Add("@DomesticImportDistinction","1");
				else
					comm.Parameters.Add("@DomesticImportDistinction","0");

				if(row["안전재고량"].ToString().Trim() == "")
					comm.Parameters.Add("@SafetyStockQuantity","0");
				else
					comm.Parameters.Add("@SafetyStockQuantity",row["안전재고량"].ToString().Trim());

				if(row["발주간격수량"].ToString().Trim() == "")
					comm.Parameters.Add("@OrderIntervalQuantity","0");
				else
					comm.Parameters.Add("@OrderIntervalQuantity",row["발주간격수량"].ToString().Trim());
				
				if(row["최소발주량"].ToString().Trim() == "")
					comm.Parameters.Add("@MinOrderQuantity","0");
				else
					comm.Parameters.Add("@MinOrderQuantity",row["최소발주량"].ToString().Trim());

				comm.Parameters.Add("@StiffenDeep",row["경화깊이"].ToString().Trim());
				comm.Parameters.Add("@ProductHeatTreatmentDescription",row["제품열처리사양"].ToString().Trim());
				comm.Parameters.Add("@MaterialHeatTreatmentDescription",row["소재열처리사양"].ToString().Trim());
				comm.Parameters.Add("@MaterialHeatTreatmentRequestDegree",row["소재열처리정도"].ToString().Trim());
				comm.Parameters.Add("@CuttingSpace",row["절단여유"].ToString().Trim());
				

				
				if(row["관세율"].ToString().Trim() == "")
					comm.Parameters.Add("@TariffRate","0");
				else
					comm.Parameters.Add("@TariffRate",row["관세율"].ToString().Trim());
				
				if(row["기준단가"].ToString().Trim() == "")
					comm.Parameters.Add("@StandardUnitCost","0");
				else
					comm.Parameters.Add("@StandardUnitCost",row["기준단가"].ToString().Trim());
				
				comm.Parameters.Add("@MainPurchaseCompany",row["주구입처"].ToString().Trim());
				comm.Parameters.Add("@MainOutSideOrderCompany",row["주외주처"].ToString().Trim());
				comm.Parameters.Add("@MainSaleCompany",row["주판매처"].ToString().Trim());
				comm.Parameters.Add("@ChargePerson",row["담당자"].ToString().Trim());
				comm.Parameters.Add("@UpdatingPerson",m_UserName);
				comm.Parameters.Add("@UpdatingPersonID",Session["ID"].ToString().Trim());
				comm.Parameters.Add("@UpdatingDate",DateTime.Now.ToShortDateString());
				comm.Parameters.Add("@ItemNum",row["품목번호"].ToString().Trim());

				
				comm.CommandText = str;
				

				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

			}
		}


		private string UnitCode(string Unit,SqlConnection con, SqlTransaction trans,int a)
		{
			string Code = "";

			if(Unit.Trim() == "")
			{
				return Code;
			}
			else
			{
				
				SqlCommand comm = new SqlCommand();
				comm.Connection = con;
				comm.Transaction = trans;
				string str ="";
				switch(a)
				{
					case 1:
                        str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0400' and SmallClassificationName = @Unit";
						break;
					case 2:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0510' and SmallClassificationName = @Unit";
						break;
					case 3:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0520' and SmallClassificationName = @Unit";
						break;
					case 4:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0530' and SmallClassificationName = @Unit";
						break;
					case 5:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0540' and SmallClassificationName = @Unit";
						break;
					case 6:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0610' and SmallClassificationName = @Unit";
						break;
					case 7:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0620' and SmallClassificationName = @Unit";
						break;
					case 8:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0630' and SmallClassificationName = @Unit";
						break;
					case 9:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0210' and SmallClassificationName = @Unit";
						break;
					case 10:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0220' and SmallClassificationName = @Unit";
						break;
					case 11:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0230' and SmallClassificationName = @Unit";
						break;
					case 12:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0240' and SmallClassificationName = @Unit";
						break;
					case 13:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0410' and SmallClassificationName = @Unit";
						break;
					case 14:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0420' and SmallClassificationName = @Unit";
						break;
					case 15:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0430' and SmallClassificationName = @Unit";
						break;
					case 16:
						str = @"Select SmallClassificationCode From PUC_MT Where RecodingState = 1 and LargeClassificationCode = '0440' and SmallClassificationName = @Unit";
						break;					
				}
				comm.CommandText = str;
				comm.Parameters.Add("@Unit",Unit.Trim());
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					Code = dr["SmallClassificationCode"].ToString();
				}
				dr.Close();

				if(Code == "")
					throw new Exception(""+Unit+" 인 공정이 존재하지 않습니다!");

				return Code;
			}
		}
		
	}
}
