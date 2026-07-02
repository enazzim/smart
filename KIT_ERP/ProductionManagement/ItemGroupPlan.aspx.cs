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

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// ItemGroupPlan에 대한 요약 설명입니다.
	/// </summary>
	public class ItemGroupPlan : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected System.Web.UI.WebControls.Button btSearch;
		protected System.Web.UI.WebControls.Button bt_Register;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected System.Web.UI.WebControls.Button Button3;
		protected KIT_ERP.ProductionManagement.dsPhantomErr dsPhantomErr1;
		protected System.Data.SqlClient.SqlConnection sqlConnection1;
		protected System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		protected KIT_ERP.ProductionManagement.dsPhantom dsPhantom1;
		protected System.Data.SqlClient.SqlDataAdapter sqlDataAdapter2;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand2;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
		protected System.Web.UI.HtmlControls.HtmlInputFile Find;
	
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
			this.dsPhantomErr1 = new KIT_ERP.ProductionManagement.dsPhantomErr();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
			this.dsPhantom1 = new KIT_ERP.ProductionManagement.dsPhantom();
			this.sqlDataAdapter2 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
			((System.ComponentModel.ISupportInitialize)(this.dsPhantomErr1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dsPhantom1)).BeginInit();
			this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			// 
			// dsPhantomErr1
			// 
			this.dsPhantomErr1.DataSetName = "dsPhantomErr";
			this.dsPhantomErr1.Locale = new System.Globalization.CultureInfo("ko-KR");
			// 
			// sqlConnection1
			// 

			this.sqlConnection1.ConnectionString = "workstation id=\"EZSYS-ERP01\";packet size=4096;user id=sa;data source=\"220.66.115.9\";persist " +
				"security info=True;initial catalog=Sindong_ERP;password=\"ezsys#0509\"";
			//this.sqlConnection1.ConnectionString = "workstation id=FREESRV05;packet size=4096;user id=sa;data source=\"220.66.115.10\";" +
			//	"persist security info=False;password=\"ezsys#0509\";initial catalog=Shindong_ERP";
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.DeleteCommand = this.sqlDeleteCommand1;
			this.sqlDataAdapter1.InsertCommand = this.sqlInsertCommand1;
			this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "ItemGroup_HT", new System.Data.Common.DataColumnMapping[] {
																																																					  new System.Data.Common.DataColumnMapping("ItemGroup", "ItemGroup"),
																																																					  new System.Data.Common.DataColumnMapping("ReceivingOrderDate", "ReceivingOrderDate"),
																																																					  new System.Data.Common.DataColumnMapping("DeliveryDate", "DeliveryDate"),
																																																					  new System.Data.Common.DataColumnMapping("Quantity", "Quantity"),
																																																					  new System.Data.Common.DataColumnMapping("ProgressCondition", "ProgressCondition"),
																																																					  new System.Data.Common.DataColumnMapping("ItemGroupIndex", "ItemGroupIndex")})});
			this.sqlDataAdapter1.UpdateCommand = this.sqlUpdateCommand1;
			// 
			// sqlDeleteCommand1
			// 
			this.sqlDeleteCommand1.CommandText = @"DELETE FROM ItemGroup_HT WHERE (ItemGroupIndex = @Original_ItemGroupIndex) AND (DeliveryDate = @Original_DeliveryDate OR @Original_DeliveryDate IS NULL AND DeliveryDate IS NULL) AND (ItemGroup = @Original_ItemGroup OR @Original_ItemGroup IS NULL AND ItemGroup IS NULL) AND (ProgressCondition = @Original_ProgressCondition OR @Original_ProgressCondition IS NULL AND ProgressCondition IS NULL) AND (Quantity = @Original_Quantity OR @Original_Quantity IS NULL AND Quantity IS NULL) AND (ReceivingOrderDate = @Original_ReceivingOrderDate OR @Original_ReceivingOrderDate IS NULL AND ReceivingOrderDate IS NULL)";
			this.sqlDeleteCommand1.Connection = this.sqlConnection1;
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemGroupIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemGroupIndex", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemGroup", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemGroup", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressCondition", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProgressCondition", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Quantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "Quantity", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderDate", System.Data.DataRowVersion.Original, null));
			// 
			// sqlInsertCommand1
			// 
			this.sqlInsertCommand1.CommandText = @"INSERT INTO ItemGroup_HT(ItemGroup, ReceivingOrderDate, DeliveryDate, Quantity, ProgressCondition) VALUES (@ItemGroup, @ReceivingOrderDate, @DeliveryDate, @Quantity, @ProgressCondition); SELECT ItemGroup, ReceivingOrderDate, DeliveryDate, Quantity, ProgressCondition, ItemGroupIndex FROM ItemGroup_HT WHERE (ItemGroupIndex = @@IDENTITY)";
			this.sqlInsertCommand1.Connection = this.sqlConnection1;
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemGroup", System.Data.SqlDbType.VarChar, 50, "ItemGroup"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, "ReceivingOrderDate"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryDate", System.Data.SqlDbType.DateTime, 4, "DeliveryDate"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Quantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "Quantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressCondition", System.Data.SqlDbType.VarChar, 50, "ProgressCondition"));
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "SELECT ItemGroup, ReceivingOrderDate, DeliveryDate, Quantity, ProgressCondition, " +
				"ItemGroupIndex FROM ItemGroup_HT WHERE (ItemGroup IS NULL)";
			this.sqlSelectCommand1.Connection = this.sqlConnection1;
			// 
			// sqlUpdateCommand1
			// 
			this.sqlUpdateCommand1.CommandText = @"UPDATE ItemGroup_HT SET ItemGroup = @ItemGroup, ReceivingOrderDate = @ReceivingOrderDate, DeliveryDate = @DeliveryDate, Quantity = @Quantity, ProgressCondition = @ProgressCondition WHERE (ItemGroupIndex = @Original_ItemGroupIndex) AND (DeliveryDate = @Original_DeliveryDate OR @Original_DeliveryDate IS NULL AND DeliveryDate IS NULL) AND (ItemGroup = @Original_ItemGroup OR @Original_ItemGroup IS NULL AND ItemGroup IS NULL) AND (ProgressCondition = @Original_ProgressCondition OR @Original_ProgressCondition IS NULL AND ProgressCondition IS NULL) AND (Quantity = @Original_Quantity OR @Original_Quantity IS NULL AND Quantity IS NULL) AND (ReceivingOrderDate = @Original_ReceivingOrderDate OR @Original_ReceivingOrderDate IS NULL AND ReceivingOrderDate IS NULL); SELECT ItemGroup, ReceivingOrderDate, DeliveryDate, Quantity, ProgressCondition, ItemGroupIndex FROM ItemGroup_HT WHERE (ItemGroupIndex = @ItemGroupIndex)";
			this.sqlUpdateCommand1.Connection = this.sqlConnection1;
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemGroup", System.Data.SqlDbType.VarChar, 50, "ItemGroup"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, "ReceivingOrderDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryDate", System.Data.SqlDbType.DateTime, 4, "DeliveryDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Quantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "Quantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProgressCondition", System.Data.SqlDbType.VarChar, 50, "ProgressCondition"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemGroupIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemGroupIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemGroup", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemGroup", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProgressCondition", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProgressCondition", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Quantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "Quantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemGroupIndex", System.Data.SqlDbType.Int, 4, "ItemGroupIndex"));
			// 
			// dsPhantom1
			// 
			this.dsPhantom1.DataSetName = "dsPhantom";
			this.dsPhantom1.Locale = new System.Globalization.CultureInfo("ko-KR");
			// 
			// sqlDataAdapter2
			// 
			this.sqlDataAdapter2.DeleteCommand = this.sqlDeleteCommand2;
			this.sqlDataAdapter2.InsertCommand = this.sqlInsertCommand2;
			this.sqlDataAdapter2.SelectCommand = this.sqlSelectCommand2;
			this.sqlDataAdapter2.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "ItemGroupErr_HT", new System.Data.Common.DataColumnMapping[] {
																																																						 new System.Data.Common.DataColumnMapping("ItemNum", "ItemNum"),
																																																						 new System.Data.Common.DataColumnMapping("CompanyName", "CompanyName"),
																																																						 new System.Data.Common.DataColumnMapping("BusinessRegistrationNum", "BusinessRegistrationNum"),
																																																						 new System.Data.Common.DataColumnMapping("ReceivingOrderDate", "ReceivingOrderDate"),
																																																						 new System.Data.Common.DataColumnMapping("DeliveryRequestDate", "DeliveryRequestDate"),
																																																						 new System.Data.Common.DataColumnMapping("TotalReceiveingOrderQuantity", "TotalReceiveingOrderQuantity"),
																																																						 new System.Data.Common.DataColumnMapping("ErrRegion", "ErrRegion"),
																																																						 new System.Data.Common.DataColumnMapping("ItemGroupErrIndex", "ItemGroupErrIndex")})});
			this.sqlDataAdapter2.UpdateCommand = this.sqlUpdateCommand2;
			// 
			// sqlDeleteCommand2
			// 
			this.sqlDeleteCommand2.CommandText = @"DELETE FROM ItemGroupErr_HT WHERE (ItemGroupErrIndex = @Original_ItemGroupErrIndex) AND (BusinessRegistrationNum = @Original_BusinessRegistrationNum OR @Original_BusinessRegistrationNum IS NULL AND BusinessRegistrationNum IS NULL) AND (CompanyName = @Original_CompanyName OR @Original_CompanyName IS NULL AND CompanyName IS NULL) AND (DeliveryRequestDate = @Original_DeliveryRequestDate OR @Original_DeliveryRequestDate IS NULL AND DeliveryRequestDate IS NULL) AND (ErrRegion = @Original_ErrRegion OR @Original_ErrRegion IS NULL AND ErrRegion IS NULL) AND (ItemNum = @Original_ItemNum OR @Original_ItemNum IS NULL AND ItemNum IS NULL) AND (ReceivingOrderDate = @Original_ReceivingOrderDate OR @Original_ReceivingOrderDate IS NULL AND ReceivingOrderDate IS NULL) AND (TotalReceiveingOrderQuantity = @Original_TotalReceiveingOrderQuantity OR @Original_TotalReceiveingOrderQuantity IS NULL AND TotalReceiveingOrderQuantity IS NULL)";
			this.sqlDeleteCommand2.Connection = this.sqlConnection1;
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemGroupErrIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemGroupErrIndex", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BusinessRegistrationNum", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BusinessRegistrationNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CompanyName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ErrRegion", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ErrRegion", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Original, null));
			// 
			// sqlInsertCommand2
			// 
			this.sqlInsertCommand2.CommandText = @"INSERT INTO ItemGroupErr_HT(ItemNum, CompanyName, BusinessRegistrationNum, ReceivingOrderDate, DeliveryRequestDate, TotalReceiveingOrderQuantity, ErrRegion) VALUES (@ItemNum, @CompanyName, @BusinessRegistrationNum, @ReceivingOrderDate, @DeliveryRequestDate, @TotalReceiveingOrderQuantity, @ErrRegion); SELECT ItemNum, CompanyName, BusinessRegistrationNum, ReceivingOrderDate, DeliveryRequestDate, TotalReceiveingOrderQuantity, ErrRegion, ItemGroupErrIndex FROM ItemGroupErr_HT WHERE (ItemGroupErrIndex = @@IDENTITY)";
			this.sqlInsertCommand2.Connection = this.sqlConnection1;
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.VarChar, 50, "ItemNum"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.VarChar, 50, "CompanyName"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BusinessRegistrationNum", System.Data.SqlDbType.VarChar, 50, "BusinessRegistrationNum"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, "ReceivingOrderDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ErrRegion", System.Data.SqlDbType.VarChar, 200, "ErrRegion"));
			// 
			// sqlSelectCommand2
			// 
			this.sqlSelectCommand2.CommandText = "SELECT ItemNum, CompanyName, BusinessRegistrationNum, ReceivingOrderDate, Deliver" +
				"yRequestDate, TotalReceiveingOrderQuantity, ErrRegion, ItemGroupErrIndex FROM It" +
				"emGroupErr_HT WHERE (ItemNum IS NULL)";
			this.sqlSelectCommand2.Connection = this.sqlConnection1;
			// 
			// sqlUpdateCommand2
			// 
			this.sqlUpdateCommand2.CommandText = @"UPDATE ItemGroupErr_HT SET ItemNum = @ItemNum, CompanyName = @CompanyName, BusinessRegistrationNum = @BusinessRegistrationNum, ReceivingOrderDate = @ReceivingOrderDate, DeliveryRequestDate = @DeliveryRequestDate, TotalReceiveingOrderQuantity = @TotalReceiveingOrderQuantity, ErrRegion = @ErrRegion WHERE (ItemGroupErrIndex = @Original_ItemGroupErrIndex) AND (BusinessRegistrationNum = @Original_BusinessRegistrationNum OR @Original_BusinessRegistrationNum IS NULL AND BusinessRegistrationNum IS NULL) AND (CompanyName = @Original_CompanyName OR @Original_CompanyName IS NULL AND CompanyName IS NULL) AND (DeliveryRequestDate = @Original_DeliveryRequestDate OR @Original_DeliveryRequestDate IS NULL AND DeliveryRequestDate IS NULL) AND (ErrRegion = @Original_ErrRegion OR @Original_ErrRegion IS NULL AND ErrRegion IS NULL) AND (ItemNum = @Original_ItemNum OR @Original_ItemNum IS NULL AND ItemNum IS NULL) AND (ReceivingOrderDate = @Original_ReceivingOrderDate OR @Original_ReceivingOrderDate IS NULL AND ReceivingOrderDate IS NULL) AND (TotalReceiveingOrderQuantity = @Original_TotalReceiveingOrderQuantity OR @Original_TotalReceiveingOrderQuantity IS NULL AND TotalReceiveingOrderQuantity IS NULL); SELECT ItemNum, CompanyName, BusinessRegistrationNum, ReceivingOrderDate, DeliveryRequestDate, TotalReceiveingOrderQuantity, ErrRegion, ItemGroupErrIndex FROM ItemGroupErr_HT WHERE (ItemGroupErrIndex = @ItemGroupErrIndex)";
			this.sqlUpdateCommand2.Connection = this.sqlConnection1;
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemNum", System.Data.SqlDbType.VarChar, 50, "ItemNum"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CompanyName", System.Data.SqlDbType.VarChar, 50, "CompanyName"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BusinessRegistrationNum", System.Data.SqlDbType.VarChar, 50, "BusinessRegistrationNum"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, "ReceivingOrderDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeliveryRequestDate", System.Data.SqlDbType.DateTime, 4, "DeliveryRequestDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ErrRegion", System.Data.SqlDbType.VarChar, 200, "ErrRegion"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemGroupErrIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemGroupErrIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_BusinessRegistrationNum", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "BusinessRegistrationNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CompanyName", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CompanyName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeliveryRequestDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeliveryRequestDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ErrRegion", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ErrRegion", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ItemNum", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemNum", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ReceivingOrderDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ReceivingOrderDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TotalReceiveingOrderQuantity", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "TotalReceiveingOrderQuantity", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ItemGroupErrIndex", System.Data.SqlDbType.Int, 4, "ItemGroupErrIndex"));
			this.Load += new System.EventHandler(this.Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.dsPhantomErr1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dsPhantom1)).EndInit();

		}
		#endregion

		private void bt_Register_Click(object sender, System.EventArgs e)
		{
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
					comm.CommandText = "DeleteItemGroupErr";
					comm.CommandType = CommandType.StoredProcedure;
					con.Open();
					comm.ExecuteNonQuery();
					con.Close();

					sqlConnection1.Open();

					sqlDataAdapter1.Fill(dsPhantom1);
					sqlDataAdapter2.Fill(dsPhantomErr1);

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
							dsPhantom.ItemGroup_HTRow row =  dsPhantom1.ItemGroup_HT.NewItemGroup_HTRow();

							//품목번호...
							if(dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() == "")
							{
								state = false;
								ErrorMessage = (i+2)+"행의 팬텀품목번호가 없습니다!";
								goto Add;
							}
							else
							{
								if(Exist(dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim()))
									row.ItemGroup = dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim();
								else
								{
									state = false;
									ErrorMessage = (i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"은 존재하지 않습니다!";
									goto Add;
								}
							}

	
							if(dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Length == 8)
							{
								//수주일자...
								string year = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(0,4).ToString();
								string month = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(4,2).ToString();
								string day = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(6,2).ToString();
								string Date = year+"-"+month+"-"+day;
								row.ReceivingOrderDate = Convert.ToDateTime(Date);

								//납입요구일
								year = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(0,4).ToString();
								month = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(4,2).ToString();
								day = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(6,2).ToString();
								Date = year+"-"+month+"-"+day;
								
								row.DeliveryDate = Convert.ToDateTime(Date);

								if(dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim() != "")
									row.Quantity = decimal.Parse(dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim());
								else
								{
									state = false;
									ErrorMessage = (i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"의 발주량이 없습니다!";
									goto Add;
								}
								row.ProgressCondition = "대기";
								
							}
							else
							{
								//수주일자...
								row.ReceivingOrderDate = Convert.ToDateTime(dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim());

								//납기요구일
								row.DeliveryDate =  Convert.ToDateTime(dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim());

								if(dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim() != "")
									row.Quantity = decimal.Parse(dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim());
								else
								{
									state = false;
									ErrorMessage = (i+2)+"행의 품목 "+dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim() +"의 발주량을 입력해주세요!";
									goto Add;
								}
								row.ProgressCondition = "대기";
							}
							

						Add:
							if(state)
								dsPhantom1.ItemGroup_HT.AddItemGroup_HTRow(row);
							else
							{

								string str = "Insert into ItemGroupErr_HT (ItemNum, CompanyName, BusinessRegistrationNum, ReceivingOrderDate, DeliveryRequestDate, TotalReceiveingOrderQuantity, ErrRegion) values (@ItemNum, @CompanyName, @BusinessRegistrationNum, @ReceivingOrderDate, @DeliveryRequestDate, @TotalReceiveingOrderQuantity, @ErrRegion)";
								SqlCommand com = new SqlCommand();
								com.CommandText = str;
								com.Transaction = tr;
								com.Connection = sqlConnection1;
								com.Parameters.Add("@ItemNum",dsExcel.Tables[0].Rows[i]["품목번호"].ToString().Trim());
								com.Parameters.Add("@CompanyName",dsExcel.Tables[0].Rows[i]["거래처명"].ToString().Trim());
								com.Parameters.Add("@BusinessRegistrationNum",dsExcel.Tables[0].Rows[i]["사업자등록번호"].ToString().Trim());
								if(dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Length == 8)
								{
									string year = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(0,4).ToString();
									string month = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(4,2).ToString();
									string day = dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim().Substring(6,2).ToString();
									string Date = year+"-"+month+"-"+day;
									com.Parameters.Add("@ReceivingOrderDate", Convert.ToDateTime(Date));
								}
								else if(dsExcel.Tables[0].Rows[i]["수주일자"].ToString() == null || dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim() == "")
									com.Parameters.Add("@ReceivingOrderDate", Convert.DBNull);
								else
									com.Parameters.Add("@ReceivingOrderDate",DateTime.Parse(dsExcel.Tables[0].Rows[i]["수주일자"].ToString()).ToShortDateString());


								if(dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Length == 8)
								{
									string year = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(0,4).ToString();
									string month = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(4,2).ToString();
									string day = dsExcel.Tables[0].Rows[i]["납기요구일"].ToString().Trim().Substring(6,2).ToString();
									string Date = year+"-"+month+"-"+day;
									com.Parameters.Add("@DeliveryRequestDate", Convert.ToDateTime(Date));
								}
								else if(dsExcel.Tables[0].Rows[i]["납기요구일"].ToString() == null || dsExcel.Tables[0].Rows[i]["수주일자"].ToString().Trim() == "")
									com.Parameters.Add("@DeliveryRequestDate", Convert.DBNull);
								else
									com.Parameters.Add("@DeliveryRequestDate",DateTime.Parse(dsExcel.Tables[0].Rows[i]["수주일자"].ToString()).ToShortDateString());


								if(dsExcel.Tables[0].Rows[i]["총발주량"].ToString() == null || dsExcel.Tables[0].Rows[i]["총발주량"].ToString().Trim() == "")
									com.Parameters.Add("@TotalReceiveingOrderQuantity", Convert.DBNull);
								else
									com.Parameters.Add("@TotalReceiveingOrderQuantity", decimal.Parse(dsExcel.Tables[0].Rows[i]["총발주량"].ToString()));

								com.Parameters.Add("@ErrRegion", ErrorMessage);
								com.ExecuteNonQuery();

								Errorcount++;



								/*
								dsPhantomErr.ItemGroupErr_HTRow Erow  = dsPhantomErr1.ItemGroupErr_HT.NewItemGroupErr_HTRow();
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

								Erow["ErrRegion"] = ErrorMessage;
								
								dsPhantomErr1.ItemGroupErr_HT.AddItemGroupErr_HTRow(Erow);
								Errorcount++;
								sqlDataAdapter2.Update(dsPhantomErr1);
								*/
							}
						}

						sqlDataAdapter1.Update(dsPhantom1);
						
						UltraWebGrid1.Visible = true;
						UltraWebGrid2.Visible = false;

						if(Errorcount == 0)
						{
							SqlCommand cmd = new SqlCommand();
							cmd.Connection = sqlConnection1;
							cmd.Transaction = tr;
							cmd.CommandText = @"Select ItemGroup_HT.*, ItemDrawNum, ItemName From ItemGroup_HT inner join II_MT on ItemGroup = ItemNum
												where RecodingState = 1 and ProgressCondition = '대기'";					
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
							cmd.CommandText = @"Select ItemGroup_HT.*, ItemDrawNum, ItemName From ItemGroup_HT inner join II_MT on ItemGroup = ItemNum
												where RecodingState = 1 and ProgressCondition = '대기'";					
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
						Response.Write("alert('"+ee.Message+"');");
						Response.Write("</script>");

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


		private bool Exist(string Item)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "Select count(ItemNum) From II_MT where ItemNum = @ItemNum and RecodingState = 1 and PropertyClassification = '팬텀'";
			comm.Parameters.Add("@ItemNum",Item);
			int count = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			if(count == 0)
				return false;
			else
				return true;
		}

		private void btSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Visible = false;
			UltraWebGrid2.Visible = true;

			UltraWebGrid2.DataSource = Search();
			UltraWebGrid2.DataBind();
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

		private DataSet Search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			//string str = @"Select ItemGroupErr_HT.ItemNum, CompanyName, BusinessRegistrationNum, ReceivingOrderDate, DeliveryRequestDate, TotalReceiveingOrderQuantity, ErrRegion, ItemName From ItemGroupErr_HT left outer join II_MT on ItemGroupErr_HT.ItemNum = II_MT.ItemNum
			//				where II_MT.RecodingState = 1";
			string str = @"Select ItemGroupErr_HT.ItemNum, CompanyName, BusinessRegistrationNum, ReceivingOrderDate, DeliveryRequestDate, TotalReceiveingOrderQuantity, ErrRegion From ItemGroupErr_HT ";
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
			string str =@"Select ItemGroup_HT.ItemNum, ReceivingOrderDate, DeliveryDate, Quantity, ItemDrawNum, ItemName From ItemGroup_HT inner join II_MT on ItemGroup = ItemNum
						where RecodingState = 1 and ProgressCondition = '대기'";		
			SqlCommand comm =  new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);

			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;
		}
	}
}
