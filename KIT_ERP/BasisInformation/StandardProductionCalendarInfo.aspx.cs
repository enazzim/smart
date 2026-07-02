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

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// ProCalendar에 대한 요약 설명입니다.
	/// </summary>
	public class StandardProductionCalendarInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Calendar Calendar1;
		protected System.Web.UI.WebControls.Label lb_Year;
		protected System.Web.UI.WebControls.Label lb_Month;
		protected System.Web.UI.WebControls.Label lb_Day;
		protected System.Web.UI.WebControls.TextBox tb_Time;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox tb_Content;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected System.Web.UI.WebControls.Label lb_Index;
		protected System.Web.UI.WebControls.DropDownList dl_Year;
		protected System.Web.UI.WebControls.DropDownList dl_Month;
		protected System.Data.SqlClient.SqlConnection conn;
		protected KIT_ERP.BasisInformation.dsCalendar_Standard dsCalendar_Standard1;
		protected System.Data.SqlClient.SqlDataAdapter sqldaProCalendar;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		protected System.Web.UI.WebControls.Button bt_Select;
		protected System.Web.UI.WebControls.RadioButton RadioButton1;
		protected System.Web.UI.WebControls.RadioButton RadioButton2;
		protected System.Web.UI.WebControls.RadioButton RadioButton3;
		protected System.Web.UI.WebControls.RadioButton RadioButton4;
		protected System.Web.UI.WebControls.RadioButton RadioButton5;
		protected System.Web.UI.WebControls.RadioButton RadioButton6;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
	
	

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			conn.ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DSN"] ;
			

			
			if(!Page.IsPostBack)
			{	
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 기준생산달력정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('등록');");
				bt_Update.Attributes.Add("onClick", "return OK('수정');");

				tb_Time.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_Time.Attributes.Add("OnFocus", "OnFocus_Obj(this);");


				dl_Year.SelectedIndex = int.Parse(DateTime.Now.Year.ToString())-2010;
				dl_Month.SelectedIndex = int.Parse(DateTime.Now.Month.ToString())-1;

				sqldaProCalendar.Fill(dsCalendar_Standard1);

				
			}
			
		}

		#region Web Form Designer generated code
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
			this.conn = new System.Data.SqlClient.SqlConnection();
			this.dsCalendar_Standard1 = new KIT_ERP.BasisInformation.dsCalendar_Standard();
			this.sqldaProCalendar = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
			((System.ComponentModel.ISupportInitialize)(this.dsCalendar_Standard1)).BeginInit();
			this.bt_Select.Click += new System.EventHandler(this.bt_Select_Click);
			this.Calendar1.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler(this.Calendar1_DayRender);
			this.Calendar1.SelectionChanged += new System.EventHandler(this.Calendar1_SelectionChanged);
			this.RadioButton1.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
			this.RadioButton2.CheckedChanged += new System.EventHandler(this.RadioButton2_CheckedChanged);
			this.RadioButton3.CheckedChanged += new System.EventHandler(this.RadioButton3_CheckedChanged);
			this.RadioButton4.CheckedChanged += new System.EventHandler(this.RadioButton4_CheckedChanged);
			this.RadioButton5.CheckedChanged += new System.EventHandler(this.RadioButton5_CheckedChanged);
			this.RadioButton6.CheckedChanged += new System.EventHandler(this.RadioButton6_CheckedChanged);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			// 
			// conn
			this.conn.ConnectionString = "workstation id=\"EZSYS-ERP01\";packet size=4096;user id=sa;data source=\"220.66.115.9\";persist " +
				"security info=True;initial catalog=Sindong_ERP;password=\"ezsys#0509\"";
			//this.conn.ConnectionString = "workstation id=109LAB_이해동;packet size=4096;user id=sa;data source=\"220.66.115.10\"" +
			//	";persist security info=False;initial catalog=Shindong_ERP";
			// 
			// dsCalendar_Standard1
			// 
			this.dsCalendar_Standard1.DataSetName = "dsCalendar_Standard";
			this.dsCalendar_Standard1.Locale = new System.Globalization.CultureInfo("ko-KR");
			// 
			// sqldaProCalendar
			// 
			this.sqldaProCalendar.DeleteCommand = this.sqlDeleteCommand1;
			this.sqldaProCalendar.InsertCommand = this.sqlInsertCommand1;
			this.sqldaProCalendar.SelectCommand = this.sqlSelectCommand1;
			this.sqldaProCalendar.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "PCI_MT", new System.Data.Common.DataColumnMapping[] {
																																																				 new System.Data.Common.DataColumnMapping("WCName", "WCName"),
																																																				 new System.Data.Common.DataColumnMapping("WC_Year", "WC_Year"),
																																																				 new System.Data.Common.DataColumnMapping("WC_Month", "WC_Month"),
																																																				 new System.Data.Common.DataColumnMapping("WC_Day", "WC_Day"),
																																																				 new System.Data.Common.DataColumnMapping("WC_Time", "WC_Time"),
																																																				 new System.Data.Common.DataColumnMapping("Content", "Content"),
																																																				 new System.Data.Common.DataColumnMapping("RecodingState", "RecodingState"),
																																																				 new System.Data.Common.DataColumnMapping("ProductionCalendarInfoIndex", "ProductionCalendarInfoIndex")})});
			this.sqldaProCalendar.UpdateCommand = this.sqlUpdateCommand1;
			// 
			// sqlDeleteCommand1
			// 
			this.sqlDeleteCommand1.CommandText = @"DELETE FROM PCI_MT WHERE (ProductionCalendarInfoIndex = @Original_ProductionCalendarInfoIndex) AND (Content = @Original_Content OR @Original_Content IS NULL AND Content IS NULL) AND (RecodingState = @Original_RecodingState OR @Original_RecodingState IS NULL AND RecodingState IS NULL) AND (WCName = @Original_WCName OR @Original_WCName IS NULL AND WCName IS NULL) AND (WC_Day = @Original_WC_Day OR @Original_WC_Day IS NULL AND WC_Day IS NULL) AND (WC_Month = @Original_WC_Month OR @Original_WC_Month IS NULL AND WC_Month IS NULL) AND (WC_Time = @Original_WC_Time OR @Original_WC_Time IS NULL AND WC_Time IS NULL) AND (WC_Year = @Original_WC_Year OR @Original_WC_Year IS NULL AND WC_Year IS NULL)";
			this.sqlDeleteCommand1.Connection = this.conn;
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductionCalendarInfoIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductionCalendarInfoIndex", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Content", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Content", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RecodingState", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RecodingState", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WCName", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WCName", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WC_Day", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WC_Day", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WC_Month", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WC_Month", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WC_Time", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WC_Time", System.Data.DataRowVersion.Original, null));
			this.sqlDeleteCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WC_Year", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WC_Year", System.Data.DataRowVersion.Original, null));
			// 
			// sqlInsertCommand1
			// 
			this.sqlInsertCommand1.CommandText = @"INSERT INTO PCI_MT(WCName, WC_Year, WC_Month, WC_Day, WC_Time, Content, RecodingState) VALUES (@WCName, @WC_Year, @WC_Month, @WC_Day, @WC_Time, @Content, @RecodingState); SELECT WCName, WC_Year, WC_Month, WC_Day, WC_Time, Content, RecodingState, ProductionCalendarInfoIndex FROM PCI_MT WHERE (ProductionCalendarInfoIndex = @@IDENTITY)";
			this.sqlInsertCommand1.Connection = this.conn;
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WCName", System.Data.SqlDbType.VarChar, 20, "WCName"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WC_Year", System.Data.SqlDbType.Int, 4, "WC_Year"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WC_Month", System.Data.SqlDbType.Int, 4, "WC_Month"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WC_Day", System.Data.SqlDbType.Int, 4, "WC_Day"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WC_Time", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WC_Time", System.Data.DataRowVersion.Current, null));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Content", System.Data.SqlDbType.VarChar, 50, "Content"));
			this.sqlInsertCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecodingState", System.Data.SqlDbType.Bit, 1, "RecodingState"));
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = "SELECT WCName, WC_Year, WC_Month, WC_Day, WC_Time, Content, RecodingState, Produc" +
				"tionCalendarInfoIndex FROM PCI_MT WHERE (WCName = \'기준 작업장\')";
			this.sqlSelectCommand1.Connection = this.conn;
			// 
			// sqlUpdateCommand1
			// 
			this.sqlUpdateCommand1.CommandText = @"UPDATE PCI_MT SET WCName = @WCName, WC_Year = @WC_Year, WC_Month = @WC_Month, WC_Day = @WC_Day, WC_Time = @WC_Time, Content = @Content, RecodingState = @RecodingState WHERE (ProductionCalendarInfoIndex = @Original_ProductionCalendarInfoIndex) AND (Content = @Original_Content OR @Original_Content IS NULL AND Content IS NULL) AND (RecodingState = @Original_RecodingState OR @Original_RecodingState IS NULL AND RecodingState IS NULL) AND (WCName = @Original_WCName OR @Original_WCName IS NULL AND WCName IS NULL) AND (WC_Day = @Original_WC_Day OR @Original_WC_Day IS NULL AND WC_Day IS NULL) AND (WC_Month = @Original_WC_Month OR @Original_WC_Month IS NULL AND WC_Month IS NULL) AND (WC_Time = @Original_WC_Time OR @Original_WC_Time IS NULL AND WC_Time IS NULL) AND (WC_Year = @Original_WC_Year OR @Original_WC_Year IS NULL AND WC_Year IS NULL); SELECT WCName, WC_Year, WC_Month, WC_Day, WC_Time, Content, RecodingState, ProductionCalendarInfoIndex FROM PCI_MT WHERE (ProductionCalendarInfoIndex = @ProductionCalendarInfoIndex)";
			this.sqlUpdateCommand1.Connection = this.conn;
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WCName", System.Data.SqlDbType.VarChar, 20, "WCName"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WC_Year", System.Data.SqlDbType.Int, 4, "WC_Year"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WC_Month", System.Data.SqlDbType.Int, 4, "WC_Month"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WC_Day", System.Data.SqlDbType.Int, 4, "WC_Day"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WC_Time", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WC_Time", System.Data.DataRowVersion.Current, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Content", System.Data.SqlDbType.VarChar, 50, "Content"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RecodingState", System.Data.SqlDbType.Bit, 1, "RecodingState"));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_ProductionCalendarInfoIndex", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ProductionCalendarInfoIndex", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_Content", System.Data.SqlDbType.VarChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Content", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_RecodingState", System.Data.SqlDbType.Bit, 1, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "RecodingState", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WCName", System.Data.SqlDbType.VarChar, 20, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WCName", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WC_Day", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WC_Day", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WC_Month", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WC_Month", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WC_Time", System.Data.SqlDbType.Decimal, 13, System.Data.ParameterDirection.Input, false, ((System.Byte)(20)), ((System.Byte)(2)), "WC_Time", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Original_WC_Year", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WC_Year", System.Data.DataRowVersion.Original, null));
			this.sqlUpdateCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ProductionCalendarInfoIndex", System.Data.SqlDbType.Int, 4, "ProductionCalendarInfoIndex"));
			this.Load += new System.EventHandler(this.Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.dsCalendar_Standard1)).EndInit();

		}
		#endregion

		private void Calendar1_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
		{
			
			CalendarDay day = e.Day;
			TableCell cell = e.Cell;

			
			//데이타셋의 내용을 전부 읽음
			foreach(DataRow dr in dsCalendar_Standard1.Tables[0].Rows)
			{ 
				//렌더링되는 달의 날짜일때만 동작 
				if(!day.IsOtherMonth)
				{
						
					if(dr["WC_Year"].ToString() == Convert.ToString(day.Date.Year) && dr["WC_Month"].ToString() == Convert.ToString(day.Date.Month) &&
						dr["WC_Day"].ToString() == Convert.ToString(day.Date.Day))					
					{

						//데이타셋과 일치하는 날짜가 있으면 그날짜의 업무 시간을 보고 셀 배경색을 넣어줌.
						switch(Convert.ToString(Math.Round(decimal.Parse(dr["WC_Time"].ToString()),0))) 
						{
							case "600":
								cell.BackColor = Color.Green;
								break;
							case "480":
								cell.BackColor = Color.Olive;
								break;
							case "360":
								cell.BackColor = Color.Chartreuse;
								break;
							case "240":
								cell.BackColor = Color.Blue;
								break;
							case "0":
								cell.BackColor = Color.Red;
								break;
							default:
								cell.BackColor = Color.DarkOrange;
								break;
						}

						//내용이 있으면 리트럴컨트롤을 사용하여 달력의 날짜바로 밑에 넣어둠.
						if(dr["Content"].ToString() != "")
						{
							cell.Controls.Add(new LiteralControl("<br><font size=2>" + dr["Content"].ToString() + "</font>"));
						}
					}
				}		
				else
				{
					//다른 달의 날짜를 렌더링할때...
					//cell.Controls.Clear();
					cell.BackColor = Color.LightGray;
				}
			}

			sqldaProCalendar.Fill(dsCalendar_Standard1);
			
		}

		private void Calendar1_SelectionChanged(object sender, System.EventArgs e)
		{
			lb_Year.Text = Calendar1.SelectedDate.Year.ToString();
			lb_Month.Text = Calendar1.SelectedDate.Month.ToString();
			lb_Day.Text = Calendar1.SelectedDate.Day.ToString();

			// 생산달력 인덱스번호 찾기(수정시 사용)
			Find_Index();

			sqldaProCalendar.Fill(dsCalendar_Standard1);


			//데이타셋의 내용을 전부읽음
			foreach(DataRow dr in dsCalendar_Standard1.Tables[0].Rows)
			{ 
				//선택한 날짜와 데이타셋의 날짜가 같은것이 있는지 검색
				if(dr["WC_Year"].ToString() == lb_Year.Text && dr["WC_Month"].ToString() == lb_Month.Text &&
					dr["WC_Day"].ToString() == lb_Day.Text)
				{
					//시간을 본후 그 시간에 맞춰 드롭다운리스트의 색을 선택해줌.
					switch(Convert.ToString(Math.Round(decimal.Parse(dr["WC_Time"].ToString().Trim()),0))) 
					{
						case "600":
							RadioButton1.Checked = true;
							RadioButton2.Checked = false;
							RadioButton3.Checked = false;
							RadioButton4.Checked = false;
							RadioButton5.Checked = false;
							RadioButton6.Checked = false;
							tb_Time.Enabled = false;
							break;
						case "480":
							RadioButton1.Checked = false;
							RadioButton2.Checked = true;
							RadioButton3.Checked = false;
							RadioButton4.Checked = false;
							RadioButton5.Checked = false;
							RadioButton6.Checked = false;
							tb_Time.Enabled = false;
							break;
						case "360":
							RadioButton1.Checked = false;
							RadioButton2.Checked = false;
							RadioButton3.Checked = true;
							RadioButton4.Checked = false;
							RadioButton5.Checked = false;
							RadioButton6.Checked = false;
							tb_Time.Enabled = false;
							break;
						case "240":
							RadioButton1.Checked = false;
							RadioButton2.Checked = false;
							RadioButton3.Checked = false;
							RadioButton4.Checked = true;
							RadioButton5.Checked = false;
							RadioButton6.Checked = false;
							tb_Time.Enabled = false;
							break;
						case "0":
							RadioButton1.Checked = false;
							RadioButton2.Checked = false;
							RadioButton3.Checked = false;
							RadioButton4.Checked = false;
							RadioButton5.Checked = false;
							RadioButton6.Checked = true;
							tb_Time.Enabled = false;
							break;
						default:
							RadioButton1.Checked = false;
							RadioButton2.Checked = false;
							RadioButton3.Checked = false;
							RadioButton4.Checked = false;
							RadioButton5.Checked = true;
							RadioButton6.Checked = false;
							tb_Time.Enabled = true;
							break;
					}
					tb_Time.Text = Convert.ToString(Math.Round(decimal.Parse(dr["WC_Time"].ToString()),0));
					tb_Content.Text = dr["Content"].ToString().Trim();
					
				}
			}
		}


		

		/// <summary>
		/// 수정버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(lb_Year.Text == "")
			{
				RegisterStartupScript("","<script>alert('날짜를 선택해 주세요');</script>");
			}
			else
			{

				ArrayList arr = new ArrayList();

				arr.Add("기준 작업장");
				arr.Add(int.Parse(lb_Year.Text));
				arr.Add(int.Parse(lb_Month.Text));
				arr.Add(int.Parse(lb_Day.Text));
				arr.Add(Decimal.Parse(tb_Time.Text));
				arr.Add(tb_Content.Text);
				arr.Add(int.Parse(lb_Index.Text));

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD("WCProductionCalendarInfo","Update","PCI_MT",(int)PageName.WCProductionCalendarInfo,Session["ID"].ToString(),arr,"낱개입력");
				rud.ActivateRUD();


				string nextday = Calendar1.SelectedDate.Date.AddDays(1).ToShortDateString();
					
				//다음날을 달력컨트롤에 선택날짜로 만든후 레이블에 월과 일을 넣어줌.
				Calendar1.SelectedDate = DateTime.Parse(nextday.ToString());
				
				lb_Year.Text = 	Calendar1.SelectedDate.Year.ToString();				
				lb_Month.Text = Calendar1.SelectedDate.Month.ToString();
				lb_Day.Text =Calendar1.SelectedDate.Day.ToString();
		
			}
			Find_Index();
			sqldaProCalendar.Fill(dsCalendar_Standard1);
		}

		/// <summary>
		/// 등록버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
		
			if(lb_Year.Text == "")
			{
				RegisterStartupScript("","<script>alert('날짜를 선택해 주세요');</script>");
			}
			else
			{

				ArrayList arr = new ArrayList();

				arr.Add("기준 작업장");
				arr.Add(int.Parse(lb_Year.Text));
				arr.Add(int.Parse(lb_Month.Text));
				arr.Add(int.Parse(lb_Day.Text));
				arr.Add(Decimal.Parse(tb_Time.Text));
				arr.Add(tb_Content.Text);
				

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD("WCProductionCalendarInfo","Registration","PCI_MT",(int)PageName.WCProductionCalendarInfo,Session["ID"].ToString(),arr,"낱개입력");
				rud.ActivateRUD();

				//다른 원장에도 필드를 채운다
				conn.ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DSN"] ;
				conn.Open();

				string str = "Select * From WCI_MT Where RecodingState = 1";
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet ds = new DataSet();
				da.Fill(ds);

				foreach(DataRow drow in ds.Tables[0].Rows)
				{
					str = "Insert into PCI_MT (WCName, WC_Year, WC_Month, WC_Day, WC_Time, Content, RecodingState) values(@name,@year,@mon,@day,@time,@content,'1')";
					SqlCommand comm1 = new SqlCommand(str,conn);
					comm1.Parameters.Add("@name",SqlDbType.VarChar).Value = drow["WCName"].ToString();
					comm1.Parameters.Add("@year",SqlDbType.Int).Value = int.Parse(lb_Year.Text);
					comm1.Parameters.Add("@mon",SqlDbType.Int).Value = int.Parse(lb_Month.Text);
					comm1.Parameters.Add("@day",SqlDbType.Int).Value = int.Parse(lb_Day.Text);
					comm1.Parameters.Add("@time",SqlDbType.Decimal).Value = decimal.Parse(tb_Time.Text);
					comm1.Parameters.Add("@content",SqlDbType.VarChar).Value = tb_Content.Text;

					comm1.ExecuteNonQuery();
				}
				conn.Close();

				


				string nextday = Calendar1.SelectedDate.Date.AddDays(1).ToShortDateString();
					
				//다음날을 달력컨트롤에 선택날짜로 만든후 레이블에 월과 일을 넣어줌.
				Calendar1.SelectedDate = DateTime.Parse(nextday.ToString());
				
				lb_Year.Text = 	Calendar1.SelectedDate.Year.ToString();				
				lb_Month.Text = Calendar1.SelectedDate.Month.ToString();
				lb_Day.Text =Calendar1.SelectedDate.Day.ToString();
			}

			sqldaProCalendar.Fill(dsCalendar_Standard1);
		}
		


		/// <summary>
		/// 인덱스번호 찾기
		/// </summary>
		private void Find_Index()
		{
			// 생산달력 인덱스번호 찾기(수정시 사용)
			conn.Open();
			string str = "Select * From PCI_MT Where WCName = '기준 작업장' and WC_Year = @year and WC_Month = @month and WC_Day = @day";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Parameters.Add("@year", SqlDbType.VarChar).Value = lb_Year.Text;
			comm.Parameters.Add("@month", SqlDbType.VarChar).Value = lb_Month.Text;
			comm.Parameters.Add("@day", SqlDbType.VarChar).Value = lb_Day.Text;
			SqlDataReader dr1 = comm.ExecuteReader();
			while(dr1.Read())
			{
				lb_Index.Text = Convert.ToString(dr1["ProductionCalendarInfoIndex"]);
			}
			conn.Close();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			sqldaProCalendar.Fill(dsCalendar_Standard1);

			string date = dl_Month.SelectedItem.Value + "/01/"+ dl_Year.SelectedItem.Value;
			Calendar1.SelectedDate = DateTime.Parse(date);//선택날짜
			Calendar1.VisibleDate = DateTime.Parse(date);//선택날짜 보이기
		}

		private void RadioButton1_CheckedChanged(object sender, System.EventArgs e)
		{
			RadioButton1.Checked = true;
			RadioButton2.Checked = false;
			RadioButton3.Checked = false;
			RadioButton4.Checked = false;
			RadioButton5.Checked = false;
			RadioButton6.Checked = false;
			tb_Time.Text = "600";
			tb_Time.Enabled = false;
			
		}

		private void RadioButton2_CheckedChanged(object sender, System.EventArgs e)
		{
			RadioButton1.Checked = false;
			RadioButton2.Checked = true;
			RadioButton3.Checked = false;
			RadioButton4.Checked = false;
			RadioButton5.Checked = false;
			RadioButton6.Checked = false;
			tb_Time.Text = "480";
			tb_Time.Enabled = false;
		}

		private void RadioButton3_CheckedChanged(object sender, System.EventArgs e)
		{
			RadioButton1.Checked = false;
			RadioButton2.Checked = false;
			RadioButton3.Checked = true;
			RadioButton4.Checked = false;
			RadioButton5.Checked = false;
			RadioButton6.Checked = false;
			tb_Time.Text = "360";
			tb_Time.Enabled = false;
		}

		private void RadioButton4_CheckedChanged(object sender, System.EventArgs e)
		{
			RadioButton1.Checked = false;
			RadioButton2.Checked = false;
			RadioButton3.Checked = false;
			RadioButton4.Checked = true;
			RadioButton5.Checked = false;
			RadioButton6.Checked = false;
			tb_Time.Text = "240";
			tb_Time.Enabled = false;
		}

		private void RadioButton5_CheckedChanged(object sender, System.EventArgs e)
		{
			RadioButton1.Checked = false;
			RadioButton2.Checked = false;
			RadioButton3.Checked = false;
			RadioButton4.Checked = false;
			RadioButton5.Checked = true;
			RadioButton6.Checked = false;
			tb_Time.Text = "0";
			tb_Time.Enabled = true;
		}

		private void RadioButton6_CheckedChanged(object sender, System.EventArgs e)
		{
			RadioButton1.Checked = false;
			RadioButton2.Checked = false;
			RadioButton3.Checked = false;
			RadioButton4.Checked = false;
			RadioButton5.Checked = false;
			RadioButton6.Checked = true;
			tb_Time.Text = "0";	
			tb_Time.Enabled = false;
		}

		private void bt_Select_Click(object sender, System.EventArgs e)
		{
			sqldaProCalendar.Fill(dsCalendar_Standard1);

			string date = dl_Month.SelectedItem.Value + "/01/"+ dl_Year.SelectedItem.Value;
			Calendar1.SelectedDate = DateTime.Parse(date);
			Calendar1.VisibleDate = DateTime.Parse(date);
		}
	}
}

