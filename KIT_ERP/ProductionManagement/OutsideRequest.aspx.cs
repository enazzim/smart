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

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// OutsideRequest에 대한 요약 설명입니다.
	/// </summary>
	public class OutsideRequest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Request;
		protected System.Web.UI.WebControls.Label Label2;
		protected DataSet ds;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		protected System.Data.SqlClient.SqlDataAdapter sqlDataAdapter2;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ToDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_FromDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected System.Data.SqlClient.SqlConnection sqlConnection1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand2;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected DataSet ds_OOR;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			
			sqlConnection1.ConnectionString = ConfigurationSettings.AppSettings["DSN"];

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 생산관리 > 외주의뢰';</script>");
				//bt_Request.Attributes.Add("onClick", "return OK('의뢰 ');");

				DataSet ds = new DataSet();
				DataSet ds_OOR = new DataSet();

				Session["ds"] = ds;
				Session["ds_OOR"] = ds_OOR;
			
				//	*************************************************
				//	**  품목분류1 드롭다운리스트... 데이타바인딩... **	
				//	*************************************************
				//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
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

			}
			else
			{
				ds = (DataSet)Session["ds"];
				ds_OOR = (DataSet)Session["ds_OOR"];
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
			this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlDataAdapter2 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
			this.bt_Request.Click += new System.EventHandler(this.bt_Request_Click);
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.DeleteCommand = this.sqlDeleteCommand1;
			this.sqlDataAdapter1.InsertCommand = this.sqlInsertCommand1;
			this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "WDWP_HT", new System.Data.Common.DataColumnMapping[] {
																																																				 new System.Data.Common.DataColumnMapping("ItemNum", "ItemNum"),
																																																				 new System.Data.Common.DataColumnMapping("ItemDrawNum", "ItemDrawNum"),
																																																				 new System.Data.Common.DataColumnMapping("ItemName", "ItemName"),
																																																				 new System.Data.Common.DataColumnMapping("ProcessSequenceNum", "ProcessSequenceNum"),
																																																				 new System.Data.Common.DataColumnMapping("ProcessCode", "ProcessCode"),
																																																				 new System.Data.Common.DataColumnMapping("ProcessName", "ProcessName"),
																																																				 new System.Data.Common.DataColumnMapping("ProductItemNum", "ProductItemNum"),
																																																				 new System.Data.Common.DataColumnMapping("ProductDrawNum", "ProductDrawNum"),
																																																				 new System.Data.Common.DataColumnMapping("ProductName", "ProductName"),
																																																				 new System.Data.Common.DataColumnMapping("ParentItemNum", "ParentItemNum"),
																																																				 new System.Data.Common.DataColumnMapping("ParentDrawNum", "ParentDrawNum"),
																																																				 new System.Data.Common.DataColumnMapping("ParentName", "ParentName"),
																																																				 new System.Data.Common.DataColumnMapping("WCName", "WCName"),
																																																				 new System.Data.Common.DataColumnMapping("WorkDistinction", "WorkDistinction"),
																																																				 new System.Data.Common.DataColumnMapping("WorkPlanQuantity", "WorkPlanQuantity"),
																																																				 new System.Data.Common.DataColumnMapping("WorkCompletionQuantity", "WorkCompletionQuantity"),
																																																				 new System.Data.Common.DataColumnMapping("OrderLeadTime", "OrderLeadTime"),
																																																				 new System.Data.Common.DataColumnMapping("WorkDate", "WorkDate"),
																																																				 new System.Data.Common.DataColumnMapping("ProgressCondition", "ProgressCondition"),
																																																				 new System.Data.Common.DataColumnMapping("RegistrationPerson", "RegistrationPerson"),
																																																				 new System.Data.Common.DataColumnMapping("RegistrationPersonID", "RegistrationPersonID"),
																																																				 new System.Data.Common.DataColumnMapping("RegistrationDate", "RegistrationDate"),
																																																				 new System.Data.Common.DataColumnMapping("UpdatingPerson", "UpdatingPerson"),
																																																				 new System.Data.Common.DataColumnMapping("UpdatingPersonID", "UpdatingPersonID"),
																																																				 new System.Data.Common.DataColumnMapping("UpdatingDate", "UpdatingDate"),
																																																				 new System.Data.Common.DataColumnMapping("ProductionPlanHistoryIndex", "ProductionPlanHistoryIndex"),
																																																				 new System.Data.Common.DataColumnMapping("Expr1", "Expr1"),
																																																				 new System.Data.Common.DataColumnMapping("WCDailyWorkPlanHistoryIndex", "WCDailyWorkPlanHistoryIndex")})});
			this.sqlDataAdapter1.UpdateCommand = this.sqlUpdateCommand1;
			// 
			// sqlDeleteCommand1
			// 
			this.sqlDeleteCommand1.CommandText = "DELETE FROM WDWP_HT WHERE (WCDailyWorkPlanHistoryIndex = @Original_WCDailyWorkPla" +
				"nHistoryIndex) AND (ProductionPlanHistoryIndex = @Original_Expr1 OR @Original_Ex" +
				"pr1 IS NULL AND ProductionPlanHistoryIndex IS NULL) AND (ItemDrawNum = @Original" +
				"_ItemDrawNum OR @Original_ItemDrawNum IS NULL AND ItemDrawNum IS NULL) AND (Item" +
				"Name = @Original_ItemName OR @Original_ItemName IS NULL AND ItemName IS NULL) AN" +
				"D (ItemNum = @Original_ItemNum OR @Original_ItemNum IS NULL AND ItemNum IS NULL)" +
				" AND (OrderLeadTime = @Original_OrderLeadTime OR @Original_OrderLeadTime IS NULL" +
				" AND OrderLeadTime IS NULL) AND (ParentDrawNum = @Original_ParentDrawNum OR @Ori" +
				"ginal_ParentDrawNum IS NULL AND ParentDrawNum IS NULL) AND (ParentItemNum = @Ori" +
				"ginal_ParentItemNum OR @Original_ParentItemNum IS NULL AND ParentItemNum IS NULL" +
				") AND (ParentName = @Original_ParentName OR @Original_ParentName IS NULL AND Par" +
				"entName IS NULL) AND (ProcessCode = @Original_ProcessCode OR @Original_ProcessCo" +
				"de IS NULL AND ProcessCode IS NULL) AND (ProcessName = @Original_ProcessName OR " +
				"@Original_ProcessName IS NULL AND ProcessName IS NULL) AND (ProcessSequenceNum =" +
				" @Original_ProcessSequenceNum OR @Original_ProcessSequenceNum IS NULL AND Proces" +
				"sSequenceNum IS NULL) AND (ProductDrawNum = @Original_ProductDrawNum OR @Origina" +
				"l_ProductDrawNum IS NULL AND ProductDrawNum IS NULL) AND (ProductItemNum = @Orig" +
				"inal_ProductItemNum OR @Original_ProductItemNum IS NULL AND ProductItemNum IS NU" +
				"LL) AND (ProductName = @Original_ProductName OR @Original_ProductName IS NULL AN" +
				"D ProductName IS NULL) AND (ProductionPlanHistoryIndex = @Original_ProductionPla" +
				"nHistoryIndex OR @Original_ProductionPlanHistoryIndex IS NULL AND ProductionPlan" +
				"HistoryIndex IS NULL) AND (ProgressCondition = @Original_ProgressCondition OR @O" +
				"riginal_ProgressCondition IS NULL AND ProgressCondition IS NULL) AND (Registrati" +
				"onDate = @Original_RegistrationDate OR @Original_RegistrationDate IS NULL AND Re" +
				"gistrationDate IS NULL) AND (RegistrationPerson = @Original_RegistrationPerson O" +
				"R @Original_RegistrationPerson IS NULL AND RegistrationPerson IS NULL) AND (Regi" +
				"strationPersonID = @Original_RegistrationPersonID OR @Original_RegistrationPerso" +
				"nID IS NULL AND RegistrationPersonID IS NULL) AND (UpdatingDate = @Original_Upda" +
				"tingDate OR @Original_UpdatingDate IS NULL AND UpdatingDate IS NULL) AND (Updati" +
				"ngPerson = @Original_UpdatingPerson OR @Original_UpdatingPerson IS NULL AND Upda" +
				"tingPerson IS NULL) AND (UpdatingPersonID = @Original_UpdatingPersonID OR @Origi" +
				"nal_UpdatingPersonID IS NULL AND UpdatingPersonID IS NULL) AND (WCName = @Origin" +
				"al_WCName OR @Original_WCName IS NULL AND WCName IS NULL) AND (WorkCompletionQua" +
				"ntity = @Original_WorkCompletionQuantity OR @Original_WorkCompletionQuantity IS " +
				"NULL AND WorkCompletionQuantity IS NULL) AND (WorkDate = @Original_WorkDate OR @" +
				"Original_WorkDate IS NULL AND WorkDate IS NULL) AND (WorkDistinction = @Original" +
				"_WorkDistinction OR @Original_WorkDistinction IS NULL AND WorkDistinction IS NUL" +
				"L) AND (WorkPlanQuantity = @Original_WorkPlanQuantity OR @Original_WorkPlanQuant" +
				"ity IS NULL AND WorkPlanQuantity IS NULL)";
			this.sqlDeleteCommand1.Connection = this.sqlConnection1;
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WCDailyWorkPlanHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WCDailyWorkPlanHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Expr1", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Expr1", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OrderLeadTime", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderLeadTime", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ParentDrawNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ParentDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ParentItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ParentItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ParentName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ParentName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessCode", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessCode", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessSequenceNum", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessSequenceNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductDrawNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductionPlanHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Expr1", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressCondition", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProgressCondition", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPerson", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPerson", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WCName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WCName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WorkCompletionQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WorkCompletionQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WorkDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WorkDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WorkDistinction", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WorkDistinction", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WorkPlanQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WorkPlanQuantity", System.Data.DataRowVersion.Original, null));
			// 
			// sqlConnection1
			// 
			this.sqlConnection1.ConnectionString = "workstation id=\"EZSYS-ERP01\";packet size=4096;user id=sa;data source=\"220.66.115.9\";persist " +
				"security info=True;initial catalog=Sindong_ERP;password=\"ezsys#0509\"";

//"workstation id=FREESRV01;packet size=4096;user id=sa;data source=\"220.66.115.10\";persis" +
//				"t security info=False;initial catalog=Shindong_ERP";
			// 
			// sqlInsertCommand1
			// 
			this.sqlInsertCommand1.CommandText = @"INSERT INTO WDWP_HT(ItemNum, ItemDrawNum, ItemName, ProcessSequenceNum, ProcessCode, ProcessName, ProductItemNum, ProductDrawNum, ProductName, ParentItemNum, ParentDrawNum, ParentName, WCName, WorkDistinction, WorkPlanQuantity, WorkCompletionQuantity, OrderLeadTime, WorkDate, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, ProductionPlanHistoryIndex) VALUES (@ItemNum, @ItemDrawNum, @ItemName, @ProcessSequenceNum, @ProcessCode, @ProcessName, @ProductItemNum, @ProductDrawNum, @ProductName, @ParentItemNum, @ParentDrawNum, @ParentName, @WCName, @WorkDistinction, @WorkPlanQuantity, @WorkCompletionQuantity, @OrderLeadTime, @WorkDate, @ProgressCondition, @RegistrationPerson, @RegistrationPersonID, @RegistrationDate, @UpdatingPerson, @UpdatingPersonID, @UpdatingDate, @ProductionPlanHistoryIndex); SELECT ItemNum, ItemDrawNum, ItemName, ProcessSequenceNum, ProcessCode, ProcessName, ProductItemNum, ProductDrawNum, ProductName, ParentItemNum, ParentDrawNum, ParentName, WCName, WorkDistinction, WorkPlanQuantity, WorkCompletionQuantity, OrderLeadTime, WorkDate, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, ProductionPlanHistoryIndex, ProductionPlanHistoryIndex AS Expr1, WCDailyWorkPlanHistoryIndex FROM WDWP_HT WHERE (WCDailyWorkPlanHistoryIndex = @@IDENTITY)";
			this.sqlInsertCommand1.Connection = this.sqlConnection1;
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.NVarChar, 50, "ItemNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, "ItemDrawNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemName", System.Data.SqlDbType.NVarChar, 50, "ItemName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessSequenceNum", System.Data.SqlDbType.TinyInt, 1, "ProcessSequenceNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessCode", System.Data.SqlDbType.NVarChar, 50, "ProcessCode"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessName", System.Data.SqlDbType.NVarChar, 50, "ProcessName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductItemNum", System.Data.SqlDbType.NVarChar, 50, "ProductItemNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductDrawNum", System.Data.SqlDbType.NVarChar, 50, "ProductDrawNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductName", System.Data.SqlDbType.NVarChar, 50, "ProductName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentItemNum", System.Data.SqlDbType.NVarChar, 50, "ParentItemNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentDrawNum", System.Data.SqlDbType.NVarChar, 50, "ParentDrawNum"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentName", System.Data.SqlDbType.NVarChar, 50, "ParentName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WCName", System.Data.SqlDbType.NVarChar, 50, "WCName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkDistinction", System.Data.SqlDbType.NVarChar, 50, "WorkDistinction"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkPlanQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WorkPlanQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkCompletionQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WorkCompletionQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderLeadTime", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderLeadTime", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkDate", System.Data.SqlDbType.DateTime, 4, "WorkDate"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressCondition", System.Data.SqlDbType.NVarChar, 50, "ProgressCondition"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, "RegistrationPerson"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, "RegistrationPersonID"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationDate", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, "UpdatingPerson"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, "UpdatingPersonID"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingDate", System.Data.SqlDbType.DateTime, 4, "UpdatingDate"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductionPlanHistoryIndex", System.Data.SqlDbType.Int, 4, "Expr1"));
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = @"SELECT 
													WDWP_HT.ItemNum, WDWP_HT.ItemDrawNum, WDWP_HT.ItemName,  SmallClassificationName as Team, ProcessSequenceNum, 
													ProcessCode, ProcessName, ProductItemNum, ProductDrawNum, ProductName, 
													ParentItemNum, ParentDrawNum, ParentName, WCName, WorkDistinction, 
													WorkPlanQuantity, WorkCompletionQuantity, OrderLeadTime, WorkDate, 
													ProgressCondition, WDWP_HT.RegistrationPerson, WDWP_HT.RegistrationPersonID, 
													WDWP_HT.RegistrationDate, WDWP_HT.UpdatingPerson, 
													WDWP_HT.UpdatingPersonID, WDWP_HT.UpdatingDate, 
													ProductionPlanHistoryIndex, ProductionPlanHistoryIndex AS Expr1, 
													WCDailyWorkPlanHistoryIndex FROM WDWP_HT 
													Inner join II_MT on WDWP_HT.ItemNum = II_MT.ItemNum
													Inner join PUC_MT on II_MT.ItemClassification1 = PUC_MT.SmallClassificationCode
													WHERE (ProgressCondition = '대기') AND (WorkDistinction <> '자가') 
													AND (II_MT.RecodingState = 1) AND (PUC_MT.RecodingState = 1)
													AND (WDWP_HT.ItemNum LIKE @num) AND (WDWP_HT.ItemDrawNum LIKE @draw) AND 
													(WDWP_HT.ItemName LIKE @name) AND (WDWP_HT.RegistrationDate >= @begin) AND 
													(WDWP_HT.RegistrationDate <= @end)  AND (SmallClassificationCode LIKE @team)";
			this.sqlSelectCommand1.Connection = this.sqlConnection1;
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@num", System.Data.SqlDbType.NVarChar, 50, "ItemNum"));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@draw", System.Data.SqlDbType.NVarChar, 50, "ItemDrawNum"));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.NVarChar, 50, "ItemName"));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@team", System.Data.SqlDbType.NVarChar, 50, "Team"));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@begin", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@end", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			// 
			// sqlUpdateCommand1
			// 
			this.sqlUpdateCommand1.CommandText = "UPDATE WDWP_HT SET ItemNum = @ItemNum, ItemDrawNum = @ItemDrawNum, ItemName = @It" +
				"emName, ProcessSequenceNum = @ProcessSequenceNum, ProcessCode = @ProcessCode, Pr" +
				"ocessName = @ProcessName, ProductItemNum = @ProductItemNum, ProductDrawNum = @Pr" +
				"oductDrawNum, ProductName = @ProductName, ParentItemNum = @ParentItemNum, Parent" +
				"DrawNum = @ParentDrawNum, ParentName = @ParentName, WCName = @WCName, WorkDistin" +
				"ction = @WorkDistinction, WorkPlanQuantity = @WorkPlanQuantity, WorkCompletionQu" +
				"antity = @WorkCompletionQuantity, OrderLeadTime = @OrderLeadTime, WorkDate = @Wo" +
				"rkDate, ProgressCondition = @ProgressCondition, RegistrationPerson = @Registrati" +
				"onPerson, RegistrationPersonID = @RegistrationPersonID, RegistrationDate = @Regi" +
				"strationDate, UpdatingPerson = @UpdatingPerson, UpdatingPersonID = @UpdatingPers" +
				"onID, UpdatingDate = @UpdatingDate, ProductionPlanHistoryIndex = @ProductionPlan" +
				"HistoryIndex WHERE (WCDailyWorkPlanHistoryIndex = @Original_WCDailyWorkPlanHisto" +
				"ryIndex) AND (ProductionPlanHistoryIndex = @Original_Expr1 OR @Original_Expr1 IS" +
				" NULL AND ProductionPlanHistoryIndex IS NULL) AND (ItemDrawNum = @Original_ItemD" +
				"rawNum OR @Original_ItemDrawNum IS NULL AND ItemDrawNum IS NULL) AND (ItemName =" +
				" @Original_ItemName OR @Original_ItemName IS NULL AND ItemName IS NULL) AND (Ite" +
				"mNum = @Original_ItemNum OR @Original_ItemNum IS NULL AND ItemNum IS NULL) AND (" +
				"OrderLeadTime = @Original_OrderLeadTime OR @Original_OrderLeadTime IS NULL AND O" +
				"rderLeadTime IS NULL) AND (ParentDrawNum = @Original_ParentDrawNum OR @Original_" +
				"ParentDrawNum IS NULL AND ParentDrawNum IS NULL) AND (ParentItemNum = @Original_" +
				"ParentItemNum OR @Original_ParentItemNum IS NULL AND ParentItemNum IS NULL) AND " +
				"(ParentName = @Original_ParentName OR @Original_ParentName IS NULL AND ParentNam" +
				"e IS NULL) AND (ProcessCode = @Original_ProcessCode OR @Original_ProcessCode IS " +
				"NULL AND ProcessCode IS NULL) AND (ProcessName = @Original_ProcessName OR @Origi" +
				"nal_ProcessName IS NULL AND ProcessName IS NULL) AND (ProcessSequenceNum = @Orig" +
				"inal_ProcessSequenceNum OR @Original_ProcessSequenceNum IS NULL AND ProcessSeque" +
				"nceNum IS NULL) AND (ProductDrawNum = @Original_ProductDrawNum OR @Original_Prod" +
				"uctDrawNum IS NULL AND ProductDrawNum IS NULL) AND (ProductItemNum = @Original_P" +
				"roductItemNum OR @Original_ProductItemNum IS NULL AND ProductItemNum IS NULL) AN" +
				"D (ProductName = @Original_ProductName OR @Original_ProductName IS NULL AND Prod" +
				"uctName IS NULL) AND (ProductionPlanHistoryIndex = @Original_ProductionPlanHisto" +
				"ryIndex OR @Original_ProductionPlanHistoryIndex IS NULL AND ProductionPlanHistor" +
				"yIndex IS NULL) AND (ProgressCondition = @Original_ProgressCondition OR @Origina" +
				"l_ProgressCondition IS NULL AND ProgressCondition IS NULL) AND (RegistrationDate" +
				" = @Original_RegistrationDate OR @Original_RegistrationDate IS NULL AND Registra" +
				"tionDate IS NULL) AND (RegistrationPerson = @Original_RegistrationPerson OR @Ori" +
				"ginal_RegistrationPerson IS NULL AND RegistrationPerson IS NULL) AND (Registrati" +
				"onPersonID = @Original_RegistrationPersonID OR @Original_RegistrationPersonID IS" +
				" NULL AND RegistrationPersonID IS NULL) AND (UpdatingDate = @Original_UpdatingDa" +
				"te OR @Original_UpdatingDate IS NULL AND UpdatingDate IS NULL) AND (UpdatingPers" +
				"on = @Original_UpdatingPerson OR @Original_UpdatingPerson IS NULL AND UpdatingPe" +
				"rson IS NULL) AND (UpdatingPersonID = @Original_UpdatingPersonID OR @Original_Up" +
				"datingPersonID IS NULL AND UpdatingPersonID IS NULL) AND (WCName = @Original_WCN" +
				"ame OR @Original_WCName IS NULL AND WCName IS NULL) AND (WorkCompletionQuantity " +
				"= @Original_WorkCompletionQuantity OR @Original_WorkCompletionQuantity IS NULL A" +
				"ND WorkCompletionQuantity IS NULL) AND (WorkDate = @Original_WorkDate OR @Origin" +
				"al_WorkDate IS NULL AND WorkDate IS NULL) AND (WorkDistinction = @Original_WorkD" +
				"istinction OR @Original_WorkDistinction IS NULL AND WorkDistinction IS NULL) AND" +
				" (WorkPlanQuantity = @Original_WorkPlanQuantity OR @Original_WorkPlanQuantity IS" +
				" NULL AND WorkPlanQuantity IS NULL); SELECT ItemNum, ItemDrawNum, ItemName, Proc" +
				"essSequenceNum, ProcessCode, ProcessName, ProductItemNum, ProductDrawNum, Produc" +
				"tName, ParentItemNum, ParentDrawNum, ParentName, WCName, WorkDistinction, WorkPl" +
				"anQuantity, WorkCompletionQuantity, OrderLeadTime, WorkDate, ProgressCondition, " +
				"RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, Upda" +
				"tingPersonID, UpdatingDate, ProductionPlanHistoryIndex, ProductionPlanHistoryInd" +
				"ex AS Expr1, WCDailyWorkPlanHistoryIndex FROM WDWP_HT WHERE (WCDailyWorkPlanHist" +
				"oryIndex = @WCDailyWorkPlanHistoryIndex)";
			this.sqlUpdateCommand1.Connection = this.sqlConnection1;
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.NVarChar, 50, "ItemNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, "ItemDrawNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemName", System.Data.SqlDbType.NVarChar, 50, "ItemName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessSequenceNum", System.Data.SqlDbType.TinyInt, 1, "ProcessSequenceNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessCode", System.Data.SqlDbType.NVarChar, 50, "ProcessCode"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProcessName", System.Data.SqlDbType.NVarChar, 50, "ProcessName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductItemNum", System.Data.SqlDbType.NVarChar, 50, "ProductItemNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductDrawNum", System.Data.SqlDbType.NVarChar, 50, "ProductDrawNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductName", System.Data.SqlDbType.NVarChar, 50, "ProductName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentItemNum", System.Data.SqlDbType.NVarChar, 50, "ParentItemNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentDrawNum", System.Data.SqlDbType.NVarChar, 50, "ParentDrawNum"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ParentName", System.Data.SqlDbType.NVarChar, 50, "ParentName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WCName", System.Data.SqlDbType.NVarChar, 50, "WCName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkDistinction", System.Data.SqlDbType.NVarChar, 50, "WorkDistinction"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkPlanQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WorkPlanQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkCompletionQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WorkCompletionQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderLeadTime", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderLeadTime", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkDate", System.Data.SqlDbType.DateTime, 4, "WorkDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressCondition", System.Data.SqlDbType.NVarChar, 50, "ProgressCondition"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, "RegistrationPerson"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, "RegistrationPersonID"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationDate", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, "UpdatingPerson"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, "UpdatingPersonID"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingDate", System.Data.SqlDbType.DateTime, 4, "UpdatingDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductionPlanHistoryIndex", System.Data.SqlDbType.Int, 4, "Expr1"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WCDailyWorkPlanHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WCDailyWorkPlanHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Expr1", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Expr1", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OrderLeadTime", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderLeadTime", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ParentDrawNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ParentDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ParentItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ParentItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ParentName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ParentName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessCode", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessCode", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProcessSequenceNum", System.Data.SqlDbType.TinyInt, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProcessSequenceNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductDrawNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductionPlanHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Expr1", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressCondition", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProgressCondition", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPerson", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPerson", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WCName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WCName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WorkCompletionQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WorkCompletionQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WorkDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WorkDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WorkDistinction", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WorkDistinction", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WorkPlanQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WorkPlanQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WCDailyWorkPlanHistoryIndex", System.Data.SqlDbType.Int, 4, "WCDailyWorkPlanHistoryIndex"));
			// 
			// sqlDataAdapter2
			// 
			this.sqlDataAdapter2.DeleteCommand = this.sqlDeleteCommand2;
			this.sqlDataAdapter2.InsertCommand = this.sqlInsertCommand2;
			this.sqlDataAdapter2.SelectCommand = this.sqlSelectCommand2;
			this.sqlDataAdapter2.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "OOR_HT", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ItemNum", "ItemNum"),
																																																				new System.Data.Common.DataColumnMapping("ItemDrawNum", "ItemDrawNum"),
																																																				new System.Data.Common.DataColumnMapping("ItemName", "ItemName"),
																																																				new System.Data.Common.DataColumnMapping("UnitCostDistinction", "UnitCostDistinction"),
																																																				new System.Data.Common.DataColumnMapping("BeginProcessCode", "BeginProcessCode"),
																																																				new System.Data.Common.DataColumnMapping("BeginProcess", "BeginProcess"),
																																																				new System.Data.Common.DataColumnMapping("EndProcessCode", "EndProcessCode"),
																																																				new System.Data.Common.DataColumnMapping("EndProcess", "EndProcess"),
																																																				new System.Data.Common.DataColumnMapping("ApplyUnitCost", "ApplyUnitCost"),
																																																				new System.Data.Common.DataColumnMapping("FirstDeliveryDemandQuantity", "FirstDeliveryDemandQuantity"),
																																																				new System.Data.Common.DataColumnMapping("FirstDeliveryDemandDate", "FirstDeliveryDemandDate"),
																																																				new System.Data.Common.DataColumnMapping("SecondDeliveryDemandQuantity", "SecondDeliveryDemandQuantity"),
																																																				new System.Data.Common.DataColumnMapping("SecondDeliveryDemandDate", "SecondDeliveryDemandDate"),
																																																				new System.Data.Common.DataColumnMapping("ThirdDeliveryDemandQuantity", "ThirdDeliveryDemandQuantity"),
																																																				new System.Data.Common.DataColumnMapping("ThirdDeliveryDemandDate", "ThirdDeliveryDemandDate"),
																																																				new System.Data.Common.DataColumnMapping("FourthDeliveryDemandQuantity", "FourthDeliveryDemandQuantity"),
																																																				new System.Data.Common.DataColumnMapping("FourthDeliveryDemandDate", "FourthDeliveryDemandDate"),
																																																				new System.Data.Common.DataColumnMapping("FifthDeliveryDemandQuantity", "FifthDeliveryDemandQuantity"),
																																																				new System.Data.Common.DataColumnMapping("FifthDeliveryDemandDate", "FifthDeliveryDemandDate"),
																																																				new System.Data.Common.DataColumnMapping("OrderQuantity", "OrderQuantity"),
																																																				new System.Data.Common.DataColumnMapping("TotalCost", "TotalCost"),
																																																				new System.Data.Common.DataColumnMapping("VolumNum", "VolumNum"),
																																																				new System.Data.Common.DataColumnMapping("ProgressCondition", "ProgressCondition"),
																																																				new System.Data.Common.DataColumnMapping("RegistrationPerson", "RegistrationPerson"),
																																																				new System.Data.Common.DataColumnMapping("RegistrationPersonID", "RegistrationPersonID"),
																																																				new System.Data.Common.DataColumnMapping("RegistrationDate", "RegistrationDate"),
																																																				new System.Data.Common.DataColumnMapping("UpdatingPerson", "UpdatingPerson"),
																																																				new System.Data.Common.DataColumnMapping("UpdatingPersonID", "UpdatingPersonID"),
																																																				new System.Data.Common.DataColumnMapping("UpdatingDate", "UpdatingDate"),
																																																				new System.Data.Common.DataColumnMapping("ProductionPlanHistoryIndex", "ProductionPlanHistoryIndex"),
																																																				new System.Data.Common.DataColumnMapping("WCDailyWorkPlanHistoryIndex", "WCDailyWorkPlanHistoryIndex"),
																																																				new System.Data.Common.DataColumnMapping("OutSideOrderRequestHistoryIndex", "OutSideOrderRequestHistoryIndex")})});
			this.sqlDataAdapter2.UpdateCommand = this.sqlUpdateCommand2;
			// 
			// sqlDeleteCommand2
			// 
			this.sqlDeleteCommand2.CommandText = "DELETE FROM OOR_HT WHERE (OutSideOrderRequestHistoryIndex = @Original_OutSideOrde" +
				"rRequestHistoryIndex) AND (ApplyUnitCost = @Original_ApplyUnitCost OR @Original_" +
				"ApplyUnitCost IS NULL AND ApplyUnitCost IS NULL) AND (BeginProcess = @Original_B" +
				"eginProcess OR @Original_BeginProcess IS NULL AND BeginProcess IS NULL) AND (Beg" +
				"inProcessCode = @Original_BeginProcessCode OR @Original_BeginProcessCode IS NULL" +
				" AND BeginProcessCode IS NULL) AND (EndProcess = @Original_EndProcess OR @Origin" +
				"al_EndProcess IS NULL AND EndProcess IS NULL) AND (EndProcessCode = @Original_En" +
				"dProcessCode OR @Original_EndProcessCode IS NULL AND EndProcessCode IS NULL) AND" +
				" (FifthDeliveryDemandDate = @Original_FifthDeliveryDemandDate OR @Original_Fifth" +
				"DeliveryDemandDate IS NULL AND FifthDeliveryDemandDate IS NULL) AND (FifthDelive" +
				"ryDemandQuantity = @Original_FifthDeliveryDemandQuantity OR @Original_FifthDeliv" +
				"eryDemandQuantity IS NULL AND FifthDeliveryDemandQuantity IS NULL) AND (FirstDel" +
				"iveryDemandDate = @Original_FirstDeliveryDemandDate OR @Original_FirstDeliveryDe" +
				"mandDate IS NULL AND FirstDeliveryDemandDate IS NULL) AND (FirstDeliveryDemandQu" +
				"antity = @Original_FirstDeliveryDemandQuantity OR @Original_FirstDeliveryDemandQ" +
				"uantity IS NULL AND FirstDeliveryDemandQuantity IS NULL) AND (FourthDeliveryDema" +
				"ndDate = @Original_FourthDeliveryDemandDate OR @Original_FourthDeliveryDemandDat" +
				"e IS NULL AND FourthDeliveryDemandDate IS NULL) AND (FourthDeliveryDemandQuantit" +
				"y = @Original_FourthDeliveryDemandQuantity OR @Original_FourthDeliveryDemandQuan" +
				"tity IS NULL AND FourthDeliveryDemandQuantity IS NULL) AND (ItemDrawNum = @Origi" +
				"nal_ItemDrawNum OR @Original_ItemDrawNum IS NULL AND ItemDrawNum IS NULL) AND (I" +
				"temName = @Original_ItemName OR @Original_ItemName IS NULL AND ItemName IS NULL)" +
				" AND (ItemNum = @Original_ItemNum OR @Original_ItemNum IS NULL AND ItemNum IS NU" +
				"LL) AND (OrderQuantity = @Original_OrderQuantity OR @Original_OrderQuantity IS N" +
				"ULL AND OrderQuantity IS NULL) AND (ProductionPlanHistoryIndex = @Original_Produ" +
				"ctionPlanHistoryIndex OR @Original_ProductionPlanHistoryIndex IS NULL AND Produc" +
				"tionPlanHistoryIndex IS NULL) AND (WCDailyWorkPlanHistoryIndex = @Original_WCDai" +
				"lyWorkPlanHistoryIndex OR @Original_WCDailyWorkPlanHistoryIndex IS NULL AND WCDa" +
				"ilyWorkPlanHistoryIndex IS NULL) AND (ProgressCondition = @Original_ProgressCond" +
				"ition OR @Original_ProgressCondition IS NULL AND ProgressCondition IS NULL) AND " +
				"(RegistrationDate = @Original_RegistrationDate OR @Original_RegistrationDate IS " +
				"NULL AND RegistrationDate IS NULL) AND (RegistrationPerson = @Original_Registrat" +
				"ionPerson OR @Original_RegistrationPerson IS NULL AND RegistrationPerson IS NULL" +
				") AND (RegistrationPersonID = @Original_RegistrationPersonID OR @Original_Regist" +
				"rationPersonID IS NULL AND RegistrationPersonID IS NULL) AND (SecondDeliveryDema" +
				"ndDate = @Original_SecondDeliveryDemandDate OR @Original_SecondDeliveryDemandDat" +
				"e IS NULL AND SecondDeliveryDemandDate IS NULL) AND (SecondDeliveryDemandQuantit" +
				"y = @Original_SecondDeliveryDemandQuantity OR @Original_SecondDeliveryDemandQuan" +
				"tity IS NULL AND SecondDeliveryDemandQuantity IS NULL) AND (ThirdDeliveryDemandD" +
				"ate = @Original_ThirdDeliveryDemandDate OR @Original_ThirdDeliveryDemandDate IS " +
				"NULL AND ThirdDeliveryDemandDate IS NULL) AND (ThirdDeliveryDemandQuantity = @Or" +
				"iginal_ThirdDeliveryDemandQuantity OR @Original_ThirdDeliveryDemandQuantity IS N" +
				"ULL AND ThirdDeliveryDemandQuantity IS NULL) AND (TotalCost = @Original_TotalCos" +
				"t OR @Original_TotalCost IS NULL AND TotalCost IS NULL) AND (UnitCostDistinction" +
				" = @Original_UnitCostDistinction OR @Original_UnitCostDistinction IS NULL AND Un" +
				"itCostDistinction IS NULL) AND (UpdatingDate = @Original_UpdatingDate OR @Origin" +
				"al_UpdatingDate IS NULL AND UpdatingDate IS NULL) AND (UpdatingPerson = @Origina" +
				"l_UpdatingPerson OR @Original_UpdatingPerson IS NULL AND UpdatingPerson IS NULL)" +
				" AND (UpdatingPersonID = @Original_UpdatingPersonID OR @Original_UpdatingPersonI" +
				"D IS NULL AND UpdatingPersonID IS NULL) AND (VolumNum = @Original_VolumNum OR @O" +
				"riginal_VolumNum IS NULL AND VolumNum IS NULL)";
			this.sqlDeleteCommand2.Connection = this.sqlConnection1;
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OutSideOrderRequestHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OutSideOrderRequestHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BeginProcess", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BeginProcess", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BeginProcessCode", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BeginProcessCode", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EndProcess", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EndProcess", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EndProcessCode", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EndProcessCode", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FifthDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "FifthDeliveryDemandDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FifthDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FifthDeliveryDemandQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FirstDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "FirstDeliveryDemandDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FirstDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FirstDeliveryDemandQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FourthDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "FourthDeliveryDemandDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FourthDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FourthDeliveryDemandQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductionPlanHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductionPlanHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressCondition", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProgressCondition", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPerson", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SecondDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "SecondDeliveryDemandDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SecondDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "SecondDeliveryDemandQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ThirdDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ThirdDeliveryDemandDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ThirdDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThirdDeliveryDemandQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TotalCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalCost", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UnitCostDistinction", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UnitCostDistinction", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPerson", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_VolumNum", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "VolumNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WCDailyWorkPlanHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WCDailyWorkPlanHistoryIndex", System.Data.DataRowVersion.Original, null));
			// 
			// sqlInsertCommand2
			// 
			this.sqlInsertCommand2.CommandText = "INSERT INTO OOR_HT(ItemNum, ItemDrawNum, ItemName, UnitCostDistinction, BeginProc" +
				"essCode, BeginProcess, EndProcessCode, EndProcess, ApplyUnitCost, FirstDeliveryD" +
				"emandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeli" +
				"veryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeli" +
				"veryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, Fifth" +
				"DeliveryDemandDate, OrderQuantity, TotalCost, VolumNum, ProgressCondition, Regis" +
				"trationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingP" +
				"ersonID, UpdatingDate, ProductionPlanHistoryIndex,WCDailyWorkPlanHistoryIndex) V" +
				"ALUES (@ItemNum, @ItemDrawNum, @ItemName, @UnitCostDistinction, @BeginProcessCod" +
				"e, @BeginProcess, @EndProcessCode, @EndProcess, @ApplyUnitCost, @FirstDeliveryDe" +
				"mandQuantity, @FirstDeliveryDemandDate, @SecondDeliveryDemandQuantity, @SecondDe" +
				"liveryDemandDate, @ThirdDeliveryDemandQuantity, @ThirdDeliveryDemandDate, @Fourt" +
				"hDeliveryDemandQuantity, @FourthDeliveryDemandDate, @FifthDeliveryDemandQuantity" +
				", @FifthDeliveryDemandDate, @OrderQuantity, @TotalCost, @VolumNum, @ProgressCond" +
				"ition, @RegistrationPerson, @RegistrationPersonID, @RegistrationDate, @UpdatingP" +
				"erson, @UpdatingPersonID, @UpdatingDate, @ProductionPlanHistoryIndex,@WCDailyWor" +
				"kPlanHistoryIndex); SELECT ItemNum, ItemDrawNum, ItemName, UnitCostDistinction, " +
				"BeginProcessCode, BeginProcess, EndProcessCode, EndProcess, ApplyUnitCost, First" +
				"DeliveryDemandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, S" +
				"econdDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, F" +
				"ourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuanti" +
				"ty, FifthDeliveryDemandDate, OrderQuantity, TotalCost, VolumNum, ProgressConditi" +
				"on, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, " +
				"UpdatingPersonID, UpdatingDate, ProductionPlanHistoryIndex,WCDailyWorkPlanHistor" +
				"yIndex, OutSideOrderRequestHistoryIndex FROM OOR_HT WHERE (OutSideOrderRequestHi" +
				"storyIndex = @@IDENTITY)";
			this.sqlInsertCommand2.Connection = this.sqlConnection1;
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.NVarChar, 50, "ItemNum"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, "ItemDrawNum"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemName", System.Data.SqlDbType.NVarChar, 50, "ItemName"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnitCostDistinction", System.Data.SqlDbType.NVarChar, 50, "UnitCostDistinction"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BeginProcessCode", System.Data.SqlDbType.NVarChar, 50, "BeginProcessCode"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BeginProcess", System.Data.SqlDbType.NVarChar, 50, "BeginProcess"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndProcessCode", System.Data.SqlDbType.NVarChar, 50, "EndProcessCode"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndProcess", System.Data.SqlDbType.NVarChar, 50, "EndProcess"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FirstDeliveryDemandQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, "FirstDeliveryDemandDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecondDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "SecondDeliveryDemandQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecondDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, "SecondDeliveryDemandDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ThirdDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThirdDeliveryDemandQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ThirdDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, "ThirdDeliveryDemandDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FourthDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FourthDeliveryDemandQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FourthDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, "FourthDeliveryDemandDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FifthDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FifthDeliveryDemandQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FifthDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, "FifthDeliveryDemandDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalCost", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@VolumNum", System.Data.SqlDbType.Int, 4, "VolumNum"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressCondition", System.Data.SqlDbType.NVarChar, 50, "ProgressCondition"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, "RegistrationPerson"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, "RegistrationPersonID"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationDate", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, "UpdatingPerson"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, "UpdatingPersonID"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingDate", System.Data.SqlDbType.DateTime, 4, "UpdatingDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductionPlanHistoryIndex", System.Data.SqlDbType.Int, 4, "ProductionPlanHistoryIndex"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WCDailyWorkPlanHistoryIndex", System.Data.SqlDbType.Int, 4, "WCDailyWorkPlanHistoryIndex"));
			// 
			// sqlSelectCommand2
			// 
			this.sqlSelectCommand2.CommandText = @"SELECT ItemNum, ItemDrawNum, ItemName, UnitCostDistinction, BeginProcessCode, BeginProcess, EndProcessCode, EndProcess, ApplyUnitCost, FirstDeliveryDemandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, OrderQuantity, TotalCost, VolumNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, ProductionPlanHistoryIndex,WCDailyWorkPlanHistoryIndex, OutSideOrderRequestHistoryIndex FROM OOR_HT WHERE (ItemNum IS NULL)";
			this.sqlSelectCommand2.Connection = this.sqlConnection1;
			// 
			// sqlUpdateCommand2
			// 
			this.sqlUpdateCommand2.CommandText = "UPDATE OOR_HT SET ItemNum = @ItemNum, ItemDrawNum = @ItemDrawNum, ItemName = @Ite" +
				"mName, UnitCostDistinction = @UnitCostDistinction, BeginProcessCode = @BeginProc" +
				"essCode, BeginProcess = @BeginProcess, EndProcessCode = @EndProcessCode, EndProc" +
				"ess = @EndProcess, ApplyUnitCost = @ApplyUnitCost, FirstDeliveryDemandQuantity =" +
				" @FirstDeliveryDemandQuantity, FirstDeliveryDemandDate = @FirstDeliveryDemandDat" +
				"e, SecondDeliveryDemandQuantity = @SecondDeliveryDemandQuantity, SecondDeliveryD" +
				"emandDate = @SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity = @ThirdDeliv" +
				"eryDemandQuantity, ThirdDeliveryDemandDate = @ThirdDeliveryDemandDate, FourthDel" +
				"iveryDemandQuantity = @FourthDeliveryDemandQuantity, FourthDeliveryDemandDate = " +
				"@FourthDeliveryDemandDate, FifthDeliveryDemandQuantity = @FifthDeliveryDemandQua" +
				"ntity, FifthDeliveryDemandDate = @FifthDeliveryDemandDate, OrderQuantity = @Orde" +
				"rQuantity, TotalCost = @TotalCost, VolumNum = @VolumNum, ProgressCondition = @Pr" +
				"ogressCondition, RegistrationPerson = @RegistrationPerson, RegistrationPersonID " +
				"= @RegistrationPersonID, RegistrationDate = @RegistrationDate, UpdatingPerson = " +
				"@UpdatingPerson, UpdatingPersonID = @UpdatingPersonID, UpdatingDate = @UpdatingD" +
				"ate, ProductionPlanHistoryIndex = @ProductionPlanHistoryIndex WHERE (OutSideOrde" +
				"rRequestHistoryIndex = @Original_OutSideOrderRequestHistoryIndex) AND (ApplyUnit" +
				"Cost = @Original_ApplyUnitCost OR @Original_ApplyUnitCost IS NULL AND ApplyUnitC" +
				"ost IS NULL) AND (BeginProcess = @Original_BeginProcess OR @Original_BeginProces" +
				"s IS NULL AND BeginProcess IS NULL) AND (BeginProcessCode = @Original_BeginProce" +
				"ssCode OR @Original_BeginProcessCode IS NULL AND BeginProcessCode IS NULL) AND (" +
				"EndProcess = @Original_EndProcess OR @Original_EndProcess IS NULL AND EndProcess" +
				" IS NULL) AND (EndProcessCode = @Original_EndProcessCode OR @Original_EndProcess" +
				"Code IS NULL AND EndProcessCode IS NULL) AND (FifthDeliveryDemandDate = @Origina" +
				"l_FifthDeliveryDemandDate OR @Original_FifthDeliveryDemandDate IS NULL AND Fifth" +
				"DeliveryDemandDate IS NULL) AND (FifthDeliveryDemandQuantity = @Original_FifthDe" +
				"liveryDemandQuantity OR @Original_FifthDeliveryDemandQuantity IS NULL AND FifthD" +
				"eliveryDemandQuantity IS NULL) AND (FirstDeliveryDemandDate = @Original_FirstDel" +
				"iveryDemandDate OR @Original_FirstDeliveryDemandDate IS NULL AND FirstDeliveryDe" +
				"mandDate IS NULL) AND (FirstDeliveryDemandQuantity = @Original_FirstDeliveryDema" +
				"ndQuantity OR @Original_FirstDeliveryDemandQuantity IS NULL AND FirstDeliveryDem" +
				"andQuantity IS NULL) AND (FourthDeliveryDemandDate = @Original_FourthDeliveryDem" +
				"andDate OR @Original_FourthDeliveryDemandDate IS NULL AND FourthDeliveryDemandDa" +
				"te IS NULL) AND (FourthDeliveryDemandQuantity = @Original_FourthDeliveryDemandQu" +
				"antity OR @Original_FourthDeliveryDemandQuantity IS NULL AND FourthDeliveryDeman" +
				"dQuantity IS NULL) AND (ItemDrawNum = @Original_ItemDrawNum OR @Original_ItemDra" +
				"wNum IS NULL AND ItemDrawNum IS NULL) AND (ItemName = @Original_ItemName OR @Ori" +
				"ginal_ItemName IS NULL AND ItemName IS NULL) AND (ItemNum = @Original_ItemNum OR" +
				" @Original_ItemNum IS NULL AND ItemNum IS NULL) AND (OrderQuantity = @Original_O" +
				"rderQuantity OR @Original_OrderQuantity IS NULL AND OrderQuantity IS NULL) AND (" +
				"ProductionPlanHistoryIndex = @Original_ProductionPlanHistoryIndex OR @Original_P" +
				"roductionPlanHistoryIndex IS NULL AND ProductionPlanHistoryIndex IS NULL) AND (W" +
				"CDailyWorkPlanHistoryIndex = @Original_WCDailyWorkPlanHistoryIndex OR @Original_" +
				"WCDailyWorkPlanHistoryIndex IS NULL AND WCDailyWorkPlanHistoryIndex IS NULL) AND" +
				" (ProgressCondition = @Original_ProgressCondition OR @Original_ProgressCondition" +
				" IS NULL AND ProgressCondition IS NULL) AND (RegistrationDate = @Original_Regist" +
				"rationDate OR @Original_RegistrationDate IS NULL AND RegistrationDate IS NULL) A" +
				"ND (RegistrationPerson = @Original_RegistrationPerson OR @Original_RegistrationP" +
				"erson IS NULL AND RegistrationPerson IS NULL) AND (RegistrationPersonID = @Origi" +
				"nal_RegistrationPersonID OR @Original_RegistrationPersonID IS NULL AND Registrat" +
				"ionPersonID IS NULL) AND (SecondDeliveryDemandDate = @Original_SecondDeliveryDem" +
				"andDate OR @Original_SecondDeliveryDemandDate IS NULL AND SecondDeliveryDemandDa" +
				"te IS NULL) AND (SecondDeliveryDemandQuantity = @Original_SecondDeliveryDemandQu" +
				"antity OR @Original_SecondDeliveryDemandQuantity IS NULL AND SecondDeliveryDeman" +
				"dQuantity IS NULL) AND (ThirdDeliveryDemandDate = @Original_ThirdDeliveryDemandD" +
				"ate OR @Original_ThirdDeliveryDemandDate IS NULL AND ThirdDeliveryDemandDate IS " +
				"NULL) AND (ThirdDeliveryDemandQuantity = @Original_ThirdDeliveryDemandQuantity O" +
				"R @Original_ThirdDeliveryDemandQuantity IS NULL AND ThirdDeliveryDemandQuantity " +
				"IS NULL) AND (TotalCost = @Original_TotalCost OR @Original_TotalCost IS NULL AND" +
				" TotalCost IS NULL) AND (UnitCostDistinction = @Original_UnitCostDistinction OR " +
				"@Original_UnitCostDistinction IS NULL AND UnitCostDistinction IS NULL) AND (Upda" +
				"tingDate = @Original_UpdatingDate OR @Original_UpdatingDate IS NULL AND Updating" +
				"Date IS NULL) AND (UpdatingPerson = @Original_UpdatingPerson OR @Original_Updati" +
				"ngPerson IS NULL AND UpdatingPerson IS NULL) AND (UpdatingPersonID = @Original_U" +
				"pdatingPersonID OR @Original_UpdatingPersonID IS NULL AND UpdatingPersonID IS NU" +
				"LL) AND (VolumNum = @Original_VolumNum OR @Original_VolumNum IS NULL AND VolumNu" +
				"m IS NULL); SELECT ItemNum, ItemDrawNum, ItemName, UnitCostDistinction, BeginPro" +
				"cessCode, BeginProcess, EndProcessCode, EndProcess, ApplyUnitCost, FirstDelivery" +
				"DemandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDel" +
				"iveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDel" +
				"iveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, Fift" +
				"hDeliveryDemandDate, OrderQuantity, TotalCost, VolumNum, ProgressCondition, Regi" +
				"strationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, Updating" +
				"PersonID, UpdatingDate, ProductionPlanHistoryIndex,WCDailyWorkPlanHistoryIndex, " +
				"OutSideOrderRequestHistoryIndex FROM OOR_HT WHERE (OutSideOrderRequestHistoryInd" +
				"ex = @OutSideOrderRequestHistoryIndex)";
			this.sqlUpdateCommand2.Connection = this.sqlConnection1;
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.NVarChar, 50, "ItemNum"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, "ItemDrawNum"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemName", System.Data.SqlDbType.NVarChar, 50, "ItemName"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UnitCostDistinction", System.Data.SqlDbType.NVarChar, 50, "UnitCostDistinction"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BeginProcessCode", System.Data.SqlDbType.NVarChar, 50, "BeginProcessCode"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BeginProcess", System.Data.SqlDbType.NVarChar, 50, "BeginProcess"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndProcessCode", System.Data.SqlDbType.NVarChar, 50, "EndProcessCode"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EndProcess", System.Data.SqlDbType.NVarChar, 50, "EndProcess"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FirstDeliveryDemandQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, "FirstDeliveryDemandDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecondDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "SecondDeliveryDemandQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SecondDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, "SecondDeliveryDemandDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ThirdDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThirdDeliveryDemandQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ThirdDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, "ThirdDeliveryDemandDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FourthDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FourthDeliveryDemandQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FourthDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, "FourthDeliveryDemandDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FifthDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FifthDeliveryDemandQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FifthDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, "FifthDeliveryDemandDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalCost", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@VolumNum", System.Data.SqlDbType.Int, 4, "VolumNum"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressCondition", System.Data.SqlDbType.NVarChar, 50, "ProgressCondition"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, "RegistrationPerson"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, "RegistrationPersonID"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationDate", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, "UpdatingPerson"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, "UpdatingPersonID"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UpdatingDate", System.Data.SqlDbType.DateTime, 4, "UpdatingDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductionPlanHistoryIndex", System.Data.SqlDbType.Int, 4, "ProductionPlanHistoryIndex"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WCDailyWorkPlanHistoryIndex", System.Data.SqlDbType.Int, 4, "WCDailyWorkPlanHistoryIndex"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OutSideOrderRequestHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "OutSideOrderRequestHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ApplyUnitCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ApplyUnitCost", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BeginProcess", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BeginProcess", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BeginProcessCode", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BeginProcessCode", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EndProcess", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EndProcess", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_EndProcessCode", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EndProcessCode", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FifthDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "FifthDeliveryDemandDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FifthDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FifthDeliveryDemandQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FirstDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "FirstDeliveryDemandDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FirstDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FirstDeliveryDemandQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FourthDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "FourthDeliveryDemandDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_FourthDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "FourthDeliveryDemandQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemDrawNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemDrawNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemName", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_OrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "OrderQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductionPlanHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductionPlanHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WCDailyWorkPlanHistoryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WCDailyWorkPlanHistoryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressCondition", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProgressCondition", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPerson", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SecondDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "SecondDeliveryDemandDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_SecondDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "SecondDeliveryDemandQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ThirdDeliveryDemandDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ThirdDeliveryDemandDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ThirdDeliveryDemandQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "ThirdDeliveryDemandQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TotalCost", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalCost", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UnitCostDistinction", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UnitCostDistinction", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPerson", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPerson", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_UpdatingPersonID", System.Data.SqlDbType.NVarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "UpdatingPersonID", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_VolumNum", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "VolumNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OutSideOrderRequestHistoryIndex", System.Data.SqlDbType.Int, 4, "OutSideOrderRequestHistoryIndex"));
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Request_Click(object sender, System.EventArgs e)
		{
			ds = (DataSet)Session["ds"];
			ds_OOR = (DataSet)Session["ds_OOR"];

			Session["ds"] = ds;
			Session["ds_OOR"] = ds_OOR;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			try
			{
				for(int i = 0; i < UltraWebGrid1.Rows.Count; i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value == null)
					{
						UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value = false;
					}

					if(!bool.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value.ToString()))
					{
						// 각원장 등록함수
						DataRow[] row = ds.Tables[0].Select("WCDailyWorkPlanHistoryIndex = '" + UltraWebGrid1.Rows[i].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value + "'");
						ds.Tables[0].Rows.Remove(row[0]);
					}
				}

				//다시 바인딩
				UltraWebGrid1.DataSource = ds;
				UltraWebGrid1.DataBind();

				if(UltraWebGrid1.Rows.Count == 0)
				{
					throw new Exception("의뢰할 항목이 없습니다!");
				}
				else
				{
					Insert(conn,tr);

					sqlDataAdapter1.Update(ds);
					sqlDataAdapter2.Update(ds_OOR);

					Response.Write("<script language=javascript>");
					Response.Write("window.status = '의뢰 되었습니다!';");
					Response.Write("</script>");

					Session.Remove("ds_OOR");
					Session.Remove("ds");				
					
				}
				
				tr.Commit();
			}
			catch(Exception ee)
			{
				RegisterStartupScript("","<script language=javascript>alert('"+ee.Message+"');</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
				page_Load();
			}
		}

		private void page_Load()
		{
			
			// WC작업계획원장에서 작업구분이 외주,자가/외주이고 진행상태가 대기인것을 불러와서 보여준다.
			DataSet ds = new DataSet() ;
			sqlDataAdapter1.SelectCommand.Parameters["@num"].Value = '%'+ItemSearchControl1.ItemNum+'%';
			sqlDataAdapter1.SelectCommand.Parameters["@draw"].Value = '%'+ItemSearchControl1.ItemDrawNum+'%';
			sqlDataAdapter1.SelectCommand.Parameters["@name"].Value = '%'+ItemSearchControl1.ItemName+'%';
			if(wdc_FromDate.Value == null)
				sqlDataAdapter1.SelectCommand.Parameters["@begin"].Value = DateTime.Parse("1900-01-01").ToShortDateString();
			else
				sqlDataAdapter1.SelectCommand.Parameters["@begin"].Value = DateTime.Parse(wdc_FromDate.Value.ToString()).ToShortDateString();
			if(wdc_ToDate.Value == null)
				sqlDataAdapter1.SelectCommand.Parameters["@end"].Value = DateTime.Parse("2076-06-06").ToShortDateString();
			else
				sqlDataAdapter1.SelectCommand.Parameters["@end"].Value = DateTime.Parse(wdc_ToDate.Value.ToString()).ToShortDateString();

            sqlDataAdapter1.SelectCommand.Parameters["@team"].Value = '%'+ddlItemClassification1.SelectedItem.Value+'%';

			sqlDataAdapter1.Fill(ds);
			
			// 빈외주의뢰원장을 만든다
			DataSet ds_OOR = new DataSet();
			sqlDataAdapter2.Fill(ds_OOR);
			
			
			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();

			Session["ds"] = ds;
			Session["ds_OOR"] = ds_OOR;
		}

		private void Insert(SqlConnection con, SqlTransaction trans)
		{
			ds = (DataSet)Session["ds"];
			ds_OOR = (DataSet)Session["ds_OOR"];

			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			string str = "";

			decimal Unit = 0;

			// WC작업계획원장의 레코드를 데이터셋에 채운뒤 하나씩 꺼내서 꺼낸품목의 공정이 외주단가 종료공정과 동일한지
			// 파악한뒤 외주단가에 공정이 존재하지 않으면 그냥두고 존재하면 새로운 곳에 옮겨둔다.
			foreach(DataRow row in ds.Tables[0].Rows)
			{
			

				//품목,공정,거래처 하나를 저장한다.
				string item = "";
				string code="";
				decimal OutRate = 0;//외주리드타임(연속된 외주공정이면 리드타임이 누적된다)

				//단가테이블에 지금 공정이 존재하는지 파악한다.
				str = "Select Top 1 * From UCI_MT where RecodingState = 1 and ItemNum = @num and EndProcessCode=@code";
				
				comm.CommandText = str;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = row["ItemNum"].ToString();
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = row["ProcessCode"].ToString();
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					item = dr["ItemNum"].ToString();
					code = dr["EndProcessCode"].ToString();
				}
				dr.Close();
				comm.Parameters.Clear();

				if(code == "")
				{
					
					//없으면 지금 품목의 바로위에공정이 있는지 있으면 외주인지 보고 외주가 아니면 에러출력, 바로위공정이 없어도 에러출력
					if(row["ProductionPlanHistoryIndex"] == Convert.DBNull)
					{
						str = @"Select top 1 * From WDWP_HT Where ProcessSequenceNum >= @sequence and ItemNum = @num";
						comm.CommandText = str;
						comm.Parameters.Add("@sequence",int.Parse(row["ProcessSequenceNum"].ToString()));
						comm.Parameters.Add("@num",row["ItemNum"].ToString());					
					
					}
					else
					{
						str = @"Select top 1 * From WDWP_HT Where ProcessSequenceNum >= @sequence and ItemNum = @num and ProductionPlanHistoryIndex = @index";
						comm.CommandText = str;
						comm.Parameters.Add("@sequence",int.Parse(row["ProcessSequenceNum"].ToString()));
						comm.Parameters.Add("@num",row["ItemNum"].ToString());
						comm.Parameters.Add("@index",int.Parse(row["ProductionPlanHistoryIndex"].ToString()));
					
					}
					SqlDataAdapter da_up = new SqlDataAdapter(comm);
					DataSet ds_up = new DataSet();
					da_up.Fill(ds_up);
					//바로위 공정이 없거나 공정이 자가이면 에러출력
					if(ds_up.Tables[0].Rows.Count == 0 || row["WorkDistinction"].ToString() == "자가")
						throw new Exception("종료공정이 "+row["ProcessName"].ToString()+"인 "+row["ItemNum"].ToString()+"의 외주단가가 존재하지 않습니다!");
					else if(row["WorkDistinction"].ToString() == "자가/외주")
					{
						throw new Exception(row["ItemNum"].ToString()+"의 외주방법이 잘못되었습니다. 외주단가정보에서 수정해 주세요!");
					}
					else
					{
						//외주이면 해당row를 완료처리만 하고 넘어간다.
						row["ProgressCondition"] = "완료";
					}
					comm.Parameters.Clear();
				}
				else
				{
					OutRate += decimal.Parse(row["OrderLeadTime"].ToString());
					
					DataTable table = ds_OOR.Tables[0];
					DataRow datarow = table.NewRow();
				
				
					int sequence = 0;
					Queue que = new Queue();
					//존재한다면 지금공정의 이전공정이 존재하는지 파악해야 한다.
					if(item != "")
					{
						// 일단 무조건 레코드를 생성해 시작,종료공정에 넣어둔다.
						datarow["ItemNum"] = row["ItemNum"].ToString();
						datarow["ItemDrawNum"] = row["ItemDrawNum"].ToString();
						datarow["ItemName"] = row["ItemName"].ToString();
						datarow["BeginProcessCode"] = code;
						datarow["BeginProcess"] = process(code);
						datarow["EndProcessCode"] = code;
						datarow["EndProcess"] = process(code);
						datarow["UnitCostDistinction"] = "외주단가";
							
						//datarow["FirstDeliveryDemandQuantity"] = Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
						datarow["FirstDeliveryDemandQuantity"] = decimal.Parse(row["WorkPlanQuantity"].ToString());
						datarow["FirstDeliveryDemandDate"] = DateTime.Parse(row["WorkDate"].ToString()).AddDays(double.Parse(OutRate.ToString())).ToShortDateString();
						datarow["SecondDeliveryDemandQuantity"] = 0;
						datarow["ThirdDeliveryDemandQuantity"] = 0;
						datarow["FourthDeliveryDemandQuantity"] = 0;
						datarow["FifthDeliveryDemandQuantity"] = 0;
						//datarow["OrderQuantity"] =  Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
						datarow["OrderQuantity"] =  decimal.Parse(row["WorkPlanQuantity"].ToString());//

						str = "Select * From II_MT Where RecodingState = 1 and ItemNum = @item";
						comm.CommandText = str;
						comm.Parameters.Add("@item",SqlDbType.VarChar).Value = row["ItemNum"].ToString();
						SqlDataReader dr_Unit = comm.ExecuteReader();
						while(dr_Unit.Read())
						{
							Unit = decimal.Parse(dr_Unit["StandardUnitCost"].ToString());
							datarow["ApplyUnitCost"] = Unit;
							datarow["TotalCost"] = Unit * decimal.Parse(datarow["OrderQuantity"].ToString());
						}
						dr_Unit.Close();
						comm.Parameters.Clear();



						datarow["VolumNum"] = volum();
						datarow["ProgressCondition"] = "대기";
						datarow["RegistrationPerson"] = Session["UserName"].ToString();
						datarow["RegistrationPersonID"] = Session["ID"].ToString();
						datarow["RegistrationDate"] = DateTime.Now.ToShortDateString();
						if(row["ProductionPlanHistoryIndex"] != Convert.DBNull)
							datarow["ProductionPlanHistoryIndex"] = int.Parse(row["ProductionPlanHistoryIndex"].ToString());
						else
							datarow["WCDailyWorkPlanHistoryIndex"] = int.Parse(row["WCDailyWorkPlanHistoryIndex"].ToString());
                    
						if(row["WorkDistinction"].ToString() == "자가/외주")
							row["ProgressCondition"] = "진행";
						else
							row["ProgressCondition"] = "완료";


						// 현재공정을 다른 변수에 저장해서 사용.
						string precode = code;


						//지금공정의 이전공정이 존재하는지 파악하기위해 공정순서를 찾아본다.
						str = "Select ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ProcessCode = @code and ItemNum = @itemnum";
						comm.CommandText = str;
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
					
						SqlDataReader dr_num = comm.ExecuteReader();
						while(dr_num.Read())
						{
							sequence = int.Parse(dr_num["ProcessSequenceNum"].ToString());
						}
						dr_num.Close();
						comm.Parameters.Clear();

					

						// 이전공정이 존재한다면
						if(sequence > 0)
						{
							//지금 공정순서보다 아래에 공정순서가 있는지 없는지를 구한다
							//이전공정을 찾아 큐에 순서대로 넣는다
							str = "Select ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ProcessSequenceNum < @num and ItemNum = @itemnum order by ProcessSequenceNum desc";
							comm.CommandText = str;
							comm.Parameters.Add("@num",SqlDbType.Int).Value = sequence;
							comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
							SqlDataReader dr_sequence = comm.ExecuteReader();
							// 이전공정들을 큐에 순서대로 넣는다
							while(dr_sequence.Read())
							{
								que.Enqueue(int.Parse(dr_sequence["ProcessSequenceNum"].ToString()));
							}
							dr_sequence.Close();
							comm.Parameters.Clear();

							int count = que.Count;

							if(que.Count ==0)
								table.Rows.Add(datarow);
						
							// 큐에 들어있는 품목의 공정순서를 하나씩 꺼내서 그 공정의 작업구분이 외주인지 파악한다.
							while(count != 0)
							{
								// 꺼낸 공정이 외주인지 파악한다.
							
								string work="";
								string procode = "";
								str = "Select WorkDistinction,ProcessCode From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum = @sequence";
								comm.CommandText = str;
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
								comm.Parameters.Add("@sequence",SqlDbType.Int).Value = int.Parse(que.Dequeue().ToString());
								SqlDataReader dr_work =comm.ExecuteReader();
								while(dr_work.Read())
								{
									work = dr_work["WorkDistinction"].ToString();
									procode = dr_work["ProcessCode"].ToString();
								}
								dr_work.Close();
								comm.Parameters.Clear();

								//꺼내온 품목의 공정이 외주이면 단가테이블에 가서 선택되어진 품목의 공정이 종료공정에 있는지 파악한다.
								if(work == "외주" || work == "자가/외주")
								{
								
									str = "Select Count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @itemnum and EndProcessCode = @code";
									comm.CommandText = str;
									comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
									comm.Parameters.Add("@code",SqlDbType.VarChar).Value = procode;//이전공정의 공정코드
									int cou = int.Parse(comm.ExecuteScalar().ToString());
									comm.Parameters.Clear();
								
									// 존재하지 않으면 그 공정은 연속으로 포함된 공정이므로 일단 시작공정에 넣어둔다. 또한 그 
									if(cou == 0)
									{
										//외주리드타임을 계속 더해주어야 하므로 
										// 그 공정의 리드타임을 구해야 한다.
										str = "Select * From PSI_MT Where RecodingState = 1 and ItemNum = @item and ProcessCode = @code";
										comm.CommandText = str;
										comm.Parameters.Add("@item",SqlDbType.VarChar).Value = item;
										comm.Parameters.Add("@code",SqlDbType.VarChar).Value = procode;
										SqlDataReader dr_Rate = comm.ExecuteReader();
										while(dr_Rate.Read())
										{
											OutRate += decimal.Parse(dr_Rate["LeadTime"].ToString());
										}
										dr_Rate.Close();
										comm.Parameters.Clear();

									
									

									
										datarow["ItemNum"] = row["ItemNum"].ToString();
										datarow["ItemDrawNum"] = row["ItemDrawNum"].ToString();
										datarow["ItemName"] = row["ItemName"].ToString();
										datarow["BeginProcessCode"] = procode;
										datarow["BeginProcess"] = process(procode);
										datarow["EndProcessCode"] = code;
										datarow["EndProcess"] = process(code);
							
										//datarow["FirstDeliveryDemandQuantity"] = Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
										datarow["FirstDeliveryDemandQuantity"] = decimal.Parse(row["WorkPlanQuantity"].ToString());
										datarow["FirstDeliveryDemandDate"] = DateTime.Parse(row["WorkDate"].ToString()).AddDays(double.Parse(OutRate.ToString())).ToShortDateString();
										datarow["SecondDeliveryDemandQuantity"] = 0;
										datarow["ThirdDeliveryDemandQuantity"] = 0;
										datarow["FourthDeliveryDemandQuantity"] = 0;
										datarow["FifthDeliveryDemandQuantity"] = 0;
										//datarow["OrderQuantity"] =  Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
										datarow["OrderQuantity"] =  decimal.Parse(row["WorkPlanQuantity"].ToString());//


										str = "Select * From II_MT Where RecodingState = 1 and ItemNum = @item";
										comm.CommandText = str;
										comm.Parameters.Add("@item",SqlDbType.VarChar).Value = row["ItemNum"].ToString();
										SqlDataReader dr_Unit1 = comm.ExecuteReader();
										while(dr_Unit1.Read())
										{
											Unit = decimal.Parse(dr_Unit1["StandardUnitCost"].ToString());
											datarow["ApplyUnitCost"] = Unit;
											datarow["TotalCost"] = Unit * decimal.Parse(datarow["OrderQuantity"].ToString());
										}
										dr_Unit1.Close();
										comm.Parameters.Clear();


										datarow["VolumNum"] = volum();
										datarow["ProgressCondition"] = "대기";
										datarow["RegistrationPerson"] = Session["UserName"].ToString();
										datarow["RegistrationPersonID"] = Session["ID"].ToString();
										datarow["RegistrationDate"] = DateTime.Now.ToShortDateString();
										if(row["ProductionPlanHistoryIndex"] != Convert.DBNull)
											datarow["ProductionPlanHistoryIndex"] = int.Parse(row["ProductionPlanHistoryIndex"].ToString());
										else
											datarow["WCDailyWorkPlanHistoryIndex"] = int.Parse(row["WCDailyWorkPlanHistoryIndex"].ToString());
                    
										//다음을 위해 시작공정을 지금 찾은 공정으로 바꾼다.
										precode = procode;

										//진행상태 업데이트를 위해 WC원장에 표시를 해 둔다.
										row["ProgressCondition"] = "완료";
									
										//마지막까지 가도 외주단가가 존재하지 않으면 DataRow를 추가시켜준다.
										if(count == 0)
											table.Rows.Add(datarow);
										else if(que.Count ==0)
											table.Rows.Add(datarow);
									}
									else//외주단가에 존재하면 이어진 공정이 아니므로 그 전 공정까지 외주가 묶여진다.
									{
										datarow["ItemNum"] = row["ItemNum"].ToString();
										datarow["ItemDrawNum"] = row["ItemDrawNum"].ToString();
										datarow["ItemName"] = row["ItemName"].ToString();
										datarow["BeginProcessCode"] = precode;
										datarow["BeginProcess"] = process(precode);
										datarow["EndProcessCode"] = code;
										datarow["EndProcess"] = process(code);
							
										//datarow["FirstDeliveryDemandQuantity"] = Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
										datarow["FirstDeliveryDemandQuantity"] = decimal.Parse(row["WorkPlanQuantity"].ToString());
										datarow["FirstDeliveryDemandDate"] = DateTime.Parse(row["WorkDate"].ToString()).AddDays(double.Parse(OutRate.ToString())).ToShortDateString();
										datarow["SecondDeliveryDemandQuantity"] = 0;
										datarow["ThirdDeliveryDemandQuantity"] = 0;
										datarow["FourthDeliveryDemandQuantity"] = 0;
										datarow["FifthDeliveryDemandQuantity"] = 0;
										//datarow["OrderQuantity"] =  Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
										datarow["OrderQuantity"] =  decimal.Parse(row["WorkPlanQuantity"].ToString());//

										datarow["VolumNum"] = volum();
										datarow["ProgressCondition"] = "대기";
										datarow["RegistrationPerson"] = Session["UserName"].ToString();
										datarow["RegistrationPersonID"] = Session["ID"].ToString();
										datarow["RegistrationDate"] = DateTime.Now.ToShortDateString();
										if(row["ProductionPlanHistoryIndex"] != Convert.DBNull)
											datarow["ProductionPlanHistoryIndex"] = int.Parse(row["ProductionPlanHistoryIndex"].ToString());
										else
											datarow["WCDailyWorkPlanHistoryIndex"] = int.Parse(row["WCDailyWorkPlanHistoryIndex"].ToString());
                    
										//다음을 위해 시작공정을 지금 찾은 공정으로 바꾼다.
										precode = procode;

										//다음 WC원장 레코드를 찾으러 간다.(goto foreach(....))
										table.Rows.Add(datarow);
										break;
									
									}

								
								}
								else//이전공정이 자가이면 이전공정에 넣어두고빠져나온다
								{
									que.Clear();

									datarow["ItemNum"] = row["ItemNum"].ToString();
									datarow["ItemDrawNum"] = row["ItemDrawNum"].ToString();
									datarow["ItemName"] = row["ItemName"].ToString();
									datarow["BeginProcessCode"] = precode;
									datarow["BeginProcess"] = process(precode);
									datarow["EndProcessCode"] = code;
									datarow["EndProcess"] = process(code);
							
									//datarow["FirstDeliveryDemandQuantity"] = Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
									datarow["FirstDeliveryDemandQuantity"] = decimal.Parse(row["WorkPlanQuantity"].ToString());
									datarow["FirstDeliveryDemandDate"] = DateTime.Parse(row["WorkDate"].ToString()).AddDays(double.Parse(OutRate.ToString())).ToShortDateString();
									datarow["SecondDeliveryDemandQuantity"] = 0;
									datarow["ThirdDeliveryDemandQuantity"] = 0;
									datarow["FourthDeliveryDemandQuantity"] = 0;
									datarow["FifthDeliveryDemandQuantity"] = 0;
									//datarow["OrderQuantity"] =  Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
									datarow["OrderQuantity"] =  decimal.Parse(row["WorkPlanQuantity"].ToString());//


									str = "Select * From II_MT Where RecodingState = 1 and ItemNum = @item";
									comm.CommandText = str;
									comm.Parameters.Add("@item",SqlDbType.VarChar).Value = row["ItemNum"].ToString();
									SqlDataReader dr_Unit2 = comm.ExecuteReader();
									while(dr_Unit2.Read())
									{
										Unit = decimal.Parse(dr_Unit2["StandardUnitCost"].ToString());
										datarow["ApplyUnitCost"] = Unit;
										datarow["TotalCost"] = Unit * decimal.Parse(datarow["OrderQuantity"].ToString());
									}
									dr_Unit2.Close();
									comm.Parameters.Clear();




									datarow["VolumNum"] = volum();
									datarow["ProgressCondition"] = "대기";
									datarow["RegistrationPerson"] = Session["UserName"].ToString();
									datarow["RegistrationPersonID"] = Session["ID"].ToString();
									datarow["RegistrationDate"] = DateTime.Now.ToShortDateString();
									if(row["ProductionPlanHistoryIndex"] != Convert.DBNull)
										datarow["ProductionPlanHistoryIndex"] = int.Parse(row["ProductionPlanHistoryIndex"].ToString());
									else
										datarow["WCDailyWorkPlanHistoryIndex"] = int.Parse(row["WCDailyWorkPlanHistoryIndex"].ToString());
									//WC원장의 진행상태는 완료로 바꿔놓는다.
									row["ProgressCondition"] = "완료";

									if(count == 0)
									{
										table.Rows.Add(datarow);
										break;
									}
									else if(que.Count ==0)
									{
										table.Rows.Add(datarow);
										break;
									}

									//break;

								}//if(work == "외주" || work == "자가/외주")

								count = count-1;

							}//que.Count !=0

							

						
						}//if(sequence < 0)
						else//하위가 없으면 품목의 최초 공정이란의미이다. 특히 하위품목들의 갯수가 1이면 하위품은 원자재, 2이상이면 조립공정이란의미
						{

							// 하위공정이 존재하지 않으면 무조건 외주의뢰원장에 기록되어지는 것은 최초공정까지이다.
							datarow["ItemNum"] = row["ItemNum"].ToString();
							datarow["ItemDrawNum"] = row["ItemDrawNum"].ToString();
							datarow["ItemName"] = row["ItemName"].ToString();
							datarow["BeginProcessCode"] = precode;
							datarow["BeginProcess"] = process(precode);
							datarow["EndProcessCode"] = code;
							datarow["EndProcess"] = process(code);
							
							//datarow["FirstDeliveryDemandQuantity"] = Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
							datarow["FirstDeliveryDemandQuantity"] = decimal.Parse(row["WorkPlanQuantity"].ToString());
							datarow["FirstDeliveryDemandDate"] = DateTime.Parse(row["WorkDate"].ToString()).AddDays(double.Parse(OutRate.ToString())).ToShortDateString();
							datarow["SecondDeliveryDemandQuantity"] = 0;
							datarow["ThirdDeliveryDemandQuantity"] = 0;
							datarow["FourthDeliveryDemandQuantity"] = 0;
							datarow["FifthDeliveryDemandQuantity"] = 0;
							//datarow["OrderQuantity"] =  Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
							datarow["OrderQuantity"] =  decimal.Parse(row["WorkPlanQuantity"].ToString());//

							str = "Select * From II_MT Where RecodingState = 1 and ItemNum = @item";
							comm.CommandText = str;
							comm.Parameters.Add("@item",SqlDbType.VarChar).Value = row["ItemNum"].ToString();
							SqlDataReader dr_Unit3 = comm.ExecuteReader();
							while(dr_Unit3.Read())
							{
								Unit = decimal.Parse(dr_Unit3["StandardUnitCost"].ToString());
								datarow["ApplyUnitCost"] = Unit;
								datarow["TotalCost"] = Unit * decimal.Parse(datarow["OrderQuantity"].ToString());
							}
							dr_Unit3.Close();
							comm.Parameters.Clear();




							datarow["VolumNum"] = volum();
							datarow["ProgressCondition"] = "대기";
							datarow["RegistrationPerson"] = Session["UserName"].ToString();
							datarow["RegistrationPersonID"] = Session["ID"].ToString();
							datarow["RegistrationDate"] = DateTime.Now.ToShortDateString();
							if(row["ProductionPlanHistoryIndex"] != Convert.DBNull)
								datarow["ProductionPlanHistoryIndex"] = int.Parse(row["ProductionPlanHistoryIndex"].ToString());
							else
								datarow["WCDailyWorkPlanHistoryIndex"] = int.Parse(row["WCDailyWorkPlanHistoryIndex"].ToString());
                    
							//WC원장의 진행상태는 완료로 바꿔놓는다.
							row["ProgressCondition"] = "완료";

							// 하위공정이 존재하지 않으면 하위품목을 들을 찾아 스택에 넣어둔다.
							// 스택에 하나씩 꺼내면서 그 품목의 공정순서대로 다른 스택에 넣는다
							Stack st = new Stack();
							str = "Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @item";
							comm.CommandText = str;
							comm.Parameters.Add("@item",SqlDbType.VarChar).Value = item;
							SqlDataReader dr_IOI = comm.ExecuteReader();
							while(dr_IOI.Read())
							{
								st.Push(dr_IOI["ChildItemNum"].ToString());
							}
							dr_IOI.Close();
							comm.Parameters.Clear();

							if(st.Count != 0)
							{
								// 스택의 내용이 있으면 다 꺼낼때까지 계속 Pop한다
								while(st.Count > 0)
								{	
									item =st.Pop().ToString();

									// 꺼낸품목의 공정순서를 스택1에 넣는다
									Stack st1 = new Stack();					
									int sequencenum=0;

									//꺼낸 품목의 공정순서를 넣어둔다.
									str = "Select ProcessSequenceNum from PSI_MT where RecodingState = 1 AND ItemNum = @itemnum  order by ProcessSequenceNum";
									comm.CommandText =str;
									comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
									SqlDataReader dr_Sequence = comm.ExecuteReader();
									while(dr_Sequence.Read())
									{
										//공정순서
										st1.Push(int.Parse(dr_Sequence["ProcessSequenceNum"].ToString()));
									}
									dr_Sequence.Close();
									comm.Parameters.Clear();

									//공정순서가 없으면 지금 품목이 원자재이므로 기록할 필요는 없다.
									if(st1.Count != 0)
									{
									
										//그렇지 않으면 그 품목의 최상위 공정순서를 찾아서 그 공정이 외주인지 판단한다.
									

										// 스택1에 있는 공정순서를 하나씩꺼낸다.
										while(st1.Count > 0)
										{
											sequencenum = int.Parse(st1.Pop().ToString());
											string processcode = "";
											// 꺼낸 품목의 공정이 외주인지 확인한다. 외주이면 단가테이블에 공정이 존재하는지 확인한다.
											// 단가테이블에 공정이 존재하면 그 이전공정까지 외주나가는 것이므로 그 공정을 WC원장에서 표시해둔다.
											str = " SELECT  ProcessCode FROM PSI_MT WHERE RecodingState = 1 and WorkDistinction <> '자가' and ItemNum = @item and ProcessSequenceNum = @num";
											comm.CommandText = str;
											comm.Parameters.Add("@item",SqlDbType.VarChar).Value = item;//품목번호
											comm.Parameters.Add("@num",SqlDbType.Int).Value = sequencenum;//공정순서
											SqlDataReader dr_Code = comm.ExecuteReader();
											while(dr_Code.Read())
											{
												processcode = dr_Code["ProcessCode"].ToString();
											}
											dr_Code.Close();
											comm.Parameters.Clear();

											precode = processcode;

											// 공정이 외주이면 단가테이블에 공정이 존재하는지 확인...
											if(processcode != "")
											{
												str = "Select Count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @item and EndProcessCode = @code";
												comm.CommandText = str;
												comm.Parameters.Add("@item",SqlDbType.VarChar).Value = item;//품목번호
												comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;//공정코드
												int i = int.Parse(comm.ExecuteScalar().ToString());
												comm.Parameters.Clear();
										
												//단가테이블에 존재하지 않으면 외주이므로 그 공정을 가진 WC원장에 진행상태를 완료처리한다.
												if(i != 0)
												{
													//그공정의 외주리드타임은 누적시켜야한다.
													str = "Select OutsideOrderRate From PSI_MT Where RecodingState = 1 and ItemNum = @item and ProcessSequenceNum = @num";
													comm.CommandText = str;
													comm.Parameters.Add("@item",SqlDbType.VarChar).Value = item;//품목번호
													comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;//공정순서
													SqlDataReader dr_Rate = comm.ExecuteReader();
													while(dr_Rate.Read())
													{
														OutRate += decimal.Parse(dr_Rate["OutsideOrderRate"].ToString());
													}
													dr_Rate.Close();
													comm.Parameters.Clear();
												
													datarow["FirstDeliveryDemandDate"] = DateTime.Parse(row["WorkDate"].ToString()).AddDays(double.Parse(OutRate.ToString())).ToShortDateString();
											

													//WC원장의 진행상태는 완료로 바꿔준다.
													row["ProgressCondition"] = "완료";
												}//if(i != 0)

											}//if(processcode != "")

										}//while(st1.Count > 0)

									}//if(st1.Count != 0)

								}//while(st.Count > 0)

							}//if(st.Count != 0)

							table.Rows.Add(datarow);

						}//if(sequence < 0) 이전공정이 존재한다면

					}//if(item != "")
					else//외주이지만 단가테이블에 공정이 없는것이므로 
					{
						// 포함되어있는 공정일 수도 있고 사용자가 WC원장을 수정한 경우도 있다.
						// 이것을 구분해야 한다.
						str = "Select count(*) From PSI_MT Where RecodingState = 1 and ItemNum = @item and ProcessCode = @code";
						comm.CommandText = str;
						comm.Parameters.Add("@item",SqlDbType.VarChar).Value = item;
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
						int a = int.Parse(comm.ExecuteScalar().ToString());

						// 공정순서에 없으면서 외주인것은 사용자가 수정한 것이므로 바로 외주의뢰원장에 등록한다.
						if(a == 0)
						{
							datarow["ItemNum"] = row["ItemNum"].ToString();
							datarow["ItemDrawNum"] = row["ItemDrawNum"].ToString();
							datarow["ItemName"] = row["ItemName"].ToString();
							datarow["BeginProcessCode"] = code;
							datarow["BeginProcess"] = process(code);
							datarow["EndProcessCode"] = code;
							datarow["EndProcess"] = process(code);
							datarow["UnitCostDistinction"] = "외주단가";
							
							//datarow["FirstDeliveryDemandQuantity"] = Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
							datarow["FirstDeliveryDemandQuantity"] = decimal.Parse(row["WorkPlanQuantity"].ToString());
							datarow["FirstDeliveryDemandDate"] = DateTime.Parse(row["WorkDate"].ToString()).AddDays(double.Parse(OutRate.ToString())).ToShortDateString();
							datarow["SecondDeliveryDemandQuantity"] = 0;
							datarow["ThirdDeliveryDemandQuantity"] = 0;
							datarow["FourthDeliveryDemandQuantity"] = 0;
							datarow["FifthDeliveryDemandQuantity"] = 0;
							//datarow["OrderQuantity"] =  Quantity(row["ItemNum"].ToString(),code,decimal.Parse(row["WorkPlanQuantity"].ToString()));
							datarow["OrderQuantity"] =  decimal.Parse(row["WorkPlanQuantity"].ToString());//
							datarow["VolumNum"] = volum();


							str = "Select * From II_MT Where RecodingState = 1 and ItemNum = @item1";
							comm.CommandText = str;
							comm.Parameters.Add("@item1",SqlDbType.VarChar).Value = row["ItemNum"].ToString();
							SqlDataReader dr_Unit = comm.ExecuteReader();
							while(dr_Unit.Read())
							{
								Unit = decimal.Parse(dr_Unit["StandardUnitCost"].ToString());
								datarow["ApplyUnitCost"] = Unit;
								datarow["TotalCost"] = Unit * decimal.Parse(datarow["OrderQuantity"].ToString());
							}
							dr_Unit.Close();
							comm.Parameters.Clear();





							datarow["ProgressCondition"] = "대기";
							datarow["RegistrationPerson"] = Session["UserName"].ToString();
							datarow["RegistrationPersonID"] = Session["ID"].ToString();
							datarow["RegistrationDate"] = DateTime.Now.ToShortDateString();
							if(row["ProductionPlanHistoryIndex"] != Convert.DBNull)
								datarow["ProductionPlanHistoryIndex"] = int.Parse(row["ProductionPlanHistoryIndex"].ToString());
							else
								datarow["WCDailyWorkPlanHistoryIndex"] = int.Parse(row["WCDailyWorkPlanHistoryIndex"].ToString());
                    
							table.Rows.Add(datarow);
						}

						//WC원장의 진행상태는 완료로 바꿔놓는다.
						row["ProgressCondition"] = "완료";
					}//if(item != "")

				}//if(code != "")

			}//foreach(DataRow row in ds.Tables[0].Rows)


			

			Session["ds"] = ds;
			Session["ds_OOR"] = ds_OOR;
		}

		/// <summary>
		/// 코드명 호출함수
		/// </summary>
		/// <param name="aaa">코드번호</param>
		/// <returns></returns>
		private string process(string aaa)
		{
			string  Name = "";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = aaa;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Name = dr["SmallClassificationName"].ToString();
			}
			dr.Close();
			conn.Close();

			return Name;
		}

		/// <summary>
		/// 원장에 기록되어져있는 원장번호
		/// </summary>
		/// <returns></returns>
		private int volum()
		{
			int vol;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select Max(VolumNum) From OOR_HT";

			SqlCommand comm = new SqlCommand(str,conn);
			if(comm.ExecuteScalar() == Convert.DBNull)
				vol = 0;
			else
				vol = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();

			return vol+1;
		}


		/// <summary>
		/// 수량 구하는 함수
		/// </summary>
		/// <param name="Item"></param>
		/// <param name="Sequence"></param>
		/// <returns></returns>
		private decimal Quantity(string Item, string Code, decimal Total)
		{
			decimal quantity=0;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select * From PSI_MT Where RecodingState = 1 and ItemNum = @item and ProcessCode = @code";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@item",SqlDbType.VarChar).Value = Item;
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = Code;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				quantity = decimal.Parse(dr["OutsideOrderRate"].ToString())/100 * Total;
			}
			dr.Close();
			conn.Close();

			//return quantity;
			return Total;
		}

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			page_Load();
		}
	}
}
