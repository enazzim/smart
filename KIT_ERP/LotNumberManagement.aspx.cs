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

namespace KIT_ERP
{
	/// <summary>
	/// LotNumberManagement에 대한 요약 설명입니다.
	/// </summary>
	public class LotNumberManagement : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button btListAll;
		protected System.Web.UI.WebControls.Button btSelectCancel;
		protected System.Web.UI.WebControls.Button btClose;
		protected System.Data.SqlClient.SqlDataAdapter sdaLotNumber;
		protected System.Data.SqlClient.SqlConnection scn;
		protected System.Web.UI.WebControls.ListBox lbLotNumber;
		protected System.Web.UI.WebControls.Button btConfirm;
		protected System.Web.UI.WebControls.TextBox tbLotCreate;
		protected System.Web.UI.WebControls.Button btLotCreate;
		protected System.Web.UI.WebControls.Button btLotDivide;
		protected System.Web.UI.WebControls.Button btLotIntegrate;
		protected System.Web.UI.WebControls.Button btLotEnd;
		protected System.Web.UI.WebControls.Button btLotDelete;
		protected System.Web.UI.WebControls.Button btLotSelect;
		protected System.Web.UI.WebControls.TextBox tbLotDivide;
		protected System.Web.UI.WebControls.TextBox tbLotIntegrate;
		protected System.Web.UI.WebControls.TextBox tbLotSelect;
		protected System.Data.SqlClient.SqlDataAdapter sdaLoad;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand2;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand2;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand2;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand2;
		protected KIT_ERP.DSLoad dsLotNumber = new DSLoad();
		

		protected enum ManagerMode{Create=0,Divide=1,Integrate=2,Select=3,End=4,Delete=5,None=6}
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			sdaLotNumber.SelectCommand.Parameters["@Active"].Value=true;
			if(ViewState["Mode"]==null) ViewState["Mode"]=ManagerMode.None;
			btClose.Attributes["onclick"]="CloseWindow()";
			if(!IsPostBack)
			{
				sdaLotNumber.Fill(dsLotNumber);
				lbLotNumber.DataBind();
			}
		}

		private void Page_PreRender(object sender, System.EventArgs e)
		{
			int aa=(int)ViewState["Mode"];
			if(aa<3) btConfirm.Attributes["onclick"]="return SubmitConfirm();";
			else if(aa==3)btConfirm.Attributes["onclick"]="return SubmitConfirm();";
			else if(aa==4)btConfirm.Attributes["onclick"]="return SubmitConfirm2();";
			else if(aa==5)btConfirm.Attributes["onclick"]="return SubmitConfirm3();"; 
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
			this.sdaLotNumber = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
			this.scn = new System.Data.SqlClient.SqlConnection();
			this.sqlInsertCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sdaLoad = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDeleteCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlSelectCommand2 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand2 = new System.Data.SqlClient.SqlCommand();
			this.lbLotNumber.SelectedIndexChanged += new System.EventHandler(this.lbLotNumber_SelectedIndexChanged);
			this.btLotSelect.Click += new System.EventHandler(this.btLotSelect_Click);
			this.btLotDelete.Click += new System.EventHandler(this.btLotDelete_Click);
			this.btLotEnd.Click += new System.EventHandler(this.btLotEnd_Click);
			this.btLotIntegrate.Click += new System.EventHandler(this.btLotIntegrate_Click);
			this.btLotDivide.Click += new System.EventHandler(this.btLotDivide_Click);
			this.btListAll.Click += new System.EventHandler(this.btListAll_Click);
			this.btSelectCancel.Click += new System.EventHandler(this.btSelectCancel_Click);
			this.btLotCreate.Click += new System.EventHandler(this.btLotCreate_Click);
			this.btClose.Click += new System.EventHandler(this.btClose_Click);
			this.btConfirm.Click += new System.EventHandler(this.btConfirm_Click);
			// 
			// sdaLotNumber
			// 
			this.sdaLotNumber.DeleteCommand = this.sqlDeleteCommand1;
			this.sdaLotNumber.InsertCommand = this.sqlInsertCommand2;
			this.sdaLotNumber.SelectCommand = this.sqlSelectCommand1;
			this.sdaLotNumber.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																								   new System.Data.Common.DataTableMapping("Table", "LotLedger", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				new System.Data.Common.DataColumnMapping("LotNumber", "LotNumber"),
																																																				new System.Data.Common.DataColumnMapping("P1", "P1"),
																																																				new System.Data.Common.DataColumnMapping("P2", "P2"),
																																																				new System.Data.Common.DataColumnMapping("RawMaterial", "RawMaterial"),
																																																				new System.Data.Common.DataColumnMapping("Active", "Active"),
																																																				new System.Data.Common.DataColumnMapping("Remark", "Remark"),
																																																				new System.Data.Common.DataColumnMapping("RegisterDate", "RegisterDate"),
																																																				new System.Data.Common.DataColumnMapping("Registerer", "Registerer"),
																																																				new System.Data.Common.DataColumnMapping("IsDelete", "IsDelete"),
																																																				new System.Data.Common.DataColumnMapping("DeleteDate", "DeleteDate")})});
			this.sdaLotNumber.UpdateCommand = this.sqlUpdateCommand1;
			// 
			// sqlDeleteCommand1
			// 
			this.sqlDeleteCommand1.CommandText = @"DELETE FROM LotLedger WHERE (ID = @Original_ID) AND (Active = @Original_Active OR @Original_Active IS NULL AND Active IS NULL) AND (DeleteDate = @Original_DeleteDate OR @Original_DeleteDate IS NULL AND DeleteDate IS NULL) AND (IsDelete = @Original_IsDelete OR @Original_IsDelete IS NULL AND IsDelete IS NULL) AND (LotNumber = @Original_LotNumber OR @Original_LotNumber IS NULL AND LotNumber IS NULL) AND (P1 = @Original_P1 OR @Original_P1 IS NULL AND P1 IS NULL) AND (P2 = @Original_P2 OR @Original_P2 IS NULL AND P2 IS NULL) AND (RawMaterial = @Original_RawMaterial OR @Original_RawMaterial IS NULL AND RawMaterial IS NULL) AND (RegisterDate = @Original_RegisterDate OR @Original_RegisterDate IS NULL AND RegisterDate IS NULL) AND (Registerer = @Original_Registerer OR @Original_Registerer IS NULL AND Registerer IS NULL) AND (Remark = @Original_Remark OR @Original_Remark IS NULL AND Remark IS NULL)";
			this.sqlDeleteCommand1.Connection = this.scn;
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ID", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Active", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Active", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeleteDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeleteDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_IsDelete", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "IsDelete", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LotNumber", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "LotNumber", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_P1", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "P1", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_P2", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "P2", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RawMaterial", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RawMaterial", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegisterDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegisterDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Registerer", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Registerer", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Remark", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Remark", System.Data.DataRowVersion.Original, null));
			// 
			// scn
			// 
			this.scn.ConnectionString = "workstation id=FREESRV01;packet size=4096;user id=sa;data source=\"220.66.115.9\";p" +
				"ersist security info=True;initial catalog=dongi_ERP;password=\"ezsys#0509\"";
			// 
			// sqlInsertCommand2
			// 
			this.sqlInsertCommand2.CommandText = @"INSERT INTO LotLedger(LotNumber, P1, P2, RawMaterial, Active, Remark, RegisterDate, Registerer, IsDelete, DeleteDate) VALUES (@LotNumber, @P1, @P2, @RawMaterial, @Active, @Remark, @RegisterDate, @Registerer, @IsDelete, @DeleteDate); SELECT ID, LotNumber, P1, P2, RawMaterial, Active, Remark, RegisterDate, Registerer, IsDelete, DeleteDate FROM LotLedger WHERE (ID = @@IDENTITY) ORDER BY LotNumber";
			this.sqlInsertCommand2.Connection = this.scn;
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LotNumber", System.Data.SqlDbType.VarChar, 200, "LotNumber"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@P1", System.Data.SqlDbType.VarChar, 200, "P1"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@P2", System.Data.SqlDbType.VarChar, 200, "P2"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RawMaterial", System.Data.SqlDbType.Bit, 1, "RawMaterial"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit, 1, "Active"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Remark", System.Data.SqlDbType.Bit, 1, "Remark"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.DateTime, 4, "RegisterDate"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Registerer", System.Data.SqlDbType.VarChar, 50, "Registerer"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDelete", System.Data.SqlDbType.Bit, 1, "IsDelete"));
			this.sqlInsertCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeleteDate", System.Data.SqlDbType.DateTime, 4, "DeleteDate"));
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "SELECT ID, LotNumber, P1, P2, RawMaterial, Active, Remark, RegisterDate, Register" +
				"er, IsDelete, DeleteDate FROM LotLedger WHERE (Active = 1 OR Active = @Active) A" +
				"ND (Remark = 1) ORDER BY LotNumber";
			this.sqlSelectCommand1.Connection = this.scn;
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit, 1, "Active"));
			// 
			// sqlUpdateCommand1
			// 
			this.sqlUpdateCommand1.CommandText = @"UPDATE LotLedger SET LotNumber = @LotNumber, P1 = @P1, P2 = @P2, RawMaterial = @RawMaterial, Active = @Active, Remark = @Remark, RegisterDate = @RegisterDate, Registerer = @Registerer, IsDelete = @IsDelete, DeleteDate = @DeleteDate WHERE (ID = @Original_ID) AND (Active = @Original_Active OR @Original_Active IS NULL AND Active IS NULL) AND (DeleteDate = @Original_DeleteDate OR @Original_DeleteDate IS NULL AND DeleteDate IS NULL) AND (IsDelete = @Original_IsDelete OR @Original_IsDelete IS NULL AND IsDelete IS NULL) AND (LotNumber = @Original_LotNumber OR @Original_LotNumber IS NULL AND LotNumber IS NULL) AND (P1 = @Original_P1 OR @Original_P1 IS NULL AND P1 IS NULL) AND (P2 = @Original_P2 OR @Original_P2 IS NULL AND P2 IS NULL) AND (RawMaterial = @Original_RawMaterial OR @Original_RawMaterial IS NULL AND RawMaterial IS NULL) AND (RegisterDate = @Original_RegisterDate OR @Original_RegisterDate IS NULL AND RegisterDate IS NULL) AND (Registerer = @Original_Registerer OR @Original_Registerer IS NULL AND Registerer IS NULL) AND (Remark = @Original_Remark OR @Original_Remark IS NULL AND Remark IS NULL); SELECT ID, LotNumber, P1, P2, RawMaterial, Active, Remark, RegisterDate, Registerer, IsDelete, DeleteDate FROM LotLedger WHERE (ID = @ID) ORDER BY LotNumber";
			this.sqlUpdateCommand1.Connection = this.scn;
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LotNumber", System.Data.SqlDbType.VarChar, 200, "LotNumber"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@P1", System.Data.SqlDbType.VarChar, 200, "P1"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@P2", System.Data.SqlDbType.VarChar, 200, "P2"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RawMaterial", System.Data.SqlDbType.Bit, 1, "RawMaterial"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit, 1, "Active"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Remark", System.Data.SqlDbType.Bit, 1, "Remark"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.DateTime, 4, "RegisterDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Registerer", System.Data.SqlDbType.VarChar, 50, "Registerer"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDelete", System.Data.SqlDbType.Bit, 1, "IsDelete"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeleteDate", System.Data.SqlDbType.DateTime, 4, "DeleteDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ID", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Active", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Active", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeleteDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeleteDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_IsDelete", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "IsDelete", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LotNumber", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "LotNumber", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_P1", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "P1", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_P2", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "P2", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RawMaterial", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RawMaterial", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegisterDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegisterDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Registerer", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Registerer", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Remark", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Remark", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID"));
			// 
			// sdaLoad
			// 
			this.sdaLoad.DeleteCommand = this.sqlDeleteCommand2;
			this.sdaLoad.InsertCommand = this.sqlInsertCommand1;
			this.sdaLoad.SelectCommand = this.sqlSelectCommand2;
			this.sdaLoad.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																							  new System.Data.Common.DataTableMapping("Table", "LotLedger", new System.Data.Common.DataColumnMapping[] {
																																																		   new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																		   new System.Data.Common.DataColumnMapping("LotNumber", "LotNumber"),
																																																		   new System.Data.Common.DataColumnMapping("P1", "P1"),
																																																		   new System.Data.Common.DataColumnMapping("P2", "P2"),
																																																		   new System.Data.Common.DataColumnMapping("RawMaterial", "RawMaterial"),
																																																		   new System.Data.Common.DataColumnMapping("Active", "Active"),
																																																		   new System.Data.Common.DataColumnMapping("Remark", "Remark"),
																																																		   new System.Data.Common.DataColumnMapping("RegisterDate", "RegisterDate"),
																																																		   new System.Data.Common.DataColumnMapping("Registerer", "Registerer"),
																																																		   new System.Data.Common.DataColumnMapping("IsDelete", "IsDelete"),
																																																		   new System.Data.Common.DataColumnMapping("DeleteDate", "DeleteDate")})});
			this.sdaLoad.UpdateCommand = this.sqlUpdateCommand2;
			// 
			// sqlDeleteCommand2
			// 
			this.sqlDeleteCommand2.CommandText = @"DELETE FROM LotLedger WHERE (ID = @Original_ID) AND (Active = @Original_Active OR @Original_Active IS NULL AND Active IS NULL) AND (DeleteDate = @Original_DeleteDate OR @Original_DeleteDate IS NULL AND DeleteDate IS NULL) AND (IsDelete = @Original_IsDelete OR @Original_IsDelete IS NULL AND IsDelete IS NULL) AND (LotNumber = @Original_LotNumber OR @Original_LotNumber IS NULL AND LotNumber IS NULL) AND (P1 = @Original_P1 OR @Original_P1 IS NULL AND P1 IS NULL) AND (P2 = @Original_P2 OR @Original_P2 IS NULL AND P2 IS NULL) AND (RawMaterial = @Original_RawMaterial OR @Original_RawMaterial IS NULL AND RawMaterial IS NULL) AND (RegisterDate = @Original_RegisterDate OR @Original_RegisterDate IS NULL AND RegisterDate IS NULL) AND (Registerer = @Original_Registerer OR @Original_Registerer IS NULL AND Registerer IS NULL) AND (Remark = @Original_Remark OR @Original_Remark IS NULL AND Remark IS NULL)";
			this.sqlDeleteCommand2.Connection = this.scn;
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ID", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Active", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Active", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeleteDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeleteDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_IsDelete", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "IsDelete", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LotNumber", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "LotNumber", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_P1", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "P1", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_P2", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "P2", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RawMaterial", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RawMaterial", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegisterDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegisterDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Registerer", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Registerer", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Remark", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Remark", System.Data.DataRowVersion.Original, null));
			// 
			// sqlInsertCommand1
			// 
			this.sqlInsertCommand1.CommandText = @"INSERT INTO LotLedger(LotNumber, P1, P2, RawMaterial, Active, Remark, RegisterDate, Registerer, IsDelete, DeleteDate) VALUES (@LotNumber, @P1, @P2, @RawMaterial, @Active, @Remark, @RegisterDate, @Registerer, @IsDelete, @DeleteDate); SELECT ID, LotNumber, P1, P2, RawMaterial, Active, Remark, RegisterDate, Registerer, IsDelete, DeleteDate FROM LotLedger WHERE (ID = @@IDENTITY)";
			this.sqlInsertCommand1.Connection = this.scn;
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LotNumber", System.Data.SqlDbType.VarChar, 200, "LotNumber"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@P1", System.Data.SqlDbType.VarChar, 200, "P1"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@P2", System.Data.SqlDbType.VarChar, 200, "P2"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RawMaterial", System.Data.SqlDbType.Bit, 1, "RawMaterial"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit, 1, "Active"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Remark", System.Data.SqlDbType.Bit, 1, "Remark"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.DateTime, 4, "RegisterDate"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Registerer", System.Data.SqlDbType.VarChar, 50, "Registerer"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDelete", System.Data.SqlDbType.Bit, 1, "IsDelete"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeleteDate", System.Data.SqlDbType.DateTime, 4, "DeleteDate"));
			// 
			// sqlSelectCommand2
			// 
			this.sqlSelectCommand2.CommandText = "SELECT ID, LotNumber, P1, P2, RawMaterial, Active, Remark, RegisterDate, Register" +
				"er, IsDelete, DeleteDate FROM LotLedger";
			this.sqlSelectCommand2.Connection = this.scn;
			// 
			// sqlUpdateCommand2
			// 
			this.sqlUpdateCommand2.CommandText = @"UPDATE LotLedger SET LotNumber = @LotNumber, P1 = @P1, P2 = @P2, RawMaterial = @RawMaterial, Active = @Active, Remark = @Remark, RegisterDate = @RegisterDate, Registerer = @Registerer, IsDelete = @IsDelete, DeleteDate = @DeleteDate WHERE (ID = @Original_ID) AND (Active = @Original_Active OR @Original_Active IS NULL AND Active IS NULL) AND (DeleteDate = @Original_DeleteDate OR @Original_DeleteDate IS NULL AND DeleteDate IS NULL) AND (IsDelete = @Original_IsDelete OR @Original_IsDelete IS NULL AND IsDelete IS NULL) AND (LotNumber = @Original_LotNumber OR @Original_LotNumber IS NULL AND LotNumber IS NULL) AND (P1 = @Original_P1 OR @Original_P1 IS NULL AND P1 IS NULL) AND (P2 = @Original_P2 OR @Original_P2 IS NULL AND P2 IS NULL) AND (RawMaterial = @Original_RawMaterial OR @Original_RawMaterial IS NULL AND RawMaterial IS NULL) AND (RegisterDate = @Original_RegisterDate OR @Original_RegisterDate IS NULL AND RegisterDate IS NULL) AND (Registerer = @Original_Registerer OR @Original_Registerer IS NULL AND Registerer IS NULL) AND (Remark = @Original_Remark OR @Original_Remark IS NULL AND Remark IS NULL); SELECT ID, LotNumber, P1, P2, RawMaterial, Active, Remark, RegisterDate, Registerer, IsDelete, DeleteDate FROM LotLedger WHERE (ID = @ID)";
			this.sqlUpdateCommand2.Connection = this.scn;
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LotNumber", System.Data.SqlDbType.VarChar, 200, "LotNumber"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@P1", System.Data.SqlDbType.VarChar, 200, "P1"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@P2", System.Data.SqlDbType.VarChar, 200, "P2"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RawMaterial", System.Data.SqlDbType.Bit, 1, "RawMaterial"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Active", System.Data.SqlDbType.Bit, 1, "Active"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Remark", System.Data.SqlDbType.Bit, 1, "Remark"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegisterDate", System.Data.SqlDbType.DateTime, 4, "RegisterDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Registerer", System.Data.SqlDbType.VarChar, 50, "Registerer"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsDelete", System.Data.SqlDbType.Bit, 1, "IsDelete"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DeleteDate", System.Data.SqlDbType.DateTime, 4, "DeleteDate"));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ID", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Active", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Active", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_DeleteDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DeleteDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_IsDelete", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "IsDelete", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_LotNumber", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "LotNumber", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_P1", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "P1", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_P2", System.Data.SqlDbType.VarChar, 200, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "P2", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RawMaterial", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RawMaterial", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegisterDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegisterDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Registerer", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Registerer", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Remark", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Remark", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand2.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int, 4, "ID"));
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);

		}
		#endregion



		private void btLotCreate_Click(object sender, System.EventArgs e)
		{
			tbLotCreate.Enabled=true;
			this.ViewState["Mode"]=ManagerMode.Create;
		}

		private void btConfirm_Click(object sender, System.EventArgs e)
		{
			if(ViewState["Mode"]==null) return;
			string sLotNumber="";
			switch((ManagerMode)ViewState["Mode"])
			{
				case ManagerMode.Create:
					sLotNumber=tbLotCreate.Text;
					break;
				case ManagerMode.Divide:
					sLotNumber=tbLotDivide.Text;
					break;
				case ManagerMode.Integrate:
					sLotNumber=tbLotIntegrate.Text;
					break;
			}

			int aa=(int)ViewState["Mode"];
			
			if(aa<6 && aa>0)
			{
				if(lbLotNumber.SelectedIndex==-1)
				{
					RegisterStartupScript("RegisterConfirm","<script language=javascript>alert('Lot Number를 선택하십시오');</script>");
					return;
				}
			}

			if(aa<3) 
			{
				if(sLotNumber=="")
				{
					RegisterStartupScript("RegisterConfirm","<script language=javascript>alert('Lot Number를 입력하십시오');</script>");
					return;
				}
			}
			sdaLoad.Fill(dsLotNumber);
			//확인 메시지
			DataRow[] adrAns=dsLotNumber.LotLedger.Select("LotNumber='"+sLotNumber+"'");
			if(adrAns.Length>0)
			{
				for(int i=0;i<adrAns.Length;i++)
				{
					if(((DSLoad.LotLedgerRow)adrAns[i]).Remark==true)
					{
						RegisterStartupScript("RegisterConfirm","<script language=javascript>alert('Lot Number : "+sLotNumber+" 이미 존재하는 Lot Number입니다..');</script>");
						return;
					}
				}
			}
			
			DSLoad.LotLedgerRow llrRow=null;
			switch((ManagerMode)ViewState["Mode"])
			{
				case ManagerMode.Create:
					llrRow=dsLotNumber.LotLedger.NewLotLedgerRow();
					llrRow.Active=true;
					llrRow.LotNumber=sLotNumber;
					llrRow.RegisterDate=DateTime.Now;
					//llrRow.Registerer=; 유저ID
					llrRow.Remark=true;
					dsLotNumber.LotLedger.Rows.Add(llrRow);
					RegisterStartupScript("RegisterConfirm","<script language=javascript>alert('Lot Number : "+sLotNumber+" 등록되었습니다.');</script>");
					break;

				case ManagerMode.Divide:
					llrRow=dsLotNumber.LotLedger.NewLotLedgerRow();
					llrRow.Active=true;
					llrRow.LotNumber=sLotNumber;
					llrRow.RegisterDate=DateTime.Now;
					//llrRow.Registerer=; 유저ID
					llrRow.Remark=true;
					llrRow.P1=lbLotNumber.SelectedItem.Text;
					dsLotNumber.LotLedger.Rows.Add(llrRow);
					RegisterStartupScript("RegisterConfirm","<script language=javascript>alert('Lot Number : "+sLotNumber+" 등록되었습니다.');</script>");
					break;

				case ManagerMode.Integrate:

					llrRow=dsLotNumber.LotLedger.NewLotLedgerRow();
					llrRow.Active=true;
					llrRow.LotNumber=sLotNumber;
					llrRow.RegisterDate=DateTime.Now;
					//llrRow.Registerer=; 유저ID
					llrRow.Remark=true;
					dsLotNumber.LotLedger.Rows.Add(llrRow);

					foreach(ListItem liLot in lbLotNumber.Items)
					{
						if(liLot.Selected)
						{
							llrRow=(DSLoad.LotLedgerRow)dsLotNumber.LotLedger.Select("ID='"+liLot.Value+"'")[0];
							llrRow.P2=sLotNumber;
						}
					}
					RegisterStartupScript("RegisterConfirm","<script language=javascript>alert('Lot Number : "+sLotNumber+" 등록되었습니다.');</script>");
					break;

				case ManagerMode.End:
					foreach(ListItem liLot in lbLotNumber.Items)
					{
						if(liLot.Selected)
						{
							DataRow[] adr=dsLotNumber.LotLedger.Select("LotNumber='"+liLot.Text+"'");
							for(int i=0;i<adr.Length;i++)
							{
								((DSLoad.LotLedgerRow)adr[i]).Active=false;
							}
						}
					}
					RegisterStartupScript("RegisterConfirm","<script language=javascript>alert('선택된 Lot Number들이 종료되었습니다.');</script>");
					break;

				case ManagerMode.Delete:
					foreach(ListItem liLot in lbLotNumber.Items)
					{
						if(liLot.Selected)
						{
							DataRow[] adr=dsLotNumber.LotLedger.Select("LotNumber='"+liLot.Text+"'");
							for(int i=0;i<adr.Length;i++)
							{
								((DSLoad.LotLedgerRow)adr[i]).Active=false;
								((DSLoad.LotLedgerRow)adr[i]).Remark=false;
							}
						}
					}
					RegisterStartupScript("RegisterConfirm","<script language=javascript>alert('선택된 Lot Number들이 삭되었습니다.');</script>");
					break;

				default:
					return;
			}

			
			sdaLoad.Update(dsLotNumber.LotLedger);
			InitForm();
		}

		private void InitForm()
		{
			//재바인딩-정렬
			dsLotNumber.LotLedger.Clear();
			sdaLotNumber.Fill(dsLotNumber);
			lbLotNumber.DataBind();
			
			//초기화
			tbLotCreate.Text="";
			tbLotCreate.Enabled=false;
			tbLotDivide.Text="";
			tbLotDivide.Enabled=false;
			tbLotIntegrate.Text="";
			tbLotIntegrate.Enabled=false;
			
			lbLotNumber.Enabled=false;
			lbLotNumber.AutoPostBack=false;

			tbLotSelect.Text="";

			ViewState["Mode"]=ManagerMode.None;
		}

		private void btLotDivide_Click(object sender, System.EventArgs e)
		{
			lbLotNumber.SelectionMode=ListSelectionMode.Single;
			lbLotNumber.Enabled=true;
			tbLotDivide.Enabled=true;
			ViewState["Mode"]=ManagerMode.Divide;
			lbLotNumber.AutoPostBack=true;
		}

		private void btLotIntegrate_Click(object sender, System.EventArgs e)
		{
			lbLotNumber.SelectionMode=ListSelectionMode.Multiple;
			lbLotNumber.Enabled=true;
			tbLotIntegrate.Enabled=true;
			ViewState["Mode"]=ManagerMode.Integrate;
		}

		private void btLotEnd_Click(object sender, System.EventArgs e)
		{
			lbLotNumber.SelectionMode=ListSelectionMode.Multiple;
			lbLotNumber.Enabled=true;
			ViewState["Mode"]=ManagerMode.End;
		}

		private void lbLotNumber_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if((ManagerMode)ViewState["Mode"]==ManagerMode.Divide)
			{
				tbLotDivide.Text=lbLotNumber.SelectedItem.Text;
			}
			else if((ManagerMode)ViewState["Mode"]==ManagerMode.Select)
			{
				tbLotSelect.Text=lbLotNumber.SelectedItem.Text;
			}
		}

		private void btLotDelete_Click(object sender, System.EventArgs e)
		{
			lbLotNumber.SelectionMode=ListSelectionMode.Multiple;
			lbLotNumber.Enabled=true;
			ViewState["Mode"]=ManagerMode.Delete;
		}

		private void btListAll_Click(object sender, System.EventArgs e)
		{
			sdaLotNumber.SelectCommand.Parameters["@Active"].Value=false;
			dsLotNumber.LotLedger.Clear();
			sdaLotNumber.Fill(dsLotNumber);
			lbLotNumber.DataBind();
		}

		private void btLotSelect_Click(object sender, System.EventArgs e)
		{
			lbLotNumber.SelectionMode=ListSelectionMode.Single;
			lbLotNumber.Enabled=true;
			ViewState["Mode"]=ManagerMode.Select;
			lbLotNumber.AutoPostBack=true;
		}

		private void btClose_Click(object sender, System.EventArgs e)
		{

		}

		private void btSelectCancel_Click(object sender, System.EventArgs e)
		{
			InitForm();
		}
	}
}
