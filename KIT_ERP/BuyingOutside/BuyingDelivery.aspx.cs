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
	/// BuyingDelivery에 대한 요약 설명입니다.
	/// </summary>
	public class BuyingDelivery : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Button Button11;
		protected System.Web.UI.WebControls.TextBox txtItemNum;
		protected System.Web.UI.WebControls.TextBox txtItemName;
		protected System.Web.UI.WebControls.TextBox txtOrderQuantity;
		protected System.Web.UI.WebControls.TextBox txtRemainQuantity;
		protected System.Web.UI.WebControls.Button btnRegistration;
		protected System.Web.UI.WebControls.TextBox txtDeliveryQuantity;
		protected System.Web.UI.WebControls.TextBox txtCashQuantity;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgBD_HT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden rowIndex1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden HistoryIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden IndexNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden UnitCost;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdLotNum;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcEndDate;
		protected System.Web.UI.WebControls.TextBox txtCost;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.DropDownList ddlMon;
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.RawMaterials = true; //원자재 바인딩
			ItemSearchControl1.Commodity = true; //상품 바인딩
			CSC1.UnitCostDistinction = "구매거래처";

			

			//젤 처음 페이지가 로드될때....
			if(!Page.IsPostBack)
			{	
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
				//	*************************************************
				//	**  품목분류1 드롭다운리스트... 데이타바인딩... **		
				//	*************************************************
				//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
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


				foreach(ListItem list in ddlYear.Items)
				{
					if(list.Value == DateTime.Now.Year.ToString())
					{
						list.Selected = true;
					}
				}

				for(int a = 0; a < ddlMon.Items.Count; a++)
				{
					if(DateTime.Now.Month == 12)
					{
						if(DateTime.Now.Day > 25)
						{
							for(int b = 0; b< ddlYear.Items.Count;b++)
							{
								ddlYear.Items[b].Selected = false;
								if(int.Parse(ddlYear.Items[b].Value) == DateTime.Now.Year + 1)
									ddlYear.Items[b].Selected = true;
							}
							ddlMon.Items[0].Selected = true;							
							break;
						}
						else
						{
							ddlMon.Items[DateTime.Now.Month-1].Selected = true;
							break;
						}
					}
					else
					{
						if(ddlMon.Items[a].Value == DateTime.Now.Month.ToString() )
						{
							if(DateTime.Now.Day > 25)
								ddlMon.Items[a+1].Selected = true;
							else
								ddlMon.Items[a].Selected = true;
							break;
						}
					}
				}
			}	
			// 등록버튼에 대하여 onclick속성을 추가 시킴
			txtDeliveryQuantity.Attributes.Add("OnKeyUp", "return Process()");
			wdcDeliveryDate.NullDateLabel = DateTime.Now.ToShortDateString();
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.uwgBD_HT.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgBD_HT_PageIndexChanged);
			this.btnRegistration.Click += new System.EventHandler(this.btnRegistration_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		
		private DataSet Search()
		{
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() =="")
                search = new KIT_ERP.Search("BuyingDelivery",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,  wcStartDate, ddlItemClassification1.SelectedItem.Value, wcEndDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			else
				search = new KIT_ERP.Search("BuyingDelivery",ItemSearchControl1.ItemNum,"", "",  wcStartDate, ddlItemClassification1.SelectedItem.Value, wcEndDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			return search.DataSet_search();
		}

		//검색버튼 클릭
		private void btnSearch_Click(object sender, System.EventArgs e)
		{

			//검색버튼을 누르고 검색조건에 해당하는 내용을 그리드에 뿌러줌
			uwgBD_HT.DisplayLayout.Pager.CurrentPageIndex = 1;
			this.uwgBD_HT.DataSource = Search();
			this.uwgBD_HT.DataBind();	


			this.txtDeliveryQuantity.Text = "0";//검색 버튼 누르고난뒤 납품수량 텍스트박스 초기화
			this.btnRegistration.Enabled = true; //검색을 바로 하고는 등록버튼이 비활성화

		}
		//납품등록버튼 클릭
		private void btnRegistration_Click(object sender, System.EventArgs e)
		{
			if(HistoryIndex.Value.Trim() =="")
			{
				Response.Write("<script language='javascript'>");
				Response.Write("alert('품목을 선택하세요');");
				Response.Write("</script>");

			}
			else if(this.txtDeliveryQuantity.Text == "0" || this.txtDeliveryQuantity.Text.Trim() == "")
			{
				Response.Write("<script language='javascript'>");
				Response.Write("alert('납품수량이 입력되지 않았습니다. 입력해주세요');");
				Response.Write("</script>");
			}
			else
			{
				ArrayList BD_HTArraylist = new ArrayList();
				BD_HTArraylist.Add(int.Parse(HistoryIndex.Value));//원장구분
				BD_HTArraylist.Add(int.Parse(IndexNum.Value));//해당원장번호
				BD_HTArraylist.Add(decimal.Parse(txtDeliveryQuantity.Text));//납품수량
				
				
				//납품일을 입력하지 않으면 등록한 날짜가 자동 입력된다.
				if(this.wdcDeliveryDate.Value == null || wdcDeliveryDate.Text.Trim() == "")
				{
					BD_HTArraylist.Add(DateTime.Now.ToShortDateString());//입고일
				}
				else
				{
					BD_HTArraylist.Add(DateTime.Parse(wdcDeliveryDate.Text));//입고일
				}		
		
				
				BD_HTArraylist.Add(decimal.Parse(txtCost.Text));//단가
				BD_HTArraylist.Add("");//Lot번호
				BD_HTArraylist.Add(ddlYear.SelectedItem.Value);	//매입년
				BD_HTArraylist.Add(ddlMon.SelectedItem.Value);	//매입월
				BD_HTArraylist.Add(decimal.Parse(UnitCost.Value));//표준단가
				if(MonthClosing())
				{
					KIT_ERP.Register register = new KIT_ERP.Register(BD_HTArraylist,"BuyingDelivery",Session["ID"].ToString());
					register.Registration();
				}
				else
				{
					RegisterStartupScript("","<script>alert('월마감이 되어 입고가 불가능 합니다!');</script>");
				}


				this.uwgBD_HT.DataSource = Search();
				this.uwgBD_HT.DataBind();	

				this.txtDeliveryQuantity.Text = "0";//납품등록 버튼 누르고난뒤 납품수량 텍스트박스 초기화
				
				this.txtCost.Text = "0";
				this.wdcDeliveryDate.Value = null;
			}		
		}

		
		private void uwgBD_HT_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgBD_HT.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			this.uwgBD_HT.DataSource = Search();
			uwgBD_HT.DataBind();
		}

		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			
			int year = 0;
			int month = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "구매";
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();

			if(year < int.Parse(ddlYear.SelectedItem.Value))
				return true;
			else if(year == int.Parse(ddlYear.SelectedItem.Value))
			{
				if(month < int.Parse(ddlMon.SelectedItem.Value))
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}		

			
		}
	}
}
