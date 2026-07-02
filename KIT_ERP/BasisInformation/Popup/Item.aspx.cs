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
	/// Item에 대한 요약 설명입니다.
	/// </summary>
	public class Item : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden ItemIndex;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.HtmlControls.HtmlInputHidden RowIndex;
		protected string lb_ItemIndex;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		DataSet dsItem = new DataSet();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			if(!Page.IsPostBack)
			{
				string str ="";
				// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
				if(Request["txt"].ToString() == "")
				{
					switch(Request["formID"].ToString())
					{
						case "ProcessSequenceNumInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = 1 and (PropertyClassification = '반제품' or PropertyClassification = '제품') order by ItemNum";
							break;
						case "BuyingUnitCodeInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = 1 and (PropertyClassification = '상품' or PropertyClassification = '원자재') order by ItemNum";
							break;
						case "OutSideOrderUnitCodeInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = 1 and (PropertyClassification = '반제품' or PropertyClassification = '제품') order by ItemNum";
							break;
						case "SellingUnitCostInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = 1 and (PropertyClassification = '상품' or PropertyClassification = '제품') order by ItemNum";
							break;
						case "ItemOrganizationInfo1":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = 1 and (PropertyClassification = '반제품' or PropertyClassification = '제품' or PropertyClassification = '팬텀') order by ItemNum";
							break;
						case "ItemOrganizationInfo2":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = 1 and (PropertyClassification = '원자재' or PropertyClassification = '반제품' or PropertyClassification = '제품') order by ItemNum";
							break;
						case "BusinessPlanInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = 1 and (PropertyClassification = '상품' or PropertyClassification = '제품' or PropertyClassification = '팬텀') order by ItemNum";
							break;
						case "ExecutionPlanInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where II_MT.RecodingState = 1 and (PropertyClassification = '상품' or PropertyClassification = '제품') order by ItemNum";
							break;
//						case "ItemOrganizationInfo1":
//							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemDrawNum like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 and (PropertyClassification = '반제품' or PropertyClassification = '제품' or PropertyClassification = '팬텀') order by ItemDrawNum";
//							break;
//						case "ItemOrganizationInfo2":
//							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemDrawNum like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 and (PropertyClassification = '원자재' or PropertyClassification = '반제품' or PropertyClassification = '제품') order by ItemDrawNum";
//							break;
//						case "BusinessPlanInfo":
//							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemDrawNum like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 and (PropertyClassification = '원자재' or PropertyClassification = '반제품' or PropertyClassification = '제품') order by ItemDrawNum";
						default :
							break;

					}

							
				}
				else
				{
					switch(Request["formID"].ToString())
					{
						case "ProcessSequenceNumInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemName like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 and (PropertyClassification = '반제품' or PropertyClassification = '제품') order by ItemNum";
							break;
						case "BuyingUnitCodeInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemName like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 and (PropertyClassification = '상품' or PropertyClassification = '원자재') order by ItemNum";
							break;
						case "OutSideOrderUnitCodeInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemName like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 and (PropertyClassification = '반제품' or PropertyClassification = '제품') order by ItemNum";
							break;
						case "SellingUnitCostInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemName like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 and (PropertyClassification = '상품' or PropertyClassification = '제품') order by ItemNum";
							break;
						case "ItemOrganizationInfo1":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemName like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 and (PropertyClassification = '반제품' or PropertyClassification = '제품' or PropertyClassification = '팬텀') order by ItemNum";
							break;
						case "ItemOrganizationInfo2":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemName like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 and (PropertyClassification = '원자재' or PropertyClassification = '반제품' or PropertyClassification = '제품') order by ItemNum";
							break;
						case "BusinessPlanInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemName like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 and (PropertyClassification = '상품' or PropertyClassification = '제품') order by ItemNum";
							break;
						case "ExecutionPlanInfo":
							str = "Select II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, II_MT.PropertyClassification, II_MT.Standard,II_MT.ItemInfoIndex, PUC_MT.SmallClassificationName  From II_MT Join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode Where ItemName like '" + Request["txt"] + "%' and II_MT.RecodingState = 1 and (PropertyClassification = '상품' or PropertyClassification = '제품') order by ItemNum";
							break;
						default :
							break;;
					}
				}
				SqlCommand comm = new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet dsItem = new DataSet();
				da.Fill(dsItem);
				UltraWebGrid1.DataSource = dsItem;
				UltraWebGrid1.DataBind();

				Session["dsItem"] = dsItem;
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

		private void UltraWebGrid1_SelectedRowsChange(object sender, Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs e)
		{
			switch(Request["formID"].ToString())
			{
				case "ItemOrganizationInfo1":
				{
					Response.Write("<script language='javascript'>");
					Response.Write("opener.ItemOrganizationInfo.tb_ParentItemNum.value = " + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemNum").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo.tb_ParentItemDrawNum.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemDrawNum").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo.tb_ParentItemName.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemName").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo.tb_ParentPropertyClassification.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("PropertyClassification").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo.tb_ParentStandard.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("Standard").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo.tb_ParentUnit.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("SmallClassificationName").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo." + Request["txtID"] + ".value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemInfoIndex").Value.ToString() + "';");
					Response.Write("self.close();");
					Response.Write("</script>");
					Response.End();
					break;
				}
				case"ItemOrganizationInfo2":
				{
					Response.Write("<script language='javascript'>");
					Response.Write("opener.ItemOrganizationInfo.tb_ChildItemNum.value = " + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemNum").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo.tb_ChildItemDrawNum.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemDrawNum").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo.tb_ChildItemName.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemName").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo.tb_ChildPropertyClassification.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("PropertyClassification").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo.tb_ChildStandard.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("Standard").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo.tb_ChildUnit.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("SmallClassificationName").Value.ToString() + "';");
					Response.Write("opener.ItemOrganizationInfo." + Request["txtID"] + ".value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemInfoIndex").Value.ToString() + "';");
					Response.Write("self.close();");
					Response.Write("</script>");
					Response.End();
					break;
				}
				default :
				{
					Response.Write("<script language='javascript'>");
					Response.Write("opener." + Request["formID"] + ".tb_ItemNum.value = " + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemNum").Value.ToString() + "';");
					Response.Write("opener." + Request["formID"] + ".tb_ItemDrawNum.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemDrawNum").Value.ToString() + "';");
					Response.Write("opener." + Request["formID"] + ".tb_ItemName.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemName").Value.ToString() + "';");
					Response.Write("opener." + Request["formID"] + ".tb_PropertyClassification.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("PropertyClassification").Value.ToString() + "';");
					Response.Write("opener." + Request["formID"] + ".tb_Standard.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("Standard").Value.ToString() + "';");
					Response.Write("opener." + Request["formID"] + ".tb_Unit.value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("SmallClassificationName").Value.ToString() + "';");
					Response.Write("opener." + Request["formID"] + "." + Request["txtID"] + ".value =" + "'" + UltraWebGrid1.Rows[int.Parse(RowIndex.Value)].Cells.FromKey("ItemInfoIndex").Value.ToString() + "';");
					Response.Write("self.close();");
					Response.Write("</script>");
					Response.End();
					break;
				}
			}
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			dsItem = (DataSet)Session["dsItem"];

			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = dsItem;
			UltraWebGrid1.DataBind();
		}
	}
}
