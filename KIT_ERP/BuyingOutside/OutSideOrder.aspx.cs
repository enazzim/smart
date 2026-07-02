using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// OutSideOrder에 대한 요약 설명입니다.
	/// </summary>
	public class OutSideOrder : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnRegistration;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRowIndex;
		protected System.Web.UI.WebControls.LinkButton linkTempUpdate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMaxDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMinDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgOO_HT;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected Infragistics.WebUI.WebDataInput.WebDateTimeEdit wdcDeliveryDemandDate;
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		DataSet dsOR = new DataSet();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.Products = true;	
			CSC1.UnitCostDistinction="외주거래처";
			

			if(!Page.IsPostBack)
			{
				Session["dsOR"] = dsOR;

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
				dsOR = (DataSet)Session["dsOR"];
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
			this.btnRegistration.Click += new System.EventHandler(this.btnRegistration_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.DataBinding += new System.EventHandler(this.uwgOO_HT_DataBinding);

		}
		#endregion

		private void uwgOO_HT_DataBinding(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			
			SqlDataAdapter OO_DataAdapter1 = new SqlDataAdapter(@"SELECT OOR_HT.ItemNum, OOR_HT.ItemDrawNum, OOR_HT.ItemName, OOR_HT.UnitCostDistinction, OOR_HT.BeginProcessCode, OOR_HT.BeginProcess, OOR_HT.EndProcessCode, OOR_HT.EndProcess, CI_MT.CompanyName, OOR_HT.FirstDeliveryDemandQuantity, OOR_HT.FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, OOR_HT.OrderQuantity, UCI_MT.StandardUnitCost as ApplyUnitCost,(UCI_MT.StandardUnitCost * FirstDeliveryDemandQuantity) as TotalCost, OOR_HT.ProgressCondition, OOR_HT.RegistrationPerson, OOR_HT.RegistrationPersonID, OOR_HT.RegistrationDate,  OOR_HT.UpdatingPerson, OOR_HT.UpdatingPersonID, OOR_HT.UpdatingDate, ProductionPlanHistoryIndex, OutSideOrderRequestHistoryIndex 
				FROM OOR_HT 
				INNER JOIN II_MT ON OOR_HT.ItemNum = II_MT.ItemNum 
				Inner join UCI_MT on OOR_HT.ItemNum = UCI_MT.ItemNum and OOR_HT.BeginProcessCode = UCI_MT.BeginProcessCode and OOR_HT.EndProcessCode = UCI_MT.EndProcessCode
				inner join CI_MT on UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
				WHERE OOR_HT.ItemNum like @num and OOR_HT.ItemDrawNum like @draw and OOR_HT.ItemName like @name 
				and II_MT.RecodingState = 1 
				and UCI_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '외주단가' and UCI_MT.OrderRate = (Select Max(OrderRate) From UCI_MT where ItemNum like @num and UnitCostDistinction = '외주단가')
				and CI_MT.CompanyName Like @CompanyName
				and CI_MT.BusinessRegistrationNum Like @BusinessRegistrationNum
				and  (OOR_HT.RegistrationDate >= @begin and OOR_HT.RegistrationDate <= @end) and ItemClassification1 Like @Classification and (ProgressCondition = '대기') order by CI_MT.CompanyName, FirstDeliveryDemandDate "+
				" SELECT ItemNum, ItemDrawNum, ItemName, UnitCostDistinction, BeginProcessCode, BeginProcess, EndProcessCode, EndProcess, CompanyName, BusinessRegistrationNum, OrderRate, FirstDeliveryDemandQuantity, FirstDeliveryDemandDate, SecondDeliveryDemandQuantity, SecondDeliveryDemandDate, ThirdDeliveryDemandQuantity, ThirdDeliveryDemandDate, FourthDeliveryDemandQuantity, FourthDeliveryDemandDate, FifthDeliveryDemandQuantity, FifthDeliveryDemandDate, OrderQuantity, ApplyUnitCost, TotalCost, RemainQuantity, VolumNum, ProgressCondition, RegistrationPerson, RegistrationPersonID, RegistrationDate, UpdatingPerson, UpdatingPersonID, UpdatingDate, OutSideOrderRequestHistoryIndex, OutSideOrderHistoryIndex FROM OO_HT WHERE (ItemNum IS NULL)",conn);

			OO_DataAdapter1.SelectCommand.Parameters.Add("@num",SqlDbType.VarChar).Value = "%"+ItemSearchControl1.ItemNum+"%";
			if(ItemSearchControl1.hdItem.Trim() == "")
			{
				OO_DataAdapter1.SelectCommand.Parameters.Add("@draw",SqlDbType.VarChar).Value = "%"+ItemSearchControl1.ItemDrawNum+"%";
				OO_DataAdapter1.SelectCommand.Parameters.Add("@name",SqlDbType.VarChar).Value = "%"+ItemSearchControl1.ItemName+"%";
			}
			else
			{
				OO_DataAdapter1.SelectCommand.Parameters.Add("@draw",SqlDbType.VarChar).Value = "%%";
				OO_DataAdapter1.SelectCommand.Parameters.Add("@name",SqlDbType.VarChar).Value = "%%";
			}

			OO_DataAdapter1.SelectCommand.Parameters.Add("@Classification",SqlDbType.VarChar).Value ="%"+ddlItemClassification1.SelectedItem.Value+"%";
			OO_DataAdapter1.SelectCommand.Parameters.Add("@CompanyName",SqlDbType.VarChar).Value = "%"+CSC1.Company+"%";
			OO_DataAdapter1.SelectCommand.Parameters.Add("@BusinessRegistrationNum",SqlDbType.VarChar).Value =  "%"+CSC1.BusinessRegistrationNum+"%";

			if(wdcMinDate.Text.Trim()=="")
				OO_DataAdapter1.SelectCommand.Parameters.Add("@begin",SqlDbType.SmallDateTime).Value = DateTime.Parse("1900-01-01").ToShortDateString();
			else
				OO_DataAdapter1.SelectCommand.Parameters.Add("@begin",SqlDbType.SmallDateTime).Value = DateTime.Parse(wdcMinDate.Value.ToString()).ToShortDateString();
			if(wdcMaxDate.Text.Trim()=="")
				OO_DataAdapter1.SelectCommand.Parameters.Add("@end",SqlDbType.SmallDateTime).Value = DateTime.Parse("2076-06-06").ToShortDateString();
			else
				OO_DataAdapter1.SelectCommand.Parameters.Add("@end",SqlDbType.SmallDateTime).Value = DateTime.Parse(wdcMaxDate.Value.ToString()).ToShortDateString();

			OO_DataAdapter1.Fill(dsOR);

			

			int drRowCount = 0;
			foreach(DataRow dr in dsOR.Tables[0].Rows)
			{
				drRowCount++;

				//빈 BO_HT을 조인문과 이전 의뢰원장의 내용을 토대로 발주
				string strSQL = @"select * from UCI_MT, CI_MT,II_MT where UCI_MT.RecodingState = 1 and CI_MT.RecodingState =1 and 
								II_MT.RecodingState = 1 and UCI_MT.UnitCostDistinction = '외주단가' and 
								UCI_MT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum AND II_MT.ItemNum = UCI_MT.ItemNum 
								AND UCI_MT.ItemNum = '" + dr["ItemNum"] + "' and BeginProcessCode = '"+dr["BeginProcessCode"]+ "' and EndProcessCode = '"+dr["EndProcessCode"]+"'  and UCI_MT.OrderRate != 0 order by UCI_MT.OrderRate desc";

				DataSet dsUCI_CI_IIJoin= new DataSet();	
				SqlDataAdapter UCI_CI_IIJoinAdapter = new SqlDataAdapter(strSQL,conn);
				UCI_CI_IIJoinAdapter.Fill(dsUCI_CI_IIJoin);

				DataTable table = dsOR.Tables[1];

				for(int count =0; count <=dsUCI_CI_IIJoin.Tables[0].Rows.Count - 1;count++)
				{
					Decimal first = 0, second = 0, third = 0, fourth = 0, fifth = 0;

					//만들어진 row를 dsBR에 추가함.

					DataRow row = table.NewRow();
					row["ItemNum"] = dsUCI_CI_IIJoin.Tables[0].Rows[count]["ItemNum"].ToString();
					row["ItemDrawNum"] = dsUCI_CI_IIJoin.Tables[0].Rows[count]["ItemDrawNum"].ToString();
					row["ItemName"] = dsUCI_CI_IIJoin.Tables[0].Rows[count]["ItemName"].ToString();

					row["CompanyName"] = dsUCI_CI_IIJoin.Tables[0].Rows[count]["CompanyName"].ToString();
					row["BusinessRegistrationNum"] = dsUCI_CI_IIJoin.Tables[0].Rows[count]["BusinessRegistrationNum"].ToString();
					row["OrderRate"] = Decimal.Parse(dsUCI_CI_IIJoin.Tables[0].Rows[count]["OrderRate"].ToString());
				
					//OO_HTrow.FirstDeliveryDemandDate = DateTime.Parse(dr["FirstDeliveryDemandDate"].ToString());					
					
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
						first=Decimal.Parse(dsOR.Tables[0].Rows[drRowCount - 1]["FirstDeliveryDemandQuantity"].ToString());
						second=Decimal.Parse(dsOR.Tables[0].Rows[drRowCount - 1]["SecondDeliveryDemandQuantity"].ToString());
						third=Decimal.Parse(dsOR.Tables[0].Rows[drRowCount - 1]["ThirdDeliveryDemandQuantity"].ToString());
						fourth=Decimal.Parse(dsOR.Tables[0].Rows[drRowCount - 1]["FourthDeliveryDemandQuantity"].ToString());
						fifth=Decimal.Parse(dsOR.Tables[0].Rows[drRowCount - 1]["FifthDeliveryDemandQuantity"].ToString());
					

						
						//한품목당 여러업체가 있을경우 마지막 회사의 발주수량 구하는 곳
						for(int rowCount = dsOR.Tables[1].Rows.Count - (dsUCI_CI_IIJoin.Tables[0].Rows.Count - 1); rowCount <= dsOR.Tables[1].Rows.Count - 1; rowCount++)
						{					
							first -= Decimal.Parse(dsOR.Tables[1].Rows[rowCount]["FirstDeliveryDemandQuantity"].ToString());
							second -= Decimal.Parse(dsOR.Tables[1].Rows[rowCount]["SecondDeliveryDemandQuantity"].ToString());
							third -= Decimal.Parse(dsOR.Tables[1].Rows[rowCount]["ThirdDeliveryDemandQuantity"].ToString());
							fourth -= Decimal.Parse(dsOR.Tables[1].Rows[rowCount]["FourthDeliveryDemandQuantity"].ToString());
							fifth -= Decimal.Parse(dsOR.Tables[1].Rows[rowCount]["FifthDeliveryDemandQuantity"].ToString());
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
					row["OutSideOrderRequestHistoryIndex"] = int.Parse(dr["OutSideOrderRequestHistoryIndex"].ToString());//외주의뢰원장번호
					
					//각 거래처별로 적용단가가 다르기때문에 거래처별로 단가테이블에서 외주단가를 가져와서 보여준다
					row["BeginProcessCode"] = dr["BeginProcessCode"].ToString(); //시작공정코드
					row["BeginProcess"] = dr["BeginProcess"].ToString();//시작공정
					row["EndProcessCode"] = dr["EndProcessCode"].ToString(); //종료공정코드
					row["EndProcess"] = dr["EndProcess"].ToString();//종료공정	

					string str = @"Select * From UCI_MT Where RecodingState = 1 and UnitCostDistinction = '외주단가' and BusinessRegistrationNum = @com and 
						BeginProcessCode = @begin and EndProcessCode = @end and ItemNum = @Item  and UCI_MT.OrderRate != 0 order by UCI_MT.OrderRate desc";
					conn.Open();
					SqlCommand comm =new SqlCommand(str,conn);
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = dsUCI_CI_IIJoin.Tables[0].Rows[count]["BusinessRegistrationNum"].ToString();
					comm.Parameters.Add("@begin",SqlDbType.VarChar).Value = row["BeginProcessCode"].ToString();
					comm.Parameters.Add("@end",SqlDbType.VarChar).Value = row["EndProcessCode"].ToString();
					comm.Parameters.Add("@Item",SqlDbType.VarChar).Value = row["ItemNum"].ToString();
					SqlDataReader drow = comm.ExecuteReader();
					while(drow.Read())
					{
						row["ApplyUnitCost"] = Decimal.Parse(drow["StandardUnitCost"].ToString());//표준단가
					}
					drow.Close();
					conn.Close();

					row["TotalCost"] = Decimal.Parse(row["ApplyUnitCost"].ToString()) * Decimal.Parse(row["OrderQuantity"].ToString()); //총금액														
					row["UnitCostDistinction"] = dr["UnitCostDistinction"].ToString();//단가구분
					
					if(first+ second + third + fourth + fifth != 0)
						dsOR.Tables[1].Rows.Add(row);		
				}
			}
			

			try
			{
				//외주의뢰원장과 아직 발주되지 않은 발주원장을 테이블끼리 관계맺어준다.
				//구매의뢰과 아직 발주되지 않은 발주원장을 테이블끼리 관계맺어준다.
				dsOR.Relations.Add(dsOR.Tables[0].Columns["OutSideOrderRequestHistoryIndex"],
					dsOR.Tables[1].Columns["OutSideOrderRequestHistoryIndex"]);	
			}

			catch(System.Exception x)
			{
				string s = x.Message;
				string m = s;
			}

			finally
			{
				//처음 보이는 화면에서 디폴트로 구매의뢰원장이 나오고 +클릭했을때는 발주원장이 나온다.
				uwgOO_HT.DataSource = dsOR.Tables[0].DefaultView;	
			}

		}


		private void btnRegistration_Click(object sender, System.EventArgs e)
		{
			
			//그리드에 품목이 없는데 발주버튼을 클릭했을때 메세지창
			if(this.uwgOO_HT.Rows.Count == 0)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('발주보낼 품목이 없습니다.');");
				Response.Write("history.back();");
				Response.Write("</script>");
			}
			else
			{


				for(int i = 0; i < uwgOO_HT.Rows.Count; i++)
				{
					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
					if(uwgOO_HT.Rows[i].Cells.FromKey("chk").Value == null)
					{
						uwgOO_HT.Rows[i].Cells.FromKey("chk").Value = false;
					}

					if(!bool.Parse(uwgOO_HT.Rows[i].Cells.FromKey("chk").Value.ToString()))
					{
						// 각원장 등록함수
						DataRow[] row = dsOR.Tables[0].Select("OutSideOrderRequestHistoryIndex = '" + uwgOO_HT.Rows[i].Cells.FromKey("OutSideOrderRequestHistoryIndex").Value + "'");
						dsOR.Tables[0].Rows.Remove(row[0]);
					}
				}

//				for(int i = uwgOO_HT.Rows.Count-1; i >= 0 ; i--)
//				{
//					// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
//					if(uwgOO_HT.Rows[i].Cells.FromKey("chk").Value == null)
//					{
//						uwgOO_HT.Rows[i].Cells.FromKey("chk").Value = false;
//					}
//
//					if(!bool.Parse(uwgOO_HT.Rows[i].Cells.FromKey("chk").Value.ToString()))
//					{
//						// 각원장 등록함수
//						DataRow[] row = dsOR.Tables[0].Select("OutSideOrderRequestHistoryIndex = '" + uwgOO_HT.Rows[i].Cells.FromKey("OutSideOrderRequestHistoryIndex").Value + "'");
//						dsOR.Tables[0].Rows.Remove(row[0]);
//						row[0].Delete();
//					}
//				}

				if(dsOR.Tables[0].Rows.Count == 0)
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('항목을 선택해 주세요!');");
					Response.Write("</script>");

				}
				else
				{
					
					//등록객체로 등록
					KIT_ERP.Register register = new Register(dsOR,"OutSideOrder",Session["ID"].ToString());
					register.DataSetRegistration();

					// 등록을 하고난뒤에는 체크박스 상자를 false로 바꾸어준다
					for(int i = 0; i < uwgOO_HT.Rows.Count; i++)
					{
						uwgOO_HT.Rows[i].Cells[0].Value = false;
					}

					dsOR.Tables[1].Clear();
					dsOR.Tables[0].Clear();
					Session["dsOR"] = dsOR;
					if(uwgOO_HT.Rows.Count == 0)
						Session.Remove("dsOR");

					this.DataBind();


				}
			}
	
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{

			if(dsOR.Tables.Count != 0)
			{
				dsOR.Tables[1].Clear();
				dsOR.Tables[0].Clear();
			}
			this.DataBind();
		}
	}
}