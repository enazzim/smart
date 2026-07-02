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

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// WorkDailyReportRegistrationPC에 대한 요약 설명입니다.
	/// </summary>
	public class WorkDailyReportRegistrationPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label6;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Button Button3;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_FromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ToDate;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_PreWorkCompletionQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_PreRemainQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_PreSuitabilityQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_PreUnSuitabilityQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_UseTool1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_UseTool2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_UseTool3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_UseJig1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_UseJig2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_UseJig3;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_WorkerID;
		protected System.Web.UI.HtmlControls.HtmlInputHidden BeginTime;
		protected System.Web.UI.HtmlControls.HtmlInputHidden EndTime;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.DropDownList ddlWorker;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_WCName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMon;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		
		/// <summary>
		/// 비작업시간
		/// </summary>
		protected DataSet NonTime
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Non = source.NonTime();			
				return ds_Non;
			}
		}


		/// <summary>
		/// 사용공구
		/// </summary>
		protected DataSet Tool
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Tool = source.Tool();			
				return ds_Tool;
			}
		}

		/// <summary>
		/// 사용치구
		/// </summary>
		protected DataSet Jig
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Jig = source.Jig();
				return ds_Jig;
			}
		}

		/// <summary>
		/// 부적합원인
		/// </summary>
		protected DataSet UnSuitabilityCause
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Cause = source.UnSuitabilityCause();			
				return ds_Cause;
			}
		}

		/// <summary>
		/// 부적합현상
		/// </summary>
		protected DataSet UnSuitabilityStatus
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Status = source.UnSuitabilityStatus();
				return ds_Status;
			}
		}

		/// <summary>
		/// 검사판정
		/// </summary>
		protected DataSet InspectionDecision
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Decision = source.InspectionDecision();
				return ds_Decision;
			}
		}

		protected DataSet WCName
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_WCName = source.WCName();
				return ds_WCName;
				
			}
		}

		protected DataSet Worker
		{
			get
			{
				Update source = new Update(Session["ID"].ToString());
				DataSet ds_Worker = source.Worker();
				return ds_Worker;
			}
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 생산관리 > 작업일보 현황';</script>");
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
			this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	

		private void page_Load()
		{


			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			//	*************************************************
			//	**  WC명 드롭다운리스트... 데이타바인딩... **
			//	*************************************************
			//WC정보테이블에서에서 WC명과 WC정보번호를 가지고옴
			string str_WC = "Select WCName, WCInfoIndex from WCI_MT where RecodingState = 1 order by WCName";
			SqlCommand comm_WC = new SqlCommand(str_WC,conn);
			SqlDataAdapter da_WC = new SqlDataAdapter(comm_WC) ;
			DataSet ds_WC = new DataSet() ;
			da_WC.Fill(ds_WC);
				
			wc_WCName.DataSource = ds_WC;
			wc_WCName.DataTextField = ds_WC.Tables[0].Columns[0].ToString();
			wc_WCName.DataValueField = ds_WC.Tables[0].Columns[0].ToString();
			wc_WCName.DataBind();

			string str = "Select ID, Name From UI_MT where RecodingState = 1 and BusinessRegistrationNum = '' order by Name";
			SqlCommand comm_Id = new SqlCommand(str,conn);
			SqlDataAdapter da_Id = new SqlDataAdapter(comm_Id) ;
			DataSet ds_Id = new DataSet() ;
			da_Id.Fill(ds_Id);
				
			ddlWorker.DataSource = ds_Id;
			ddlWorker.DataTextField = ds_Id.Tables[0].Columns[1].ToString();
			ddlWorker.DataValueField = ds_Id.Tables[0].Columns[0].ToString();
			ddlWorker.DataBind();
			ddlWorker.Items.Insert(0, "-선택-") ;
			ddlWorker.Items[0].Value = "";

			conn.Close();


		}

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		}

		private DataSet search()
		{
			Search search = new Search("WorkDailyReportRegistrationPC",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName,wc_WCName,wdc_FromDate,wdc_ToDate, ddlWorker);
			return search.DataSet_search();
			
		}

		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('저장할 항목이 없습니다!');");
				Response.Write("</script>");
			}
			else
			{
				Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg_PR = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
				uwg_PR = UltraWebGrid1;
				uwg_PR.DisplayLayout.Pager.AllowPaging = false;
				uwg_PR.Columns.FromKey("chk").Hidden = true;
				uwg_PR.DataSource = search();
				uwg_PR.DataBind();
				UltraWebGridExcelExporter1.Export(uwg_PR);

			}
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			ItemSearchControl1.ClearTextBox();
			wdc_FromDate.Value = "";
			wdc_ToDate.Value = "";
			wc_WCName.DataValue = "";
			ddlWorker.SelectedIndex = 0;
			//dl_ProcessCode.SelectedIndex = 0;
			
		}

		/// <summary>
		/// 수정버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			ArrayList arr = new ArrayList();
			
			arr.Add(decimal.Parse(lb_PreWorkCompletionQuantity.Value));//이전금번작업완료수량
			arr.Add(decimal.Parse(lb_PreRemainQuantity.Value));//이전잔량
			arr.Add(decimal.Parse(lb_PreSuitabilityQuantity.Value));//이전적합수량
			arr.Add(decimal.Parse(lb_PreUnSuitabilityQuantity.Value));//이전부적합수량
			
			arr.Add(lb_UseTool1.Value);//이전사용공구1
			arr.Add(lb_UseJig1.Value);//이전사용치구1
			arr.Add(lb_UseTool2.Value);//이전사용공구2
			arr.Add(lb_UseJig2.Value);//이전사용치구2
			arr.Add(lb_UseTool3.Value);//이전사용공구3
			arr.Add(lb_UseJig3.Value);//이전사용치구3
			arr.Add(BeginTime.Value);//변경된 작업시작시간
			arr.Add(EndTime.Value);//변경된 작업종료시간
			arr.Add(hdYear.Value);
			arr.Add(hdMon.Value);
			
			arr.Add(int.Parse(lb_RowSelectIndex.Value));//선택된 그리드 인덱스

			Update up = new Update("WorkDailyReportRegistrationPC",arr,UltraWebGrid1,Session["ID"].ToString());
			up.MainTableUpdate();
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		}

		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			string State = "";//품질검사원장 진행상태
			string State1 = "";//영업창고입고의뢰원장 진행상태

			for(int i = 0; i < UltraWebGrid1.Rows.Count; i++)
			{
				// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
				if(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value == null)
				{
					UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value = false;
				}
			
				if(bool.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value.ToString()))
				{
					if(Division(UltraWebGrid1.Rows[i].Cells.FromKey("ItemNum").Text,
						int.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("ProcessSequenceNum").Text)))
					{
						//품질검사원장에 등록된 레코드의 진행상태가 대기인경우에만 삭제가능
						SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
						conn.Open();
					
						string str = "Select * From QI_HT Where HistoryIndex1 = @index and HistorySection1 = '작업일보'";
						SqlCommand comm = new SqlCommand(str,conn);
						comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
						SqlDataReader dr = comm.ExecuteReader();
						while(dr.Read())
						{
							State = dr["ProgressCondition"].ToString();
						}
						dr.Close();
						conn.Close();
						conn.Open();
						string str1 = "Select * From ISR_HT Where HistoryIndex = @index and HistorySection = '작업일보'";
						SqlCommand comm1 = new SqlCommand(str1,conn);
						//comm1.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
						comm1.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(UltraWebGrid1.Rows[i].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
						SqlDataReader dr1 = comm1.ExecuteReader();
						while(dr1.Read())
						{
							State1 = dr1["ProgressCondition"].ToString();
						}
						dr1.Read();
						conn.Close();
					
					}
				}
			}
			
			

			if(State != "입고완료" || State == "" || State1 == "대기" || State1 == "")
			{
				Delete del = new Delete(UltraWebGrid1,"WorkDailyReportRegistrationPC");
				del.MainRowDelete();
				UltraWebGrid1.DataSource = search();
				UltraWebGrid1.DataBind();
			}
			else
			{
				RegisterStartupScript("","<script>alert('삭제할 수 없는 항목입니다!');</script>");
			}
			// 등록을 하고난뒤에는 체크박스 상자를 false로 바꾸어준다
			for(int i = 0; i < UltraWebGrid1.Rows.Count; i++)
			{
				UltraWebGrid1.Rows[i].Cells[0].Value = false;
			}
			
		}

		private bool Division(string Item, int Seq)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string PropertyClassification = "";
			int Sequence = 0;
			string str = "Select * From II_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",Item);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				PropertyClassification = dr["PropertyClassification"].ToString();
			}
			dr.Close();

			str = @"Select Max(ProcessSequenceNum) as Process From PSI_MT where RecodingState = 1 and ItemNum = @ItemNum";
			comm.CommandText = str;
			SqlDataReader dr1 = comm.ExecuteReader();
			while(dr1.Read())
			{
				Sequence = int.Parse(dr1["Process"].ToString());
			}
			conn.Close();

			if((PropertyClassification == "제품" || PropertyClassification == "팬텀") && Sequence == Seq)
				return true;
			else
				return false;
		}

		
	}
}
