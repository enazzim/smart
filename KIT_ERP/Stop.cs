using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Infragistics.WebUI;
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP
{
	/// <summary>
	/// Strop에 대한 요약 설명입니다.
	/// </summary>
	public class Stop
	{
		private string m_PageName;		//요청페이지를 담는 필드
		private string m_Table;			//테이블명을 담는필드
		
		private UltraWebGrid m_Grid;	//넘어온 그리드 저장 필드
		private string m_Distinction ;//업무구분(모듈구분)
		
		private string m_UserID;
		private string m_UserName;
		private string m_Index;
		//private int stop = 0;		//중단이 성공되었는지 확인하는 필드
		//private int count = 0;	//그리드에 체크된것이 있는지 확인하는 필드

		/// <summary>
		/// 중단객체 생성자
		/// </summary>
		/// <param name="grid"></param>
		/// <param name="PageName"></param>
		/// <param name="User"></param>
		public Stop(UltraWebGrid grid, string PageName, string User)
		{
			m_Grid = grid;
			m_PageName = PageName.ToString();
			m_UserID = User.ToString();

			//////////////////////
			// 사용자 이름 입력 //
			//////////////////////
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = m_UserID.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				m_UserName = dr["Name"].ToString();
			}
			dr.Close();
			conn.Close();

			switch(m_PageName)
			{
				case "ReceiveingPC":						// 수주현황
					m_Table = "RO_HT";
					m_Distinction = "영업";
					m_Index = "ReceivingOrderHistoryIndex";
					break;				
				case "ProductionRequestPC":					// 생산의뢰현황
					m_Table = "PR_HT";
					m_Distinction = "영업";
					m_Index = "ProductionRequestHistoryIndex";
					break;
				case "GoodsBuyingRequestPC":				// 상품구매의뢰현황
					m_Table = "BR_HT";
					m_Distinction = "영업";
					m_Index = "BuyingRequestHistoryIndex";
					break;
				
				case "GoodsManufactureOutStorehousePC":		// 상품제품출고현황
					m_Table = "OS_HT";
					m_Distinction = "영업";
					m_Index = "OutStorehouseHistoryIndex";
					break;
				

				case "ProductionPlanPC":					// 생산계획현황
					m_Table = "PP_HT";
					m_Distinction = "생산";
					m_Index = "ProductionPlanHistoryIndex";
					break;	
			
				case "RowMaterialRequirementCalculatePC":	// 자재소요량산출현황
					m_Table = "MRC_HT";
					m_Distinction = "생산";
					m_Index = "ProductionPlanHistoryIndex";	//자재소요원장의 생산계획번호가 동일한 것들을 중단시킨다
					break;
				case "RowMaterialRequriementPC":			// 원자재의뢰현황
					m_Table = "BR_HT";
					m_Distinction = "생산";
					m_Index ="BuyingRequestHistoryIndex";
					break;
				case "OutsideRequestPC":					// 외주의뢰현황
					m_Table = "OOR_HT";
					m_Distinction = "생산";
					m_Index = "OutSideOrderRequestHistoryIndex";

					break;


				case "BuyingOrderPC":					// 구매발주현황
					m_Table = "BO_HT";
					m_Distinction = "구매";
					m_Index = "BuyingOrderHistoryIndex";
					break;				
				case "BuyingDeliveryPC":					// 구매납품현황
					m_Table = "BD_HT";
					m_Distinction = "구매";
					m_Index = "BuyingDeliveryHistoryIndex";
					break;
				case "OutSideOrderPC":						// 외주발주현황
					m_Table = "OO_HT";
					m_Distinction = "외주";
					m_Index = "OutSideOrderHistoryIndex";
					break;
				case "OutSideOutStorehousePC":				// 외주출고현황
					m_Table = "OOS_HT";
					m_Distinction = "외주";
					m_Index = "OutSideOutStorehouseHistoryIndex";
					break;				
				case "OutSideDeliveryPC":					// 외주납품현황
					m_Table = "OSD_HT";	
					m_Distinction = "외주";
					m_Index = "OutSideDeliveryHistoryIndex";
					break;
				default :
					break;

			}
		}




		/// <summary>
		/// 월마감여부 
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing(SqlConnection con, SqlTransaction trans)
		{
			
			int year = 0;
			int month = 0;
			
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = m_Distinction.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			dr.Close();

			if(year < int.Parse(DateTime.Now.ToShortDateString().Substring(0,4).Trim()))
			{
				return true;
			}
			else if(year == int.Parse(DateTime.Now.ToShortDateString().Substring(0,4).Trim())) 
			{
				if(month < int.Parse(DateTime.Now.ToShortDateString().Substring(5,2).Trim()))
					return true;
				else
				{
					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('월마감이되어 중단이 불가능합니다.');");
					hc.Response.Write("</script>");
					return false;
				}
			}
			else
			{
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('월마감이되어 중단이 불가능합니다.');");
				hc.Response.Write("</script>");
				return false;
			}		
			
		}



		public UltraWebGrid MainTableStop()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			try
			{
				// 월마감 체크후 중단이 가능한지 확인한다
				if(MonthClosing(conn,tr) == true)
				{
				
				
					for(int i = 0; i < m_Grid.Rows.Count; i++)
					{
						// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
						if(m_Grid.Rows[i].Cells.FromKey("chk").Value == null)
						{
							m_Grid.Rows[i].Cells.FromKey("chk").Value = false;
						}

						if(bool.Parse(m_Grid.Rows[i].Cells.FromKey("chk").Value.ToString()))
						{
							if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Text == "완료")				// 선택항목들중 진행상태가 완료가 있으면 중단이 불가능
							{
								throw new Exception(m_Grid.Rows[i].Cells.FromKey("ItemName").Text + " 품목의 진행상태가 완료되어 중단할수 없습니다!");
							}
							else
							{
								switch(m_PageName)
								{
									case "ReceiveingPC":					//수주현황
										if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() == "대기")
										{
											RO_HTStop(conn,tr,i);
										}
										else
										{
											RORowFinish(conn,tr,i);				
											RORowStop(conn,tr,i);		//중단항목 생성
										}
										break;
									case "BuyingOrderPC":					//구매발주현황
										if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() != "대기")
										{
											BORowFinish(conn,tr,i);		//완료처리
											BORowStop(conn,tr,i);		//중단항목 생성
										}
										else if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() == "대기")
										{
											BO_HTStop(conn,tr,i);	//중단처리
										}
										break;
									case "OutSideOrderPC":					//외주발주현황
										if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Value.ToString() != "대기")
										{
											OORowFinish(conn,tr,i);		//중단처리
											OORowStop(conn,tr,i);		//중단항목 생성
										}
										else
										{
											OO_HTStop(conn,tr,i);		//중단처리
										}
										
										break;
									default :
										RowStop(conn,tr,i);
										break;
								}
							}
						
						
						}
					
					}
					// 등록을 하고난뒤에는 체크박스 상자를 false로 바꾸어준다
					for(int i = 0; i < m_Grid.Rows.Count; i++)
					{
						m_Grid.Rows[i].Cells[0].Value = false;
					}

					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('중단 되었습니다!');");
					hc.Response.Write("</script>");
				}
				tr.Commit();
			}
			catch(Exception ee)
			{
				tr.Rollback();
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");
			}
			finally
			{
				conn.Close();
				
			}
			return m_Grid;
		}


		private void RowStop(SqlConnection con, SqlTransaction trans,int count)
		{
			string str="Update "+m_Table.ToString()+" set ProgressCondition = '중단' Where "+m_Index.ToString()+" = @idx";
			SqlCommand comm = new SqlCommand(str, con);
			comm.Transaction = trans;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[count].Cells.FromKey(m_Index.ToString()).Text;
			comm.ExecuteNonQuery();
		}



		/// <summary>
		/// 수주중단시 완료로 업데이트
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void RORowFinish(SqlConnection con, SqlTransaction trans, int count)
		{
			string str = "Update RO_HT set TotalCost=@totalcost,"+
				" OutStorehouseQuantity=@storequantity,SuitabilityQuantity=@suitquantity, "+
				" ProgressCondition = '완료',UnInspectionQuantity=@unquantity,RemainderQuantity=0,"+
				" UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date "+
				" Where ReceivingOrderHistoryIndex = @Index";

			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("SuitabilityQuantity").Text)*decimal.Parse(m_Grid.Rows[count].Cells.FromKey("ApplyUnitCost").Text);
			comm.Parameters.Add("@storequantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("OutStorehouseQuantity").Text);
			comm.Parameters.Add("@suitquantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("SuitabilityQuantity").Text);
			comm.Parameters.Add("@unquantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("UnInspectionQuantity").Text);
			comm.Parameters.Add("@remainquantity",SqlDbType.Decimal).Value = 0;
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName;
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID;				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@Index", SqlDbType.Int).Value = m_Grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Text;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 수주중단항목 생성
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void RORowStop(SqlConnection con, SqlTransaction trans, int count)
		{	
			string str = "Insert into RO_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,PropertyClassification,ProductionRequestDivision,ReceivingOrderDate,"+
				"ApplyUnitCost,DeliveryRequestQuantity1,DeliveryRequestDate1,DeliveryRequestQuantity2,DeliveryRequestDate2,DeliveryRequestQuantity3,"+
				"DeliveryRequestDate3,DeliveryRequestQuantity4,DeliveryRequestDate4,DeliveryRequestQuantity5,DeliveryRequestDate5,TotalReceiveingOrderQuantity,"+
				"TotalCost,OutStorehouseQuantity,SuitabilityQuantity,UnInspectionQuantity,RemainderQuantity,OrderNum,DeliveryPlace,ProgressCondition,"+
				"RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
				"@itemnum, @itemdrawnum,@itemname,@com,@comnum,@classification,@division,@receivingdate,@cost,@quantity1,@date1,@quantity2,@date2,@quantity3,@date3,"+
				"@quantity4,@date4,@quantity5,@date5,@total,@totalcost,@storequantity,@suitquantity,@unquantity,@remainquantity,@ordernum,@place,'중단',"+
				"@Person, @PersonID, @Date)";

			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("ItemDrawNum").Text;
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("ItemName").Text;
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("CompanyName").Text;
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("BusinessRegistrationNum").Text;
			comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("PropertyClassification").Text;
			
			if(m_Grid.Rows[count].Cells.FromKey("ProductionRequestDivision").Value.ToString() == "예")
                comm.Parameters.Add("@division",SqlDbType.Bit).Value = 1;
			else
				comm.Parameters.Add("@division",SqlDbType.Bit).Value = 0;
			comm.Parameters.Add("@receivingdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("ReceivingOrderDate").Text).ToShortDateString();
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("ApplyUnitCost").Text);

			comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity1").Text);
			comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate1").Text).ToShortDateString();
			
			if(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity2").Value == null || m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity2").Text.Trim() == "")
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = Convert.DBNull ;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity2").Text);

			if(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate2").Value == null || m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate2").Text.Trim() == "")
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate2").Text).ToShortDateString();
			
			if(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity3").Value == null || m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity3").Text.Trim() == "")
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = Convert.DBNull ;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity3").Text);

			if(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate3").Value == null || m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate3").Text.Trim() == "")
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate3").Text).ToShortDateString();

			if(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity4").Value == null || m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity4").Text.Trim() == "")
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = Convert.DBNull ;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity4").Text);
			
			if(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate4").Value == null || m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate4").Text.Trim() == "")
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate4").Text).ToShortDateString();
			
			if(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity5").Value == null || m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity5").Text.Trim() == "")
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = Convert.DBNull ;
			else
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestQuantity5").Text);

			if(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate5").Value == null || m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate5").Text.Trim() == "")
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("DeliveryRequestDate5").Text).ToShortDateString();
			comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("TotalReceiveingOrderQuantity").Text);
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("TotalCost").Text);
			comm.Parameters.Add("@storequantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("OutStorehouseQuantity").Text);
			comm.Parameters.Add("@suitquantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("SuitabilityQuantity").Text);
			comm.Parameters.Add("@unquantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("UnInspectionQuantity").Text);
			comm.Parameters.Add("@remainquantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("RemainderQuantity").Text);
			comm.Parameters.Add("@ordernum",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("OrderNum").Text;
			comm.Parameters.Add("@place",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("DeliveryPlace").Text;
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();

			comm.ExecuteNonQuery();

		}

		private void RO_HTStop(SqlConnection con, SqlTransaction trans, int count)
		{
			string str = "Update RO_HT set "+
				" ProgressCondition = '중단', " +
				" UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date "+
				" Where ReceivingOrderHistoryIndex = @Index";

			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName;
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID;				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@Index", SqlDbType.Int).Value = m_Grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Text;
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 구매발주중단시 완료로 업데이트
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void BORowFinish(SqlConnection con, SqlTransaction trans, int count)
		{
			string str =" UPDATE BO_HT SET " +
				" TotalCost = @totalcost, " +
				" OrderQuantity = @orderquantity, RemainQuantity = 0, "+
				" ProgressCondition = '완료', "+
				" UpdatingPerson = @person,"+
				" UpdatingPersonID = @personid, "+
				" UpdatingDate = @date "+
				"WHERE BuyingOrderHistoryIndex= @INDEX ";
		
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@totalcost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("ApplyUnitCost").Value.ToString())*(decimal.Parse(m_Grid.Rows[count].Cells.FromKey("OrderQuantity").Value.ToString())-decimal.Parse(m_Grid.Rows[count].Cells.FromKey("RemainQuantity").Value.ToString()));
			comm.Parameters.Add("@orderquantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("OrderQuantity").Value.ToString())-decimal.Parse(m_Grid.Rows[count].Cells.FromKey("RemainQuantity").Value.ToString());
			comm.Parameters.Add("@person", SqlDbType.VarChar).Value = m_UserName;
			comm.Parameters.Add("@personid", SqlDbType.VarChar).Value = m_UserID;
			comm.Parameters.Add("@date", SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[count].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();	
		}


		/// <summary>
		/// 구매발주중단항목 생성
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void BORowStop(SqlConnection con, SqlTransaction trans, int count)
		{	
			string str="Insert into BO_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,"+
				"FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,"+
				"ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,"+
				"FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,ApplyUnitCost,TotalCost,OrderRate,RemainQuantity,ProgressCondition,"+
				"RegistrationPerson,RegistrationPersonID,RegistrationDate,BuyingRequestHistoryIndex) values("+ 
				"@itemnum, @itemdrawnum,@itemname,@com,@comnum,@quantity1,@date1,@quantity2,@date2,"+
				"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,@orderrate,@remainquantity,'중단', @Person, @PersonID, @Date,@idx)";


			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("ItemNum").ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("ItemDrawNum").ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("ItemName").ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("CompanyName").ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("BusinessRegistrationNum").ToString();
			
			if(m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandQuantity").ToString() == null || m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandQuantity").ToString().Trim() =="")
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandQuantity").ToString());
			if(m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandDate").Value == null || m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandDate").Text.Trim() =="")
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandDate").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandQuantity").ToString() == null || m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandQuantity").ToString().Trim() =="")
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value =  Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandQuantity").ToString());
			if(m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandDate").Value == null || m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandDate").ToString().Trim() =="")
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandDate").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandQuantity").Value == null || m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandQuantity").ToString().Trim() =="")
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandQuantity").ToString());
			if(m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandDate").Value == null || m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandDate").ToString().Trim()  == "")
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandDate").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandQuantity").Value == null || m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandQuantity").ToString().Trim() =="")
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandQuantity").ToString());
			if(m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandDate").Value == null || m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandDate").ToString().Trim()  =="")
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandDate").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandQuantity").Value == null || m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandQuantity").ToString().Trim() =="")
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandQuantity").ToString());
			if(m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandDate").Value == null || m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandDate").ToString().Trim()  == "")
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandDate").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("OrderQuantity").Value == null || m_Grid.Rows[count].Cells.FromKey("OrderQuantity").ToString().Trim()  == "")
				comm.Parameters.Add("@total",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("OrderQuantity").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("ApplyUnitCost").Value == null || m_Grid.Rows[count].Cells.FromKey("ApplyUnitCost").ToString().Trim()  == "")
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("ApplyUnitCost").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("TotalCost").Value == null || m_Grid.Rows[count].Cells.FromKey("TotalCost").ToString().Trim()  == "")
				comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("TotalCost").ToString());
			if(m_Grid.Rows[count].Cells.FromKey("OrderRate").ToString() == null || m_Grid.Rows[count].Cells.FromKey("OrderRate").ToString() =="")
				comm.Parameters.Add("@orderrate",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@orderrate",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("OrderRate").ToString());
			if(m_Grid.Rows[count].Cells.FromKey("RemainQuantity").Value == null || m_Grid.Rows[count].Cells.FromKey("RemainQuantity").ToString() == "")
				comm.Parameters.Add("@remainquantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@remainquantity",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("RemainQuantity").ToString());
					
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName;
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID;
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			//comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[count].Cells.FromKey("BuyingRequestHistoryIndex").ToString());
            comm.Parameters.Add("@idx",SqlDbType.Int).Value = Convert.DBNull;

			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 대기중 중단은 그냥 중단시킨다.
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void BO_HTStop(SqlConnection con, SqlTransaction trans, int count)
		{
			string str =" UPDATE BO_HT SET " +
				" ProgressCondition = '중단', "+
				" UpdatingPerson = @person,"+
				" UpdatingPersonID = @personid, "+
				" UpdatingDate = @date "+
				"WHERE BuyingOrderHistoryIndex= @INDEX ";
		
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@person", SqlDbType.VarChar).Value = m_UserName;
			comm.Parameters.Add("@personid", SqlDbType.VarChar).Value = m_UserID;
			comm.Parameters.Add("@date", SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[count].Cells.FromKey("BuyingOrderHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();	
		}


		/// <summary>
		/// 외주발주중단시 완료로 업데이트
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void OORowFinish(SqlConnection con, SqlTransaction trans, int count)
		{
			string str =" UPDATE OO_HT SET " +
				" TotalCost = @totalcost, " +
				" OrderQuantity = @orderquantity, RemainQuantity = 0, "+
				" ProgressCondition = '완료', "+
				" UpdatingPerson = @person, "+
				" UpdatingPersonID = @personid, "+
				" UpdatingDate = @date WHERE OutSideOrderHistoryIndex= @INDEX ";
		
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@totalcost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("ApplyUnitCost").Value.ToString())*(decimal.Parse(m_Grid.Rows[count].Cells.FromKey("OrderQuantity").Value.ToString())-decimal.Parse(m_Grid.Rows[count].Cells.FromKey("RemainQuantity").Value.ToString()));
			comm.Parameters.Add("@orderquantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[count].Cells.FromKey("OrderQuantity").Value.ToString())-decimal.Parse(m_Grid.Rows[count].Cells.FromKey("RemainQuantity").Value.ToString());
			comm.Parameters.Add("@person", SqlDbType.VarChar).Value = m_UserName;
			comm.Parameters.Add("@personid", SqlDbType.VarChar).Value = m_UserID;
			comm.Parameters.Add("@date", SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[count].Cells.FromKey("OutSideOrderHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();	
		}


		/// <summary>
		/// 외주발주중단항목 생성
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void OORowStop(SqlConnection con, SqlTransaction trans, int count)
		{	
			string str = @"Insert into OO_HT (ItemNum,ItemDrawNum,ItemName,UnitCostDistinction,BeginProcessCode,BeginProcess,EndProcessCode,EndProcess,CompanyName,BusinessRegistrationNum,
				OrderRate,FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,ThirdDeliveryDemandQuantity,
				ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,
				ApplyUnitCost,TotalCost,RemainQuantity,ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate,OutSideOrderRequestHistoryIndex) values(
				@itemnum, @itemdrawnum,@itemname,@distinction,@beginsourcecode,@beginsource,@endsourcecode,@endsource,@com,@comnum,@rate,@quantity1,@date1,@quantity2,@date2,@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,@remain,'중단', @Person, @PersonID, @Date,@idx)";


			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;

			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("ItemNum").ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("ItemDrawNum").ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("ItemName").ToString();
			comm.Parameters.Add("@distinction",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("UnitCostDistinction").ToString();
			comm.Parameters.Add("@beginsourcecode",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("BeginProcessCode").ToString();
			comm.Parameters.Add("@beginsource",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("BeginProcess").ToString();
			comm.Parameters.Add("@endsourcecode",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("EndProcessCode").ToString();
			comm.Parameters.Add("@endsource",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("EndProcess").ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("CompanyName").ToString();
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("BusinessRegistrationNum").ToString();
			
			if(m_Grid.Rows[count].Cells.FromKey("OrderRate").Value == null || m_Grid.Rows[count].Cells.FromKey("OrderRate").ToString() =="")
				comm.Parameters.Add("@rate",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@rate",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("OrderRate").ToString());

			if(m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandQuantity").Value == null || m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandQuantity").ToString() =="")
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandQuantity").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandDate").Value == null || m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandDate").ToString().Trim() =="")
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("FirstDeliveryDemandDate").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandQuantity").Value == null || m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandQuantity").ToString() =="")
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandQuantity").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandDate").Value == null || m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandDate").ToString().Trim() =="")
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("SecondDeliveryDemandDate").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandQuantity").Value == null || m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandQuantity").ToString() =="")
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandQuantity").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandDate").Value == null || m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandDate").ToString().Trim()  == "")
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("ThirdDeliveryDemandDate").ToString());

			if(m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandQuantity").Value == null || m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandQuantity").ToString() =="")
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandQuantity").ToString());
			if(m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandDate").Value == null || m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandDate").ToString().Trim()  =="")
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("FourthDeliveryDemandDate").ToString());

			if(m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandQuantity").Value == null || m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandQuantity").ToString() =="")
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandQuantity").ToString());
			if(m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandDate").Value == null || m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandDate").ToString().Trim()  == "")
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("FifthDeliveryDemandDate").ToString());
	
			if(m_Grid.Rows[count].Cells.FromKey("OrderQuantity").Value == null || m_Grid.Rows[count].Cells.FromKey("OrderQuantity").ToString() =="")
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("OrderQuantity").ToString());
			if(m_Grid.Rows[count].Cells.FromKey("ApplyUnitCost").Value == null || m_Grid.Rows[count].Cells.FromKey("ApplyUnitCost").ToString() =="")
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("ApplyUnitCost").ToString());
			
			if(m_Grid.Rows[count].Cells.FromKey("TotalCost").ToString() == null || m_Grid.Rows[count].Cells.FromKey("TotalCost").ToString() =="")
				comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("TotalCost").ToString());
			if(m_Grid.Rows[count].Cells.FromKey("RemainQuantity").ToString() == null || m_Grid.Rows[count].Cells.FromKey("RemainQuantity").ToString().Trim() == "")	
				comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = Decimal.Parse(m_Grid.Rows[count].Cells.FromKey("RemainQuantity").ToString());
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName;
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID;				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			//comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[count].Cells.FromKey("OutSideOrderRequestHistoryIndex").ToString());
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = Convert.DBNull;

			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 대기시 진행 중단
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void OO_HTStop(SqlConnection con, SqlTransaction trans, int count)
		{
			string str =" UPDATE OO_HT SET " +
				" ProgressCondition = '중단', "+
				" UpdatingPerson = @person, "+
				" UpdatingPersonID = @personid, "+
				" UpdatingDate = @date WHERE OutSideOrderHistoryIndex= @INDEX ";
		
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@person", SqlDbType.VarChar).Value = m_UserName;
			comm.Parameters.Add("@personid", SqlDbType.VarChar).Value = m_UserID;
			comm.Parameters.Add("@date", SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[count].Cells.FromKey("OutSideOrderHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();	
		}
	}
}

