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
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BusinessManagement
{
	/// <summary>
	/// ItemGroupDeliveryResult에 대한 요약 설명입니다.
	/// </summary>
	public class ItemGroupDeliveryResult : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcFromDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcToDate;
		protected System.Web.UI.WebControls.Button btItemGroupSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button Button1;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup2;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup3;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup4;
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompany;
		protected System.Web.UI.WebControls.DropDownList ddlItemGroup1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Homepage/default.htm';</script>");
			}

			if(!IsPostBack)
			{
				// 페이지가 처음 로드될때 실행
				this.Load_Bind();				
			}
		}

		private void Load_Bind()
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
				
			ddlItemGroup1.DataSource = ds_ItemClassification1;
			ddlItemGroup1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
			ddlItemGroup1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
			ddlItemGroup1.DataBind();
			ddlItemGroup1.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup1.Items[0].Value = "";



			


			//	*************************************************
			//	**  품목분류2 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류2인 소분류명을 가지고 옴.
			string str_ItemClassification2 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0220'" ;
			SqlCommand comm_ItemClassification2 = new SqlCommand(str_ItemClassification2,conn);
			SqlDataAdapter da_ItemClassification2 = new SqlDataAdapter(comm_ItemClassification2) ;
			DataSet ds_ItemClassification2 = new DataSet() ;
			da_ItemClassification2.Fill(ds_ItemClassification2);
				
			ddlItemGroup2.DataSource = ds_ItemClassification2;
			ddlItemGroup2.DataTextField = ds_ItemClassification2.Tables[0].Columns[0].ToString();
			ddlItemGroup2.DataValueField = ds_ItemClassification2.Tables[0].Columns[1].ToString();
			ddlItemGroup2.DataBind();
			ddlItemGroup2.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup2.Items[0].Value = "";



			//	*************************************************
			//	**  품목분류3 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류3인 소분류명을 가지고 옴.
			string str_ItemClassification3 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0230'" ;
			SqlCommand comm_ItemClassification3 = new SqlCommand(str_ItemClassification3,conn);
			SqlDataAdapter da_ItemClassification3 = new SqlDataAdapter(comm_ItemClassification3) ;
			DataSet ds_ItemClassification3 = new DataSet() ;
			da_ItemClassification3.Fill(ds_ItemClassification3);
				
			ddlItemGroup3.DataSource = ds_ItemClassification3;
			ddlItemGroup3.DataTextField = ds_ItemClassification3.Tables[0].Columns[0].ToString();
			ddlItemGroup3.DataValueField = ds_ItemClassification3.Tables[0].Columns[1].ToString();
			ddlItemGroup3.DataBind();
			ddlItemGroup3.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup3.Items[0].Value = "";

			


			//	*************************************************
			//	**  품목분류4 드롭다운리스트... 데이타바인딩... **		
			//	*************************************************
			//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
			string str_ItemClassification4 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0240'" ;
			SqlCommand comm_ItemClassification4 = new SqlCommand(str_ItemClassification4,conn);
			SqlDataAdapter da_ItemClassification4 = new SqlDataAdapter(comm_ItemClassification4) ;
			DataSet ds_ItemClassification4 = new DataSet() ;
			da_ItemClassification4.Fill(ds_ItemClassification4);
				
			ddlItemGroup4.DataSource = ds_ItemClassification4;
			ddlItemGroup4.DataTextField = ds_ItemClassification4.Tables[0].Columns[0].ToString();
			ddlItemGroup4.DataValueField = ds_ItemClassification4.Tables[0].Columns[1].ToString();
			ddlItemGroup4.DataBind();
			ddlItemGroup4.Items.Insert(0, "-선택하세요-") ;
			ddlItemGroup4.Items[0].Value = "";



			string strSQL = "SELECT CompanyName, PresidentName, BusinessRegistrationNum, CompanyInfoIndex FROM CI_MT where RecodingState = 1 and ReceiveingOrderCompany = 1 order by CompanyName";
			SqlCommand commCom = new SqlCommand(strSQL,conn);
			SqlDataAdapter daCom = new SqlDataAdapter(commCom) ;
			DataSet dsCom = new DataSet() ;
			daCom.Fill(dsCom);

			wcCompany.DataSource = dsCom;
			wcCompany.DataTextField = dsCom.Tables[0].Columns[0].ToString();
			wcCompany.DataValueField = dsCom.Tables[0].Columns[2].ToString();
			wcCompany.DataBind();

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
			this.btItemGroupSearch.Click += new System.EventHandler(this.btItemGroupSearch_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btItemGroupSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = ItemGroupSearch();
			UltraWebGrid1.DataBind();
		}

		private DataTable ItemGroupSearch()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter adap = new SqlDataAdapter("SPItemGroupResult",conn);
			adap.SelectCommand.Parameters.Add("@ItemClassification1",ddlItemGroup1.SelectedValue);
			adap.SelectCommand.Parameters.Add("@ItemClassification2",ddlItemGroup2.SelectedValue);
			adap.SelectCommand.Parameters.Add("@ItemClassification3",ddlItemGroup3.SelectedValue);
			adap.SelectCommand.Parameters.Add("@ItemClassification4",ddlItemGroup4.SelectedValue);
			if(wcCompany.DataValue == null)
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum","");
			else
				adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum",wcCompany.DataValue.ToString());
			adap.SelectCommand.Parameters.Add("@StartDate",wdcFromDate.Text);
			adap.SelectCommand.Parameters.Add("@EndDate",wdcToDate.Text);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			
			DataSet ds = new DataSet();
			
			adap.Fill(ds);	
		
			return ds.Tables[0];
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = ItemGroupSearch();
			UltraWebGrid1.DataBind();
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid uwg = new UltraWebGrid();
			uwg = UltraWebGrid1;
			uwg.DisplayLayout.Pager.AllowPaging = false;
			uwg.DataSource = ItemGroupSearch();
			uwg.DataBind();
			UltraWebGridExcelExporter1.Export(uwg);
		}
	}
}
