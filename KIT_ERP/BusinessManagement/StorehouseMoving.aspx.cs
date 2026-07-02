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
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// StorehouseMoving에 대한 요약 설명입니다.
	/// </summary>
	public class StorehouseMoving : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.DropDownList dl_Store;
		protected System.Web.UI.WebControls.TextBox tb_ItemNum;
		protected System.Web.UI.WebControls.TextBox tb_ItemDrawNum;
		protected System.Web.UI.WebControls.TextBox tb_ItemName;
		protected System.Web.UI.WebControls.TextBox tb_MovingQuantity;
		protected System.Web.UI.WebControls.Button bt_Reset;
		protected System.Web.UI.WebControls.Button bt_Move;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_RowSelectIndex;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected System.Web.UI.WebControls.Button bt_Clear;
		protected System.Web.UI.WebControls.DropDownList dl_MovingStore;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.Commodity = true; //상품 바인딩				
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.Phantom = true;//펜텀 바인딩

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{ 
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 영업관리 > 창고이동';</script>");

				//bt_Move.Attributes.Add("onclick","return OK('선택항목을 이동 '); ");
				
				//숫자만 입력가능
				tb_MovingQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				// focus를 얻으면 자동으로 select()
				tb_MovingQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				// focus를 잃으면 다시 입력란을 체크
				tb_MovingQuantity.Attributes.Add("OnBlur", "OnBlur_Float(this);");
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
			this.bt_Reset.Click += new System.EventHandler(this.bt_Reset_Click);
			this.bt_Move.Click += new System.EventHandler(this.bt_Move_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			if(dl_Store.SelectedIndex ==0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('창고를 선택하세요');");
				Response.Write("</script>");
			}
			else
			{
				Search();
			}
		}

		/// <summary>
		/// 창고검색함수
		/// </summary>
		private void Search()
		{
			Search search = new Search("StorehouseMoving",ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName,int.Parse(dl_Store.SelectedItem.Value));
			UltraWebGrid1.DataSource = search.DataSet_search();
			UltraWebGrid1.DataBind();	
		}

		/// <summary>
		/// 이동 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Move_Click(object sender, System.EventArgs e)
		{
			if(dl_MovingStore.SelectedItem.Value == dl_Store.SelectedItem.Value)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('이동하고자하는 창고가 동일합니다');");
				Response.Write("</script>");
			}
			else if(dl_MovingStore.SelectedIndex == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('이동할 창고를 선택하세요');");
				Response.Write("</script>");
			}
			else if(tb_MovingQuantity.Text =="0" || tb_MovingQuantity.Text.Trim() == "")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('이동시킬 수량을 선택하세요');");
				Response.Write("</script>");
			}
			else
			{
				ArrayList Arr = new ArrayList();
				Arr.Add(decimal.Parse(tb_MovingQuantity.Text));
				Arr.Add(dl_Store.SelectedItem.Text);//원창고명
				Arr.Add(int.Parse(dl_Store.SelectedItem.Value));//원창고번호
				Arr.Add(dl_MovingStore.SelectedItem.Text);//이동창고명
				Arr.Add(int.Parse(dl_MovingStore.SelectedItem.Value));//이동창고번호
				Arr.Add(int.Parse(lb_RowSelectIndex.Value));
				Register reg = new Register(UltraWebGrid1,Arr,"StorehouseMoving",Session["ID"].ToString());
				reg.RowRegistration();
				Search();
			}
		}

		/// <summary>
		/// 그리드 아래 초기화버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Reset_Click(object sender, System.EventArgs e)
		{
			dl_MovingStore.SelectedIndex = 0;
			tb_ItemDrawNum.Text = "";
			tb_ItemName.Text = "";
			tb_ItemNum.Text ="";
			tb_MovingQuantity.Text = "";
		}

		private void bt_Clear_Click(object sender, System.EventArgs e)
		{
			dl_Store.SelectedIndex = 0;			

		}
	}
}
