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
	/// 삭제를 위한 객체
	/// </summary>
	public class Delete
	{
		private string m_PageName;		//요청페이지를 담는 필드
		private string m_Table;			//테이블명을 담는필드
		private string m_Table1;		//이전테이블명을 담는필드
		private string m_Index;			//삭제할 레코드 인덱스 담는 필드
		private string m_Index1;		//이전원장 레코드인덱스 담는 필드
		private UltraWebGrid m_Grid;	//넘어온 그리드 저장 필드
		private string m_Distinction ;	//업무구분(모듈구분)
		private string m_Section;		//원장구분 필드
		private string m_UserName ;
		private string m_User;


		public Delete(UltraWebGrid grid, string PageName, string Name, string ID)
		{
			switch(PageName)
			{
				case "QualityInspectionPC":
					m_Distinction = "품질";
					break;
				case "OutSideDeliveryPC":					// 외주납품현황
					m_Table = "OSD_HT";
					m_Table1 = "ODR_HT";					
					m_Distinction = "외주";
					m_Index = "OutSideDeliveryHistoryIndex";
					m_Index1 = "OutSideDeliveryRequestHistoryIndex";			
					break;
				case "BuyingDeliveryPC":					// 구매납품현황
					m_Table = "BD_HT";
					m_Table1 = "BDR_HT";			
					m_Distinction = "구매";
					m_Index = "BuyingDeliveryHistoryIndex";
					m_Index1 = "BuyingDeliveryRequestHistoryIndex";
					break;
				case "SubBuyingDeliveryPC":
					m_Table = "SBD_HT";
					m_Distinction = "구매";
					m_Index = "SubBuyingDeliveryHistoryIndex";
					break;
				case "SaleHistoryRegistrationPC":			// 매출원장등록현황
					m_Table = "S_HT";
					m_Table1 = "OS_HT";
					m_Distinction = "영업";
					m_Index = "SaleHistoryIndex";
					m_Index1 = "OutStorehouseHistoryIndex";
					break;
				default :
					break;
			}
			m_Grid = grid;
			m_PageName = PageName.ToString();			
			m_UserName = Name;
			m_User = ID;

		}
		/// <summary>
		/// 삭제 생성자
		/// </summary>
		/// <param name="grid">요청페이지에서 넘어온 그리드</param>
		/// <param name="PageName">요청페이지명 </param>
		public Delete(UltraWebGrid grid, string PageName )
		{
			m_Grid = grid;
			m_PageName = PageName.ToString();

			
			switch(m_PageName)
			{
				case "ReceiveingPC":						// 수주현황
					m_Table = "RO_HT";
					m_Distinction = "영업";
					m_Index = "ReceivingOrderHistoryIndex";
					 
					break;				
				case "ProductionRequestPC":					// 생산의뢰현황
					m_Table = "PR_HT";
					m_Table1 = "RO_HT";
					m_Distinction = "영업";
					m_Index = "ProductionRequestHistoryIndex";
					m_Index1 = "ReceivingOrderHistoryIndex";
					break;
				case "GoodsBuyingRequestPC":				// 상품구매의뢰현황
					m_Table1 = "RO_HT";
					m_Table = "BR_HT";
					m_Distinction = "영업";
					m_Section ="수주";
					m_Index = "BuyingRequestHistoryIndex";
					m_Index1 = "HistoryIndex";
					break;
				case "BusinessStorehouseInStorehousePC":	// 영업창고입고현황
					m_Table = "IS_HT";
					m_Table1 = "ISR_HT";
					m_Distinction = "영업";
					m_Index = "InStorehouseHistoryIndex";
					m_Index1 = "InStorehouseRequestHistoryIndex";
					break;				
				case "GoodsManufactureOutStorehousePC":		// 상품제품출고현황
					m_Table = "OS_HT";
					m_Table1 = "RO_HT";
					m_Distinction = "영업";
					m_Index = "OutStorehouseHistoryIndex";
					m_Index1 = "ReceivingOrderHistoryIndex";
					break;
				case "SaleHistoryRegistrationPC":			// 매출원장등록현황
					m_Table = "S_HT";
					m_Table1 = "OS_HT";
					m_Distinction = "영업";
					m_Index = "SaleHistoryIndex";
					m_Index1 = "OutStorehouseHistoryIndex";
					break;
				case "CollectMoneyRegistrationPC":			// 수금현황
					m_Table = "CM_HT";
					m_Distinction = "영업";
					m_Index = "CollectMoneyHistoryIndex";
					break;				
				case "OtherInOutStorehousePC":				// 기타입출고현황
					m_Table = "OIOS_HT";
					m_Distinction = "영업";
					m_Index = "OtherInOutStorehouseHistoryIndex";
					break;				
				case "StorehouseMovingPC":					// 창고이동현황
					m_Table = "SM_HT";
					m_Distinction = "영업";
					m_Index = "StorehouseMovingHistoryIndex";
					break;
				case "ClaimRegistrationPC":					//클레임등록현황
					m_Table = "CL_HT";
					m_Distinction = "영업";
					m_Index = "ClameHistoryIndex";
					break;
				case "AddItemInStorePC":					// 보용품 입고 현황
					m_Table = "AS_HT";
					m_Distinction = "영업";
					m_Index = "AddItemInStoreIndex";
					break;



					// 생산

				case "ProductionPlanPC":					// 생산계획현황
					m_Table = "PP_HT";
					m_Table1 = "PR_HT";
					m_Distinction = "생산";
					m_Index = "ProductionPlanHistoryIndex";
					m_Index1 = "HistoryIndex";
					break;	
				case "WCPlanPC":							//WC작업계획현황
					m_Table = "WDWP_HT";
					m_Distinction = "생산";
					m_Index = "WCDailyWorkPlanHistoryIndex";
					break;
				
				case "WorkDailyReportRegistrationPC":		// 작업일보현황
					m_Table = "WDR_HT";
					m_Table1 = "WDWP_HT";
					m_Distinction = "생산";
					m_Index = "WorkDailyReportHistoryIndex";
					m_Index1 = "WCDailyWorkPlanHistoryIndex";
					break;				  
				case "RowMaterialRequriementPC":			// 원자재의뢰현황
					m_Table = "BR_HT";
					m_Table1 = "MRC_HT";
					m_Distinction = "생산";
					m_Index = "BuyingRequestHistoryIndex";
					m_Index1 = "MaterialRequirementCalculateHistoryIndex";
					break;
				case "OutsideRequestPC":					//외주의뢰현황
					m_Table = "OOR_HT";
					m_Table1 = "WDWP_HT";
					m_Distinction = "생산";
					m_Index = "OutSideOrderRequestHistoryIndex";
					m_Index1 = "ProductionPlanHistoryIndex";
					break;

				case "SubItemDeletePC":
					m_Table = "ST_HT";
					m_Distinction = "생산";
					m_Index = "SubItemThrowingIndex";
					break;

					//구매외주

				case "BuyingOrderPC":						// 구매발주현황
					m_Table = "BO_HT";
					m_Table1 = "BR_HT";
					m_Distinction = "구매";
					m_Index = "BuyingOrderHistoryIndex";
					m_Index1 = "BuyingRequestHistoryIndex";
					break;				
				case "BuyingDeliveryPC":					// 구매납품현황
					m_Table = "BD_HT";
					m_Table1 = "BDR_HT";			
					m_Distinction = "구매";
					m_Index = "BuyingDeliveryHistoryIndex";
					m_Index1 = "BuyingDeliveryRequestHistoryIndex";
					break;
				case "OutSideOrderPC":						// 외주발주현황
					m_Table = "OO_HT";
					m_Table1 = "OOR_HT";
					m_Distinction = "외주";
					m_Index = "OutSideOrderHistoryIndex";
					m_Index1 = "OutSideOrderRequestHistoryIndex";
					break;
				case "OutSideOutStorehousePC":				// 외주출고현황
					m_Table = "OOS_HT";
					m_Distinction = "외주";
					m_Index = "OutSideOutStorehouseHistoryIndex";
					break;				
				
				case "PaymentPlanResultRegistrationPC":					// 지급등록현황
					m_Table = "P_HT";
					m_Distinction = "외주";
					m_Index = "PaymentHistoryIndex";
					break;
				case "QualityInspectionPC":						//품질검사현황
					m_Distinction = "품질";
					break;
				case "ClaimPC":						//클레임
					m_Table = "PCL_HT";
					m_Distinction = "구매";
					m_Index = "ClameHistoryIndex";
					break;
				case "SubBuyingOrderPC":
					m_Table = "SBO_HT";
					m_Distinction = "구매";
					m_Index = "SubBuyingOrderHistoryIndex";
					break;
				case "RowItemExhaustPC":
					m_Table = "E_HT";
					m_Distinction = "생산";
					m_Index = "ExhaustHistoryIndex";
					break;
				case "EtcClaimPC":						//클레임
					m_Table = "ECL_HT";
					m_Distinction = "구매";
					m_Index = "EtcClameHistoryIndex";
					break;
				default :
					
					break;

			}
			
		}

		/// <summary>
		/// 월마감여부 
		/// </summary>
		/// <returns></returns>
		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			
			int year = 0;
			int month = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = m_Distinction.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();

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
					hc.Response.Write("alert('월마감이되어 취소가 불가능합니다.');");
					hc.Response.Write("</script>");
					return false;
				}
			}
			else
			{
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('월마감이되어 취소가 불가능합니다.');");
				hc.Response.Write("</script>");
				return false;
			}	
		}


		

		public UltraWebGrid MainRowDelete()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			
			
			SqlCommand comm = new SqlCommand();
			SqlTransaction Trans = conn.BeginTransaction();

			try
			{
				int a = 0;//선택항목있는지 판단

				// 월마감 체크후 중단이 가능한지 확인한다			
				if(MonthClosing() == true)
				{
					//체크 항목중 진행상태가 대기이거나 중단일때만 삭제 가능
					for(int i = 0; i < m_Grid.Rows.Count; i++)
					{
						// 체크되지 않으면 Value값이 null이다 그러므로 확인후 false값을 넣어준다.
						if(m_Grid.Rows[i].Cells.FromKey("chk").Value == null)
						{
							m_Grid.Rows[i].Cells.FromKey("chk").Value = false;
						}

						// 체크된것만 삭제한다.
						if(bool.Parse(m_Grid.Rows[i].Cells.FromKey("chk").Value.ToString()))
						{
							a = 1;
							// 수금등록과 지급등록원장, 기타입출고원장, 매출원장,창고이동원장, 작업계획원장, 클레임원장, 작업일보원장 에는 진행상태가 없다 그러므로 체크하지 않는다
							if(m_PageName.ToString() == "CollectMoneyRegistrationPC" || m_PageName.ToString() == "PaymentPlanResultRegistrationPC" || m_PageName.ToString() == "WorkDailyReportRegistrationPC" 
								|| m_PageName.ToString() == "OtherInOutStorehousePC" || m_PageName.ToString() == "StorehouseMovingPC" || m_PageName.ToString() == "SaleHistoryRegistrationPC"
								|| m_PageName.ToString() == "ClaimRegistrationPC" || m_PageName == "SubItemDeletePC" || m_PageName.ToString() == "ClaimPC" || m_PageName.ToString() == "EtcClaimPC" || m_PageName.ToString() == "AddItemInStorePC" || m_PageName == "RowItemExhaustPC")
							{
								// 각원장 삭제함수
								//if(MonthClosing(i))
								RowDelete(conn,Trans,i);
								//else
								//	throw new Exception("선택항목 "+Item+"은 월 마감이 되어 삭제가 불가능 합니다!");

							}
							else if(m_PageName == "WorkDailyReportRegistrationPC")
							{
								//체크 항목중 진행상태가 대기이거나 중단일때만 삭제 가능
								if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Text == "완료" || m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Text == "진행" || m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Text == "지시")
								{
									string Item = m_Grid.Rows[i].Cells.FromKey("ItemNum").Text;
									throw new Exception("선택항목 중 "+Item+"은 삭제할수 진행상태가 대기가 아니어서 삭제가 불가능 합니다!");
								}
								else
								{
									// 각원장 삭제함수
									RowDelete(conn,Trans,i);
								}
							}
							else if(m_PageName == "BuyingDeliveryPC" || m_PageName == "OutSideDeliveryPC" || m_PageName == "BusinessStorehouseInStorehousePC" || m_PageName == "GoodsManufactureOutStorehousePC" ||
								m_PageName == "GoodsBuyingRequestPC")
							{
								//체크 항목중 진행상태가 대기이거나 중단일때만 삭제 가능
								if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Text == "완료")
								{
									//자산분류가 원자재이며 무검사인경우 삭제
									if(Division(i) == 1 && m_Grid.Rows[i].Cells.FromKey("CheckDistinction").Text == "False")
									{
										RowDelete(conn,Trans,i);
									}
									else
									{
										string Item = m_Grid.Rows[i].Cells.FromKey("ItemNum").Text;
										throw new Exception("선택항목 중 "+Item+"은 진행상태가 완료가 되어 삭제가 불가능 합니다!");
									}
								}
								else
								{
									// 각원장 삭제함수
									RowDelete(conn,Trans,i);
								}

							}
							else if(m_PageName == "QualityInspectionPC")
							{
								//체크 항목중 진행상태가 검사완료일때만 취소 가능
								if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Text == "검사완료")
								{
									//품질검사 취소함수
									InspectioRowDelete(conn,Trans,i);
								}
								else if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Text == "대기")
								{	
									string Item = m_Grid.Rows[i].Cells.FromKey("ItemNum").Text;
									throw new Exception("선택항목 중 "+Item+"은 품질검사를 하지 않은 상태입니다!");
								}
								else
								{	
									string Item = m_Grid.Rows[i].Cells.FromKey("ItemNum").Text;
									throw new Exception("선택항목 중 "+Item+"은 이미 입고완료되어 검사취소가 불가능 합니다!");
								}
							}
							else
							{
								//체크 항목중 진행상태가 대기이거나 중단일때만 삭제 가능
								if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Text == "대기")
								{
									// 각원장 삭제함수
									RowDelete(conn,Trans,i);
								}
								else
								{	
									string Item = m_Grid.Rows[i].Cells.FromKey("ItemNum").Text;
									throw new Exception("선택항목 중 "+Item+"은 진행상태가 대기가 아니어서 삭제가 불가능 합니다!");
								}
							}

						}
					}

					if(a == 0)
					{
						HttpContext hc = HttpContext.Current;
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('항목을 선택해 주세요!');");
						hc.Response.Write("</script>");

					}
					else 
					{
						HttpContext hc = HttpContext.Current;
						if(m_PageName == "QualityInspectionPC")
						{
							hc.Response.Write("<script language=javascript>");
							hc.Response.Write("alert('품질검사가 취소 되었습니다!');");
							hc.Response.Write("</script>");
						}
						else
						{
						
							hc.Response.Write("<script language=javascript>");
							hc.Response.Write("alert('삭제 되었습니다!');");
							hc.Response.Write("</script>");						
						}
					}

					// 등록을 하고난뒤에는 체크박스 상자를 false로 바꾸어준다
					for(int i = 0; i < m_Grid.Rows.Count; i++)
					{
						m_Grid.Rows[i].Cells[0].Value = false;
					}
				}
				else
				{
					throw new Exception("월마감이 되어 삭제가 불가능 합니다!");
				}

				Trans.Commit();
			}
			catch(Exception ee)
			{
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");

				Trans.Rollback();
			}
			finally
			{
				conn.Close();
			}
			return m_Grid;
		}



		/// <summary>
		/// 품질검사 취소
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void InspectioRowDelete(SqlConnection con,SqlTransaction trans, int count)
		{
			string Check = "";
			string str = @"Select CheckDistinction From II_Mt where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",m_Grid.Rows[count].Cells.FromKey("ItemNum").Text);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Check = dr["CheckDistinction"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();

			if(Check == "")
				throw new Exception("품목 "+m_Grid.Rows[count].Cells.FromKey("ItemNum").Text+"이 품목정보테이블에 존재하지 않습니다!");
			else if(Check == "False")
			{
				throw new Exception("품목 "+m_Grid.Rows[count].Cells.FromKey("ItemNum").Text+"은 무검사품이므로 품질검사취소를 할수 없습니다!");
			}
			else
			{
				if(MonthClosing(count))
				{
					if(m_Grid.Rows[count].Cells.FromKey("historysection1").Text == "구매")
					{
						//구매인경우
						//구매발주원장 입고수량을 합격수량 만큼 마이너스
						//구매발주원장의 진행상태변경(수량변경후 잔량이 발주량과 동일하면 대기, 아니면 진행)
						BuyingOrderQuantityUpdate(con,trans,count);
						//원자재 창고수량 입고수량 마이너스 증가
						if(Division(m_Grid.Rows[count].Cells.FromKey("ItemNum").Text) == 1)
							RowStockQuantityUpdate(con,trans,count);
						else//상품인 경우 생산창고 입고수량 마이너스 증가
							ComStockQuantityUpdate(con,trans,count);

						BOS_HTUpdate(con,trans,count);

						//구매납품원장 진행상태 변경
						BD_HTUpdate(con,trans,count);

						BuyingOrderInStoreWaitingQuantityUpdate(con,trans,count);
						ISRDelete(con,trans,count,"품질검사");
						//매입매출테이블 금액수정
						CancleBSI_MT(con,trans,count);
					}
					else if(m_Grid.Rows[count].Cells.FromKey("historysection1").Text == "외주")
					{
						//외주인경우
						//외주발주원장 입고수량을 합격수량만큼 마이너스
						//외주발주원장의 진행상태변경(수량변경후 잔량이 발주량과 동일하면 대기, 아니면 진행)
						OutSideOrderQuantityUpdate(con,trans,count);
						//생산창고수량 입고수량 마이너스증가
						if(!WorkPlan3() || (StoreManage(con,trans,m_Grid.Rows[count].Cells.FromKey("ItemNum").Text)=="예"))
							ProductStockQuantityUpdate(con,trans,count);
//						else
//						{
//							ProductStockQuantityUpdate(con,trans,count);
//							BS_MTUpdate(con,trans,count);
//						}

						//외주창고 수량 출고수량 마이너스 증가
						OutStockQuantityUpdate(con,trans,count);
				
						BOS_HTOutUpdate(con,trans,count);
						//외주납품원장 진행상태변경
						OSD_HTUpdate(con,trans,count);

						OutSideOrderInStoreWaitingQuantityUpdate(con,trans,count);
						ISRDelete(con,trans,count,"품질검사");
						//매입매출테이블 금액수정
						CancleBSI_MT(con,trans,count);

						QuantityUpdate(con,trans,count);
					}
						//자가인경우
					else
					{
						ISRDelete(con,trans,count,"작업일보");
						SelfQuantityUpdate(con,trans,count);
					}

				
				
					//품질검사원장 진행상태변경(검사완료-> 대기)
					QualityInspectionUpdate(con,trans,count);
					DelSH_HT(con,trans,count);
				}
				else
					throw new Exception("월 마감이 되어 품질검사취소를 할수 없습니다!");
			}
		}


		private bool WorkPlan3()
		{
			bool Work = false;
			// 현재 설정되어있는 각종 정보를 가져옴
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();

			string strSQL = @"SELECT WorkPlan3 FROM SCS_T";
			SqlCommand comm = new SqlCommand(strSQL, conn);

			SqlDataReader reader = comm.ExecuteReader();
			if(reader.Read())
			{
				Work = bool.Parse(reader["WorkPlan3"].ToString());
			}
			conn.Close();

			return Work;

		}


		/// <summary>
		/// 해당 품목의 공정이 최종공정인지 파악
		/// </summary>
		/// <returns></returns>
		private bool LastProcess(SqlConnection conn,SqlTransaction tr,int count)
		{
			string str = "Select count(*) From PSI_MT Where RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @Code and ProcessSequenceNum > @num";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",m_Grid.Rows[count].Cells.FromKey("ItemNum").Text);
			comm.Parameters.Add("@Code",m_Grid.Rows[count].Cells.FromKey("종료공정코드").Text);
			comm.Parameters.Add("@num",int.Parse(m_Grid.Rows[count].Cells.FromKey("종료공정순서").Text));
			int cnt = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			if(cnt == 0)
				return true;
			else
				return false;
		}


		/// <summary>
		/// 외주입고시 취소함수
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void QuantityUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			string  str ;

			// 최종공정이면 생산계획원장에 완료수량을 변경
			// 진행상태 변경
			if(LastProcess(conn,tr,rowcount))
			{
				int idx = 0;
				str = "Select HistoryIndex2 From QI_HT Where HistorySection2 = '외주발주' and QualityInspectionHistoryIndex = @index";
				comm.CommandText = str;
				comm.Parameters.Add("@index",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionHistoryIndex").Value.ToString()));
				SqlDataReader reader = comm.ExecuteReader();
				while(reader.Read())
				{
					idx = int.Parse(reader["HistoryIndex2"].ToString());
				}
				reader.Close();
				comm.Parameters.Clear();

				if(idx != 0)
				{
					string number = "";
					str = "Select OutSideOrderRequestHistoryIndex From OO_HT where OutSideOrderHistoryIndex = @index";
					comm.CommandText = str;
					comm.Parameters.Add("@index",idx);
					SqlDataReader dr1 = comm.ExecuteReader();
					while(dr1.Read())
					{
						number = dr1["OutSideOrderRequestHistoryIndex"].ToString();
					}
					dr1.Close();
					comm.Parameters.Clear();
					if(number != "")
					{
						str = "select ProductionPlanHistoryIndex From OOR_HT where OutSideOrderRequestHistoryIndex = @index";
						comm.CommandText = str;
						comm.Parameters.Add("@index",number);
						SqlDataReader dr3 = comm.ExecuteReader();
						string num = "";
						while(dr3.Read())
						{
							num = dr3["ProductionPlanHistoryIndex"].ToString();
						}
						dr3.Close();
						comm.Parameters.Clear();

						if(num != "")
						{

							str = "Update PP_HT Set  ProductionCompleteQuantity = ProductionCompleteQuantity - @quantity Where ProductionPlanHistoryIndex = @index";
							comm.CommandText = str;
							comm.Parameters.Add("@quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Text));
							comm.Parameters.Add("@index",int.Parse(num));
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();

							
							str = "Select (ProductionPlanQuantity - ProductionCompleteQuantity) as Quantity From PP_HT Where ProductionPlanHistoryIndex = @index";
							comm.CommandText = str;
							comm.Parameters.Add("@index",int.Parse(num));

							decimal Quantity1 = 0;
							SqlDataReader dr2 = comm.ExecuteReader();
							while(dr2.Read())
							{
								Quantity1 = decimal.Parse(dr2["Quantity"].ToString());
							}
							dr2.Close();
							comm.Parameters.Clear();	
							//
							if(Quantity1 <= 0)
							{
								str = "Update PP_HT Set ProgressCondition = '완료'  Where ProductionPlanHistoryIndex = @index";
								comm.CommandText = str;
								comm.Parameters.Add("@index",int.Parse(num));
								comm.ExecuteNonQuery();
								comm.Parameters.Clear();
							}
							else
							{
								str = "Update PP_HT Set ProgressCondition = '진행'  Where ProductionPlanHistoryIndex = @index";
								comm.CommandText = str;
								comm.Parameters.Add("@index",int.Parse(num));
								comm.ExecuteNonQuery();
								comm.Parameters.Clear();
							}
						}
					}
				}
			}

		}




		/// <summary>
		/// 자가 취소
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void SelfQuantityUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			string str  ;

			//작업일보 번호를 찾아 생산계획번호를 가져오고
			//이 생산계획번호를 가지고 완료수량을 누적한다.
			//누적완료수량이  생산계획수량보다 많으면 생산계획원장 진행상태를 완료로 처리

			int idx = 0;
			str = "Select HistoryIndex2 From QI_HT Where HistorySection2 = '작업계획' and QualityInspectionHistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@index",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionHistoryIndex").Value.ToString()));
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				idx = int.Parse(dr["HistoryIndex2"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();

			if(idx != 0)
			{
				string num = "";
				str = "Select ProductionPlanHistoryIndex From WDWP_HT where WCDailyWorkPlanHistoryIndex = @index";
				comm.CommandText = str;
				comm.Parameters.Add("@index",idx);
				SqlDataReader dr1 = comm.ExecuteReader();
				while(dr1.Read())
				{
					num = dr1["ProductionPlanHistoryIndex"].ToString();
				}
				dr1.Close();
				comm.Parameters.Clear();
				if(num != "")
				{
					str = "Update PP_HT Set  ProductionCompleteQuantity = ProductionCompleteQuantity - @quantity Where ProductionPlanHistoryIndex = @index";
					comm.CommandText = str;
					comm.Parameters.Add("@quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Text));
					comm.Parameters.Add("@index",int.Parse(num));
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					str = "Select (ProductionPlanQuantity - ProductionCompleteQuantity) as Quantity From PP_HT Where ProductionPlanHistoryIndex = @index";
					comm.CommandText = str;
					comm.Parameters.Add("@index",int.Parse(num));

					decimal Quantity1 = 0;
					SqlDataReader dr2 = comm.ExecuteReader();
					while(dr2.Read())
					{
						Quantity1 = decimal.Parse(dr2["Quantity"].ToString());
					}
					dr2.Close();
					comm.Parameters.Clear();

					//
					if(Quantity1 > 0)
					{
						str = "Update PP_HT Set ProgressCondition = '진행'  Where ProductionPlanHistoryIndex = @index";
						comm.CommandText = str;
						comm.Parameters.Add("@index",int.Parse(num));
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();
					}
					else
					{
						str = "Update PP_HT Set ProgressCondition = '완료'  Where ProductionPlanHistoryIndex = @index";
						comm.CommandText = str;
						comm.Parameters.Add("@index",int.Parse(num));
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();
					}
				}
			}

			
		}


		private void DelSH_HT(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			string str = @"Delete From SH_HT Where HistoryDivision = '품질검사' and HistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@index",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionHistoryIndex").Text));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

		}


		/// <summary>
		/// 외주납품 삭제이력 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void BOS_HTOutUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,'삭제', @Date,@Person, @PersonID,'외주납품',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			
			comm.Parameters.Add("@itemnum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
			comm.Parameters.Add("@itemdrawnum", m_Grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);							//도면번호
			comm.Parameters.Add("@itemname",m_Grid.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
			comm.Parameters.Add("@CompanyName",m_Grid.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",m_Grid.Rows[rowcount].Cells.FromKey("BusinessCompanyNum").Text);
			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("종료공정순서").Text));							//공정순서
			comm.Parameters.Add("@ProcessCode",m_Grid.Rows[rowcount].Cells.FromKey("종료공정코드").Text);						//공정코드
			comm.Parameters.Add("@ProcessName",m_Grid.Rows[rowcount].Cells.FromKey("종료공정명").Text);							//공정명
			comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Value.ToString()));//합격수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
			comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());
			comm.Parameters.Add("@Person",m_UserName.ToString());
			comm.Parameters.Add("@PersonID", m_User.ToString());	
			comm.Parameters.Add("@HistoryIndex",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex1").Text));												//원장번호
			
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}


		/// <summary>
		/// 무검사품 구매납품 삭제이력원장 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void BOS_HTUnCheckedUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			
			
			if(Division(m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text) == 1)
			{
				string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) 
			values(@ItemNum,@ItemDrawNum,@ItemName,'0','14000000','소재',@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,'삭제', @Date,@Person, @PersonID,'구매납품',@HistoryIndex)";
			
				comm.Parameters.Add("@ItemNum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
				comm.Parameters.Add("@ItemDrawNum", m_Grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);							//도면번호
				comm.Parameters.Add("@ItemName",m_Grid.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
//				comm.Parameters.Add("@ProcessSequenceNum",0);							//공정순서
//				comm.Parameters.Add("@ProcessCode","14000000");						//공정코드
//				comm.Parameters.Add("@ProcessName","소재");							//공정명
				comm.Parameters.Add("@CompanyName",m_Grid.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
				comm.Parameters.Add("@BusinessRegistrationNum",m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text);
				comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));//합격수량
				comm.Parameters.Add("@UnitCost",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
				comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());//외주출고일자
				comm.Parameters.Add("@Person",m_UserName.ToString());
				comm.Parameters.Add("@PersonID", m_User.ToString());	
				comm.Parameters.Add("@HistoryIndex",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Text));	
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
			{
				string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) 
			values(@ItemNum,@ItemDrawNum,@ItemName,'99','14009999','최종품',@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,'삭제', @Date,@Person, @PersonID,'구매납품',@HistoryIndex)";
			
				comm.Parameters.Add("@ItemNum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
				comm.Parameters.Add("@ItemDrawNum", m_Grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);							//도면번호
				comm.Parameters.Add("@ItemName",m_Grid.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
//				comm.Parameters.Add("@ProcessSequenceNum",99);							//공정순서
//				comm.Parameters.Add("@ProcessCode","14009999");						//공정코드
//				comm.Parameters.Add("@ProcessName","최종품");							//공정명
				comm.Parameters.Add("@CompanyName",m_Grid.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
				comm.Parameters.Add("@BusinessRegistrationNum",m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text);
				comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));//합격수량
				comm.Parameters.Add("@UnitCost",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
				comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());//외주출고일자
				comm.Parameters.Add("@Person",m_UserName.ToString());
				comm.Parameters.Add("@PersonID", m_User.ToString());	
				comm.Parameters.Add("@HistoryIndex",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Text));	
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
														//원장번호

			
		}

		/// <summary>
		/// 검사품 구매납품 삭제이력 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void BOS_HTUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			
			
			if(Division(m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text) == 1)
			{
				string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,'0','14000000','소재',@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,'삭제', @Date,@Person, @PersonID,'구매납품',@HistoryIndex)";
			
				comm.Parameters.Add("@itemnum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
				comm.Parameters.Add("@itemdrawnum", m_Grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);							//도면번호
				comm.Parameters.Add("@itemname",m_Grid.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
				comm.Parameters.Add("@CompanyName",m_Grid.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
				comm.Parameters.Add("@BusinessRegistrationNum",m_Grid.Rows[rowcount].Cells.FromKey("BusinessCompanyNum").Text);
//				comm.Parameters.Add("@ProcessSequenceNum",0);							//공정순서
//				comm.Parameters.Add("@ProcessCode","14000000");							//공정코드
//				comm.Parameters.Add("@ProcessName","소재");								//공정명
				comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Value.ToString()));//합격수량
				comm.Parameters.Add("@UnitCost",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
				comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());
				comm.Parameters.Add("@Person",m_UserName.ToString());
				comm.Parameters.Add("@PersonID", m_User.ToString());	
				comm.Parameters.Add("@HistoryIndex",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex1").Text));												//원장번호
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
			{
				string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,'99','14009999','최종품',@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,'삭제', @Date,@Person, @PersonID,'구매납품',@HistoryIndex)";
			
				comm.Parameters.Add("@itemnum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
				comm.Parameters.Add("@itemdrawnum", m_Grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);							//도면번호
				comm.Parameters.Add("@itemname",m_Grid.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
				comm.Parameters.Add("@CompanyName",m_Grid.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
				comm.Parameters.Add("@BusinessRegistrationNum",m_Grid.Rows[rowcount].Cells.FromKey("BusinessCompanyNum").Text);
//				comm.Parameters.Add("@ProcessSequenceNum",99);							//공정순서
//				comm.Parameters.Add("@ProcessCode","14009999");						//공정코드
//				comm.Parameters.Add("@ProcessName","최종품");							//공정명
				comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Value.ToString()));//합격수량
				comm.Parameters.Add("@UnitCost",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
				comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());
				comm.Parameters.Add("@Person",m_UserName.ToString());
				comm.Parameters.Add("@PersonID", m_User.ToString());	
				comm.Parameters.Add("@HistoryIndex",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex1").Text));												//원장번호
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			
		}

		/// <summary>
		/// 매입매출 금액 수정
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void CancleBSI_MT(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

//			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Year;
//			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Month;
			int year = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Year").Text);
			int mon = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Month").Text);
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessCompanyNum").Text;
            comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Text) * decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Text));
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);//
			comm.CommandText = table.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
		}
		
		private void ISRDelete(SqlConnection conn, SqlTransaction tr, int rowcount,string Section)
		{
			string State = "";
			string Item = "";
			string str  = "Select ProgressCondition,ItemNum From ISR_HT Where HistoryIndex = @index and HistorySection= @Section";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionHistoryIndex").Value.ToString());
			comm.Parameters.Add("@Section",Section);
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				State = dr["ProgressCondition"].ToString();
				Item = dr["ItemNum"].ToString();
			}
			dr.Close();
			if(State == "대기")
			{
				str = "Delete From ISR_HT Where HistoryIndex = @index and HistorySection= @Section";
			
				comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionHistoryIndex").Value.ToString());
				comm.Parameters.Add("@Section",Section);
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else if(State == "완료")
				throw new Exception("품목번호 "+Item+"인 품목은 이미 입고완료되어 삭제가 불가능 합니다!");
		}

		/// <summary>
		/// 구매발주 원장 잔량 변경
		/// 현재 잔량에서 합격수량을 더하면 된다
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void BuyingOrderQuantityUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			//먼저 총발주량을 구한다. 그런다음 변경된 잔량과 비교해서 발주량과 동일하면 진행상태는 대기, 그렇지 않으면 진행으로 업데이트한다
			decimal Order = 0;
			decimal Remain = 0;
			string str = "Select OrderQuantity From BO_HT Where BuyingOrderHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex2").Text);
			SqlDataReader dr_Order = comm.ExecuteReader();
			while(dr_Order.Read())
			{
				Order = decimal.Parse(dr_Order["OrderQuantity"].ToString());
			}
			dr_Order.Close();
			comm.Parameters.Clear();

			str = "UPDATE BO_HT SET RemainQuantity = RemainQuantity + @RemainQuantity WHERE BuyingOrderHistoryIndex = @idx";
			comm.CommandText = str;
			comm.Parameters.Add("@RemainQuantity", decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Text));//합격수량
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex2").Text);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			
			str = "Select RemainQuantity From BO_HT Where BuyingOrderHistoryIndex = @idx";
			comm.CommandText = str;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex2").Text);
			SqlDataReader dr_Remain = comm.ExecuteReader();
			while(dr_Remain.Read())
			{
				Remain = decimal.Parse(dr_Remain["RemainQuantity"].ToString());
			}
			dr_Remain.Close();
			comm.Parameters.Clear();

			if(Order == Remain)
				str = "Update BO_HT Set ProgressCondition = '대기' Where BuyingOrderHistoryIndex = @idx";
			else
				str = "Update BO_HT Set ProgressCondition = '진행'  Where BuyingOrderHistoryIndex = @idx";
			comm.CommandText = str;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex2").Text);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

		}


		private void ComStockQuantityUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Month;
			
			Table table = new Table(year,mon);
			decimal Quantity = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Text);
			decimal Cost = TotalCost(conn,tr,rowcount,Quantity);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = table.InProductionTable();
			comm.Parameters.Add("@quantity",-Quantity);
			comm.Parameters.Add("@cost",-Cost);
			comm.Parameters.Add("@num",m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);
			comm.Parameters.Add("@code","14009999");
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리가 불가능 합니다.");
				}
			}
		}

		/// <summary>
		/// 원자재 창고 입고수량 마이너스
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void RowStockQuantityUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Month;
			
			Table table = new Table(year,mon);
			decimal Quantity = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Text);
			decimal Cost = TotalCost(conn,tr,rowcount,Quantity);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = table.InRowTable();
			comm.Parameters.Add("@quantity",-Quantity);
			comm.Parameters.Add("@cost",-Cost);
			comm.Parameters.Add("@num",m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리가 불가능 합니다.");
				}
			}
		}

		/// <summary>
		/// 원자재 재금액
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		/// <param name="total"></param>
		private decimal TotalCost(SqlConnection con,SqlTransaction trans, int count,decimal total)
		{
			decimal cost  = -1;
			string str = "Select StandardUnitCost From II_Mt Where ItemNum = @ItemNum and RecodingState = 1" ;
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@ItemNum",m_Grid.Rows[count].Cells.FromKey("ItemNum").Text);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				cost = decimal.Parse(dr["StandardUnitCost"].ToString())*total;
			}
			dr.Close();
			if(cost == -1)
				throw new Exception("품목 " + m_Grid.Rows[count].Cells.FromKey("ItemNum").Text + "이(가) 없습니다!");
			
			return cost;

		}

		/// <summary>
		/// 구매납품원장 진행상태 변경(완료=>대기)
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void BD_HTUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str = "Update BD_HT Set DeliveryQuantity = @DeliveryQuantity, ProgressCondition = '대기' Where BuyingDeliveryHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@DeliveryQuantity", decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text));//의뢰수량
			comm.Parameters.Add("@idx", int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex1").Text));//구매납품원장번호
			comm.ExecuteNonQuery();
		}

		


		/// <summary>
		/// 외주발주 원장 잔량 변경
		/// 현재 잔량에서 합격수량을 더하면 된다
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void OutSideOrderQuantityUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			//먼저 총발주량을 구한다. 그런다음 변경된 잔량과 비교해서 발주량과 동일하면 진행상태는 대기, 그렇지 않으면 진행으로 업데이트한다
			decimal Order = 0;
			decimal Remain = 0;
			string str = "Select OrderQuantity From OO_HT Where OutSideOrderHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex2").Text);
			SqlDataReader dr_Order = comm.ExecuteReader();
			while(dr_Order.Read())
			{
				Order = decimal.Parse(dr_Order["OrderQuantity"].ToString());
			}
			dr_Order.Close();
			comm.Parameters.Clear();

			str = "UPDATE OO_HT SET RemainQuantity = RemainQuantity + @RemainQuantity WHERE OutSideOrderHistoryIndex = @idx";
			comm.CommandText = str;
			comm.Parameters.Add("@RemainQuantity", decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Text));//합격수량
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex2").Text);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			
			str = "Select RemainQuantity From OO_HT Where OutSideOrderHistoryIndex = @idx";
			comm.CommandText = str;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex2").Text);
			SqlDataReader dr_Remain = comm.ExecuteReader();
			while(dr_Remain.Read())
			{
				Remain = decimal.Parse(dr_Remain["RemainQuantity"].ToString());
			}
			dr_Remain.Close();
			comm.Parameters.Clear();

			if(Order == Remain)
				str = "Update OO_HT Set ProgressCondition = '대기' Where OutSideOrderHistoryIndex = @idx";
			else
				str = "Update OO_HT Set ProgressCondition = '진행' Where OutSideOrderHistoryIndex = @idx";
			comm.CommandText = str;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex2").Text);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

		}



		/// <summary>
		/// 영업창고 입고수량 마이너스 수량 증가
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void BS_MTUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Month;
		
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;



			Table table = new Table(year,mon);
			

			decimal Quantity = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Text);
			decimal Cost = TotalCost(conn,tr,rowcount,Quantity);
			
			comm.CommandText = table.InBusinessTable();
			comm.Parameters.Add("@quantity",-Quantity);
			comm.Parameters.Add("@cost",-Cost);
			comm.Parameters.Add("@num",m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim());
			comm.Parameters.Add("@storenum",1);
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and BusinessStorehouseNum = 1 and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim();
				if(reader.Read())
				{
					if(decimal.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리가 불가능 합니다.");
				}
			}
		}

		/// <summary>
		/// 생산창고 입고수량 마이너스 증가 
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void ProductStockQuantityUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Month;
		
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;



			Table table = new Table(year,mon);
			//공정코드
			string ProcessCode = "";
			//진척율
			decimal Rate = 100;
			string str = "Select ProgressRate,ProcessCode From PSI_MT Where RecodingState = 1 and ItemNum = @itemnum and ProcessSequenceNum = @sequence";
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim();
			comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("종료공정순서").Text.Trim());//종료공정순서
			comm.CommandText = str;
			SqlDataReader dr_rate = comm.ExecuteReader();
			if(dr_rate.Read())
			{
				if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
					Rate = 100;
				else
					Rate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
				ProcessCode = dr_rate["ProcessCode"].ToString();
			}
			dr_rate.Close();
			comm.Parameters.Clear();

			decimal Quantity = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("적합수량").Text);
			decimal Cost = TotalCost(conn,tr,rowcount,Quantity);
			

			
			comm.CommandText = table.InProductionTable();
			comm.Parameters.Add("@quantity",-Quantity);
			comm.Parameters.Add("@cost",-Cost*Rate/100);
			comm.Parameters.Add("@num",m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim());
			comm.Parameters.Add("@code",ProcessCode);
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim();
				comm.Parameters.Add("@code",ProcessCode);
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim();
				if(reader.Read())
				{
					if(decimal.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리가 불가능 합니다.");
				}
			}
		}
		
		/// <summary>
		/// 외주창고 출고수량 마이너스 증가
		/// 수량은 의뢰수량만큼 마이너스 증가
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void OutStockQuantityUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			//시작공정코드,시작공정순서
			string begincode = "";
			int beginsequence =int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("시작공정순서").Text.Trim());
			string itemnum = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text.Trim();

			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Month;
		

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			string str = "Select ProcessCode From PSI_MT Where RecodingState = 1 and ItemNum = @itemnum and ProcessSequenceNum = @sequence";
			comm.Parameters.Add("@itemnum",itemnum);
			comm.Parameters.Add("@sequence",beginsequence);//시작공정순서
			comm.CommandText = str;
			SqlDataReader dr_Code = comm.ExecuteReader();
			if(dr_Code.Read())
			{
				begincode = dr_Code["ProcessCode"].ToString();
			}
			dr_Code.Close();
			comm.Parameters.Clear();

			//해당 품목이 가장 낮은 공정이면 하위품목들의 최상위공정을 떨어주고
			//그렇지 않으면 해당공정의 바로 아래공정품을 외주창고에서 떨어낸다
			//이때 하위품목의 자산분류가 원자재이면 해당 품목을 떨어준다

			decimal m_cost = 0;
			decimal m_progressrate = 100;

			str = @"Select isnull(Min(ProcessSequenceNum),0) From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum < @sequence";
			comm.Parameters.Add("@num",itemnum);
			comm.Parameters.Add("@sequence",beginsequence);
			comm.CommandText = str;
			

			if(int.Parse(comm.ExecuteScalar().ToString()) == 0)
			{
				comm.Parameters.Clear();
				//공정순서에 가서 해당자품목을 떨어준다
				str = @"Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @num";
				comm.Parameters.Add("@num",itemnum);
				comm.CommandText = str;
				DataSet ds = new DataSet();
				SqlDataAdapter da = new SqlDataAdapter(comm);
				da.Fill(ds);
				comm.Parameters.Clear();
				foreach(DataRow dr in ds.Tables[0].Rows)
				{
					//하위품목이 원자재이면 바로 떨어준다
					if(Division(dr["ChildItemNum"].ToString()) == 1)
					{
						// 품목의 금액
						string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
						SqlCommand comm_cost = new SqlCommand(str_cost,conn);
						comm_cost.Transaction = tr;
						comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
						if(dr_cost.Read())
							m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
						dr_cost.Close();

						
						decimal a = 0;
						str = @"Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @parent and ChildItemNum = @child";
						SqlCommand comm1 = new SqlCommand(str,conn);
						comm1.Transaction = tr;
						comm1.Parameters.Add("@parent",itemnum);
						comm1.Parameters.Add("@child",dr["ChildItemNum"].ToString());
						SqlDataReader dr_1 =  comm1.ExecuteReader();
						while(dr_1.Read())
						{
							a = decimal.Round((decimal.Parse(dr_1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr_1["NeedQuantityDenominator"].ToString())),3);
						}
						dr_1.Close();
						comm1.Parameters.Clear();

						if(a == 0)
							throw new Exception(itemnum + " 품목의 품목구성이 없거나 올바르지 않습니다!");
						

						//외주창고 출고수량 마이너스 증가
		
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(a *decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text)* m_cost * m_progressrate/100);
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(a* decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text));
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm.Parameters.Add("@code", SqlDbType.VarChar).Value = "14000000";
						comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessCompanyNum").Text;
						comm.Parameters.Add("@year",year);

						Table table = new Table(year,mon);
						comm.CommandText = table.OutTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  dr["ChildItemNum"].ToString();
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = "14000000";
							comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessCompanyNum").Text;
							comm.Parameters.Add("@year",DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
							comm.Parameters.Clear();
						
							string StockQuantity = "False";
							string ItemName = dr["ChildItemNum"].ToString();
							if(reader.Read())
							{
								if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
								{
									StockQuantity = "True";
								}
							}
							reader.Close();

							if(StockQuantity == "True")
							{
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}

						}

					}
					else
					{
						//공정품이면 자품목의 최대공정을 떨어준다.
						string code = "";
						str = @"Select ProcessCode, ProcessSequenceNum From PSI_MT Where RecodingState = 1 and 
							ItemNum = @num and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT 
							Where RecodingState = 1 and ItemNum = @num)";
						comm.Parameters.Add("@num",dr["ChildItemNum"].ToString());
						comm.CommandText = str;
						SqlDataReader dr_Code1 = comm.ExecuteReader();
						while(dr_Code1.Read())
						{
							code = dr_Code1["ProcessCode"].ToString();
						}
						dr_Code1.Close();
						comm.Parameters.Clear();

						// 품목의 금액
						string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
						SqlCommand comm_cost = new SqlCommand(str_cost,conn);
						comm_cost.Transaction = tr;
						comm_cost.Parameters.Add("@num",dr["ChildItemNum"].ToString());
						SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
						if(dr_cost.Read())
							m_cost = decimal.Parse(dr_cost["StandardUnitCost"].ToString());
						dr_cost.Close();

						// 품목의 진척율
						string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
						SqlCommand comm_rate = new SqlCommand(str_rate,conn);
						comm_rate.Transaction = tr;
						comm_rate.Parameters.Add("@num",dr["ChildItemNum"].ToString());
						comm_rate.Parameters.Add("@code",code);
						SqlDataReader dr_rate = comm_rate.ExecuteReader();			
						if(dr_rate.Read())
							m_progressrate = decimal.Parse(dr_rate["ProgressRate"].ToString());
						dr_rate.Close();
						comm_rate.Parameters.Clear();

						//자품목분자/분모량
						decimal a = 0;
						str = @"Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @parent and ChildItemNum = @child";
						SqlCommand comm1 = new SqlCommand(str,conn);
						comm1.Transaction = tr;
						comm1.Parameters.Add("@parent",itemnum);
						comm1.Parameters.Add("@child",dr["ChildItemNum"].ToString());
						SqlDataReader dr_1 =  comm1.ExecuteReader();
						while(dr_1.Read())
						{
							a = decimal.Round((decimal.Parse(dr_1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr_1["NeedQuantityDenominator"].ToString())),3);
							//a = decimal.Parse(dr_1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr_1["NeedQuantityDenominator"].ToString());
						}
						dr_1.Close();
						comm1.Parameters.Clear();

						if(a == 0)
							throw new Exception(itemnum + " 품목의 품목구성이 없거나 올바르지 않습니다!");

						//기존테이블에서 이전수량을 마이너스 증가
		
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(a *decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text)*m_cost * m_progressrate/100);
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(a* decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text));
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
						comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessCompanyNum").Text;
						comm.Parameters.Add("@year",year);

						Table table = new Table(year,mon);
						comm.CommandText = table.OutTable();
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();


						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  dr["ChildItemNum"].ToString();
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
							comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessCompanyNum").Text;
							comm.Parameters.Add("@year",DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
							comm.Parameters.Clear();
						
							string StockQuantity = "False";
							string ItemName = dr["ChildItemNum"].ToString();
							if(reader.Read())
							{
								if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
								{
									StockQuantity = "True";
								}
							}
							reader.Close();

							if(StockQuantity == "True")
							{
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}

						}

						

					}
				}
				

			}
			else
			{
				comm.Parameters.Clear();
				//해당품목의 바로 아래 공정을 떨어준다
				string code = "";
				str = @"Select ProcessCode, ProcessSequenceNum From PSI_MT Where RecodingState = 1 and 
							ItemNum = @num and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT 
							Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum < @sequence)";
				comm.Parameters.Add("@num",itemnum);
				comm.Parameters.Add("@sequence",beginsequence);
				comm.CommandText = str;				
				SqlDataReader dr_Code1 = comm.ExecuteReader();
				while(dr_Code1.Read())
				{
					code = dr_Code1["ProcessCode"].ToString();
				}
				dr_Code1.Close();
				comm.Parameters.Clear();

				// 품목의 금액
				string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
				SqlCommand comm_cost = new SqlCommand(str_cost,conn);
				comm_cost.Transaction = tr;
				comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum;
				SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
				if(dr_cost.Read())
					m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
				dr_cost.Close();

				// 품목의 진척율
				string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
				SqlCommand comm_rate = new SqlCommand(str_rate,conn);
				comm_rate.Transaction = tr;
				comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum;
				comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
				SqlDataReader dr_rate = comm_rate.ExecuteReader();			
				if(dr_rate.Read())
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
				dr_rate.Close();
				comm_rate.Parameters.Clear();

				//				//외주창고에 떨어줄 품목
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text)* m_cost * m_progressrate/100);
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text));
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = itemnum;
				comm.Parameters.Add("@code", SqlDbType.VarChar).Value = code;
				comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessCompanyNum").Text;
				comm.Parameters.Add("@year",year);

				Table table = new Table(year,mon);
				comm.CommandText = table.OutTable();
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();


				if(MinusStore() == false)
				{
					string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  itemnum;
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessCompanyNum").Text;
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
					comm.Parameters.Clear();						
					string StockQuantity = "False";
					string ItemName = itemnum;
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
					}
				}
			}
		}

		/// <summary>
		/// 외주납품원장 진행상태 및 입고의뢰수량 변경
		/// </summary>
		private void OSD_HTUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str = "Update OSD_HT Set DeliveryQuantity = @DeliveryQuantity, ProgressCondition = '대기' Where OutSideDeliveryHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@DeliveryQuantity", decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text));//외주납품원장번호
			comm.Parameters.Add("@idx", int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex1").Text));//외주납품원장번호
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 품질검사원장 진행상태 변경
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void QualityInspectionUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str = "Update QI_HT Set ProgressCondition = '대기', QualityInspectionCompleteDate = @date  Where QualityInspectionHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@date", Convert.DBNull);//
			comm.Parameters.Add("@idx", int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionHistoryIndex").Text));//
			comm.ExecuteNonQuery();
		}

		


		private bool MonthClosing(int rowcount)
		{
			int year = 0;
			int month = 0;

			int CloseYear = 0;
			int CloseMonth = 0;
			switch(m_PageName)
			{
				case "AddItemInStorePC":
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Text).Month;
					break;
				case "ReceiveingPC":
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Text).Month;
					break;
				case "BusinessStorehouseInStorehousePC":
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStoreDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStoreDate").Text).Month;
					break;
				case "ClaimRegistrationPC":
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Month;
					break;
				case "CollectMoneyRegistrationPC":						//수금등록현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("CollectMoneyDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("CollectMoneyDate").Text).Month;
					break;
				case "GoodsBuyingRequestPC":				// 상품구매의뢰현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Text).Month;
					break;	
				case "GoodsManufactureOutStorehousePC":					// 상품제품출고현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Month;
					break;
				case "SaleHistoryRegistrationPC":						// 매출원장등록현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleDate").Text).Month;
					break;	
				case "ProductionRequestPC":					// 생산의뢰현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate1").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate1").Text).Month;
					break;
							
				
				
				case "OtherInOutStorehousePC":				//기타입출고원
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Month;
					break;	
				case "StorehouseMovingPC":				//창고이
					CloseYear = 0;
					CloseMonth = 0;
					break;	
				
				case "ProductionPlanPC":					// 생산계획현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;
					break;	
				case "WorkDailyReportRegistrationPC":		// 작업일보현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkEndTime").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkEndTime").Text).Month;
					break;	
				case "RowMaterialRequriementPC":			// 원자재의뢰현황: 추가된것만 삭제가 가능
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Text).Month;
					break;	
				case "OutsideRequestPC":						//외주의뢰현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Text).Month;
					break;	
				case "SubItemDeletePC":				//부자
					CloseYear = 0;
					CloseMonth = 0;
					break;	
				case "BuyingDeliveryPC"://구매입고현황
					CloseYear = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Year").Text);
					CloseMonth = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Month").Text);
					break;	
				case "BuyingOrderPC":			//구매발주현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Text).Month;
					break;	
				
				
				case "OutSideOrderPC":						// 외주발주현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Text).Month;
					break;	
				case "OutSideDeliveryPC":						// 외주납품현황
					CloseYear = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Year").Text);
					CloseMonth = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Month").Text);
					break;	
				case "OutSideOutStorehousePC":					// 외주출고현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text).Month;
					break;	
				case "PaymentPlanResultRegistrationPC":					// 지급현황
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("PaymentDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("PaymentDate").Text).Month;
					break;	
				case "ClaimPC":
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Month;
					break;	
				case "RowItemExhaustPC":
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ExhaustDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ExhaustDate").Text).Month;
					break;	
				case "EtcClaimPC":
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Month;
					break;	
				case "SubBuyingDeliveryPC":
					CloseYear = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Year").Text);
					CloseMonth = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Month").Text);
					break;	
				case "SubBuyingOrderPC":
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;
					break;	//WCPlanPC
				case "WCPlanPC":
					CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
					CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;
					break;	//
				case "QualityInspectionPC":
				{
					if(m_Grid.Rows[rowcount].Cells.FromKey("historysection1").Text.Trim() == "자가")
					{
						CloseYear = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Year;
						CloseMonth = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("QualityInspectionCompleteDate").Text).Month;
					}
					else
					{
						CloseYear = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Year").Text);
						CloseMonth = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Month").Text);
					}
					break;
				}
				default :
					CloseYear = 0;
					CloseMonth = 0;
					break;	//
			}

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = m_Distinction.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();


			if(year < CloseYear)
			{
				return true;
			}
			else if(year == CloseYear) 
			{
				if(month < CloseMonth)
					return true;
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}	
			
		}



		/// <summary>
		/// 레코드 삭제 함수
		/// </summary>
		/// <param name="count"></param>
		private void RowDelete(SqlConnection con,SqlTransaction trans, int count)
		{
			
			switch(m_PageName)							// 요청페이지에 따라 이전원장 수정
			{
				case "ReceiveingPC":
					if(MonthClosing(count))
						TableDelete(con,trans,count);
					else
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					break;
				case "ProductionRequestPC":					// 생산의뢰현황
					if(MonthClosing(count))
					{
						if(m_Grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value == null)
							TableDelete(con,trans,count);						// 자신의 원장 삭제
						else
						{
							TableUpdate(con,trans,count);							// 수주원장 진행상태 변경(대기)
							TableDelete(con,trans,count);							// 자신의 원장 삭제
						}
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;
				case "GoodsBuyingRequestPC":				// 상품구매의뢰현황
					if(MonthClosing(count))
					{
						if(m_Grid.Rows[count].Cells.FromKey("HistoryIndex").Value == null)
							TableDelete(con,trans,count);							// 자신의 원장 삭제
						else
						{
							TableUpdate11(con,trans,count);					// 수주원장 진행상태 변경(대기)
							TableDelete(con,trans,count);							// 자신의 원장 삭제
						}
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;
					
				case "BusinessStorehouseInStorehousePC":	// 영업창고입고현황					
					if(MonthClosing(count))
					{
						ISR_HTUpdate(con,trans,count);						// 입고의뢰원장 진행상태 대기
						QI_HTUpdate(con,trans,count);						//품질검사원장 진행상태 검사완료
						InBS_MTUpdate(con,trans,count);						// 취소수량만큼 영업창고 입고수량 마이너스증가
						OutPS_MTUpdate(con,trans,count);					// 취소수량만큼 생산창고 출고수량 마이너스증가	
						SH_HTDelete(con,trans,count);
						TableDelete(con,trans,count);						// 자신의 원장 삭제
						BD_HTUpdate1(con,trans,count);
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;				
				case "GoodsManufactureOutStorehousePC":					// 상품제품출고현황
					if(MonthClosing(count))
					{
						if(m_Grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value != null)
						{
							RO_HTUpdate(con,trans,count);						// 취소수량을 수주원장 출고수량에 증가시킨다
						}
						if(Division(m_Grid.Rows[count].Cells.FromKey("ItemNum").Text) == 1 || Division(m_Grid.Rows[count].Cells.FromKey("ItemNum").Text) ==2)
							OutAS_MTUpdate(con,trans,count);					//취소수량만큼 보용품 창고 출고수량 마이너스 증가
						else if(Division(m_Grid.Rows[count].Cells.FromKey("ItemNum").Text) == 3 || Division(m_Grid.Rows[count].Cells.FromKey("ItemNum").Text) == 4)//상품인 경우만 출고수량 마이너스 증가
							OutBS_MTUpdate(con,trans,count);						// 취소수량만큼 영업창고 출고수량 마이너스증가
						InDS_MTUpdate(con,trans,count);						// 취소수량만큼 납품창고 입고수량 마이너스증가
						if(StoreManage(con,trans,m_Grid.Rows[count].Cells.FromKey("ItemNum").Text)=="아니오")
							SH_HT_OS_HTDelete(con,trans,count);						// 삭제레코드에 대한 이력 삭제

						TableDelete(con,trans,count);							// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;	
				case "SaleHistoryRegistrationPC":						// 매출원장등록현황
					if(MonthClosing(count))
					{
						if(m_Grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value != null)
						{
							RO_HTUpdate1(con,trans,count);						// 수주원장 납품잔량및 미검수량등 변경
						}
						OS_HTUpdate(con,trans,count);						// 출고원장 수량과 부적합수량등을 삭제
						DS_MTUpdate(con,trans,count);						// 취소수량만큼 납품창고 출고수량 마이너스증가
						BSI_MinusUpdate(con,trans,count);					// 취소금액만큼 매출입테이블에서 매출금액 마이너스증가
						BOS_HTDelete(con,trans,count);
						TableDelete(con,trans,count);						// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;	
				case "OtherInOutStorehousePC":
					if(MonthClosing(count))
					{
						StoreTableDelete(con,trans,count);				//창고에서 수량삭제
						SH_HT_OIOS_HTDelete(con,trans,count);
						TableDelete(con,trans,count);						//자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;
				case "StorehouseMovingPC":
					if(MonthClosing(count))
					{
						SM_HTTableUpdate(con,trans,count);				// 영업창고테이블 수량변경
						TableDelete(con,trans,count);						// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;
				case "ClaimRegistrationPC":					
					if(MonthClosing(count))
					{
						BuyingTable(con,trans,count);
						TableDelete(con,trans,count);						// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;
				case "CollectMoneyRegistrationPC":						//수금등록현황
					if(MonthClosing(count))
					{
						BuyingTable(con,trans,count);						// 매입매출테이블수량 변경
						TableDelete(con,trans,count);						// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;
				case "ProductionPlanPC":					// 생산계획현황
					if(MonthClosing(count))
					{
						if(m_Grid.Rows[count].Cells.FromKey("RowMaterialCalculation").Text == "산출")
						{
							throw new Exception("품목 " +m_Grid.Rows[count].Cells.FromKey("ItemName").Text+"에 대한 자재소요량이 이미 산출되어 삭제가 불가능합니다!");
						}
						else
						{
							if(m_Grid.Rows[count].Cells.FromKey("HistoryIndex").Value == null || m_Grid.Rows[count].Cells.FromKey("HistorySection").Text == "기종등록")
								TableDelete(con,trans,count);						// 자신의 원장 삭제
							else if(m_Grid.Rows[count].Cells.FromKey("HistorySection").Text == "실행계획")
							{
								EPI_MTTableUpdate(con,trans,count);						// 실행계획 테이블 State필드 수정
								TableDelete(con,trans,count);						// 자신의 원장 삭제
							}
							else
							{
								TableUpdate(con,trans,count);						// 생산의뢰원장 진행상태 수정
								TableDelete(con,trans,count);						// 자신의 원장 삭제
							}
						}

					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;
				case "WorkDailyReportRegistrationPC":		// 작업일보현황
					if(MonthClosing(count))
					{
						// 지금공정이 검사이고 최종공정이면 품질검사원장에 레코드를 등록해야 한다.
						// 최종공정은 해당품목의 지금공정순서보다 큰게 없으면 최종공정이다.
						SqlCommand comm = new SqlCommand();
						comm.Connection = con;
						comm.Transaction = trans;
						string str = "Select count(*) From PSI_MT Where RecodingState = 1 and ItemNum = @itemnum and ProcessSequenceNum > @num";
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("ItemNum").Value;
						comm.Parameters.Add("@num",SqlDbType.VarChar).Value = int.Parse(m_Grid.Rows[count].Cells.FromKey("ProcessSequenceNum").Value.ToString());
						comm.CommandText = str;
						int a = int.Parse(comm.ExecuteScalar().ToString());
						comm.Parameters.Clear();
					
						if(Division(m_Grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString().Trim()) == 3 && a==0)//최종공정인경우
						{
							ISR_HTDelete(con,trans,count);					// 영업창고입고의뢰원장 삭제
							NonQI_HTDelete(con,trans,count);					// 품질검사원장 삭제
							//생산계획번호가 존재하면 생산완료량 감소
							if(ExistPPIndex(con,trans,count))
								PP_Update(con,trans,count);
						}
						
						if(WorkPlan3())
						{
							if(IsManaged(con,trans,m_Grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString().Trim()))
								StoreUpdate(con,trans,count);						//각창고수량변경
						}
						else
						{
							StoreUpdate(con,trans,count);						//각창고수량변경
						}
						EI_MTMinusUpdate(con,trans,count);					//사용공구치구누계샷변경
						WDWP_Update(con,trans,count);						//WC별 일자별 작업계획원장 변경						
						TableDelete(con,trans,count);						// 자신의 원장 삭제
						SH_WDR_HTDelete(con,trans,count);					//이력원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;
				case "RowMaterialRequriementPC":			// 원자재의뢰현황: 추가된것만 삭제가 가능
					if(MonthClosing(count))
					{
						if(m_Grid.Rows[count].Cells.FromKey("HistoryIndex").Value == null || m_Grid.Rows[count].Cells.FromKey("HistoryIndex").Text == "")
							TableDelete(con,trans,count);//구매의뢰원장 삭제
						else
						{
							TableUpdate(con,trans,count);//자재소요원장업데이트
							TableDelete(con,trans,count);//구매의뢰원장 삭제
						}
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;
					
				case "OutsideRequestPC":						//외주의뢰현황
				{
					if(MonthClosing(count))
					{
						int idx = 0;
						if(m_Grid.Rows[count].Cells.FromKey("ProductionPlanHistoryIndex").Value == null)// || (m_Grid.Rows[count].Cells.FromKey("ProductionPlanHistoryIndex").Text.Trim() == "" ))
							WDWPUpdate(con,trans,int.Parse(m_Grid.Rows[count].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Text),0);				//작업계획원장 진행상태 변경
						else
						{
							idx = int.Parse(m_Grid.Rows[count].Cells.FromKey("ProductionPlanHistoryIndex").Text);
							WDWPUpdate(con,trans,idx);				//작업계획원장 진행상태 변경
						}
					
						TableDelete(con,trans,count);	
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					break;
				}

				case "SubItemDeletePC":
				{
					if(MonthClosing(count))
					{
						if(m_Grid.Rows[count].Cells.FromKey("StoreNum").Text == "4")
						{
							//원자재 창고 출고수량 마이너스 증가]
							OutRMS_MTTable(con,trans,count);
						}
						else if(m_Grid.Rows[count].Cells.FromKey("StoreNum").Text == "5")
						{
							//생산창고 출고수량 마이너스 증가
							OutPS_MTTable(con,trans,count);
						}
						else
						{
							// 영업창고 마이너스 증가
							OutBS_MTTable(con,trans,count);
						}

						TableDelete(con,trans,count);						// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}				
					
				}
					break;
				case "BuyingOrderPC":
				{
					if(MonthClosing(count))
					{
						// 구매발주현황
						if(m_Grid.Rows[count].Cells.FromKey("BuyingRequestHistoryIndex").Value != null)
						{
							int idx = int.Parse(m_Grid.Rows[count].Cells.FromKey("BuyingRequestHistoryIndex").Text);
							BuyingRequestUpdate(con,trans,count,idx);				//구매의뢰원장 진행상태 변경
						}
						TableDelete(con,trans,count);							// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
				}
					break;	
				case "SubBuyingDeliveryPC":
				{
					if(MonthClosing(count))
					{
						SaleTable(con,trans,count);											// 매입테이블에 금액감소
						SubBuyingOrderUpdate(con,trans,count);								// 부자재구매발주원장 진행상태및 잔량수정
						SubBOS_HTUnCheckedUpdate(con,trans,count);				// 부자재구매입고 삭제이력원장 등록
						TableDelete(con,trans,count);							// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}					
					break;
				}
				case "BuyingDeliveryPC":
				{	// 구매납품현황
					if(MonthClosing(count))
					{
						int idx = int.Parse(m_Grid.Rows[count].Cells.FromKey("BuyingDeliveryHistoryIndex").Text);	//구매납품원장 번호
						int idx1 = int.Parse(m_Grid.Rows[count].Cells.FromKey("BuyingOrderHistoryIndex").Text);		//구매발주원장 번호
						if(m_Grid.Rows[count].Cells.FromKey("BuyingDeliveryRequestHistoryIndex").Value != null)//구매납품의뢰원장번호가 있으면..
							TableUpdate(con,trans,count);						// 구매납품의뢰원장 진행상태 대기
						if(Check(count))	// 검사인경우
						{
							if(m_Grid.Rows[count].Cells.FromKey("BuyingDeliveryRequestHistoryIndex").Value != null)
								QualityInspectionDelete(con,trans,idx,int.Parse(m_Grid.Rows[count].Cells.FromKey("BuyingDeliveryRequestHistoryIndex").Text),"구매납품","구매납품의뢰");			// 품질검사 원장 삭제
							else
								QualityInspectionDelete(con,trans,idx,idx1,"구매납품","구매발주");			// 품질검사 원장 삭제

							// 발주원장 입고대기수량 마이너스 증가
							BuyingOrderInStoreWaitingQuantityUpdate(con,trans,count);								// 구매발주원장 입고대기수량 마이너스

							
						}
						else	// 무검사인경우
						{
							int Index;
							if(m_Grid.Rows[count].Cells.FromKey("BuyingDeliveryRequestHistoryIndex").Value != null)
								Index = NonQualityInspectionDelete(con,trans,idx,int.Parse(m_Grid.Rows[count].Cells.FromKey("BuyingDeliveryRequestHistoryIndex").Text),"구매납품","구매납품의뢰");			// 품질검사 원장 삭제
							else
								Index = NonQualityInspectionDelete(con,trans,idx,idx1,"구매납품","구매발주");			// 품질검사 원장 삭제

							if(Division(count) == 4)
							{
								InStoreRequestDelete(con,trans,Index,"품질검사");				//입고의뢰원장 삭제(상품인경우)
								ProductionTable(con,trans,count,"14009999");					//생산창고테이블 입고수량 마이너스증가(상품인 경우)
							}
							else if(Division(count) == 1)
								RowTable(con,trans,count);									//원자재 창고테이블 입고수량 마이너스증가	
							SH_HT_BD_HTDelete(con,trans,count);
							//BOS_HTDelete(con,trans,count);
							SaleTable(con,trans,count);										// 매입테이블에 금액감소
							BuyingHistoryDelete(con,trans,count,"구매납품");								// 매입원장 삭제
							BuyingOrderUpdate(con,trans,count);								// 구매발주원장 진행상태및 잔량수정
							BOS_HTUnCheckedUpdate(con,trans,count);		// 구매납품 삭제이력원장 등록
						}
						TableDelete(con,trans,count);							// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}					
				}
					break;
				case "OutSideOrderPC":						// 외주발주현황
				{
					if(MonthClosing(count))
					{
						int idx = int.Parse(m_Grid.Rows[count].Cells.FromKey("OutSideOrderRequestHistoryIndex").Text);	
						OutSideOrderRequestUpdate(con,trans,count, idx);						//외주의뢰원장 진행상태 변경
						TableDelete(con,trans,count);							// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}					
				}
					break;
				case "OutSideDeliveryPC":						// 외주납품현황
				{
					if(MonthClosing(count))
					{
						int Index ;
						int idx = int.Parse(m_Grid.Rows[count].Cells.FromKey("OutSideDeliveryHistoryIndex").Text);//외주납품원장번호
						int idx1 = int.Parse(m_Grid.Rows[count].Cells.FromKey("OutSideOrderHistoryIndex").Text);//외주발주원장번호
						if(m_Grid.Rows[count].Cells.FromKey("OutSideDeliveryRequestHistoryIndex").Value != null)
							TableUpdate(con,trans,count);							// 외주납품의뢰원장 진행상태 대기
						if(Check(count))	//검사인경우
						{
							if(m_Grid.Rows[count].Cells.FromKey("OutSideDeliveryRequestHistoryIndex").Value != null)
								QualityInspectionDelete(con,trans,idx,int.Parse(m_Grid.Rows[count].Cells.FromKey("OutSideDeliveryRequestHistoryIndex").Text),"외주납품","외주납품의뢰");							// 품질검사 원장 삭제
							else
								QualityInspectionDelete(con,trans,idx,idx1,"외주납품","외주발주");	// 품질검사 원장 삭제				
							OutSideOrderInStoreWaitingQuantityUpdate(con,trans,count);
						}	
						else		// 무검사인경우
						{
							if(m_Grid.Rows[count].Cells.FromKey("OutSideDeliveryRequestHistoryIndex").Value != null)
								Index = NonQualityInspectionDelete(con,trans,idx,int.Parse(m_Grid.Rows[count].Cells.FromKey("OutSideDeliveryRequestHistoryIndex").Text),"외주납품","외주납품의뢰");							// 품질검사 원장 삭제
							else
								Index = NonQualityInspectionDelete(con,trans,idx,idx1,"외주납품","외주발주");	// 품질검사 원장 삭제


							if(Division(count) == 3 || MaxSequence(con,trans,Index))	
							{
								InStoreRequestDelete(con,trans,Index,"품질검사");	//입고의뢰원장 삭제(제품인경우)
								BS_MTDelete(con,trans,count);						//영업창고 입고수량 마이너스 감소
							}
							SH_HT_OD_HTDelete(con,trans,count);
							BOS_OS_HTDelete(con,trans,count);
							SaleTable(con,trans,count);																			// 매입테이블에 금액감소
							ProductionTable(con,trans,count,m_Grid.Rows[count].Cells.FromKey("ProcessCode").Text);				//창고테이블 수량증가및 감소(제품,반제품-생산창고)
							OutSideTable(con,trans,count);						//외주창고 출고수량 마이너스 감소
							BuyingHistoryDelete(con,trans,count,"외주납품");														// 매입원장 삭제
							OutSideOrderUpdate(con,trans,count);				//외주납품 삭제시 외주발주원장 진행상태와 수량변경							
							BOS_HTUnCheckdOutUpdate(con,trans,count);			//무검사 외주납품 삭제 이력 등록
						}
						TableDelete(con,trans,count);							// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}
					
				}
					break;
				case "OutSideOutStorehousePC":					// 외주출고현황
					if(MonthClosing(count))
					{
						Out_Table(con,trans,count);							// 창고 수량 변경						
						SH_HT_OOS_HTDelete(con,trans,count);				//이력원장 삭제
						TableDelete(con,trans,count);							// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}		
					break;
				case "PaymentPlanResultRegistrationPC":					// 지급현황
					if(MonthClosing(count))
					{
						BuyingTable(con,trans,count);							//매입매출테이블 금액 감소
						TableDelete(con,trans,count);							// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}		
					break;
				case "ClaimPC":
					if(MonthClosing(count))
					{
						BuyingTable(con,trans,count);
						TableDelete(con,trans,count);						// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}	
					break;
				case "AddItemInStorePC":
					if(MonthClosing(count))
					{
						TableDelete(con,trans,count);						// 자신의 원장 삭제
						AddItemTable(con,trans,count);						// 창고수량입출고 수량변경
						SH_HT_AS_HTDelete(con,trans,count);					// 이력원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어 삭제가 불가능 합니다!");
					}
					
					break;
				case "RowItemExhaustPC":
					if(MonthClosing(count))
					{
						TableDelete(con,trans,count);						// 자신의 원장 삭제
						RMS_MTDelete(con,trans,count);						// 창고수량입출고 수량변경
						SH_HT_E_HT_Delete(con,trans,count);						// 창고입출고이력 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}	
					break;
					
				case "EtcClaimPC":
					if(MonthClosing(count))
					{
						BuyingTable(con,trans,count);
						TableDelete(con,trans,count);						// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}	
					break;					
				default :
					if(MonthClosing(count))
					{
						TableDelete(con,trans,count);							// 자신의 원장 삭제
					}
					else
					{
						throw new Exception("월마감이 되어서 삭제가 불가능 합니다!");
					}	
					break;
			}			
		}


		/// <summary>
		/// 원자재 소진현황 페이지에서 원자재 창고 수량 마이너스 증가
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void RMS_MTDelete(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ExhaustDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ExhaustDate").Text).Month;
			Table table = new Table(year,mon);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			decimal m_cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ExhaustItemNum").Text;
			comm.CommandText = str_cost;
			SqlDataReader dr_cost = comm.ExecuteReader();
			if(dr_cost.Read())
				m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			dr_cost.Close();
			comm.Parameters.Clear();

			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ExhaustItemNum").Text;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ExhaustQuantity").Text);
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(m_cost*decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ExhaustQuantity").Text));
			comm.Parameters.Add("@year",year);

			comm.CommandText= table.OutRowTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("CreateItemNum").Text;
			comm.CommandText = str_cost;
			SqlDataReader drcost = comm.ExecuteReader();
			if(drcost.Read())
				m_cost = Decimal.Parse(drcost["StandardUnitCost"].ToString());
			drcost.Close();
			comm.Parameters.Clear();

			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("CreateItemNum").Text;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("CreateQuantity").Text);
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(m_cost*decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("CreateQuantity").Text));
			comm.Parameters.Add("@year",year);

			comm.CommandText= table.InRowTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			
		}


		/// <summary>
		/// 이력원장 삭제
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void SH_HT_E_HT_Delete(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str="Delete SH_HT Where HistoryIndex = @idx and HistoryDivision = '공통소진'";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("ExhaustHistoryIndex").Text;
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 원장 레코드 삭제함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void TableDelete(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str = "Delete "+m_Table.ToString()+" Where "+m_Index.ToString()+ " = @idx";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey(m_Index.ToString()).Text;
			comm.ExecuteNonQuery();
		}



		/// <summary>
		/// 실행계획 테이블 State 필드 변경 함수
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void EPI_MTTableUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str = "UPDATE EPI_MT SET State = '0' WHERE RecodingState = 1 and ExecutionPlanInfoIndex = @idx";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex").Text);
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 이전원장 업데이트 함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void TableUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction= tr;
			switch(m_PageName)
			{

				case "RowMaterialRequriementPC":
					str =  "UPDATE "+m_Table1.ToString()+" SET ProgressCondition = '대기' WHERE "+m_Index1.ToString()+ " = @idx";
					comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex").Text;
					comm.CommandText  = str;
					comm.ExecuteNonQuery();
					break;
				case "ProductionPlanPC":
					str =  "UPDATE PR_HT SET ProgressCondition = '대기' WHERE ProductionRequestHistoryIndex = @idx";
					comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex").Text;
					comm.CommandText  = str;
					comm.ExecuteNonQuery();
					break;
				default :
					str =  "UPDATE "+m_Table1.ToString()+" SET ProgressCondition = '대기' WHERE "+m_Index1.ToString()+ " = @idx";
					comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey(m_Index1.ToString()).Text;
					comm.CommandText= str;
					comm.ExecuteNonQuery();
					break;
				 
			}
		}



		/// <summary>
		/// 이전원장 구분 필드가 있는 테이블 업데이트 함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void TableUpdate1(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str = "UPDATE "+m_Table1.ToString()+" SET ProgressCondition = '대기' WHERE "+m_Index1.ToString()+ " = @idx  and "+m_Section.ToString()+" = @section";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey(m_Index1.ToString()).Text;
			comm.Parameters.Add("@section", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey(m_Section.ToString()).Text;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 상품구매의뢰현황페이지에서 삭제시 발주원장 진행상태 업데이트
		/// </summary>
		/// <param name="rowcount"></param>
		private void TableUpdate11(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str = "UPDATE RO_HT SET ProgressCondition = '대기' WHERE ReceivingOrderHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey(m_Index1.ToString()).Text;
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 외주의뢰원장의 레코드 삭제시 작업계획원장 진행상태 수정
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void WDWPUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str = "UPDATE WDWP_HT SET ProgressCondition = '대기' WHERE ProductionPlanHistoryIndex = @idx";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx",rowcount);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 외주의뢰원장의 레코드 삭제시 작업계획원장 진행상태 수정
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void WDWPUpdate(SqlConnection conn, SqlTransaction tr,int rowcount, int count)
		{
			string str = "UPDATE WDWP_HT SET ProgressCondition = '대기' WHERE WCDailyWorkPlanHistoryIndex = @idx";
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx",rowcount);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}



		/// <summary>
		/// 작업일보원장 삭제시 WC일자별작업계획원장 진행상태 수정
		/// </summary>
		/// <param name="rowcount"></param>
		private void WDWP_Update(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			//먼저 WC작업계획원장에서 작업완료수량을 불러온다
			decimal CompletionQuantity = 0;
			
			int index = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value.ToString());
			string str = "select * From WDWP_HT Where WCDailyWorkPlanHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx",index);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				CompletionQuantity = decimal.Parse(dr["WorkCompletionQuantity"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();

			str = "UPDATE WDWP_HT SET WorkCompletionQuantity = @quantity, ProgressCondition = '진행' WHERE WCDailyWorkPlanHistoryIndex = @idx";
			SqlCommand comm1 = new SqlCommand(str, conn);
			comm1.Transaction = tr;
			comm1.Parameters.Add("@idx", index);
			comm1.Parameters.Add("@quantity", SqlDbType.Decimal).Value = CompletionQuantity -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Text);
			comm1.ExecuteNonQuery();
			comm1.Parameters.Clear();


			//다시 불러온 WDWP 테이블의 완료수량이 0 이면 대기
			str = "select * From WDWP_HT Where WCDailyWorkPlanHistoryIndex = @idx";
			SqlCommand comm2 = new SqlCommand(str, conn);
			comm2.Transaction = tr;
			comm2.Parameters.Add("@idx",index);
			SqlDataReader dr1 = comm2.ExecuteReader();
			while(dr1.Read())
			{
				CompletionQuantity = decimal.Parse(dr1["WorkCompletionQuantity"].ToString());
			}
			dr1.Close();
			comm2.Parameters.Clear();

			if(CompletionQuantity == 0)
			{
				str = "UPDATE WDWP_HT SET ProgressCondition = '지시' WHERE WCDailyWorkPlanHistoryIndex = @idx";
				SqlCommand comm3 = new SqlCommand(str,conn);
				comm3.Transaction = tr;
				comm3.Parameters.Add("@idx",index);
				comm3.ExecuteNonQuery();
				comm3.Parameters.Clear();
			}

			
		
		}


		/// <summary>
		/// 작업일보원장에서 레코드 삭제시
		/// 이동되었던 품목들의 수정
		/// </summary>
		/// <param name="rowcount"></param>
		private void StoreUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			

			//이전의 완료수량만큼 창고에서 빼고 지금 수정된 수량만큼 창고에 입력한다.
			//이때 떨어준 수량도 이전에 떨어준수량은 더하고 지금 떨어준 수량을 빼준다.
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkEndTime").Value.ToString()).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkEndTime").Value.ToString()).Month;

			////////////////////////////////////////////////////////////////////////////////////
			// 이전 하위품목이 원자재이면 원자재 창고의 출고수량(BOM에 맞는수량) 마이너스증가 //
			// 반제품 이면 생산창고의 출고수량(BOM에 맞는수량) 마이너스증가   -시작-          //
			////////////////////////////////////////////////////////////////////////////////////
					
			string str = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			str = "Select * From SH_HT Where HistoryIndex = @idx and HistoryDivision = '작업일보' and Division = 0";
			comm.CommandText = str;
			comm.Parameters.Add("@idx",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey(m_Index.ToString()).Text));

			SqlDataAdapter da = new SqlDataAdapter(comm);

			DataSet ds = new DataSet();

			da.Fill(ds);
			comm.Parameters.Clear();

			
			Table table = new Table(year,mon);

			decimal Cost= 0;

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				SqlCommand comm2 = new SqlCommand();
				comm2.Connection = conn;
				comm2.Transaction = tr;

				//창고에서 떨어줄 금액
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
							
				comm2.Parameters.Add("@itemnum", dr["ItemNum"].ToString());
				comm2.CommandText = str;
				comm.CommandType = CommandType.Text;
				SqlDataReader dr3 = comm2.ExecuteReader();
				while(dr3.Read())
				{
					Cost = decimal.Parse(dr3["StandardUnitCost"].ToString());
				}
				dr3.Close();
				comm2.Parameters.Clear();

				if(Division(dr["ItemNum"].ToString()) == 1)
				{
					//창고에서 떨어줘야 한다
					str = table.OutRowTable();
					comm2.Parameters.Add("@quantity",-(decimal.Parse(dr["Quantity"].ToString())));
					comm2.Parameters.Add("@cost",-(Cost*decimal.Parse(dr["Quantity"].ToString())));
					comm2.Parameters.Add("@num",dr["ItemNum"].ToString());
					comm2.Parameters.Add("@year",year);
					comm2.CommandText = str;
					comm2.ExecuteNonQuery();
					comm2.Parameters.Clear();
				}
				else
				{
					decimal Rate = 100;
					//창고에서 더해줄 금액
					str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = dr["ItemNum"].ToString();
					comm.CommandText = str;
					SqlDataReader dr2 = comm.ExecuteReader();
					while(dr2.Read())
					{
						Cost = decimal.Parse(dr2["StandardUnitCost"].ToString());
					}
					dr2.Close();
					comm.Parameters.Clear();

					// 이전공정의 진척율
					str = "Select ProgressRate,ProcessCode From PSI_MT Where RecodingState = 1 and ItemNum = @itemnum and ProcessSequenceNum = @sequence";
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = dr["ItemNum"].ToString();
					comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = int.Parse(dr["ProcessSequenceNum"].ToString());
					comm.CommandText = str;
					SqlDataReader dr_rate = comm.ExecuteReader();
					if(dr_rate.Read())
					{
						if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
							Rate = 100;
						else
							Rate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
					}
					dr_rate.Close();
					comm.Parameters.Clear();


					//생산창고에 이전품목을 더해준다
					str = table.OutProductionTable();
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(dr["Quantity"].ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(Cost*decimal.Parse(dr["Quantity"].ToString())*(Rate/100));
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ItemNum"].ToString();
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = dr["ProcessCode"].ToString();
					comm.Parameters.Add("@year",year);
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					
				}
			}
			


			//해당 하위품목들에 대한 마이너스 재고 감안
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				//마이너스재고를 허용하지 않으면 
				if(MinusStore() == false)
				{
					string strSQL="";
					if(Division(dr["ItemNum"].ToString()) == 1)
					{
						strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
						comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = dr["ItemNum"].ToString();
						comm.Parameters.Add("@year",DateTime.Now.Year);
					}
					else
					{
						strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @ProcessCode and [Year] = @year";
						comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = dr["ItemNum"].ToString();
						comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = dr["ProcessCode"].ToString();
						comm.Parameters.Add("@year",DateTime.Now.Year);
					}
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
					comm.Parameters.Clear();
						
					string StockQuantity = "False";
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(dr["ItemNum"].ToString() + " 의 재고량 부족으로 입고처리할 수 없습니다.");
					}
				}
			}

			//제품 창고입고품목의 창고금액
			Cost = 0;
			str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
			comm.Parameters.Add("@itemnum",m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString());
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			SqlDataReader dr_Cost = comm.ExecuteReader();
			while(dr_Cost.Read())
			{
				Cost = decimal.Parse(dr_Cost["StandardUnitCost"].ToString());
			}
			dr_Cost.Close();
			comm.Parameters.Clear();
			decimal rate = ProgressRate(m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString(),m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text,conn,tr);
			// 가공된 지금 품목의 생산창고 입고
			str = table.InProductionTable();
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Text));
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(Cost*decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Text)*rate);
			comm.Parameters.Add("@num",m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString());
			comm.Parameters.Add("@code",m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString());
			comm.Parameters.Add("@year",year);
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			if(MinusStore() == false)
			{
				string strSQL="";
				strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @ProcessCode and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@ProcessCode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
				comm.Parameters.Clear();
						
				string StockQuantity = "False";
				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 입고처리할 수 없습니다.");
				}
			}
		}

		/// <summary>
		/// 진척율
		/// </summary>
		/// <param name="Item"></param>
		/// <param name="code"></param>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <returns></returns>
		private decimal ProgressRate(string Item, string code, SqlConnection conn,SqlTransaction tr)
		{
			string str = @"select * From PSI_MT where RecodingState = 1 and ItemNum=@num and ProcessCode = @ProcessCode";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",Item);
			comm.Parameters.Add("@ProcessCode",code);
			SqlDataReader dr = comm.ExecuteReader();
			decimal rate = -1;
			while(dr.Read())
			{
				rate = decimal.Parse(dr["ProgressRate"].ToString())/100;
			}
			dr.Close();

			if(rate == -1)
				throw new Exception(Item+" 품목의 공정정보가 입력되어 있지 않습니다!");
			else
				return rate;
		}

		

		/// <summary>
		/// 설비정보내의 사용공구,치구변경시 누적샷 변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void EI_MTMinusUpdate(SqlConnection conn,SqlTransaction tr, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			string str = "";	
			//설비정보에 공구1의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and RecodingState = 1 and EquipmentClassification = '07200010'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseTool1").Value.ToString();
			comm.CommandText = str;
			SqlDataAdapter daShot = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilder = new SqlCommandBuilder(daShot) ;
			DataSet dsShot = new DataSet() ;
			daShot.Fill(dsShot) ;
			if(dsShot.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Value.ToString()) / decimal.Parse(dsShot.Tables[0].Rows[0]["Cavity"].ToString())));
				dsShot.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot.Tables[0].Rows[0]["TotalShot"].ToString())-tmp;
				dsShot.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot.Tables[0].Rows[0]["WorkShot"].ToString())-tmp;
				daShot.Update(dsShot) ;
			}
			//설비정보에 공구1의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();

			//설비정보에 치구1의 작업shot을 누적하는 부분......시작
					
			str = "select * from EI_MT where EquipmentNum = @name and RecodingState = 1 and EquipmentClassification = '07200020'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseJig1").Value.ToString();
			comm.CommandText = str;
			SqlDataAdapter daJig = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilderJig = new SqlCommandBuilder(daJig) ;
			DataSet dsJig = new DataSet() ;
			daJig.Fill(dsJig) ;
			if(dsJig.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Value.ToString()) / decimal.Parse(dsJig.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsJig.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsJig.Tables[0].Rows[0]["TotalShot"].ToString())-tmpJig;
				dsJig.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsJig.Tables[0].Rows[0]["WorkShot"].ToString())-tmpJig;
				daJig.Update(dsJig) ;
			}
			//설비정보에 치구1의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();
					
			//설비정보에 공구2의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and RecodingState = 1 and EquipmentClassification = '07200010'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseTool2").Value.ToString();
			comm.CommandText = str;				
			SqlDataAdapter daShot1 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilder1 = new SqlCommandBuilder(daShot1) ;
			DataSet dsShot1 = new DataSet() ;
			daShot1.Fill(dsShot1) ;
			if(dsShot1.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp1 = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Value.ToString()) / decimal.Parse(dsShot1.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsShot1.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot1.Tables[0].Rows[0]["TotalShot"].ToString())-tmp1;
				dsShot1.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot1.Tables[0].Rows[0]["WorkShot"].ToString())-tmp1;
				daShot1.Update(dsShot1);
			}
			//설비정보에 공구2의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();

			//설비정보에 치구2의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and RecodingState = 1 and EquipmentClassification = '07200020'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseJig2").Value.ToString();
			comm.CommandText = str;
			SqlDataAdapter daJig1 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilderJig1 = new SqlCommandBuilder(daJig1) ;
			DataSet dsJig1 = new DataSet() ;
			daJig1.Fill(dsJig1) ;
			if(dsJig1.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig1 = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Value.ToString()) / decimal.Parse(dsJig1.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsJig1.Tables[0].Rows[0]["TotalShot"] = -tmpJig1;
				dsJig1.Tables[0].Rows[0]["WorkShot"] = -tmpJig1;
				daJig1.Update(dsJig1) ;
			}
			//설비정보에 치구2의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();


			//설비정보에 공구3의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and RecodingState = 1 and EquipmentClassification = '07200010'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseTool3").Value.ToString();
			comm.CommandText = str;				
			SqlDataAdapter daShot2 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilder2 = new SqlCommandBuilder(daShot2) ;
			DataSet dsShot2 = new DataSet() ;
			daShot2.Fill(dsShot2) ;
			if(dsShot2.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp2 = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Value.ToString()) / decimal.Parse(dsShot2.Tables[0].Rows[0]["Cavity"].ToString())));
				dsShot2.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot2.Tables[0].Rows[0]["TotalShot"].ToString()) -tmp2;
				dsShot2.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot2.Tables[0].Rows[0]["WorkShot"].ToString())-tmp2;
				daShot2.Update(dsShot2) ;
			}
			//설비정보에 공구3의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();

			//설비정보에 치구3의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and RecodingState = 1 and EquipmentClassification = '07200020'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseJig3").Value.ToString();
			comm.CommandText = str;
			SqlDataAdapter daJig2 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilderJig2 = new SqlCommandBuilder(daJig2) ;
			DataSet dsJig2 = new DataSet() ;
			daJig2.Fill(dsJig2) ;
			if(dsJig2.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig2 = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Value.ToString())/ decimal.Parse(dsJig2.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsJig2.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsJig2.Tables[0].Rows[0]["TotalShot"].ToString())-tmpJig2;
				dsJig2.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsJig2.Tables[0].Rows[0]["WorkShot"].ToString())-tmpJig2;
				daJig2.Update(dsJig2) ;
			}
			//설비정보에 치구3의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();
		}


		/// <summary>
		/// 부자재구매입고 삭제시 부자재구매발주원장 진행상태와 수량변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void SubBuyingOrderUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			//잔량은 누적치를 넣어야 한다.
			//그러므로 구매발주원장의 잔량을 가져온다.
			decimal Remain = 0;
			decimal Total = 0;
			string str  = "select * from SBO_HT Where SubBuyingOrderHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("SubBuyingOrderHistoryIndex").Text;
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Remain = decimal.Parse(dr["DeliveryRemainQuantity"].ToString());
				Total = decimal.Parse(dr["DeliveryQuantity"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();

			Remain = Remain + Decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Text);

			if(Remain == Total)
				str = "UPDATE SBO_HT SET ProgressCondition = '대기', DeliveryRemainQuantity = @remain WHERE SubBuyingOrderHistoryIndex = @idx";
			else
				str = "UPDATE SBO_HT SET ProgressCondition = '진행', DeliveryRemainQuantity = @remain WHERE SubBuyingOrderHistoryIndex = @idx";
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SubBuyingOrderHistoryIndex").Text);
			comm.Parameters.Add("@remain", SqlDbType.Decimal).Value = Remain;
			comm.CommandText = str;
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 부자재 구매입고 삭제이력원장 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void SubBOS_HTUnCheckedUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) 
			values(@ItemNum,@ItemDrawNum,@ItemName,@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,'삭제', @Date,@Person, @PersonID,'부자재구매입고',@HistoryIndex)";
			
			comm.Parameters.Add("@ItemNum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
			comm.Parameters.Add("@ItemDrawNum", m_Grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);							//도면번호
			comm.Parameters.Add("@ItemName",m_Grid.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
			comm.Parameters.Add("@CompanyName",m_Grid.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text);
			comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
			comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());//외주출고일자
			comm.Parameters.Add("@Person",m_UserName.ToString());
			comm.Parameters.Add("@PersonID", m_User.ToString());	
			comm.Parameters.Add("@HistoryIndex",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SubBuyingDeliveryHistoryIndex").Text));	
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			
		}


		/// <summary>
		/// 검사품이면서 최종공정인경우의 품질검사원장 삭제
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private void QI_HTDelete(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string State = "";
			string Item = "";
			string str  = "Select ProgressCondition,ItemNum From QI_HT Where HistoryIndex1 = @index and HistorySection1='작업일보'";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				State = dr["ProgressCondition"].ToString();
				Item = dr["ItemNum"].ToString();
			}
			dr.Close();
			if(State == "대기")
			{
				str = "Delete From QI_HT Where HistoryIndex1 = @index and HistorySection1='작업일보'";
			
				
				comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
				comm.CommandText = 	str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
				throw new Exception("품목번호 "+Item+"인 품목은 품질검사가 이미 완료되어 삭제가 불가능 합니다!");
		}

		/// <summary>
		/// 무검사품이면서 제품이면서 최종공정인경우의 영업창고입고의뢰원장 삭제
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private void ISR_HTDelete(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string State = "";
			string Item = "";
			string str  = "Select ProgressCondition,ItemNum From ISR_HT Where HistoryIndex = @index and HistorySection='작업일보'";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			comm.Parameters.Clear();
			while(dr.Read())
			{
				State = dr["ProgressCondition"].ToString();
				Item = dr["ItemNum"].ToString();
			}
			dr.Close();
			if(State == "대기")
			{
				str = "Delete From ISR_HT Where HistoryIndex = @index and HistorySection='작업일보'";
			
				comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
				throw new Exception("품목번호 "+Item+"인 품목은 이미 입고완료되어 삭제가 불가능 합니다!");
		}


		/// <summary>
		/// 무검사품이면서 제품이면서 최종공정인경우 품질검사원장에서 삭제
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void NonQI_HTDelete(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			
			string str  = "Delete From QI_HT Where HistoryIndex1 = @index and HistorySection1='작업일보'";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction= tr;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 구매발주 품목 삭제시 진행되는 구매의뢰원장 진행상태 변경
		/// </summary>
		/// <param name="rowcount"></param>
		/// <param name="index">구매의뢰원장번호</param>
		private void BuyingRequestUpdate(SqlConnection conn, SqlTransaction tr, int rowcount, int index)
		{
			//구매의뢰 원장에 진행상태는
			string str="Update BR_HT Set ProgressCondition = '대기' Where BuyingRequestHistoryIndex = @idx";
			
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = index;
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 외주발주 품목 삭제시 진행되는 외주의뢰원장 진행상태 변경
		/// </summary>
		/// <param name="rowcount"></param>
		/// <param name="index"></param>
		private void OutSideOrderRequestUpdate(SqlConnection conn, SqlTransaction tr, int rowcount, int index)
		{
			string str="Update OOR_HT Set ProgressCondition = '대기' Where OutSideOrderRequestHistoryIndex = @idx";
			
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = index;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 구매,외주납품 삭제시 진행되는 입고의뢰원장 삭제
		/// </summary>
		private void InStoreRequestDelete(SqlConnection conn, SqlTransaction tr,int index, string section)
		{
			string str = "Delete ISR_HT Where HistoryIndex = @idx and HistorySection = @section";
			
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = index;
			comm.Parameters.Add("@section", SqlDbType.VarChar).Value = section;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 구매,외주납품 삭제시 진행되는 매입매출테이블 매입금액및 미지급금액 마이너스 증가
		/// </summary>
		/// <param name="rowcount"></param>
		private void SaleTable(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			int year = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Year").Value.ToString());
			int mon = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("Month").Value.ToString());
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("TotalCost").Text));
			comm.Parameters.Add("@year",year);
			Table table = new Table(year,mon);

			comm.CommandText = table.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
		}

        

		/// <summary>
		/// 무검사 외주납품 삭제 이력 등록
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void BOS_HTUnCheckdOutUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,'삭제', @Date,@Person, @PersonID,'외주납품',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			
			comm.Parameters.Add("@itemnum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
			comm.Parameters.Add("@itemdrawnum", m_Grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);							//도면번호
			comm.Parameters.Add("@itemname",m_Grid.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
			comm.Parameters.Add("@CompanyName",m_Grid.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text);
			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ProcessSequenceNum").Text));							//공정순서
			comm.Parameters.Add("@ProcessCode",m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text);						//공정코드
			comm.Parameters.Add("@ProcessName",m_Grid.Rows[rowcount].Cells.FromKey("ProcessName").Text);							//공정명
			comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
			comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());
			comm.Parameters.Add("@Person",m_UserName.ToString());
			comm.Parameters.Add("@PersonID", m_User.ToString());	
			comm.Parameters.Add("@HistoryIndex",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutSideDeliveryHistoryIndex").Text));												//원장번호

			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 외주납품 삭제시 외주발주원장 진행상태와 수량변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void OutSideOrderUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			//잔량은 누적치를 넣어야 한다.
			//그러므로 구매발주원장의 잔량을 가져온다.
			decimal Remain = 0;
			decimal Total = 0;
			string str  = "select * from OO_HT Where OutSideOrderHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("OutSideOrderHistoryIndex").Text;
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Remain = decimal.Parse(dr["RemainQuantity"].ToString());
				Total = decimal.Parse(dr["OrderQuantity"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();

			Remain = Remain + Decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Text);

			if(Remain == Total)
				str ="UPDATE OO_HT SET ProgressCondition = '대기', RemainQuantity = @remain WHERE OutSideOrderHistoryIndex = @idx";
			else
				str ="UPDATE OO_HT SET ProgressCondition = '진행', RemainQuantity = @remain WHERE OutSideOrderHistoryIndex = @idx";
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutSideOrderHistoryIndex").Text);
			comm.Parameters.Add("@remain", SqlDbType.Decimal).Value = Remain;
			comm.CommandText = str;
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 검사품 구매납품삭제시 구매발주원장 입고대기수량 마이너스 증가
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void BuyingOrderInStoreWaitingQuantityUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str = "UPDATE BO_HT SET InStoreWaitingQuantity = InStoreWaitingQuantity + @InStoreWaitingQuantity WHERE BuyingOrderHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			if(m_PageName == "QualityInspectionPC")
			{
				comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex2").Text);//구매발주원장번호
				comm.Parameters.Add("@InStoreWaitingQuantity", decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text));//의뢰수량
			}
			else
			{
				comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BuyingOrderHistoryIndex").Text);//구매발주원장번호
				comm.Parameters.Add("@InStoreWaitingQuantity", -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Text));//납품수량
			}
			comm.CommandText = str;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 구매납품 삭제시 구매발주원장 진행상태와 수량변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void BuyingOrderUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			//잔량은 누적치를 넣어야 한다.
			//그러므로 구매발주원장의 잔량을 가져온다.
			decimal Remain = 0;
			decimal Total = 0;
			string str  = "select * from BO_HT Where BuyingOrderHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("BuyingOrderHistoryIndex").Text;
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Remain = decimal.Parse(dr["RemainQuantity"].ToString());
				//				if(Remain <= 0)
				//					Remain = 0;
				Total = decimal.Parse(dr["OrderQuantity"].ToString());
			}
			dr.Close();
			comm.Parameters.Clear();

			Remain = Remain + Decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Text);

			if(Remain == Total)
				str = "UPDATE BO_HT SET ProgressCondition = '대기', RemainQuantity = @remain WHERE BuyingOrderHistoryIndex = @idx";
			else
				str = "UPDATE BO_HT SET ProgressCondition = '진행', RemainQuantity = @remain WHERE BuyingOrderHistoryIndex = @idx";
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BuyingOrderHistoryIndex").Text);
			comm.Parameters.Add("@remain", SqlDbType.Decimal).Value = Remain;
			comm.CommandText = str;
			comm.ExecuteNonQuery();
		}




		/// <summary>
		/// 구매,외주납품 삭제시 무검사품에 대한 매입원장 레코드 삭제
		/// </summary>
		/// <param name="rowcount"></param>
		private void BuyingHistoryDelete(SqlConnection conn, SqlTransaction tr,int rowcount,string section)
		{
			string str="Delete B_HT Where HistoryIndex = @idx and HistorySection = @setcion";
			
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			if(section == "구매납품")
				comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Text;
			else
				comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("OutSideDeliveryHistoryIndex").Text;
			
			comm.Parameters.Add("@setcion", SqlDbType.VarChar).Value = section;
			comm.ExecuteNonQuery();
		}

		private void SH_HT_BD_HTDelete(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str="Delete SH_HT Where HistoryIndex = @idx and HistoryDivision = '구매납품'";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("BuyingDeliveryHistoryIndex").Text;
			comm.ExecuteNonQuery();
		}

		private void SH_HT_OD_HTDelete(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str="Delete SH_HT Where HistoryIndex = @idx and HistoryDivision = '외주납품'";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("OutSideDeliveryHistoryIndex").Text;
			comm.ExecuteNonQuery();
		}

		private void SH_WDR_HTDelete(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str="Delete SH_HT Where HistoryIndex = @idx and HistoryDivision = '작업일보'";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey(m_Index.ToString()).Text));
			comm.ExecuteNonQuery();
		}


		private bool ExistPPIndex(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str="Select isnull(ProductionPlanHistoryIndex,0) From WDR_HT Where WorkDailyReportHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey(m_Index.ToString()).Text));
			int index = int.Parse(comm.ExecuteScalar().ToString());

			if(index == 0)
				return false;
			else
				return true;
		}
		
		private void PP_Update(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str="Update PP_HT Set ProductionCompleteQuantity = ProductionCompleteQuantity - @Quantity Where ProductionPlanHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Text));
			comm.Parameters.Add("@idx",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ProductionPlanHistoryIndex").Text));
			comm.ExecuteNonQuery();
		}

		private void BOS_BD_HTDelete(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str="Delete BOS_HT Where HistoryIndex = @idx and HistoryDivision = '구매납품'";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey(m_Index.ToString()).Text));
			comm.ExecuteNonQuery();
		}

		private void BOS_OS_HTDelete(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str="Delete BOS_HT Where HistoryIndex = @idx and HistoryDivision = '외주납품'";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey(m_Index.ToString()).Text));
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 검사품 외주납품삭제시 외주발주원장 입고대기수량 마이너스 증가
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void OutSideOrderInStoreWaitingQuantityUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str = "UPDATE OO_HT SET InStoreWaitingQuantity = InStoreWaitingQuantity + @InStoreWaitingQuantity WHERE OutSideOrderHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			if(m_PageName == "QualityInspectionPC")
			{
				comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex2").Text);//외주발주원장번호
				comm.Parameters.Add("@InStoreWaitingQuantity", decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity").Text));//의뢰수량
			}
			else
			{
				comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutSideOrderHistoryIndex").Text);//외주발주원장번호
				comm.Parameters.Add("@InStoreWaitingQuantity", -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Text));//납품수량
			}
			comm.CommandText = str;
			comm.ExecuteNonQuery();
		}



		/// <summary>
		/// 구매,외주납품삭제시 진행되는 품질검사원장 레코드 삭제
		/// </summary>
		/// <param name="rowcount"></param>
		private void QualityInspectionDelete(SqlConnection conn, SqlTransaction tr,int index, int index1, string section, string section1)
		{
			string str = "Select * From QI_HT Where HistoryIndex1 = @idx and HistorySection1 = @section and HistoryIndex2 = @idx1 and HistorySection2 = @section1";
			string Condition = "";
			string Item = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = index;
			comm.Parameters.Add("@idx1", SqlDbType.Int).Value = index1;
			comm.Parameters.Add("@section", SqlDbType.VarChar).Value = section;
			comm.Parameters.Add("@section1", SqlDbType.VarChar).Value = section1;
			comm.CommandText= str;
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			comm.Parameters.Clear();
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				Condition = dr["ProgressCondition"].ToString();
				Item = dr["ItemNum"].ToString();
			}

			if(Condition == "대기")
			{
				str="Delete QI_HT Where HistoryIndex1 = @idx and HistorySection1 = @section and HistoryIndex2 = @idx1 and HistorySection2 = @section1";
			
				comm.Parameters.Add("@idx", SqlDbType.Int).Value = index;
				comm.Parameters.Add("@idx1", SqlDbType.Int).Value = index1;
				comm.Parameters.Add("@section", SqlDbType.VarChar).Value = section;
				comm.Parameters.Add("@section1", SqlDbType.VarChar).Value = section1;
				comm.CommandText = str;
				comm.ExecuteNonQuery();
			}
			else
			{
				throw new Exception("품목번호가 "+Item + "인 품목은 품질검사가 완료되어 삭제가 불가능 합니다!");
			}
		}



		/// <summary>
		/// 무검사 구매,외주납품삭제시 진행되는 품질검사원장 레코드 삭제
		/// </summary>
		/// <param name="rowcount"></param>
		private int NonQualityInspectionDelete(SqlConnection conn, SqlTransaction tr,int index, int index1, string section, string section1)
		{
			int count = 0;
			string str = "Select * From QI_HT Where HistoryIndex1 = @idx and HistorySection1 = @section and HistoryIndex2 = @idx1 and HistorySection2 = @section1";
			string Condition = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = index;
			comm.Parameters.Add("@idx1", SqlDbType.Int).Value = index1;
			comm.Parameters.Add("@section", SqlDbType.VarChar).Value = section;
			comm.Parameters.Add("@section1", SqlDbType.VarChar).Value = section1;
			comm.CommandText= str;
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			comm.Parameters.Clear();
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				Condition = dr["ProgressCondition"].ToString();
				count = int.Parse(dr["QualityInspectionHistoryIndex"].ToString());
			}
			str="Delete QI_HT Where HistoryIndex1 = @idx and HistorySection1 = @section and HistoryIndex2 = @idx1 and HistorySection2 = @section1";
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = index;
			comm.Parameters.Add("@idx1", SqlDbType.Int).Value = index1;
			comm.Parameters.Add("@section", SqlDbType.VarChar).Value = section;
			comm.Parameters.Add("@section1", SqlDbType.VarChar).Value = section1;
			comm.CommandText= str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			if(count == 0)
				throw new Exception("품질검사원장에 해당 레코드가 존재하지 않습니다!");

			return count;
		}

		/// <summary>
		/// 해당 품목의 최종공정인지 판
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		private bool MaxSequence(SqlConnection conn, SqlTransaction tr,int index)
		{
			string ItemNum = "" ;
			int Sequence =0 ;
			string str = "Select ItemNum, EndProcessSequenceNum From QI_HT Where QualityInspectionHistoryIndex = @index";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@index", index);
			comm.CommandText= str;
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			comm.Parameters.Clear();
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				ItemNum = dr["ItemNum"].ToString();
				Sequence = int.Parse(dr["EndProcessSequenceNum"].ToString());
			}
			
			str="Select Count(*) From PSI_MT where RecodingState = 1 and ItemNum = @ItemNum and ProcessSequenceNum > @Num";
			comm.Parameters.Add("@ItemNum", ItemNum);
			comm.Parameters.Add("@Num", Sequence);
			comm.CommandText= str;
			int count = int.Parse(comm.ExecuteScalar().ToString());

			//높은 순위 공정이므로 찾는 공정보다 높은 공순이 없으면 최종공정
			if(count == 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// 구매,외주납품 삭제시 진행되는 생산창고테이블 수량변경
		/// 생산창고의 입고수량을 마이너스 증가시킨다.
		/// </summary>
		/// <param name="rowcount"></param>
		private void ProductionTable(SqlConnection conn, SqlTransaction tr,int rowcount, string process)
		{

			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;
			

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			Decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = Decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			

			///////////////////////////////////////////
			// 창고의 금액을 위해 공정정보에서       //
			// 진척율을   가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal rate = 0;
			string str_rate = "Select ProgressRate From PSI_MT Where RecodingState =1 and ItemNum = @ItemNum and ProcessCode = @code";
			SqlCommand comm_rate = new SqlCommand(str_rate,conn);
			comm_rate.Transaction = tr;
			comm_rate.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm_rate.Parameters.Add("@code", SqlDbType.VarChar).Value = process;
			SqlDataReader dr_rate = comm_rate.ExecuteReader();
			if(dr_rate.Read())
				rate = decimal.Parse(dr_rate["ProgressRate"].ToString());
			dr_rate.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 공정정보에서       //
			// 진척율을   가져와 계산한다.   -끝-    //
			///////////////////////////////////////////

			
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Text);
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = process;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Text)*cost*rate/100);
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);

			comm.CommandText= table.InProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @num and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = process;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}


		}


		/// <summary>
		/// 영업창고 수량 마이너스 증가
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void BS_MTDelete(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			Decimal m_cost = 0;
			string str = "";
			// 품목의 금액
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
			if(dr_cost.Read())
				m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			dr_cost.Close();

			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;


			Table table = new Table(year,mon);
			str = table.InBusinessTable();
			
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@storenum",SqlDbType.VarChar).Value = 1;

			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -( Decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString()));

			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -( Decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString())) * Decimal.Parse(m_cost.ToString());
			comm.Parameters.Add("@year",year);
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 외주납품현황에서 수량만큼 마이너스 출고 
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		/// <param name="process"></param>
		private void OutSideTable(SqlConnection conn, SqlTransaction tr,int rowcount)
		{

			//시작공정코드,시작공정순서
			string begincode = "";
			int beginsequence = 0;
			string itemnum = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();

			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;

			//발주원장 인덱스가 동일한 것을 찾으면 시작공정을 찾을수 있다.
			string str = @"Select ItemNum, BeginProcessCode, BusinessRegistrationNum  From OO_HT Where OutSideOrderHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@idx",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutSideOrderHistoryIndex").Value.ToString());
			comm.CommandText = str;
			DataSet ds_code = new DataSet();
			SqlDataAdapter da_code = new SqlDataAdapter(comm);
			da_code.Fill(ds_code);
			comm.Parameters.Clear();

			foreach(DataRow dr_code in ds_code.Tables[0].Rows)
			{
				begincode = dr_code["BeginProcessCode"].ToString();
				str = "Select * From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
				comm.Parameters.Add("@num", dr_code["ItemNum"].ToString());
				comm.Parameters.Add("@code",dr_code["BeginProcessCode"].ToString());
				comm.CommandText = str;
				SqlDataReader dr1 = comm.ExecuteReader();
				while(dr1.Read())
				{
					beginsequence = int.Parse(dr1["ProcessSequenceNum"].ToString());
				}
				dr1.Close();
				comm.Parameters.Clear();

			}
			
			//해당 품목이 가장 낮은 공정이면 하위품목들의 최상위공정을 떨어주고
			//그렇지 않으면 해당공정의 바로 아래공정품을 외주창고에서 떨어낸다
			//이때 하위품목의 자산분류가 원자재이면 해당 품목을 떨어준다

			Decimal m_cost = 0;
			Decimal m_progressrate = 100;

			str = @"Select isnull(Min(ProcessSequenceNum),0) From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum < @sequence";
			comm.Parameters.Add("@num",itemnum);
			comm.Parameters.Add("@sequence",beginsequence);
			comm.CommandText = str;

			if(int.Parse(comm.ExecuteScalar().ToString()) == 0)
			{
				comm.Parameters.Clear();
				//공정순서에 가서 해당자품목을 떨어준다
				str = @"Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @num";
				comm.Parameters.Add("@num",itemnum);
				comm.CommandText = str;
				DataSet ds = new DataSet();
				SqlDataAdapter da = new SqlDataAdapter(comm);
				da.Fill(ds);
				foreach(DataRow dr in ds.Tables[0].Rows)
				{
					//하위품목이 원자재이면 바로 떨어준다
					if(Division(dr["ChildItemNum"].ToString()) == 1)
					{
						// 품목의 금액
						string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
						SqlCommand comm_cost = new SqlCommand(str_cost,conn);
						comm_cost.Transaction = tr;
						comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
						if(dr_cost.Read())
							m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
						dr_cost.Close();

						
						decimal a = 0;
						str = @"Select * From IOI_MT Where RecodingState = 1 and ParentItemNum = @parent and ChildItemNum = @child";
						SqlCommand comm1 = new SqlCommand(str,conn);
						comm1.Transaction = tr;
						comm1.Parameters.Add("@parent",itemnum);
						comm1.Parameters.Add("@child",dr["ChildItemNum"].ToString());
						SqlDataReader dr_1 =  comm1.ExecuteReader();
						while(dr_1.Read())
						{
							a = decimal.Parse(dr_1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr_1["NeedQuantityDenominator"].ToString());
						}
						dr_1.Close();
						comm1.Parameters.Clear();

						if(a == 0)
							throw new Exception(itemnum + " 품목의 품목구성이 없거나 올바르지 않습니다!");
						Table table = new Table(year,mon);
						str = table.OutTable();
						SqlCommand comm2 = new SqlCommand(str,conn);
						comm2.Transaction = tr;
						comm2.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm2.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
						comm2.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -a * Decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());

						comm2.Parameters.Add("@code",SqlDbType.VarChar).Value = "14000000";
						comm2.Parameters.Add("@cost",SqlDbType.Decimal).Value = -a * Decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString())* Decimal.Parse(m_cost.ToString()) ;
						comm2.Parameters.Add("@year",year);

						comm2.ExecuteNonQuery();

						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  dr["ChildItemNum"].ToString();
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = "14000000";
							comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
							comm.Parameters.Add("@year",DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
							comm.Parameters.Clear();
						
							string StockQuantity = "False";
							string ItemName = dr["ChildItemNum"].ToString();
							if(reader.Read())
							{
								if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
								{
									StockQuantity = "True";
								}
							}
							reader.Close();

							if(StockQuantity == "True")
							{
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}

						}

					}
					else
					{
						//공정품이면 자품목의 최대공정을 떨어준다.
						string code = "";
						str = @"Select ProcessCode, ProcessSequenceNum From PSI_MT Where RecodingState = 1 and 
							ItemNum = @num and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT 
							Where RecodingState = 1 and ItemNum = @num)";
						SqlCommand comm_code = new SqlCommand(str,conn);
						comm_code.Transaction = tr;
						comm_code.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						SqlDataReader dr_Code = comm_code.ExecuteReader();
						while(dr_Code.Read())
						{
							code = dr_Code["ProcessCode"].ToString();
						}
						dr_Code.Close();
						comm_code.Parameters.Clear();

						// 품목의 금액
						string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
						SqlCommand comm_cost = new SqlCommand(str_cost,conn);
						comm_cost.Transaction = tr;
						comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
						if(dr_cost.Read())
							m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
						dr_cost.Close();

						// 품목의 진척율
						string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
						SqlCommand comm_rate = new SqlCommand(str_rate,conn);
						comm_rate.Transaction = tr;
						comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
						SqlDataReader dr_rate = comm_rate.ExecuteReader();			
						if(dr_rate.Read())
							m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
						dr_rate.Close();
						comm_rate.Parameters.Clear();

						Table table = new Table(year,mon);
						str = table.OutTable();
						SqlCommand comm2 = new SqlCommand(str,conn);
						comm2.Transaction = tr;
						comm2.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
						comm2.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
						comm2.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -Decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());
						comm2.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
						comm2.Parameters.Add("@cost",SqlDbType.Decimal).Value = -Decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString())* Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100;
						comm2.Parameters.Add("@year",year);

						comm2.ExecuteNonQuery();


						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  dr["ChildItemNum"].ToString();
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
							comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
							comm.Parameters.Add("@year",DateTime.Now.Year);
							comm.CommandText = strSQL;
							SqlDataReader reader = comm.ExecuteReader();
							comm.Parameters.Clear();						
							string StockQuantity = "False";
							string ItemName = dr["ChildItemNum"].ToString();
							if(reader.Read())
							{
								if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
								{
									StockQuantity = "True";
								}
							}
							reader.Close();

							if(StockQuantity == "True")
							{
								throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
							}

						}

						

					}
				}
				

			}
			else
			{
				comm.Parameters.Clear();
				//해당품목의 바로 아래 공정을 떨어준다
				string code = "";
				str = @"Select ProcessCode, ProcessSequenceNum From PSI_MT Where RecodingState = 1 and 
							ItemNum = @num and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT 
							Where RecodingState = 1 and ItemNum = @num and ProcessSequenceNum < @sequence)";
				SqlCommand comm_code = new SqlCommand(str,conn);
				comm_code.Transaction = tr;
				comm_code.Parameters.Add("@num",itemnum);
				comm_code.Parameters.Add("@sequence",beginsequence);
				SqlDataReader dr_Code = comm_code.ExecuteReader();

				//				comm.Parameters.Add("@num",itemnum);
				//				comm.Parameters.Add("@sequence",beginsequence);
				//				comm.CommandText = str;
				//				SqlDataReader dr_Code = comm.ExecuteReader();
				while(dr_Code.Read())
				{
					code = dr_Code["ProcessCode"].ToString();
				}
				dr_Code.Close();
				comm_code.Parameters.Clear();

				// 품목의 금액
				string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
				SqlCommand comm_cost = new SqlCommand(str_cost,conn);
				comm_cost.Transaction = tr;
				comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum;
				SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
				if(dr_cost.Read())
					m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
				dr_cost.Close();

				// 품목의 진척율
				string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
				SqlCommand comm_rate = new SqlCommand(str_rate,conn);
				comm_rate.Transaction = tr;
				comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum;
				comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
				SqlDataReader dr_rate = comm_rate.ExecuteReader();			
				if(dr_rate.Read())
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
				dr_rate.Close();
				comm_rate.Parameters.Clear();

				//외주창고에 떨어줄 품목
				Table table = new Table(year,mon);
				str = table.OutTable();
				SqlCommand comm2 = new SqlCommand(str,conn);
				comm2.Transaction = tr;
				comm2.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum;
				comm2.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
				comm2.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString());

				comm2.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
				comm2.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Value.ToString())* Decimal.Parse(m_cost.ToString()) * Decimal.Parse(m_progressrate.ToString())/100;
				comm2.Parameters.Add("@year",year);

				comm2.ExecuteNonQuery();

				if(MinusStore() == false)
				{
					string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  itemnum;
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = code;
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
						
					string StockQuantity = "False";
					string ItemName = itemnum;
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
					}
				}
			}
		}



		/// <summary>
		/// 원자재 창고 입고수량 마이너스증가
		/// </summary>
		/// <param name="rowcount"></param>
		/// <param name="process"></param>
		private void RowTable(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text).Month;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			Decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = Decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Text);
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(cost * decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryQuantity").Text));
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText= table.InRowTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스재고를 허용하지 않으면 
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
		}


		/// <summary>
		/// 취소수량만큼 창고수량변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void Out_Table(SqlConnection conn, SqlTransaction tr,int rowcount)
		{

			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseDate").Text).Month;
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			decimal m_cost=0;
			Table table = new Table(year,mon);

			// 테이블 수량이동
			// 선택된 품목이 원자재이면 원자재창고의 출고수량을 마이너스증가하고 외주창고 입고수량을 마이너스증가
			// 그렇지 않으면 생산창고의 출고수량 마이너스 증가 한뒤 외주창고 입고수량 마이너스증가
			

			if(!WorkPlan3() || (StoreManage(conn,tr,m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text) == "예"))
			{


				// 선택된 품목의 자산분류를 확인한다.0이면 원자재이므로 원자재 창고의 출고수량을 마이너스증가시킨다
				if(Division(rowcount) == 1)
				{
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.CommandText = str_cost;
					SqlDataReader dr_cost = comm.ExecuteReader();
					if(dr_cost.Read())
						m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
					dr_cost.Close();
					comm.Parameters.Clear();

					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(m_cost*decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
					comm.Parameters.Add("@year",year);

					comm.CommandText= table.OutRowTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
						
				}
				else// 생산창고 출고수량 마이너스 증가
				{
					// 창고금액은 품목정보의 기준단가와 공정순서의 진척율을 곱해서 구한다.
					string str_cost = "SELECT ProgressRate, StandardUnitCost FROM II_MT INNER JOIN PSI_MT ON II_MT.ItemNum = PSI_MT.ItemNum Where II_MT.RecodingState = 1 and II_MT.ItemNum = @num and PSI_MT.ProcessSequenceNum = @sequence";
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@sequence",SqlDbType.Int).Value  = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ProcessSequenceNum").Text);
					comm.CommandText = str_cost;
					SqlDataReader dr_cost = comm.ExecuteReader();
					if(dr_cost.Read())
						m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString()) * decimal.Parse(dr_cost["ProgressRate"].ToString())/100;
					dr_cost.Close();
					comm.Parameters.Clear();

					//품목의 진척율
					decimal m_progressrate = 100;
					string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
					SqlCommand comm_rate = new SqlCommand(str_rate,conn);
					comm_rate.Transaction = tr;
					comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
					SqlDataReader dr_rate = comm_rate.ExecuteReader();

					if(dr_rate.Read())
					{
						if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
							m_progressrate = 0;
						else
							m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
					}
					dr_rate.Close();
					comm_rate.Parameters.Clear();

					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(m_cost*decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value  = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
					comm.Parameters.Add("@year",year);
						
					comm.CommandText= table.OutProductionTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}
			}

			// 외주창고 입고수량 마이너스 증가

			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Text);
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(m_cost*decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThistimeOutStorehouseQuantity").Text));
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
			comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
			comm.Parameters.Add("@year",year);

			comm.CommandText= table.InTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
				comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}	

		}


		/// <summary>
		/// 재고관리여부
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private string StoreManage(SqlConnection con, SqlTransaction trans, string ItemNum)
		{
			string store = "아니오";
			string str = "Select Unit4 From II_MT Where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				store = dr["Unit4"].ToString();				
			}
			dr.Close();

			return store;
		}



		/// <summary>
		/// 이력원장에서 외주출고품목 삭제삭제
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void SH_HT_OOS_HTDelete(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			string str = @"Delete From SH_HT Where HistoryDivision = '외주출고' and HistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@index",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutSideOutStorehouseHistoryIndex").Text));
			comm.ExecuteNonQuery();


		}


		private void BuyingTable(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			switch(m_PageName)
			{
				case "CollectMoneyRegistrationPC"://수금등록삭제
					Table table2 = new Table(DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("CollectMoneyDate").Text).Year,DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("CollectMoneyDate").Text).Month);
					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ItemPaymentCost").Text);
					comm.Parameters.Add("@year",DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("CollectMoneyDate").Text).Year);
					comm.CommandText = table2.CollectMoneyDiminutionUnCollectMoneyIncreaseTable();
					break;
				case "ClaimRegistrationPC":			//클레임삭제
					Table table1 = new Table(DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year, DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Month);
					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Text);
					comm.Parameters.Add("@year",DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year);
					comm.CommandText = table1.ClaimMoneyIncreaseTable();
					break;
				case "ClaimPC":			//클레임삭제
					Table table3 = new Table(DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year, DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Month);
					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Text);
					comm.Parameters.Add("@year",DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year);
					comm.CommandText = table3.PayClaimMoneyIncreaseTable();
					break;
				case "EtcClaimPC":			//기타공제 삭제
					Table table4 = new Table(DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year, DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Month);
					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Text);
					comm.Parameters.Add("@year",DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year);
					comm.CommandText = table4.PayClaimMoneyIncreaseTable();
					break;
				
				default:		//지급등록삭제
					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ItemPaymentCost").Text);
					comm.Parameters.Add("@year",DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("PaymentDate").Text).Year);
					Table table = new Table(DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("PaymentDate").Text).Year, DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("PaymentDate").Text).Month);
					comm.CommandText = table.UNCollectMoneyDiminutionPaymentMoneyIncreaseTable();
					break;
			}
			comm.ExecuteNonQuery();
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////                             //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		///////////////////////////////// 영업관련 원장및 테이블 삭제 //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/////////////////////////////////                             //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// 영업창고입고원장 레코드 삭제시 입고의뢰원장 진행상태 변경 함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void ISR_HTUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str = "Update ISR_HT Set ProgressCondition = '대기' where InStorehouseRequestHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseRequestHistoryIndex").Text);
			comm.ExecuteNonQuery();
			
		}
		

		/// <summary>
		/// 상품 구매납품원장 진행상태 변경(완료=>대기)
		/// 무검사인경우
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void BD_HTUpdate1(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			if(!Check(rowcount) && Division(rowcount)==4)
			{
				
				string str = "Select Max(HistoryIndex) From ISR_HT where InStorehouseRequestHistoryIndex = @index";
				SqlCommand comm = new SqlCommand();
				comm.Connection = conn;
				comm.Transaction = tr;
				comm.CommandText = str;
				comm.Parameters.Add("@index", int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseRequestHistoryIndex").Text));//입고의뢰원장번호

				int count = int.Parse(comm.ExecuteScalar().ToString());//품질검사원장 번호
				comm.Parameters.Clear();

				str = "Select HistoryIndex1 From QI_HT where QualityInspectionHistoryIndex = @index";
				comm.CommandText = str;
				comm.Parameters.Add("@index", count);//품질검사원장 번호
				count = int.Parse(comm.ExecuteScalar().ToString());//구매납품원장번호
				comm.Parameters.Clear();
			
				str = "Update BD_HT Set DeliveryQuantity = @DeliveryQuantity, ProgressCondition = '대기' Where BuyingDeliveryHistoryIndex = @index";
				comm.CommandText = str;
				comm.Parameters.Add("@DeliveryQuantity", decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseQuantity").Text));//입고수량
				comm.Parameters.Add("@index", count);//구매납품원장번호
				comm.ExecuteNonQuery();
			}
		}


		/// <summary>
		/// 영업창고 입고원장 레코드 삭제시 품질검사 원장 진행상태 변경 함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void QI_HTUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			// 품질검사원장 인덱스 찾기
			int historyindex=0;
			string str_QI = "Select HistoryIndex, HistorySection From ISR_HT Where InStorehouseRequestHistoryIndex = @index";
			SqlCommand comm_QI = new SqlCommand(str_QI, conn);
			comm_QI.Transaction = tr;
			comm_QI.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseRequestHistoryIndex").Text);
			SqlDataReader dr = comm_QI.ExecuteReader();
			while(dr.Read())
			{
				historyindex = int.Parse(dr["HistoryIndex"].ToString());
			}
			dr.Close();

			string str = "Update QI_HT Set ProgressCondition = '검사완료' where QualityInspectionHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = historyindex;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 영업창고입고원장 삭제시 영업창고 입고수량 마이너스 증가
		/// </summary>
		/// <param name="rowcount"></param>
		private void InBS_MTUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStoreDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStoreDate").Text).Month;
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
				
			///////////////////////////////////////////
			// 영업창고 번호를 입고원장에서 가져온다 //
			//   -시작-                              //
			///////////////////////////////////////////
			int num = 0;
			string str_num = "Select BusinessStorehouseNum From IS_HT Where InStorehouseHistoryIndex = @index";
			SqlCommand comm_num = new SqlCommand(str_num,conn);
			comm_num.Transaction = tr;
			comm_num.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseHistoryIndex").Text);
			SqlDataReader dr_num = comm_num.ExecuteReader();
			if(dr_num.Read())
				num = int.Parse(dr_num["BusinessStorehouseNum"].ToString());
			dr_num.Close();
			///////////////////////////////////////////
			// 영업창고 번호를 입고원장에서 가져온다 //
			//   -끝-                                //
			///////////////////////////////////////////

			
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseQuantity").Text));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseQuantity").Text) * cost);
			comm.Parameters.Add("@storenum", SqlDbType.Int).Value = num;
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText= table.InBusinessTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@storenum", SqlDbType.Int).Value = num;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}


		}

		/// <summary>
		/// 영업창고입고원장 삭제시 생산창고 출고수량 마이너스 증가
		/// </summary>
		/// <param name="rowcount"></param>
		private void OutPS_MTUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStoreDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStoreDate").Text).Month;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
				
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseQuantity").Text));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseQuantity").Text) * cost);
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText= table.OutProductionTable();
			comm.ExecuteNonQuery();
		}

		private void SH_HTDelete(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			string str = @"Delete From SH_HT Where HistoryDivision = '제품입고' and HistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@index",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseRequestHistoryIndex").Text));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		private void SH_HT_OS_HTDelete(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			DataSet ds = new DataSet();

			decimal m_cost = 0;
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Month;
			Table table = new Table(year,mon);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			string str = @"Select ItemNum, Quantity From SH_HT Where StoreName ='원자재창고' and HistoryDivision = '제품출고' and HistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@index",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Text));
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(ds);
			string str_cost = "";
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				// 품목의 금액
				str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
				SqlCommand comm_cost = new SqlCommand(str_cost,conn);
				comm_cost.Transaction = tr;
				comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ItemNum"].ToString();
				SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
				if(dr_cost.Read())
					m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
				dr_cost.Close();

				str = table.OutRowTable();
				SqlCommand comm2 = new SqlCommand(str,conn);
				comm2.Transaction = tr;
				comm2.Parameters.Add("@num",SqlDbType.VarChar).Value = dr["ItemNum"].ToString();
				comm2.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(Decimal.Parse(dr["Quantity"].ToString()));
				comm2.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(Decimal.Parse(dr["Quantity"].ToString())* Decimal.Parse(m_cost.ToString())); //m_cost * -(Decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString())* Decimal.Parse(m_cost.ToString())) ;
				comm2.Parameters.Add("@year",year);
				comm2.ExecuteNonQuery();
			}
			
			str = @"Delete From SH_HT Where HistoryDivision = '제품출고' and HistoryIndex = @index";
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}


		
		/// <summary>
		/// 상품제품출고원장 삭제시 출고수량 감소시키는 함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void RO_HTUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
//			//일단 수주원장에서 출고수량을 가져와야 한다
//			decimal quantity = 0;
//			string str_RO = "Select OutStorehouseQuantity From RO_HT Where ReceivingOrderHistoryIndex = @index";
//			SqlCommand comm_RO = new SqlCommand(str_RO,conn);
//			comm_RO.Transaction = tr;
//			comm_RO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
//			SqlDataReader dr_RO = comm_RO.ExecuteReader();
//			while(dr_RO.Read())
//			{
//				quantity = decimal.Parse(dr_RO["OutStorehouseQuantity"].ToString());
//			}
//			dr_RO.Close();
//
//
//			//수주원장에서 가져온 출고수량에 지금 취소하는 수량을 빼준다
//			string str = "Update RO_HT Set OutStorehouseQuantity = @quantity, UnInspectionQuantity = @quantity where ReceivingOrderHistoryIndex = @index";
//			SqlCommand comm = new SqlCommand(str, conn);
//			comm.Transaction = tr;
//			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = quantity - decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Text);
//			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
//			comm.ExecuteNonQuery();


			//일단 수주원장에서 출고수량을 가져와야 한다
			decimal quantity = 0;
			string str_RO = "Select OutStorehouseQuantity From RO_HT Where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm_RO = new SqlCommand(str_RO,conn);
			comm_RO.Transaction = tr;
			comm_RO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
			SqlDataReader dr_RO = comm_RO.ExecuteReader();
			while(dr_RO.Read())
			{
				quantity = decimal.Parse(dr_RO["OutStorehouseQuantity"].ToString());
			}
			dr_RO.Close();


			//수주원장에서 가져온 출고수량에 지금 취소하는 수량을 빼준다
			string str = "Update RO_HT Set OutStorehouseQuantity = @quantity, UnInspectionQuantity = @quantity where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandType = CommandType.Text;
			comm.CommandText = str;
			comm.Transaction = tr;
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = quantity - decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Text);
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//잔량과 수주량이 동일하고 미검수량이 0이며 생산의뢰여부가 아니오면 진행상태를 대기로 변경시킨다.
			str = "Select * From RO_HT where ReceivingOrderHistoryIndex = @index and ProductionRequestDivision = '0' and UnInspectionQuantity='0'";
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			decimal Quantity = 0;
			decimal Quantity1 = 0;
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["TotalReceiveingOrderQuantity"].ToString());
				Quantity1 = decimal.Parse(dr["RemainderQuantity"].ToString());
			}
			dr.Close();
			

			if(Quantity == Quantity1 && Quantity != 0)
			{
				str = "Update RO_HT Set ProgressCondition = '대기' where ReceivingOrderHistoryIndex = @index ";
				comm.CommandText = str;
				comm.ExecuteNonQuery();
			
			}
		}


		/// <summary>
		/// 보용품 창고 출고수량 마이너스 증가
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void OutAS_MTUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;//
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Month;
			
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
				
			
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Text));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Text) * cost);
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText= table.OutAddItemTable();
			comm.ExecuteNonQuery();
		}
		/// <summary>
		/// 영업창고 출고수량 마이너스 증가
		/// </summary>
		/// <param name="rowcount"></param>
		private void OutBS_MTUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;//
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Month;
			
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
				
			///////////////////////////////////////////
			// 영업창고 번호를 출고원장에서 가져온다 //
			//   -시작-                              //
			///////////////////////////////////////////
			int num = 0;
			string str_num = "Select BusinessStorehouseNum From OS_HT Where OutStorehouseHistoryIndex = @index";
			SqlCommand comm_num = new SqlCommand(str_num,conn);
			comm_num.Transaction = tr;
			comm_num.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Text);
			SqlDataReader dr_num = comm_num.ExecuteReader();
			if(dr_num.Read())
				num = int.Parse(dr_num["BusinessStorehouseNum"].ToString());
			dr_num.Close();
			///////////////////////////////////////////
			// 영업창고 번호를 입고원장에서 가져온다 //
			//   -끝-                                //
			///////////////////////////////////////////

			

			
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Text));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Text) * cost);
			comm.Parameters.Add("@storenum", SqlDbType.Int).Value = num;
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText= table.OutBusinessTable();
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 출고원장 취소후 납품창고 입고수량 마이너스 증가
		/// </summary>
		/// <param name="rowcount"></param>
		private void InDS_MTUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Month;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
				
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Text));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Text) * cost);
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText= table.InDeliveryTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM DS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
		}

		/// <summary>
		/// 창고이동원장 삭제후 영업창고 수량변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void SM_HTTableUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;//

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////

			int year  = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RegistrationDate").Text).Year;
			int mon  = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RegistrationDate").Text).Month;


			// 영업창고 출고수량 마이너스증가(원창고)
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("MovingQuantity").Text));
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("MovingQuantity").Text)*cost);
			comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum1").Text);//원 창고번호
			comm.Parameters.Add("@year",year);


			Table table = new Table(year,mon);					
			
			comm.CommandText= table.OutBusinessTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			// 영업창고 입고수량 마이너스증가(이동창고)
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("MovingQuantity").Text));
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("MovingQuantity").Text)*cost);
			comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum2").Text);//이동 창고번호
			comm.Parameters.Add("@year",year);
			
			comm.CommandText= table.InBusinessTable();
			comm.ExecuteNonQuery();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum = and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum2").Text);//이동 창고번호
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
			comm.Parameters.Clear();


		}

		/// <summary>
		/// 매출원장 삭제시 수주원장 수량및 변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void RO_HTUpdate1(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			//			//일단 수주원장에서 출고수량과 합격수량을 가져와야 한다
			//			decimal quantity = 0;//수주원장 출고수량
			//			decimal suitquantity = 0;//수주원장 합격수량
			//			decimal remain = 0;
			//
			//			string str_RO = "Select OutStorehouseQuantity,SuitabilityQuantity, RemainderQuantity From RO_HT Where ReceivingOrderHistoryIndex = @index";
			//			SqlCommand comm_RO = new SqlCommand(str_RO,conn);
			//			comm_RO.Transaction = tr;
			//			comm_RO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
			//			SqlDataReader dr_RO = comm_RO.ExecuteReader();
			//			while(dr_RO.Read())
			//			{
			//				quantity = decimal.Parse(dr_RO["OutStorehouseQuantity"].ToString());
			//				suitquantity = decimal.Parse(dr_RO["SuitabilityQuantity"].ToString());
			//				remain = decimal.Parse(dr_RO["RemainderQuantity"].ToString());
			//			}
			//			dr_RO.Close();
			//
			//			//출고원장의 출고수량도 가져온다
			//			decimal quantity1 = 0;
			//			string str_OS = "Select OutStorehouseQuantity From OS_HT Where ReceivingOrderHistoryIndex = @index";
			//			SqlCommand comm_OS = new SqlCommand(str_OS,conn);
			//			comm_OS.Transaction = tr;
			//			comm_OS.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Text);
			//			SqlDataReader dr_OS = comm_OS.ExecuteReader();
			//			while(dr_OS.Read())
			//			{
			//				quantity1 = decimal.Parse(dr_OS["OutStorehouseQuantity"].ToString());
			//			}
			//			dr_OS.Close();
			//
			//			//매출원장의 합수량도 가져온다
			//			decimal SuitabilityQuantity = 0;
			//			string str_S = "Select SuitabilityQuantity From S_HT Where SaleHistoryIndex = @index";
			//			SqlCommand comm_S = new SqlCommand(str_S,conn);
			//			comm_S.Transaction = tr;
			//			comm_S.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleHistoryIndex").Text);
			//			SqlDataReader dr_S = comm_S.ExecuteReader();
			//			while(dr_S.Read())
			//			{
			//				SuitabilityQuantity = decimal.Parse(dr_S["SuitabilityQuantity"].ToString());
			//			}
			//			dr_S.Close();

			//수주원장에는 합격수량과 미검수량,출고수량 잔량을 수정한다.
			//출고수량은 출고원장의 출고수량을 빼준다
			//합격수량은 매출원장의 합격수량을 빼준다
			//미검수량은 출고원장의 출고수량을 빼준다
			//잔량은 출고수량에 매출원장의 합격수량을 빼준다
			//			string str = @"Update RO_HT Set OutStorehouseQuantity = @quantity, UnInspectionQuantity = @UnInspectionQuantity, 
			//						RemainderQuantity = @remain, SuitabilityQuantity = @SuitabilityQuantity
			//						where ReceivingOrderHistoryIndex = @index";
			//			SqlCommand comm = new SqlCommand(str, conn);
			//			comm.Transaction = tr;
			//			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = quantity - decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Text);
			//			comm.Parameters.Add("@UnInspectionQuantity",SqlDbType.Decimal).Value = suitquantity - quantity1;
			//			comm.Parameters.Add("@SuitabilityQuantity",SqlDbType.Decimal).Value = suitquantity - quantity1;
			//			comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = quantity - decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Text);
			//			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
			//			comm.ExecuteNonQuery();
			
			
			//출고원장의 출고수량 가져온다
			decimal quantity1 = 0;
			string str_OS = "Select OutStorehouseQuantity From OS_HT Where OutStorehouseHistoryIndex = @index";
			SqlCommand comm_OS = new SqlCommand(str_OS,conn);
			comm_OS.Transaction = tr;
			comm_OS.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Text);
			SqlDataReader dr_OS = comm_OS.ExecuteReader();
			while(dr_OS.Read())
			{
				quantity1 = decimal.Parse(dr_OS["OutStorehouseQuantity"].ToString());
			}
			dr_OS.Close();

			//매출원장의 합격수량도 가져온다
			decimal SuitabilityQuantity = 0;
			string str_S = "Select SuitabilityQuantity From S_HT Where SaleHistoryIndex = @index";
			SqlCommand comm_S = new SqlCommand(str_S,conn);
			comm_S.Transaction = tr;
			comm_S.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleHistoryIndex").Text);
			SqlDataReader dr_S = comm_S.ExecuteReader();
			while(dr_S.Read())
			{
				SuitabilityQuantity = decimal.Parse(dr_S["SuitabilityQuantity"].ToString());
			}
			dr_S.Close();
			
			string str = @"Update RO_HT Set UnInspectionQuantity = UnInspectionQuantity + @quantity, 
						RemainderQuantity = RemainderQuantity + @SuitabilityQuantity, SuitabilityQuantity = SuitabilityQuantity - @SuitabilityQuantity
						where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = quantity1;
			comm.Parameters.Add("@SuitabilityQuantity",SqlDbType.Decimal).Value = SuitabilityQuantity;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
			comm.ExecuteNonQuery();


//			잔량이 0보다 크면 진행 그렇지 않으면 완료처리한다.
//
//			string str_Remain = "Select TotalReceiveingOrderQuantity, RemainderQuantity  From RO_HT where ReceivingOrderHistoryIndex = @index";
//			decimal Remain = 0;
//			decimal total = 0;
//			SqlCommand comm_Remain = new SqlCommand(str_Remain,conn);
//			comm_Remain.Transaction = tr;
//			comm_Remain.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
//			SqlDataReader dr = comm_Remain.ExecuteReader();
//			while(dr.Read())
//			{
//				Remain = decimal.Parse(dr["RemainderQuantity"].ToString());
//				total = decimal.Parse(dr["TotalReceiveingOrderQuantity"].ToString());
//			}
//			dr.Close();
//
//			string str_Up = "";
//			if(Remain == total && total != 0)
//				str_Up = "Update RO_HT Set ProgressCondition = '대기' where ReceivingOrderHistoryIndex = @index";
//			else
			string str_Up = "Update RO_HT Set ProgressCondition = '진행' where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm_total = new SqlCommand(str_Up,conn);
			comm_total.Transaction = tr;
			comm_total.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Text);
			comm_total.ExecuteNonQuery();
		}

		/// <summary>
		/// 매출원장 삭제시 출고원장 수량및 변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void OS_HTUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			//출고원장의 출고수량을 가져온다
			decimal quantity1 = 0;
			string str_OS = "Select OutStorehouseQuantity From OS_HT Where OutStorehouseHistoryIndex = @index";
			SqlCommand comm_OS = new SqlCommand(str_OS,conn);
			comm_OS.Transaction = tr;
			comm_OS.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Text);
			SqlDataReader dr_OS = comm_OS.ExecuteReader();
			while(dr_OS.Read())
			{
				quantity1 = decimal.Parse(dr_OS["OutStorehouseQuantity"].ToString());
			}
			dr_OS.Close();


			// 출고원장에는 합격수량, 부적합수량,부적합금액, 미검수량, 부적합원인, 부적합원인코드, 부적합현상, 부적합현상코드, 부적합세부사유, 검사판정코드,검사판정, 거래명세서번호, 진행상태의 필드를 변경한다.
			// 합격수량및 부적합수량,부적합금액은 0, 미검수량은 출고원장의 출고수량,나머지는 null을 넣어둔다
			string str = "Update OS_HT Set UnInspectionQuantity=@uninspection, SuitabilityQuantity='0', UnSuitabilityQuantity='0', " +
				" UnSuitabilityCauseCode=@causecode, UnSuitabilityCauseMeaning=@cause, UnSuitabilityStatusCode=@statuscode, " +
				" UnSuitabilityStatusMeaning=@status, UnSuitabilityDetailMeaning = @detail, UnSuitabilityCost = '0', " +
				" InspectionDecisionCode=@decisioncode, InspectionDecisionMeaning = @decision, ItemizeAccountNum=@account, ProgressCondition = '대기' "+
				" Where OutStorehouseHistoryIndex = @index";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@uninspection",SqlDbType.Decimal).Value = quantity1;
			comm.Parameters.Add("@causecode",SqlDbType.VarChar).Value = Convert.DBNull;
			comm.Parameters.Add("@cause",SqlDbType.VarChar).Value = Convert.DBNull;
			comm.Parameters.Add("@statuscode",SqlDbType.VarChar).Value = Convert.DBNull;
			comm.Parameters.Add("@status",SqlDbType.VarChar).Value = Convert.DBNull;
			comm.Parameters.Add("@detail",SqlDbType.VarChar).Value = Convert.DBNull;
			comm.Parameters.Add("@decisioncode",SqlDbType.VarChar).Value = Convert.DBNull;
			comm.Parameters.Add("@decision",SqlDbType.VarChar).Value = Convert.DBNull;
			comm.Parameters.Add("@account",SqlDbType.VarChar).Value = Convert.DBNull;
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Text);

			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 납품창고의 출고수량을 마이너스 증가시키고
		/// 출고금액을 마이너스 증가시킨다.
		/// </summary>
		/// <param name="rowcount"></param>
		private void DS_MTUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			Decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = Decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleDate").Text).Year; 
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleDate").Text).Month; 
			
			//납품창고의 출고수량을 마이너스 증가
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Text);
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(cost * decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Text));
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText= table.OutDeliveryTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();



			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM DS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 매입매출테이블의 매출액및 미수금액 마이너스증가
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void BSI_MinusUpdate(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleDate").Text).Year; 
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleDate").Text).Month; 
			

			Table table2 = new Table(year,mon);
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("TotalCost").Text);
			comm.Parameters.Add("@year",year);
			comm.CommandText = table2.CollectMoneyIncreaseTable();
			comm.ExecuteNonQuery();

		}

		private void BOS_HTDelete(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			string str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason, [Date],UpdatePerson, UpdatePersonID,HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,'99','14009999','최종품',@CompanyName,@BusinessRegistrationNum,@Quantity,@UnitCost,'삭제', @Date,@Person, @PersonID,'매출등록',@HistoryIndex)";
				
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			
			comm.Parameters.Add("@itemnum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);	//품목번호
			comm.Parameters.Add("@itemdrawnum", m_Grid.Rows[rowcount].Cells.FromKey("ItemDrawNum").Text);							//도면번호
			comm.Parameters.Add("@itemname",m_Grid.Rows[rowcount].Cells.FromKey("ItemName").Text);									//품목명
			comm.Parameters.Add("@CompanyName",m_Grid.Rows[rowcount].Cells.FromKey("CompanyName").Text);								//업체명
			comm.Parameters.Add("@BusinessRegistrationNum",m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text);
//			comm.Parameters.Add("@ProcessSequenceNum",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("종료공정순서").Text));							//공정순서
//			comm.Parameters.Add("@ProcessCode",m_Grid.Rows[rowcount].Cells.FromKey("종료공정코드").Text);						//공정코드
//			comm.Parameters.Add("@ProcessName",m_Grid.Rows[rowcount].Cells.FromKey("종료공정명").Text);							//공정명
			comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@UnitCost",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()));//합격수량
			comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());
			comm.Parameters.Add("@Person",m_UserName.ToString());
			comm.Parameters.Add("@PersonID", m_User.ToString());	
			comm.Parameters.Add("@HistoryIndex",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleHistoryIndex").Text));												//원장번호

			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

		}



		/// <summary>
		/// 기타입출고현황 삭제
		/// </summary>
		/// <param name="rowcount"></param>
		private void StoreTableDelete(SqlConnection conn, SqlTransaction tr,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Month;

			Table table = new Table(year,mon);
			string str = "";
			switch(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Text)
			{
				case "원자재창고" :
				{
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					Decimal cost = 0;
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
					SqlCommand comm_cost = new SqlCommand(str_cost,conn);
					comm_cost.Transaction = tr;
					comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					SqlDataReader dr = comm_cost.ExecuteReader();
					if(dr.Read())
						cost = Decimal.Parse(dr["StandardUnitCost"].ToString());
					dr.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////
					
					
					if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "입고")
					{
						str = table.InRowTable();
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text);
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text) * cost;					
					}
					else
					{
						str = table.OutRowTable();
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text);
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text) * cost;
					}
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@year",year);
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//마이너스 재고 처리
					if(MinusStore() == false)
					{
						string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
						comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@year",DateTime.Now.Year);
						comm.CommandText = strSQL;
						SqlDataReader reader = comm.ExecuteReader();
						
						string StockQuantity = "False";
						string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						if(reader.Read())
						{
							if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
							{
								StockQuantity = "True";
							}
						}
						reader.Close();

						if(StockQuantity == "True")
						{
							throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
						}
					}



					break;
				}
				case "생산창고" :
				{
					//창고에서 떨어줄 금액
					decimal Cost = 0;
					str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.CommandText = str;
					SqlDataReader dr = comm.ExecuteReader();
					while(dr.Read())
					{
						Cost = decimal.Parse(dr["StandardUnitCost"].ToString());
					}
					dr.Close();
					comm.Parameters.Clear();
					//진척율
					decimal Rate = 0;
					str = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @itemnum and ProcessSequenceNum = @sequence";
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@sequence",SqlDbType.TinyInt).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ProcessSequenceNum").Text);
					comm.CommandText = str;
					SqlDataReader dr_rate = comm.ExecuteReader();
					if(dr_rate.Read())
					{
						if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
							Rate = 100;
						else
							Rate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
					}
					dr_rate.Close();
					comm.Parameters.Clear();


					
					if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "입고")
					{
						str = table.InProductionTable();
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text);
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text) * Cost *Rate/100;
					}
					else
					{
						str = table.OutProductionTable();
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text);
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text) * Cost * Rate/100;
					}
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
					comm.Parameters.Add("@year",year);					
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();


					//마이너스 재고허용 여부
					if(MinusStore() == false)
					{
						string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and [Year] = @year";
						comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
						comm.Parameters.Add("@year",DateTime.Now.Year);
						comm.CommandText = strSQL;
						SqlDataReader reader = comm.ExecuteReader();
						
						string StockQuantity = "False";
						string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						if(reader.Read())
						{
							if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
							{
								StockQuantity = "True";
							}
						}
						reader.Close();

						if(StockQuantity == "True")
						{
							throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
						}
					}
					comm.Parameters.Clear();
					break;
				}
				case "외주창고" :
				{
					Decimal m_cost = 0;
					Decimal m_progressrate = 100;
					// 품목의 금액
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
					SqlCommand comm_cost = new SqlCommand(str_cost,conn);
					comm_cost.Transaction = tr;
					comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					SqlDataReader dr_cost = comm_cost.ExecuteReader();
			
					if(dr_cost.Read())
					{
						if(dr_cost["StandardUnitCost"].ToString() == null || dr_cost["StandardUnitCost"].ToString().Trim() =="")
							m_cost = 0;
						else
							m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
					}
					dr_cost.Close();

					// 품목의 진척율
					string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num";
					SqlCommand comm_rate = new SqlCommand(str_rate,conn);
					comm_rate.Transaction = tr;
					comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					SqlDataReader dr_rate = comm_rate.ExecuteReader();
			
					if(dr_rate.Read())
					{

						if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
							m_progressrate = 100;
						else
							m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
					}
					dr_rate.Close();


					if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "입고")
					{
						str = table.InTable();
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text);
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text) * m_cost*m_progressrate/100;
					}
					else
					{
						str = table.OutTable();
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text);
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text) * m_cost*m_progressrate/100;
					}
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
					comm.Parameters.Add("@year",year);
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//마이너스 재고허용 여부
					if(MinusStore() == false)
					{
						string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @comnum and [Year] = @year";
						comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
						comm.Parameters.Add("@year",DateTime.Now.Year);
						comm.CommandText = strSQL;
						SqlDataReader reader = comm.ExecuteReader();
						
						string StockQuantity = "False";
						string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						if(reader.Read())
						{
							if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
							{
								StockQuantity = "True";
							}
						}
						reader.Close();

						if(StockQuantity == "True")
						{
							throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
						}
					}
				}
					break;
				case "납품창고" :
				{
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					Decimal cost = 0;
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
					SqlCommand comm_cost = new SqlCommand(str_cost,conn);
					comm_cost.Transaction = tr;
					comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					SqlDataReader dr = comm_cost.ExecuteReader();
					if(dr.Read())
						cost = Decimal.Parse(dr["StandardUnitCost"].ToString());
					dr.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////
					
					if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "입고")
					{
						str = table.InDeliveryTable();
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text);
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text) * cost;
					}
					else
					{
						str = table.OutDeliveryTable();
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text);
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text) * cost;
					}
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@year",year);
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//마이너스 재고허용 여부
					if(MinusStore() == false)
					{
						string strSQL = "SELECT StockQuantity12 FROM DS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and [Year] = @year";
						comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@year",DateTime.Now.Year);
						comm.CommandText = strSQL;
						SqlDataReader reader = comm.ExecuteReader();
						
						string StockQuantity = "False";
						string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						if(reader.Read())
						{
							if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
							{
								StockQuantity = "True";
							}
						}
						reader.Close();

						if(StockQuantity == "True")
						{
							throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
						}
					}
					comm.Parameters.Clear();
					
				}
					break;
				default://영업창고
				{
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					Decimal cost = 0;
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
					SqlCommand comm_cost = new SqlCommand(str_cost,conn);
					comm_cost.Transaction = tr;
					comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					SqlDataReader dr = comm_cost.ExecuteReader();
					if(dr.Read())
						cost = Decimal.Parse(dr["StandardUnitCost"].ToString());
					dr.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////
					
					
					if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "입고")
					{
						str = table.InBusinessTable();
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text);
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text) * cost;
					}
					else
					{
						str = table.OutBusinessTable();

						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text);
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Text) * cost;
					}
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@storenum",SqlDbType.TinyInt).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Text);
					comm.Parameters.Add("@year",year);
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//마이너스 재고허용 여부
					if(MinusStore() == false)
					{
						string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
						comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Text);
						comm.Parameters.Add("@year",DateTime.Now.Year);
						comm.CommandText = strSQL;
						SqlDataReader reader = comm.ExecuteReader();
						
						string StockQuantity = "False";
						string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						if(reader.Read())
						{
							if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
							{
								StockQuantity = "True";
							}
						}
						reader.Close();

						if(StockQuantity == "True")
						{
							throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
						}
					}
					comm.Parameters.Clear();


				}
					break;
			}
			comm.Parameters.Clear();
		}


		private void SH_HT_OIOS_HTDelete(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str="Delete SH_HT Where HistoryIndex = @idx and HistoryDivision = '기타입출고'";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("OtherInOutStorehouseHistoryIndex").Text;
			comm.ExecuteNonQuery();
		}


		private void OutRMS_MTTable(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingDate").Text).Month;
			Table table = new Table(year,mon);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			decimal m_cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.CommandText = str_cost;
			SqlDataReader dr_cost = comm.ExecuteReader();
			if(dr_cost.Read())
				m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString());
			dr_cost.Close();
			comm.Parameters.Clear();

			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingQuanity").Text);
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(m_cost*decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingQuanity").Text));
			comm.Parameters.Add("@year",year);

			comm.CommandText= table.OutRowTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		private void OutPS_MTTable(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingDate").Text).Month;
			Table table = new Table(year,mon);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			decimal m_cost = 0;
			// 창고금액은 품목정보의 기준단가와 공정순서의 진척율을 곱해서 구한다.
			string str_cost = "SELECT ProgressRate, StandardUnitCost FROM II_MT INNER JOIN PSI_MT ON II_MT.ItemNum = PSI_MT.ItemNum Where II_MT.RecodingState = 1 and II_MT.ItemNum = @num and PSI_MT.ProcessSequenceNum = @sequence";
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@sequence",SqlDbType.Int).Value  = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ProcessSequenceNum").Text);
			comm.CommandText = str_cost;
			SqlDataReader dr_cost = comm.ExecuteReader();
			if(dr_cost.Read())
				m_cost = Decimal.Parse(dr_cost["StandardUnitCost"].ToString()) * decimal.Parse(dr_cost["ProgressRate"].ToString())/100;
			dr_cost.Close();
			comm.Parameters.Clear();

			//품목의 진척율
			decimal m_progressrate = 100;
			string str_rate = "Select ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
			SqlCommand comm_rate = new SqlCommand(str_rate,conn);
			comm_rate.Transaction = tr;
			comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
			SqlDataReader dr_rate = comm_rate.ExecuteReader();

			if(dr_rate.Read())
			{
				if(dr_rate["ProgressRate"].ToString() == null || dr_rate["ProgressRate"].ToString().Trim() =="")
					m_progressrate = 0;
				else
					m_progressrate = Decimal.Parse(dr_rate["ProgressRate"].ToString());
			}
			dr_rate.Close();
			comm_rate.Parameters.Clear();

			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingQuanity").Text);
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(m_cost*decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingQuanity").Text)*(m_progressrate/100));
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value  = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
			comm.Parameters.Add("@year",year);


			comm.CommandText= table.OutProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		private void OutBS_MTTable(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingDate").Text).Month;
			Table table = new Table(year,mon);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
				
			
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingQuanity").Text));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThrowingQuanity").Text) * cost);
			comm.Parameters.Add("@storenum", SqlDbType.TinyInt).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("StoreNum").Text);
			comm.Parameters.Add("@year",year);


			comm.CommandText= table.OutBusinessTable();
			comm.ExecuteNonQuery();
		}



		/// <summary>
		/// 이력원장 삭제
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void SH_HT_AS_HTDelete(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str="Delete SH_HT Where HistoryIndex = @idx and HistoryDivision = '보용품입고'";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = m_Grid.Rows[rowcount].Cells.FromKey("AddItemInStoreIndex").Text;
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 창고수량 변경
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void AddItemTable(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Text).Month;
			Table table = new Table(year,mon);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			if(Division(m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text) == 1)
			{
				///////////////////////////////////////////
				// 창고의 금액을 위해 품목정보테이블에서 //
				// 기준단가를 가져와 계산한다.   -시작-  //
				///////////////////////////////////////////
				decimal cost = 0;
				string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
				SqlCommand comm_cost = new SqlCommand(str_cost,conn);
				comm_cost.Transaction = tr;
				comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				SqlDataReader dr = comm_cost.ExecuteReader();
				if(dr.Read())
					cost = decimal.Parse(dr["StandardUnitCost"].ToString());
				dr.Close();
				///////////////////////////////////////////
				// 창고의 금액을 위해 품목정보테이블에서 //
				// 기준단가를 가져와 계산한다.   -끝-    //
				///////////////////////////////////////////
				
				//일단 원자재창고에서 출고수량을 마이너스 증가시킨다(원래수량)
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()));
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()) * cost);
				comm.Parameters.Add("@year",year);
			
				comm.CommandText= table.OutRowTable();
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				//보용품 창고에 입고수량을 마이너스 증가시킨다.
				comm.CommandText= table.InAddItemTable();
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()));
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				//comm.Parameters.Add("@code", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()) * cost);
				comm.Parameters.Add("@year",year);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else
			{
				decimal cost = 0; //금액
				decimal m_progressrate = 0;//진척율

				if(!WorkPlan3())
				{
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
					SqlCommand comm_cost = new SqlCommand(str_cost,conn);
					comm_cost.Transaction = tr;
					comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					SqlDataReader dr_cost = comm_cost.ExecuteReader();
					
					if(dr_cost.Read())
						cost = decimal.Parse(dr_cost["StandardUnitCost"].ToString());
					dr_cost.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////

					 
					//////////////////////////////////////////
					// 품목의 진척율, 공정순서을 위해		//
					// 품목정보테이블에서					//
					// 기준단가를 가져와 계산한다.   -시작- //
					//////////////////////////////////////////
					string str_rate = "Select ProgressRate,ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
					SqlCommand comm_rate = new SqlCommand(str_rate,conn);
					comm_rate.Transaction = tr;		
					comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
					SqlDataReader dr_rate = comm_rate.ExecuteReader();
					if(dr_rate.Read())
					{
						m_progressrate = decimal.Parse(dr_rate["ProgressRate"].ToString());
					}
					dr_rate.Close();
					//////////////////////////////////////////
					// 품목의 진척율, 공정순서을 위해		//
					// 품목정보테이블에서					//
					// 기준단가를 가져와 계산한다.   -끝-   //
					//////////////////////////////////////////

					
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
					comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()));
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()))*(decimal.Parse(cost.ToString())) * decimal.Parse(m_progressrate.ToString())/100;
					comm.Parameters.Add("@year",year);
					
					comm.CommandText= table.OutProductionTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}
				else
				{
					comm.CommandText = "dbo.WorkRowBomTree";
					comm.CommandType = CommandType.StoredProcedure;

					comm.Parameters.Add("@ItemNum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);
					comm.Parameters.Add("@Quantity", decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Text));
					comm.Parameters.Add("@Tree", SqlDbType.Bit).Value = 0;

				
					SqlDataAdapter adapter = new SqlDataAdapter(comm);
					DataSet ds = new DataSet();
				
					adapter.Fill(ds);
					foreach(DataRow dr in ds.Tables[0].Rows)
					{
					
						SubRMS_MTUpdate(conn,tr,dr["ItemNum"].ToString(),decimal.Parse(dr["Quantity"].ToString()),year,mon);		//원자재창고 마이너스출고
					}
					comm.Parameters.Clear();
	

					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
					SqlCommand comm_cost = new SqlCommand(str_cost,conn);
					comm_cost.Transaction = tr;
					comm_cost.Parameters.Add("@itemnum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text);
					SqlDataReader dr1 = comm_cost.ExecuteReader();
					if(dr1.Read())
						cost = decimal.Parse(dr1["StandardUnitCost"].ToString());
					dr1.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////
				}

				//보용품 창고에 입고수량을 마이너스 증가시킨다.
				comm.CommandText= table.InAddItemTable();
				comm.CommandType = CommandType.Text;
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()));
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				//comm.Parameters.Add("@code", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()) * cost);
				comm.Parameters.Add("@year",year);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}

			
			

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM AS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
				comm.Parameters.Clear();

				string StockQuantity = "False";
				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
				}
			}
		}


		/// <summary>
		/// 보용품 자산분류가 원자재가 아닌경우 하위 원자재를 창고에 마이너스 출고시키는 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="ItemNum"></param>
		/// <param name="Quantity"></param>
		/// <param name="year"></param>
		/// <param name="mon"></param>
		private void SubRMS_MTUpdate(SqlConnection con, SqlTransaction trans, string ItemNum, decimal Quantity,int year, int mon)
		{

			Table table = new Table(year,mon);

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,con);
			comm_cost.Transaction =trans;
			comm_cost.Parameters.Add("@itemnum",ItemNum);
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			SqlCommand comm = new SqlCommand();
			comm.Transaction = trans;	
			comm.Connection = con;

			comm.Parameters.Add("@quantity", -Quantity);
			comm.Parameters.Add("@num", ItemNum);
			comm.Parameters.Add("@cost", -(Quantity * cost));
			comm.Parameters.Add("@year",year);		
			
			comm.CommandText= table.OutRowTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();			
		}
		
		

		/// <summary>
		/// 검사품인지 무검사인지 판단하는 함수 
		/// 0이면 무검사,1이면 검사
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private bool Check(int rowcount)
		{
			string aa = "0";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select CheckDistinction From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["CheckDistinction"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "True")
				return true	;
			else
				return false;
		}


		/// <summary>
		/// 품목의 자산분류가 무었인지 판단하는 함수
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private int Division(int rowcount)
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "원자재")
				return 1;
			else if(aa.ToString() =="반제품")
				return 2;
			else if(aa.ToString() =="제품")
				return 3;
			else if(aa.ToString() =="상품")
				return 4;
			else //if(aa.ToString() =="팬텀")
				return 5;
		}


		/// <summary>
		/// 작업일보등록시 창고에 입출력을 위해 
		/// 품목의 종류를 구분하는 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private int Division(string ItemNum)
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemNum.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "원자재")
				return 1;
			else if(aa.ToString() =="반제품")
				return 2;
			else if(aa.ToString() =="제품")
				return 3;
			else if(aa.ToString() =="상품")
				return 4;
			else //if(aa.ToString() =="팬텀")
				return 5;
		}



		/// <summary>
		/// 관리여부 판단 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		private bool IsManaged(SqlConnection con, SqlTransaction trans, string item)
		{
			SqlCommand comm = new SqlCommand();
			comm.Transaction = trans;
			comm.Connection = con;

			string str = "Select Unit4 From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
			string manage = "아니오";
							
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				manage = dr["Unit4"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();

			if(manage == "예")
				return true;
			else
				return false;

		}

		/// <summary>
		/// 마이너스 재고 허용여부
		/// </summary>
		/// <returns></returns>
		private bool MinusStore()
		{
			bool aa = true;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();
			string str = "Select MinusRowMaterialPermissionUsed From SCS_T";
			SqlCommand comm =  new SqlCommand(str,conn);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = bool.Parse(dr["MinusRowMaterialPermissionUsed"].ToString());
			}
			dr.Close();
			conn.Close();

			return aa;
		}

		

	}
}
