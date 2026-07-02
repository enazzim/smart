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
using Microsoft.Win32;
using System.Configuration;

namespace KIT_ERP.SystemInfoManagement.PopupWindows
{
	
	public class PageAuthority : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		protected System.Web.UI.WebControls.CheckBox CheckBox2;
		protected System.Web.UI.WebControls.CheckBox CheckBox3;
		protected System.Web.UI.WebControls.CheckBox CheckBox4;
		protected System.Web.UI.WebControls.CheckBox CheckBox5;
		protected System.Web.UI.WebControls.CheckBox CheckBox6;
		protected System.Web.UI.WebControls.CheckBox CheckBox7;
		protected System.Web.UI.WebControls.CheckBox CheckBox8;
		protected System.Web.UI.WebControls.CheckBox CheckBox9;
		protected System.Web.UI.WebControls.CheckBox CheckBox10;
		protected System.Web.UI.WebControls.CheckBox CheckBox11;
		protected System.Web.UI.WebControls.CheckBox CheckBox12;
		protected System.Web.UI.WebControls.CheckBox CheckBox13;
		protected System.Web.UI.WebControls.CheckBox CheckBox14;
		protected System.Web.UI.WebControls.CheckBox CheckBox15;
		protected System.Web.UI.WebControls.CheckBox CheckBox16;
		protected System.Web.UI.WebControls.CheckBox CheckBox17;
		protected System.Web.UI.WebControls.CheckBox CheckBox18;
		protected System.Web.UI.WebControls.CheckBox CheckBox19;
		protected System.Web.UI.WebControls.CheckBox CheckBox20;
		protected System.Web.UI.WebControls.CheckBox CheckBox21;
		protected System.Web.UI.WebControls.CheckBox CheckBox22;
		protected System.Web.UI.WebControls.CheckBox CheckBox23;
		protected System.Web.UI.WebControls.CheckBox CheckBox24;
		protected System.Web.UI.WebControls.CheckBox CheckBox25;
		protected System.Web.UI.WebControls.CheckBox CheckBox26;
		protected System.Web.UI.WebControls.CheckBox CheckBox27;
		protected System.Web.UI.WebControls.CheckBox CheckBox28;
		protected System.Web.UI.WebControls.CheckBox CheckBox29;
		protected System.Web.UI.WebControls.CheckBox CheckBox30;
		protected System.Web.UI.WebControls.CheckBox CheckBox31;
		protected System.Web.UI.WebControls.CheckBox CheckBox32;
		protected System.Web.UI.WebControls.CheckBox CheckBox33;
		protected System.Web.UI.WebControls.CheckBox CheckBox34;
		protected System.Web.UI.WebControls.CheckBox CheckBox35;
		protected System.Web.UI.WebControls.CheckBox CheckBox36;
		protected System.Web.UI.WebControls.CheckBox CheckBox37;
		protected System.Web.UI.WebControls.CheckBox CheckBox38;
		protected System.Web.UI.WebControls.CheckBox CheckBox39;
		protected System.Web.UI.WebControls.CheckBox CheckBox40;
		protected System.Web.UI.WebControls.CheckBox CheckBox41;
		protected System.Web.UI.WebControls.CheckBox CheckBox42;
		protected System.Web.UI.WebControls.CheckBox CheckBox43;
		protected System.Web.UI.WebControls.CheckBox CheckBox44;
		protected System.Web.UI.WebControls.CheckBox CheckBox45;
		protected System.Web.UI.WebControls.CheckBox CheckBox46;
		protected System.Web.UI.WebControls.CheckBox CheckBox47;
		protected System.Web.UI.WebControls.CheckBox CheckBox48;
		protected System.Web.UI.WebControls.CheckBox CheckBox49;
		protected System.Web.UI.WebControls.CheckBox CheckBox50;
		protected System.Web.UI.WebControls.CheckBox CheckBox51;
		protected System.Web.UI.WebControls.CheckBox CheckBox52;
		protected System.Web.UI.WebControls.CheckBox CheckBox53;
		protected System.Web.UI.WebControls.CheckBox CheckBox54;
		protected System.Web.UI.WebControls.CheckBox CheckBox55;
		protected System.Web.UI.WebControls.CheckBox CheckBox56;
		protected System.Web.UI.WebControls.CheckBox CheckBox57;
		protected System.Web.UI.WebControls.CheckBox CheckBox58;
		protected System.Web.UI.WebControls.CheckBox CheckBox59;
		protected System.Web.UI.WebControls.CheckBox CheckBox60;
		protected System.Web.UI.WebControls.CheckBox CheckBox61;
		protected System.Web.UI.WebControls.CheckBox CheckBox62;
		protected System.Web.UI.WebControls.CheckBox CheckBox63;
		protected System.Web.UI.WebControls.CheckBox CheckBox64;
		protected System.Web.UI.WebControls.CheckBox CheckBox65;
		protected System.Web.UI.WebControls.CheckBox CheckBox66;
		protected System.Web.UI.WebControls.CheckBox CheckBox67;
		protected System.Web.UI.WebControls.CheckBox CheckBox68;
		protected System.Web.UI.WebControls.CheckBox CheckBox69;
		protected System.Web.UI.WebControls.CheckBox CheckBox70;
		protected System.Web.UI.WebControls.CheckBox CheckBox71;
		protected System.Web.UI.WebControls.CheckBox CheckBox72;
		protected System.Web.UI.WebControls.CheckBox CheckBox73;
		protected System.Web.UI.WebControls.CheckBox CheckBox74;
		protected System.Web.UI.WebControls.CheckBox CheckBox75;
		protected System.Web.UI.WebControls.CheckBox CheckBox76;
		protected System.Web.UI.WebControls.CheckBox CheckBox77;
		protected System.Web.UI.WebControls.CheckBox CheckBox78;
		protected System.Web.UI.WebControls.CheckBox CheckBox79;
		protected System.Web.UI.WebControls.CheckBox CheckBox80;
		protected System.Web.UI.WebControls.CheckBox CheckBox81;
		protected System.Web.UI.WebControls.CheckBox CheckBox82;
		protected System.Web.UI.WebControls.CheckBox CheckBox83;
		protected System.Web.UI.WebControls.CheckBox CheckBox84;
		protected System.Web.UI.WebControls.CheckBox CheckBox85;
		protected System.Web.UI.WebControls.CheckBox CheckBox86;
		protected System.Web.UI.WebControls.CheckBox CheckBox87;
		protected System.Web.UI.WebControls.CheckBox CheckBox88;
		protected System.Web.UI.WebControls.CheckBox CheckBox89;
		protected System.Web.UI.WebControls.CheckBox CheckBox90;
		protected System.Web.UI.WebControls.CheckBox CheckBox91;
		protected System.Web.UI.WebControls.CheckBox CheckBox92;
		protected System.Web.UI.WebControls.CheckBox CheckBox93;
		protected System.Web.UI.WebControls.CheckBox CheckBox94;
		protected System.Web.UI.WebControls.CheckBox CheckBox95;
		protected System.Web.UI.WebControls.CheckBox CheckBox96;
		protected System.Web.UI.WebControls.CheckBox CheckBox97;
		protected System.Web.UI.WebControls.CheckBox CheckBox98;
		protected System.Web.UI.WebControls.CheckBox CheckBox99;
		protected System.Web.UI.WebControls.CheckBox CheckBox100;
		protected System.Web.UI.WebControls.CheckBox CheckBox101;
		protected System.Web.UI.WebControls.CheckBox CheckBox102;
		protected System.Web.UI.WebControls.CheckBox CheckBox103;
		protected System.Web.UI.WebControls.CheckBox CheckBox104;
		protected System.Web.UI.WebControls.CheckBox CheckBox105;
		protected System.Web.UI.WebControls.CheckBox CheckBox106;
		protected System.Web.UI.WebControls.CheckBox CheckBox107;
		protected System.Web.UI.WebControls.CheckBox CheckBox108;
		protected System.Web.UI.WebControls.CheckBox CheckBox109;
		protected System.Web.UI.WebControls.CheckBox CheckBox110;
		protected System.Web.UI.WebControls.CheckBox CheckBox111;
		protected System.Web.UI.WebControls.CheckBox CheckBox112;
		protected System.Web.UI.WebControls.CheckBox CheckBox113;
		protected System.Web.UI.WebControls.CheckBox CheckBox114;
		protected System.Web.UI.WebControls.CheckBox CheckBox115;
		protected System.Web.UI.WebControls.CheckBox CheckBox116;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.DropDownList ddlAuthority;


		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				for(int i = 0; i < 100; i++)
				{
					ListItem item = new ListItem(i.ToString("0#") + " 권한");
					ddlAuthority.Items.Add(item);
				}

				if(Request["id"].ToString() == "Add")
				{
					Button1.Text = "확   인";
				}
				else
				{
					Button1.Text = "수   정";

					// 현재 설정되어있는 각종 정보를 가져옴
//					RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
//					SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionStringSystem").ToString());
//					conn.Open();
					SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
					conn.Open();

					string strSQL = @"SELECT * FROM PA_T WHERE PageAuthorityIndex = @PageAuthorityIndex";
					SqlCommand comm = new SqlCommand(strSQL, conn);
					comm.Parameters.Add("@PageAuthorityIndex", SqlDbType.Int).Value = Request["no"].ToString();

					SqlDataReader reader = comm.ExecuteReader();

					while(reader.Read())
					{
						if(bool.Parse(reader["S01_ReceiveingRegistration"].ToString()))
							CheckBox1.Checked = true;
						if(bool.Parse(reader["S02_ReceiveingbundleRegistration"].ToString()))
							CheckBox2.Checked = true;
						if(bool.Parse(reader["S03_ReceiveingPC"].ToString()))
							CheckBox3.Checked = true;
						if(bool.Parse(reader["S04_ReceiveingProgressLook"].ToString()))
							CheckBox4.Checked = true;
						if(bool.Parse(reader["S05_ProductionRequest"].ToString()))
							CheckBox5.Checked = true;
						if(bool.Parse(reader["S06_ProductionRequestPC"].ToString()))
							CheckBox6.Checked = true;
						if(bool.Parse(reader["S07_ProductionRequestAdd"].ToString()))
							CheckBox7.Checked = true;
						if(bool.Parse(reader["S08_GoodsBuyingRequest"].ToString()))
							CheckBox8.Checked = true;
						if(bool.Parse(reader["S09_GoodsBuyingRequestPC"].ToString()))
							CheckBox9.Checked = true;
						if(bool.Parse(reader["S10_GoodsBuyingRequestAdd"].ToString()))
							CheckBox10.Checked = true;
						if(bool.Parse(reader["S11_BusinessStorehouseInStorehouse"].ToString()))
							CheckBox11.Checked = true;
						if(bool.Parse(reader["S12_BusinessStorehouseInStorehousePC"].ToString()))
							CheckBox12.Checked = true;
						if(bool.Parse(reader["S13_GoodsManufactureOutStorehouse"].ToString()))
							CheckBox13.Checked = true;
						if(bool.Parse(reader["S14_GoodsManufactureOutStorehousePC"].ToString()))
							CheckBox14.Checked = true;
						if(bool.Parse(reader["S15_SaleHistoryRegistration"].ToString()))
							CheckBox15.Checked = true;
						if(bool.Parse(reader["S16_SaleHistoryRegistrationPC"].ToString()))
							CheckBox16.Checked = true;
						if(bool.Parse(reader["S17_CollectMoneyRegistration"].ToString()))
							CheckBox17.Checked = true;
						if(bool.Parse(reader["S18_CollectMoneyRegistrationPC"].ToString()))
							CheckBox18.Checked = true;
						if(bool.Parse(reader["S19_UnCollectMoneyLook"].ToString()))
							CheckBox19.Checked = true;
						if(bool.Parse(reader["S20_ClaimRegistration"].ToString()))
							CheckBox20.Checked = true;
						if(bool.Parse(reader["S21_ClaimRegistrationPC"].ToString()))
							CheckBox21.Checked = true;
						if(bool.Parse(reader["S22_StorehouseMoving"].ToString()))
							CheckBox22.Checked = true;
						if(bool.Parse(reader["S23_StorehouseMovingPC"].ToString()))
							CheckBox23.Checked = true;
						if(bool.Parse(reader["P01_ProductionPlan"].ToString()))
							CheckBox24.Checked = true;
						if(bool.Parse(reader["P02_ProductionPlanPC"].ToString()))
							CheckBox25.Checked = true;
						if(bool.Parse(reader["P03_ProductionPlanAdd"].ToString()))
							CheckBox26.Checked = true;
						if(bool.Parse(reader["P04_RowMaterialRequirementCalculate"].ToString()))
							CheckBox27.Checked = true;
						if(bool.Parse(reader["P05_RowMaterialRequirementCalculatePC"].ToString()))
							CheckBox28.Checked = true;
						if(bool.Parse(reader["P06_RowMaterialRequriement"].ToString()))
							CheckBox29.Checked = true;
						if(bool.Parse(reader["P07_RowMaterialRequriementPC"].ToString()))
							CheckBox30.Checked = true;
						if(bool.Parse(reader["P08_RowMaterialRequriementAdd"].ToString()))
							CheckBox31.Checked = true;
						if(bool.Parse(reader["P09_WorkPlan"].ToString()))
							CheckBox32.Checked = true;
						if(bool.Parse(reader["P10_WCPlanPC"].ToString()))
							CheckBox33.Checked = true;
						if(bool.Parse(reader["P11_WorkDailyReportRegistration"].ToString()))
							CheckBox34.Checked = true;
						if(bool.Parse(reader["P12_WorkDailyReportRegistrationPC"].ToString()))
							CheckBox35.Checked = true;
						if(bool.Parse(reader["P13_OutsideRequest"].ToString()))
							CheckBox36.Checked = true;
						if(bool.Parse(reader["P14_OutsideRequestPC"].ToString()))
							CheckBox37.Checked = true;
						if(bool.Parse(reader["O01_BuyingOrder"].ToString()))
							CheckBox38.Checked = true;
						if(bool.Parse(reader["O02_BuyingOrderPC"].ToString()))
							CheckBox39.Checked = true;
						if(bool.Parse(reader["O03_BuyingOrderAdd"].ToString()))
							CheckBox40.Checked = true;
						if(bool.Parse(reader["O04_BuyingDelivery"].ToString()))
							CheckBox41.Checked = true;
						if(bool.Parse(reader["O05_BuyingDeliveryPC"].ToString()))
							CheckBox42.Checked = true;
						if(bool.Parse(reader["O06_OutSideOrder"].ToString()))
							CheckBox43.Checked = true;
						if(bool.Parse(reader["O07_OutSideOrderPC"].ToString()))
							CheckBox44.Checked = true;
						if(bool.Parse(reader["O08_OutSideOutStorehouse"].ToString()))
							CheckBox45.Checked = true;
						if(bool.Parse(reader["O09_OutSideOutStorehousePC"].ToString()))
							CheckBox46.Checked = true;
						if(bool.Parse(reader["O10_OutSideDelivery"].ToString()))
							CheckBox47.Checked = true;
						if(bool.Parse(reader["O11_OutSideDeliveryPC"].ToString()))
							CheckBox48.Checked = true;
						if(bool.Parse(reader["O12_PaymentPlanResultRegistration"].ToString()))
							CheckBox49.Checked = true;
						if(bool.Parse(reader["O13_PaymentPlanResultRegistrationPC"].ToString()))
							CheckBox50.Checked = true;
						if(bool.Parse(reader["O14_QualityInspectionRegistration"].ToString()))
							CheckBox51.Checked = true;
						if(bool.Parse(reader["O15_QualityInspectionPC"].ToString()))
							CheckBox52.Checked = true;
						if(bool.Parse(reader["M01_BusinessPlanInfo"].ToString()))
							CheckBox53.Checked = true;
						if(bool.Parse(reader["M02_BusinessPlanInfoPC"].ToString()))
							CheckBox54.Checked = true;
						if(bool.Parse(reader["M03_ExecutionPlanInfo"].ToString()))
							CheckBox55.Checked = true;
						if(bool.Parse(reader["M04_ExecutionPlanInfoPC"].ToString()))
							CheckBox56.Checked = true;
						if(bool.Parse(reader["M05_OtherInOutStorehouse"].ToString()))
							CheckBox57.Checked = true;
						if(bool.Parse(reader["M06_OtherInOutStorehousePC"].ToString()))
							CheckBox58.Checked = true;
						if(bool.Parse(reader["M07_BizPlanResultIndex"].ToString()))
							CheckBox59.Checked = true;
						if(bool.Parse(reader["M08_SaleTatalProfitIndex"].ToString()))
							CheckBox60.Checked = true;
						if(bool.Parse(reader["M09_SaleIndex"].ToString()))
							CheckBox61.Checked = true;
						if(bool.Parse(reader["M10_BuyingIndex"].ToString()))
							CheckBox62.Checked = true;
						if(bool.Parse(reader["Q01_QualityInspectionStatistics"].ToString()))
							CheckBox63.Checked = true;
						if(bool.Parse(reader["Q02_IncongruityPresentIndex"].ToString()))
							CheckBox64.Checked = true;
						if(bool.Parse(reader["Q03_IncongruityCauseIndex"].ToString()))
							CheckBox65.Checked = true;
						if(bool.Parse(reader["Q04_AutonomyInspectPC"].ToString()))
							CheckBox66.Checked = true;
						if(bool.Parse(reader["Q05_AutonomyInspectStatistics"].ToString()))
							CheckBox67.Checked = true;
						if(bool.Parse(reader["Q06_AutonomyInspectIndex"].ToString()))
							CheckBox68.Checked = true;
						if(bool.Parse(reader["Q07_MoneyStandardIndex"].ToString()))
							CheckBox69.Checked = true;
						if(bool.Parse(reader["Q08_QuantityStandardIndex"].ToString()))
							CheckBox70.Checked = true;
						if(bool.Parse(reader["Q09_LotStandardIndex"].ToString()))
							CheckBox71.Checked = true;
						if(bool.Parse(reader["Q10_ItemsStockIndex"].ToString()))
							CheckBox72.Checked = true;
						if(bool.Parse(reader["Q11_StoreHouseStockIndex"].ToString()))
							CheckBox73.Checked = true;
						if(bool.Parse(reader["Q12_StockMoneyIndex"].ToString()))
							CheckBox74.Checked = true;
						if(bool.Parse(reader["Q13_WorkingRatioIndex"].ToString()))
							CheckBox75.Checked = true;
						if(bool.Parse(reader["Q14_NonWorkingRatioIndex"].ToString()))
							CheckBox76.Checked = true;
						if(bool.Parse(reader["Q15_ProductivityIndex"].ToString()))
							CheckBox77.Checked = true;
//						if(bool.Parse(reader["Q16_ProductionResultLook"].ToString()))
//							CheckBox78.Checked = true;
						if(bool.Parse(reader["B01_CompanyInfo"].ToString()))
							CheckBox79.Checked = true;
						if(bool.Parse(reader["B02_ItemInfo"].ToString()))
							CheckBox80.Checked = true;
						if(bool.Parse(reader["B03_ItemOrganizationInfo"].ToString()))
							CheckBox81.Checked = true;
						if(bool.Parse(reader["B04_WCInfo"].ToString()))
							CheckBox82.Checked = true;
						if(bool.Parse(reader["B05_ProcessSequenceInfo"].ToString()))
							CheckBox83.Checked = true;
						if(bool.Parse(reader["B06_EquipmentInfo"].ToString()))
							CheckBox84.Checked = true;
						if(bool.Parse(reader["B07_WorkStandardInfo"].ToString()))
							CheckBox85.Checked = true;
						if(bool.Parse(reader["B08_SaleUnitCodeInfo"].ToString()))
							CheckBox86.Checked = true;
						if(bool.Parse(reader["B09_OutSideOrderUnitCodeInfo"].ToString()))
							CheckBox87.Checked = true;
						if(bool.Parse(reader["B10_BuyingUnitCodeInfo"].ToString()))
							CheckBox88.Checked = true;
						if(bool.Parse(reader["B11_StandardProductionCalendarInfo"].ToString()))
							CheckBox89.Checked = true;
						if(bool.Parse(reader["B12_WCProductionCalendarInfo"].ToString()))
							CheckBox90.Checked = true;
						if(bool.Parse(reader["B13_UserInfo"].ToString()))
							CheckBox91.Checked = true;
						if(bool.Parse(reader["B14_PublicUseCode"].ToString()))
							CheckBox92.Checked = true;
						if(bool.Parse(reader["H01_OrderPC"].ToString()))
							CheckBox93.Checked = true;
						if(bool.Parse(reader["H02_DeliveryRequestPC"].ToString()))
							CheckBox94.Checked = true;
						if(bool.Parse(reader["H03_DeliveryPC"].ToString()))
							CheckBox95.Checked = true;
						if(bool.Parse(reader["H04_OutStorehousePC"].ToString()))
							CheckBox96.Checked = true;
						if(bool.Parse(reader["H05_StockPC"].ToString()))
							CheckBox97.Checked = true;
						if(bool.Parse(reader["H06_QualityPC"].ToString()))
							CheckBox98.Checked = true;
						if(bool.Parse(reader["H07_QualityIndex"].ToString()))
							CheckBox99.Checked = true;
						if(bool.Parse(reader["H08_SubNotice"].ToString()))
							CheckBox100.Checked = true;
						if(bool.Parse(reader["H09_SubFreeBoard"].ToString()))
							CheckBox101.Checked = true;
						if(bool.Parse(reader["H10_SubData"].ToString()))
							CheckBox102.Checked = true;
						if(bool.Parse(reader["C01_Notice"].ToString()))
							CheckBox103.Checked = true;
						if(bool.Parse(reader["C02_FreeBoard"].ToString()))
							CheckBox104.Checked = true;
						if(bool.Parse(reader["C03_GeneralData"].ToString()))
							CheckBox105.Checked = true;
						if(bool.Parse(reader["C04_TechnologyData"].ToString()))
							CheckBox106.Checked = true;
						if(bool.Parse(reader["C05_CompanyStandard"].ToString()))
							CheckBox107.Checked = true;
						if(bool.Parse(reader["C06_link"].ToString()))
							CheckBox108.Checked = true;
						if(bool.Parse(reader["C07_WorkDiary"].ToString()))
							CheckBox109.Checked = true;
						if(bool.Parse(reader["U01_UserCompanyInput"].ToString()))
							CheckBox110.Checked = true;
						if(bool.Parse(reader["U02_StandardInforbundleRegistration"].ToString()))
							CheckBox111.Checked = true;
						if(bool.Parse(reader["U03_SystemAbilitySet"].ToString()))
							CheckBox112.Checked = true;
						if(bool.Parse(reader["U04_UserAuthoritySet"].ToString()))
							CheckBox113.Checked = true;
						if(bool.Parse(reader["U05_DataBackUpRestoration"].ToString()))
							CheckBox114.Checked = true;
						if(bool.Parse(reader["U06_HistoryAdjustment"].ToString()))
							CheckBox115.Checked = true;
						foreach(ListItem item in ddlAuthority.Items)
						{
							if(item.Text == reader["Grade"].ToString())
							{
								item.Selected = true;
								break;
							}
						}
						ddlAuthority.Enabled = false;
						TextBox1.Text = reader["Explanation"].ToString();
					}
					reader.Close();
					conn.Close();
				}
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(TextBox1.Text != "")
			{
//				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\KIT_ERP");
//				SqlConnection conn = new SqlConnection(registryKey.GetValue("ConnectionStringSystem").ToString());
//				conn.Open();
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn.Open();

				SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
				string strSQL = "";
				string message = "";
				bool same = false;

				if(Request["id"].ToString() == "Add")
				{
					strSQL = @"SELECT COUNT(*) AS ROW  FROM PA_T WHERE Grade = @Grade";
					comm.Parameters.Add("@Grade", SqlDbType.VarChar).Value = ddlAuthority.SelectedItem.Text;
	
                    comm.CommandText = strSQL;
					if(int.Parse(comm.ExecuteScalar().ToString()) == 0)
					{
					
						strSQL = @"INSERT INTO PA_T (S01_ReceiveingRegistration, S02_ReceiveingbundleRegistration, S03_ReceiveingPC, S04_ReceiveingProgressLook,
								S05_ProductionRequest, S06_ProductionRequestPC, S07_ProductionRequestAdd, S08_GoodsBuyingRequest,
								S09_GoodsBuyingRequestPC, S10_GoodsBuyingRequestAdd, S11_BusinessStorehouseInStorehouse, S12_BusinessStorehouseInStorehousePC,
								S13_GoodsManufactureOutStorehouse, S14_GoodsManufactureOutStorehousePC, S15_SaleHistoryRegistration, S16_SaleHistoryRegistrationPC,
								S17_CollectMoneyRegistration, S18_CollectMoneyRegistrationPC, S19_UnCollectMoneyLook, S20_ClaimRegistration,
								S21_ClaimRegistrationPC, S22_StorehouseMoving, S23_StorehouseMovingPC,
								P01_ProductionPlan, P02_ProductionPlanPC, P03_ProductionPlanAdd, P04_RowMaterialRequirementCalculate,
								P05_RowMaterialRequirementCalculatePC, P06_RowMaterialRequriement, P07_RowMaterialRequriementPC,
								P08_RowMaterialRequriementAdd, P09_WorkPlan, P10_WCPlanPC, P11_WorkDailyReportRegistration, P12_WorkDailyReportRegistrationPC,
								P13_OutsideRequest, P14_OutsideRequestPC, 
								O01_BuyingOrder, O02_BuyingOrderPC, O03_BuyingOrderAdd, O04_BuyingDelivery, O05_BuyingDeliveryPC, O06_OutSideOrder,
								O07_OutSideOrderPC, O08_OutSideOutStorehouse, O09_OutSideOutStorehousePC, O10_OutSideDelivery, O11_OutSideDeliveryPC,
								O12_PaymentPlanResultRegistration, O13_PaymentPlanResultRegistrationPC, O14_QualityInspectionRegistration, O15_QualityInspectionPC,
								M01_BusinessPlanInfo, M02_BusinessPlanInfoPC, M03_ExecutionPlanInfo, M04_ExecutionPlanInfoPC, M05_OtherInOutStorehouse,
								M06_OtherInOutStorehousePC, M07_BizPlanResultIndex, M08_SaleTatalProfitIndex, M09_SaleIndex, M10_BuyingIndex,
								Q01_QualityInspectionStatistics, Q02_IncongruityPresentIndex, Q03_IncongruityCauseIndex, Q04_AutonomyInspectPC,
								Q05_AutonomyInspectStatistics, Q06_AutonomyInspectIndex, Q07_MoneyStandardIndex, Q08_QuantityStandardIndex,
								Q09_LotStandardIndex, Q10_ItemsStockIndex, Q11_StoreHouseStockIndex, Q12_StockMoneyIndex, Q13_WorkingRatioIndex,
								Q14_NonWorkingRatioIndex, Q15_ProductivityIndex, 
								B01_CompanyInfo, B02_ItemInfo, B03_ItemOrganizationInfo, B04_WCInfo, B05_ProcessSequenceInfo, B06_EquipmentInfo,
								B07_WorkStandardInfo, B08_SaleUnitCodeInfo, B09_OutSideOrderUnitCodeInfo, B10_BuyingUnitCodeInfo, B13_UserInfo,
								B14_PublicUseCode, B11_StandardProductionCalendarInfo, B12_WCProductionCalendarInfo, 
								H01_OrderPC, H02_DeliveryRequestPC, H03_DeliveryPC, H04_OutStorehousePC, H05_StockPC, H06_QualityPC, H07_QualityIndex,
								H08_SubNotice, H09_SubFreeBoard, H10_SubData,
								C01_Notice, C02_FreeBoard, C03_GeneralData, C04_TechnologyData, C05_CompanyStandard, C06_link, C07_WorkDiary,
								U01_UserCompanyInput, U02_StandardInforbundleRegistration, U03_SystemAbilitySet, U04_UserAuthoritySet, U05_DataBackUpRestoration, U06_HistoryAdjustment, Grade, Explanation)
								VALUES (@S01_ReceiveingRegistration, @S02_ReceiveingbundleRegistration, @S03_ReceiveingPC, @S04_ReceiveingProgressLook,
								@S05_ProductionRequest, @S06_ProductionRequestPC, @S07_ProductionRequestAdd, @S08_GoodsBuyingRequest,
								@S09_GoodsBuyingRequestPC, @S10_GoodsBuyingRequestAdd, @S11_BusinessStorehouseInStorehouse, @S12_BusinessStorehouseInStorehousePC,
								@S13_GoodsManufactureOutStorehouse, @S14_GoodsManufactureOutStorehousePC, @S15_SaleHistoryRegistration, @S16_SaleHistoryRegistrationPC,
								@S17_CollectMoneyRegistration, @S18_CollectMoneyRegistrationPC, @S19_UnCollectMoneyLook, @S20_ClaimRegistration,
								@S21_ClaimRegistrationPC, @S22_StorehouseMoving, @S23_StorehouseMovingPC,
								@P01_ProductionPlan, @P02_ProductionPlanPC, @P03_ProductionPlanAdd, @P04_RowMaterialRequirementCalculate,
								@P05_RowMaterialRequirementCalculatePC, @P06_RowMaterialRequriement, @P07_RowMaterialRequriementPC,
								@P08_RowMaterialRequriementAdd, @P09_WorkPlan, @P10_WCPlanPC, @P11_WorkDailyReportRegistration, @P12_WorkDailyReportRegistrationPC,
								@P13_OutsideRequest, @P14_OutsideRequestPC, 
								@O01_BuyingOrder, @O02_BuyingOrderPC, @O03_BuyingOrderAdd, @O04_BuyingDelivery, @O05_BuyingDeliveryPC, @O06_OutSideOrder,
								@O07_OutSideOrderPC, @O08_OutSideOutStorehouse, @O09_OutSideOutStorehousePC, @O10_OutSideDelivery, @O11_OutSideDeliveryPC,
								@O12_PaymentPlanResultRegistration, @O13_PaymentPlanResultRegistrationPC, @O14_QualityInspectionRegistration, @O15_QualityInspectionPC,
								@M01_BusinessPlanInfo, @M02_BusinessPlanInfoPC, @M03_ExecutionPlanInfo, @M04_ExecutionPlanInfoPC, @M05_OtherInOutStorehouse,
								@M06_OtherInOutStorehousePC, @M07_BizPlanResultIndex, @M08_SaleTatalProfitIndex, @M09_SaleIndex, @M10_BuyingIndex,
								@Q01_QualityInspectionStatistics, @Q02_IncongruityPresentIndex, @Q03_IncongruityCauseIndex, @Q04_AutonomyInspectPC,
								@Q05_AutonomyInspectStatistics, @Q06_AutonomyInspectIndex, @Q07_MoneyStandardIndex, @Q08_QuantityStandardIndex,
								@Q09_LotStandardIndex, @Q10_ItemsStockIndex, @Q11_StoreHouseStockIndex, @Q12_StockMoneyIndex, @Q13_WorkingRatioIndex,
								@Q14_NonWorkingRatioIndex, @Q15_ProductivityIndex, 
								@B01_CompanyInfo, @B02_ItemInfo, @B03_ItemOrganizationInfo, @B04_WCInfo, @B05_ProcessSequenceInfo, @B06_EquipmentInfo,
								@B07_WorkStandardInfo, @B08_SaleUnitCodeInfo, @B09_OutSideOrderUnitCodeInfo, @B10_BuyingUnitCodeInfo, @B13_UserInfo,
								@B14_PublicUseCode, @B11_StandardProductionCalendarInfo, @B12_WCProductionCalendarInfo, 
								@H01_OrderPC, @H02_DeliveryRequestPC, @H03_DeliveryPC, @H04_OutStorehousePC, @H05_StockPC, @H06_QualityPC, @H07_QualityIndex,
								@H08_SubNotice, @H09_SubFreeBoard, @H10_SubData,
								@C01_Notice, @C02_FreeBoard, @C03_GeneralData, @C04_TechnologyData, @C05_CompanyStandard, @C06_link, @C07_WorkDiary,
								@U01_UserCompanyInput, @U02_StandardInforbundleRegistration, @U03_SystemAbilitySet, @U04_UserAuthoritySet, @U05_DataBackUpRestoration, @U06_HistoryAdjustment, @Grade, @Explanation)";

						message = "등록하였습니다.";
					}
					else
					{
						same = true;
					}
					
				}
				else
				{
					strSQL = @"UPDATE PA_T SET S01_ReceiveingRegistration = @S01_ReceiveingRegistration,
							S02_ReceiveingbundleRegistration = @S02_ReceiveingbundleRegistration ,
							S03_ReceiveingPC = @S03_ReceiveingPC ,
							S04_ReceiveingProgressLook = @S04_ReceiveingProgressLook ,
							S05_ProductionRequest = @S05_ProductionRequest ,
							S06_ProductionRequestPC = @S06_ProductionRequestPC ,
							S07_ProductionRequestAdd = @S07_ProductionRequestAdd ,
							S08_GoodsBuyingRequest = @S08_GoodsBuyingRequest ,
							S09_GoodsBuyingRequestPC = @S09_GoodsBuyingRequestPC,
							S10_GoodsBuyingRequestAdd = @S10_GoodsBuyingRequestAdd ,
							S11_BusinessStorehouseInStorehouse = @S11_BusinessStorehouseInStorehouse ,
							S12_BusinessStorehouseInStorehousePC = @S12_BusinessStorehouseInStorehousePC ,
							S13_GoodsManufactureOutStorehouse = @S13_GoodsManufactureOutStorehouse ,
							S14_GoodsManufactureOutStorehousePC = @S14_GoodsManufactureOutStorehousePC ,
							S15_SaleHistoryRegistration = @S15_SaleHistoryRegistration ,
							S16_SaleHistoryRegistrationPC = @S16_SaleHistoryRegistrationPC ,
							S17_CollectMoneyRegistration = @S17_CollectMoneyRegistration ,
							S18_CollectMoneyRegistrationPC = @S18_CollectMoneyRegistrationPC ,
							S19_UnCollectMoneyLook = @S19_UnCollectMoneyLook ,
							S20_ClaimRegistration = @S20_ClaimRegistration ,
							S21_ClaimRegistrationPC = @S21_ClaimRegistrationPC ,
							S22_StorehouseMoving = @S22_StorehouseMoving ,
							S23_StorehouseMovingPC = @S23_StorehouseMovingPC ,
							P01_ProductionPlan = @P01_ProductionPlan ,
							P02_ProductionPlanPC = @P02_ProductionPlanPC ,
							P03_ProductionPlanAdd = @P03_ProductionPlanAdd ,
							P04_RowMaterialRequirementCalculate = @P04_RowMaterialRequirementCalculate ,
							P05_RowMaterialRequirementCalculatePC = @P05_RowMaterialRequirementCalculatePC ,
							P06_RowMaterialRequriement = @P06_RowMaterialRequriement ,
							P07_RowMaterialRequriementPC = @P07_RowMaterialRequriementPC ,
							P08_RowMaterialRequriementAdd = @P08_RowMaterialRequriementAdd ,
							P09_WorkPlan = @P09_WorkPlan ,
							P10_WCPlanPC = @P10_WCPlanPC ,
							P11_WorkDailyReportRegistration = @P11_WorkDailyReportRegistration ,
							P12_WorkDailyReportRegistrationPC = @P12_WorkDailyReportRegistrationPC ,
							P13_OutsideRequest = @P13_OutsideRequest ,
							P14_OutsideRequestPC = @P14_OutsideRequestPC ,
							O01_BuyingOrder = @O01_BuyingOrder ,
							O02_BuyingOrderPC = @O02_BuyingOrderPC ,
							O03_BuyingOrderAdd = @O03_BuyingOrderAdd ,
							O04_BuyingDelivery = @O04_BuyingDelivery ,
							O05_BuyingDeliveryPC = @O05_BuyingDeliveryPC ,
							O06_OutSideOrder = @O06_OutSideOrder, 
							O07_OutSideOrderPC = @O07_OutSideOrderPC ,
							O08_OutSideOutStorehouse = @O08_OutSideOutStorehouse, 
							O09_OutSideOutStorehousePC = @O09_OutSideOutStorehousePC, 
							O10_OutSideDelivery = @O10_OutSideDelivery, 
							O11_OutSideDeliveryPC = @O11_OutSideDeliveryPC, 
							O12_PaymentPlanResultRegistration = @O12_PaymentPlanResultRegistration ,
							O13_PaymentPlanResultRegistrationPC = @O13_PaymentPlanResultRegistrationPC ,
							O14_QualityInspectionRegistration = @O14_QualityInspectionRegistration ,
							O15_QualityInspectionPC = @O15_QualityInspectionPC ,
							M01_BusinessPlanInfo = @M01_BusinessPlanInfo ,
							M02_BusinessPlanInfoPC = @M02_BusinessPlanInfoPC ,
							M03_ExecutionPlanInfo = @M03_ExecutionPlanInfo ,
							M04_ExecutionPlanInfoPC = @M04_ExecutionPlanInfoPC ,
							M05_OtherInOutStorehouse = @M05_OtherInOutStorehouse ,
							M06_OtherInOutStorehousePC = @M06_OtherInOutStorehousePC ,
							M07_BizPlanResultIndex = @M07_BizPlanResultIndex ,
							M08_SaleTatalProfitIndex = @M08_SaleTatalProfitIndex ,
							M09_SaleIndex = @M09_SaleIndex ,
							M10_BuyingIndex = @M10_BuyingIndex ,
							Q01_QualityInspectionStatistics = @Q01_QualityInspectionStatistics ,
							Q02_IncongruityPresentIndex = @Q02_IncongruityPresentIndex, 
							Q03_IncongruityCauseIndex = @Q03_IncongruityCauseIndex ,
							Q04_AutonomyInspectPC = @Q04_AutonomyInspectPC,
							Q05_AutonomyInspectStatistics = @Q05_AutonomyInspectStatistics, 
							Q06_AutonomyInspectIndex = @Q06_AutonomyInspectIndex,
							Q07_MoneyStandardIndex = @Q07_MoneyStandardIndex,
							Q08_QuantityStandardIndex = @Q08_QuantityStandardIndex,
							Q09_LotStandardIndex = @Q09_LotStandardIndex,
							Q10_ItemsStockIndex = @Q10_ItemsStockIndex,
							Q11_StoreHouseStockIndex = @Q11_StoreHouseStockIndex,
							Q12_StockMoneyIndex = @Q12_StockMoneyIndex, 
							Q13_WorkingRatioIndex = @Q13_WorkingRatioIndex, 
							Q14_NonWorkingRatioIndex = @Q14_NonWorkingRatioIndex, 
							Q15_ProductivityIndex = @Q15_ProductivityIndex, 
							
							B01_CompanyInfo = @B01_CompanyInfo ,
							B02_ItemInfo = @B02_ItemInfo ,
							B03_ItemOrganizationInfo = @B03_ItemOrganizationInfo ,
							B04_WCInfo = @B04_WCInfo ,
							B05_ProcessSequenceInfo = @B05_ProcessSequenceInfo ,
							B06_EquipmentInfo = @B06_EquipmentInfo ,
							B07_WorkStandardInfo = @B07_WorkStandardInfo ,
							B08_SaleUnitCodeInfo = @B08_SaleUnitCodeInfo ,
							B09_OutSideOrderUnitCodeInfo = @B09_OutSideOrderUnitCodeInfo ,
							B10_BuyingUnitCodeInfo = @B10_BuyingUnitCodeInfo ,
							B13_UserInfo = @B13_UserInfo ,
							B14_PublicUseCode = @B14_PublicUseCode ,
							B11_StandardProductionCalendarInfo = @B11_StandardProductionCalendarInfo ,
							B12_WCProductionCalendarInfo = @B12_WCProductionCalendarInfo ,
							H01_OrderPC = @H01_OrderPC ,
							H02_DeliveryRequestPC = @H02_DeliveryRequestPC ,
							H03_DeliveryPC = @H03_DeliveryPC ,
							H04_OutStorehousePC = @H04_OutStorehousePC ,
							H05_StockPC = @H05_StockPC ,
							H06_QualityPC = @H06_QualityPC ,
							H07_QualityIndex = @H07_QualityIndex ,
							H08_SubNotice = @H08_SubNotice ,
							H09_SubFreeBoard = @H09_SubFreeBoard ,
							H10_SubData = @H10_SubData ,
							C01_Notice = @C01_Notice ,
							C02_FreeBoard = @C02_FreeBoard ,
							C03_GeneralData = @C03_GeneralData ,
							C04_TechnologyData = @C04_TechnologyData ,
							C05_CompanyStandard = @C05_CompanyStandard, 
							C06_link = @C06_link ,
							C07_WorkDiary = @C07_WorkDiary ,
							U01_UserCompanyInput = @U01_UserCompanyInput ,
							U02_StandardInforbundleRegistration = @U02_StandardInforbundleRegistration ,
							U03_SystemAbilitySet = @U03_SystemAbilitySet ,
							U04_UserAuthoritySet = @U04_UserAuthoritySet ,
							U05_DataBackUpRestoration = @U05_DataBackUpRestoration ,
							U06_HistoryAdjustment = @U06_HistoryAdjustment , Explanation = @Explanation WHERE PageAuthorityIndex = @PageAuthorityIndex";

					message = "수정하였습니다.";
				}
				comm.Parameters.Add("@S01_ReceiveingRegistration", SqlDbType.Bit).Value = CheckBox1.Checked;
				comm.Parameters.Add("@S02_ReceiveingbundleRegistration", SqlDbType.Bit).Value = CheckBox2.Checked;
				comm.Parameters.Add("@S03_ReceiveingPC", SqlDbType.Bit).Value = CheckBox3.Checked;
				comm.Parameters.Add("@S04_ReceiveingProgressLook", SqlDbType.Bit).Value = CheckBox4.Checked;
				comm.Parameters.Add("@S05_ProductionRequest", SqlDbType.Bit).Value = CheckBox5.Checked;
				comm.Parameters.Add("@S06_ProductionRequestPC", SqlDbType.Bit).Value = CheckBox6.Checked;
				comm.Parameters.Add("@S07_ProductionRequestAdd", SqlDbType.Bit).Value = CheckBox7.Checked;
				comm.Parameters.Add("@S08_GoodsBuyingRequest", SqlDbType.Bit).Value = CheckBox8.Checked;
				comm.Parameters.Add("@S09_GoodsBuyingRequestPC", SqlDbType.Bit).Value = CheckBox9.Checked;
				comm.Parameters.Add("@S10_GoodsBuyingRequestAdd", SqlDbType.Bit).Value = CheckBox10.Checked;
				comm.Parameters.Add("@S11_BusinessStorehouseInStorehouse", SqlDbType.Bit).Value = CheckBox11.Checked;
				comm.Parameters.Add("@S12_BusinessStorehouseInStorehousePC", SqlDbType.Bit).Value = CheckBox12.Checked;
				comm.Parameters.Add("@S13_GoodsManufactureOutStorehouse", SqlDbType.Bit).Value = CheckBox13.Checked;
				comm.Parameters.Add("@S14_GoodsManufactureOutStorehousePC", SqlDbType.Bit).Value = CheckBox14.Checked;
				comm.Parameters.Add("@S15_SaleHistoryRegistration", SqlDbType.Bit).Value = CheckBox15.Checked;
				comm.Parameters.Add("@S16_SaleHistoryRegistrationPC", SqlDbType.Bit).Value = CheckBox16.Checked;
				comm.Parameters.Add("@S17_CollectMoneyRegistration", SqlDbType.Bit).Value = CheckBox17.Checked;
				comm.Parameters.Add("@S18_CollectMoneyRegistrationPC", SqlDbType.Bit).Value = CheckBox18.Checked;
				comm.Parameters.Add("@S19_UnCollectMoneyLook", SqlDbType.Bit).Value = CheckBox19.Checked;
				comm.Parameters.Add("@S20_ClaimRegistration", SqlDbType.Bit).Value = CheckBox20.Checked;
				comm.Parameters.Add("@S21_ClaimRegistrationPC", SqlDbType.Bit).Value = CheckBox21.Checked;
				comm.Parameters.Add("@S22_StorehouseMoving", SqlDbType.Bit).Value = CheckBox22.Checked;
				comm.Parameters.Add("@S23_StorehouseMovingPC", SqlDbType.Bit).Value = CheckBox23.Checked;
				comm.Parameters.Add("@P01_ProductionPlan", SqlDbType.Bit).Value = CheckBox24.Checked;
				comm.Parameters.Add("@P02_ProductionPlanPC", SqlDbType.Bit).Value = CheckBox25.Checked;
				comm.Parameters.Add("@P03_ProductionPlanAdd", SqlDbType.Bit).Value = CheckBox26.Checked;
				comm.Parameters.Add("@P04_RowMaterialRequirementCalculate", SqlDbType.Bit).Value = CheckBox27.Checked;
				comm.Parameters.Add("@P05_RowMaterialRequirementCalculatePC", SqlDbType.Bit).Value = CheckBox28.Checked;
				comm.Parameters.Add("@P06_RowMaterialRequriement", SqlDbType.Bit).Value = CheckBox29.Checked;
				comm.Parameters.Add("@P07_RowMaterialRequriementPC", SqlDbType.Bit).Value = CheckBox30.Checked;
				comm.Parameters.Add("@P08_RowMaterialRequriementAdd", SqlDbType.Bit).Value = CheckBox31.Checked;
				comm.Parameters.Add("@P09_WorkPlan", SqlDbType.Bit).Value = CheckBox32.Checked;
				comm.Parameters.Add("@P10_WCPlanPC", SqlDbType.Bit).Value = CheckBox33.Checked;
				comm.Parameters.Add("@P11_WorkDailyReportRegistration", SqlDbType.Bit).Value = CheckBox34.Checked;
				comm.Parameters.Add("@P12_WorkDailyReportRegistrationPC", SqlDbType.Bit).Value = CheckBox35.Checked;
				comm.Parameters.Add("@P13_OutsideRequest", SqlDbType.Bit).Value = CheckBox36.Checked;
				comm.Parameters.Add("@P14_OutsideRequestPC", SqlDbType.Bit).Value = CheckBox37.Checked;
				comm.Parameters.Add("@O01_BuyingOrder", SqlDbType.Bit).Value = CheckBox38.Checked;
				comm.Parameters.Add("@O02_BuyingOrderPC", SqlDbType.Bit).Value = CheckBox39.Checked;
				comm.Parameters.Add("@O03_BuyingOrderAdd", SqlDbType.Bit).Value = CheckBox40.Checked;
				comm.Parameters.Add("@O04_BuyingDelivery", SqlDbType.Bit).Value = CheckBox41.Checked;
				comm.Parameters.Add("@O05_BuyingDeliveryPC", SqlDbType.Bit).Value = CheckBox42.Checked;
				comm.Parameters.Add("@O06_OutSideOrder", SqlDbType.Bit).Value = CheckBox43.Checked;
				comm.Parameters.Add("@O07_OutSideOrderPC", SqlDbType.Bit).Value = CheckBox44.Checked;
				comm.Parameters.Add("@O08_OutSideOutStorehouse", SqlDbType.Bit).Value = CheckBox45.Checked;
				comm.Parameters.Add("@O09_OutSideOutStorehousePC", SqlDbType.Bit).Value = CheckBox46.Checked;
				comm.Parameters.Add("@O10_OutSideDelivery", SqlDbType.Bit).Value = CheckBox47.Checked;
				comm.Parameters.Add("@O11_OutSideDeliveryPC", SqlDbType.Bit).Value = CheckBox48.Checked;
				comm.Parameters.Add("@O12_PaymentPlanResultRegistration", SqlDbType.Bit).Value = CheckBox49.Checked;
				comm.Parameters.Add("@O13_PaymentPlanResultRegistrationPC", SqlDbType.Bit).Value = CheckBox50.Checked;
				comm.Parameters.Add("@O14_QualityInspectionRegistration", SqlDbType.Bit).Value = CheckBox51.Checked;
				comm.Parameters.Add("@O15_QualityInspectionPC", SqlDbType.Bit).Value = CheckBox52.Checked;
				comm.Parameters.Add("@M01_BusinessPlanInfo", SqlDbType.Bit).Value = CheckBox53.Checked;
				comm.Parameters.Add("@M02_BusinessPlanInfoPC", SqlDbType.Bit).Value = CheckBox54.Checked;
				comm.Parameters.Add("@M03_ExecutionPlanInfo", SqlDbType.Bit).Value = CheckBox55.Checked;
				comm.Parameters.Add("@M04_ExecutionPlanInfoPC", SqlDbType.Bit).Value = CheckBox56.Checked;
				comm.Parameters.Add("@M05_OtherInOutStorehouse", SqlDbType.Bit).Value = CheckBox57.Checked;
				comm.Parameters.Add("@M06_OtherInOutStorehousePC", SqlDbType.Bit).Value = CheckBox58.Checked;
				comm.Parameters.Add("@M07_BizPlanResultIndex", SqlDbType.Bit).Value = CheckBox59.Checked;
				comm.Parameters.Add("@M08_SaleTatalProfitIndex", SqlDbType.Bit).Value = CheckBox60.Checked;
				comm.Parameters.Add("@M09_SaleIndex", SqlDbType.Bit).Value = CheckBox61.Checked;
				comm.Parameters.Add("@M10_BuyingIndex", SqlDbType.Bit).Value = CheckBox62.Checked;
				comm.Parameters.Add("@Q01_QualityInspectionStatistics", SqlDbType.Bit).Value = CheckBox63.Checked;
				comm.Parameters.Add("@Q02_IncongruityPresentIndex", SqlDbType.Bit).Value = CheckBox64.Checked;
				comm.Parameters.Add("@Q03_IncongruityCauseIndex", SqlDbType.Bit).Value = CheckBox65.Checked;
				comm.Parameters.Add("@Q04_AutonomyInspectPC", SqlDbType.Bit).Value = CheckBox66.Checked;
				comm.Parameters.Add("@Q05_AutonomyInspectStatistics", SqlDbType.Bit).Value = CheckBox67.Checked;
				comm.Parameters.Add("@Q06_AutonomyInspectIndex", SqlDbType.Bit).Value = CheckBox68.Checked;
				comm.Parameters.Add("@Q07_MoneyStandardIndex", SqlDbType.Bit).Value = CheckBox69.Checked;
				comm.Parameters.Add("@Q08_QuantityStandardIndex", SqlDbType.Bit).Value = CheckBox70.Checked;
				comm.Parameters.Add("@Q09_LotStandardIndex", SqlDbType.Bit).Value = CheckBox71.Checked;
				comm.Parameters.Add("@Q10_ItemsStockIndex", SqlDbType.Bit).Value = CheckBox72.Checked;
				comm.Parameters.Add("@Q11_StoreHouseStockIndex", SqlDbType.Bit).Value = CheckBox73.Checked;
				comm.Parameters.Add("@Q12_StockMoneyIndex", SqlDbType.Bit).Value = CheckBox74.Checked;
				comm.Parameters.Add("@Q13_WorkingRatioIndex", SqlDbType.Bit).Value = CheckBox75.Checked;
				comm.Parameters.Add("@Q14_NonWorkingRatioIndex", SqlDbType.Bit).Value = CheckBox76.Checked;
				comm.Parameters.Add("@Q15_ProductivityIndex", SqlDbType.Bit).Value = CheckBox77.Checked;
				//comm.Parameters.Add("@Q16_ProductionResultLook", SqlDbType.Bit).Value = CheckBox78.Checked;
				comm.Parameters.Add("@B01_CompanyInfo", SqlDbType.Bit).Value = CheckBox79.Checked;
				comm.Parameters.Add("@B02_ItemInfo", SqlDbType.Bit).Value = CheckBox80.Checked;
				comm.Parameters.Add("@B03_ItemOrganizationInfo", SqlDbType.Bit).Value = CheckBox81.Checked;
				comm.Parameters.Add("@B04_WCInfo", SqlDbType.Bit).Value = CheckBox82.Checked;
				comm.Parameters.Add("@B05_ProcessSequenceInfo", SqlDbType.Bit).Value = CheckBox83.Checked;
				comm.Parameters.Add("@B06_EquipmentInfo", SqlDbType.Bit).Value = CheckBox84.Checked;
				comm.Parameters.Add("@B07_WorkStandardInfo", SqlDbType.Bit).Value = CheckBox85.Checked;
				comm.Parameters.Add("@B08_SaleUnitCodeInfo", SqlDbType.Bit).Value = CheckBox86.Checked;
				comm.Parameters.Add("@B09_OutSideOrderUnitCodeInfo", SqlDbType.Bit).Value = CheckBox87.Checked;
				comm.Parameters.Add("@B10_BuyingUnitCodeInfo", SqlDbType.Bit).Value = CheckBox88.Checked;
				comm.Parameters.Add("@B11_StandardProductionCalendarInfo", SqlDbType.Bit).Value = CheckBox89.Checked;
				comm.Parameters.Add("@B12_WCProductionCalendarInfo", SqlDbType.Bit).Value = CheckBox90.Checked;
				comm.Parameters.Add("@B13_UserInfo", SqlDbType.Bit).Value = CheckBox91.Checked;
				comm.Parameters.Add("@B14_PublicUseCode", SqlDbType.Bit).Value = CheckBox92.Checked;
				comm.Parameters.Add("@H01_OrderPC", SqlDbType.Bit).Value = CheckBox93.Checked;
				comm.Parameters.Add("@H02_DeliveryRequestPC", SqlDbType.Bit).Value = CheckBox94.Checked;
				comm.Parameters.Add("@H03_DeliveryPC", SqlDbType.Bit).Value = CheckBox95.Checked;
				comm.Parameters.Add("@H04_OutStorehousePC", SqlDbType.Bit).Value = CheckBox96.Checked;
				comm.Parameters.Add("@H05_StockPC", SqlDbType.Bit).Value = CheckBox97.Checked;
				comm.Parameters.Add("@H06_QualityPC", SqlDbType.Bit).Value = CheckBox98.Checked;
				comm.Parameters.Add("@H07_QualityIndex", SqlDbType.Bit).Value = CheckBox99.Checked;
				comm.Parameters.Add("@H08_SubNotice", SqlDbType.Bit).Value = CheckBox100.Checked;
				comm.Parameters.Add("@H09_SubFreeBoard", SqlDbType.Bit).Value = CheckBox101.Checked;
				comm.Parameters.Add("@H10_SubData", SqlDbType.Bit).Value = CheckBox102.Checked;
				comm.Parameters.Add("@C01_Notice", SqlDbType.Bit).Value = CheckBox103.Checked;
				comm.Parameters.Add("@C02_FreeBoard", SqlDbType.Bit).Value = CheckBox104.Checked;
				comm.Parameters.Add("@C03_GeneralData", SqlDbType.Bit).Value = CheckBox105.Checked;
				comm.Parameters.Add("@C04_TechnologyData", SqlDbType.Bit).Value = CheckBox106.Checked;
				comm.Parameters.Add("@C05_CompanyStandard", SqlDbType.Bit).Value = CheckBox107.Checked;
				comm.Parameters.Add("@C06_link", SqlDbType.Bit).Value = CheckBox108.Checked;
				comm.Parameters.Add("@C07_WorkDiary", SqlDbType.Bit).Value = CheckBox109.Checked;
				comm.Parameters.Add("@U01_UserCompanyInput", SqlDbType.Bit).Value = CheckBox110.Checked;
				comm.Parameters.Add("@U02_StandardInforbundleRegistration", SqlDbType.Bit).Value = CheckBox111.Checked;
				comm.Parameters.Add("@U03_SystemAbilitySet", SqlDbType.Bit).Value = CheckBox112.Checked;
				comm.Parameters.Add("@U04_UserAuthoritySet", SqlDbType.Bit).Value = CheckBox113.Checked;
				comm.Parameters.Add("@U05_DataBackUpRestoration", SqlDbType.Bit).Value = CheckBox114.Checked;
				comm.Parameters.Add("@U06_HistoryAdjustment", SqlDbType.Bit).Value = CheckBox115.Checked;
				
				comm.Parameters.Add("@Explanation", SqlDbType.VarChar).Value = TextBox1.Text;
				if(Request["id"].ToString() == "UpDate")
					comm.Parameters.Add("@PageAuthorityIndex", SqlDbType.Int).Value = int.Parse(Request["no"].ToString());

				if((Request["id"].ToString() == "Add") && (same))
				{
					RegisterStartupScript("","<script>alert('이미 같은 권한이 존재합니다.');</script>");
					//Response.Write("<script>alert('이미 같은 권한이 존재합니다.');</script>");
				}
				else
				{
					comm.CommandText = strSQL;
					comm.ExecuteNonQuery();
					RegisterStartupScript("","<script>alert('" + message.ToString() + "');window.open('../UserAuthoritySet.aspx', 'Contents','');window.close();</script>");
					//Response.Write("<script>alert('" + message.ToString() + "');window.open('../SystemAbilitySet.aspx', 'Contents','');window.close();</script>");
				}
				conn.Close();
			}
			else
			{
				RegisterStartupScript("","<script>alert('권한 설명이 입력되지 않았습니다.');</script>");
			}
		}

	}
}
