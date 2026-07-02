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

using Microsoft.Win32;
using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// ProductionPlanAdd에 대한 요약 설명입니다.
	/// </summary>
	public class ProductionPlanAdd : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label5;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgPP_HT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtItemDrawNum;
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtProductionPlanHistoryIndex;
		protected System.Web.UI.WebControls.DropDownList ddlProductionPlanHistorySource;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcStartDate;
		protected Infragistics.WebUI.WebDataInput.WebNumericEdit wneProductionPlanQuantity;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Button btnDelete;
		protected System.Web.UI.WebControls.Button btnUpDate;
		protected System.Web.UI.HtmlControls.HtmlInputText txtItemState;
		protected System.Web.UI.WebControls.LinkButton LinkButton2;
		protected System.Web.UI.HtmlControls.HtmlInputButton btAdd;
		
		private ArrayList productionPlanAdd;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;	
			ItemSearchControl1.IsDoPost = true;

			if(!Page.IsPostBack)
			{
				// MainTitle Frame에 해당 페이지 Title출력
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 생산관리 > 생산계획 추가';</script>");

				// 불량품목현황버튼에 대하여 onclick 속성 추가
				//lnkInferiorItemPC.Attributes.Add("onclick", "Popup('PopupWindows/InferiorItemPC.aspx', '불량품목 현황', '800', '600');");
				// 추가버튼에 대하여 onclick 속성 추가
				//btnAdd.Attributes.Add("onclick", "return Confirm('선택한 품목을 추가하시겠습니까?');");
				// 삭제버튼에 대하여 onclick 속성 추가
				//btnDelete.Attributes.Add("onclick", "return Confirm('선택한 품목을 삭제하시겠습니까?');");
				// 수정버튼에 대하여 onclick 속성 추가
				btnUpDate.Attributes.Add("onclick", "return Confirm('선택한 품목을 수정하시겠습니까?');");

				// 생산계획에서 생사시작일은 현재 일자의 하루 뒷날을 표시
				wdcStartDate.NullDateLabel = DateTime.Today.AddDays(1.0).ToShortDateString();

				
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				// 생산계획원천선택을 위한 DropDownList에 해당 Data를 바인딩
				string strSQL = @"SELECT SmallClassificationCode, SmallClassificationName FROM PUC_MT 
								  WHERE LargeClassificationCode LIKE '0320%' AND RecodingState = '1' AND SmallClassificationName != '' and SmallClassificationName != '정상' and SmallClassificationName != '실행계획' and SmallClassificationName != '기종등록'";
				SqlCommand comm = new SqlCommand(strSQL, conn);
				SqlDataReader reader = comm.ExecuteReader();
				ddlProductionPlanHistorySource.DataSource = reader;
				ddlProductionPlanHistorySource.DataTextField = "SmallClassificationName";
				ddlProductionPlanHistorySource.DataValueField = "SmallClassificationCode";
				ddlProductionPlanHistorySource.DataBind();
				
				reader.Close();
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
			this.LinkButton2.Click += new System.EventHandler(this.LinkButton2_Click);
			this.LinkButton1.Click += new System.EventHandler(this.btnAdd_Click);
			this.btnUpDate.Click += new System.EventHandler(this.btnUpDate_Click);
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// TODO : 추가버튼 클릭시
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if (Validate(ItemSearchControl1.ItemNum.Trim(), ItemSearchControl1.ItemDrawNum.Trim()
				, ItemSearchControl1.ItemName.Trim())  )
			{

				InputData();

				KIT_ERP.Registration registrPP_HT = new KIT_ERP.Registration("ProductionPlanAdd",Session["ID"].ToString(), "", productionPlanAdd);

				uwgPP_HT.DataSource = registrPP_HT.MainTableRegistration();
				uwgPP_HT.DataBind();
			}
		}


		// TODO : 현재 각 컨트롤의 값을 ArrayList의 저장
		private void InputData()
		{
			productionPlanAdd = new ArrayList();

			//productionPlanAdd.Add(wcbItemName.DataValue);
			productionPlanAdd.Add(ItemSearchControl1.ItemNum);

			productionPlanAdd.Add(ItemSearchControl1.ItemDrawNum);

			//productionPlanAdd.Add(wcbItemName.DisplayValue);
			productionPlanAdd.Add(ItemSearchControl1.ItemName);

			productionPlanAdd.Add(ddlProductionPlanHistorySource.SelectedItem.Value);
			productionPlanAdd.Add(ddlProductionPlanHistorySource.SelectedItem.Text);
			productionPlanAdd.Add(wneProductionPlanQuantity.ValueDecimal);
			productionPlanAdd.Add(wdcStartDate.Text);
		}


		// TODO : 초기화버튼 클릭시
		private void btnClear_Click(object sender, System.EventArgs e)
		{
			//wcbItemName.DataValue = "";
			ItemSearchControl1.ClearTextBox();
			wdcStartDate.NullDateLabel = DateTime.Today.AddDays(1.0).ToShortDateString();
			ddlProductionPlanHistorySource.SelectedIndex = 0;
			wneProductionPlanQuantity.Text = "";
			txtProductionPlanHistoryIndex.Value = "0";
			txtItemDrawNum.Value = "";
			txtItemState.Value = "";
		}


		// TODO : 수정버튼 클릭시
		private void btnUpDate_Click(object sender, System.EventArgs e)
		{
			InputData();

			KIT_ERP.Registration registrPP_HT = new KIT_ERP.Registration("ProductionPlanAdd",Session["ID"].ToString(), txtProductionPlanHistoryIndex.Value, productionPlanAdd);

			uwgPP_HT.DataSource = registrPP_HT.MainTableUpdate();
			uwgPP_HT.DataBind();
		}

		// TODO : 삭제버튼 클릭시
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			KIT_ERP.Registration registrPP_HT = new KIT_ERP.Registration("ProductionPlanAdd", txtProductionPlanHistoryIndex.Value);
			
			uwgPP_HT.DataSource = registrPP_HT.MainTableDelete();
			uwgPP_HT.DataBind();

			this.btnClear_Click(sender, e);
		}

		private bool Validate(string item, string draw, string name)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			SqlTransaction tr = conn.BeginTransaction();
			comm.Connection = conn;
			comm.Transaction = tr;
			int a = 0;

			try
			{
				string str = @"Select ItemNum From II_MT Where RecodingState = 1 and ItemNum = @num";
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
				comm.CommandText = str;
				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet ds = new DataSet();
				da.Fill(ds);

				if(ds.Tables[0].Rows.Count == 0 )
					throw new Exception("품목번호를 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemDrawNum From II_MT Where RecodingState = 1 and ItemDrawNum = @draw";
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = draw;
				comm.CommandText = str;
				SqlDataAdapter da1 = new SqlDataAdapter(comm);
				DataSet ds1 = new DataSet();
				da1.Fill(ds1);

				if(ds1.Tables[0].Rows.Count == 0 )
					throw new Exception("도면번호를 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemName From II_MT Where RecodingState = 1 and ItemName = @name";
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = name;
				comm.CommandText = str;
				SqlDataAdapter da2 = new SqlDataAdapter(comm);
				DataSet ds2 = new DataSet();
				da2.Fill(ds2);

				if(ds2.Tables[0].Rows.Count == 0 )
					throw new Exception("품목명을 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemName From II_MT Where RecodingState = 1 and ItemNum = @num and ItemDrawNum = @draw and ItemName = @name";
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = draw;
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = name;

				comm.CommandText = str;
				SqlDataAdapter da3 = new SqlDataAdapter(comm);
				DataSet ds3 = new DataSet();
				da3.Fill(ds3);

				if(ds3.Tables[0].Rows.Count == 0 )
					throw new Exception("품목번호, 도면번호, 품목명을 다시 확인하세요.");

				tr.Commit();
			}
			catch(Exception ee)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('"+ee.Message+"');");
				Response.Write("</script>");
				tr.Rollback();
				a = 1;
			}
			finally
			{
				conn.Close();
			}
			if(a == 0)
				return true;
			else
				return false;
		}

		private void LinkButton2_Click(object sender, System.EventArgs e)
		{
			string str = "Select SmallClassificationName From PUC_MT inner join II_MT on SmallClassificationCode = ItemState where ItemNum = @ItemNum and II_MT.RecodingState = 1";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			SqlDataReader dr = comm.ExecuteReader();

			if(dr.Read())
			{
				txtItemState.Value = dr["SmallClassificationName"].ToString();
			}
			conn.Close();

		}

	

		
		
	}
}
