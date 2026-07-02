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
	/// SubBuyingDelivery에 대한 요약 설명입니다.
	/// </summary>
	public class SubBuyingDelivery : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wcEndDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.TextBox txtItemNum;
		protected System.Web.UI.WebControls.TextBox txtItemName;
		protected System.Web.UI.WebControls.TextBox txtDeliveryQuantity;
		protected System.Web.UI.WebControls.TextBox txtCost;
		protected System.Web.UI.WebControls.TextBox txtOrderQuantity;
		protected System.Web.UI.WebControls.TextBox txtCashQuantity;
		protected System.Web.UI.WebControls.TextBox txtRemainQuantity;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDeliveryDate;
		protected System.Web.UI.WebControls.Button btnRegistration;
		protected System.Web.UI.HtmlControls.HtmlInputHidden IndexNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden HistoryIndex;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden UnitCost;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		protected System.Web.UI.WebControls.DropDownList ddlMon;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemDrawNum;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.SubMaterials = true; //부자재 바인딩
			ItemSearchControl1.Expendable = true;// 소모품
			CSC1.UnitCostDistinction = "구매거래처";

			

			//젤 처음 페이지가 로드될때....
			if(!Page.IsPostBack)
			{	
				txtDeliveryQuantity.Attributes.Add("OnKeyUp", "return Process()");
				wdcDeliveryDate.NullDateLabel = DateTime.Now.ToShortDateString();


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
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.btnRegistration.Click += new System.EventHandler(this.btnRegistration_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//페이지이동
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource =  Search();
			UltraWebGrid1.DataBind();
		}

		//검색버튼
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			//검색버튼을 누르고 검색조건에 해당하는 내용을 그리드에 뿌러줌
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			this.UltraWebGrid1.DataSource = Search();
			this.UltraWebGrid1.DataBind();	


			this.TextBox_Reset();
			//this.txtDeliveryQuantity.Text = "0";//검색 버튼 누르고난뒤 납품수량 텍스트박스 초기화
			this.btnRegistration.Enabled = true; //검색을 바로 하고는 등록버튼이 비활성화
		}

		//등록버튼
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
				BD_HTArraylist.Add(ddlYear.SelectedItem.Value);	//매입년
				BD_HTArraylist.Add(ddlMon.SelectedItem.Value);	//매입월
				BD_HTArraylist.Add(decimal.Parse(UnitCost.Value));//표준단가




				if(MonthClosing())
				{
				
					KIT_ERP.Register register = new KIT_ERP.Register(BD_HTArraylist,"SubBuyingDelivery",Session["ID"].ToString());
					register.Registration();
				}
				else
				{
					RegisterStartupScript("","<script>alert('월마감이 되어 입고가 불가능 합니다!');</script>");
				}
					
				

				this.UltraWebGrid1.DataSource = Search();
				this.UltraWebGrid1.DataBind();	

				this.TextBox_Reset();
				
			}
		}

		/// <summary>
		/// 데이터 초기화
		/// </summary>
		private void TextBox_Reset()
		{
			txtItemName.Text = "";
			txtItemNum.Text = "";
			hdItemDrawNum.Value = "";
			txtCashQuantity.Text  ="0";
			txtDeliveryQuantity.Text = "";
			txtOrderQuantity.Text = "";
			txtRemainQuantity.Text = "";
			txtCost.Text = "0";
			wdcDeliveryDate.Value = null;
		}

	

		//검색함수
		private DataSet Search()
		{
			KIT_ERP.Search search;
			if(ItemSearchControl1.hdItem.Trim() =="")
				search = new KIT_ERP.Search("SubBuyingDelivery",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,  wcStartDate, wcEndDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			else
				search = new KIT_ERP.Search("SubBuyingDelivery",ItemSearchControl1.ItemNum,"", "",  wcStartDate, wcEndDate,CSC1.Company,CSC1.BusinessRegistrationNum);
			return search.DataSet_search();
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
			{
				return true;
			}
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
