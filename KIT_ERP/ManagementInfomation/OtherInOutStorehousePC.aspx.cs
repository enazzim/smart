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
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.ManagementInfomation
{
	/// <summary>
	/// OtherInOutStorehousePC에 대한 요약 설명입니다.
	/// </summary>
	public class OtherInOutStorehousePC : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected Infragistics.WebUI.WebCombo.WebCombo WebCombo1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Button Button4;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_FromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdc_ToDate;
		protected System.Web.UI.WebControls.DropDownList dl_Store;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button bt_Excel;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Update;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Quantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Store;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_StoreNum;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_InOut;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Reason;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_StoreName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Distinction;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReasonCode;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected Infragistics.WebUI.WebCombo.WebCombo wc_ItemName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdMon;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdYear;

		protected DataSet ds;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.RawMaterials = true; //원자재 바인딩
			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true; //반제품 바인딩
			ItemSearchControl1.Products = true; //제품 바인딩	
			

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 기타입출고현황';</script>");


				bt_Delete.Attributes.Add("onclick","return OK('선택항목을 삭제');");
				bt_Clear.Attributes.Add("onClick", "ResetTextBox()");
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

		/// <summary>
		/// 검색버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();		
		}



		/// <summary>
		/// 검색객체생성
		/// </summary>
		/// <returns></returns>
		private DataSet search()
		{
			Search search;
			if(ItemSearchControl1.hdItem.Trim() == "")
				search = new Search(wdc_FromDate,wdc_ToDate, "OtherInOutStorehousePC",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName, dl_Store.SelectedItem.Value);
			else
				search = new Search(wdc_FromDate,wdc_ToDate, "OtherInOutStorehousePC",ItemSearchControl1.ItemNum, "", "", dl_Store.SelectedItem.Value);
			return search.DataSet_search();
		}

		/// <summary>
		/// 페이지 이동시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// Excel버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Excel_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg_OS = new UltraWebGrid();
			uwg_OS = UltraWebGrid1;
			uwg_OS.DisplayLayout.Pager.AllowPaging = false;
			uwg_OS.Columns.FromKey("chk").Hidden = true;
			uwg_OS.DataSource = search();
			uwg_OS.DataBind();
			UltraWebGridExcelExporter1.Export(uwg_OS);
		}

		/// <summary>
		/// 삭제버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Delete_Click(object sender, System.EventArgs e)
		{
			Delete del = new Delete(UltraWebGrid1,"OtherInOutStorehousePC");
			del.MainRowDelete();
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 초기화버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Clear_Click(object sender, System.EventArgs e)
		{			
			dl_Store.SelectedIndex = 0;
			wdc_FromDate.Value = "";
			wdc_ToDate.Value = "";
		}

		
		/// <summary>
		/// 수정버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			int index = int.Parse(lb_Index.Value.Trim());
			string store = UltraWebGrid1.Rows[index].Cells.FromKey("StorehouseName").Text;
			string Business = UltraWebGrid1.Rows[index].Cells.FromKey("BusinessRegistrationNum").Text;
			string Item = UltraWebGrid1.Rows[index].Cells.FromKey("ItemNum").Text;
			int sequence = int.Parse(UltraWebGrid1.Rows[index].Cells.FromKey("ProcessSequenceNum").Text);
			if(Exist(Item,sequence,store,Business))
			{
				ArrayList arr = new ArrayList();
				//기존수량
				arr.Add(decimal.Parse(lb_Quantity.Value.Trim()));
				//기존창고명
				arr.Add(lb_Store.Value);
				//기존창고번호
				if(lb_StoreNum.Value == null || lb_StoreNum.Value.Trim() =="")
					arr.Add(null);
				else
					arr.Add(int.Parse(lb_StoreNum.Value.Trim()));
				//기존 입출고도면
				arr.Add(int.Parse(lb_InOut.Value));

				arr.Add(lb_Distinction.Value.Trim());//변경된 입출고 도면
				arr.Add(lb_ReasonCode.Value.Trim());//변경된 입출고 사유코드
				arr.Add(lb_Reason.Value.Trim());//변경된 입출고 사유
				arr.Add(int.Parse(lb_StoreName.Value.Trim()));//변경된 창고번호
				arr.Add(hdYear.Value);
				arr.Add(hdMon.Value);

				//그리드 선택 인덱스
				arr.Add(int.Parse(lb_Index.Value.Trim()));
			
				Update up = new Update("OtherInOutStorehousePC",arr,UltraWebGrid1,Session["ID"].ToString());
				up.MainTableUpdate();
				
			}
			else
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('해당품목이 창고에 존재하지 않습니다!');");
				Response.Write("</script>");
			}

			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();
		
		}

		private bool Exist(string Item, int seq, string storehouse,string Num)
		{
			string str = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection=conn;
			switch(storehouse)
			{
				case "원자재창고":
					str = "Select count(*) From RMS_MT Where ItemNum=@ItemNum and RecodingState = 1";
					comm.Parameters.Add("@ItemNum",Item);
					break;
				case "생산창고":
					str = "Select count(*) From PS_MT Where ItemNum=@ItemNum and ProcessSequenceNum = @Process and RecodingState = 1";
					comm.Parameters.Add("@ItemNum",Item);
					comm.Parameters.Add("@Process",seq);
					break;
				case "외주창고":
					str = "Select count(*) From OS_MT Where ItemNum=@ItemNum and ProcessSequenceNum = @Process and BusinessRegistrationNum = @Num and RecodingState = 1";
					comm.Parameters.Add("@ItemNum",Item);
					comm.Parameters.Add("@Process",seq);
					comm.Parameters.Add("@Process",Num);
					break;
				case "납품창고":
					str = "Select count(*) From DS_MT Where ItemNum=@ItemNum and RecodingState = 1";
					comm.Parameters.Add("@ItemNum",Item);
					break;
				default :
					str = "Select count(*) From BS_MT Where ItemNum=@ItemNum and RecodingState = 1";
					comm.Parameters.Add("@ItemNum",Item);
					break;

			}
			comm.CommandText=str;
			if(int.Parse(comm.ExecuteScalar().ToString()) == 0)
			{
				conn.Close();
				return false;
			}
			else
			{
				conn.Close();
				return true;
			}
			
			

		}


		/// <summary>
		/// 편집모드에서 입출고사유 바인딩시키는 데이터셋을 리턴
		/// </summary>
		/// <returns></returns>
		protected DataSet Store
		{
			get
			{
				Update reason = new Update(Session["ID"].ToString());
				DataSet ds_Reson = reason.Store();
				
				return ds_Reson;
			}
		}
	}
}

