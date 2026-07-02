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

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// AddItemInStore에 대한 요약 설명입니다.
	/// </summary>
	public class AddItemInStore : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList ddlStore;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.TextBox tb_Quantity;
		protected System.Web.UI.WebControls.DropDownList dl_Reason;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdctDate;
		protected System.Web.UI.WebControls.Button bt_InOutStore;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdComnum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdComname;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts = true;//반제품 바인딩
			ItemSearchControl1.RawMaterials = true; //원자재

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{

				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 기타입출고';</script>");

				bt_InOutStore.Attributes.Add("onclick","return OK('선택항목을 입고 ');");
				
				tb_Quantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// focus를 얻으면 자동으로 select()
				tb_Quantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_Quantity.Attributes.Add("OnBlur", "OnBlur_Float(this);");
				
				
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				
				//	*************************************************
				//	**  사유 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				//코드분류표에서 대분류명이 입출고사유인 소분류명을 가지고 옴.
				string str_Reason = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1700'" ;
				SqlCommand comm_Reason = new SqlCommand(str_Reason,conn);
				SqlDataAdapter da_Reason = new SqlDataAdapter(comm_Reason) ;
				DataSet ds_Reason = new DataSet() ;
				da_Reason.Fill(ds_Reason);
				
				dl_Reason.DataSource = ds_Reason;
				dl_Reason.DataTextField = ds_Reason.Tables[0].Columns[0].ToString();
				dl_Reason.DataValueField = ds_Reason.Tables[0].Columns[1].ToString();
				dl_Reason.DataBind();
				dl_Reason.Items.Insert(0, "-선택-") ;
				dl_Reason.Items[0].Value = "";

				conn.Close();

				wdctDate.NullDateLabel = DateTime.Now.ToShortDateString();
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
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.bt_InOutStore.Click += new System.EventHandler(this.bt_InOutStore_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_InOutStore_Click(object sender, System.EventArgs e)
		{
			if(tb_Quantity.Text == "0" || tb_Quantity.Text.Trim() == "")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('수량을 입력하세요!');");
				Response.Write("</script>");
			}
			else if(lb_RowSelectIndex.Value.Trim() == "")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('입고할 품목을 그리드에서 선택하세요!');");
				Response.Write("</script>");
			}
			else if(dl_Reason.SelectedItem.Value == "")
			{
				RegisterStartupScript("","<script>alert('입고 사유를 선택하세요!');</script>");
			}
			else
			{
				ArrayList arr = new ArrayList();

				arr.Add(decimal.Parse(tb_Quantity.Text));
				
				//창고명
				arr.Add(ddlStore.SelectedItem.Text);
				//영업창고번호
				//입출고 사유
				arr.Add(dl_Reason.SelectedItem.Value);//입출고사유코드
				arr.Add(dl_Reason.SelectedItem.Text);//입출고사유내용
				arr.Add(wdctDate.Text);
				arr.Add(int.Parse(lb_RowSelectIndex.Value));//그리드 Row 인덱스

				Register reg = new Register(UltraWebGrid1,arr,"AddItemInStore",Session["ID"].ToString());
				reg.RowRegistration();
				

				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();

				lb_RowSelectIndex.Value = "";
			}
		}

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			if(ddlStore.SelectedIndex == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('창고를 선택하세요!');");
				Response.Write("</script>");

			}
			else
			{
				UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
				UltraWebGrid1.DataSource = Search();
				UltraWebGrid1.DataBind();
			}
		}

		private DataSet Search()
		{
			Search search;
			search = new Search("AddItemInStore",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName, ddlStore.SelectedItem.Text);
			return search.DataSet_search();
			
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Search();
			UltraWebGrid1.DataBind();
		}

	}
}
