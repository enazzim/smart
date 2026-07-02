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


namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// ProductionCalendarInfo에 대한 요약 설명입니다.
	/// </summary>
	public class ProductionCalendarInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Calendar Calendar1;
		protected System.Web.UI.WebControls.DropDownList dl_Month;
		protected System.Web.UI.WebControls.DropDownList dl_Year;
		protected System.Web.UI.WebControls.Label lb_Month;
		protected System.Web.UI.WebControls.Label lb_Day;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList dl_WCName;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.TextBox tb_Content;
		protected System.Data.SqlClient.SqlDataAdapter sqldaProCalendar;
		protected System.Data.SqlClient.SqlConnection conn;
		protected KIT_ERP.BasisInformation.dsCalendar dsCalendar1;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlCommand sqlInsertCommand1;
		protected System.Data.SqlClient.SqlCommand sqlUpdateCommand1;
		protected System.Data.SqlClient.SqlCommand sqlDeleteCommand1;
		protected System.Web.UI.WebControls.Label lb_Index;
		protected System.Web.UI.WebControls.Button bt_Select;
		protected System.Web.UI.WebControls.RadioButton RadioButton1;
		protected System.Web.UI.WebControls.RadioButton RadioButton2;
		protected System.Web.UI.WebControls.RadioButton RadioButton3;
		protected System.Web.UI.WebControls.RadioButton RadioButton4;
		protected System.Web.UI.WebControls.RadioButton RadioButton5;
		protected System.Web.UI.WebControls.RadioButton RadioButton6;
		protected System.Web.UI.WebControls.TextBox tb_Time;
		protected System.Web.UI.WebControls.Label lb_Year;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			conn.ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["DSN"] ;

			
			if(!Page.IsPostBack)
			{

				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > WC별생산달력정보';</script>");


				bt_Update.Attributes.Add("onClick", "return OK('수정');");


				tb_Time.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tb_Time.Attributes.Add("OnFocus", "OnFocus_Obj(this);");


				//	*************************************************
				//	**  WC명 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				//WC정보테이블에서에서 WC명과 WC정보번호를 가지고옴
				string str_WC = "Select WCName, WCInfoIndex from WCI_MT where RecodingState = 1";
				SqlCommand comm_WC = new SqlCommand(str_WC,conn);
				SqlDataAdapter da_WC = new SqlDataAdapter(comm_WC) ;
				DataSet ds_WC = new DataSet() ;
				da_WC.Fill(ds_WC);
				
				dl_WCName.DataSource = ds_WC;
				dl_WCName.DataTextField = ds_WC.Tables[0].Columns[0].ToString();
				dl_WCName.DataValueField = ds_WC.Tables[0].Columns[0].ToString();
				dl_WCName.DataBind();
				dl_WCName.Items.Insert(0, "-선택-") ;
				dl_WCName.Items[0].Value = "";

				sqldaProCalendar.SelectCommand.Parameters["@name"].Value = dl_WCName.SelectedItem.Value;

				dl_Year.SelectedIndex = int.Parse(DateTime.Now.Year.ToString())-2015;
				dl_Month.SelectedIndex = int.Parse(DateTime.Now.Month.ToString())-1;

				sqldaProCalendar.Fill(dsCalendar1);

				
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
			this.sqldaProCalendar = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlDeleteCommand1 = new System.Data.SqlClient.SqlCommand();
			this.conn = new System.Data.SqlClient.SqlConnection();
			this.sqlInsertCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlUpdateCommand1 = new System.Data.SqlClient.SqlCommand();
			this.dsCalendar1 = new KIT_ERP.BasisInformation.dsCalendar();
			((System.ComponentModel.ISupportInitialize)(this.dsCalendar1)).BeginInit();
			this.dl_WCName.SelectedIndexChanged += new System.EventHandler(this.dl_WCName_SelectedIndexChanged);
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
			// conn
			// 
			this.conn.ConnectionString = "workstation id=\"EZSYS-ERP01\";packet size=4096;user id=sa;data source=\"220.66.115.9\";persist " +
				"security info=True;initial catalog=Sindong_ERP;password=\"ezsys#0509\"";
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
				"tionCalendarInfoIndex FROM PCI_MT WHERE (RecodingState = 1) AND (WCName = @name)" +
				"";
			this.sqlSelectCommand1.Connection = this.conn;
			this.sqlSelectCommand1.Parameters.Add(new System.Data.SqlClient.SqlParameter("@name", System.Data.SqlDbType.VarChar, 20, "WCName"));
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
			// 
			// dsCalendar1
			// 
			this.dsCalendar1.DataSetName = "dsCalendar";
			this.dsCalendar1.Locale = new System.Globalization.CultureInfo("ko-KR");
			this.Load += new System.EventHandler(this.Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.dsCalendar1)).EndInit();

		}
		#endregion

		private void Calendar1_SelectionChanged(object sender, System.EventArgs e)
		{

			lb_Year.Text = Calendar1.SelectedDate.Year.ToString();
			lb_Month.Text = Calendar1.SelectedDate.Month.ToString();
			lb_Day.Text = Calendar1.SelectedDate.Day.ToString();

			// 생산달력 인덱스번호 찾기(수정시 사용)
			Find_Index();


			sqldaProCalendar.SelectCommand.Parameters["@name"].Value = dl_WCName.SelectedItem.Value;
			sqldaProCalendar.Fill(dsCalendar1);


			//데이타셋의 내용을 전부읽음
			foreach(DataRow dr in dsCalendar1.Tables[0].Rows)
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

		private void Calendar1_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
		{

			CalendarDay day = e.Day;
			TableCell cell = e.Cell;

			
			//데이타셋의 내용을 전부 읽음
			foreach(DataRow dr in dsCalendar1.Tables[0].Rows)
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

			


			sqldaProCalendar.SelectCommand.Parameters["@name"].Value = dl_WCName.SelectedItem.Value;
			sqldaProCalendar.Fill(dsCalendar1);
			
		}

        /// <summary>
        /// 작업장선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Button1_Click(object sender, System.EventArgs e)
		{

			sqldaProCalendar.SelectCommand.Parameters["@name"].Value = dl_WCName.SelectedItem.Value;
			sqldaProCalendar.Fill(dsCalendar1);

		}

		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			if(lb_Year.Text == "")
			{
				RegisterStartupScript("","<script>alert('날짜를 선택해 주세요');</script>");
			}
			else if(dl_WCName.SelectedItem.Value == "")
			{
				RegisterStartupScript("","<script>alert('WC를 선택해 주세요');</script>");
			}
			else
			{

				ArrayList arr = new ArrayList();

				arr.Add(dl_WCName.SelectedItem.Text);
				arr.Add(int.Parse(lb_Year.Text));
				arr.Add(int.Parse(lb_Month.Text));
				arr.Add(int.Parse(lb_Day.Text));
				arr.Add(Decimal.Parse(tb_Time.Text));
				arr.Add(tb_Content.Text);
				

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD("WCProductionCalendarInfo","Registration","PCI_MT",(int)PageName.WCProductionCalendarInfo,Session["ID"].ToString(),arr,"낱개입력");
				rud.ActivateRUD();

				


				string nextday = Calendar1.SelectedDate.Date.AddDays(1).ToShortDateString();
					
				//다음날을 달력컨트롤에 선택날짜로 만든후 레이블에 월과 일을 넣어줌.
				Calendar1.SelectedDate = DateTime.Parse(nextday.ToString());
				
				lb_Year.Text = 	Calendar1.SelectedDate.Year.ToString();				
				lb_Month.Text = Calendar1.SelectedDate.Month.ToString();
				lb_Day.Text =Calendar1.SelectedDate.Day.ToString();
			}

			sqldaProCalendar.SelectCommand.Parameters["@name"].Value = dl_WCName.SelectedItem.Value;
			sqldaProCalendar.Fill(dsCalendar1);
			
		}

		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(lb_Year.Text == "")
			{
				RegisterStartupScript("","<script>alert('날짜를 선택해 주세요');</script>");
			}
			else if(dl_WCName.SelectedItem.Value == "")
			{
				RegisterStartupScript("","<script>alert('WC를 선택해 주세요');</script>");
			}
			else
			{

				ArrayList arr = new ArrayList();

				arr.Add(dl_WCName.SelectedItem.Text);
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

			sqldaProCalendar.SelectCommand.Parameters["@name"].Value = dl_WCName.SelectedItem.Value;
			sqldaProCalendar.Fill(dsCalendar1);
		}


		private void Find_Index()
		{
			// 생산달력 인덱스번호 찾기(수정시 사용)
			conn.Open();
			string str = "Select * From PCI_MT Where WCName = @name and WC_Year = @year and WC_Month = @month and WC_Day = @day";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Parameters.Add("@name", SqlDbType.VarChar).Value = dl_WCName.SelectedItem.Value;
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

		private void bt_Select_Click(object sender, System.EventArgs e)
		{
			if(dl_WCName.SelectedIndex == 0)
			{
				RegisterStartupScript("","<script>alert('작업장을 선택해 주세요');</script>");
			}
			else
			{
				sqldaProCalendar.SelectCommand.Parameters["@name"].Value = dl_WCName.SelectedItem.Value;
				sqldaProCalendar.Fill(dsCalendar1);

				string date = dl_Month.SelectedItem.Value + "/01/"+ dl_Year.SelectedItem.Value;
				Calendar1.SelectedDate = DateTime.Parse(date);
				Calendar1.VisibleDate = DateTime.Parse(date);
			}
		}

		private void dl_WCName_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
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
	}
}
