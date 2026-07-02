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
	/// SubItemDelete에 대한 요약 설명입니다.
	/// </summary>
	public class SubItemDelete : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList dl_Store;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.DropDownList dl_WC;
		protected System.Web.UI.WebControls.TextBox tb_Quantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdProcessName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdProcessCode;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemDrawNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden5;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemName;
		protected System.Web.UI.HtmlControls.HtmlInputHidden Hidden1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdItemNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdProcessSequenceNum;
		protected System.Web.UI.WebControls.Button bt_Throw;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			ItemSearchControl1.RawMaterials = true; //원자재 바인딩
			ItemSearchControl1.Commodity = true; //상품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true; //반제품 바인딩
			ItemSearchControl1.Products = true; //제품 바인딩	

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{

				////Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 기타입출고';</script>");

				bt_Throw.Attributes.Add("onclick","return OK('선택품목을 투입 ');");
				
				tb_Quantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// focus를 얻으면 자동으로 select()
				tb_Quantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_Quantity.Attributes.Add("OnBlur", "OnBlur_Float(this);");
				bt_Clear.Attributes.Add("onClick", "ResetTextBox()");



				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				


				string str1 = "Select WCName, WCInfoIndex From WCI_MT Where RecodingState = 1";
				SqlCommand comm1 = new SqlCommand(str1,conn);
				SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
				DataSet ds1 = new DataSet() ;
				da1.Fill(ds1);

				dl_WC.DataSource = ds1;
				dl_WC.DataTextField = ds1.Tables[0].Columns[0].ToString();
				dl_WC.DataValueField = ds1.Tables[0].Columns[1].ToString();
				dl_WC.DataBind();
				dl_WC.Items.Insert(0, "-선택-") ;
				dl_WC.Items[0].Value = "";

				

				conn.Close();

				wdcDate.NullDateLabel = DateTime.Now.ToShortDateString();
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
			this.bt_Throw.Click += new System.EventHandler(this.bt_Throw_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			if(dl_Store.SelectedIndex == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('창고를 선택하세요!');");
				Response.Write("</script>");

			}
			else
			{
				Search();
			}
		}

		private void Search()
		{
			Search search = new Search("SubItemDelete",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,dl_Store.SelectedItem.Text,int.Parse(dl_Store.SelectedItem.Value));
			UltraWebGrid1.DataSource = search.DataSet_search();
			UltraWebGrid1.DataBind();

			hdItemDrawNum.Value = "";
			hdItemName.Value = "";
			hdItemNum.Value = "";
			hdProcessCode.Value = "";
			hdProcessName.Value ="";
			hdProcessSequenceNum.Value ="";
			
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			dl_Store.SelectedIndex = 0;
		}

		

		/// <summary>
		/// 페이지 이동시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			Search search = new Search("SubItemDelete",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,dl_Store.SelectedItem.Text,int.Parse(dl_Store.SelectedItem.Value));
			UltraWebGrid1.DataSource = search.DataSet_search();
			UltraWebGrid1.DataBind();
		}

		private void bt_Throw_Click(object sender, System.EventArgs e)
		{
			if(hdItemNum.Value.Trim() == "")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('품목을 선택하세요!');");
				Response.Write("</script>");

			}
			else if(tb_Quantity.Text.Trim() == "" || tb_Quantity.Text.Trim() == "0")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('수량을 입력하세요!');");
				Response.Write("</script>");
			}
			else if(dl_WC.SelectedIndex == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('작업장을 선택하세요!');");
				Response.Write("</script>");
			}
			else
			{
				ArrayList Arr = new ArrayList();
				Arr.Add(hdItemNum.Value.Trim());
				Arr.Add(hdItemDrawNum.Value.Trim());
				Arr.Add(hdItemName.Value.Trim());
				Arr.Add(hdProcessSequenceNum.Value.Trim());
				Arr.Add(hdProcessCode.Value.Trim());
				Arr.Add(hdProcessName.Value.Trim());
				Arr.Add(tb_Quantity.Text.Trim());
				Arr.Add(dl_WC.SelectedItem.Text);
			
				Register reg = new Register(Arr,wdcDate,int.Parse(dl_Store.SelectedItem.Value),Session["ID"].ToString(),Session["UserName"].ToString());
				
				reg.SubItemTrowingRegister();
				
				Search();
				
			}
		}

		
	}
}
