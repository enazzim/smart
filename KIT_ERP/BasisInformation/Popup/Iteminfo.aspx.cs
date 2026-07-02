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

namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// Iteminfo에 대한 요약 설명입니다.
	/// </summary>
	public class Iteminfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ItemIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden RowIndex;

		protected string lb_ItemIndex;
		DataSet dsIteminfo = new DataSet();

		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			if(!Page.IsPostBack)
			{
				string str ="";
				// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
				if(Request["txt"].ToString() == "")
					str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = 1 order by ItemNum";
				else
					str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemName like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 order by ItemNum";
				
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet dsIteminfo = new DataSet();
				da.Fill(dsIteminfo);
				UltraWebGrid1.DataSource = dsIteminfo;
				UltraWebGrid1.DataBind();

				Session["dsIteminfo"] = dsIteminfo;
			}

			conn.Close();
							
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
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			dsIteminfo = (DataSet)Session["dsIteminfo"];

			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = dsIteminfo;
			UltraWebGrid1.DataBind();
		}
	}
}
