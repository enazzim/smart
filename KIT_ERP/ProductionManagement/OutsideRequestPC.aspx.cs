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
	/// OutsideRequestPC에 대한 요약 설명입니다.
	/// </summary>
	public class OutsideRequestPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label searchTitle;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_FirstDeliveryDemandQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_FifthDeliveryDemandQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ThirdDeliveryDemandQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_FourthDeliveryDemandQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_SecondDeliveryDemandQuantity;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		
		protected static int Division;	//검색버튼이 눌러졌는지 비디오버튼이 눌러졌는지 파악하는 변수
		protected DataSet ds;
		protected System.Web.UI.WebControls.Button bt_Stop;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Volum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Total;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button bt_Delete;		
		//protected static string  Volum;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;

		private void Page_Load(object sender, System.EventArgs e)
		{

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.Commodity = true;
			CSC1.UnitCostDistinction="외주거래처";

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 생산관리 > 외주의뢰 현황';</script>");
				btnCancel.Attributes.Add("onClick", "return confirm('취소 하시겠습니까? ');");
				bt_Stop.Attributes.Add("onClick", "return confirm('선택한 항목을 중단 하시겠습니까?');");
				
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
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
			this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			this.bt_Excel.Click += new System.EventHandler(this.bt_Excel_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.bt_Stop.Click += new System.EventHandler(this.bt_Stop_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void page_Load()
		{

			///////////////////////////////////////
			// 비디오버튼을 위해
			// lb_vol에 볼륨번호 최고값을 넣어둔다.
			//////////////////////////////////////
			///
			//	*************************************************
			//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
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

				
			this.Volnum();
		}


		/// <summary>
		/// 검색버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
			bt_Delete.Enabled = true;
			btnCancel.Enabled = false;
			Division = 0;//검색버튼이 눌러지면 0  비디오버튼이 눌러지면 1
		}

		/// <summary>
		/// 검색함수
		/// </summary>
		/// <returns></returns>
		private DataSet Search()
		{
			//Search search = new Search("OutsideRequestPC",ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName,wdcFromDate, ddlItemClassification1.SelectedItem.Value.Trim(),wdcToDate,CSC1.Company, CSC1.BusinessRegistrationNum);
		
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			string str = @"Select CI_MT.CompanyName,
						OOR_HT.ItemNum, OOR_HT.ItemDrawNum, OOR_HT.ItemName, OOR_HT.UnitCostDistinction, OOR_HT.BeginProcessCode, OOR_HT.BeginProcess, OOR_HT.EndProcessCode, OOR_HT.EndProcess, OOR_HT.ApplyUnitCost, OOR_HT.FirstDeliveryDemandQuantity, OOR_HT.FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, OOR_HT.OrderQuantity, OOR_HT.TotalCost, OOR_HT.VolumNum, OOR_HT.ProgressCondition, OOR_HT.RegistrationPerson, OOR_HT.RegistrationPersonID, OOR_HT.RegistrationDate, OOR_HT.UpdatingPerson, OOR_HT.UpdatingPersonID, OOR_HT.UpdatingDate, OOR_HT.ProductionPlanHistoryIndex, OOR_HT.WCDailyWorkPlanHistoryIndex, OOR_HT.OutSideOrderRequestHistoryIndex
				From OOR_HT
				inner join II_MT on OOR_HT.ItemNum = II_MT.ItemNum
				inner join UCI_MT on OOR_HT.ItemNum = UCI_MT.ItemNum and OOR_HT.BeginProcessCode = UCI_MT.BeginProcessCode and OOR_HT.EndProcessCode = UCI_MT.EndProcessCode
				inner join CI_MT on UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
				WHERE II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and UCI_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '외주단가' 
				and UCI_MT.OrderRate = (Select Max(OrderRate) From UCI_MT where ItemNum like @ItemNum and UnitCostDistinction = '외주단가')				
				and OOR_HT.ItemNum like @ItemNum and OOR_HT.ItemDrawNum like @ItemDrawNum and OOR_HT.ItemName like @ItemName
				and CI_MT.CompanyName Like @CompanyName
				and CI_MT.BusinessRegistrationNum Like @BusinessRegistrationNum
				and ItemClassification1 Like @ItemClassification
				and OOR_HT.RegistrationDate >= @begin and OOR_HT.RegistrationDate <= @end
				order by OutSideOrderRequestHistoryIndex desc";

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum", "%"+ItemSearchControl1.ItemNum+"%");
			comm.Parameters.Add("@ItemDrawNum", "%"+ItemSearchControl1.ItemDrawNum+"%");
			comm.Parameters.Add("@ItemName", "%"+ItemSearchControl1.ItemName+"%");
			comm.Parameters.Add("@CompanyName", "%"+CSC1.Company+"%");
			comm.Parameters.Add("@BusinessRegistrationNum", "%"+CSC1.BusinessRegistrationNum+"%");
			comm.Parameters.Add("@ItemClassification", "%"+ddlItemClassification1.SelectedItem.Value+"%");
			if(wdcFromDate.Text.Trim() == "")
				comm.Parameters.Add("@begin", "1999-01-01");
			else
				comm.Parameters.Add("@begin", wdcFromDate.Text.Trim());

			if(wdcToDate.Text.Trim() == "")
				comm.Parameters.Add("@end", "2050-12-31");
			else
				comm.Parameters.Add("@end", wdcToDate.Text.Trim());
			
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet dsSearch = new DataSet();
			da.Fill(dsSearch);

			return dsSearch;
			//return search.DataSet_search();
		}

		/// <summary>
		/// 중단버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Stop_Click(object sender, System.EventArgs e)
		{
			Stop stop = new Stop(UltraWebGrid1,"OutsideRequestPC",Session["ID"].ToString());
			stop.MainTableStop();

			if(Division == 0)
			{
				btnCancel.Enabled = false;
				ds = Search();
				UltraWebGrid1.DataSource = ds;
				UltraWebGrid1.DataBind();				
			}
			else
			{
				btnCancel.Enabled = true;
				VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
				ds = vol.FindVolum();
				UltraWebGrid1.DataSource = ds;
				UltraWebGrid1.DataBind();					
			}
		}

		/// <summary>
		/// 취소버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Cancel cancel = new Cancel(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
			cancel.MainRowCancel();
            Volnum();
			VideoButton aa = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
			UltraWebGrid1.DataSource = aa.FindVolum();
			UltraWebGrid1.DataBind();
			
		}

		/// <summary>
		/// 볼륨번호 찾는 함수
		/// </summary>
		/// <returns></returns>
		private void Volnum()
		{
			///////////////////////////////////////
			// 비디오버튼을 위해
			// Volum에 볼륨번호 최고값을 넣어둔다.
			//////////////////////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str_Vol = "Select Max(VolumNum) From OOR_HT";
			SqlCommand comm_Vol = new SqlCommand(str_Vol,conn);
			if(!Convert.IsDBNull(comm_Vol.ExecuteScalar()))
				Volum.Value = Convert.ToString(comm_Vol.ExecuteScalar());
			else
				Volum.Value = "0";
			conn.Close();

			if(Volum.Value == "0")
			{
				btnNext.Enabled = false;
				btnPre.Enabled = false;
			}
		}

		/// <summary>
		/// 이전버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPre_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			btnCancel.Enabled = true;
			bt_Delete.Enabled = false;
			vol_Pre();

			VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
			ds = vol.FindVolum();
			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();
			Division = 1;
			btnNext.Enabled = true;
		}


		/// <summary>
		/// 가운데 비디오버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNow_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			btnCancel.Enabled = true;
			bt_Delete.Enabled = false;
			vol_Now();
			VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
			ds = vol.FindVolum();
			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();
			Volnum();
			Division = 1;
		}

		/// <summary>
		/// 다음버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNext_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			btnCancel.Enabled = true;
			bt_Delete.Enabled = false;
			vol_Next();
			
			VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
			ds = vol.FindVolum();
			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();
			Division = 1;
			btnPre.Enabled = true;
		}


		/// <summary>
		/// 이전볼퓸번호 찾는 함수
		/// </summary>
		private void vol_Pre()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "Select Max(VolumNum) as VolumNum From OOR_HT where VolumNum < @num";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = int.Parse(Volum.Value);
							
			if(comm.ExecuteScalar() != Convert.DBNull)
			{
				SqlDataReader dr = comm.ExecuteReader();
				if(dr.Read())
					Volum.Value = dr["VolumNum"].ToString();
			}
			else
			{
				RegisterStartupScript("","<script>alert('맨 처음입니다!')</script>");
				btnPre.Enabled = false;
				btnNext.Enabled = true;
			}
			
			conn.Close();	
			
			
		}

		/// <summary>
		/// 현재 볼륨번호 찾는 함수
		/// </summary>
		private void vol_Now()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "Select Max(VolumNum) as VolumNum From OOR_HT";
							
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
				Volum.Value = dr["VolumNum"].ToString();
			dr.Close();
			
			if(Volum.Value == "")
			{
				RegisterStartupScript("","<script>alert('볼륨번호가 없습니다!')</script>");
				Volum.Value = "0";
				btnCancel.Enabled = false;
				btnPre.Enabled = false;
				btnNext.Enabled = false;
			}
			else
			{			
				btnPre.Enabled = true;
				btnNext.Enabled = true;
			}
			conn.Close();
		}

		/// <summary>
		/// 이후볼륨번호 찾는 함수
		/// </summary>
		private void vol_Next()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "Select Min(VolumNum) as VolumNum From OOR_HT where VolumNum > @num";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = int.Parse(Volum.Value);
							
			if(comm.ExecuteScalar() != Convert.DBNull)
			{
				SqlDataReader dr = comm.ExecuteReader();
				if(dr.Read())
					Volum.Value = dr["VolumNum"].ToString();
			}
			else
			{
				RegisterStartupScript("","<script>alert('맨 끝입니다!');</script>");
				btnPre.Enabled = true;
				btnNext.Enabled = false;
			}
			
			conn.Close();	
			
		}

		/// <summary>
		/// 페이지 이동
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			if(Division == 0)
			{
				UltraWebGrid1.DataSource = Search();
			}
			else
			{
				VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
				UltraWebGrid1.DataSource = vol.FindVolum();
			}
			UltraWebGrid1.DataBind();
		}

		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			if(UltraWebGrid1.Rows.Count == 0)
			{
				RegisterStartupScript("","<script>alert('저장할 항목이 없습니다!');</script>");
			}
			else
			{
				Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg = new Infragistics.WebUI.UltraWebGrid.UltraWebGrid();
				uwg = UltraWebGrid1;
				uwg.DisplayLayout.Pager.AllowPaging = false;
				uwg.Columns.FromKey("chk").Hidden = true;
				if(Division == 0)
				{
					uwg.DataSource = Search();
				}
				else
				{
					VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
					uwg.DataSource = vol.FindVolum();
				}

				uwg.DataBind();
				UltraWebGridExcelExporter1.Export(uwg);

			}
		}

		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			
			ArrayList arr = new ArrayList();
			
			arr.Add(int.Parse(lb_RowSelectIndex.Value));//선택된 그리드 인덱스
			
			Update up = new Update("OutsideRequestPC",arr,UltraWebGrid1,Session["ID"].ToString());
			up.MainTableUpdate();
			if(Division == 0)
			{
				UltraWebGrid1.DataSource = Search();
			}
			else
			{
				VideoButton vol = new VideoButton(UltraWebGrid1,"OutsideRequestPC",Volum.Value);
				UltraWebGrid1.DataSource = vol.FindVolum();
			}
			UltraWebGrid1.DataBind();
		}
		
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			Delete del = new Delete(UltraWebGrid1,"OutsideRequestPC");
			del.MainRowDelete();

			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
			btnCancel.Enabled = false;
		}

		
	}
}
