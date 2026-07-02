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
	/// RowMaterialRequriement에 대한 요약 설명입니다.
	/// </summary>
	public class RowMaterialRequriement : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Button btnRequest;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgBR_HT;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcDemandDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			ItemSearchControl1.RawMaterials = true;

			if(!Page.IsPostBack)
			{
				wdcDemandDate.NullDateLabel = DateTime.Today.AddDays(1.0).ToShortDateString();
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
			this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// TODO : 의뢰버튼 클릭시
		private void btnRequest_Click(object sender, System.EventArgs e)
		{
			DataSet dset = new DataSet();
			dset = (DataSet)Session["dataset"];

			bool check = true;
			foreach(Infragistics.WebUI.UltraWebGrid.UltraGridRow row in uwgBR_HT.Rows)
			{
				if((row.Cells.FromKey("FirstDeliveryDemandDate").Value == null) && (wdcDemandDate.Value == null))
				{
					check = false;
					Response.Write("<script>alert('최종납기일이 입력되지않았습니다.');</script>");
					break;
				}
			}
			if(check)
			{

				for(int i = 0; i <uwgBR_HT.Rows.Count;i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(uwgBR_HT.Rows[i].Cells.FromKey("chk").Value == null)
					{
						uwgBR_HT.Rows[i].Cells.FromKey("chk").Value = false;
					}
					if(!bool.Parse(uwgBR_HT.Rows[i].Cells.FromKey("chk").Value.ToString()))
					{
						DataRow[] row = dset.Tables[0].Select("HistoryIndex = '" + uwgBR_HT.Rows[i].Cells.FromKey("HistoryIndex").Value + "'");
						//row[0].Delete(); 
						dset.Tables[0].Rows.Remove(row[0]);
					}
				}

				if(dset.Tables[0].Rows.Count == 0)
				{
					Response.Write("<script>alert('선택한 항목이 없습니다')</script>");
				}
				else
				{
					//uwgBR_HT.DataSource = dset;
					//uwgBR_HT.DataBind();


					KIT_ERP.Register register = new KIT_ERP.Register(uwgBR_HT, wdcDemandDate, "RowMaterialRequriement",Session["ID"].ToString());
					register.GridRegistration();
					
					uwgBR_HT.DataSource = this.Search();;
					uwgBR_HT.DataBind();


					Session.Remove("dataset");
				}
			}
		}

		// TODO : 검색
		private DataSet Search()
		{
			// 자재소요원장의 진행상태가 대기인 품목들을 모두 보여줌
//			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
//			SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionString").ToString());
//			conn.Open();
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string strSQL = @"SELECT MRC_HT.ItemNum AS ItemNum, MRC_HT.ItemDrawNum AS ItemDrawNum, MRC_HT.ItemName AS ItemName,
							PUC_MT.SmallClassificationName as MateralQuality, II_MT.Standard,
							MRC_HT.DeliveryOrderRequestQuantity AS OrderQuantity, StandardUnitCost AS ApplyUnitCost,
							MRC_HT.DeliveryOrderRequestQuantity * II_MT.StandardUnitCost AS TotalCost,
							MRC_HT.MaterialRequirementCalculateHistoryIndex AS HistoryIndex
							FROM MRC_HT  inner join II_MT on MRC_HT.ItemNum = II_MT.ItemNum
							left outer join PUC_MT on II_MT.MateralQuality = PUC_MT.SmallClassificationCode
							where II_MT.RecodingState = 1 and MRC_HT.ItemNum like @num and MRC_HT.ItemDrawNum like @draw and MRC_HT.ItemName like @name and MRC_HT.ProgressCondition = '대기' AND MRC_HT.DeliveryOrderRequestQuantity > 0
							order by ItemNum";

//			string strSQL = @"SELECT MRC_HT.ItemNum AS ItemNum, MRC_HT.ItemDrawNum AS ItemDrawNum, MRC_HT.ItemName AS ItemName,
//							PUC_MT.SmallClassificationName as MateralQuality, II_MT.Standard,
//							MRC_HT.DeliveryOrderRequestQuantity AS OrderQuantity, StandardUnitCost AS ApplyUnitCost,
//							MRC_HT.DeliveryOrderRequestQuantity * II_MT.StandardUnitCost AS TotalCost,
//							MRC_HT.MaterialRequirementCalculateHistoryIndex AS HistoryIndex
//							FROM MRC_HT  inner join II_MT on MRC_HT.ItemNum = II_MT.ItemNum
//							left outer join PUC_MT on II_MT.MateralQuality = PUC_MT.SmallClassificationCode
//							where II_MT.RecodingState = 1 and MRC_HT.ItemNum like @num and MRC_HT.ItemDrawNum like @draw and MRC_HT.ItemName like @name and MRC_HT.ProgressCondition = '대기' AND MRC_HT.DeliveryOrderRequestQuantity > 0
//							order by ItemNum";

			SqlCommand comm = new SqlCommand(strSQL, conn);

			if(ItemSearchControl1.ItemNum == null || ItemSearchControl1.ItemNum == "")
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = "%"+ItemSearchControl1.ItemNum+"%";

			if(ItemSearchControl1.ItemDrawNum == null || ItemSearchControl1.ItemDrawNum == "")
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = "%"+ItemSearchControl1.ItemDrawNum+"%";

			if(ItemSearchControl1.ItemName == null || ItemSearchControl1.ItemName == "")
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = "%%";
			else
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = "%"+ItemSearchControl1.ItemName+"%";

			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet dataset = new DataSet();
			da.Fill(dataset);
			conn.Close();

			Session["dataset"] = dataset;

			return dataset;
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			uwgBR_HT.DataSource = this.Search();;
			uwgBR_HT.DataBind();
		}
	}
}
