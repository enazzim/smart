using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Infragistics.WebUI;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;


using System.IO;
using Microsoft.Win32;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// ReceiveingbundleRegistration에 대한 요약 설명입니다.
	/// </summary>
	public class ReceiveingbundleRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		protected System.Data.SqlClient.SqlConnection sqlConnection1;
		protected System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		protected KIT_ERP.BusinessManagement.dsRO dsRO1;
		protected System.Data.SqlClient.SqlDataAdapter sqlDataAdapter2;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand2;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
		protected KIT_ERP.BusinessManagement.dsROErr dsROErr1;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.Button btSearch;
		protected System.Web.UI.WebControls.Button bt_Register;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected System.Web.UI.HtmlControls.HtmlInputFile Find;
		protected System.Web.UI.WebControls.Button Button3;
		//protected KIT_ERP.BusinessManagement.dsROErr dsROErr1;
		protected System.Web.UI.WebControls.Label Label2;
		//protected int vol=0;//볼륨번호 저장
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
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
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
			this.dsRO1 = new KIT_ERP.BusinessManagement.dsRO();
			this.sqlDataAdapter2 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
			this.dsROErr1 = new KIT_ERP.BusinessManagement.dsROErr();
			((System.ComponentModel.ISupportInitialize)(this.dsRO1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsROErr1)).BeginInit();
			this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.UltraWebGrid2.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid2_PageIndexChanged);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = @"SELECT ItemNum, ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, PropertyClassification, ProductionRequestDivision, ReceivingOrderDate, ApplyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, DeliveryRequestQuantity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, DeliveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, ReceivingOrderHistoryIndex FROM RO_HT where ItemNum is null";
			this.sqlSelectCommand1.Connection = this.sqlConnection1;
			// 
			// sqlConnection1
			// 
			this.sqlConnection1.ConnectionString = "workstation id=\"EZSYS-ERP01\";packet size=4096;user id=sa;data source=\"220.66.115." +
				"9\";persist security info=True;initial catalog=Sindong_ERP;password=\"ezsys#0509\"";
			// 
			// sqlInsertCommand1
			// 
			this.sqlInsertCommand1.CommandText = "INSERT INTO RO_HT(ItemNum, ItemDrawNum, ItemName, CompanyName, BusinessRegistrati" +
				"onNum, PropertyClassification, ProductionRequestDivision, ReceivingOrderDate, Ap" +
				"plyUnitCost, DeliveryRequestQuantity1, DeliveryRequestDate1, DeliveryRequestQuan" +
				"tity2, DeliveryRequestDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, Del" +
				"iveryRequestQuantity4, DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryR" +
				"equestDate5, TotalReceiveingOrderQuantity, TotalCost, OutStorehouseQuantity, Sui" +
				"tabilityQuantity, UnInspectionQuantity, RemainderQuantity, OrderNum, DeliveryPla" +
				"ce, VolumNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, Regis" +
				"trationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate) VALUES (@ItemNum, @" +
				"ItemDrawNum, @ItemName, @CompanyName, @BusinessRegistrationNum, @PropertyClassif" +
				"ication, @ProductionRequestDivision, @ReceivingOrderDate, @ApplyUnitCost, @Deliv" +
				"eryRequestQuantity1, @DeliveryRequestDate1, @DeliveryRequestQuantity2, @Delivery" +
				"RequestDate2, @DeliveryRequestQuantity3, @DeliveryRequestDate3, @DeliveryRequest" +
				"Quantity4, @DeliveryRequestDate4, @DeliveryRequestQuantity5, @DeliveryRequestDat" +
				"e5, @TotalReceiveingOrderQuantity, @TotalCost, @OutStorehouseQuantity, @Suitabil" +
				"ityQuantity, @UnInspectionQuantity, @RemainderQuantity, @OrderNum, @DeliveryPlac" +
				"e, @VolumNum, @ProgressCondition, @RegistrationPerson, @RegistrationPersonID, @R" +
				"egistrationDate, @UpdatingPerson, @UpdatingPersonID, @UpdatingDate); SELECT Item" +
				"Num, ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, PropertyClassi" +
				"fication, ProductionRequestDivision, ReceivingOrderDate, ApplyUnitCost, Delivery" +
				"RequestQuantity1, DeliveryRequestDate1, DeliveryRequestQuantity2, DeliveryReques" +
				"tDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, DeliveryRequestQuantity4" +
				", DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, TotalRec" +
				"eiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, UnI" +
				"nspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, Progres" +
				"sCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, Updating" +
				"Person, UpdatingPersonID, UpdatingDate, ReceivingOrderHistoryIndex FROM RO_HT WH" +
				"ERE (ReceivingOrderHistoryIndex = @@IDENTITY)";
			this.sqlInsertCommand1.Connection = this.sqlConnection1;
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.VarChar, 30, "ItemNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemDrawNum", System.Data.SqlDbType.VarChar, 30, "ItemDrawNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemName", System.Data.SqlDbType.VarChar, 30, "ItemName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.VarChar, 50, "CompanyName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BusinessRegistrationNum", System.Data.SqlDbType.VarChar, 30, "BusinessRegistrationNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyClassification", System.Data.SqlDbType.VarChar, 10, "PropertyClassification"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductionRequestDivision", System.Data.SqlDbType.Bit, 1, "ProductionRequestDivision"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, "ReceivingOrderDate"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestQuantity1", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity1", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate1", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate1"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestQuantity2", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity2", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate2", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate2"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestQuantity3", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity3", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate3", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate3"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestQuantity4", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity4", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate4", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate4"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestQuantity5", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity5", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate5", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate5"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalCost", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OutStorehouseQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OutStorehouseQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SuitabilityQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "SuitabilityQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnInspectionQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "UnInspectionQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RemainderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "RemainderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderNum", System.Data.SqlDbType.VarChar, 50, "OrderNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryPlace", System.Data.SqlDbType.VarChar, 50, "DeliveryPlace"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@VolumNum", System.Data.SqlDbType.Int, 4, "VolumNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressCondition", System.Data.SqlDbType.VarChar, 10, "ProgressCondition"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPerson", System.Data.SqlDbType.VarChar, 10, "RegistrationPerson"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPersonID", System.Data.SqlDbType.VarChar, 10, "RegistrationPersonID"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationDate", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPerson", System.Data.SqlDbType.VarChar, 10, "UpdatingPerson"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPersonID", System.Data.SqlDbType.VarChar, 10, "UpdatingPersonID"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingDate", System.Data.SqlDbType.DateTime, 4, "UpdatingDate"));
			// 
			// sqlUpdateCommand1
			// 
			this.sqlUpdateCommand1.CommandText = "UPDATE RO_HT SET ItemNum = @ItemNum, ItemDrawNum = @ItemDrawNum, ItemName = @Item" +
				"Name, CompanyName = @CompanyName, BusinessRegistrationNum = @BusinessRegistratio" +
				"nNum, PropertyClassification = @PropertyClassification, ProductionRequestDivisio" +
				"n = @ProductionRequestDivision, ReceivingOrderDate = @ReceivingOrderDate, ApplyU" +
				"nitCost = @ApplyUnitCost, DeliveryRequestQuantity1 = @DeliveryRequestQuantity1, " +
				"DeliveryRequestDate1 = @DeliveryRequestDate1, DeliveryRequestQuantity2 = @Delive" +
				"ryRequestQuantity2, DeliveryRequestDate2 = @DeliveryRequestDate2, DeliveryReques" +
				"tQuantity3 = @DeliveryRequestQuantity3, DeliveryRequestDate3 = @DeliveryRequestD" +
				"ate3, DeliveryRequestQuantity4 = @DeliveryRequestQuantity4, DeliveryRequestDate4" +
				" = @DeliveryRequestDate4, DeliveryRequestQuantity5 = @DeliveryRequestQuantity5, " +
				"DeliveryRequestDate5 = @DeliveryRequestDate5, TotalReceiveingOrderQuantity = @To" +
				"talReceiveingOrderQuantity, TotalCost = @TotalCost, OutStorehouseQuantity = @Out" +
				"StorehouseQuantity, SuitabilityQuantity = @SuitabilityQuantity, UnInspectionQuan" +
				"tity = @UnInspectionQuantity, RemainderQuantity = @RemainderQuantity, OrderNum =" +
				" @OrderNum, DeliveryPlace = @DeliveryPlace, VolumNum = @VolumNum, ProgressCondit" +
				"ion = @ProgressCondition, RegistrationPerson = @RegistrationPerson, Registration" +
				"PersonID = @RegistrationPersonID, RegistrationDate = @RegistrationDate, Updating" +
				"Person = @UpdatingPerson, UpdatingPersonID = @UpdatingPersonID, UpdatingDate = @" +
				"UpdatingDate WHERE (ReceivingOrderHistoryIndex = @Original_ReceivingOrderHistory" +
				"Index) AND (ApplyUnitCost = @Original_ApplyUnitCost OR @Original_ApplyUnitCost I" +
				"S NULL AND ApplyUnitCost IS NULL) AND (BusinessRegistrationNum = @Original_Busin" +
				"essRegistrationNum OR @Original_BusinessRegistrationNum IS NULL AND BusinessRegi" +
				"strationNum IS NULL) AND (CompanyName = @Original_CompanyName OR @Original_Compa" +
				"nyName IS NULL AND CompanyName IS NULL) AND (DeliveryPlace = @Original_DeliveryP" +
				"lace OR @Original_DeliveryPlace IS NULL AND DeliveryPlace IS NULL) AND (Delivery" +
				"RequestDate1 = @Original_DeliveryRequestDate1 OR @Original_DeliveryRequestDate1 " +
				"IS NULL AND DeliveryRequestDate1 IS NULL) AND (DeliveryRequestDate2 = @Original_" +
				"DeliveryRequestDate2 OR @Original_DeliveryRequestDate2 IS NULL AND DeliveryReque" +
				"stDate2 IS NULL) AND (DeliveryRequestDate3 = @Original_DeliveryRequestDate3 OR @" +
				"Original_DeliveryRequestDate3 IS NULL AND DeliveryRequestDate3 IS NULL) AND (Del" +
				"iveryRequestDate4 = @Original_DeliveryRequestDate4 OR @Original_DeliveryRequestD" +
				"ate4 IS NULL AND DeliveryRequestDate4 IS NULL) AND (DeliveryRequestDate5 = @Orig" +
				"inal_DeliveryRequestDate5 OR @Original_DeliveryRequestDate5 IS NULL AND Delivery" +
				"RequestDate5 IS NULL) AND (DeliveryRequestQuantity1 = @Original_DeliveryRequestQ" +
				"uantity1 OR @Original_DeliveryRequestQuantity1 IS NULL AND DeliveryRequestQuanti" +
				"ty1 IS NULL) AND (DeliveryRequestQuantity2 = @Original_DeliveryRequestQuantity2 " +
				"OR @Original_DeliveryRequestQuantity2 IS NULL AND DeliveryRequestQuantity2 IS NU" +
				"LL) AND (DeliveryRequestQuantity3 = @Original_DeliveryRequestQuantity3 OR @Origi" +
				"nal_DeliveryRequestQuantity3 IS NULL AND DeliveryRequestQuantity3 IS NULL) AND (" +
				"DeliveryRequestQuantity4 = @Original_DeliveryRequestQuantity4 OR @Original_Deliv" +
				"eryRequestQuantity4 IS NULL AND DeliveryRequestQuantity4 IS NULL) AND (DeliveryR" +
				"equestQuantity5 = @Original_DeliveryRequestQuantity5 OR @Original_DeliveryReques" +
				"tQuantity5 IS NULL AND DeliveryRequestQuantity5 IS NULL) AND (ItemDrawNum = @Ori" +
				"ginal_ItemDrawNum OR @Original_ItemDrawNum IS NULL AND ItemDrawNum IS NULL) AND " +
				"(ItemName = @Original_ItemName OR @Original_ItemName IS NULL AND ItemName IS NUL" +
				"L) AND (ItemNum = @Original_ItemNum OR @Original_ItemNum IS NULL AND ItemNum IS " +
				"NULL) AND (OrderNum = @Original_OrderNum OR @Original_OrderNum IS NULL AND Order" +
				"Num IS NULL) AND (OutStorehouseQuantity = @Original_OutStorehouseQuantity OR @Or" +
				"iginal_OutStorehouseQuantity IS NULL AND OutStorehouseQuantity IS NULL) AND (Pro" +
				"ductionRequestDivision = @Original_ProductionRequestDivision OR @Original_Produc" +
				"tionRequestDivision IS NULL AND ProductionRequestDivision IS NULL) AND (Progress" +
				"Condition = @Original_ProgressCondition OR @Original_ProgressCondition IS NULL A" +
				"ND ProgressCondition IS NULL) AND (PropertyClassification = @Original_PropertyCl" +
				"assification OR @Original_PropertyClassification IS NULL AND PropertyClassificat" +
				"ion IS NULL) AND (ReceivingOrderDate = @Original_ReceivingOrderDate OR @Original" +
				"_ReceivingOrderDate IS NULL AND ReceivingOrderDate IS NULL) AND (RegistrationDat" +
				"e = @Original_RegistrationDate OR @Original_RegistrationDate IS NULL AND Registr" +
				"ationDate IS NULL) AND (RegistrationPerson = @Original_RegistrationPerson OR @Or" +
				"iginal_RegistrationPerson IS NULL AND RegistrationPerson IS NULL) AND (Registrat" +
				"ionPersonID = @Original_RegistrationPersonID OR @Original_RegistrationPersonID I" +
				"S NULL AND RegistrationPersonID IS NULL) AND (RemainderQuantity = @Original_Rema" +
				"inderQuantity OR @Original_RemainderQuantity IS NULL AND RemainderQuantity IS NU" +
				"LL) AND (SuitabilityQuantity = @Original_SuitabilityQuantity OR @Original_Suitab" +
				"ilityQuantity IS NULL AND SuitabilityQuantity IS NULL) AND (TotalCost = @Origina" +
				"l_TotalCost OR @Original_TotalCost IS NULL AND TotalCost IS NULL) AND (TotalRece" +
				"iveingOrderQuantity = @Original_TotalReceiveingOrderQuantity OR @Original_TotalR" +
				"eceiveingOrderQuantity IS NULL AND TotalReceiveingOrderQuantity IS NULL) AND (Un" +
				"InspectionQuantity = @Original_UnInspectionQuantity OR @Original_UnInspectionQua" +
				"ntity IS NULL AND UnInspectionQuantity IS NULL) AND (UpdatingDate = @Original_Up" +
				"datingDate OR @Original_UpdatingDate IS NULL AND UpdatingDate IS NULL) AND (Upda" +
				"tingPerson = @Original_UpdatingPerson OR @Original_UpdatingPerson IS NULL AND Up" +
				"datingPerson IS NULL) AND (UpdatingPersonID = @Original_UpdatingPersonID OR @Ori" +
				"ginal_UpdatingPersonID IS NULL AND UpdatingPersonID IS NULL) AND (VolumNum = @Or" +
				"iginal_VolumNum OR @Original_VolumNum IS NULL AND VolumNum IS NULL); SELECT Item" +
				"Num, ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, PropertyClassi" +
				"fication, ProductionRequestDivision, ReceivingOrderDate, ApplyUnitCost, Delivery" +
				"RequestQuantity1, DeliveryRequestDate1, DeliveryRequestQuantity2, DeliveryReques" +
				"tDate2, DeliveryRequestQuantity3, DeliveryRequestDate3, DeliveryRequestQuantity4" +
				", DeliveryRequestDate4, DeliveryRequestQuantity5, DeliveryRequestDate5, TotalRec" +
				"eiveingOrderQuantity, TotalCost, OutStorehouseQuantity, SuitabilityQuantity, UnI" +
				"nspectionQuantity, RemainderQuantity, OrderNum, DeliveryPlace, VolumNum, Progres" +
				"sCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, Updating" +
				"Person, UpdatingPersonID, UpdatingDate, ReceivingOrderHistoryIndex FROM RO_HT WH" +
				"ERE (ReceivingOrderHistoryIndex = @ReceivingOrderHistoryIndex)";
			this.sqlUpdateCommand1.Connection = this.sqlConnection1;
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.VarChar, 30, "ItemNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemDrawNum", System.Data.SqlDbType.VarChar, 30, "ItemDrawNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemName", System.Data.SqlDbType.VarChar, 30, "ItemName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.VarChar, 50, "CompanyName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BusinessRegistrationNum", System.Data.SqlDbType.VarChar, 30, "BusinessRegistrationNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PropertyClassification", System.Data.SqlDbType.VarChar, 10, "PropertyClassification"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductionRequestDivision", System.Data.SqlDbType.Bit, 1, "ProductionRequestDivision"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, "ReceivingOrderDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestQuantity1", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity1", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate1", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate1"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestQuantity2", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity2", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate2", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate2"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestQuantity3", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity3", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate3", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate3"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestQuantity4", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity4", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate4", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate4"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestQuantity5", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity5", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate5", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate5"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalCost", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OutStorehouseQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OutStorehouseQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SuitabilityQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "SuitabilityQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnInspectionQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "UnInspectionQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RemainderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "RemainderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderNum", System.Data.SqlDbType.VarChar, 50, "OrderNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryPlace", System.Data.SqlDbType.VarChar, 50, "DeliveryPlace"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@VolumNum", System.Data.SqlDbType.Int, 4, "VolumNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressCondition", System.Data.SqlDbType.VarChar, 10, "ProgressCondition"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPerson", System.Data.SqlDbType.VarChar, 10, "RegistrationPerson"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPersonID", System.Data.SqlDbType.VarChar, 10, "RegistrationPersonID"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationDate", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPerson", System.Data.SqlDbType.VarChar, 10, "UpdatingPerson"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPersonID", System.Data.SqlDbType.VarChar, 10, "UpdatingPersonID"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingDate", System.Data.SqlDbType.DateTime, 4, "UpdatingDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BusinessRegistrationNum", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BusinessRegistrationNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CompanyName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryPlace", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryPlace", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate1", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate1", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate2", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate2", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate3", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate3", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate4", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate4", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate5", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate5", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestQuantity1", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity1", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestQuantity2", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity2", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestQuantity3", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity3", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestQuantity4", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity4", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestQuantity5", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity5", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemDrawNum", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemName", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OrderNum", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OrderNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OutStorehouseQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OutStorehouseQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductionRequestDivision", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductionRequestDivision", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressCondition", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProgressCondition", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PropertyClassification", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PropertyClassification", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPerson", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPerson", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPersonID", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RemainderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "RemainderQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SuitabilityQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "SuitabilityQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TotalCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalCost", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UnInspectionQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "UnInspectionQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPerson", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPerson", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPersonID", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_VolumNum", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "VolumNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceivingOrderHistoryIndex", System.Data.SqlDbType.Int, 4, "ReceivingOrderHistoryIndex"));
			// 
			// sqlDeleteCommand1
			// 
			this.sqlDeleteCommand1.CommandText = "DELETE FROM RO_HT WHERE (ReceivingOrderHistoryIndex = @Original_ReceivingOrderHis" +
				"toryIndex) AND (ApplyUnitCost = @Original_ApplyUnitCost OR @Original_ApplyUnitCo" +
				"st IS NULL AND ApplyUnitCost IS NULL) AND (BusinessRegistrationNum = @Original_B" +
				"usinessRegistrationNum OR @Original_BusinessRegistrationNum IS NULL AND Business" +
				"RegistrationNum IS NULL) AND (CompanyName = @Original_CompanyName OR @Original_C" +
				"ompanyName IS NULL AND CompanyName IS NULL) AND (DeliveryPlace = @Original_Deliv" +
				"eryPlace OR @Original_DeliveryPlace IS NULL AND DeliveryPlace IS NULL) AND (Deli" +
				"veryRequestDate1 = @Original_DeliveryRequestDate1 OR @Original_DeliveryRequestDa" +
				"te1 IS NULL AND DeliveryRequestDate1 IS NULL) AND (DeliveryRequestDate2 = @Origi" +
				"nal_DeliveryRequestDate2 OR @Original_DeliveryRequestDate2 IS NULL AND DeliveryR" +
				"equestDate2 IS NULL) AND (DeliveryRequestDate3 = @Original_DeliveryRequestDate3 " +
				"OR @Original_DeliveryRequestDate3 IS NULL AND DeliveryRequestDate3 IS NULL) AND " +
				"(DeliveryRequestDate4 = @Original_DeliveryRequestDate4 OR @Original_DeliveryRequ" +
				"estDate4 IS NULL AND DeliveryRequestDate4 IS NULL) AND (DeliveryRequestDate5 = @" +
				"Original_DeliveryRequestDate5 OR @Original_DeliveryRequestDate5 IS NULL AND Deli" +
				"veryRequestDate5 IS NULL) AND (DeliveryRequestQuantity1 = @Original_DeliveryRequ" +
				"estQuantity1 OR @Original_DeliveryRequestQuantity1 IS NULL AND DeliveryRequestQu" +
				"antity1 IS NULL) AND (DeliveryRequestQuantity2 = @Original_DeliveryRequestQuanti" +
				"ty2 OR @Original_DeliveryRequestQuantity2 IS NULL AND DeliveryRequestQuantity2 I" +
				"S NULL) AND (DeliveryRequestQuantity3 = @Original_DeliveryRequestQuantity3 OR @O" +
				"riginal_DeliveryRequestQuantity3 IS NULL AND DeliveryRequestQuantity3 IS NULL) A" +
				"ND (DeliveryRequestQuantity4 = @Original_DeliveryRequestQuantity4 OR @Original_D" +
				"eliveryRequestQuantity4 IS NULL AND DeliveryRequestQuantity4 IS NULL) AND (Deliv" +
				"eryRequestQuantity5 = @Original_DeliveryRequestQuantity5 OR @Original_DeliveryRe" +
				"questQuantity5 IS NULL AND DeliveryRequestQuantity5 IS NULL) AND (ItemDrawNum = " +
				"@Original_ItemDrawNum OR @Original_ItemDrawNum IS NULL AND ItemDrawNum IS NULL) " +
				"AND (ItemName = @Original_ItemName OR @Original_ItemName IS NULL AND ItemName IS" +
				" NULL) AND (ItemNum = @Original_ItemNum OR @Original_ItemNum IS NULL AND ItemNum" +
				" IS NULL) AND (OrderNum = @Original_OrderNum OR @Original_OrderNum IS NULL AND O" +
				"rderNum IS NULL) AND (OutStorehouseQuantity = @Original_OutStorehouseQuantity OR" +
				" @Original_OutStorehouseQuantity IS NULL AND OutStorehouseQuantity IS NULL) AND " +
				"(ProductionRequestDivision = @Original_ProductionRequestDivision OR @Original_Pr" +
				"oductionRequestDivision IS NULL AND ProductionRequestDivision IS NULL) AND (Prog" +
				"ressCondition = @Original_ProgressCondition OR @Original_ProgressCondition IS NU" +
				"LL AND ProgressCondition IS NULL) AND (PropertyClassification = @Original_Proper" +
				"tyClassification OR @Original_PropertyClassification IS NULL AND PropertyClassif" +
				"ication IS NULL) AND (ReceivingOrderDate = @Original_ReceivingOrderDate OR @Orig" +
				"inal_ReceivingOrderDate IS NULL AND ReceivingOrderDate IS NULL) AND (Registratio" +
				"nDate = @Original_RegistrationDate OR @Original_RegistrationDate IS NULL AND Reg" +
				"istrationDate IS NULL) AND (RegistrationPerson = @Original_RegistrationPerson OR" +
				" @Original_RegistrationPerson IS NULL AND RegistrationPerson IS NULL) AND (Regis" +
				"trationPersonID = @Original_RegistrationPersonID OR @Original_RegistrationPerson" +
				"ID IS NULL AND RegistrationPersonID IS NULL) AND (RemainderQuantity = @Original_" +
				"RemainderQuantity OR @Original_RemainderQuantity IS NULL AND RemainderQuantity I" +
				"S NULL) AND (SuitabilityQuantity = @Original_SuitabilityQuantity OR @Original_Su" +
				"itabilityQuantity IS NULL AND SuitabilityQuantity IS NULL) AND (TotalCost = @Ori" +
				"ginal_TotalCost OR @Original_TotalCost IS NULL AND TotalCost IS NULL) AND (Total" +
				"ReceiveingOrderQuantity = @Original_TotalReceiveingOrderQuantity OR @Original_To" +
				"talReceiveingOrderQuantity IS NULL AND TotalReceiveingOrderQuantity IS NULL) AND" +
				" (UnInspectionQuantity = @Original_UnInspectionQuantity OR @Original_UnInspectio" +
				"nQuantity IS NULL AND UnInspectionQuantity IS NULL) AND (UpdatingDate = @Origina" +
				"l_UpdatingDate OR @Original_UpdatingDate IS NULL AND UpdatingDate IS NULL) AND (" +
				"UpdatingPerson = @Original_UpdatingPerson OR @Original_UpdatingPerson IS NULL AN" +
				"D UpdatingPerson IS NULL) AND (UpdatingPersonID = @Original_UpdatingPersonID OR " +
				"@Original_UpdatingPersonID IS NULL AND UpdatingPersonID IS NULL) AND (VolumNum =" +
				" @Original_VolumNum OR @Original_VolumNum IS NULL AND VolumNum IS NULL)";
			this.sqlDeleteCommand1.Connection = this.sqlConnection1;
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BusinessRegistrationNum", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BusinessRegistrationNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CompanyName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryPlace", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryPlace", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate1", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate1", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate2", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate2", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate3", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate3", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate4", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate4", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate5", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate5", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestQuantity1", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity1", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestQuantity2", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity2", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestQuantity3", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity3", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestQuantity4", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity4", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestQuantity5", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "DeliveryRequestQuantity5", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemDrawNum", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemName", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.VarChar, 30, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OrderNum", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OrderNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OutStorehouseQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OutStorehouseQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductionRequestDivision", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductionRequestDivision", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressCondition", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProgressCondition", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_PropertyClassification", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "PropertyClassification", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPerson", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPerson", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPersonID", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RemainderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "RemainderQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SuitabilityQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "SuitabilityQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TotalCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalCost", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UnInspectionQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "UnInspectionQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPerson", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPerson", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPersonID", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_VolumNum", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "VolumNum", System.Data.DataRowVersion.Original, null));
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.DeleteCommand = this.sqlDeleteCommand1;
			this.sqlDataAdapter1.InsertCommand = this.sqlInsertCommand1;
			this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "RO_HT", new System.Data.Common.DataColumnMapping[] {
																																																			   new System.Data.Common.DataColumnMapping("ItemNum", "ItemNum"),
																																																			   new System.Data.Common.DataColumnMapping("ItemDrawNum", "ItemDrawNum"),
																																																			   new System.Data.Common.DataColumnMapping("ItemName", "ItemName"),
																																																			   new System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"),
																																																			   new System.Data.Common.DataColumnMapping("BusinessRegistrationNum", "BusinessRegistrationNum"),
																																																			   new System.Data.Common.DataColumnMapping("PropertyClassification", "PropertyClassification"),
																																																			   new System.Data.Common.DataColumnMapping("ProductionRequestDivision", "ProductionRequestDivision"),
																																																			   new System.Data.Common.DataColumnMapping("ReceivingOrderDate", "ReceivingOrderDate"),
																																																			   new System.Data.Common.DataColumnMapping("ApplyUnitCost", "ApplyUnitCost"),
																																																			   new System.Data.Common.DataColumnMapping("DeliveryRequestQuantity1", "DeliveryRequestQuantity1"),
																																																			   new System.Data.Common.DataColumnMapping("DeliveryRequestDate1", "DeliveryRequestDate1"),
																																																			   new System.Data.Common.DataColumnMapping("DeliveryRequestQuantity2", "DeliveryRequestQuantity2"),
																																																			   new System.Data.Common.DataColumnMapping("DeliveryRequestDate2", "DeliveryRequestDate2"),
																																																			   new System.Data.Common.DataColumnMapping("DeliveryRequestQuantity3", "DeliveryRequestQuantity3"),
																																																			   new System.Data.Common.DataColumnMapping("DeliveryRequestDate3", "DeliveryRequestDate3"),
																																																			   new System.Data.Common.DataColumnMapping("DeliveryRequestQuantity4", "DeliveryRequestQuantity4"),
																																																			   new System.Data.Common.DataColumnMapping("DeliveryRequestDate4", "DeliveryRequestDate4"),
																																																			   new System.Data.Common.DataColumnMapping("DeliveryRequestQuantity5", "DeliveryRequestQuantity5"),
																																																			   new System.Data.Common.DataColumnMapping("DeliveryRequestDate5", "DeliveryRequestDate5"),
																																																			   new System.Data.Common.DataColumnMapping("TotalReceiveingOrderQuantity", "TotalReceiveingOrderQuantity"),
																																																			   new System.Data.Common.DataColumnMapping("TotalCost", "TotalCost"),
																																																			   new System.Data.Common.DataColumnMapping("OutStorehouseQuantity", "OutStorehouseQuantity"),
																																																			   new System.Data.Common.DataColumnMapping("SuitabilityQuantity", "SuitabilityQuantity"),
																																																			   new System.Data.Common.DataColumnMapping("UnInspectionQuantity", "UnInspectionQuantity"),
																																																			   new System.Data.Common.DataColumnMapping("RemainderQuantity", "RemainderQuantity"),
																																																			   new System.Data.Common.DataColumnMapping("OrderNum", "OrderNum"),
																																																			   new System.Data.Common.DataColumnMapping("DeliveryPlace", "DeliveryPlace"),
																																																			   new System.Data.Common.DataColumnMapping("VolumNum", "VolumNum"),
																																																			   new System.Data.Common.DataColumnMapping("ProgressCondition", "ProgressCondition"),
																																																			   new System.Data.Common.DataColumnMapping("RegistrationPerson", "RegistrationPerson"),
																																																			   new System.Data.Common.DataColumnMapping("RegistrationPersonID", "RegistrationPersonID"),
																																																			   new System.Data.Common.DataColumnMapping("RegistrationDate", "RegistrationDate"),
																																																			   new System.Data.Common.DataColumnMapping("UpdatingPerson", "UpdatingPerson"),
																																																			   new System.Data.Common.DataColumnMapping("UpdatingPersonID", "UpdatingPersonID"),
																																																			   new System.Data.Common.DataColumnMapping("UpdatingDate", "UpdatingDate"),
																																																			   new System.Data.Common.DataColumnMapping("ReceivingOrderHistoryIndex", "ReceivingOrderHistoryIndex")})});
			this.sqlDataAdapter1.UpdateCommand = this.sqlUpdateCommand1;
			// 
			// dsRO1
			// 
			this.dsRO1.DataSetName = "dsRO";
			this.dsRO1.Locale = new System.Globalization.CultureInfo("ko-KR");
			// 
			// sqlDataAdapter2
			// 
			this.sqlDataAdapter2.DeleteCommand = this.sqlDeleteCommand2;
			this.sqlDataAdapter2.InsertCommand = this.sqlInsertCommand2;
			this.sqlDataAdapter2.SelectCommand = this.sqlSelectCommand2;
			this.sqlDataAdapter2.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "ROErr_HT", new System.Data.Common.DataColumnMapping[] {
																																																				  new System.Data.Common.DataColumnMapping("ItemNum", "ItemNum"),
																																																				  new System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"),
																																																				  new System.Data.Common.DataColumnMapping("BusinessRegistrationNum", "BusinessRegistrationNum"),
																																																				  new System.Data.Common.DataColumnMapping("ReceivingOrderDate", "ReceivingOrderDate"),
																																																				  new System.Data.Common.DataColumnMapping("ApplyUnitCost", "ApplyUnitCost"),
																																																				  new System.Data.Common.DataColumnMapping("DeliveryRequestDate", "DeliveryRequestDate"),
																																																				  new System.Data.Common.DataColumnMapping("TotalReceiveingOrderQuantity", "TotalReceiveingOrderQuantity"),
																																																				  new System.Data.Common.DataColumnMapping("OrderNum", "OrderNum"),
																																																				  new System.Data.Common.DataColumnMapping("DeliveryPlace", "DeliveryPlace"),
																																																				  new System.Data.Common.DataColumnMapping("ErrorReason", "ErrorReason"),
																																																				  new System.Data.Common.DataColumnMapping("ReceivingOrderErrorHistoryIndex", "ReceivingOrderErrorHistoryIndex")})});
			this.sqlDataAdapter2.UpdateCommand = this.sqlUpdateCommand2;
			// 
			// sqlDeleteCommand2
			// 
			this.sqlDeleteCommand2.CommandText = @"DELETE FROM ROErr_HT WHERE (ReceivingOrderErrorHistoryIndex = @Original_ReceivingOrderErrorHistoryIndex) AND (ApplyUnitCost = @Original_ApplyUnitCost OR @Original_ApplyUnitCost IS NULL AND ApplyUnitCost IS NULL) AND (BusinessRegistrationNum = @Original_BusinessRegistrationNum OR @Original_BusinessRegistrationNum IS NULL AND BusinessRegistrationNum IS NULL) AND (CompanyName = @Original_CompanyName OR @Original_CompanyName IS NULL AND CompanyName IS NULL) AND (DeliveryPlace = @Original_DeliveryPlace OR @Original_DeliveryPlace IS NULL AND DeliveryPlace IS NULL) AND (DeliveryRequestDate = @Original_DeliveryRequestDate OR @Original_DeliveryRequestDate IS NULL AND DeliveryRequestDate IS NULL) AND (ErrorReason = @Original_ErrorReason OR @Original_ErrorReason IS NULL AND ErrorReason IS NULL) AND (ItemNum = @Original_ItemNum OR @Original_ItemNum IS NULL AND ItemNum IS NULL) AND (OrderNum = @Original_OrderNum OR @Original_OrderNum IS NULL AND OrderNum IS NULL) AND (ReceivingOrderDate = @Original_ReceivingOrderDate OR @Original_ReceivingOrderDate IS NULL AND ReceivingOrderDate IS NULL) AND (TotalReceiveingOrderQuantity = @Original_TotalReceiveingOrderQuantity OR @Original_TotalReceiveingOrderQuantity IS NULL AND TotalReceiveingOrderQuantity IS NULL)";
			this.sqlDeleteCommand2.Connection = this.sqlConnection1;
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderErrorHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderErrorHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BusinessRegistrationNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BusinessRegistrationNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CompanyName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryPlace", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryPlace", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ErrorReason", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ErrorReason", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OrderNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OrderNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Original, null));
			// 
			// sqlInsertCommand2
			// 
			this.sqlInsertCommand2.CommandText = @"INSERT INTO ROErr_HT(ItemNum, CompanyName, BusinessRegistrationNum, ReceivingOrderDate, ApplyUnitCost, DeliveryRequestDate, TotalReceiveingOrderQuantity, OrderNum, DeliveryPlace, ErrorReason) VALUES (@ItemNum, @CompanyName, @BusinessRegistrationNum, @ReceivingOrderDate, @ApplyUnitCost, @DeliveryRequestDate, @TotalReceiveingOrderQuantity, @OrderNum, @DeliveryPlace, @ErrorReason); SELECT ItemNum, CompanyName, BusinessRegistrationNum, ReceivingOrderDate, ApplyUnitCost, DeliveryRequestDate, TotalReceiveingOrderQuantity, OrderNum, DeliveryPlace, ErrorReason, ReceivingOrderErrorHistoryIndex FROM ROErr_HT WHERE (ReceivingOrderErrorHistoryIndex = @@IDENTITY)";
			this.sqlInsertCommand2.Connection = this.sqlConnection1;
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.NVarChar, 50, "ItemNum"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar, 50, "CompanyName"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BusinessRegistrationNum", System.Data.SqlDbType.NVarChar, 50, "BusinessRegistrationNum"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, "ReceivingOrderDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderNum", System.Data.SqlDbType.NVarChar, 50, "OrderNum"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryPlace", System.Data.SqlDbType.NVarChar, 50, "DeliveryPlace"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ErrorReason", System.Data.SqlDbType.NVarChar, 500, "ErrorReason"));
			// 
			// sqlSelectCommand2
			// 
			this.sqlSelectCommand2.CommandText = "SELECT ItemNum, CompanyName, BusinessRegistrationNum, ReceivingOrderDate, ApplyUn" +
				"itCost, DeliveryRequestDate, TotalReceiveingOrderQuantity, OrderNum, DeliveryPla" +
				"ce, ErrorReason, ReceivingOrderErrorHistoryIndex FROM ROErr_HT";
			this.sqlSelectCommand2.Connection = this.sqlConnection1;
			// 
			// sqlUpdateCommand2
			// 
			this.sqlUpdateCommand2.CommandText = "UPDATE ROErr_HT SET ItemNum = @ItemNum, CompanyName = @CompanyName, BusinessRegis" +
				"trationNum = @BusinessRegistrationNum, ReceivingOrderDate = @ReceivingOrderDate," +
				" ApplyUnitCost = @ApplyUnitCost, DeliveryRequestDate = @DeliveryRequestDate, Tot" +
				"alReceiveingOrderQuantity = @TotalReceiveingOrderQuantity, OrderNum = @OrderNum," +
				" DeliveryPlace = @DeliveryPlace, ErrorReason = @ErrorReason WHERE (ReceivingOrde" +
				"rErrorHistoryIndex = @Original_ReceivingOrderErrorHistoryIndex) AND (ApplyUnitCo" +
				"st = @Original_ApplyUnitCost OR @Original_ApplyUnitCost IS NULL AND ApplyUnitCos" +
				"t IS NULL) AND (BusinessRegistrationNum = @Original_BusinessRegistrationNum OR @" +
				"Original_BusinessRegistrationNum IS NULL AND BusinessRegistrationNum IS NULL) AN" +
				"D (CompanyName = @Original_CompanyName OR @Original_CompanyName IS NULL AND Comp" +
				"anyName IS NULL) AND (DeliveryPlace = @Original_DeliveryPlace OR @Original_Deliv" +
				"eryPlace IS NULL AND DeliveryPlace IS NULL) AND (DeliveryRequestDate = @Original" +
				"_DeliveryRequestDate OR @Original_DeliveryRequestDate IS NULL AND DeliveryReques" +
				"tDate IS NULL) AND (ErrorReason = @Original_ErrorReason OR @Original_ErrorReason" +
				" IS NULL AND ErrorReason IS NULL) AND (ItemNum = @Original_ItemNum OR @Original_" +
				"ItemNum IS NULL AND ItemNum IS NULL) AND (OrderNum = @Original_OrderNum OR @Orig" +
				"inal_OrderNum IS NULL AND OrderNum IS NULL) AND (ReceivingOrderDate = @Original_" +
				"ReceivingOrderDate OR @Original_ReceivingOrderDate IS NULL AND ReceivingOrderDat" +
				"e IS NULL) AND (TotalReceiveingOrderQuantity = @Original_TotalReceiveingOrderQua" +
				"ntity OR @Original_TotalReceiveingOrderQuantity IS NULL AND TotalReceiveingOrder" +
				"Quantity IS NULL); SELECT ItemNum, CompanyName, BusinessRegistrationNum, Receivi" +
				"ngOrderDate, ApplyUnitCost, DeliveryRequestDate, TotalReceiveingOrderQuantity, O" +
				"rderNum, DeliveryPlace, ErrorReason, ReceivingOrderErrorHistoryIndex FROM ROErr_" +
				"HT WHERE (ReceivingOrderErrorHistoryIndex = @ReceivingOrderErrorHistoryIndex)";
			this.sqlUpdateCommand2.Connection = this.sqlConnection1;
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.NVarChar, 50, "ItemNum"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar, 50, "CompanyName"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BusinessRegistrationNum", System.Data.SqlDbType.NVarChar, 50, "BusinessRegistrationNum"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, "ReceivingOrderDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderNum", System.Data.SqlDbType.NVarChar, 50, "OrderNum"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryPlace", System.Data.SqlDbType.NVarChar, 50, "DeliveryPlace"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ErrorReason", System.Data.SqlDbType.NVarChar, 500, "ErrorReason"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderErrorHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderErrorHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BusinessRegistrationNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BusinessRegistrationNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CompanyName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryPlace", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryPlace", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ErrorReason", System.Data.SqlDbType.NVarChar, 500, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ErrorReason", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OrderNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OrderNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceivingOrderErrorHistoryIndex", System.Data.SqlDbType.Int, 4, "ReceivingOrderErrorHistoryIndex"));
			// 
			// dsROErr1
			// 
			this.dsROErr1.DataSetName = "dsROErr";
			this.dsROErr1.Locale = new System.Globalization.CultureInfo("ko-KR");
			this.Load += new System.EventHandler(this.Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.dsRO1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsROErr1)).EndInit();

		}
		#endregion

		/// <summary>
		/// 등록버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Register_Click(object sender, System.EventArgs e)
		{
			int count1 = 2;
			decimal ApplyCost = 0;
			if(Find.Value != "")
			{
				string FileName = System.IO.Path.GetDirectoryName(Find.PostedFile.FileName) + System.IO.Path.GetFileName(Find.PostedFile.FileName);
				
				if(FileName.ToString().Substring(FileName.ToString().Length - 3) == "xls" || FileName.ToString().Substring(FileName.ToString().Length - 3) == "XLS")
				{
					string upLoadFile = Path.GetTempFileName();
					Find.PostedFile.SaveAs(upLoadFile);

					OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + upLoadFile + ";Extended Properties=Excel 8.0;");
			
					conn.Open();
			
					OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", conn);
						
					DataSet dsExcel = new DataSet();
					adapter.Fill(dsExcel);
					conn.Close();
			
					//기존자료 삭제
					SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
					SqlCommand comm = new SqlCommand();
					comm.Connection = con;
					comm.CommandText = "ItemErr";
					comm.CommandType = CommandType.StoredProcedure;
					con.Open();
					comm.ExecuteNonQuery();
					con.Close();

					sqlDataAdapter1.Fill(dsRO1);
					sqlDataAdapter2.Fill(dsROErr1);
					int volum = vol();
					sqlConnection1.Open();
					SqlTransaction tr = sqlConnection1.BeginTransaction();
					sqlInsertCommand1.Connection = sqlConnection1;
					sqlInsertCommand1.Transaction = tr;
					sqlInsertCommand2.Connection = sqlConnection1;
					sqlInsertCommand2.Transaction = tr;
					try
					{
						string ErrorMessage = "";
						int Errorcount = 0;
						bool state = true;

						if(dsExcel.Tables[0].Rows.Count == 0)
						{
							throw new Exception("업로드 할 엑셀자료가 없습니다!");
						}
                        					
						for(int i = 0; i < dsExcel.Tables[0].Rows.Count; i++)
						{

							state = true;
							dsRO.RO_HTRow row = dsRO1.RO_HT.NewRO_HTRow();
							if(!Comparison(tr,dsExcel.Tables[0].Rows[i]["사업자등록번호"].ToString()))
							{
								state = false;
								//throw new Exception((i+2)+"행 "+dsExcel.Tables[0].Rows[i]["품목명"].ToString() + "품목의 거래처가 존재하지 않습니다!");
								ErrorMessage = (i+2)+"행 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString() + "품목의 거래처가 존재하지 않습니다!";
								goto Add;
							}

							//품목번호...
							if(dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() == "")
							{
								state = false;
								//throw new Exception((i+2)+"행의 품목번호가 없습니다!");
								ErrorMessage = (i+2)+"행의 품목번호가 없습니다!";
								goto Add;
							}
							else
							{
								if(Exist(dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim()))
									row.ItemNum = dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim();
								else
								{
									state = false;
									//throw new Exception((i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"은 존재하지 않습니다!");
									ErrorMessage = (i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"은 존재하지 않습니다!";
									goto Add;
								}
							}

							//도면번호
							row.ItemDrawNum = SearchDrawNum(tr,dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim());
							//품목명
							row.ItemName = SearchName(tr,dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim());

							//거래처명...
							row.CompanyName = Company(tr, dsExcel.Tables[0].Rows[i]["사업자등록번호"].ToString().Trim());
							//사업자등록번호
							row.BusinessRegistrationNum = dsExcel.Tables[0].Rows[i]["사업자등록번호"].ToString().Trim();
							//자산분류
							if(Division(dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim()) == "상품")
							{
								row.PropertyClassification = "상품";
								row.ProductionRequestDivision = false;
							}
							else if(Division(dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim()) == "제품")
							{
								row.PropertyClassification = "제품";
								if(dsExcel.Tables[0].Rows[i]["생산의뢰여부"].ToString().Trim() == "예")
									row.ProductionRequestDivision = true ;
								else
									row.ProductionRequestDivision = false;

							}
							else
							{
								row.PropertyClassification = Division(dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim());
								row.ProductionRequestDivision = false;
								//state = false;
								//ErrorMessage = (i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"은 자산분류가 제품이나 상품이 아닙니다!";
								//goto Add;
							}
	
							if(dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Length == 8)
							{
								//수주일자...
								string year = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(0,4).ToString();
								string month = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(4,2).ToString();
								string day = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(6,2).ToString();
								string Date = year+"-"+month+"-"+day;
								row.ReceivingOrderDate = Convert.ToDateTime(Date);

								//적용단가...
								if(dsExcel.Tables[0].Rows[i]["적용단가"].ToString().Trim() != "")
								{
									row.ApplyUnitCost = Decimal.Parse(dsExcel.Tables[0].Rows[i]["적용단가"].ToString().Trim());
									ApplyCost = Decimal.Parse(dsExcel.Tables[0].Rows[i]["적용단가"].ToString().Trim());
								}
								else
								{
									ApplyCost = UnitCost(row.BusinessRegistrationNum,row.ItemNum);

									if( ApplyCost == -100)
									{
										state = false;
										ErrorMessage = (i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"의 단가가 존재하지 않습니다!";
										goto Add;
									}
									else
									{
										row.ApplyUnitCost = ApplyCost;
										//적용단가가 없으면 판매단가에서 가져와 넣어주는 것으로 수정
										//state = false;
										//ErrorMessage = (i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"의 적용단가를 입력해주세요!";
										//goto Add;
									}
								}

								//납입요청일1,납일요청수량1...
								year = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(0,4).ToString();
								month = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(4,2).ToString();
								day = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(6,2).ToString();
								Date = year+"-"+month+"-"+day;
								row.DeliveryRequestDate1 = Convert.ToDateTime(Date);

								if(dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim() != "")
									row.DeliveryRequestQuantity1 = decimal.Parse(dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim());
								else
								{
									state = false;
									ErrorMessage = (i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"의 발주량을 입력해주세요!";
									goto Add;
									//row.DeliveryRequestQuantity1 = 0;
								}
								
								//총수주량...
								row.TotalReceiveingOrderQuantity = decimal.Parse(row.DeliveryRequestQuantity1.ToString());
								//총금액...
								row.TotalCost = decimal.Parse(row.DeliveryRequestQuantity1.ToString()) * ApplyCost;
							
								//미납량...
								row.RemainderQuantity = decimal.Parse(dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim());
								//출고수량...
								row.OutStorehouseQuantity = 0;
								//합격수량...
								row.SuitabilityQuantity = 0;
								//부적합수량...
								row.UnInspectionQuantity = 0;

								//발주번호...
								row.OrderNum = dsExcel.Tables[0].Rows[i]["발주번호"].ToString().Trim();
								//납품장소...
								row.DeliveryPlace = dsExcel.Tables[0].Rows[i]["납품장소"].ToString().Trim();
								//볼륨번호
								row.VolumNum = volum;
								//진행상태...
								row.ProgressCondition = "대기";
								//등록자...
								row.RegistrationPerson = Session["UserName"].ToString();
								//등록자ID
								row.RegistrationPersonID = Session["ID"].ToString();
								//등록자...
								row.RegistrationDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
							
							}
							else
							{



								//수주일자...
								row.ReceivingOrderDate = Convert.ToDateTime(dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim());
								
								//적용단가...
								if(dsExcel.Tables[0].Rows[i]["적용단가"].ToString().Trim() != "")
								{
									row.ApplyUnitCost = Decimal.Parse(dsExcel.Tables[0].Rows[i]["적용단가"].ToString().Trim());
									ApplyCost = Decimal.Parse(dsExcel.Tables[0].Rows[i]["적용단가"].ToString().Trim());									
								}
								else
								{
									ApplyCost = UnitCost(row.BusinessRegistrationNum,row.ItemNum);

									if( ApplyCost == -100)
									{
										state = false;
										ErrorMessage = (i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"의 단가가 존재하지 않습니다!";
										goto Add;
									}
									else
									{
										row.ApplyUnitCost = ApplyCost;
										//적용단가가 없으면 판매단가에서 가져와 넣어주는 것으로 수정
										//state = false;
										//ErrorMessage = (i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"의 적용단가를 입력해주세요!";
										//goto Add;
									}
								}

								//납기요구일
								row.DeliveryRequestDate1 =  Convert.ToDateTime(dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim());

								if(dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim() != "")
									row.DeliveryRequestQuantity1 = decimal.Parse(dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim());
								else
								{
									state = false;
									ErrorMessage = (i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"의 발주량을 입력해주세요!";
									goto Add;
									//row.DeliveryRequestQuantity1 = 0;
								}
								
								//총수주량...
								row.TotalReceiveingOrderQuantity = decimal.Parse(row.DeliveryRequestQuantity1.ToString());
								//총금액...
								row.TotalCost = decimal.Parse(row.DeliveryRequestQuantity1.ToString()) * ApplyCost;
								//잔량
								row.RemainderQuantity = decimal.Parse(dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim());
								//출고수량...
								row.OutStorehouseQuantity = 0;
								//합격수량...
								row.SuitabilityQuantity = 0;
								//부적합수량...
								row.UnInspectionQuantity = 0;

								//발주번호...
								row.OrderNum = dsExcel.Tables[0].Rows[i]["발주번호"].ToString().Trim();
								//납품장소...
								row.DeliveryPlace = dsExcel.Tables[0].Rows[i]["납품장소"].ToString().Trim();
								//볼륨번호
								row.VolumNum = volum;
								//진행상태...
								row.ProgressCondition = "대기";
								//등록자...
								row.RegistrationPerson = Session["UserName"].ToString();
								//등록자ID
								row.RegistrationPersonID = Session["ID"].ToString();
								//등록자...
								row.RegistrationDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
							}
							
							if(dsExcel.Tables[0].Rows[i]["발주번호"].ToString().Trim() != "")
							{
								for(int count = 0 ; count < dsRO1.RO_HT.Rows.Count; count++)
								{
								
								
									if(dsRO1.RO_HT.Rows[count]["OrderNum"].ToString().Trim() == dsExcel.Tables[0].Rows[i]["발주번호"].ToString().Trim() && dsRO1.RO_HT.Rows[count]["ItemNum"].ToString().Trim() == dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() )
									{
										state = false;
										//throw new Exception("업로드하려는 엑셀의 "+count+"행과 "+i+"행의 발주번호가 동일합니다!");
										ErrorMessage = "업로드하려는 엑셀의 "+(i+2)+"행이 지금 등록하려는 엑셀자료 "+(count+2)+"행의 발주번호가 동일합니다!";
										goto Add;
									}

								}
								if(ReceiveingValidate(dsExcel.Tables[0].Rows[i]["발주번호"].ToString().Trim(),dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim(),tr))
								{
									state = false;
									//throw new Exception((i+2)+"행과 동일한 발주번호가 이미 존재합니다!");
									ErrorMessage ="기존 자료에 "+(i+2)+"행과 동일한 발주번호가 이미 존재합니다!";
									goto Add;
								}
							}
							

						Add:
							if(state)
								dsRO1.RO_HT.AddRO_HTRow(row);
							else
							{
								dsROErr.ROErr_HTRow Erow  = dsROErr1.ROErr_HT.NewROErr_HTRow();
								Erow["ItemNum"] = dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim();
								Erow["CompanyName"] = dsExcel.Tables[0].Rows[i]["거래처명"].ToString().Trim();
								Erow["BusinessRegistrationNum"] = dsExcel.Tables[0].Rows[i]["사업자등록번호"].ToString().Trim();
								if(dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Length == 8)
								{
									//수주일자...
									string year = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(0,4).ToString();
									string month = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(4,2).ToString();
									string day = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(6,2).ToString();
									string Date = year+"-"+month+"-"+day;
									Erow["ReceivingOrderDate"] = Convert.ToDateTime(Date);
								}
								else if(dsExcel.Tables[0].Rows[i]["수주일자"].ToString() == null || dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim() == "")
									Erow["ReceivingOrderDate"] = Convert.DBNull;
								else
									Erow["ReceivingOrderDate"] = DateTime.Parse(dsExcel.Tables[0].Rows[i]["수주일자"].ToString()).ToShortDateString();

								if(dsExcel.Tables[0].Rows[i]["적용단가"].ToString() == null || dsExcel.Tables[0].Rows[i]["적용단가"].ToString().Trim() == "")
								{
									if(ApplyUnitCost(tr, dsExcel.Tables[0].Rows[i]["사업자등록번호"].ToString().Trim(), dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim()) == -100)
										Erow["ApplyUnitCost"] = Convert.DBNull;
									else
										Erow["ApplyUnitCost"] = ApplyUnitCost(tr, dsExcel.Tables[0].Rows[i]["사업자등록번호"].ToString().Trim(), dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim());									
								}
								else
									Erow["ApplyUnitCost"] = decimal.Parse(dsExcel.Tables[0].Rows[i]["적용단가"].ToString());

								if(dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Length == 8)
								{
									//수주일자...
									string year = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(0,4).ToString();
									string month = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(4,2).ToString();
									string day = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(6,2).ToString();
									string Date = year+"-"+month+"-"+day;
									Erow["DeliveryRequestDate"] = Convert.ToDateTime(Date);
								}
								else if(dsExcel.Tables[0].Rows[i]["납기요구일"].ToString() == null || dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim() == "")
									Erow["DeliveryRequestDate"] = Convert.DBNull;
								else
									Erow["DeliveryRequestDate"] = DateTime.Parse(dsExcel.Tables[0].Rows[i]["납기요구일"].ToString()).ToShortDateString();

								if(dsExcel.Tables[0].Rows[i]["총발주량"].ToString() == null || dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim() == "")
									Erow["TotalReceiveingOrderQuantity"] = Convert.DBNull;
								else
									Erow["TotalReceiveingOrderQuantity"] = decimal.Parse(dsExcel.Tables[0].Rows[i]["총발주량"].ToString());

								Erow["OrderNum"] = dsExcel.Tables[0].Rows[i]["발주번호"].ToString().Trim();
								Erow["DeliveryPlace"] = dsExcel.Tables[0].Rows[i]["납품장소"].ToString().Trim();

								Erow["ErrorReason"] = ErrorMessage;
								
								dsROErr1.ROErr_HT.AddROErr_HTRow(Erow);
								Errorcount++;

								sqlDataAdapter2.Update(dsROErr1);

							}

							count1++;
							sqlDataAdapter1.Update(dsRO1);
						}

						//						con.Open();
						//						comm.CommandText = "select count(*) From ROErr_HT";
						//						int recordcount = int.Parse(comm.ExecuteScalar().ToString());
						//						con.Close();
						//						
						//sqlDataAdapter1.Update(dsRO1);
						
						
						

						UltraWebGrid1.Visible = true;
						UltraWebGrid2.Visible = false;
						

						if(Errorcount == 0)
						{
							SqlCommand cmd = new SqlCommand();
							cmd.Connection = sqlConnection1;
							cmd.Transaction = tr;
							cmd.CommandText = @"Select isnull(SmallClassificationName,'') as ItemState, RO_HT.* 
											From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
											left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
											where II_MT.RecodingState = 1 and  VolumNum = (Select Max(VolumNum) From RO_HT)";						
							SqlDataReader dr = cmd.ExecuteReader();
							
							UltraWebGrid1.DataSource = dr;
							UltraWebGrid1.DataBind();

							Response.Write("<script language=javascript>");
							Response.Write("alert('모든 엑셀자료가 올바르게 업로드 되었습니다.');");
							Response.Write("</script>");
						}
						else if(Errorcount == dsExcel.Tables[0].Rows.Count)
						{
							Response.Write("<script language=javascript>");
							Response.Write("alert('엑셀자료의 모든 행이 에러입니다!!');");
							Response.Write("</script>");
						}
						else
						{
							int rightcount = dsExcel.Tables[0].Rows.Count - Errorcount;
							SqlCommand cmd = new SqlCommand();
							cmd.Connection = sqlConnection1;
							cmd.Transaction = tr;
							cmd.CommandText = @"Select isnull(SmallClassificationName,'') as ItemState, RO_HT.* 
											From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
											left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
											where II_MT.RecodingState = 1 and  VolumNum = (Select Max(VolumNum) From RO_HT)";						
							SqlDataReader dr = cmd.ExecuteReader();
							
							UltraWebGrid1.DataSource = dr;
							UltraWebGrid1.DataBind();

							Response.Write("<script language=javascript>");
							Response.Write("alert('"+Errorcount+"개의 자료를 제외한 "+rightcount+"개의 엑셀자료가 업로드 되었습니다.');");
							Response.Write("</script>");
						}

						dsExcel.Clear();
						tr.Commit();

					}
					catch(Exception ee)
					{


						Response.Write("<script language=javascript>");
						Response.Write("alert('"+ee.Message+" "+count1+"');");
						Response.Write("</script>");
						count1 = count1;
						tr.Rollback();
					}
					finally
					{
						File.Delete(upLoadFile);
						//con.Close();
						sqlConnection1.Close();
					}
					
					
				}
				else
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('엑셀형식의 자료가 아닙니다.');");
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

		private bool ReceiveingValidate(string Order, string ItemNum ,SqlTransaction trans)
		{

			int Exist = 0;
			SqlCommand comm = new SqlCommand();
			comm.Connection = sqlConnection1;
			comm.Transaction = trans;
			comm.CommandText = @"Select count(OrderNum) From RO_HT where OrderNum = @OrderNum and ItemNum = @ItemNum";
			//발주번호가 동일한 것이 있는지 파악
			comm.Parameters.Add("@OrderNum", Order);
			comm.Parameters.Add("@ItemNum", Order);
			Exist = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			
			if(Exist != 0)
				return true;
			else
				return false;
			
		}

		private int vol()
		{
			int vol;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str  ="Select Max(VolumNum) From RO_HT";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.ExecuteScalar();
			if(Convert.IsDBNull(comm.ExecuteScalar()))
				vol = 0;
			else 
				vol = int.Parse(comm.ExecuteScalar().ToString());

			return (vol+1);
		}


		private string Company(SqlTransaction trans,string num)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = sqlConnection1;
			comm.Transaction = trans;

			string ComNum = "";

			string str = "select CompanyName from CI_MT where RecodingState = 1 and BusinessRegistrationNum = @num";
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = num;
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ComNum = dr["CompanyName"].ToString();
			}
			dr.Close();

			return ComNum;
		}


		/// <summary>
		/// 거래처명과 사업자번호가 동일한지 파악
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="trans"></param>
		/// <param name="com"></param>
		/// <param name="comnum"></param>
		/// <returns></returns>
		private bool Comparison(SqlTransaction trans,string comnum)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = sqlConnection1;
			comm.Transaction = trans;

			bool Equal = false;

			string str = "select * from CI_MT where RecodingState = 1 and BusinessRegistrationNum = @num";
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = comnum;
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Equal = true;
			}
			dr.Close();

			return Equal;

		}

		private string SearchDrawNum(SqlTransaction trans,string itemnum)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = sqlConnection1;
			comm.Transaction = trans;

			string DrawNum = "";

			string str = "select ITemDrawNum from II_MT where RecodingState = 1 and ItemNum = @num";//CompanyName = @name and BusinessRegistrationNum = @num";
			comm.Parameters.Add("@num",itemnum);
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				DrawNum = dr["ItemDrawNum"].ToString();;
			}
			dr.Close();

			return DrawNum;
		}

		private string SearchName(SqlTransaction trans,string itemnum)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = sqlConnection1;
			comm.Transaction = trans;

			string Name = "";

			string str = "select ItemName from II_MT where RecodingState = 1 and ItemNum = @num";//CompanyName = @name and BusinessRegistrationNum = @num";
			comm.Parameters.Add("@num",itemnum);
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Name = dr["ItemName"].ToString();
			}
			dr.Close();

			return Name;
		}


		private string Division(string itemnum)
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",itemnum);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			conn.Close();

			if(aa == "")
				throw new Exception(itemnum + "은 해당 품목이 존재하지 않습니다!");
			else
				return aa;
			
		}
		

		private bool Exist(string itemnum)
		{
			int count=0;
			bool Equal = false;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select count(ItemNum) From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",itemnum);
			count = int.Parse(comm.ExecuteScalar().ToString());
			
			if(count == 1)
				Equal = true;

			return Equal;
			
		}

		private decimal UnitCost(string BusinessRegistrationNum, string ItemNum)
		{
			decimal Unit = -100;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select StandardUnitCost From UCI_MT Where RecodingState = 1 and ItemNum = @num and BusinessRegistrationNum = @BusinessRegistrationNum and UnitCostDistinction = '판매단가'";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",ItemNum);
			comm.Parameters.Add("@BusinessRegistrationNum", BusinessRegistrationNum);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
				Unit = decimal.Parse(dr["StandardUnitCost"].ToString());

			conn.Close();
			return Unit;
		}

		private void btSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Visible = false;
			UltraWebGrid2.Visible = true;

			UltraWebGrid2.DataSource = Search();
			UltraWebGrid2.DataBind();

		}

		private DataSet Search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = @"Select ROErr_HT.ItemNum,isnull(II_MT.ItemName,'') as ItemName, CompanyName, BusinessRegistrationNum, ReceivingOrderDate, ApplyUnitCost, DeliveryRequestDate, TotalReceiveingOrderQuantity, OrderNum, DeliveryPlace, ErrorReason, ReceivingOrderErrorHistoryIndex
						From ROErr_HT left outer join II_MT on ROErr_HT.ItemNum = II_MT.ItemNum where II_MT.RecodingState = 1 or II_Mt.RecodingState is null ORDER BY ReceivingOrderErrorHistoryIndex";
			SqlCommand comm =  new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);

			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}

		private DataSet Search1()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = @"Select isnull(SmallClassificationName,'') as ItemState, RO_HT.* 
											From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
											left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
											where II_MT.RecodingState = 1 and  VolumNum = (Select Max(VolumNum) From RO_HT)";						
			SqlCommand comm =  new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);

			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Visible)
			{
				if(UltraWebGrid1.Rows.Count == 0)
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('저장할 항목이 없습니다!');");
					Response.Write("</script>");
				}
				else
				{
					UltraWebGrid uwg_RO = new UltraWebGrid();
					uwg_RO = UltraWebGrid1;
					uwg_RO.DisplayLayout.Pager.AllowPaging = false;
					uwg_RO.DataSource = Search1();
					uwg_RO.DataBind();
					UltraWebGridExcelExporter1.Export(uwg_RO);
				}
			}
			else
			{
				UltraWebGrid uwg_RO = new UltraWebGrid();
				uwg_RO = UltraWebGrid2;
				uwg_RO.DisplayLayout.Pager.AllowPaging = false;
				uwg_RO.DataSource = Search();
				uwg_RO.DataBind();
				UltraWebGridExcelExporter1.Export(uwg_RO);

			}
		}

		private void UltraWebGrid2_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid2.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid2.DataSource = Search();
			UltraWebGrid2.DataBind();
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}
		

		private decimal ApplyUnitCost(SqlTransaction trans,string comnum, string itemnum)
		{
			decimal Cost = -100 ;
			SqlCommand comm = new SqlCommand();
			comm.Connection = sqlConnection1;
			comm.Transaction = trans;

			string str = "select StandardUnitCost from UCI_MT where RecodingState = 1 and BusinessRegistrationNum = @num and ItemNum = @item and UnitCostDistinction = '판매단가'";
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = comnum;
			comm.Parameters.Add("@item",SqlDbType.VarChar).Value = itemnum;
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();

			return Cost;
		}
	}
}
