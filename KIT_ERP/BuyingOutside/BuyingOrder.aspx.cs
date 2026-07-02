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
	/// BuyingOrder1에 대한 요약 설명입니다.
	/// </summary>
	public class BuyingOrder : System.Web.UI.Page
	{
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMaxDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMinDate;
		protected System.Web.UI.WebControls.Button btnRegistration;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgBO_HT;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected System.Web.UI.WebControls.Button btnSearch;
		DataSet dsBR = new DataSet();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			ItemSearchControl1.RawMaterials = true;
			ItemSearchControl1.Commodity = true;	//상품 바인딩
			CSC1.UnitCostDistinction = "구매거래처";

			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			
			if(!Page.IsPostBack)
			{
				//발주버튼을 클릭했을때 메세지창
				//btnRegistration.Attributes.Add("onclick","return Confirm('선택한 품목들의 대하여 발주를 하시겠습니까?');");
				//btnRegistration.Attributes.Add("onClick","return OK('선택한 품목들의 대하여 발주를 '))");
				btnRegistration.Attributes.Add("onclick","return OkCancel();");
				
				Session["dsBR"] = dsBR;

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
				
				ddlItemClassification1.DataSource = ds_ItemClassification1;
				ddlItemClassification1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
				ddlItemClassification1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
				ddlItemClassification1.DataBind();
				ddlItemClassification1.Items.Insert(0, "-선택하세요-") ;
				ddlItemClassification1.Items[0].Value = "";

				
			}				
		
			else
			{	
				dsBR = (DataSet)Session["dsBR"];
			}

		}


		private void uwgBO_HT_DataBinding(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlDataAdapter BuyingRequestAdapter1 = new SqlDataAdapter(@"SELECT BR_HT.ItemNum, BR_HT.ItemDrawNum, BR_HT.ItemName, BR_HT.PropertyClassification, P1.SmallClassificationName as Unit,  II_MT.Standard, CI_MT.CompanyName, BuyingRequestSourceCode, BuyingRequestSource, FirstDeliveryDemandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, OrderQuantity, UCI_MT.StandardUnitCost as ApplyUnitCost,(UCI_MT.StandardUnitCost * FirstDeliveryDemandQuantity) as TotalCost, VolumNum, ProgressCondition, BR_HT.RegistrationPerson, BR_HT.RegistrationPersonID, BR_HT.RegistrationDate, BR_HT.UpdatingPerson, BR_HT.UpdatingPersonID, BR_HT.UpdatingDate, HistoryIndex, HistorySection, BuyingRequestHistoryIndex FROM BR_HT
				Inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum 
				Inner join PUC_MT P on II_MT.ItemClassification1 = P.SmallClassificationCode
				Inner join PUC_MT P1 on II_MT.Unit = P1.SmallClassificationCode
				inner join UCI_MT on II_MT.ItemNum = UCI_MT.ItemNum
				inner join CI_MT on UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
				WHERE II_MT.RecodingState = 1 and P.RecodingState = 1 and BR_HT.ItemNum like @num and BR_HT.ItemDrawNum like @draw and BR_HT.ItemName like @name 
				and UCI_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '구매단가' 				
				and ItemClassification1 Like @ItemClassification   and (FirstDeliveryDemandDate >= @begin and FirstDeliveryDemandDate <= @end) and (ProgressCondition = '대기') 
				and UCI_MT.BusinessRegistrationNum = (Select Top 1 BusinessRegistrationNum From UCI_MT where UCI_MT.ItemNum = BR_HT.ItemNum and UnitCostDistinction = '구매단가' and UCI_MT.StandardUnitCost = (Select Min(StandardUnitCost) From UCI_MT where UCI_MT.ItemNum = BR_HT.ItemNum and  UnitCostDistinction = '구매단가' and UCI_MT.RecodingState = 1) and UCI_MT.RecodingState = 1)
				and CI_MT.CompanyName Like @CompanyName and CI_MT.BusinessRegistrationNum Like @BusinessRegistrationNum order by CI_MT.CompanyName, FirstDeliveryDemandDate, BuyingRequestHistoryIndex desc
				 SELECT BO_HT.ItemNum, BO_HT.ItemDrawNum, BO_HT.ItemName, SmallClassificationCode as Unit,  BO_HT.CompanyName, BO_HT.BusinessRegistrationNum, BO_HT.FirstDeliveryDemandQuantity, BO_HT.FirstDeliveryDemandDate, 
				BO_HT.SecondDeliveryDemandQuantity, BO_HT.SecondDeliveryDemandDate, BO_HT.ThirdDeliveryDemandQuantity, BO_HT.ThirdDeliveryDemandDate, BO_HT.FourthDeliveryDemandQuantity, BO_HT.FourthDeliveryDemandDate, BO_HT.FifthDeliveryDemandQuantity, BO_HT.FifthDeliveryDemandDate, BO_HT.OrderQuantity, BO_HT.ApplyUnitCost, BO_HT.TotalCost, BO_HT.OrderRate, BO_HT.RemainQuantity, BO_HT.VolumNum, BO_HT.ProgressCondition, BO_HT.RegistrationPerson, BO_HT.RegistrationPersonID, BO_HT.RegistrationDate, BO_HT.UpdatingPerson, BO_HT.UpdatingPersonID, BO_HT.UpdatingDate, BO_HT.BuyingRequestHistoryIndex, BO_HT.BuyingOrderHistoryIndex FROM BO_HT inner join II_MT on BO_HT.ItemNum = II_MT.ItemNum Inner join PUC_MT P on II_MT.Unit = P.SmallClassificationCode WHERE (BO_HT.ItemNum IS NULL)",conn);
//			SqlDataAdapter BuyingRequestAdapter1 = new SqlDataAdapter(@"SELECT BR_HT.ItemNum, BR_HT.ItemDrawNum, BR_HT.ItemName, BR_HT.PropertyClassification, P1.SmallClassificationName as Unit,  II_MT.Standard, CI_MT.CompanyName, BuyingRequestSourceCode, BuyingRequestSource, FirstDeliveryDemandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, OrderQuantity, UCI_MT.StandardUnitCost as ApplyUnitCost,(UCI_MT.StandardUnitCost * FirstDeliveryDemandQuantity) as TotalCost, VolumNum, ProgressCondition, BR_HT.RegistrationPerson, BR_HT.RegistrationPersonID, BR_HT.RegistrationDate, BR_HT.UpdatingPerson, BR_HT.UpdatingPersonID, BR_HT.UpdatingDate, HistoryIndex, HistorySection, BuyingRequestHistoryIndex FROM BR_HT
//				Inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum 
//				Inner join PUC_MT P on II_MT.ItemClassification1 = P.SmallClassificationCode
//				Inner join PUC_MT P1 on II_MT.Unit = P1.SmallClassificationCode
//				inner join UCI_MT on II_MT.ItemNum = UCI_MT.ItemNum
//				inner join CI_MT on UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
//				WHERE II_MT.RecodingState = 1 and P.RecodingState = 1 and BR_HT.ItemNum like @num and BR_HT.ItemDrawNum like @draw and BR_HT.ItemName like @name 
//				and UCI_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '구매단가' 				
//				and ItemClassification1 Like @ItemClassification   and (FirstDeliveryDemandDate >= @begin and FirstDeliveryDemandDate <= @end) and (ProgressCondition = '대기') 
//				and UCI_MT.OrderRate = (Select Max(OrderRate) From UCI_MT where BR_HT.ItemNum like @num and BR_HT.ItemDrawNum like @draw and BR_HT.ItemName like @name and UnitCostDistinction = '구매단가')
//				and CI_MT.CompanyName Like @CompanyName and CI_MT.BusinessRegistrationNum Like @BusinessRegistrationNum order by CI_MT.CompanyName, FirstDeliveryDemandDate, BuyingRequestHistoryIndex 
//				 SELECT BO_HT.ItemNum, BO_HT.ItemDrawNum, BO_HT.ItemName, SmallClassificationCode as Unit,  BO_HT.CompanyName, BO_HT.BusinessRegistrationNum, BO_HT.FirstDeliveryDemandQuantity, BO_HT.FirstDeliveryDemandDate, 
//				BO_HT.SecondDeliveryDemandQuantity, BO_HT.SecondDeliveryDemandDate, BO_HT.ThirdDeliveryDemandQuantity, BO_HT.ThirdDeliveryDemandDate, BO_HT.FourthDeliveryDemandQuantity, BO_HT.FourthDeliveryDemandDate, BO_HT.FifthDeliveryDemandQuantity, BO_HT.FifthDeliveryDemandDate, BO_HT.OrderQuantity, BO_HT.ApplyUnitCost, BO_HT.TotalCost, BO_HT.OrderRate, BO_HT.RemainQuantity, BO_HT.VolumNum, BO_HT.ProgressCondition, BO_HT.RegistrationPerson, BO_HT.RegistrationPersonID, BO_HT.RegistrationDate, BO_HT.UpdatingPerson, BO_HT.UpdatingPersonID, BO_HT.UpdatingDate, BO_HT.BuyingRequestHistoryIndex, BO_HT.BuyingOrderHistoryIndex FROM BO_HT inner join II_MT on BO_HT.ItemNum = II_MT.ItemNum Inner join PUC_MT P on II_MT.Unit = P.SmallClassificationCode WHERE (BO_HT.ItemNum IS NULL)",conn);
			//SqlDataAdapter BuyingRequestAdapter1 = new SqlDataAdapter(@"SELECT ItemNum, ItemDrawNum, ItemName, PropertyClassification, BuyingRequestSourceCode, BuyingRequestSource, FirstDeliveryDemandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, OrderQuantity, ApplyUnitCost, TotalCost, VolumNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, HistoryIndex, HistorySection, BuyingRequestHistoryIndex FROM BR_HT WHERE ItemNum like @num and ItemDrawNum like @draw and ItemName like @name and (RegistrationDate >= @begin and RegistrationDate <= @end) and (ProgressCondition = '대기')"+
			//	"SELECT ItemNum, ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, FirstDeliveryDemandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, OrderQuantity, ApplyUnitCost, TotalCost, OrderRate, RemainQuantity, VolumNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, BuyingRequestHistoryIndex, BuyingOrderHistoryIndex FROM BO_HT WHERE (ItemNum IS NULL)",conn);


			BuyingRequestAdapter1.SelectCommand.Parameters.Add("@num",SqlDbType.VarChar).Value = "%"+ItemSearchControl1.ItemNum+"%";
			if(ItemSearchControl1.hdItem.Trim() == "")
			{
				BuyingRequestAdapter1.SelectCommand.Parameters.Add("@draw",SqlDbType.VarChar).Value = "%"+ItemSearchControl1.ItemDrawNum+"%";
				BuyingRequestAdapter1.SelectCommand.Parameters.Add("@name",SqlDbType.VarChar).Value = "%"+ItemSearchControl1.ItemName+"%";
			}
			else
			{
				BuyingRequestAdapter1.SelectCommand.Parameters.Add("@draw",SqlDbType.VarChar).Value = "%%";
				BuyingRequestAdapter1.SelectCommand.Parameters.Add("@name",SqlDbType.VarChar).Value = "%%";
			}
			BuyingRequestAdapter1.SelectCommand.Parameters.Add("@CompanyName",SqlDbType.VarChar).Value = "%"+CSC1.Company+"%";
			BuyingRequestAdapter1.SelectCommand.Parameters.Add("@BusinessRegistrationNum",SqlDbType.VarChar).Value = "%"+CSC1.BusinessRegistrationNum+"%";
			if(ddlItemClassification1.SelectedIndex == 0)
				BuyingRequestAdapter1.SelectCommand.Parameters.Add("@ItemClassification" ,"%%");
			else
				BuyingRequestAdapter1.SelectCommand.Parameters.Add("@ItemClassification" ,"%"+ddlItemClassification1.SelectedItem.Value+"%");
			if(wdcMinDate.Text.Trim()=="")
				BuyingRequestAdapter1.SelectCommand.Parameters.Add("@begin",SqlDbType.SmallDateTime).Value = DateTime.Parse("1900-01-01").ToShortDateString();
			else
                BuyingRequestAdapter1.SelectCommand.Parameters.Add("@begin",SqlDbType.SmallDateTime).Value = DateTime.Parse(wdcMinDate.Value.ToString()).ToShortDateString();
			if(wdcMaxDate.Text.Trim()=="")
				BuyingRequestAdapter1.SelectCommand.Parameters.Add("@end",SqlDbType.SmallDateTime).Value = DateTime.Parse("2076-06-06").ToShortDateString();
			else
				BuyingRequestAdapter1.SelectCommand.Parameters.Add("@end",SqlDbType.SmallDateTime).Value = DateTime.Parse(wdcMaxDate.Value.ToString()).ToShortDateString();

			BuyingRequestAdapter1.Fill(dsBR);

			int drRowCount = 0;
			foreach(DataRow dr in dsBR.Tables[0].Rows)
			{
				drRowCount++;

				string strSQL = "SELECT * from UCI_MT, CI_MT,II_MT where UCI_MT.RecodingState = 1 and CI_MT.RecodingState =1 and II_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '구매단가' and UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum AND II_MT.ItemNum = UCI_MT.ItemNum AND UCI_MT.ItemNum = '" + dr["ItemNum"] + "' and UCI_MT.OrderRate != 0 order by UCI_MT.OrderRate desc";
		
				DataSet dsUCI_CI_IIJoin= new DataSet();	
				SqlDataAdapter UCI_CI_IIJoinAdapter = new SqlDataAdapter(strSQL,conn);
				UCI_CI_IIJoinAdapter.Fill(dsUCI_CI_IIJoin);
				
				DataTable table = dsBR.Tables[1];

				for(int count = 0;count <= dsUCI_CI_IIJoin.Tables[0].Rows.Count - 1;count++)
				{					
					
					//float orderRate = 0;
					Decimal first = 0, second = 0, third = 0, fourth = 0, fifth = 0;

					//만들어진 row를 dsBR에 추가함.

					DataRow row = table.NewRow();

					row["ItemNum"] = dsUCI_CI_IIJoin.Tables[0].Rows[count]["ItemNum"].ToString();
					row["ItemDrawNum"] = dsUCI_CI_IIJoin.Tables[0].Rows[count]["ItemDrawNum"].ToString();
					row["ItemName"] = dsUCI_CI_IIJoin.Tables[0].Rows[count]["ItemName"].ToString();
				
					row["CompanyName"] = dsUCI_CI_IIJoin.Tables[0].Rows[count]["CompanyName"].ToString();
					row["BusinessRegistrationNum"] = dsUCI_CI_IIJoin.Tables[0].Rows[count]["BusinessRegistrationNum"].ToString();
					row["OrderRate"] = Decimal.Parse(dsUCI_CI_IIJoin.Tables[0].Rows[count]["OrderRate"].ToString());
				
					//BO_HTrow["FirstDeliveryDemandDate"] = DateTime.Parse(dr["FirstDeliveryDemandDate"].ToString());
					if(Convert.IsDBNull(dr["FirstDeliveryDemandDate"]))
						row["FirstDeliveryDemandDate"] = Convert.DBNull;
					else
						row["FirstDeliveryDemandDate"] = DateTime.Parse(dr["FirstDeliveryDemandDate"].ToString());

					if(Convert.IsDBNull(dr["SecondDeliveryDemandDate"]))
						row["SecondDeliveryDemandDate"] = Convert.DBNull;
					else
						row["SecondDeliveryDemandDate"] = DateTime.Parse(dr["SecondDeliveryDemandDate"].ToString());

					if(Convert.IsDBNull(dr["ThirdDeliveryDemandDate"]))
						row["ThirdDeliveryDemandDate"] = Convert.DBNull;
					else
						row["ThirdDeliveryDemandDate"] = DateTime.Parse(dr["ThirdDeliveryDemandDate"].ToString());					
					
					if(Convert.IsDBNull(dr["FourthDeliveryDemandDate"]))
						row["FourthDeliveryDemandDate"] = Convert.DBNull;
					else
						row["FourthDeliveryDemandDate"] = DateTime.Parse(dr["FourthDeliveryDemandDate"].ToString());					
					
					if(Convert.IsDBNull(dr["FifthDeliveryDemandDate"]))
						row["FifthDeliveryDemandDate"] = Convert.DBNull;
					else
						row["FifthDeliveryDemandDate"] = DateTime.Parse(dr["FifthDeliveryDemandDate"].ToString());
								
					if(count == dsUCI_CI_IIJoin.Tables[0].Rows.Count - 1)
					{
						//1차부터 5차까지 납기량
						first=Decimal.Parse(dsBR.Tables[0].Rows[drRowCount - 1]["FirstDeliveryDemandQuantity"].ToString());
						second=Decimal.Parse(dsBR.Tables[0].Rows[drRowCount - 1]["SecondDeliveryDemandQuantity"].ToString());
						third=Decimal.Parse(dsBR.Tables[0].Rows[drRowCount - 1]["ThirdDeliveryDemandQuantity"].ToString());
						fourth=Decimal.Parse(dsBR.Tables[0].Rows[drRowCount - 1]["FourthDeliveryDemandQuantity"].ToString());
						fifth=Decimal.Parse(dsBR.Tables[0].Rows[drRowCount - 1]["FifthDeliveryDemandQuantity"].ToString());
					
						
						//한품목당 여러업체가 있을경우 마지막 회사의 발주수량 구하는 곳
						for(int rowCount = dsBR.Tables[1].Rows.Count - (dsUCI_CI_IIJoin.Tables[0].Rows.Count - 1); rowCount <= dsBR.Tables[1].Rows.Count - 1; rowCount++)
						{
							first -= Decimal.Parse(dsBR.Tables[1].Rows[rowCount]["FirstDeliveryDemandQuantity"].ToString());
							second -= Decimal.Parse(dsBR.Tables[1].Rows[rowCount]["SecondDeliveryDemandQuantity"].ToString());
							third -= Decimal.Parse(dsBR.Tables[1].Rows[rowCount]["ThirdDeliveryDemandQuantity"].ToString());
							fourth -= Decimal.Parse(dsBR.Tables[1].Rows[rowCount]["FourthDeliveryDemandQuantity"].ToString());
							fifth -= Decimal.Parse(dsBR.Tables[1].Rows[rowCount]["FifthDeliveryDemandQuantity"].ToString());		
						
							
						}						
					}
					else
					{			
						//각각의 변수에 1차부터 5차까지의 납품요구량을 계산해서 값을 넣는다.
						first = Decimal.Parse(dr["FirstDeliveryDemandQuantity"].ToString())*Decimal.Parse(dsUCI_CI_IIJoin.Tables[0].Rows[count]["OrderRate"].ToString())/100;
						second = Decimal.Parse(dr["SecondDeliveryDemandQuantity"].ToString())*Decimal.Parse(dsUCI_CI_IIJoin.Tables[0].Rows[count]["OrderRate"].ToString())/100;
						third = Decimal.Parse(dr["ThirdDeliveryDemandQuantity"].ToString())*Decimal.Parse(dsUCI_CI_IIJoin.Tables[0].Rows[count]["OrderRate"].ToString())/100;
						fourth = Decimal.Parse(dr["FourthDeliveryDemandQuantity"].ToString())*Decimal.Parse(dsUCI_CI_IIJoin.Tables[0].Rows[count]["OrderRate"].ToString())/100;
						fifth = Decimal.Parse(dr["FifthDeliveryDemandQuantity"].ToString())*Decimal.Parse(dsUCI_CI_IIJoin.Tables[0].Rows[count]["OrderRate"].ToString())/100;						
					
					
					}

					//1차부터 5차까지 납품요구량을 계산한 값을 각각의 요구량에 넣어준다.
					row["FirstDeliveryDemandQuantity"] = first;
					row["SecondDeliveryDemandQuantity"] = second;
					row["ThirdDeliveryDemandQuantity"] = third;
					row["FourthDeliveryDemandQuantity"] = fourth;
					row["FifthDeliveryDemandQuantity"] = fifth;

									
					//각회사별 총 납품요구량은 1차부터 5차까지의 납품요구량을 합한값을 넣어준다.
					row["OrderQuantity"] = first+ second + third + fourth + fifth;
					row["RemainQuantity"] = first+ second + third + fourth + fifth;


					//각 거래처별로 적용단가가 다르기때문에 거래처별로 단가테이블에서 구매단가를 가져와서 보여준다
					string str = "Select * From UCI_MT Where RecodingState = 1 and UnitCostDistinction = '구매단가' and BusinessRegistrationNum = @com and ItemNum = @num and OrderRate != 0 order by OrderRate desc";
					//sqlConnection1.Open();
					conn.Open();
					SqlCommand comm =new SqlCommand(str,conn);
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = dsUCI_CI_IIJoin.Tables[0].Rows[count]["BusinessRegistrationNum"].ToString();
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = dsUCI_CI_IIJoin.Tables[0].Rows[count]["ItemNum"].ToString();
					SqlDataReader drow = comm.ExecuteReader();
					while(drow.Read())
					{
						row["ApplyUnitCost"] = Decimal.Parse(drow["StandardUnitCost"].ToString());//표준단가
					}
					drow.Close();
					conn.Close();
					//sqlConnection1.Close();




					
					row["TotalCost"] = Decimal.Parse(row["OrderQuantity"].ToString()) * Decimal.Parse(row["ApplyUnitCost"].ToString());//총금액
					row["BuyingRequestHistoryIndex"] = int.Parse(dr["BuyingRequestHistoryIndex"].ToString());
				

					//if(first+ second + third + fourth + fifth != 0)
					//dsBuyingOrder1.BO_HT.AddBO_HTRow(BO_HTrow);
					dsBR.Tables[1].Rows.Add(row);
					
					
				}
			}
			
			try
			{
				//구매의뢰과 아직 발주되지 않은 발주원장을 테이블끼리 관계맺어준다.
				dsBR.Relations.Add(dsBR.Tables[0].Columns["BuyingRequestHistoryIndex"],
					dsBR.Tables[1].Columns["BuyingRequestHistoryIndex"]);		
			}
			catch(System.Exception x)
			{
				string s = x.Message;
				string m = s;
			}
			finally
			{			
				//처음 보이는 화면에서 디폴트로 구매의뢰원장이 나오고 +클릭했을때는 발주원장이 나온다.
				this.uwgBO_HT.DataSource = dsBR.Tables[0].DefaultView;	

				Session["dsBR"] = dsBR;
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
			this.uwgBO_HT.DataBinding += new System.EventHandler(this.uwgBO_HT_DataBinding);
			this.btnRegistration.Click += new System.EventHandler(this.btnRegistration_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			if(dsBR.Tables.Count != 0)
			{
				dsBR.Tables[1].Clear();
				dsBR.Tables[0].Clear();
			}
			this.DataBind();
		}


		private void btnRegistration_Click(object sender, System.EventArgs e)
		{
			
			//그리드에 품목이 없는데 발주버튼을 클릭했을때 메세지창
			if(uwgBO_HT.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('발주보낼 품목이 없습니다.');");
				Response.Write("history.back();");
				Response.Write("</script>");
			}
			else
			{
			

				for(int i = 0; i < uwgBO_HT.Rows.Count; i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(uwgBO_HT.Rows[i].Cells.FromKey("chk").Value == null)
					{
						uwgBO_HT.Rows[i].Cells.FromKey("chk").Value = false;
					}

					if(!bool.Parse(uwgBO_HT.Rows[i].Cells.FromKey("chk").Value.ToString()))
					{
						// 각원장 등록함수
						DataRow[] row = dsBR.Tables[0].Select("BuyingRequestHistoryIndex = '" + uwgBO_HT.Rows[i].Cells.FromKey("BuyingRequestHistoryIndex").Value + "'");
						dsBR.Tables[0].Rows.Remove(row[0]);
					}
				}

				if(dsBR.Tables[0].Rows.Count == 0)
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('항목을 선택해 주세요!');");
					Response.Write("</script>");

				}
				else
				{
					//등록객체로 등록
					KIT_ERP.Register register = new Register(dsBR,"BuyingOrder",Session["ID"].ToString());
					register.DataSetRegistration();

					// 등록을 하고난뒤에는 체크박스 상자를 false로 바꾸어준다
					for(int i = 0; i < uwgBO_HT.Rows.Count; i++)
					{
						uwgBO_HT.Rows[i].Cells[0].Value = false;
					}

					dsBR.Tables[1].Clear();
					dsBR.Tables[0].Clear();
					Session.Remove("dsBR");

					this.DataBind();

					

				}
				
			}
		}

	}
}
