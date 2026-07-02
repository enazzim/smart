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
using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// WorkDiaryAdjustment에 대한 요약 설명입니다.
	/// </summary>
	public class WorkDiaryAdjustment : System.Web.UI.Page
	{
		protected System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		protected System.Data.SqlClient.SqlConnection sqlConnection1;
		protected KIT_ERP.Community.DataSet1 dataSet11;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		string yymmdd = "";
			
		private void Page_Load(object sender, System.EventArgs e)
		{	
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Homepage/default.htm';</script>");
			}
			string orginValidateCode;
			orginValidateCode = "if(typeof(Page_ClientValidate) == 'function')";
			orginValidateCode += "if(!Page_ClientValidate()) return false;";

			string MyScriptCode;
			MyScriptCode = "if(!Closing()) return false;";

			this.Button1.Attributes["onclick"] = orginValidateCode + MyScriptCode;
			Button1.Attributes.Add("onclick", "return Closing();");
							
			yymmdd = Request.QueryString["WorkDateTitle"].ToString().Substring(0, 10);
			
			sqlConnection1.ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DSN"];

			
			Label1.Text = Request.QueryString[0].ToString().Substring(0,4).Trim();
			Label2.Text = Request.QueryString[0].ToString().Substring(5,2).Trim();
			Label3.Text = Request.QueryString[0].ToString().Substring(8,2).Trim();


			if(Session["ID"].ToString() == "admin")
			{
				Button1.Enabled = true;
			}

			if(!Page.IsPostBack)
			{
				sqlDataAdapter1.SelectCommand.Parameters["@yymmdd"].Value = yymmdd;
				sqlDataAdapter1.Fill(dataSet11);
				DataGrid1.DataSource = dataSet11;
				DataGrid1.DataBind();
				Closing();                  // 마감여부 확인
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
			this.dataSet11 = new KIT_ERP.Community.DataSet1();
			((System.ComponentModel.ISupportInitialize)(this.dataSet11)).BeginInit();
			this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
			this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
			this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.DeleteCommand = this.sqlDeleteCommand1;
			this.sqlDataAdapter1.InsertCommand = this.sqlInsertCommand1;
			this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "CWD_T", new System.Data.Common.DataColumnMapping[] {
																																																			   new System.Data.Common.DataColumnMapping("CWorkDiaryIndex", "CWorkDiaryIndex"),
																																																			   new System.Data.Common.DataColumnMapping("WorkDateTitle", "WorkDateTitle"),
																																																			   new System.Data.Common.DataColumnMapping("Id", "Id"),
																																																			   new System.Data.Common.DataColumnMapping("Name", "Name"),
																																																			   new System.Data.Common.DataColumnMapping("MorningWork", "MorningWork"),
																																																			   new System.Data.Common.DataColumnMapping("AfternoonWork", "AfternoonWork"),
																																																			   new System.Data.Common.DataColumnMapping("NightWork", "NightWork"),
																																																			   new System.Data.Common.DataColumnMapping("TomorrowWork", "TomorrowWork"),
																																																			   new System.Data.Common.DataColumnMapping("Listing", "Listing"),
																																																			   new System.Data.Common.DataColumnMapping("Closing", "Closing"),
																																																			   new System.Data.Common.DataColumnMapping("NoWriter", "NoWriter"),
																																																			   new System.Data.Common.DataColumnMapping("RegistrationDate", "RegistrationDate")})});
			this.sqlDataAdapter1.UpdateCommand = this.sqlUpdateCommand1;
			// 
			// sqlDeleteCommand1
			// 
			this.sqlDeleteCommand1.CommandText = @"DELETE FROM CWD_T WHERE (CWorkDiaryIndex = @Original_CWorkDiaryIndex) AND (AfternoonWork = @Original_AfternoonWork OR @Original_AfternoonWork IS NULL AND AfternoonWork IS NULL) AND (Closing = @Original_Closing OR @Original_Closing IS NULL AND Closing IS NULL) AND (Id = @Original_Id OR @Original_Id IS NULL AND Id IS NULL) AND (Listing = @Original_Listing OR @Original_Listing IS NULL AND Listing IS NULL) AND (MorningWork = @Original_MorningWork OR @Original_MorningWork IS NULL AND MorningWork IS NULL) AND (Name = @Original_Name OR @Original_Name IS NULL AND Name IS NULL) AND (NightWork = @Original_NightWork OR @Original_NightWork IS NULL AND NightWork IS NULL) AND (NoWriter = @Original_NoWriter OR @Original_NoWriter IS NULL AND NoWriter IS NULL) AND (RegistrationDate = @Original_RegistrationDate OR @Original_RegistrationDate IS NULL AND RegistrationDate IS NULL) AND (TomorrowWork = @Original_TomorrowWork OR @Original_TomorrowWork IS NULL AND TomorrowWork IS NULL) AND (WorkDateTitle = @Original_WorkDateTitle OR @Original_WorkDateTitle IS NULL AND WorkDateTitle IS NULL)";
			this.sqlDeleteCommand1.Connection = this.sqlConnection1;
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CWorkDiaryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWorkDiaryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_AfternoonWork", System.Data.SqlDbType.VarChar, 7000, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "AfternoonWork", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Closing", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Closing", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Id", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Listing", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Listing", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MorningWork", System.Data.SqlDbType.VarChar, 7000, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "MorningWork", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Name", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Name", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_NightWork", System.Data.SqlDbType.VarChar, 7000, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "NightWork", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_NoWriter", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "NoWriter", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationDate", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TomorrowWork", System.Data.SqlDbType.VarChar, 7000, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TomorrowWork", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WorkDateTitle", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WorkDateTitle", System.Data.DataRowVersion.Original, null));
			// 
			// sqlConnection1
			// 
			this.sqlConnection1.ConnectionString = "workstation id=RYU;packet size=4096;user id=sa;data source=\"220.66.115.10\";persis" +
				"t security info=False;initial catalog=KIT_ERP";
			// 
			// sqlInsertCommand1
			// 
			this.sqlInsertCommand1.CommandText = @"INSERT INTO CWD_T(WorkDateTitle, Id, Name, MorningWork, AfternoonWork, NightWork, TomorrowWork, Listing, Closing, NoWriter, RegistrationDate) VALUES (@WorkDateTitle, @Id, @Name, @MorningWork, @AfternoonWork, @NightWork, @TomorrowWork, @Listing, @Closing, @NoWriter, @RegistrationDate); SELECT CWorkDiaryIndex, WorkDateTitle, Id, Name, MorningWork, AfternoonWork, NightWork, TomorrowWork, Listing, Closing, NoWriter, RegistrationDate FROM CWD_T WHERE (CWorkDiaryIndex = @@IDENTITY)";
			this.sqlInsertCommand1.Connection = this.sqlConnection1;
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkDateTitle", System.Data.SqlDbType.VarChar, 50, "WorkDateTitle"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.VarChar, 50, "Id"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.VarChar, 50, "Name"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MorningWork", System.Data.SqlDbType.VarChar, 7000, "MorningWork"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AfternoonWork", System.Data.SqlDbType.VarChar, 7000, "AfternoonWork"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NightWork", System.Data.SqlDbType.VarChar, 7000, "NightWork"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TomorrowWork", System.Data.SqlDbType.VarChar, 7000, "TomorrowWork"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Listing", System.Data.SqlDbType.Bit, 1, "Listing"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Closing", System.Data.SqlDbType.VarChar, 10, "Closing"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NoWriter", System.Data.SqlDbType.Int, 4, "NoWriter"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationDate", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "SELECT CWorkDiaryIndex, WorkDateTitle, Id, Name, MorningWork, AfternoonWork, Nigh" +
				"tWork, TomorrowWork, Listing, Closing, NoWriter, RegistrationDate FROM CWD_T WHE" +
				"RE (WorkDateTitle = @yymmdd)";
			this.sqlSelectCommand1.Connection = this.sqlConnection1;
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@yymmdd", System.Data.SqlDbType.VarChar, 50, "WorkDateTitle"));
			// 
			// sqlUpdateCommand1
			// 
			this.sqlUpdateCommand1.CommandText = "UPDATE CWD_T SET WorkDateTitle = @WorkDateTitle, Id = @Id, Name = @Name, MorningW" +
				"ork = @MorningWork, AfternoonWork = @AfternoonWork, NightWork = @NightWork, Tomo" +
				"rrowWork = @TomorrowWork, Listing = @Listing, Closing = @Closing, NoWriter = @No" +
				"Writer, RegistrationDate = @RegistrationDate WHERE (CWorkDiaryIndex = @Original_" +
				"CWorkDiaryIndex) AND (AfternoonWork = @Original_AfternoonWork OR @Original_After" +
				"noonWork IS NULL AND AfternoonWork IS NULL) AND (Closing = @Original_Closing OR " +
				"@Original_Closing IS NULL AND Closing IS NULL) AND (Id = @Original_Id OR @Origin" +
				"al_Id IS NULL AND Id IS NULL) AND (Listing = @Original_Listing OR @Original_List" +
				"ing IS NULL AND Listing IS NULL) AND (MorningWork = @Original_MorningWork OR @Or" +
				"iginal_MorningWork IS NULL AND MorningWork IS NULL) AND (Name = @Original_Name O" +
				"R @Original_Name IS NULL AND Name IS NULL) AND (NightWork = @Original_NightWork " +
				"OR @Original_NightWork IS NULL AND NightWork IS NULL) AND (NoWriter = @Original_" +
				"NoWriter OR @Original_NoWriter IS NULL AND NoWriter IS NULL) AND (RegistrationDa" +
				"te = @Original_RegistrationDate OR @Original_RegistrationDate IS NULL AND Regist" +
				"rationDate IS NULL) AND (TomorrowWork = @Original_TomorrowWork OR @Original_Tomo" +
				"rrowWork IS NULL AND TomorrowWork IS NULL) AND (WorkDateTitle = @Original_WorkDa" +
				"teTitle OR @Original_WorkDateTitle IS NULL AND WorkDateTitle IS NULL); SELECT CW" +
				"orkDiaryIndex, WorkDateTitle, Id, Name, MorningWork, AfternoonWork, NightWork, T" +
				"omorrowWork, Listing, Closing, NoWriter, RegistrationDate FROM CWD_T WHERE (CWor" +
				"kDiaryIndex = @CWorkDiaryIndex)";
			this.sqlUpdateCommand1.Connection = this.sqlConnection1;
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WorkDateTitle", System.Data.SqlDbType.VarChar, 50, "WorkDateTitle"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Id", System.Data.SqlDbType.VarChar, 50, "Id"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Name", System.Data.SqlDbType.VarChar, 50, "Name"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MorningWork", System.Data.SqlDbType.VarChar, 7000, "MorningWork"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AfternoonWork", System.Data.SqlDbType.VarChar, 7000, "AfternoonWork"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NightWork", System.Data.SqlDbType.VarChar, 7000, "NightWork"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TomorrowWork", System.Data.SqlDbType.VarChar, 7000, "TomorrowWork"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Listing", System.Data.SqlDbType.Bit, 1, "Listing"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Closing", System.Data.SqlDbType.VarChar, 10, "Closing"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NoWriter", System.Data.SqlDbType.Int, 4, "NoWriter"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RegistrationDate", System.Data.SqlDbType.DateTime, 4, "RegistrationDate"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_CWorkDiaryIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CWorkDiaryIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_AfternoonWork", System.Data.SqlDbType.VarChar, 7000, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "AfternoonWork", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Closing", System.Data.SqlDbType.VarChar, 10, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Closing", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Id", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Id", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Listing", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Listing", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_MorningWork", System.Data.SqlDbType.VarChar, 7000, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "MorningWork", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Name", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Name", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_NightWork", System.Data.SqlDbType.VarChar, 7000, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "NightWork", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_NoWriter", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "NoWriter", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RegistrationDate", System.Data.SqlDbType.DateTime, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RegistrationDate", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_TomorrowWork", System.Data.SqlDbType.VarChar, 7000, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TomorrowWork", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WorkDateTitle", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WorkDateTitle", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CWorkDiaryIndex", System.Data.SqlDbType.Int, 4, "CWorkDiaryIndex"));
			// 
			// dataSet11
			// 
			this.dataSet11.DataSetName = "DataSet1";
			this.dataSet11.Locale = new System.Globalization.CultureInfo("ko-KR");
			this.Load += new System.EventHandler(this.Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataSet11)).EndInit();

		}
		#endregion


		// 마감후 수정할 수 없음
		private void Closing()
		{
			string Closing = "";
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;			
			Con.Open();
			Cmd.CommandText = "SELECT WorkDateTitle, Closing FROM CWD_T WHERE WorkDateTitle = @WorkDateTitle Group By WorkDateTitle, Closing";
			Cmd.Parameters.Add("@WorkDateTitle", SqlDbType.VarChar).Value = yymmdd;
			
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

			if(reader.Read())
			{
				Closing = reader["Closing"].ToString();

				if(Closing == "마감")
					DataGrid1.Columns[0].Visible = false;
			}
			reader.Close();
			Con.Close();
		}
		


		private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGrid1.SelectedIndex = -1;


			//ID를 비교해서 동일하면 수정가능하도록 한다.
			
			sqlDataAdapter1.SelectCommand.Parameters["@yymmdd"].Value = yymmdd;
			sqlDataAdapter1.Fill(dataSet11);

			if(Session["ID"].ToString() == dataSet11.Tables[0].Rows[e.Item.ItemIndex]["Id"].ToString())
			{

//				sqlDataAdapter1.SelectCommand.Parameters["@yymmdd"].Value = yymmdd;
//				sqlDataAdapter1.Fill(dataSet11);
				DataGrid1.EditItemIndex = e.Item.ItemIndex;
				DataGrid1.DataSource = dataSet11;
				DataGrid1.DataBind();
			}
			else
			{
				Response.Write("<script>alert('수정할 수 없습니다!');</script>");
			}

			
		}

		private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			sqlDataAdapter1.SelectCommand.Parameters["@yymmdd"].Value = yymmdd;
			sqlDataAdapter1.Fill(dataSet11);
			DataGrid1.EditItemIndex = -1;
			DataGrid1.DataBind();	
		}

		private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			DataGrid1.SelectedIndex = -1;
			DataGrid1.EditItemIndex = -1;

			sqlDataAdapter1.SelectCommand.Parameters["@yymmdd"].Value = yymmdd;
			sqlDataAdapter1.Fill(dataSet11);

			string UpdateItemID;
			UpdateItemID = DataGrid1.DataKeys[e.Item.ItemIndex].ToString();

			DataRow[] Row = dataSet11.Tables[0].Select("CWorkDiaryIndex = " + UpdateItemID);
			Row[0]["MorningWork"] = ((TextBox)(e.Item.Cells[3].Controls[1])).Text;
			Row[0]["AfternoonWork"] = ((TextBox)(e.Item.Cells[4].Controls[1])).Text;
			Row[0]["NightWork"] = ((TextBox)(e.Item.Cells[5].Controls[1])).Text;
			Row[0]["TomorrowWork"] = ((TextBox)(e.Item.Cells[6].Controls[1])).Text;
            			
			sqlDataAdapter1.Update(dataSet11);
			DataGrid1.DataBind();

		}


		// 마감버튼...
		private void Button1_Click(object sender, System.EventArgs e)
		{
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();			
			SqlTransaction trans = Con.BeginTransaction();			
			Cmd.Transaction = trans;

			try
			{
				Cmd.CommandText = "UPDATE CWD_T SET Closing=@Closing WHERE WorkDateTitle=@yymmdd";
				Cmd.Parameters.Add("@Closing", SqlDbType.VarChar).Value = "마감";
				Cmd.Parameters.Add("@yymmdd", SqlDbType.VarChar).Value = yymmdd;
				
				Cmd.ExecuteNonQuery();
				trans.Commit();
			}
			catch(Exception ex)
			{
				Response.Write(ex.Message);
				trans.Rollback();
			}
			finally
			{				
				Con.Close();				
				Response.Redirect("WorkDiary.aspx");				
			}
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("WorkDiary.aspx");
		}
	}
}
