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
using Infragistics.WebUI;
using Infragistics.WebUI.WebSchedule;
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;


namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// ReceiveingPC에 대한 요약 설명입니다.
	/// </summary>
	public class ReceiveingPC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.DropDownList dl_ProgressState;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcEndDate;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button btnNow;
		protected System.Web.UI.WebControls.Button btnPre;
		protected System.Web.UI.WebControls.Button btnNext;
		protected System.Web.UI.WebControls.Button bt_Cancel;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Stop;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Label Label2;		
		protected DataSet ds;		
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected System.Web.UI.HtmlControls.HtmlInputHidden tb_TotalCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Total;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Volum;	
		protected static int Division;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;	//검색버튼이 눌러졌는지 비디오버튼이 눌러졌는지 파악하는 변수
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	


	




		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.Phantom = true;//펜텀 바인딩				
			CSC1.UnitCostDistinction = "수주거래처";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 수주등록현황';</script>");

				bt_Cancel.Attributes.Add("onClick", "return OK('취소 ');");
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제 ');");
				bt_Stop.Attributes.Add("onClick", "return OK('선택한 항목을 중단 ');");

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


				///////////////////////////////////////
				// 비디오버튼을 위해
				// lb_vol에 볼륨번호 최고값을 넣어둔다.
				//////////////////////////////////////
				
				this.Volnum();
				
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
			this.lnk_Update.Click += new System.EventHandler(this.LinkButton1_Click);
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
			this.btnNow.Click += new System.EventHandler(this.btnNow_Click);
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
			this.bt_Delete.Click += new System.EventHandler(this.bt_Delete_Click);
			this.bt_Stop.Click += new System.EventHandler(this.bt_Stop_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	
		#endregion

		

		/// <summary>
		/// 검색버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
            UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
			bt_Cancel.Enabled = false;
			Division = 0;//검색버튼이 눌러지면 0  비디오버튼이 눌러지면 1
		}


		/// <summary>
		/// 삭제버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			Delete del = new Delete(UltraWebGrid1,"ReceiveingPC");
			del.MainRowDelete();
	
			if(Division == 0)
			{
				bt_Cancel.Enabled = false;
				ds = Search();
				UltraWebGrid1.DataSource = ds;
				UltraWebGrid1.DataBind();				
			}
			else
			{
				bt_Cancel.Enabled = true;
				VideoButton vol = new VideoButton(UltraWebGrid1,"ReceiveingPC",Volum.Value);
				ds = vol.FindVolum();
				UltraWebGrid1.DataSource = ds;
				UltraWebGrid1.DataBind();					
			}

		}

		

		/// <summary>
		/// 중단버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Stop_Click(object sender, System.EventArgs e)
		{
			Stop stop = new Stop(UltraWebGrid1,"ReceiveingPC",Session["ID"].ToString());
			stop.MainTableStop();

			if(Division == 0)
			{
				bt_Cancel.Enabled = false;
				ds = Search();
				UltraWebGrid1.DataSource = ds;
				UltraWebGrid1.DataBind();				
			}
			else
			{
				bt_Cancel.Enabled = true;
				VideoButton vol = new VideoButton(UltraWebGrid1,"ReceiveingPC",Volum.Value);
				ds = vol.FindVolum();
				UltraWebGrid1.DataSource = ds;
				UltraWebGrid1.DataBind();					
			}
		}

		/// <summary>
		/// 취소버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Cancel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = false;
			VideoButton aa = new VideoButton(UltraWebGrid1,"ReceiveingPC",Volum.Value);
			UltraWebGrid1.DataSource = aa.FindVolum();
			UltraWebGrid1.DataBind();
			Cancel cancel = new Cancel(UltraWebGrid1,"ReceiveingPC",Volum.Value);
			cancel.MainRowCancel();
            Volnum();
			UltraWebGrid1.DisplayLayout.Pager.AllowPaging = true;
			VideoButton bb = new VideoButton(UltraWebGrid1,"ReceiveingPC",Volum.Value);
			UltraWebGrid1.DataSource = bb.FindVolum();
			UltraWebGrid1.DataBind();
			
		}

		/// <summary>
		/// 검색 함수
		/// </summary>
		private DataSet Search()
		{

			Search aa;
			if(ItemSearchControl1.hdItem.Trim() == "")
				aa = new Search("ReceiveingPC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum,dl_ProgressState.SelectedItem.Value,ddlItemClassification1.SelectedItem.Value);
			else
				aa = new Search("ReceiveingPC",ItemSearchControl1.ItemNum,"","",wdcStartDate,wdcEndDate,CSC1.Company, CSC1.BusinessRegistrationNum,dl_ProgressState.SelectedItem.Value,ddlItemClassification1.SelectedItem.Value);
			return  aa.DataSet_search();
			
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
			string str_Vol = "Select Max(VolumNum) From RO_HT";
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
			bt_Cancel.Enabled = true;
			vol_Pre();

			VideoButton vol = new VideoButton(UltraWebGrid1,"ReceiveingPC",Volum.Value);
			ds = vol.FindVolum();
			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();
			Division = 1;
		}


		/// <summary>
		/// 가운데 비디오버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNow_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.Columns[0].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			bt_Cancel.Enabled = true;
			vol_Now();
			VideoButton vol = new VideoButton(UltraWebGrid1,"ReceiveingPC",Volum.Value);
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
			bt_Cancel.Enabled = true;
			vol_Next();
			
			VideoButton vol = new VideoButton(UltraWebGrid1,"ReceiveingPC",Volum.Value);
			ds = vol.FindVolum();
			UltraWebGrid1.DataSource = ds;
			UltraWebGrid1.DataBind();
			Division = 1;
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
			comm.CommandText = "Select Max(VolumNum) as VolumNum From RO_HT where VolumNum < @num";
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
			comm.CommandText = "Select Max(VolumNum) as VolumNum From RO_HT";
							
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
				Volum.Value = dr["VolumNum"].ToString();
			dr.Close();
			
			if(Volum.Value == "")
			{
				RegisterStartupScript("","<script>alert('볼륨번호가 없습니다!')</script>");
				Volum.Value = "0";
				bt_Cancel.Enabled = false;
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
			comm.CommandText = "Select Min(VolumNum) as VolumNum From RO_HT where VolumNum > @num";
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
		/// 엑셀저장
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Button3_Click(object sender, System.EventArgs e)
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
				uwg_RO.Columns.FromKey("chk").Hidden = true;
				if(Division == 0)
				{
					uwg_RO.DataSource = Search();
				}
				else
				{
					VideoButton vol = new VideoButton(UltraWebGrid1,"ReceiveingPC",Volum.Value);
					uwg_RO.DataSource = vol.FindVolum();
				}

				uwg_RO.DataBind();
				UltraWebGridExcelExporter1.Export(uwg_RO);

			}
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
				VideoButton vol = new VideoButton(UltraWebGrid1,"ReceiveingPC",Volum.Value);
				UltraWebGrid1.DataSource = vol.FindVolum();
			}
			UltraWebGrid1.DataBind();
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			//총수량
			decimal total = decimal.Parse(lb_Total.Value);

			//총금액(총수량*적용단가)
			decimal totalcost = total * decimal.Parse(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("ApplyUnitCost").Value.ToString()) ;

			ArrayList arr = new ArrayList();
			arr.Add(total); // 총수량
			arr.Add(totalcost);//총금액
			arr.Add(total);//잔량
			arr.Add(int.Parse(lb_RowSelectIndex.Value));//선택된 그리드 인덱스

			Update up = new Update("ReceiveingPC",arr,UltraWebGrid1,Session["ID"].ToString());
			up.MainTableUpdate();

//			if(ReceiveingValidate())
//			{
//				Update up = new Update("ReceiveingPC",arr,UltraWebGrid1,Session["ID"].ToString());
//				up.MainTableUpdate();
//			}
//			else
//			{
//				RegisterStartupScript("","<script>alert('동일한 발주번호가 존재합니다!');</script>");
//			}

			if(Division == 0)
			{
				UltraWebGrid1.DataSource = Search();
			}
			else
			{
				VideoButton vol = new VideoButton(UltraWebGrid1,"ReceiveingPC",Volum.Value);
				UltraWebGrid1.DataSource = vol.FindVolum();
			}
			UltraWebGrid1.DataBind();

		}

		private bool ReceiveingValidate()
		{
			int Exist = 0;
			if(UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OrderNum").Text.Trim() != "")
			{
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
				string str = @"Select count(OrderNum) From RO_HT where OrderNum = @OrderNum and ReceivingOrderHistoryIndex != @Index";
				SqlCommand comm = new SqlCommand(str,conn);
				//발주번호가 동일한 것이 있는지 파악
				comm.Parameters.Add("@OrderNum", UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("OrderNum").Text.Trim());
				comm.Parameters.Add("@Index",UltraWebGrid1.Rows[int.Parse(lb_RowSelectIndex.Value)].Cells.FromKey("ReceivingOrderHistoryIndex").Text.Trim());
			
				conn.Open();
				Exist = int.Parse(comm.ExecuteScalar().ToString());
				comm.Parameters.Clear();
				conn.Close();
			}
			if(Exist == 0)
				return true;
			else
				return false;
			
		}


		
	}
}
