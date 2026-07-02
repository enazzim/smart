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
	/// InferiorIndemnity에 대한 요약 설명입니다.
	/// </summary>
	public class InferiorIndemnity : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDate;
		protected System.Web.UI.WebControls.TextBox tbQuantity;
		protected System.Web.UI.WebControls.TextBox tb_Cost;
		protected System.Web.UI.WebControls.Button bt_Delete;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.Button bt_Register;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_OldCost;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected System.Web.UI.WebControls.DropDownList ddlInferiorStatus;
		protected System.Web.UI.WebControls.DropDownList ddlInferiorCause;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			ItemSearchControl1.Products = true; //제품 바인딩
			ItemSearchControl1.HalfFinishedProducts=true;
			ItemSearchControl1.RawMaterials = true;
			CSC1.UnitCostDistinction = "구매외주";
			
			if(Session["ID"] == null)
			{	
				//Session.Abandon();
				//Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Homepage/default.htm';</script>");
			}
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				bt_Delete.Attributes.Add("onClick", "return OK('선택한 항목을 삭제');");
				
				tbQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tbQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tbQuantity.Attributes.Add("OnBlur", "OnBlur_Float(this);");

				tb_Cost.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
				tb_Cost.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				tb_Cost.Attributes.Add("OnBlur", "OnBlur_Float(this);");
				

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();


				/////////////////////////////////////////
				/// 클레임 현상 코드 삽입
				/////////////////////////////////////////
				string str = "SELECT SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1000' and SmallClassificationName != '' order by SmallClassificationName";
				SqlCommand comm =  new SqlCommand(str,conn);
				SqlDataAdapter da = new SqlDataAdapter(comm) ;
				DataSet ds = new DataSet() ;
				da.Fill(ds,"Code");

				ddlInferiorStatus.DataSource = ds;
				ddlInferiorStatus.DataMember = "Code";
				ddlInferiorStatus.DataTextField = "SmallClassificationName";
				ddlInferiorStatus.DataValueField = "SmallClassificationCode";
				ddlInferiorStatus.DataBind();
				ddlInferiorStatus.Items.Insert(0, "---선택---");
				ddlInferiorStatus.Items[0].Value = "";

				/////////////////////////////////////////
				/// 클레임 원인 코드 삽입
				/////////////////////////////////////////
				string str1 = "SELECT SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '1010' and SmallClassificationName != '' order by SmallClassificationName";
				SqlCommand comm1 =  new SqlCommand(str1,conn);
				SqlDataAdapter da1 = new SqlDataAdapter(comm1) ;
				DataSet ds1 = new DataSet() ;
				da1.Fill(ds1,"Code");

				ddlInferiorCause.DataSource = ds1;
				ddlInferiorCause.DataMember = "Code";
				ddlInferiorCause.DataTextField = "SmallClassificationName";
				ddlInferiorCause.DataValueField = "SmallClassificationCode";
				ddlInferiorCause.DataBind();
				ddlInferiorCause.Items.Insert(0, "---선택---");
				ddlInferiorCause.Items[0].Value = "";


				wdcDate.NullDateLabel = DateTime.Now.ToShortDateString();


				conn.Close();
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
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Register_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
