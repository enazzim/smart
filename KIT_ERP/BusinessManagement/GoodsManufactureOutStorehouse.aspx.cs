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
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// GoodsManufactureOutStorehouse에 대한 요약 설명입니다.
	/// </summary>
	public class GoodsManufactureOutStorehouse : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.TextBox tb_ItemNum;
		protected System.Web.UI.WebControls.TextBox tb_ItemDrawNum;
		protected System.Web.UI.WebControls.TextBox tb_ItemName;
		protected System.Web.UI.WebControls.TextBox TextBox2;
		protected System.Web.UI.WebControls.TextBox TextBox4;
		protected System.Web.UI.WebControls.TextBox TextBox6;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.TextBox tb_RemainderQuantity;
		protected System.Web.UI.WebControls.TextBox tb_OutStoreQuantity;
		protected System.Web.UI.WebControls.DropDownList dl_Store;
		protected System.Web.UI.WebControls.Button bt_OutStore;
		protected System.Web.UI.WebControls.TextBox tb_UnInspectionQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden idx;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.WebControls.Button bt_Publication;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected System.Web.UI.WebControls.LinkButton lnk_Update;
		protected System.Web.UI.WebControls.Label hd_Num;
		protected System.Web.UI.WebControls.TextBox Textbox3;
		protected System.Web.UI.WebControls.Button btLot;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdLotNum;
		protected System.Web.UI.WebControls.TextBox tb_StoreQuantit;
		protected Infragistics.WebUI.WebDataInput.WebNumericEdit tb_StoreQuantity;
		protected System.Web.UI.WebControls.TextBox txtOrderNum;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.TextBox txtOrderNum1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.DropDownList ddlPropertyClassification;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true;//반제품 바인딩
			ItemSearchControl1.RawMaterials = true;//원자재 바인딩

			CSC1.UnitCostDistinction="수주거래처";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{

				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 상품/제품출고';</script>");

				//bt_OutStore.Attributes.Add("onClick","return confirm('출고하시겠습니까?');");

				tb_OutStoreQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// onkeyup 이벤트가 발생하면 수량을 계산하는 자바스크립트 함수 매핑
				tb_OutStoreQuantity.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");
				// focus를 얻으면 자동으로 select()
				tb_OutStoreQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_OutStoreQuantity.Attributes.Add("OnBlur", "OnBlur_Cur(this);");

				wdcStartDate.NullDateLabel = DateTime.Now.ToShortDateString();
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
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.dl_Store.SelectedIndexChanged += new System.EventHandler(this.dl_Store_SelectedIndexChanged);
			this.bt_OutStore.Click += new System.EventHandler(this.bt_OutStore_Click);
			this.btLot.Click += new System.EventHandler(this.btLot_Click);
			this.lnk_Update.Click += new System.EventHandler(this.lnk_Update_Click);
			this.bt_Publication.Click += new System.EventHandler(this.bt_Publication_Click);
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
			//상품제품출고
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			search();
		}

		private void bt_OutStore_Click(object sender, System.EventArgs e)
		{
			if(decimal.Parse(tb_OutStoreQuantity.Text) == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('출고수량을 입력하세요!');");
				Response.Write("</script>");
			}
//			else if(decimal.Parse(tb_StoreQuantity.Text) < decimal.Parse(tb_OutStoreQuantity.Text))
//			{
//				Response.Write("<script language=javascript>");
//				Response.Write("alert('출고수량이 재고수량보다 많습니다!');");
//				Response.Write("</script>");
//			}
			else
			{

				Textbox3.Text = hdLotNum.Value ;

				ArrayList list = new ArrayList();
		
				list.Add(decimal.Parse(tb_OutStoreQuantity.Text));//출고수량
				list.Add(dl_Store.SelectedItem.Value);//영업창고번호
				list.Add(wdcStartDate.Text);		//출고일자
				list.Add(Textbox3.Text);	//Lot번호
				list.Add(int.Parse(lb_RowSelectIndex.Value));//그리드 Row 인덱스

				//제품은 무조건 마이너스 재고 허용하고 상품은 상황에 따라 마이너스 재고 허용여부를 결정한다 (2010.01.04)
				Register rg = new Register(UltraWebGrid1, UltraWebGrid2,list, "GoodsManufactureOutStorehouse",Session["ID"].ToString());
				rg.RowRegistration();

				search();
				databind();
				hd_Num.Text = UltraWebGrid2.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString();

				tb_ItemNum.Text = "";
				tb_ItemDrawNum.Text = "";
				tb_ItemName.Text = "";
				tb_RemainderQuantity.Text = "0";
				tb_OutStoreQuantity.Text = "0";
				tb_UnInspectionQuantity.Text = "0";
				Textbox3.Text = "";
				hdLotNum.Value = "";
				SearchStore();

				
			}

		}

		/// <summary>
		/// 바인딩 함수
		/// </summary>
		private void databind()
		{
			UltraWebGrid2.DataSource = UWG_Search();
			UltraWebGrid2.DataBind();

			//Register rs = new Register("SaleHistoryRegistration",Session["ID"].ToString(),UltraWebGrid2);
			//rs.SaleRegistration();

			
		}

		/// <summary>
		/// 검색함수
		/// </summary>
		private void search()
		{
			Search aa;
			if(ItemSearchControl1.hdItem.Trim() =="")
				aa = new Search("GoodsManufactureOutStorehouse",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum, txtOrderNum1.Text.Trim(),ddlPropertyClassification.SelectedIndex);
			else
				aa = new Search("GoodsManufactureOutStorehouse",ItemSearchControl1.ItemNum,"", "",wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum, txtOrderNum1.Text.Trim(),ddlPropertyClassification.SelectedIndex);
			UltraWebGrid1.DataSource = aa.DataSet_search();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 창고선택시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dl_Store_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(dl_Store.SelectedIndex != 0)
				SearchStore();
		}


		private void SearchStore()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "";
			if(dl_Store.SelectedItem.Value == "7")
			{
				str = "Select StockQuantity12 From AS_MT WHERE RecodingState = 1 and  ItemNum = @num and [Year] = @year";

				SqlCommand comm = new SqlCommand(str,conn);
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = tb_ItemNum.Text.Trim();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
				
					tb_StoreQuantity.Text = dr["StockQuantity12"].ToString();
				
				}
				dr.Read();
			}
			else
			{
				str = "Select StockQuantity12 From BS_MT WHERE RecodingState = 1 and  ItemNum = @num and BusinessStorehouseNum = @storenum and [Year] = @year";

				SqlCommand comm = new SqlCommand(str,conn);
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = tb_ItemNum.Text.Trim();
				comm.Parameters.Add("@storenum", SqlDbType.VarChar).Value = dl_Store.SelectedItem.Value;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
				
					tb_StoreQuantity.Text = dr["StockQuantity12"].ToString();
				
				}
				dr.Read();

			}
//			if(tb_StoreQuantity.Text == "0")
//			{
//				Response.Write("<script language=javascript>");
//				Response.Write("alert('재고가 없습니다!');");
//				Response.Write("</script>");
//			}
			conn.Close();
		}
		/// <summary>
		/// 발행버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Publication_Click(object sender, System.EventArgs e)
		{
			int cnt = UltraWebGrid2.Rows.Count;
			bool state = false;
			
			if(cnt == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('발행할 항목이 없습니다!');");
				Response.Write("</script>");
			}
			
			else if(cnt > 9)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('발행할 항목은 최대 9개입니다!');");
				Response.Write("</script>");
			}
			
			else
			{
				for(int i=0; i<=cnt-1; i++)
				{
					if(UltraWebGrid2.Rows[i].Cells.FromKey("BusinessRegistrationNum").ToString() == hd_Num.Text)
					{
						state = true;
					}
					else
					{
						state = false;
						break;
					}
				}
			
				if(state == true)
				{
					Session["Grid"] = UltraWebGrid2;
					Response.Write("<script>window.open('Popup/BusinessDetailedStatement.aspx','BusinessDetailedStatement', 'menubar=yes, status=no,center=yes,toolbar=1, resizable=0, scrollbars=1, width=686, height=800');</script>");
				}			
			
				else
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('발행할 거래처가 하나 이상입니다!');");
					Response.Write("</script>");
				}
			}
		
		}


		private void lnk_Update_Click(object sender, System.EventArgs e)
		{
			SearchStore();
		}

		private void btLot_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.open('../LotNumberManagement.aspx','LotNumberManagement','width=760,height=400,left=300,top=400, center=yes, resizable= yes');</script>");				
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;

			Search aa = new Search("GoodsManufactureOutStorehouse",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,wdcFromDate,wdcToDate,CSC1.Company,CSC1.BusinessRegistrationNum, txtOrderNum1.Text.Trim());
			UltraWebGrid1.DataSource = aa.DataSet_search();
			UltraWebGrid1.DataBind();
		}


		private DataSet UWG_Search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select * from OS_HT Where RegistrationDate = @date order by OutStorehouseHistoryIndex desc";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;			
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			string formID = "OutReceiveingOutStorehose" ;

			Response.Write("<script language=javascript>");
			Response.Write("window.open('OutReceiveingOutStorehose.aspx?formID=" + formID + "','Calendar'," + "'width=680px" + "," + "height=270px');");
			Response.Write("</script>");  
		}
	}
}
