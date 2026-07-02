using System;
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
using System.Configuration;

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// OutSideOutStorehouse에 대한 요약 설명입니다.
	/// </summary>
	public class OutSideOutStorehouse : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebCombo.WebCombo wdcItem;
		protected System.Web.UI.WebControls.Button btnRegister;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgOOS_HT;
		protected System.Web.UI.WebControls.LinkButton lnk;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgOO_HT;
		protected System.Data.SqlClient.SqlDataAdapter OOS_HTAdapter;
		protected System.Web.UI.WebControls.LinkButton preOutStorehouse;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.TextBox txtThisOrderQuantity;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryDate;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		protected System.Data.SqlClient.SqlConnection sqlConnection1;
		protected KIT_ERP.BuyingOutside.dsOOS_HT dsOOS_HT1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;

		
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			sqlConnection1.ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DSN"] ;


			
			ItemSearchControl1.RawMaterials = true; //원자재 바인딩
			ItemSearchControl1.HalfFinishedProducts = true; //반제품 바인딩
			ItemSearchControl1.Products = true; //제품 바인딩
			CSC1.UnitCostDistinction = "외주거래처";

			if(!Page.IsPostBack)
			{
				OOS_HTAdapter.Fill(dsOOS_HT1);
				uwgOOS_HT.DataSource = dsOOS_HT1;
				uwgOOS_HT.DataBind();

				Session["dsOOS_HT1"] = dsOOS_HT1 ;

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
				//	*************************************************
				//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
				//	*************************************************
				//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
				string str_ItemClassification1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0210'" ;
				SqlCommand comm_ItemClassification1 = new SqlCommand(str_ItemClassification1,conn);
				SqlDataAdapter da_ItemClassification1 = new SqlDataAdapter(comm_ItemClassification1) ;
				DataSet ds_ItemClassification1 = new DataSet() ;
				da_ItemClassification1.Fill(ds_ItemClassification1);
				
				ddlItemClassification1.DataSource = ds_ItemClassification1;
				ddlItemClassification1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
				ddlItemClassification1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
				ddlItemClassification1.DataBind();
				ddlItemClassification1.Items.Insert(0, "-선택하세요-") ;
				ddlItemClassification1.Items[0].Value = "";


				txtThisOrderQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				txtThisOrderQuantity.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");
				txtThisOrderQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				txtThisOrderQuantity.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

			}
			else
			{
				dsOOS_HT1= (dsOOS_HT)Session["dsOOS_HT1"] ;
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
			this.OOS_HTAdapter = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
			this.dsOOS_HT1 = new KIT_ERP.BuyingOutside.dsOOS_HT();
			((System.ComponentModel.ISupportInitialize)(this.dsOOS_HT1)).BeginInit();
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.preOutStorehouse.Click += new System.EventHandler(this.preOutStorehouse_Click);
			this.uwgOO_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgOO_HT_PageIndexChanged);
			this.uwgOO_HT.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.uwgOO_HT_DblClick);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
			// 
			// OOS_HTAdapter
			// 
			this.OOS_HTAdapter.DeleteCommand = this.sqlDeleteCommand1;
			this.OOS_HTAdapter.InsertCommand = this.sqlInsertCommand1;
			this.OOS_HTAdapter.SelectCommand = this.sqlSelectCommand1;
			this.OOS_HTAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									new System.Data.Common.DataTableMapping("Table", "OOS_HT", new System.Data.Common.DataColumnMapping[] {
																																																			  new System.Data.Common.DataColumnMapping("ItemNum", "ItemNum"),
																																																			  new System.Data.Common.DataColumnMapping("ItemDrawNum", "ItemDrawNum"),
																																																			  new System.Data.Common.DataColumnMapping("ItemName", "ItemName"),
																																																			  new System.Data.Common.DataColumnMapping("OrderRate", "OrderRate"),
																																																			  new System.Data.Common.DataColumnMapping("ProgressRate", "ProgressRate"),
																																																			  new System.Data.Common.DataColumnMapping("ProcessSequenceNum", "ProcessSequenceNum"),
																																																			  new System.Data.Common.DataColumnMapping("ProcessCode", "ProcessCode"),
																																																			  new System.Data.Common.DataColumnMapping("ProcessName", "ProcessName"),
																																																			  new System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"),
																																																			  new System.Data.Common.DataColumnMapping("BusinessRegistrationNum", "BusinessRegistrationNum"),
																																																			  new System.Data.Common.DataColumnMapping("ThistimeOutStorehouseQuantity", "ThistimeOutStorehouseQuantity"),
																																																			  new System.Data.Common.DataColumnMapping("OutStorehouseDate", "OutStorehouseDate"),
																																																			  new System.Data.Common.DataColumnMapping("ProgressCondition", "ProgressCondition"),
																																																			  new System.Data.Common.DataColumnMapping("RegistrationPerson", "RegistrationPerson"),
																																																			  new System.Data.Common.DataColumnMapping("RegistrationPersonID", "RegistrationPersonID"),
																																																			  new System.Data.Common.DataColumnMapping("RegistrationDate", "RegistrationDate"),
																																																			  new System.Data.Common.DataColumnMapping("UpdatingPerson", "UpdatingPerson"),
																																																			  new System.Data.Common.DataColumnMapping("UpdatingPersonID", "UpdatingPersonID"),
																																																			  new System.Data.Common.DataColumnMapping("UpdatingDate", "UpdatingDate"),
																																																			  new System.Data.Common.DataColumnMapping("OutSideOrderHistoryIndex", "OutSideOrderHistoryIndex"),
																																																			  new System.Data.Common.DataColumnMapping("OutSideOutStorehouseHistoryIndex", "OutSideOutStorehouseHistoryIndex"),
																																																			  new System.Data.Common.DataColumnMapping("ThisOrderQuantity", "ThisOrderQuantity"),
																																																			  new System.Data.Common.DataColumnMapping("ThisDeliveryDate", "ThisDeliveryDate")})});
			this.OOS_HTAdapter.UpdateCommand = this.sqlUpdateCommand1;
			// 
			// sqlDeleteCommand1
			// 
			this.sqlDeleteCommand1.CommandText = "DELETE FROM OOS_HT WHERE (OutSideOutStorehouseHistoryIndex = @Original_OutSideOut" +
				"StorehouseHistoryIndex) AND (BusinessRegistrationNum = @Original_BusinessRegistr" +
				"ationNum OR @Original_BusinessRegistrationNum IS NULL AND BusinessRegistrationNu" +
				"m IS NULL) AND (CompanyName = @Original_CompanyName OR @Original_CompanyName IS " +
				"NULL AND CompanyName IS NULL) AND (ItemDrawNum = @Original_ItemDrawNum OR @Origi" +
				"nal_ItemDrawNum IS NULL AND ItemDrawNum IS NULL) AND (ItemName = @Original_ItemN" +
				"ame OR @Original_ItemName IS NULL AND ItemName IS NULL) AND (ItemNum = @Original" +
				"_ItemNum OR @Original_ItemNum IS NULL AND ItemNum IS NULL) AND (OrderRate = @Ori" +
				"ginal_OrderRate OR @Original_OrderRate IS NULL AND OrderRate IS NULL) AND (OutSi" +
				"deOrderHistoryIndex = @Original_OutSideOrderHistoryIndex OR @Original_OutSideOrd" +
				"erHistoryIndex IS NULL AND OutSideOrderHistoryIndex IS NULL) AND (OutStorehouseD" +
				"ate = @Original_OutStorehouseDate OR @Original_OutStorehouseDate IS NULL AND Out" +
				"StorehouseDate IS NULL) AND (ProcessCode = @Original_ProcessCode OR @Original_Pr" +
				"ocessCode IS NULL AND ProcessCode IS NULL) AND (ProcessName = @Original_ProcessN" +
				"ame OR @Original_ProcessName IS NULL AND ProcessName IS NULL) AND (ProcessSequen" +
				"ceNum = @Original_ProcessSequenceNum OR @Original_ProcessSequenceNum IS NULL AND" +
				" ProcessSequenceNum IS NULL) AND (ProgressCondition = @Original_ProgressConditio" +
				"n OR @Original_ProgressCondition IS NULL AND ProgressCondition IS NULL) AND (Pro" +
				"gressRate = @Original_ProgressRate OR @Original_ProgressRate IS NULL AND Progres" +
				"sRate IS NULL) AND (RegistrationDate = @Original_RegistrationDate OR @Original_R" +
				"egistrationDate IS NULL AND RegistrationDate IS NULL) AND (RegistrationPerson = " +
				"@Original_RegistrationPerson OR @Original_RegistrationPerson IS NULL AND Registr" +
				"ationPerson IS NULL) AND (RegistrationPersonID = @Original_RegistrationPersonID " +
				"OR @Original_RegistrationPersonID IS NULL AND RegistrationPersonID IS NULL) AND " +
				"(ThisDeliveryDate = @Original_ThisDeliveryDate OR @Original_ThisDeliveryDate IS " +
				"NULL AND ThisDeliveryDate IS NULL) AND (ThisOrderQuantity = @Original_ThisOrderQ" +
				"uantity OR @Original_ThisOrderQuantity IS NULL AND ThisOrderQuantity IS NULL) AN" +
				"D (ThistimeOutStorehouseQuantity = @Original_ThistimeOutStorehouseQuantity OR @O" +
				"riginal_ThistimeOutStorehouseQuantity IS NULL AND ThistimeOutStorehouseQuantity " +
				"IS NULL) AND (UpdatingDate = @Original_UpdatingDate OR @Original_UpdatingDate IS" +
				" NULL AND UpdatingDate IS NULL) AND (UpdatingPerson = @Original_UpdatingPerson O" +
				"R @Original_UpdatingPerson IS NULL AND UpdatingPerson IS NULL) AND (UpdatingPers" +
				"onID = @Original_UpdatingPersonID OR @Original_UpdatingPersonID IS NULL AND Upda" +
				"tingPersonID IS NULL)";
			this.sqlDeleteCommand1.Connection = this.sqlConnection1;
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OutSideOutStorehouseHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OutSideOutStorehouseHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BusinessRegistrationNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BusinessRegistrationNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CompanyName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OrderRate", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderRate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OutSideOrderHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OutSideOrderHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OutStorehouseDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OutStorehouseDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessCode", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessCode", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessSequenceNum", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessSequenceNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressCondition", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProgressCondition", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressRate", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ProgressRate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPerson", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ThisDeliveryDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ThisDeliveryDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ThisOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThisOrderQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ThistimeOutStorehouseQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThistimeOutStorehouseQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPerson", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPersonID", System.Data.DataRowVersion.Original, null));
			// 
			// sqlConnection1
			// 

			this.sqlConnection1.ConnectionString = "workstation id=\"EZSYS-ERP01\";packet size=4096;user id=sa;data source=\"220.66.115.9\";persist " +
				"security info=True;initial catalog=Sindong_ERP;password=\"ezsys#0509\"";
			//this.sqlConnection1.ConnectionString = "workstation id=FREESRV05;packet size=4096;user id=sa;data source=\"220.66.115.10\";" +
			//	"persist security info=True;initial catalog=Shindong_ERP;password=\"ezsys#0509\"";
			// 
			// sqlInsertCommand1
			// 
			this.sqlInsertCommand1.CommandText = @"INSERT INTO OOS_HT(ItemNum, ItemDrawNum, ItemName, OrderRate, ProgressRate, ProcessSequenceNum, ProcessCode, ProcessName, CompanyName, BusinessRegistrationNum, ThistimeOutStorehouseQuantity, OutStorehouseDate, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, OutSideOrderHistoryIndex, ThisOrderQuantity, ThisDeliveryDate) VALUES (@ItemNum, @ItemDrawNum, @ItemName, @OrderRate, @ProgressRate, @ProcessSequenceNum, @ProcessCode, @ProcessName, @CompanyName, @BusinessRegistrationNum, @ThistimeOutStorehouseQuantity, @OutStorehouseDate, @ProgressCondition, @RegistrationPerson, @RegistrationPersonID, @RegistrationDate, @UpdatingPerson, @UpdatingPersonID, @UpdatingDate, @OutSideOrderHistoryIndex, @ThisOrderQuantity, @ThisDeliveryDate); SELECT ItemNum, ItemDrawNum, ItemName, OrderRate, ProgressRate, ProcessSequenceNum, ProcessCode, ProcessName, CompanyName, BusinessRegistrationNum, ThistimeOutStorehouseQuantity, OutStorehouseDate, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, OutSideOrderHistoryIndex, OutSideOutStorehouseHistoryIndex, ThisOrderQuantity, ThisDeliveryDate FROM OOS_HT WHERE (OutSideOutStorehouseHistoryIndex = @@IDENTITY)";
			this.sqlInsertCommand1.Connection = this.sqlConnection1;
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.NVarChar, 50, "ItemNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, "ItemDrawNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemName", System.Data.SqlDbType.NVarChar, 50, "ItemName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderRate", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderRate", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressRate", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ProgressRate", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessSequenceNum", System.Data.SqlDbType.Int, 4, "ProcessSequenceNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessCode", System.Data.SqlDbType.NVarChar, 50, "ProcessCode"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessName", System.Data.SqlDbType.NVarChar, 50, "ProcessName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar, 50, "CompanyName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BusinessRegistrationNum", System.Data.SqlDbType.NVarChar, 50, "BusinessRegistrationNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ThistimeOutStorehouseQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThistimeOutStorehouseQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OutStorehouseDate", System.Data.SqlDbType.DateTime, 4, "OutStorehouseDate"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressCondition", System.Data.SqlDbType.NVarChar, 50, "ProgressCondition"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, "RegistrationPerson"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, "RegistrationPersonID"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationDate", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, "UpdatingPerson"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, "UpdatingPersonID"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingDate", System.Data.SqlDbType.DateTime, 4, "UpdatingDate"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OutSideOrderHistoryIndex", System.Data.SqlDbType.Int, 4, "OutSideOrderHistoryIndex"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ThisOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThisOrderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ThisDeliveryDate", System.Data.SqlDbType.DateTime, 4, "ThisDeliveryDate"));
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = @"SELECT ItemNum, ItemDrawNum, ItemName, OrderRate, ProgressRate, ProcessSequenceNum, ProcessCode, ProcessName, CompanyName, BusinessRegistrationNum, ThistimeOutStorehouseQuantity, OutStorehouseDate, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, OutSideOrderHistoryIndex, OutSideOutStorehouseHistoryIndex, ThisOrderQuantity, ThisDeliveryDate FROM OOS_HT WHERE (ItemNum IS NULL)";
			this.sqlSelectCommand1.Connection = this.sqlConnection1;
			// 
			// sqlUpdateCommand1
			// 
			this.sqlUpdateCommand1.CommandText = "UPDATE OOS_HT SET ItemNum = @ItemNum, ItemDrawNum = @ItemDrawNum, ItemName = @Ite" +
				"mName, OrderRate = @OrderRate, ProgressRate = @ProgressRate, ProcessSequenceNum " +
				"= @ProcessSequenceNum, ProcessCode = @ProcessCode, ProcessName = @ProcessName, C" +
				"ompanyName = @CompanyName, BusinessRegistrationNum = @BusinessRegistrationNum, T" +
				"histimeOutStorehouseQuantity = @ThistimeOutStorehouseQuantity, OutStorehouseDate" +
				" = @OutStorehouseDate, ProgressCondition = @ProgressCondition, RegistrationPerso" +
				"n = @RegistrationPerson, RegistrationPersonID = @RegistrationPersonID, Registrat" +
				"ionDate = @RegistrationDate, UpdatingPerson = @UpdatingPerson, UpdatingPersonID " +
				"= @UpdatingPersonID, UpdatingDate = @UpdatingDate, OutSideOrderHistoryIndex = @O" +
				"utSideOrderHistoryIndex, ThisOrderQuantity = @ThisOrderQuantity, ThisDeliveryDat" +
				"e = @ThisDeliveryDate WHERE (OutSideOutStorehouseHistoryIndex = @Original_OutSid" +
				"eOutStorehouseHistoryIndex) AND (BusinessRegistrationNum = @Original_BusinessReg" +
				"istrationNum OR @Original_BusinessRegistrationNum IS NULL AND BusinessRegistrati" +
				"onNum IS NULL) AND (CompanyName = @Original_CompanyName OR @Original_CompanyName" +
				" IS NULL AND CompanyName IS NULL) AND (ItemDrawNum = @Original_ItemDrawNum OR @O" +
				"riginal_ItemDrawNum IS NULL AND ItemDrawNum IS NULL) AND (ItemName = @Original_I" +
				"temName OR @Original_ItemName IS NULL AND ItemName IS NULL) AND (ItemNum = @Orig" +
				"inal_ItemNum OR @Original_ItemNum IS NULL AND ItemNum IS NULL) AND (OrderRate = " +
				"@Original_OrderRate OR @Original_OrderRate IS NULL AND OrderRate IS NULL) AND (O" +
				"utSideOrderHistoryIndex = @Original_OutSideOrderHistoryIndex OR @Original_OutSid" +
				"eOrderHistoryIndex IS NULL AND OutSideOrderHistoryIndex IS NULL) AND (OutStoreho" +
				"useDate = @Original_OutStorehouseDate OR @Original_OutStorehouseDate IS NULL AND" +
				" OutStorehouseDate IS NULL) AND (ProcessCode = @Original_ProcessCode OR @Origina" +
				"l_ProcessCode IS NULL AND ProcessCode IS NULL) AND (ProcessName = @Original_Proc" +
				"essName OR @Original_ProcessName IS NULL AND ProcessName IS NULL) AND (ProcessSe" +
				"quenceNum = @Original_ProcessSequenceNum OR @Original_ProcessSequenceNum IS NULL" +
				" AND ProcessSequenceNum IS NULL) AND (ProgressCondition = @Original_ProgressCond" +
				"ition OR @Original_ProgressCondition IS NULL AND ProgressCondition IS NULL) AND " +
				"(ProgressRate = @Original_ProgressRate OR @Original_ProgressRate IS NULL AND Pro" +
				"gressRate IS NULL) AND (RegistrationDate = @Original_RegistrationDate OR @Origin" +
				"al_RegistrationDate IS NULL AND RegistrationDate IS NULL) AND (RegistrationPerso" +
				"n = @Original_RegistrationPerson OR @Original_RegistrationPerson IS NULL AND Reg" +
				"istrationPerson IS NULL) AND (RegistrationPersonID = @Original_RegistrationPerso" +
				"nID OR @Original_RegistrationPersonID IS NULL AND RegistrationPersonID IS NULL) " +
				"AND (ThisDeliveryDate = @Original_ThisDeliveryDate OR @Original_ThisDeliveryDate" +
				" IS NULL AND ThisDeliveryDate IS NULL) AND (ThisOrderQuantity = @Original_ThisOr" +
				"derQuantity OR @Original_ThisOrderQuantity IS NULL AND ThisOrderQuantity IS NULL" +
				") AND (ThistimeOutStorehouseQuantity = @Original_ThistimeOutStorehouseQuantity O" +
				"R @Original_ThistimeOutStorehouseQuantity IS NULL AND ThistimeOutStorehouseQuant" +
				"ity IS NULL) AND (UpdatingDate = @Original_UpdatingDate OR @Original_UpdatingDat" +
				"e IS NULL AND UpdatingDate IS NULL) AND (UpdatingPerson = @Original_UpdatingPers" +
				"on OR @Original_UpdatingPerson IS NULL AND UpdatingPerson IS NULL) AND (Updating" +
				"PersonID = @Original_UpdatingPersonID OR @Original_UpdatingPersonID IS NULL AND " +
				"UpdatingPersonID IS NULL); SELECT ItemNum, ItemDrawNum, ItemName, OrderRate, Pro" +
				"gressRate, ProcessSequenceNum, ProcessCode, ProcessName, CompanyName, BusinessRe" +
				"gistrationNum, ThistimeOutStorehouseQuantity, OutStorehouseDate, ProgressConditi" +
				"on, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, " +
				"UpdatingPersonID, UpdatingDate, OutSideOrderHistoryIndex, OutSideOutStorehouseHi" +
				"storyIndex, ThisOrderQuantity, ThisDeliveryDate FROM OOS_HT WHERE (OutSideOutSto" +
				"rehouseHistoryIndex = @OutSideOutStorehouseHistoryIndex)";
			this.sqlUpdateCommand1.Connection = this.sqlConnection1;
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.NVarChar, 50, "ItemNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, "ItemDrawNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemName", System.Data.SqlDbType.NVarChar, 50, "ItemName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderRate", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderRate", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressRate", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ProgressRate", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessSequenceNum", System.Data.SqlDbType.Int, 4, "ProcessSequenceNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessCode", System.Data.SqlDbType.NVarChar, 50, "ProcessCode"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessName", System.Data.SqlDbType.NVarChar, 50, "ProcessName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.NVarChar, 50, "CompanyName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BusinessRegistrationNum", System.Data.SqlDbType.NVarChar, 50, "BusinessRegistrationNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ThistimeOutStorehouseQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThistimeOutStorehouseQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OutStorehouseDate", System.Data.SqlDbType.DateTime, 4, "OutStorehouseDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressCondition", System.Data.SqlDbType.NVarChar, 50, "ProgressCondition"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, "RegistrationPerson"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, "RegistrationPersonID"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationDate", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, "UpdatingPerson"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, "UpdatingPersonID"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingDate", System.Data.SqlDbType.DateTime, 4, "UpdatingDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OutSideOrderHistoryIndex", System.Data.SqlDbType.Int, 4, "OutSideOrderHistoryIndex"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ThisOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThisOrderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ThisDeliveryDate", System.Data.SqlDbType.DateTime, 4, "ThisDeliveryDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OutSideOutStorehouseHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OutSideOutStorehouseHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BusinessRegistrationNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BusinessRegistrationNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CompanyName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OrderRate", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderRate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OutSideOrderHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OutSideOrderHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OutStorehouseDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OutStorehouseDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessCode", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessCode", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessSequenceNum", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessSequenceNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressCondition", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProgressCondition", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressRate", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ProgressRate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPerson", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ThisDeliveryDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ThisDeliveryDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ThisOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThisOrderQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ThistimeOutStorehouseQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThistimeOutStorehouseQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPerson", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OutSideOutStorehouseHistoryIndex", System.Data.SqlDbType.Int, 4, "OutSideOutStorehouseHistoryIndex"));
			// 
			// dsOOS_HT1
			// 
			this.dsOOS_HT1.DataSetName = "dsOOS_HT";
			this.dsOOS_HT1.Locale = new System.Globalization.CultureInfo("ko-KR");
			this.Load += new System.EventHandler(this.Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.dsOOS_HT1)).EndInit();

		}
		#endregion

		/// <summary>
		/// 검색함수
		/// </summary>
		/// <returns></returns>
		private DataSet Search()
		{
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
				search = new KIT_ERP.Search("OutSideOutStorehouse",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcStartDate,wdcEndDate,CSC1.Company,CSC1.BusinessRegistrationNum,"대기",ddlItemClassification1.SelectedItem.Value);
			else
				search = new KIT_ERP.Search("OutSideOutStorehouse",ItemSearchControl1.ItemNum, "", "",wdcStartDate,wdcEndDate,CSC1.Company,CSC1.BusinessRegistrationNum,"대기",ddlItemClassification1.SelectedItem.Value);
			return search.DataSet_search();
		}

		//검색버튼 클릭
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			uwgOO_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
			uwgOO_HT.DataSource = Search();
			uwgOO_HT.DataBind();
		}

		//등록버튼 클릭
		private void btnRegister_Click(object sender, System.EventArgs e)
		{
			if(dsOOS_HT1.Tables[0].Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('등록할 항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{

				//출고수량이 0인것은 삭제
				for(int count = uwgOOS_HT.Rows.Count -1 ; count >=0 ; count--)
				{
					if(decimal.Parse(uwgOOS_HT.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text.Trim()) == 0)
					uwgOOS_HT.Rows[count].Delete();
				}

				Register rg = new Register(uwgOOS_HT,"OutSideOutStorehouse",Session["ID"].ToString());
				rg.GridRegistration();

				dsOOS_HT1.Tables[0].Rows.Clear();
				uwgOOS_HT.DataSource = dsOOS_HT1;
				uwgOOS_HT.DataBind();

				//검색
				uwgOO_HT.DataSource = Search();
				uwgOO_HT.DataBind();
			}

			Session["dsOOS_HT1"] = dsOOS_HT1 ;

		}

		/// <summary>
		/// 외주 발주 선택시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void uwgOO_HT_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{

			if(txtThisOrderQuantity.Text .Trim() == "" || txtThisOrderQuantity.Text.Trim() == "0")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('금번발주수량을 입력하세요!');");
				Response.Write("</script>");
			}
			else if(wdcDeliveryDate.Text.Trim() == "" || wdcDeliveryDate.Value ==null)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('금번납기일을 입력하세요!');");
				Response.Write("</script>");
			}
			else
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				// 일단 발주원장의 시작공정 아래단계가 어떤 공정인지 알아야 한다
				// 공정순서가 제일 낮은 것이면 바로 아래단계 품목이 나가야 하고
				// 그렇지 않으면 그아래 공정순서 품목이 나가야 한다.
				// 그러므로 아랫단계에 공정이 있는지 없는지 확인하는것이 우선이다

				// 일단 지금 시작공정의 공정순서를 구한다음 그아래 가 있는지 확인한다
				int sequence = 0;

				string str = "Select ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ProcessCode = @code and ItemNum = @itemnum";
				SqlCommand comm = new SqlCommand(str,conn);
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BeginProcessCode").Text;//.Substring(0,8).Trim();
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("ItemNum").Text;;

				SqlDataReader dr_num = comm.ExecuteReader();
				while(dr_num.Read())
				{
					sequence = int.Parse(dr_num["ProcessSequenceNum"].ToString());
				}
				dr_num.Close();
			
				//지금 공정순서보다 아래에 공정순서가 있는지 없는지를 구한다
				int sequence1 = 0;	//공정순서
				string Code = "";	// 공정코드
				string Name = "";	// 공정명
				str = "Select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ProcessSequenceNum < @num and ItemNum = @itemnum";
				SqlCommand comm1 = new SqlCommand(str,conn);
				comm1.Parameters.Add("@num",SqlDbType.Int).Value = sequence;
				comm1.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("ItemNum").Text;
				if(comm1.ExecuteScalar() == null || comm1.ExecuteScalar().ToString().Trim() == "")
					sequence1 = 0;
				else
					sequence1 = int.Parse(comm1.ExecuteScalar().ToString());

			
				str = "Select ProcessCode From PSI_MT Where RecodingState = 1 and ProcessSequenceNum = @num and ItemNum = @itemnum";
				SqlCommand comm2 = new SqlCommand(str,conn);
				comm2.Parameters.Add("@num",SqlDbType.Int).Value = sequence1;
				comm2.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("ItemNum").Text;
				SqlDataReader dr2 = comm2.ExecuteReader();
				while(dr2.Read())
				{
					Code = dr2["ProcessCode"].ToString();
				}
				dr2.Close();

			


				str = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code";

				SqlCommand comm3 = new SqlCommand(str,conn);
				comm3.Parameters.Add("@code",SqlDbType.VarChar).Value = Code;
				SqlDataReader dr3 = comm3.ExecuteReader();
				while(dr3.Read())
				{
					Name = dr3["SmallClassificationName"].ToString();
				}
				dr3.Close();
		

			

				// 아래 공정순서번호가 존재할때는(공정순서가 0이 아닌 경우) 바로 그 품목을 출고시킨다.
				// 지금 들어있는 공정순서번호와 품목을 출고시킨다.
				string st_ItemNum = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("ItemNum").Text;

				if(sequence1 != 0)
				{
					// 진척비율을 구해야 한다
					decimal rate = 0;	//발주비율(공정순서에 있는 발주비율과 단가테이블에 있는 발주비율을 곱하면 실제 출고되어야 할 발주비율
					decimal rate1 = 0;	//진척비율(공정순서에 있는 진척비율을 넣어둔다.

					str = "Select OutsideOrderRate, ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @item and ProcessSequenceNum = @num";
					SqlCommand comm4 = new SqlCommand(str,conn);
					comm4.Parameters.Add("@item",SqlDbType.VarChar).Value = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("ItemNum").Text;
					comm4.Parameters.Add("@num",SqlDbType.Int).Value = sequence1;
					SqlDataReader dr_Rate = comm4.ExecuteReader();
					while(dr_Rate.Read())
					{
						rate = decimal.Parse(dr_Rate["OutsideOrderRate"].ToString());
						rate1 = decimal.Parse(dr_Rate["ProgressRate"].ToString());
					}
					dr_Rate.Close();

				

				
				

					// 데이터셋에 추가하기위한 Row
					dsOOS_HT.OOS_HTRow dr = (dsOOS_HT.OOS_HTRow)dsOOS_HT1.OOS_HT.NewOOS_HTRow();

					dr.ItemNum = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("ItemNum").Text;
					dr.ItemDrawNum = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("ItemDrawNum").Text;
					dr.ItemName = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("ItemName").Text;
					dr.OrderRate = decimal.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OrderRate").Text);//단가테이블에 있는 거래처별 발주비율
					dr.ProgressRate = rate1;//진척비율
					dr.ProcessSequenceNum = sequence1;//공정순서
					dr.ProcessCode = Code;//공정코드
					dr.ProcessName = Name;//공정명
					dr.CompanyName = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("CompanyName").Text;
					dr.BusinessRegistrationNum = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BusinessRegistrationNum").Text;
					dr.ThistimeOutStorehouseQuantity = decimal.Parse(txtThisOrderQuantity.Text);//decimal.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OrderQuantity").Text);// 거래처별 금번출고량
					dr.ProgressCondition = "대기";
					dr.OutStorehouseDate = DateTime.Parse(DateTime.Now.ToShortDateString());
					dr.RegistrationPerson = Session["UserName"].ToString();
					dr.RegistrationPersonID = Session["ID"].ToString();
					dr.RegistrationDate = DateTime.Parse(DateTime.Now.ToShortDateString());
					dr.OutSideOrderHistoryIndex = int.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OutSideOrderHistoryIndex").Text);// 외주발주원장번호
					dr.ThisOrderQuantity = decimal.Parse(txtThisOrderQuantity.Text);
					dr.ThisDeliveryDate = DateTime.Parse(wdcDeliveryDate.Text);

					dsOOS_HT1.OOS_HT.AddOOS_HTRow(dr);

				}
				else
				{
					// 순서번호가 0 이면 하위품목을 찾아 스택에 Push한다
					Stack st = new Stack();	

					string item = "";
					//하위품목을 찾기위해서는 품목구성정보에 지금 품목을 모품으로 하는 모든 레코드를 스택에 넣는다
					//str = "Select ChildItemNum, ItemDrawNum, Item From IOI_MT JOIN ON II_MT Where RecodingState = 1 and ParentItemNum = @itemnum";
					str = @"SELECT ItemNum, ItemDrawNum, ItemName, NeedQuantityNumerator, NeedQuantityDenominator FROM II_MT INNER JOIN IOI_MT ON II_MT.ItemNum = IOI_MT.ChildItemNum 	WHERE (II_MT.RecodingState = 1) AND (IOI_MT.RecodingState = 1) AND (IOI_MT.ParentItemNum = @itemnum) ";
					SqlCommand comm5 = new SqlCommand(str,conn);
					comm5.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
					SqlDataReader dr_Child = comm5.ExecuteReader();
					while(dr_Child.Read())
					{
						st.Push(dr_Child["ItemNum"].ToString());
					}
					dr_Child.Close();
				
				
					// 스택에 품목이 있는지 확인한다.
					if(st.Count != 0)
					{
						// 스택의 내용이 있으면 다 꺼낼때까지 계속 Pop한다
						while(st.Count > 0)
						{	
							item =st.Pop().ToString();
							decimal quantity = 1;

							// 꺼낸품목의 공정순서를 스택1에 넣는다
							Stack st1 = new Stack();					
							int sequencenum=0;

							//꺼낸 품목의 공정순서를 넣어둔다.
							str = "Select ProcessSequenceNum from PSI_MT where RecodingState = 1 AND ItemNum = @itemnum  order by ProcessSequenceNum";
							SqlCommand comm6 = new SqlCommand();
							comm6.Connection = conn;
							comm6.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
							comm6.CommandText = str;
							SqlDataReader dr_Sequence = comm6.ExecuteReader();
							while(dr_Sequence.Read())
							{
								//공정순서
								st1.Push(int.Parse(dr_Sequence["ProcessSequenceNum"].ToString()));
							
							}
							dr_Sequence.Close();
							comm6.Parameters.Clear();

							//꺼낸품목의 소요량을 넣어둔다
							str = "select * From IOI_MT Where ParentItemNum = @num and ChildItemNum = @item and RecodingState = 1 ";
							comm6.Parameters.Add("@num",st_ItemNum);
							comm6.Parameters.Add("@item",item);
							comm6.CommandText = str;
							SqlDataReader dr1 = comm6.ExecuteReader();
							while(dr1.Read())
							{
								//quantity = Math.Round(decimal.Parse(dr1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr1["NeedQuantityDenominator"].ToString()));
								quantity = decimal.Round(decimal.Parse(dr1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr1["NeedQuantityDenominator"].ToString()),2);
							}
							dr1.Close();
							comm6.Parameters.Clear();

							//공정순서가 없으면 지금 품목이 원자재이므로 이를 출고시킨다.
							if(st1.Count == 0)
							{
								//품번,도번,품명 구한다
								string itemdraw=""; string name="";
								str = "Select * From II_MT Where ItemNum = @num and RecodingState = 1";
								SqlCommand comm7 = new SqlCommand(str,conn);
								comm7.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
								SqlDataReader dr_Item = comm7.ExecuteReader();
								while(dr_Item.Read())
								{
									itemdraw = dr_Item["ItemDrawNum"].ToString();
									name = dr_Item["ItemName"].ToString();
								}
								dr_Item.Close();

								// 데이터셋에 추가하기위한 Row
								dsOOS_HT.OOS_HTRow dr = (dsOOS_HT.OOS_HTRow)dsOOS_HT1.OOS_HT.NewOOS_HTRow();

								dr.ItemNum = item;
								dr.ItemDrawNum = itemdraw;
								dr.ItemName = name;
								dr.OrderRate = decimal.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OrderRate").Text);//단가테이블에 있는 거래처별 발주비율
								dr.ProgressRate = 100;//진척비율
								dr.ProcessSequenceNum = sequence1;//공정순서
								dr.ProcessCode = "14000000";//공정코드(원자재)
								dr.ProcessName = "소재";//공정명(원자재)
								dr.CompanyName = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("CompanyName").Text;
								dr.BusinessRegistrationNum = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BusinessRegistrationNum").Text;
								dr.ThistimeOutStorehouseQuantity = decimal.Parse(txtThisOrderQuantity.Text)*quantity;//decimal.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OrderQuantity").Text)*quantity;//*decimal.Parse(arr[3].ToString());// 거래처별 금번출고량*하위품목구성량
								dr.ProgressCondition = "대기";
								dr.OutStorehouseDate = DateTime.Parse(DateTime.Now.ToShortDateString());
								dr.RegistrationPerson = Session["UserName"].ToString();
								dr.RegistrationPersonID = Session["ID"].ToString();
								dr.RegistrationDate = DateTime.Parse(DateTime.Now.ToShortDateString());
								dr.OutSideOrderHistoryIndex = int.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OutSideOrderHistoryIndex").Text);// 외주발주원장번호
								dr.ThisOrderQuantity = decimal.Parse(txtThisOrderQuantity.Text);
								dr.ThisDeliveryDate = DateTime.Parse(wdcDeliveryDate.Text);


								dsOOS_HT1.OOS_HT.AddOOS_HTRow(dr);
							}
							else //그렇지 않으면 그 품목의 최상위 공정순서를 찾아서 그 공정이 외주인지 판단한다. 카운트가 0이면 외주가 아니므로 출고시킨다.
							{
								// 스택1에 있는 공정순서를 하나씩꺼낸다.
								while(st1.Count > 0)
								{
									sequencenum = int.Parse(st1.Pop().ToString());
									str = " SELECT  count(*) FROM PSI_MT WHERE RecodingState = 1 and WorkDistinction <> '외주' and ItemNum = @item and ProcessSequenceNum = @num";
									SqlCommand comm8 = new SqlCommand(str,conn);
									comm8.Parameters.Add("@item",SqlDbType.VarChar).Value = item;//품목번호
									comm8.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;//공정순서
									int aaa = int.Parse(comm8.ExecuteScalar().ToString());
								

									//품번,도번,품명 구한다
									string itemdraw=""; string name="";
									str = "Select * From II_MT Where ItemNum = @num";
									SqlCommand comm9 = new SqlCommand(str,conn);
									comm9.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
									SqlDataReader dr_Item = comm9.ExecuteReader();
									while(dr_Item.Read())
									{
										itemdraw = dr_Item["ItemDrawNum"].ToString();
										name = dr_Item["ItemName"].ToString();
									}
									dr_Item.Close();

									//공정코드와 공정명을 구한다
									string processcode="";string processname="";
									str = " SELECT  ProcessSequenceNum, ProcessCode, SmallClassificationName " +
										" FROM      PSI_MT INNER JOIN " +
										" PUC_MT ON ProcessCode = SmallClassificationCode " +
										" WHERE PSI_MT.RecodingState = 1 AND ItemNum = @itemnum and ProcessSequenceNum = @num";
									SqlCommand comm10 = new SqlCommand(str,conn);
									comm10.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
									comm10.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;
									SqlDataReader dr_code = comm10.ExecuteReader();
									while(dr_code.Read())
									{
										processcode = dr_code["ProcessCode"].ToString();//공정코드
										processname = dr_code["SmallClassificationName"].ToString();//공정명
									}
									dr_code.Close();


									// 외주가 아니면 출고
									if(aaa != 0)
									{
										// 데이터셋에 추가하기위한 Row
										dsOOS_HT.OOS_HTRow dr = (dsOOS_HT.OOS_HTRow)dsOOS_HT1.OOS_HT.NewOOS_HTRow();

										dr.ItemNum = item;
										dr.ItemDrawNum = itemdraw;
										dr.ItemName = name;
										dr.OrderRate = decimal.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OrderRate").Text);//단가테이블에 있는 거래처별 발주비율
										dr.ProgressRate = 100;//진척비율
										dr.ProcessSequenceNum = sequencenum;//공정순서
										dr.ProcessCode = processcode;//공정코드
										dr.ProcessName = processname;//공정명
										dr.CompanyName = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("CompanyName").Text;
										dr.BusinessRegistrationNum = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BusinessRegistrationNum").Text;
										dr.ThistimeOutStorehouseQuantity = decimal.Parse(txtThisOrderQuantity.Text)*quantity;//decimal.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OrderQuantity").Text)*quantity;//*decimal.Parse(arr[3].ToString());// 거래처별 금번출고량*하위품목구성량
										dr.ProgressCondition = "대기";
										dr.OutStorehouseDate = DateTime.Parse(DateTime.Now.ToShortDateString());
										dr.RegistrationPerson = Session["UserName"].ToString();
										dr.RegistrationPersonID = Session["ID"].ToString();
										dr.RegistrationDate = DateTime.Parse(DateTime.Now.ToShortDateString());
										dr.OutSideOrderHistoryIndex = int.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OutSideOrderHistoryIndex").Text);// 외주발주원장번호
										dr.ThisOrderQuantity = decimal.Parse(txtThisOrderQuantity.Text);
										dr.ThisDeliveryDate = DateTime.Parse(wdcDeliveryDate.Text);

										dsOOS_HT1.OOS_HT.AddOOS_HTRow(dr);
										st1.Clear();
										break;
									}
									else//외주이면 외주단가에서 지금 찾은 공정이랑 종료공정이 동일한것이 있는지 살펴본다. 있으면 출고시키고 없으면 이전공정을 찾는다
									{
										// 외주단가에서 동일회사 외주공정인지 파악한다.
										str = "select count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @itemnum and EndProcessCode = @code and BusinessRegistrationNum = @com";
										SqlCommand comm11 = new SqlCommand(str,conn);
										comm11.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
										comm11.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;
										comm11.Parameters.Add("@com",SqlDbType.VarChar).Value = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BusinessRegistrationNum").Text;

										int find = int.Parse(comm11.ExecuteScalar().ToString());
									

										// 동일회사가 아니면 출고
										if(find == 0)
										{
											// 데이터셋에 추가하기위한 Row
											dsOOS_HT.OOS_HTRow dr = (dsOOS_HT.OOS_HTRow)dsOOS_HT1.OOS_HT.NewOOS_HTRow();

											dr.ItemNum = item;
											dr.ItemDrawNum = itemdraw;
											dr.ItemName = name;
											dr.OrderRate = decimal.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OrderRate").Text);//단가테이블에 있는 거래처별 발주비율
											dr.ProgressRate = 100;//진척비율
											dr.ProcessSequenceNum = sequencenum;//공정순서
											dr.ProcessCode = processcode;//공정코드
											dr.ProcessName = processname;//공정명
											dr.CompanyName = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("CompanyName").Text;
											dr.BusinessRegistrationNum = uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("BusinessRegistrationNum").Text;
											dr.ThistimeOutStorehouseQuantity =  decimal.Parse(txtThisOrderQuantity.Text)*quantity;//decimal.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OrderQuantity").Text)*quantity;//*decimal.Parse(arr[3].ToString());// 거래처별 금번출고량*하위품목구성량
											dr.ProgressCondition = "대기";
											dr.OutStorehouseDate = DateTime.Parse(DateTime.Now.ToShortDateString());
											dr.RegistrationPerson = Session["UserName"].ToString();
											dr.RegistrationPersonID = Session["ID"].ToString();
											dr.RegistrationDate = DateTime.Parse(DateTime.Now.ToShortDateString());
											dr.OutSideOrderHistoryIndex = int.Parse(uwgOO_HT.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OutSideOrderHistoryIndex").Text);// 외주발주원장번호
											dr.ThisOrderQuantity = decimal.Parse(txtThisOrderQuantity.Text);
											dr.ThisDeliveryDate = DateTime.Parse(wdcDeliveryDate.Text);

											dsOOS_HT1.OOS_HT.AddOOS_HTRow(dr);

											st1.Clear();
											break;
										}
									}
								}
							}
						}
					}
				}

				uwgOOS_HT.DataSource = dsOOS_HT1;
				uwgOOS_HT.DataBind();
			}
			Session["dsOOS_HT1"] = dsOOS_HT1 ;
		}


		/// <summary>
		/// 품목의 자산분류가 무었인지 판단하는 함수
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private int Division(int rowcount)
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value =  uwgOOS_HT.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "원자재")
				return 1;
			else if(aa.ToString() =="반제품")
				return 2;
			else if(aa.ToString() =="제품")
				return 3;
			else if(aa.ToString() =="상품")
				return 4;
			else //if(aa.ToString() =="팬텀")
				return 5;
		}


		private void preOutStorehouse_Click(object sender, System.EventArgs e)
		{
			string formID = "PreOutStorehousePopUp" ;

			Response.Write("<script language=javascript>");
			Response.Write("window.open('PreOutStorehousePopUp.aspx?formID=" + formID + "','Calendar'," + "'width=675px" + "," + "height=270px');");
			Response.Write("</script>");  
		}

		

		private void uwgOO_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgOO_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			uwgOO_HT.DataSource = Search();
			uwgOO_HT.DataBind();
			
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			Infragistics.WebUI.UltraWebGrid.UltraWebGrid UWG = uwgOOS_HT;
			bool check = true;
			
			if(UWG.Rows.Count == 0)
			{
				check = false;
				RegisterStartupScript("","<script>alert('선택한 항목이 없습니다!');</script>");
			}
			else if(UWG.Rows.Count >= 15)
			{
				check = false;
				RegisterStartupScript("","<script>alert('선택한 항목이 너무 많습니다!');</script>");
			}
			else
			{
				for(int i = 0; i < UWG.Rows.Count; i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(UWG.Rows[0].Cells.FromKey("BusinessRegistrationNum").Text != UWG.Rows[i].Cells.FromKey("BusinessRegistrationNum").Text)
					{
						check = false;
						RegisterStartupScript("","<script>alert('발행할 거래처가 2곳 이상입니다!');</script>");
					}
				}
			}


			if(check)
			{
				//출고수량이 0인것은 삭제
				for(int count = UWG.Rows.Count -1 ; count >=0 ; count--)
				{
					if(decimal.Parse(UWG.Rows[count].Cells.FromKey("ThistimeOutStorehouseQuantity").Text.Trim()) == 0)
						UWG.Rows[count].Delete();
				}
					
				Session["Grid"] = UWG;
				//RegisterStartupScript("","<script>window.open('./Popup/OutSideOutStorehousePurchase.aspx','','width=1100,scrollbars=yes,menubar=yes,status=yes,toolbar=yes,center=yes');</script>");
				RegisterStartupScript("","<script>window.open('./Popup/OutSideStorehousePurchase.aspx','','width=795,scrollbars=yes,menubar=yes,status=yes,toolbar=yes,center=yes');</script>");
			
			}			
		}
	}
}
