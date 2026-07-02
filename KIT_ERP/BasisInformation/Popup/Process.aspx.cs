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
	/// Process에 대한 요약 설명입니다.
	/// </summary>
	public class Process : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden ItemIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden RowIndex;
		
		protected string lb_ItemIndex;
		DataSet dsProcess = new DataSet();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			if(!Page.IsPostBack)
			{
				string str ="";
				// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
				if(Request["txt"].ToString() == "")
					str = @"SELECT   II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, 
							II_MT.PropertyClassification, II_MT.Standard, 
							PUC_MT.SmallClassificationName AS Unit, PSI_MT.ProcessSequenceNum, PSI_MT.ProcessSequenceInfoIndex,
							PSI_MT.ProcessCode, PUC_MT_1.SmallClassificationName as ProcessName
							FROM      II_MT INNER JOIN
							PSI_MT ON II_MT.ItemNum = PSI_MT.ItemNum INNER JOIN
							PUC_MT ON II_MT.Unit = PUC_MT.SmallClassificationCode INNER JOIN
							PUC_MT PUC_MT_1 ON 
							PSI_MT.ProcessCode = PUC_MT_1.SmallClassificationCode
							Where II_MT.RecodingState = 1 and PSI_MT.WorkDistinction != '외주' and PSI_MT.RecodingState = 1 order by II_MT.ItemNum, PSI_MT.ProcessSequenceNum";
				else
					str = @"SELECT   II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, 
							II_MT.PropertyClassification, II_MT.Standard, 
							PUC_MT.SmallClassificationName AS Unit, PSI_MT.ProcessSequenceNum, PSI_MT.ProcessSequenceInfoIndex,
							PSI_MT.ProcessCode, PUC_MT_1.SmallClassificationName as ProcessName
							FROM      II_MT INNER JOIN
							PSI_MT ON II_MT.ItemNum = PSI_MT.ItemNum INNER JOIN
							PUC_MT ON II_MT.Unit = PUC_MT.SmallClassificationCode INNER JOIN
							PUC_MT PUC_MT_1 ON 
							PSI_MT.ProcessCode = PUC_MT_1.SmallClassificationCode 						
							Where ItemName like '" + Request["txt"] + "%' and PSI_MT.WorkDistinction != '외주' and PSI_MT.RecodingState = 1 and II_MT.RecodingState = 1 order by PSI_MT.ItemNum, PSI_MT.ProcessSequenceNum";
				
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet dsProcess = new DataSet();
				da.Fill(dsProcess);
				UltraWebGrid1.DataSource = dsProcess;
				UltraWebGrid1.DataBind();

				Session["dsProcess"] = dsProcess;
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
			this.UltraWebGrid1.SelectedRowsChange += new Infragistics.WebUI.UltraWebGrid.SelectedRowsChangeEventHandler(this.UltraWebGrid1_SelectedRowsChange);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			dsProcess = (DataSet)Session["dsProcess"];

			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = dsProcess;
			UltraWebGrid1.DataBind();
		}

		private void UltraWebGrid1_SelectedRowsChange(object sender, Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs e)
		{
			Response.Write("<script language='javascript'>");
			Response.Write("opener.WorkStandardInfo.tb_ItemNum.value = " + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemNum").Value.ToString() + "';");
			Response.Write("opener.WorkStandardInfo.tb_ItemDrawNum.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemDrawNum").Value.ToString() + "';");
			Response.Write("opener.WorkStandardInfo.tb_ItemName.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemName").Value.ToString() + "';");
			Response.Write("opener.WorkStandardInfo.tb_PropertyClassification.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("PropertyClassification").Value.ToString() + "';");
			Response.Write("opener.WorkStandardInfo.tb_Standard.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("Standard").Value.ToString() + "';");
			Response.Write("opener.WorkStandardInfo.tb_Unit.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("Unit").Value.ToString() + "';");
			Response.Write("opener.WorkStandardInfo.tb_Process.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ProcessName").Value.ToString() + "';");
			Response.Write("opener.WorkStandardInfo.lb_ProcessCode1.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ProcessCode").Value.ToString() + "';");
			Response.Write("opener.WorkStandardInfo.tb_ProcessSequenc.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ProcessSequenceNum").Value.ToString() + "';");
			Response.Write("opener.WorkStandardInfo." + Request["txtID"] + ".value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ProcessSequenceInfoIndex").Value.ToString() + "';");
			Response.Write("self.close();");
			Response.Write("</script>");
			Response.End();
		}
	}
}
