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

namespace KIT_ERP
{
	/// <summary>
	/// Registration에 대한 요약 설명입니다.
	/// </summary>
	public class Registration
	{
		private string m_PageName;		//요청페이지 담는 변수
		private string m_Table;			//넘어온 테이블 담는 변수
		private string m_UserID;		//사용자 아이디 저장
		private string m_UserName;		//사용자 이름 저장
		private string m_Index;			//레코드 인덱스값 저장
		private string m_Distinction;	//업무구분(모듈구분) 월마감에 사용되어진다
		private ArrayList m_InputList = new ArrayList();
		//private ArrayList m_InputList;

		/// <summary>
		/// 등록버튼 클릭시 생성자 함수, 수정버튼 클릭시 생성자함수
		/// </summary>
		/// <param name="RequestPage">요청페이지</param>
		/// <param name="UserID">사용자</param>
		/// <param name="RecordIdx">레코드인덱스(수정시에 사용)</param>
		/// <param name="aaa">넘겨주는 값들을 저장하는 ArrayList</param>
        public Registration(string RequestPage, string UserID, string RecordIdx, ArrayList aaa)
		{
			m_PageName = RequestPage.ToString();
			m_UserID = UserID.ToString();
			m_Index = RecordIdx.ToString();

			for(int i = 0; i < aaa.Count; i++)
			{
				if(aaa[i] ==null)
					m_InputList.Add("");
				else
					m_InputList.Add(aaa[i]);
			}

			
			switch(m_PageName)
			{
				case "ReceiveingRegistration":		//수주등록페이지
					m_Table = "RO_HT";
					m_Distinction = "영업";
					break;
				case "CollectMoneyRegistration":	//수금등록페이지
					m_Table = "CM_HT";
					m_Distinction = "영업";
					break;
				case "PaymentPlanResultRegistration":	//지급등록페이지
					m_Table = "P_HT";
					m_Distinction = "외주";
					break;
				case "GoodsBuyingRequestAdd":			//상품구매의뢰추가페이지
					m_Table = "BR_HT";
					m_Distinction = "영업";
					break;
				case "RowMaterialRequriementAdd":		//원자재구매의뢰추가페이지
					m_Table = "BR_HT";
					m_Distinction = "생산";
					break;
				case "BuyingOrderAdd":					//구매발주추가페이지
					m_Table = "BO_HT";
					m_Distinction = "구매";
					break;
				case "ProductionRequestAdd":		//생산의뢰추가페이지
					m_Table = "PR_HT";
					m_Distinction = "영업";
					break;
				case "ProductionPlanAdd":		//생산계획추가페이지
					m_Table = "PP_HT";
					m_Distinction = "생산";
					break;
				case "ClaimRegistration":		//크레임등록페이지
					m_Table = "CL_HT";
					m_Distinction = "영업";
					break;
				case "Claim":					//지급 크레임등록페이지
					m_Table = "PCL_HT";
					m_Distinction = "구매";
					break;
				case "SubBuyingOrder":
					m_Table = "SBO_HT";
					m_Distinction = "구매";
					break;
				case "RowItemExhaust":			//원자재 소진 페이지
					m_Table = "E_HT";
					m_Distinction = "생산";
					break;
				case "EtcClaim"	:				//기타공제등록페이지
					m_Table = "ECL_HT";
					m_Distinction = "구매";
					break;
				default:					
					break;
			}


			// 사용자 이름을 입력
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
			conn.Close();


		}


		/// <summary>
		/// 수금등록,지급등록 삭제버튼 클릭시 생성자
		/// </summary>
		/// <param name="RequestPage">요청페이지</param>
		/// <param name="RecordIdx">삭제할 레코드 인덱스</param>
		/// <param name="aaa">배열리스트</param>
		public Registration(string RequestPage, string RecordIdx,ArrayList aaa)
		{
			m_PageName = RequestPage.ToString();
			m_Index = RecordIdx.ToString();
			for(int i = 0; i < aaa.Count; i++)
			{
				if(aaa[i] ==null)
					m_InputList.Add("");
				else
					m_InputList.Add(aaa[i]);
			}
			
			switch(m_PageName)
			{
				case "CollectMoneyRegistration":	//수금등록페이지
					m_Table = "CM_HT";
					m_Distinction = "영업";
					break;
				case "PaymentPlanResultRegistration":	//지급등록페이지
					m_Table = "P_HT";
					m_Distinction = "외주";
					break;
				case "ClaimRegistration":		//크레임등록페이지
					m_Table = "CL_HT";
					m_Distinction = "영업";
					break;
				case "Claim":					//지급 크레임등록페이지
					m_Table = "PCL_HT";
					m_Distinction = "구매";
					break;
				case "SubBuyingOrder":
					m_Table = "SBO_HT";
					m_Distinction = "구매";
					break;
				case "RowItemExhaust":
					m_Table = "E_HT";
					m_Distinction = "생산";
					break;
				case "EtcClaim"	:				//기타공제등록페이지
					m_Table = "ECL_HT";
					m_Distinction = "구매";
					break;
				default:
					break;
			}


		}


		/// <summary>
		/// 삭제버튼 클릭시 생성자
		/// </summary>
		/// <param name="RequestPage">요청페이지</param>
		/// <param name="RecordIdx">삭제할 레코드 인덱스</param>
		public Registration(string RequestPage, string RecordIdx)
		{
			m_PageName = RequestPage.ToString();
			m_Index = RecordIdx.ToString();
			
			
			switch(m_PageName)
			{
				case "ReceiveingRegistration":		//수주등록페이지
					m_Table = "RO_HT";
					m_Distinction = "영업";
					break;
				case "GoodsBuyingRequestAdd":			//상품구매의뢰추가페이지
					m_Table = "BR_HT";
					m_Distinction = "영업";
					break;
				case "RowMaterialRequriementAdd":		//원자재구매의뢰추가페이지
					m_Table = "BR_HT";
					m_Distinction = "생산";
					break;
				case "BuyingOrderAdd":					//구매발주추가페이지
					m_Table = "BO_HT";
					m_Distinction = "구매";
					break;
				case "ProductionRequestAdd":		//생산의뢰추가페이지
					m_Table = "PR_HT";
					m_Distinction = "영업";
					break;
				case "ProductionPlanAdd":		//생산계획추가페이지
					m_Table = "PP_HT";
					m_Distinction = "생산";
					break;
				case "SubBuyingOrder":
					m_Table = "SBO_HT";
					m_Distinction = "구매";
					break;
				case "RowItemExhaust":			//원자재 소진 페이지
					m_Table = "E_HT";
					m_Distinction = "생산";
					break;

				default:
					break;
			}


		}


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
					return false;
			}
			else
			{
				return false;
			}	
		}


		public void ReceiveRegistration()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			DataSet ds = new DataSet();
			try
			{
				SqlCommand comm = new SqlCommand();
				comm.Connection = conn;
				comm.Transaction = tr;
				
				
				

				if(MonthClosing() == false)
				{
					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language='JavaScript'>");
					hc.Response.Write("alert('마감이 되어 등록이 불가능합니다!');");
					hc.Response.Write("</script>");	
					
				}
				else
				{

					if(Validated(conn,tr))
					{
						if(ReceiveingRegistrationValidate() == true)
						{
							Regist(conn,tr);
						}
					}
				}
				
				tr.Commit();
			}
			catch(Exception ee)
			{
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
			
		}

		/// <summary>
		/// 등록 함수
		/// </summary>
		public DataSet MainTableRegistration()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			DataSet ds = new DataSet();
			try
			{
				SqlCommand comm = new SqlCommand();
				comm.Connection = conn;
				comm.Transaction = tr;
				
				
				string str = "";

				if(MonthClosing() == false)
				{
					if(m_PageName.ToString() == "CollectMoneyRegistration" || m_PageName.ToString() == "PaymentPlanResultRegistration" )
						str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today";
					else if(m_PageName =="ReceiveingRegistration")
					{
						str = @"SELECT isnull(PUC.SmallClassificationName,'') as ItemState,   II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName,
							II_MT.PropertyClassification, PUC_MT.SmallClassificationName as Unit, II_MT.Standard , 
							II_MT.Standard, RO_HT.CompanyName, RO_HT.BusinessRegistrationNum, CompanyPersonInCharge, CI_MT.TelephoneNum, 
							RO_HT.ReceivingOrderDate, 
							RO_HT.ApplyUnitCost, RO_HT.DeliveryRequestQuantity1, 
							CASE RO_HT.ProductionRequestDivision WHEN '1' THEN '예' ELSE '아니오' END AS ProductionRequestDivision,
							RO_HT.DeliveryRequestDate1, RO_HT.DeliveryRequestQuantity2, 
							RO_HT.DeliveryRequestDate2, RO_HT.DeliveryRequestQuantity3,
							RO_HT.DeliveryRequestDate3, RO_HT.DeliveryRequestQuantity4, 
							RO_HT.DeliveryRequestDate4, RO_HT.DeliveryRequestQuantity5,
							RO_HT.DeliveryRequestDate5, RO_HT.TotalReceiveingOrderQuantity,
							RO_HT.TotalCost, RO_HT.OutStorehouseQuantity, RO_HT.SuitabilityQuantity,
							RO_HT.UnInspectionQuantity, RO_HT.RemainderQuantity, RO_HT.OrderNum,
							RO_HT.DeliveryPlace, RO_HT.VolumNum, RO_HT.ProgressCondition,
							RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID,
							RO_HT.RegistrationDate, RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, 
							RO_HT.UpdatingDate, RO_HT.ReceivingOrderHistoryIndex 
							FROM RO_HT
							INNER JOIN II_MT ON RO_HT.ItemNum = II_MT.ItemNum
							Inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
							INNER JOIN PUC_MT ON II_MT.Unit = PUC_MT.SmallClassificationCode
							left outer join PUC_MT PUC on II_MT.ItemState = PUC.SmallClassificationCode
							
							Where CI_MT.RecodingState = 1 and II_MT.RecodingState = '1' and RO_HT.RegistrationDate = @today and RO_HT.ProgressCondition = '대기'";
					}
					else
						str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today";

					comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					
				}
				else
				{

					if(Validated(conn,tr))
					{
					
						switch(m_PageName)
						{
							case "ReceiveingRegistration":		//수주등록 유효성 검사
							{
								if(ReceiveingRegistrationValidate() == true)
								{
									Regist(conn,tr);
								}
						
								str = @"SELECT isnull(PUC.SmallClassificationName,'') as ItemState,   II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName,
										II_MT.PropertyClassification, PUC_MT.SmallClassificationName as Unit, II_MT.Standard , 
										II_MT.Standard, RO_HT.CompanyName, RO_HT.BusinessRegistrationNum, CompanyPersonInCharge, CI_MT.TelephoneNum, 
										RO_HT.ReceivingOrderDate, 
										RO_HT.ApplyUnitCost, RO_HT.DeliveryRequestQuantity1, 
										CASE RO_HT.ProductionRequestDivision WHEN '1' THEN '예' ELSE '아니오' END AS ProductionRequestDivision,
										RO_HT.DeliveryRequestDate1, RO_HT.DeliveryRequestQuantity2, 
										RO_HT.DeliveryRequestDate2, RO_HT.DeliveryRequestQuantity3,
										RO_HT.DeliveryRequestDate3, RO_HT.DeliveryRequestQuantity4, 
										RO_HT.DeliveryRequestDate4, RO_HT.DeliveryRequestQuantity5,
										RO_HT.DeliveryRequestDate5, RO_HT.TotalReceiveingOrderQuantity,
										RO_HT.TotalCost, RO_HT.OutStorehouseQuantity, RO_HT.SuitabilityQuantity,
										RO_HT.UnInspectionQuantity, RO_HT.RemainderQuantity, RO_HT.OrderNum,
										RO_HT.DeliveryPlace, RO_HT.VolumNum, RO_HT.ProgressCondition,
										RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID,
										RO_HT.RegistrationDate, RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, 
										RO_HT.UpdatingDate, RO_HT.ReceivingOrderHistoryIndex 
										FROM RO_HT
										INNER JOIN II_MT ON RO_HT.ItemNum = II_MT.ItemNum
										Inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
										INNER JOIN PUC_MT ON II_MT.Unit = PUC_MT.SmallClassificationCode
										left outer join PUC_MT PUC on II_MT.ItemState = PUC.SmallClassificationCode
										
										Where CI_MT.RecodingState = 1 and II_MT.RecodingState = '1' and RO_HT.RegistrationDate = @today and RO_HT.ProgressCondition = '대기'";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "CollectMoneyRegistration":	//수금등록 유효성 검사
							{
								if(CollectMoneyRegistrationValidate()==true)
								{
									Regist(conn,tr);
									BSI_Update(conn,tr);
								}
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today  ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "PaymentPlanResultRegistration":	//지급등록 유효성 검사
							{
								if(PaymentPlanResultRegistrationValidate()==true)
								{
									Regist(conn,tr);
									BSI_Update(conn,tr);
								}
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "GoodsBuyingRequestAdd":			//상품구매의뢰 유효성 검사
							{
								if(GoodsBuyingRequestAddValidate()==true)
								{
									Regist(conn,tr);
								}

								str = @"Select SmallClassificationName as ItemState, BR_HT.* 
								From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and II_MT.PropertyClassification = '상품' and ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null ";
								//str = "Select * From BR_HT where PropertyClassification = '상품' and ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "RowMaterialRequriementAdd":		//원자재구매의뢰 유효성 검사
							{
								if(RowMaterialRequriementAddValidate()==true)
								{
									Regist(conn,tr);
								}

								//str = "Select BR_HT.*, II_MT.Unit From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum where II_MT.RecodingState = 1 and PropertyClassification = '원자재' and ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
								str = "Select BR_HT.*, PUC_MT.SmallClassificationName as Unit From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode where PUC_MT.RecodingState =1 and II_MT.RecodingState = 1 and II_MT.PropertyClassification = '원자재' and BR_HT.ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "BuyingOrderAdd":					//구매발주추가 유효성 검사
							{
								if(BuyingOrderAddValidate()==true)
								{
									Regist(conn,tr);
								}

								str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "ProductionRequestAdd":		//생산의뢰추가 유효성 검사
							{
								if(ProductionRequestAddValidate()==true)
								{
									Regist(conn,tr);
								}

								str = @"Select SmallClassificationName as ItemState, PR_HT.* 
								From PR_HT inner join II_MT on PR_HT.ItemNum = II_MT.ItemNum 
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PR_HT.RegistrationDate = @today and volumnum is null ";
								//str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "ProductionPlanAdd":		//생산계획추가 유효성 검사
							{
								if(ProductionPlanAddValidate()==true)
								{
									Regist(conn,tr);
								}

								str = @"Select SmallClassificationName as ItemState, PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanHistorySourceCode,
								ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate, DeliveryDate,
								case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation,
								VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID,
								PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate,
								HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT
								inner join II_MT on PP_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PP_HT.RegistrationDate = @today and volumnum is null  ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "ClaimRegistration":		//클레임등록 유효성 검사
							{
								if(ClaimRegistrationValidate()==true)
								{
									Regist(conn,tr);
									BSI_Update(conn,tr);
								}

								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today  ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "Claim":		//지급 클레임등록 유효성 검사
							{
								if(ClaimValidate()==true)
								{
									Regist(conn,tr);
									BSI_Update(conn,tr);
								}

								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "SubBuyingOrder":
							{
								if(SubBuyingOrderValidate()==true)
								{
									Regist(conn,tr);
								}

								str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and  RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "RowItemExhaust":			//원자재 소진 페이지
							{
								if(RowItemExhaustValidate()==true)
								{
									Regist(conn,tr);	//등록
									Table table = new Table(DateTime.Parse(m_InputList[4].ToString()).Year,DateTime.Parse(m_InputList[4].ToString()).Month);
									comm.Parameters.Add("@num",m_InputList[0].ToString());
									comm.Parameters.Add("@quantity",decimal.Parse(m_InputList[3].ToString()));
									comm.Parameters.Add("@cost",TotalCost(decimal.Parse(m_InputList[3].ToString()),m_InputList[0].ToString()));
									comm.Parameters.Add("@year",DateTime.Parse(m_InputList[4].ToString()).Year);
									comm.CommandText = table.OutRowTable();//원자재 창고 소진수량 감소
									comm.ExecuteNonQuery();
									comm.Parameters.Clear();

									comm.Parameters.Add("@num",m_InputList[5].ToString());
									comm.Parameters.Add("@quantity",decimal.Parse(m_InputList[8].ToString()));
									comm.Parameters.Add("@cost",TotalCost(decimal.Parse(m_InputList[8].ToString()),m_InputList[5].ToString()));
									comm.Parameters.Add("@year",DateTime.Parse(m_InputList[4].ToString()).Year);
									comm.CommandText = table.InRowTable();//원자재 창고 생성수량 증가
									comm.ExecuteNonQuery();
									comm.Parameters.Clear();
									
									StoreHistoryRegister(conn,tr);
								}

								str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							E_HT.RegistrationDate = @today order by ExhaustHistoryIndex desc";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "EtcClaim":					//기타공제 등록
							{
								Regist(conn,tr);	//등록
								BSI_Update(conn,tr);
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							default :
								break;

						}
					}
				}
				comm.CommandText = str;
				SqlDataAdapter da = new SqlDataAdapter(comm);
				
				da.Fill(ds);
				tr.Commit();
			}
			catch(Exception ee)
			{
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
			return ds;
		}


		

		/// <summary>
		/// 수주등록 유효성 검사 함수
		/// </summary>
		/// <returns></returns>
		private bool ReceiveingRegistrationValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[2].ToString() == null ||m_InputList[2].ToString().Trim() =="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('거래처를 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[0].ToString()== null || m_InputList[0].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('품목을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[9].ToString().Trim() == "0")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('1차납품량은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[10].ToString().Trim() == " "||m_InputList[10].ToString() == null)
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('1차납품요구일은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}

			else
				return true;

		}

		/// <summary>
		/// 생산계획추가 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool ProductionPlanAddValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[0].ToString() == null ||m_InputList[0].ToString().Trim() =="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('품목을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[3].ToString()== null || m_InputList[3].ToString().Trim()== "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('생산계획원천을 선택해주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[5].ToString().Trim() == "0")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('수량은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[6].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('생산시작일은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}

			else
				return true;
		}
		/// <summary>
		/// 생산의뢰 추가 유효성검사
		/// </summary>
		/// <returns></returns>
		private bool ProductionRequestAddValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[0].ToString() == null ||m_InputList[0].ToString().Trim() =="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('품목을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[4].ToString()== null || m_InputList[4].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('생산의뢰원천을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[8].ToString().Trim() == "0")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('1차납품량은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[9].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('1차납품요구일은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			return true;
		}

		/// <summary>
		/// 구매발주추가 유효성검사
		/// </summary>
		/// <returns></returns>
		private bool BuyingOrderAddValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[0].ToString() == null ||m_InputList[0].ToString().Trim() =="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('품목을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[4].ToString()== null || m_InputList[4].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('거래처를 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[5].ToString().Trim() == "0" || m_InputList[5].ToString().Trim() == null )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('1차납품량은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[6].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('1차납품요구일은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			return true;
		}

		/// <summary>
		/// 원자재구매의뢰추가 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool RowMaterialRequriementAddValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[0].ToString() == null ||m_InputList[0].ToString().Trim() =="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('품목을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[4].ToString()== null || m_InputList[4].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('구매의뢰원천을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[6].ToString().Trim() == "0")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('1차납기량은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[7].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('1차납기요구일은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			return true;
		}

		// 부자재 발주 유효성 검사
		private bool SubBuyingOrderValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[0].ToString() == null ||m_InputList[0].ToString().Trim() =="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('품목을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[4].ToString()== null || m_InputList[4].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('거래처를 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[5].ToString().Trim() == "0")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('수량은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[11].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('납기일은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			return true;
		}

		

		// 원자재 소진 유효성 검사
		private bool RowItemExhaustValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[0].ToString() == null ||m_InputList[0].ToString().Trim() =="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('소진품목을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[5].ToString()== null || m_InputList[5].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('생성품목을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}			
			return true;
		}

		/// <summary>
		/// 상품구매의뢰추가 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool GoodsBuyingRequestAddValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[0].ToString() == null ||m_InputList[0].ToString().Trim() =="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('품목을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[4].ToString()== null || m_InputList[4].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('구매의뢰원천을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[6].ToString().Trim() == "0")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('1차납기량은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[7].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('1차납기요구일은 반드시 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			return true;
		}

		/// <summary>
		/// 지급등록 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool PaymentPlanResultRegistrationValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[1].ToString() == null ||m_InputList[1].ToString()=="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('거래처를 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[2].ToString()== null || m_InputList[2].ToString().Trim()== "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('지급일을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[3].ToString().Trim() == "0" || m_InputList[3].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('수금액을 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			
			return true;
		}

		/// <summary>
		/// 수금등록 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool CollectMoneyRegistrationValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[1].ToString() == null ||m_InputList[1].ToString().Trim()=="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('거래처를 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[2].ToString()== null || m_InputList[2].ToString().Trim()== "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('수금일을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[3].ToString().Trim() == "0" || m_InputList[3].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('수금액을 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			
			return true;
		}


		/// <summary>
		/// 클레임 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool ClaimRegistrationValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[0].ToString() == null ||m_InputList[0].ToString().Trim()=="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('품목을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[3].ToString()== null || m_InputList[3].ToString().Trim()== "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('거래처를 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[5].ToString().Trim() == null || m_InputList[5].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('접수일을 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[6].ToString()== "0" || m_InputList[6].ToString().Trim()== "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('클레임 수량을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[7].ToString().Trim() == "0" || m_InputList[7].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('클레임 금액을 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			
			return true;
		}


		/// <summary>
		/// 클레임 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool ClaimValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[0].ToString() == null ||m_InputList[0].ToString().Trim()=="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('품목을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[3].ToString()== null || m_InputList[3].ToString().Trim()== "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('거래처를 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[5].ToString().Trim() == null || m_InputList[5].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('접수일을 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[6].ToString()== "0" || m_InputList[6].ToString().Trim()== "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('클레임 수량을 선택해 주세요.');");
				hc.Response.Write("</script>");

				return false;
			}
			else if(m_InputList[7].ToString().Trim() == "0" || m_InputList[7].ToString().Trim() == "")
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('클레임 금액을 입력해야 합니다!');");
				hc.Response.Write("</script>");

				return false;
			}
			
			return true;
		}


		/// <summary>
		/// 실제등록함수
		/// </summary>
		private void Regist(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			
			string str = "";

			
			switch(m_PageName)	
			{

				case "ReceiveingRegistration":		//수주등록페이지
				{
					str = "Insert into RO_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,PropertyClassification,ProductionRequestDivision,ReceivingOrderDate,"+
						"ApplyUnitCost,DeliveryRequestQuantity1,DeliveryRequestDate1,DeliveryRequestQuantity2,DeliveryRequestDate2,DeliveryRequestQuantity3,"+
						"DeliveryRequestDate3,DeliveryRequestQuantity4,DeliveryRequestDate4,DeliveryRequestQuantity5,DeliveryRequestDate5,TotalReceiveingOrderQuantity,"+
						"TotalCost,OutStorehouseQuantity,SuitabilityQuantity,UnInspectionQuantity,RemainderQuantity,OrderNum,DeliveryPlace,ProgressCondition,"+
						"RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
						"@itemnum, @itemdrawnum,@itemname,@com,@comnum,@classification,@division,@receivingdate,@cost,@quantity1,@date1,@quantity2,@date2,@quantity3,@date3,"+
						"@quantity4,@date4,@quantity5,@date5,@total,@totalcost,@storequantity,@suitquantity,@unquantity,@remainquantity,@ordernum,@place,'대기',"+
						"@Person, @PersonID, @Date)";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@division",SqlDbType.Bit).Value = int.Parse(m_InputList[6].ToString());
					comm.Parameters.Add("@receivingdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[7].ToString()).ToShortDateString();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());

					comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[9].ToString());
					comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[10].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[11].ToString());
					if(m_InputList[12].ToString().Trim() == "")
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[12].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[13].ToString());
					if(m_InputList[14].ToString().Trim() == "")
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[14].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[15].ToString());
					if(m_InputList[16].ToString().Trim() == "")
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[16].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[17].ToString());
					if(m_InputList[18].ToString().Trim() == "")
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[18].ToString()).ToShortDateString();
					comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[19].ToString());
					comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[20].ToString());
					comm.Parameters.Add("@storequantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[21].ToString());
					comm.Parameters.Add("@suitquantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[22].ToString());
					comm.Parameters.Add("@unquantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[23].ToString());
					comm.Parameters.Add("@remainquantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[24].ToString());
					comm.Parameters.Add("@ordernum",SqlDbType.VarChar).Value = m_InputList[25].ToString();
					comm.Parameters.Add("@place",SqlDbType.VarChar).Value = m_InputList[26].ToString();
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();				
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.CommandText = str;
					comm.ExecuteNonQuery();

				}
					break;
				case "CollectMoneyRegistration":	//수금등록페이지
				{
					str = "Insert into CM_HT (CompanyName,BusinessRegistrationNum,CollectMoneyDate,ItemPaymentCost,SupplementaryValueTaxPaymentCost,DecisionMethodCode,DecisionMethod,"+
						"BillNum1,BillPaymentDate1,BankCode1,BankName1,BillNum2,BillPaymentDate2,BankCode2,BankName2,BillNum3,BillPaymentDate3,BankCode3,BankName3,"+
						"RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
						"@com,@comnum,@collectdate,@cost,@tax,@methodcode,@method,@billnum1,@bulldate1,@bankcode1,@bank1,@billnum2,@bulldate2,@bankcode2,@bank2,"+
						"@billnum3,@bulldate3,@bankcode3,@bank3,@Person, @PersonID, @Date)";

					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@collectdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[2].ToString()).ToShortDateString();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[3].ToString());
					comm.Parameters.Add("@tax",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[4].ToString());
					comm.Parameters.Add("@methodcode",SqlDbType.VarChar).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@method",SqlDbType.VarChar).Value = m_InputList[6].ToString();
					comm.Parameters.Add("@billnum1",SqlDbType.VarChar).Value = m_InputList[7].ToString();
					if(m_InputList[8].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate1",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[8].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode1",SqlDbType.VarChar).Value = m_InputList[9].ToString();
					comm.Parameters.Add("@bank1",SqlDbType.VarChar).Value = m_InputList[10].ToString();
					comm.Parameters.Add("@billnum2",SqlDbType.VarChar).Value = m_InputList[11].ToString();
					if(m_InputList[12].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[12].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode2",SqlDbType.VarChar).Value = m_InputList[13].ToString();
					comm.Parameters.Add("@bank2",SqlDbType.VarChar).Value = m_InputList[14].ToString();
					comm.Parameters.Add("@billnum3",SqlDbType.VarChar).Value = m_InputList[15].ToString();
					if(m_InputList[16].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[16].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode3",SqlDbType.VarChar).Value = m_InputList[17].ToString();
					comm.Parameters.Add("@bank3",SqlDbType.VarChar).Value = m_InputList[18].ToString();
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();							
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.CommandText = str;
					comm.ExecuteNonQuery();


						
				}
					break;
				case "PaymentPlanResultRegistration":	//지급등록페이지
				{
					str = "Insert into P_HT (CompanyName,BusinessRegistrationNum,PaymentDate,ItemPaymentCost,SupplementaryValueTaxPaymentCost,DecisionMethodCode,DecisionMethod,"+
						"ProofData,BillNum1,BillPaymentDate1,BankCode1,BankName1,BillNum2,BillPaymentDate2,BankCode2,BankName2,BillNum3,BillPaymentDate3,BankCode3,BankName3,"+
						"RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
						"@com,@comnum,@collectdate,@cost,@tax,@methodcode,@method,@data,@billnum1,@bulldate1,@bankcode1,@bank1,@billnum2,@bulldate2,@bankcode2,@bank2,"+
						"@billnum3,@bulldate3,@bankcode3,@bank3,@Person, @PersonID, @Date)";

					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@collectdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[2].ToString()).ToShortDateString();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[3].ToString());
					comm.Parameters.Add("@tax",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[4].ToString());
					comm.Parameters.Add("@methodcode",SqlDbType.VarChar).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@method",SqlDbType.VarChar).Value = m_InputList[6].ToString();
					comm.Parameters.Add("@data",SqlDbType.VarChar).Value = m_InputList[7].ToString();
					comm.Parameters.Add("@billnum1",SqlDbType.VarChar).Value = m_InputList[8].ToString();
					if(m_InputList[9].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate1",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode1",SqlDbType.VarChar).Value = m_InputList[10].ToString();
					comm.Parameters.Add("@bank1",SqlDbType.VarChar).Value = m_InputList[11].ToString();
					comm.Parameters.Add("@billnum2",SqlDbType.VarChar).Value = m_InputList[12].ToString();
					if(m_InputList[13].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[13].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode2",SqlDbType.VarChar).Value = m_InputList[14].ToString();
					comm.Parameters.Add("@bank2",SqlDbType.VarChar).Value = m_InputList[15].ToString();
					comm.Parameters.Add("@billnum3",SqlDbType.VarChar).Value = m_InputList[16].ToString();
					if(m_InputList[17].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[17].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode3",SqlDbType.VarChar).Value = m_InputList[18].ToString();
					comm.Parameters.Add("@bank3",SqlDbType.VarChar).Value = m_InputList[19].ToString();
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();						
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.CommandText = str;
					comm.ExecuteNonQuery();

				}
					break;
				case "GoodsBuyingRequestAdd":			//상품구매의뢰추가페이지
				{
					str="Insert into BR_HT (ItemNum,ItemDrawNum,ItemName,PropertyClassification,BuyingRequestSourceCode,BuyingRequestSource,"+
						"FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,"+
						"ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,"+
						"FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,ApplyUnitCost,TotalCost,ProgressCondition,"+
						"RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
						"@itemnum, @itemdrawnum,@itemname,@classification,@sourcecode,@source,@quantity1,@date1,@quantity2,@date2,"+
						"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,'대기', @Person, @PersonID, @Date)";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@source",SqlDbType.VarChar).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[6].ToString());
					comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[7].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
					if(m_InputList[9].ToString().Trim() == "")
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[10].ToString());
					if(m_InputList[11].ToString().Trim() == "")
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[11].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[12].ToString());
					if(m_InputList[13].ToString().Trim() == "")
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[13].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[14].ToString());
					if(m_InputList[15].ToString().Trim() == "")
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[15].ToString()).ToShortDateString();
					comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[16].ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[17].ToString());
					comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[18].ToString());
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();				
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.CommandText = str;
					comm.ExecuteNonQuery();

				}
					break;
				case "RowMaterialRequriementAdd":		//원자재구매의뢰추가페이지
				{
					
						str="Insert into BR_HT (ItemNum,ItemDrawNum,ItemName,PropertyClassification,BuyingRequestSourceCode,BuyingRequestSource,"+
							"FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,"+
							"ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,"+
							"FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,ApplyUnitCost,TotalCost,RequestPostCode,RequestPost,ProgressCondition,"+
							"RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
							"@itemnum, @itemdrawnum,@itemname,@classification,@sourcecode,@source,@quantity1,@date1,@quantity2,@date2,"+
							"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,@postcode,@post,'대기', @Person, @PersonID, @Date)";

						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
						comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
						comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
						comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_InputList[3].ToString();
						comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = m_InputList[4].ToString();
						comm.Parameters.Add("@source",SqlDbType.VarChar).Value = m_InputList[5].ToString();
						comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[6].ToString());
						comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[7].ToString()).ToShortDateString();
						if(m_InputList[8].ToString() == "")
							comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
						else
							comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
						if(m_InputList[9].ToString().Trim() == "")
							comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
						else
							comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
						if(m_InputList[10].ToString() == "")
							comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
						else
							comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[10].ToString());
						if(m_InputList[11].ToString().Trim() == "")
							comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
						else
							comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[11].ToString()).ToShortDateString();
						if(m_InputList[12].ToString() == "")
							comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
						else
							comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[12].ToString());
						if(m_InputList[13].ToString().Trim() == "")
							comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
						else
							comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[13].ToString()).ToShortDateString();
						if(m_InputList[14].ToString() == "")
							comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
						else
							comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[14].ToString());
						if(m_InputList[15].ToString().Trim() == "")
							comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
						else
							comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[15].ToString()).ToShortDateString();
						comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[16].ToString());
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[17].ToString());
						comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[18].ToString());
						
						comm.Parameters.Add("@postcode",SqlDbType.VarChar).Value = m_InputList[19].ToString();
						comm.Parameters.Add("@post",SqlDbType.VarChar).Value = m_InputList[20].ToString();

						comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
						comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();								
						comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
						comm.CommandText = str;
						comm.ExecuteNonQuery();
					

				}
					break;
				case "BuyingOrderAdd":					//구매발주추가페이지
				{
					str="Insert into BO_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,"+
						"SecondDeliveryDemandDate,ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,FifthDeliveryDemandQuantity,"+
						"FifthDeliveryDemandDate,OrderQuantity,ApplyUnitCost,TotalCost,OrderRate,RemainQuantity,StoreQuantity,ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
						"@itemnum, @itemdrawnum,@itemname,@com,@comnum,@quantity1,@date1,@quantity2,@date2,"+
						"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,@rate,@remain,@StoreQuantity,'대기', @Person, @PersonID, @Date)";
					
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[5].ToString());
					comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[6].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					if(m_InputList[8].ToString().Trim() == "")
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[8].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[9].ToString());
					if(m_InputList[10].ToString().Trim() == "")
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[10].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[11].ToString());
					if(m_InputList[12].ToString().Trim() == "")
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[12].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[13].ToString());
					if(m_InputList[14].ToString().Trim() == "")
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[14].ToString()).ToShortDateString();
					comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[15].ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[16].ToString());
					comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[17].ToString());
					comm.Parameters.Add("@StoreQuantity",SqlDbType.Decimal).Value = StoreQuantity(con,trans,m_InputList[0].ToString());
					comm.Parameters.Add("@rate",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[18].ToString());
					comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[19].ToString());
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();						
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.CommandText = str;
					comm.ExecuteNonQuery();

				}
					break;
				case "ProductionRequestAdd":		//생산의뢰추가페이지
				{
					
						str="Insert into PR_HT (ItemNum,ItemDrawNum,ItemName,ProductionRequestSourceCode,ProductionRequestSource,PropertyClassification,"+
							"CompanyName,BusinessRegistrationNum,ProductionRequestQuantity,RequestQuantity1,RequestDate1,RequestQuantity2,"+
							"RequestDate2,RequestQuantity3,RequestDate3,RequestQuantity4,RequestDate4,RequestQuantity5,RequestDate5,ApplyUnitCost,"+
							"ProgressCondition, RegistrationPerson,RegistrationPersonID, RegistrationDate) values("+
							"@itemnum, @itemdrawnum,@itemname,@sourcecode,@source,@PropertyClassification,@comname,@comnum,@total,@quantity1,@date1,@quantity2,@date2,"+
							"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@cost,'대기', @Person, @PersonID, @Date)";

						
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
						comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
						comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
						comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = m_InputList[3].ToString();
						comm.Parameters.Add("@source",SqlDbType.VarChar).Value = m_InputList[4].ToString();
						comm.Parameters.Add("@PropertyClassification",SqlDbType.VarChar).Value = m_InputList[5].ToString();
						comm.Parameters.Add("@comname",SqlDbType.VarChar).Value = "";//Convert.DBNull;//m_InputList[5].ToString();
						comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = "";//Convert.DBNull;//m_InputList[6].ToString();
						comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
						comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
						comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
						comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[10].ToString());
						if(m_InputList[11].ToString().Trim() == "")
							comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
						else
							comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[11].ToString()).ToShortDateString();
						comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[12].ToString());
						if(m_InputList[13].ToString().Trim() == "")
							comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
						else
							comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[13].ToString()).ToShortDateString();
						comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[14].ToString());
						if(m_InputList[15].ToString().Trim() == "")
							comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
						else
							comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[15].ToString()).ToShortDateString();
						comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[16].ToString());
						if(m_InputList[17].ToString().Trim() == "")
							comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
						else
							comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[17].ToString()).ToShortDateString();
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[18].ToString());
						comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
						comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();					
						comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
						comm.CommandText = str;
						comm.ExecuteNonQuery();
					

				}
					break;
				case "ProductionPlanAdd":		//생산계획추가페이지
				{
					if(Division(m_InputList[0].ToString()))
					{
						str = @"Select ChildItemNum,ItemDrawNum,ItemName, (NeedQuantityNumerator/NeedQuantityDenominator) as Quantity 
							From IOI_MT inner join II_MT on IOI_MT.ChildItemNum = II_MT.ItemNum 
							Where ParentItemNum = @ItemNum and IOI_MT.RecodingState = 1 and II_MT.RecodingState = 1";
						comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
						DataSet ds = new DataSet();
						SqlDataAdapter da = new SqlDataAdapter(comm);
						da.Fill(ds);
						comm.Parameters.Clear();

						foreach(DataRow dr in ds.Tables[0].Rows)
						{
						
							str="Insert into PP_HT (ItemNum,ItemDrawNum,ItemName,ProductionPlanHistorySourceCode,ProductionPlanHistorySource,"+
								"ProductionPlanQuantity,ProductionBeginDate,DeliveryDate,RowMaterialCalculation,ProgressCondition,MaterialRequirementVolumNum,"+
								"RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
								"@itemnum, @itemdrawnum,@itemname,@sourcecode,@source,@total,@begindate,@DeliveryDate,@calculation,@MaterialRequirementVolumNum,'대기', @Person, @PersonID, @Date)";
						
							comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = dr["ChildItemNum"].ToString();
							comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = dr["ItemDrawNum"].ToString();
							comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = dr["ItemName"].ToString();
							comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = m_InputList[3].ToString();
							comm.Parameters.Add("@source",SqlDbType.VarChar).Value = m_InputList[4].ToString();
							comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[5].ToString())*decimal.Parse(dr["Quantity"].ToString());
							comm.Parameters.Add("@begindate",SqlDbType.SmallDateTime).Value =  DateTime.Now.ToShortDateString();
							comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value =DateTime.Parse(m_InputList[6].ToString()).ToShortDateString();
							comm.Parameters.Add("@calculation",SqlDbType.Bit).Value = 0;	// 미산출은 0, 산출은1	 MaterialRequirementVolumNum
							comm.Parameters.Add("@MaterialRequirementVolumNum",SqlDbType.Int).Value = Max_MaterialRequirementVolumNum(con,trans);
							comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
							comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();						
							comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							comm.CommandText = str;
							comm.ExecuteNonQuery();
						}
						
						comm.Parameters.Clear();

					}
					else
					{	

						str="Insert into PP_HT (ItemNum,ItemDrawNum,ItemName,ProductionPlanHistorySourceCode,ProductionPlanHistorySource,"+
							"ProductionPlanQuantity,ProductionBeginDate,DeliveryDate,RowMaterialCalculation,MaterialRequirementVolumNum,ProgressCondition,"+
							"RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
							"@itemnum, @itemdrawnum,@itemname,@sourcecode,@source,@total,@begindate,@DeliveryDate,@calculation,@MaterialRequirementVolumNum,'대기', @Person, @PersonID, @Date)";
						
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
						comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
						comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
						comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = m_InputList[3].ToString();
						comm.Parameters.Add("@source",SqlDbType.VarChar).Value = m_InputList[4].ToString();
						comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[5].ToString());
						comm.Parameters.Add("@begindate",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
						comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[6].ToString()).ToShortDateString();
						comm.Parameters.Add("@calculation",SqlDbType.Bit).Value = 0;	// 미산출은 0, 산출은1	
						comm.Parameters.Add("@MaterialRequirementVolumNum",SqlDbType.Int).Value = Max_MaterialRequirementVolumNum(con,trans);
						comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
						comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();						
						comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
						comm.CommandText = str;
						comm.ExecuteNonQuery();
					}

				}
					break;
				case "ClaimRegistration":		//클레임등록 페이지
				{
					str=@"Insert into CL_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,ReceiptDate,ClaimQuantity,ClaimCost,
							ClaimStatusMeaning,ClaimStatusCode,ClaimCauseMeaning,ClaimCauseCode,RegistrationPerson,RegistrationPersonID,RegistrationDate) values(
							@itemnum, @itemdrawnum,@itemname,@com,@comnum,@receiptdate,@claimquantity,@claimcost,@claimstatusmeaning,@claimstatuscode,@claimcausemeaning,
							@claimcausecode,@person,@personid,@Date)";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@receiptDate",SqlDbType.SmallDateTime).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@claimquantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[6].ToString());
					comm.Parameters.Add("@claimcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					comm.Parameters.Add("@claimstatusmeaning",SqlDbType.VarChar).Value = m_InputList[8].ToString();
					comm.Parameters.Add("@claimstatuscode",SqlDbType.VarChar).Value = m_InputList[9].ToString();
					comm.Parameters.Add("@claimcausemeaning",SqlDbType.VarChar).Value = m_InputList[10].ToString();
					comm.Parameters.Add("@claimcausecode",SqlDbType.VarChar).Value = m_InputList[11].ToString();
					comm.Parameters.Add("@person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = m_UserID.ToString();						
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					break;
				}
				case "Claim":		//지급 클레임등록 페이지
				{
					str=@"Insert into PCL_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,ReceiptDate,ClaimQuantity,ClaimCost,
							ClaimStatusMeaning,ClaimStatusCode,ClaimCauseMeaning,ClaimCauseCode,RegistrationPerson,RegistrationPersonID,RegistrationDate) values(
							@itemnum, @itemdrawnum,@itemname,@com,@comnum,@receiptdate,@claimquantity,@claimcost,@claimstatusmeaning,@claimstatuscode,@claimcausemeaning,
							@claimcausecode,@person,@personid,@Date)";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@receiptDate",SqlDbType.SmallDateTime).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@claimquantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[6].ToString());
					comm.Parameters.Add("@claimcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					comm.Parameters.Add("@claimstatusmeaning",SqlDbType.VarChar).Value = m_InputList[8].ToString();
					comm.Parameters.Add("@claimstatuscode",SqlDbType.VarChar).Value = m_InputList[9].ToString();
					comm.Parameters.Add("@claimcausemeaning",SqlDbType.VarChar).Value = m_InputList[10].ToString();
					comm.Parameters.Add("@claimcausecode",SqlDbType.VarChar).Value = m_InputList[11].ToString();
					comm.Parameters.Add("@person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = m_UserID.ToString();						
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					break;
				}
				case "SubBuyingOrder":
				{
					str=@"Insert into SBO_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,DeliveryDate,DeliveryQuantity,DeliveryRemainQuantity,ApplyUnitCost,TotalCost,
							ProgressCondition,RegistrationPerson,RegistrationPersonID,RegistrationDate) values(
							@itemnum, @itemdrawnum,@itemname,@com,@comnum,@DeliveryDate,@DeliveryQuantity,@DeliveryRemainQuantity,@ApplyUnitCost,@TotalCost,
							'대기', @person,@personid,@Date)";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = m_InputList[11].ToString();
					comm.Parameters.Add("@DeliveryQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[5].ToString());
					comm.Parameters.Add("@DeliveryRemainQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[5].ToString());
					comm.Parameters.Add("@ApplyUnitCost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[6].ToString());
					comm.Parameters.Add("@TotalCost",SqlDbType.VarChar).Value = m_InputList[7].ToString();
					comm.Parameters.Add("@person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = m_UserID.ToString();						
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					break;
				}
				case "RowItemExhaust":				//원자재 소진 등록
				{
					str=@"Insert into E_HT (ExhaustItemNum,ExhaustItemDrawNum,ExhaustItemName,ExhaustQuantity, ExhaustDate, CreateItemNum,CreateItemDrawNum,CreateItemName,
							CreateQuantity,[Use], StartTime, EndTime, WorkerID, WorkerName, RegistrationPerson,RegistrationPersonID,RegistrationDate) values(
							@ExhaustItemNum, @ExhaustItemDrawNum,@ExhaustItemName,@ExhaustQuantity,@ExhaustDate,@CreateItemNum,@CreateItemDrawNum,@CreateItemName,
							@CreateQuantity,@Use, @StartTiem, @EndTime,@WorkerID, @WorkerName, @person,@personid,@Date)";

					comm.Parameters.Add("@ExhaustItemNum",m_InputList[0].ToString());
					comm.Parameters.Add("@ExhaustItemDrawNum", m_InputList[1].ToString());
					comm.Parameters.Add("@ExhaustItemName", m_InputList[2].ToString());
					comm.Parameters.Add("@ExhaustQuantity", decimal.Parse(m_InputList[3].ToString()));
					comm.Parameters.Add("@ExhaustDate", DateTime.Parse(m_InputList[4].ToString()).ToShortDateString());
					comm.Parameters.Add("@CreateItemNum", m_InputList[5].ToString());
					comm.Parameters.Add("@CreateItemDrawNum", m_InputList[6].ToString());
					comm.Parameters.Add("@CreateItemName", m_InputList[7].ToString());
					comm.Parameters.Add("@CreateQuantity", decimal.Parse(m_InputList[8].ToString()));
					comm.Parameters.Add("@Use", m_InputList[9].ToString());
					comm.Parameters.Add("@StartTiem", m_InputList[10].ToString());		
					comm.Parameters.Add("@EndTime", m_InputList[11].ToString());	
					comm.Parameters.Add("@WorkerID", m_InputList[12].ToString());		
					comm.Parameters.Add("@WorkerName", m_InputList[13].ToString());	
					comm.Parameters.Add("@person", m_UserName.ToString());
					comm.Parameters.Add("@personid", m_UserID.ToString());						
					comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					break;
				}
				case "EtcClaim"	:				//기타공제 등록
				{
					str=@"Insert into ECL_HT (CompanyName,BusinessRegistrationNum,ReceiptDate,EtcClaimRegion,ClaimCost,
							RegistrationPerson,RegistrationPersonID,RegistrationDate) values(
							@com,@comnum,@receiptdate,@region,@claimcost,@person,@personid,@Date)";

					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@receiptDate",SqlDbType.SmallDateTime).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@region",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@claimcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[4].ToString());
					comm.Parameters.Add("@person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = m_UserID.ToString();						
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					break;
				}
				default:					
					break;
			}
				
			
			
			HttpContext hc = HttpContext.Current;
			hc.Response.Write("<script language=javascript>");
			hc.Response.Write("window.status='등록되었습니다!';");
			//hc.Response.Write("alert('등록하였습니다.');");
			hc.Response.Write("</script>");
			
		}




		/// <summary>
		/// 자산분류 찾는 메서드
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="item"></param>
		private string PropertyClassification(SqlConnection conn, SqlTransaction tr,string item)
		{
			string m_PropertyClassification = "원자재";
			string str = "Select PropertyClassification From II_MT where RecodingState =1 and ItemNum=@item";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@item", SqlDbType.VarChar).Value = item;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
				m_PropertyClassification = dr["PropertyClassification"].ToString();
			dr.Close();
			comm.Parameters.Clear();

			return m_PropertyClassification;
		}

		/// <summary>
		/// 구매발주시 현재고량을 입력하는 메서드
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		private decimal StoreQuantity(SqlConnection conn, SqlTransaction tr,string item)
		{

			
			string m_PropertyClassification = PropertyClassification(conn,tr,item);
			string str="";
			decimal m_Quantity = 0;

			
			if(m_PropertyClassification == "원자재")
				str = "Select StockQuantity12 From RMS_MT where RecodingState =1 and [Year] = @year and ItemNum = @item";
			else
				str = "Select StockQuantity12 From BS_MT where RecodingState =1 and [Year] = @year and ItemNum = @item and BusinessStorehouseNum =1";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@item", SqlDbType.VarChar).Value = item;
			comm.Parameters.Add("@year", SqlDbType.Int).Value = DateTime.Now.Year;

			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
				m_Quantity = Decimal.Parse(dr["StockQuantity12"].ToString());
			dr.Close();

			return m_Quantity;
		}


		private void StoreHistoryRegister(SqlConnection con, SqlTransaction trans)
		{
			string str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','원자재창고','0',@Quantity,@Date,'공통소진',@HistoryIndex)";
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
				
			comm.CommandText = str;
			comm.CommandType = CommandType.Text;
			comm.Transaction =trans;			
			comm.Parameters.Add("@itemnum",m_InputList[0].ToString());
			comm.Parameters.Add("@itemdrawnum", m_InputList[1].ToString());
			comm.Parameters.Add("@itemname", m_InputList[2].ToString());
			if(m_InputList[3] == null || m_InputList[3].ToString() == "")
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[3].ToString());
			comm.Parameters.Add("@Date",DateTime.Parse(m_InputList[4].ToString()));
			comm.Parameters.Add("@HistoryIndex",MaxE_HT(con,trans));				//원장번호
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();


			str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','원자재창고','1',@Quantity,@Date,'공통소진',@HistoryIndex)";
				
			comm.CommandText = str;
			comm.Parameters.Add("@itemnum",m_InputList[5].ToString());
			comm.Parameters.Add("@itemdrawnum", m_InputList[6].ToString());
			comm.Parameters.Add("@itemname", m_InputList[7].ToString());
			if(m_InputList[8] == null || m_InputList[8].ToString() == "")
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@Quantity",SqlDbType.Decimal).Value = Decimal.Parse(m_InputList[8].ToString());
			comm.Parameters.Add("@Date",DateTime.Parse(m_InputList[4].ToString()));
			comm.Parameters.Add("@HistoryIndex",MaxE_HT(con,trans));				//원장번호
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();
		}


		private int Max_MaterialRequirementVolumNum(SqlConnection conn, SqlTransaction tr)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			comm.CommandText = "Select Max(MaterialRequirementVolumNum) From PP_HT";
			int Num = int.Parse(comm.ExecuteScalar().ToString()) + 1;

			return Num;
		}

            /// <summary>
            /// 매입매출테이블 수금액 등록
            /// </summary>
		private void BSI_Update(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			int year;
			int mon;
			string str = "";
			switch(m_PageName)
			{
				case "CollectMoneyRegistration": //수금등록
					year = DateTime.Parse(m_InputList[2].ToString()).Year;
					mon =  DateTime.Parse(m_InputList[2].ToString()).Month;
					Table table = new Table(year,mon);
					str = table.CollectMoneyIncreaseUnCollectMoneyDiminutionTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[3].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@year",year);
					break;
				case "PaymentPlanResultRegistration":
					year = DateTime.Parse(m_InputList[2].ToString()).Year;
					mon =  DateTime.Parse(m_InputList[2].ToString()).Month;
					Table table1 = new Table(year,mon);
					str = table1.UNCollectMoneyIncreasePaymentMoneyDiminutionTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[3].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@year",year);
					break;
				case "ClaimRegistration": //클레임등록
					year = DateTime.Parse(m_InputList[5].ToString()).Year;
					mon =  DateTime.Parse(m_InputList[5].ToString()).Month;
					Table table2 = new Table(year,mon);
					str = table2.ClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@year",year);
					break;
				case "Claim": //지급 클레임등록
					year = DateTime.Parse(m_InputList[5].ToString()).Year;
					mon =  DateTime.Parse(m_InputList[5].ToString()).Month;
					Table table3 = new Table(year,mon);
					str = table3.PayClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@year",year);
					break;
				case "EtcClaim":		//기타공제
					year = DateTime.Parse(m_InputList[2].ToString()).Year;
					mon =  DateTime.Parse(m_InputList[2].ToString()).Month;
					Table table4 = new Table(year,mon);
					str = table4.PayClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[4].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@year",year);
					break;
				default:
					break;
			}
			comm.CommandText = str;
			comm.ExecuteNonQuery();
		}













		/// <summary>
		/// 생산의뢰등록시 자산분류가 팬텀인지 
		/// 파악하기위해 호출하는 함수
		/// 작업일보등록시 창고에 입출력을 위해
		/// 품목의 종류를 구분하는 함수
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private bool Division(string ItemNum)
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

			if(aa.ToString() =="팬텀")
				return true;
			else
				return false;
		}



		public void ReceiveUpdate()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			DataSet ds = new DataSet();
			try
			{
				SqlCommand comm = new SqlCommand();
				comm.Connection = conn;
				comm.Transaction = tr;
				
			
				

				if(MonthClosing() == false)
				{
					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('월마감되어 수정이 불가능합니다!');");
					hc.Response.Write("</script>");
				}
				else if(m_Index == "0")//레코드 등록여부
				{
					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('등록되지 않은 품목입니다.');");
					hc.Response.Write("</script>");

				}
				else
				{
					if(ReceiveingRegistrationValidate() == true)
					{
						Update(conn,tr);
					}				
				}
			
				tr.Commit();
			}
			catch(Exception ee)
			{
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
			
		}
		
		/// <summary>
		/// 수정함수
		/// </summary>
		/// <returns></returns>
		public DataSet MainTableUpdate()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			DataSet ds = new DataSet();
			try
			{
				SqlCommand comm = new SqlCommand();
				comm.Connection = conn;
				comm.Transaction = tr;
				
			
				string str = "";

				if(MonthClosing() == false)
				{
					if(m_PageName.ToString() == "CollectMoneyRegistration" || m_PageName.ToString() == "PaymentPlanResultRegistration" || m_PageName.ToString() == "ClaimRegistration" || m_PageName.ToString() == "Claim" )
						str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today";
					else if(m_PageName.ToString() == "SubBuyingOrder")
					{
						str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today";
					}
					else if(m_PageName == "RowItemExhaust")
					{
						str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							E_HT.RegistrationDate = @today order by ExhaustHistoryIndex desc";
						comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					}
					else
						str = "Select SmallClassificationName as ItemState, "+m_Table+".* "+
							" From "+m_Table+" inner join II_MT on "+m_Table+".ItemNum = II_MT.ItemNum "+
							" inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode "+
							" where II_MT.RecodingState = 1 and ProgressCondition = '대기' and "+m_Table+".RegistrationDate = @today";
					comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
				}
				else if(m_Index == "0")//레코드 등록여부
				{
					if(m_PageName.ToString() == "CollectMoneyRegistration" || m_PageName.ToString() == "PaymentPlanResultRegistration" || m_PageName.ToString() == "ClaimRegistration" || m_PageName.ToString() == "Claim" || m_PageName.ToString() == "EtcClaim")
						str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today";
					else if(m_PageName.ToString() == "SubBuyingOrder")
					{
						str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today";
					}
					else if(m_PageName == "RowItemExhaust")
					{
						str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							E_HT.RegistrationDate = @today order by ExhaustHistoryIndex desc";
					}
					else
						str = "Select SmallClassificationName as ItemState, "+m_Table+".* "+
							" From "+m_Table+" inner join II_MT on "+m_Table+".ItemNum = II_MT.ItemNum "+
							" inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode "+
							" where II_MT.RecodingState = 1 and ProgressCondition = '대기' and "+m_Table+".RegistrationDate = @today";
					comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();

					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('등록되지 않은 품목입니다.');");
					hc.Response.Write("</script>");

				}
				else
				{
					switch(m_PageName)
					{
						case "ReceiveingRegistration":		//수주등록 유효성 검사
						{
							if(ReceiveingRegistrationValidate() == true)
							{
								Update(conn,tr);
							}
							str = @"SELECT isnull(PUC.SmallClassificationName,'') as ItemState,   II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName,
							II_MT.PropertyClassification, PUC_MT.SmallClassificationName as Unit, II_MT.Standard , 
							II_MT.Standard, RO_HT.CompanyName, RO_HT.BusinessRegistrationNum, CompanyPersonInCharge, CI_MT.TelephoneNum, 
							RO_HT.ReceivingOrderDate, 
							RO_HT.ApplyUnitCost, RO_HT.DeliveryRequestQuantity1, 
							CASE RO_HT.ProductionRequestDivision WHEN '1' THEN '예' ELSE '아니오' END AS ProductionRequestDivision,
							RO_HT.DeliveryRequestDate1, RO_HT.DeliveryRequestQuantity2, 
							RO_HT.DeliveryRequestDate2, RO_HT.DeliveryRequestQuantity3,
							RO_HT.DeliveryRequestDate3, RO_HT.DeliveryRequestQuantity4, 
							RO_HT.DeliveryRequestDate4, RO_HT.DeliveryRequestQuantity5,
							RO_HT.DeliveryRequestDate5, RO_HT.TotalReceiveingOrderQuantity,
							RO_HT.TotalCost, RO_HT.OutStorehouseQuantity, RO_HT.SuitabilityQuantity,
							RO_HT.UnInspectionQuantity, RO_HT.RemainderQuantity, RO_HT.OrderNum,
							RO_HT.DeliveryPlace, RO_HT.VolumNum, RO_HT.ProgressCondition,
							RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID,
							RO_HT.RegistrationDate, RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, 
							RO_HT.UpdatingDate, RO_HT.ReceivingOrderHistoryIndex 
							FROM RO_HT
							INNER JOIN II_MT ON RO_HT.ItemNum = II_MT.ItemNum
							Inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
							INNER JOIN PUC_MT ON II_MT.Unit = PUC_MT.SmallClassificationCode
							left outer join PUC_MT PUC on II_MT.ItemState = PUC.SmallClassificationCode
							
							Where CI_MT.RecodingState = 1 and II_MT.RecodingState = '1' and RO_HT.RegistrationDate = @today and RO_HT.ProgressCondition = '대기'";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						
						}
						case "CollectMoneyRegistration":	//수금등록 유효성 검사
						{
							if(CollectMoneyRegistrationValidate()==true)
							{
								Update(conn,tr);
								BSI_MTUpdate(conn,tr);
							}						
					
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "PaymentPlanResultRegistration":	//지급등록 유효성 검사
						{
							if(PaymentPlanResultRegistrationValidate()==true)
							{
								Update(conn,tr);
								BSI_MTUpdate(conn,tr);
							
							}
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "GoodsBuyingRequestAdd":			//상품구매의뢰 유효성 검사
						{
							if(GoodsBuyingRequestAddValidate()==true)
							{
								Update(conn,tr);
							}

							str = @"Select SmallClassificationName as ItemState, BR_HT.* 
								From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and PropertyClassification = '상품' and ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "RowMaterialRequriementAdd":		//원자재구매의뢰 유효성 검사
						{
							if(RowMaterialRequriementAddValidate()==true)
							{
								Update(conn,tr);
							}

							//str = "Select ItemState, BR_HT.* From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum where PropertyClassification = '원자재' and ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and RecodingState = 1 and volumnum is null  ";
							str = "Select BR_HT.*, PUC_MT.SmallClassificationName as Unit From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode where PUC_MT.RecodingState =1 and II_MT.RecodingState = 1 and II_MT.PropertyClassification = '원자재' and BR_HT.ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "BuyingOrderAdd":					//구매발주추가 유효성 검사
						{
							if(BuyingOrderAddValidate()==true)
							{
								Update(conn,tr);
							}

							str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "ProductionRequestAdd":		//생산의뢰추가 유효성 검사
						{
							if(ProductionRequestAddValidate()==true)
							{
								Update(conn,tr);							
							}
							str = @"Select SmallClassificationName as ItemState, PR_HT.* 
								From PR_HT inner join II_MT on PR_HT.ItemNum = II_MT.ItemNum 
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PR_HT.RegistrationDate = @today and volumnum is null ";
							//str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "ClaimRegistration":		//클레임등록 유효성 검사
						{
							if(ClaimRegistrationValidate() ==true)
							{
								Update(conn,tr);
								BSI_MTUpdate(conn,tr);
							}

							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "Claim":		//지급 클레임등록 유효성 검사
						{
							if(ClaimValidate() ==true)
							{
								Update(conn,tr);
								BSI_MTUpdate(conn,tr);
							}

							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "SubBuyingOrder":
						{
							if(SubBuyingOrderValidate()==true)
							{
								Update(conn,tr);
							}

							str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and  RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}

						case "RowItemExhaust":			//원자재 소진
						{
							if(RowItemExhaustValidate()==true)
							{
								Update(conn,tr);
								RMS_MTUpdate(conn,tr);
							}

							str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							E_HT.RegistrationDate = @today order by ExhaustHistoryIndex desc";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "EtcClaim":		//지급 클레임등록 
						{
							Update(conn,tr);
							BSI_MTUpdate(conn,tr);

							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						default :		//생산계획추가 유효성 검사
						{
							if(ProductionPlanAddValidate()==true)
							{
								Update(conn,tr);
							}

							str = @"Select SmallClassificationName as ItemState, PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanHistorySourceCode,
								ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate, DeliveryDate,
								case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation,
								VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID,
								PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate,
								HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT
								inner join II_MT on PP_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PP_HT.RegistrationDate = @today and volumnum is null  ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
					
					}
					
				}
				comm.CommandText = str;
				SqlDataAdapter da = new SqlDataAdapter(comm);
				
				da.Fill(ds);
				tr.Commit();
			}
			catch(Exception ee)
			{
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
			return ds;
		}


		


		/// <summary>
		/// 실제 수정함수
		/// </summary>
		private void Update(SqlConnection con, SqlTransaction trans) 
		{								
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			
			string str = "";

			switch(m_PageName)	
			{

				case "ReceiveingRegistration":		//수주등록페이지
				{
					str = "Update RO_HT set ItemNum=@itemnum,ItemDrawNum=@itemdrawnum,ItemName=@itemname,CompanyName=@com,BusinessRegistrationNum=@comnum,"+
						"PropertyClassification=@classification,ProductionRequestDivision=@division,ReceivingOrderDate=@receivingdate,ApplyUnitCost=@cost,"+
						"DeliveryRequestQuantity1=@quantity1,DeliveryRequestDate1=@date1,DeliveryRequestQuantity2=@quantity2,DeliveryRequestDate2=@date2,"+
						"DeliveryRequestQuantity3=@quantity3,DeliveryRequestDate3=@date3,DeliveryRequestQuantity4=@quantity4,DeliveryRequestDate4=@date4,"+
						"DeliveryRequestQuantity5=@quantity5,DeliveryRequestDate5=@date5,TotalReceiveingOrderQuantity=@total,TotalCost=@totalcost,"+
						"OutStorehouseQuantity=@storequantity,SuitabilityQuantity=@suitquantity,UnInspectionQuantity=@unquantity,RemainderQuantity=@remainquantity,"+
						"OrderNum=@ordernum,DeliveryPlace=@place,UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date Where ReceivingOrderHistoryIndex = @Index";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@division",SqlDbType.Bit).Value = int.Parse(m_InputList[6].ToString());
					comm.Parameters.Add("@receivingdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[7].ToString()).ToShortDateString();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());

					comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[9].ToString());
					comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[10].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[11].ToString());
					if(m_InputList[12].ToString().Trim() == "")
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[12].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[13].ToString());
					if(m_InputList[14].ToString().Trim() == "")
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[14].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[15].ToString());
					if(m_InputList[16].ToString().Trim() == "")
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[16].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[17].ToString());
					if(m_InputList[18].ToString().Trim() == "")
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[18].ToString()).ToShortDateString();
					comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[19].ToString());
					comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[20].ToString());
					comm.Parameters.Add("@storequantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[21].ToString());
					comm.Parameters.Add("@suitquantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[22].ToString());
					comm.Parameters.Add("@unquantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[23].ToString());
					comm.Parameters.Add("@remainquantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[24].ToString());
					comm.Parameters.Add("@ordernum",SqlDbType.VarChar).Value = m_InputList[25].ToString();
					comm.Parameters.Add("@place",SqlDbType.VarChar).Value = m_InputList[26].ToString();
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();				
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();

					comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());

				}
					break;
				case "CollectMoneyRegistration":	//수금등록페이지
				{
					str = "Update CM_HT set CompanyName=@com,BusinessRegistrationNum=@comnum,CollectMoneyDate=@collectdate,ItemPaymentCost=@cost,"+
						"SupplementaryValueTaxPaymentCost=@tax,DecisionMethodCode=@methodcode,DecisionMethod=@method,BillNum1=@billnum1,"+
						"BillPaymentDate1=@bulldate1,BankCode1=@bankcode1,BankName1=@bank1,BillNum2=@billnum2,BillPaymentDate2=@bulldate2,"+
						"BankCode2=@bankcode2,BankName2=@bank2,BillNum3=@billnum3,BillPaymentDate3=@bulldate3,BankCode3=@bankcode3,BankName3=@bank3,"+
						"UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date Where CollectMoneyHistoryIndex = @Index";

					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@collectdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[2].ToString()).ToShortDateString();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[3].ToString());
					comm.Parameters.Add("@tax",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[4].ToString());
					comm.Parameters.Add("@methodcode",SqlDbType.VarChar).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@method",SqlDbType.VarChar).Value = m_InputList[6].ToString();
					comm.Parameters.Add("@billnum1",SqlDbType.VarChar).Value = m_InputList[7].ToString();
					if(m_InputList[8].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate1",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[8].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode1",SqlDbType.VarChar).Value = m_InputList[9].ToString();
					comm.Parameters.Add("@bank1",SqlDbType.VarChar).Value = m_InputList[10].ToString();
					comm.Parameters.Add("@billnum2",SqlDbType.VarChar).Value = m_InputList[11].ToString();
					if(m_InputList[12].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[12].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode2",SqlDbType.VarChar).Value = m_InputList[13].ToString();
					comm.Parameters.Add("@bank2",SqlDbType.VarChar).Value = m_InputList[14].ToString();
					comm.Parameters.Add("@billnum3",SqlDbType.VarChar).Value = m_InputList[15].ToString();
					if(m_InputList[16].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[16].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode3",SqlDbType.VarChar).Value = m_InputList[17].ToString();
					comm.Parameters.Add("@bank3",SqlDbType.VarChar).Value = m_InputList[18].ToString();
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();			
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());
				}
					break;
				case "PaymentPlanResultRegistration":	//지급등록페이지
				{
					str = "Update P_HT set CompanyName=@com,BusinessRegistrationNum=@comnum,PaymentDate=@collectdate,ItemPaymentCost=@cost,"+
						"SupplementaryValueTaxPaymentCost=@tax,DecisionMethodCode=@methodcode,DecisionMethod=@method,ProofData=@data,"+
						"BillNum1=@billnum1,BillPaymentDate1=@bulldate1,BankCode1=@bankcode1,BankName1=@bank1,BillNum2=@billnum2,"+
						"BillPaymentDate2=@bulldate2,BankCode2=@bankcode2,BankName2=@bank2,BillNum3=@billnum3,BillPaymentDate3=@bulldate3,BankCode3=@bankcode3,BankName3=@bank3,"+
						"UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date Where PaymentHistoryIndex = @Index";
							
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@collectdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[2].ToString()).ToShortDateString();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[3].ToString());
					comm.Parameters.Add("@tax",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[4].ToString());
					comm.Parameters.Add("@methodcode",SqlDbType.VarChar).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@method",SqlDbType.VarChar).Value = m_InputList[6].ToString();
					comm.Parameters.Add("@data",SqlDbType.VarChar).Value = m_InputList[7].ToString();
					comm.Parameters.Add("@billnum1",SqlDbType.VarChar).Value = m_InputList[8].ToString();
					if(m_InputList[9].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate1",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode1",SqlDbType.VarChar).Value = m_InputList[10].ToString();
					comm.Parameters.Add("@bank1",SqlDbType.VarChar).Value = m_InputList[11].ToString();
					comm.Parameters.Add("@billnum2",SqlDbType.VarChar).Value = m_InputList[12].ToString();
					if(m_InputList[13].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[13].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode2",SqlDbType.VarChar).Value = m_InputList[14].ToString();
					comm.Parameters.Add("@bank2",SqlDbType.VarChar).Value = m_InputList[15].ToString();
					comm.Parameters.Add("@billnum3",SqlDbType.VarChar).Value = m_InputList[16].ToString();
					if(m_InputList[17].ToString().Trim() == "")
						comm.Parameters.Add("@bulldate3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@bulldate3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[17].ToString()).ToShortDateString();
					comm.Parameters.Add("@bankcode3",SqlDbType.VarChar).Value = m_InputList[18].ToString();
					comm.Parameters.Add("@bank3",SqlDbType.VarChar).Value = m_InputList[19].ToString();
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();	;				
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());
				}
					break;
				case "GoodsBuyingRequestAdd":			//상품구매의뢰추가페이지
				{
					str="Update BR_HT set ItemNum=@itemnum,ItemDrawNum=@itemdrawnum,ItemName=@itemname,PropertyClassification=@classification,"+
						"BuyingRequestSourceCode=@sourcecode,BuyingRequestSource=@source,FirstDeliveryDemandQuantity=@quantity1,FirstDeliveryDemandDate=@date1,"+
						"SecondDeliveryDemandQuantity=@quantity2,SecondDeliveryDemandDate=@date2,ThirdDeliveryDemandQuantity=@quantity3,ThirdDeliveryDemandDate=@date3,"+
						"FourthDeliveryDemandQuantity=@quantity4,FourthDeliveryDemandDate=@date4,FifthDeliveryDemandQuantity=@quantity5,FifthDeliveryDemandDate=@date5,"+
						"OrderQuantity=@total,ApplyUnitCost=@cost,TotalCost=@totalcost,"+
						"UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date Where BuyingRequestHistoryIndex = @Index";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@source",SqlDbType.VarChar).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[6].ToString());
					comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[7].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
					if(m_InputList[9].ToString().Trim() == "")
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[10].ToString());
					if(m_InputList[11].ToString().Trim() == "")
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[11].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[12].ToString());
					if(m_InputList[13].ToString().Trim() == "")
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[13].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[14].ToString());
					if(m_InputList[15].ToString().Trim() == "")
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[15].ToString()).ToShortDateString();
					comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[16].ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[17].ToString());
					comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[18].ToString());
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();				
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());
				}
					break;
				case "RowMaterialRequriementAdd":		//원자재구매의뢰추가페이지
				{
					str="Update BR_HT set ItemNum=@itemnum,ItemDrawNum=@itemdrawnum,ItemName=@itemname,PropertyClassification=@classification,"+
						"BuyingRequestSourceCode=@sourcecode,BuyingRequestSource=@source,FirstDeliveryDemandQuantity=@quantity1,FirstDeliveryDemandDate=@date1,"+
						"SecondDeliveryDemandQuantity=@quantity2,SecondDeliveryDemandDate=@date2,ThirdDeliveryDemandQuantity=@quantity3,ThirdDeliveryDemandDate=@date3,"+
						"FourthDeliveryDemandQuantity=@quantity4,FourthDeliveryDemandDate=@date4,FifthDeliveryDemandQuantity=@quantity5,FifthDeliveryDemandDate=@date5,"+
						"OrderQuantity=@total,ApplyUnitCost=@cost,TotalCost=@totalcost,RequestPostCode = @postcode,RequestPost = @post, "+
						"UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date Where BuyingRequestHistoryIndex = @Index";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@source",SqlDbType.VarChar).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[6].ToString());
					comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[7].ToString()).ToShortDateString();
					if(m_InputList[8].ToString() == "")
						comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
					else
						comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
					if(m_InputList[9].ToString().Trim() == "")
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
					if(m_InputList[10].ToString() == "")
						comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
					else
						comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[10].ToString());
					if(m_InputList[11].ToString().Trim() == "")
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[11].ToString()).ToShortDateString();
					if(m_InputList[12].ToString() == "")
						comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
					else
						comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[12].ToString());
					if(m_InputList[13].ToString().Trim() == "")
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[13].ToString()).ToShortDateString();
					if(m_InputList[14].ToString() == "")
						comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
					else
						comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[14].ToString());
					if(m_InputList[15].ToString().Trim() == "")
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[15].ToString()).ToShortDateString();
					comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[16].ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[17].ToString());
					comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[18].ToString());
						
					comm.Parameters.Add("@postcode",SqlDbType.VarChar).Value = m_InputList[19].ToString();
					comm.Parameters.Add("@post",SqlDbType.VarChar).Value = m_InputList[20].ToString();

					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();			
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());
				}
					break;
				case "BuyingOrderAdd":					//구매발주추가페이지
				{
					str="Update BO_HT Set ItemNum=@itemnum,ItemDrawNum=@itemdrawnum,ItemName=@itemname,CompanyName=@com,BusinessRegistrationNum=@comnum,FirstDeliveryDemandQuantity=@quantity1,FirstDeliveryDemandDate=@date1,"+
						"SecondDeliveryDemandQuantity=@quantity2,SecondDeliveryDemandDate=@date2,ThirdDeliveryDemandQuantity=@quantity3,ThirdDeliveryDemandDate=@date3,FourthDeliveryDemandQuantity=@quantity4,FourthDeliveryDemandDate=@date4,"+
						"FifthDeliveryDemandQuantity=@quantity5,FifthDeliveryDemandDate=@date5,OrderQuantity=@total,ApplyUnitCost=@cost,TotalCost=@totalcost,OrderRate=@rate,RemainQuantity=@remain, "+
						"UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date Where BuyingOrderHistoryIndex = @Index";
						
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[5].ToString());
					comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[6].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					if(m_InputList[8].ToString().Trim() == "")
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[8].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[9].ToString());
					if(m_InputList[10].ToString().Trim() == "")
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[10].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[11].ToString());
					if(m_InputList[12].ToString().Trim() == "")
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[12].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[13].ToString());
					if(m_InputList[14].ToString().Trim() == "")
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[14].ToString()).ToShortDateString();
					comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[15].ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[16].ToString());
					comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[17].ToString());
					comm.Parameters.Add("@rate",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[18].ToString());
					comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[19].ToString());
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();	
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@Index",SqlDbType.Int).Value = m_Index.ToString();
				}
					break;
				case "ProductionRequestAdd":		//생산의뢰추가페이지
				{
					str="Update PR_HT Set ItemNum=@itemnum,ItemDrawNum=@itemdrawnum,ItemName=@itemname,ProductionRequestSourceCode=@sourcecode,ProductionRequestSource=@source, PropertyClassification = @PropertyClassification,"+
						"CompanyName=@comname,BusinessRegistrationNum=@comnum,ProductionRequestQuantity=@total,RequestQuantity1=@quantity1,RequestDate1=@date1,RequestQuantity2=@quantity2,"+
						"RequestDate2=@date2,RequestQuantity3=@quantity3,RequestDate3=@date3,RequestQuantity4=@quantity4,RequestDate4=@date4,RequestQuantity5=@quantity5,RequestDate5=@date5,"+
						"ApplyUnitCost=@cost,UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date Where ProductionRequestHistoryIndex = @Index";

						
					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@source",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@PropertyClassification",SqlDbType.VarChar).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@comname",SqlDbType.VarChar).Value = "";
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = "";
					comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
					comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[10].ToString());
					if(m_InputList[11].ToString().Trim() == "")
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[11].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[12].ToString());
					if(m_InputList[13].ToString().Trim() == "")
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[13].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[14].ToString());
					if(m_InputList[15].ToString().Trim() == "")
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[15].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[16].ToString());
					if(m_InputList[17].ToString().Trim() == "")
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					else
						comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[17].ToString()).ToShortDateString();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[18].ToString());
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();				
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());
				}
					break;
				case "ProductionPlanAdd":		//생산계획추가페이지
				{
					str="Update PP_HT set ItemNum=@itemnum,ItemDrawNum=@itemdrawnum,ItemName=@itemname,ProductionPlanHistorySourceCode=@sourcecode,ProductionPlanHistorySource=@source,"+
						"ProductionPlanQuantity=@total,DeliveryDate=@begindate,UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date Where ProductionPlanHistoryIndex = @Index";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@sourcecode",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@source",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[5].ToString());
					comm.Parameters.Add("@begindate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[6].ToString()).ToShortDateString();
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();			
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());
				}
					break;
				case "ClaimRegistration":		//클레임 등록페이지
				{
					str=@"Update CL_HT set ItemNum = @itemnum, ItemDrawNum = @itemdrawnum, ItemName = @itemname, CompanyName = @com, BusinessRegistrationNum = @comnum,
						ReceiptDate = @receiptdate, ClaimQuantity = @claimquantity, ClaimCost = @claimcost, ClaimStatusMeaning = @claimstatusmeaning,
						ClaimStatusCode = @claimstatuscode, ClaimCauseMeaning = @claimcausemeaning, ClaimCauseCode = @claimcausecode, 
						UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @date Where ClameHistoryIndex = @index";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@receiptDate",SqlDbType.SmallDateTime).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@claimquantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[6].ToString());
					comm.Parameters.Add("@claimcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					comm.Parameters.Add("@claimstatusmeaning",SqlDbType.VarChar).Value = m_InputList[8].ToString();
					comm.Parameters.Add("@claimstatuscode",SqlDbType.VarChar).Value = m_InputList[9].ToString();
					comm.Parameters.Add("@claimcausemeaning",SqlDbType.VarChar).Value = m_InputList[10].ToString();
					comm.Parameters.Add("@claimcausecode",SqlDbType.VarChar).Value = m_InputList[11].ToString();
					comm.Parameters.Add("@person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = m_UserID.ToString();						
					comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());
				}
					break;

				case "Claim":		//지급 클레임 등록페이지
				{
					str=@"Update PCL_HT set ItemNum = @itemnum, ItemDrawNum = @itemdrawnum, ItemName = @itemname, CompanyName = @com, BusinessRegistrationNum = @comnum,
						ReceiptDate = @receiptdate, ClaimQuantity = @claimquantity, ClaimCost = @claimcost, ClaimStatusMeaning = @claimstatusmeaning,
						ClaimStatusCode = @claimstatuscode, ClaimCauseMeaning = @claimcausemeaning, ClaimCauseCode = @claimcausecode, 
						UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @date Where ClameHistoryIndex = @index";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@receiptDate",SqlDbType.SmallDateTime).Value = m_InputList[5].ToString();
					comm.Parameters.Add("@claimquantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[6].ToString());
					comm.Parameters.Add("@claimcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					comm.Parameters.Add("@claimstatusmeaning",SqlDbType.VarChar).Value = m_InputList[8].ToString();
					comm.Parameters.Add("@claimstatuscode",SqlDbType.VarChar).Value = m_InputList[9].ToString();
					comm.Parameters.Add("@claimcausemeaning",SqlDbType.VarChar).Value = m_InputList[10].ToString();
					comm.Parameters.Add("@claimcausecode",SqlDbType.VarChar).Value = m_InputList[11].ToString();
					comm.Parameters.Add("@person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = m_UserID.ToString();						
					comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());
				}
					break;
				case "SubBuyingOrder":
				{
					str=@"Update SBO_HT set ItemNum = @itemnum ,ItemDrawNum = @itemdrawnum,ItemName = @itemname,CompanyName = @com,
							BusinessRegistrationNum = @comnum,DeliveryDate = @DeliveryDate ,DeliveryQuantity = @DeliveryQuantity, DeliveryRemainQuantity = @DeliveryRemainQuantity,ApplyUnitCost = @ApplyUnitCost,TotalCost = @TotalCost,
							UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @date Where SubBuyingOrderHistoryIndex = @index";

					comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = m_InputList[6].ToString();
					comm.Parameters.Add("@DeliveryQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[5].ToString());
					comm.Parameters.Add("@DeliveryRemainQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[5].ToString());
					comm.Parameters.Add("@ApplyUnitCost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					comm.Parameters.Add("@TotalCost",SqlDbType.VarChar).Value = m_InputList[8].ToString();
					comm.Parameters.Add("@person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = m_UserID.ToString();						
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					break;
				}
				case "RowItemExhaust":			// 원자재 소진
				{
					str=@"Update E_HT set ExhaustItemNum = @ExhaustItemNum, ExhaustItemDrawNum = @ExhaustItemDrawNum, ExhaustItemName = @ExhaustItemName,
					ExhaustQuantity = @ExhaustQuantity, ExhaustDate = @ExhaustDate, 
					CreateItemNum = @CreateItemNum, CreateItemDrawNum = @CreateItemDrawNum, CreateItemName = @CreateItemName, CreateQuantity = @CreateQuantity,
					UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @Date where ExhaustHistoryIndex = @Index";
					comm.Parameters.Add("@ExhaustItemNum", m_InputList[0].ToString());
					comm.Parameters.Add("@ExhaustItemDrawNum",m_InputList[1].ToString());
					comm.Parameters.Add("@ExhaustItemName",m_InputList[2].ToString());
					comm.Parameters.Add("@ExhaustQuantity", decimal.Parse(m_InputList[3].ToString()));
					comm.Parameters.Add("@ExhaustDate", DateTime.Parse(m_InputList[4].ToString()).ToShortDateString());
					comm.Parameters.Add("@CreateItemNum", m_InputList[5].ToString());
					comm.Parameters.Add("@CreateItemDrawNum", m_InputList[6].ToString());
					comm.Parameters.Add("@CreateItemName", m_InputList[7].ToString());
					comm.Parameters.Add("@CreateQuantity", decimal.Parse(m_InputList[8].ToString()));
					comm.Parameters.Add("@person", m_UserName.ToString());
					comm.Parameters.Add("@personid", m_UserID.ToString());						
					comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());
					comm.Parameters.Add("@Index", int.Parse(m_Index.ToString()));
					comm.CommandText = str;
					comm.ExecuteNonQuery();
					break;
				}
				case "EtcClaim":		//지급 클레임등록 
				{
					str=@"Update ECL_HT set CompanyName = @com, BusinessRegistrationNum = @comnum,
						ReceiptDate = @receiptdate, EtcClaimRegion = @EtcClaimRegion, ClaimCost = @claimcost,UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @date Where EtcClameHistoryIndex = @index";

					comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_InputList[0].ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@receiptDate",SqlDbType.SmallDateTime).Value = m_InputList[2].ToString();
					comm.Parameters.Add("@EtcClaimRegion",SqlDbType.VarChar).Value = m_InputList[3].ToString();
					comm.Parameters.Add("@claimcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[4].ToString());
					comm.Parameters.Add("@person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = m_UserID.ToString();						
					comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());
				}
					break;
				default:					
					break;
			}
				
			
			comm.CommandText = str;
			comm.ExecuteNonQuery();

			HttpContext hc = HttpContext.Current;
			hc.Response.Write("<script language=javascript>");
			hc.Response.Write("alert('수정하였습니다.');");
			hc.Response.Write("</script>");
			
		}


		/// <summary>
		/// 원자재 소진시 원자재창고수정
		/// </summary>
		private void RMS_MTUpdate(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			Table table1 = new Table(int.Parse(m_InputList[10].ToString()),int.Parse(m_InputList[11].ToString()));
			comm.Parameters.Add("@num",m_InputList[0].ToString());
			comm.Parameters.Add("@quantity",-decimal.Parse(m_InputList[9].ToString()));
			comm.Parameters.Add("@cost",TotalCost(-decimal.Parse(m_InputList[9].ToString()),m_InputList[0].ToString()));
			comm.Parameters.Add("@year",int.Parse(m_InputList[10].ToString()));
			comm.CommandText = table1.OutRowTable();//원자재 창고 소진수량 마이너스감소
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			Table table = new Table(DateTime.Parse(m_InputList[4].ToString()).Year,DateTime.Parse(m_InputList[4].ToString()).Month);
			comm.Parameters.Add("@num",m_InputList[0].ToString());
			comm.Parameters.Add("@quantity",decimal.Parse(m_InputList[3].ToString()));
			comm.Parameters.Add("@cost",TotalCost(decimal.Parse(m_InputList[3].ToString()),m_InputList[0].ToString()));
			comm.Parameters.Add("@year",DateTime.Parse(m_InputList[4].ToString()).Year);
			comm.CommandText = table.OutRowTable();//원자재 창고 소진수량 감소
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_InputList[0].ToString();
				comm.Parameters.Add("@year",DateTime.Now.Year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = m_InputList[0].ToString();
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
					throw new Exception(ItemName + " 의 재고량 부족으로 출고가 불가능 합니다.");
				}
			}
		}

		// 매입매출테이블 수정함수
		private void BSI_MTUpdate(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			int year;
			int mon;

			
			string str = "";
			
			switch(m_PageName)
			{
				case "CollectMoneyRegistration": //수등록
					year = DateTime.Parse(m_InputList[20].ToString()).Year;
					mon = DateTime.Parse(m_InputList[20].ToString()).Month;
					Table table = new Table(year,mon);
					str = table.CollectMoneyDiminutionUnCollectMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[19].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@year",year);
					break;
				case "PaymentPlanResultRegistration":
					year = int.Parse(m_InputList[21].ToString());
					mon = int.Parse(m_InputList[22].ToString());
					Table table1 = new Table(year,mon);
					str = table1.UNCollectMoneyDiminutionPaymentMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[20].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@year",year);
					break;
				case "ClaimRegistration":	//크레임등록페이지
					year = DateTime.Parse(m_InputList[13].ToString()).Year;
					mon = DateTime.Parse(m_InputList[13].ToString()).Month;
					Table table2 = new Table(year,mon);
					str = table2.ClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_InputList[12].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@year",year);
					break;
				case "Claim":	//불량변상등록페이지
					year = DateTime.Parse(m_InputList[13].ToString()).Year;
					mon = DateTime.Parse(m_InputList[13].ToString()).Month;
					Table table3 = new Table(year,mon);
					str = table3.PayClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_InputList[12].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@year",year);
					break;
				case "EtcClaim":	//불량변상등록페이지
					year = DateTime.Parse(m_InputList[6].ToString()).Year;
					mon = DateTime.Parse(m_InputList[6].ToString()).Month;
					Table table4 = new Table(year,mon);
					str = table4.PayClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_InputList[5].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@year",year);
					break;
				default:
					break;
			}
			comm.CommandText = str;
			comm.ExecuteNonQuery();

			comm.Parameters.Clear();
			
			switch(m_PageName)
			{
					

				case "CollectMoneyRegistration": //수주등록
					year = DateTime.Parse(m_InputList[2].ToString()).Year;
					mon = DateTime.Parse(m_InputList[2].ToString()).Month;
					Table table4 = new Table(year,mon);
					str = table4.CollectMoneyIncreaseUnCollectMoneyDiminutionTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[3].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@year",year);					
					break;
				case "PaymentPlanResultRegistration":
					year = DateTime.Parse(m_InputList[2].ToString()).Year;
					mon = DateTime.Parse(m_InputList[2].ToString()).Month;
					Table table5 = new Table(year,mon);
					str = table5.UNCollectMoneyIncreasePaymentMoneyDiminutionTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[3].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@year",year);
					break;
				case "ClaimRegistration": //클레임등록
					year = DateTime.Parse(m_InputList[5].ToString()).Year;
					mon = DateTime.Parse(m_InputList[5].ToString()).Month;
					Table table6 = new Table(year,mon);
					str = table6.ClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@year",year);
					
					break;
				case "Claim":	//불량변상등록페이지
					year = DateTime.Parse(m_InputList[5].ToString()).Year;
					mon = DateTime.Parse(m_InputList[5].ToString()).Month;
					Table table7 = new Table(year,mon);
					str = table7.PayClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();
					comm.Parameters.Add("@year",year);
					break;
				case "EtcClaim":	//불량변상등록페이지
					year = DateTime.Parse(m_InputList[2].ToString()).Year;
					mon = DateTime.Parse(m_InputList[2].ToString()).Month;
					Table table3 = new Table(year,mon);
					str = table3.PayClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[4].ToString());
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
					comm.Parameters.Add("@year",year);
					break;
				default:
					break;
			}
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			
		}
		


		public void ReceiveDelete()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			DataSet ds = new DataSet();
			try
			{
				SqlCommand comm = new SqlCommand();
				comm.Connection = conn;
				comm.Transaction = tr;

			


				if(MonthClosing() == false)
				{
					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('마감되어 삭제할수 없습니다!');");
					hc.Response.Write("</script>");

				}
				else if(m_Index == "0")//레코드 등록여부
				{
					

					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('등록되지 않은 품목입니다.');");
					hc.Response.Write("</script>");
				}
				else
				{

					RowDelete(conn,tr);
				}

				tr.Commit();
			}
			catch(Exception ee)
			{
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
			
		}

		/// <summary>
		/// 삭제함수
		/// </summary>
		/// <returns></returns>
		public DataSet MainTableDelete()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			DataSet ds = new DataSet();
			try
			{
				SqlCommand comm = new SqlCommand();
				comm.Connection = conn;
				comm.Transaction = tr;

			
				string str = "";

				if(MonthClosing() == false)
				{
					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('마감되어 삭제할수 없습니다!');");
					hc.Response.Write("</script>");

					switch(m_PageName)
					{
						case "ReceiveingRegistration":		//수주등록 유효성 검사
						{
							str = @"SELECT isnull(PUC.SmallClassificationName,'') as ItemState,   II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName,
								II_MT.PropertyClassification, PUC_MT.SmallClassificationName as Unit, II_MT.Standard , 
								II_MT.Standard, RO_HT.CompanyName, RO_HT.BusinessRegistrationNum, CompanyPersonInCharge, CI_MT.TelephoneNum, 
								RO_HT.ReceivingOrderDate, 
								RO_HT.ApplyUnitCost, RO_HT.DeliveryRequestQuantity1, 
								CASE RO_HT.ProductionRequestDivision WHEN '1' THEN '예' ELSE '아니오' END AS ProductionRequestDivision,
								RO_HT.DeliveryRequestDate1, RO_HT.DeliveryRequestQuantity2, 
								RO_HT.DeliveryRequestDate2, RO_HT.DeliveryRequestQuantity3,
								RO_HT.DeliveryRequestDate3, RO_HT.DeliveryRequestQuantity4, 
								RO_HT.DeliveryRequestDate4, RO_HT.DeliveryRequestQuantity5,
								RO_HT.DeliveryRequestDate5, RO_HT.TotalReceiveingOrderQuantity,
								RO_HT.TotalCost, RO_HT.OutStorehouseQuantity, RO_HT.SuitabilityQuantity,
								RO_HT.UnInspectionQuantity, RO_HT.RemainderQuantity, RO_HT.OrderNum,
								RO_HT.DeliveryPlace, RO_HT.VolumNum, RO_HT.ProgressCondition,
								RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID,
								RO_HT.RegistrationDate, RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, 
								RO_HT.UpdatingDate, RO_HT.ReceivingOrderHistoryIndex 
								FROM RO_HT
								INNER JOIN II_MT ON RO_HT.ItemNum = II_MT.ItemNum
								Inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								INNER JOIN PUC_MT ON II_MT.Unit = PUC_MT.SmallClassificationCode
								left outer join PUC_MT PUC on II_MT.ItemState = PUC.SmallClassificationCode
								
								Where CI_MT.RecodingState = 1 and II_MT.RecodingState = '1' and RO_HT.RegistrationDate = @today and RO_HT.ProgressCondition = '대기'";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "CollectMoneyRegistration":	//수금등록 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "PaymentPlanResultRegistration":	//지급등록 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today  ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "GoodsBuyingRequestAdd":			//상품구매의뢰 유효성 검사
						{
							str = @"Select SmallClassificationName as ItemState, BR_HT.* 
								From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and II_MT.PropertyClassification = '상품' and ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null ";
							//str = "Select * From BR_HT where PropertyClassification = '상품' and ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "RowMaterialRequriementAdd":		//원자재구매의뢰 유효성 검사
						{
							//str = "Select * From BR_HT where PropertyClassification = '원자재' and ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null ";
							str = "Select BR_HT.*, PUC_MT.SmallClassificationName as Unit From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode where PUC_MT.RecodingState =1 and II_MT.RecodingState = 1 and II_MT.PropertyClassification = '원자재' and BR_HT.ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "BuyingOrderAdd":					//구매발주추가 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "ProductionRequestAdd":		//생산의뢰추가 유효성 검사
						{
							str = @"Select SmallClassificationName as ItemState, PR_HT.* 
								From PR_HT inner join II_MT on PR_HT.ItemNum = II_MT.ItemNum 
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PR_HT.RegistrationDate = @today and volumnum is null ";
							//str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "ProductionPlanAdd":		//생산계획추가 유효성 검사
						{
							str = @"Select SmallClassificationName as ItemState, PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanHistorySourceCode,
								ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate, DeliveryDate,
								case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation,
								VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID,
								PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate,
								HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT
								inner join II_MT on PP_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PP_HT.RegistrationDate = @today and volumnum is null ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "ClaimRegistration":		//클레임등록 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "Claim":		//지급 클레임등록 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "SubBuyingOrder":		//부자재 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and  RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}	
						case "RowItemExhaust":			//원자재 소진 페이지
						{
							str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							E_HT.RegistrationDate = @today order by ExhaustHistoryIndex desc";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "EtcClaim":		//지급 클레임등록 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						default :
							break;

					}

				}
				else if(m_Index == "0")//레코드 등록여부
				{
					switch(m_PageName)
					{
						case "ReceiveingRegistration":		//수주등록 유효성 검사
						{
							str = @"SELECT isnull(PUC.SmallClassificationName,'') as ItemState,   II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName,
							II_MT.PropertyClassification, PUC_MT.SmallClassificationName as Unit, II_MT.Standard , 
							II_MT.Standard, RO_HT.CompanyName, RO_HT.BusinessRegistrationNum, CompanyPersonInCharge, CI_MT.TelephoneNum, 
							RO_HT.ReceivingOrderDate, 
							RO_HT.ApplyUnitCost, RO_HT.DeliveryRequestQuantity1, 
							CASE RO_HT.ProductionRequestDivision WHEN '1' THEN '예' ELSE '아니오' END AS ProductionRequestDivision,
							RO_HT.DeliveryRequestDate1, RO_HT.DeliveryRequestQuantity2, 
							RO_HT.DeliveryRequestDate2, RO_HT.DeliveryRequestQuantity3,
							RO_HT.DeliveryRequestDate3, RO_HT.DeliveryRequestQuantity4, 
							RO_HT.DeliveryRequestDate4, RO_HT.DeliveryRequestQuantity5,
							RO_HT.DeliveryRequestDate5, RO_HT.TotalReceiveingOrderQuantity,
							RO_HT.TotalCost, RO_HT.OutStorehouseQuantity, RO_HT.SuitabilityQuantity,
							RO_HT.UnInspectionQuantity, RO_HT.RemainderQuantity, RO_HT.OrderNum,
							RO_HT.DeliveryPlace, RO_HT.VolumNum, RO_HT.ProgressCondition,
							RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID,
							RO_HT.RegistrationDate, RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, 
							RO_HT.UpdatingDate, RO_HT.ReceivingOrderHistoryIndex 
							FROM RO_HT
							INNER JOIN II_MT ON RO_HT.ItemNum = II_MT.ItemNum
							Inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
							INNER JOIN PUC_MT ON II_MT.Unit = PUC_MT.SmallClassificationCode
							left outer join PUC_MT PUC on II_MT.ItemState = PUC.SmallClassificationCode
							
							Where CI_MT.RecodingState = 1 and II_MT.RecodingState = '1' and RO_HT.RegistrationDate = @today and RO_HT.ProgressCondition = '대기'";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "CollectMoneyRegistration":	//수금등록 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "PaymentPlanResultRegistration":	//지급등록 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "GoodsBuyingRequestAdd":			//상품구매의뢰 유효성 검사
						{
							str = @"Select SmallClassificationName as ItemState, BR_HT.* 
								From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and II_MT.PropertyClassification = '상품' and ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null ";
							//str = "Select * From BR_HT where PropertyClassification = '상품' and ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "RowMaterialRequriementAdd":		//원자재구매의뢰 유효성 검사
						{
							//str = "Select * From BR_HT where PropertyClassification = '원자재' and ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null ";
							str = "Select BR_HT.*, PUC_MT.SmallClassificationName as Unit From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode where PUC_MT.RecodingState =1 and II_MT.RecodingState = 1 and II_MT.PropertyClassification = '원자재' and BR_HT.ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "BuyingOrderAdd":					//구매발주추가 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "ProductionRequestAdd":		//생산의뢰추가 유효성 검사
						{
							str = @"Select SmallClassificationName as ItemState, PR_HT.* 
								From PR_HT inner join II_MT on PR_HT.ItemNum = II_MT.ItemNum 
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PR_HT.RegistrationDate = @today and volumnum is null ";
							//str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "ProductionPlanAdd":		//생산계획추가 유효성 검사
						{
							str = @"Select SmallClassificationName as ItemState, PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanHistorySourceCode,
								ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate,DeliveryDate,
								case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation,
								VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID,
								PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate,
								HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT
								inner join II_MT on PP_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PP_HT.RegistrationDate = @today and volumnum is null ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "ClaimRegistration":		//클레임등록 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "Claim":		//지급 클레임등록 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "SubBuyingOrder":		//부자재 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and  RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						case "EtcClaim":		//지급 클레임등록 유효성 검사
						{
							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
							comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							break;
						}
						default :
							break;

					}

					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('등록되지 않은 품목입니다.');");
					hc.Response.Write("</script>");
				}
				else
				{
					if(m_Index == "0")//레코드 등록여부
					{
						switch(m_PageName)
						{
							case "ReceiveingRegistration":		//수주등록 유효성 검사
							{
								str = @"SELECT isnull(PUC.SmallClassificationName,'') as ItemState,   II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName,
								II_MT.PropertyClassification, PUC_MT.SmallClassificationName as Unit, II_MT.Standard , 
								II_MT.Standard, RO_HT.CompanyName, RO_HT.BusinessRegistrationNum, CompanyPersonInCharge, CI_MT.TelephoneNum, 
								RO_HT.ReceivingOrderDate, 
								RO_HT.ApplyUnitCost, RO_HT.DeliveryRequestQuantity1, 
								CASE RO_HT.ProductionRequestDivision WHEN '1' THEN '예' ELSE '아니오' END AS ProductionRequestDivision,
								RO_HT.DeliveryRequestDate1, RO_HT.DeliveryRequestQuantity2, 
								RO_HT.DeliveryRequestDate2, RO_HT.DeliveryRequestQuantity3,
								RO_HT.DeliveryRequestDate3, RO_HT.DeliveryRequestQuantity4, 
								RO_HT.DeliveryRequestDate4, RO_HT.DeliveryRequestQuantity5,
								RO_HT.DeliveryRequestDate5, RO_HT.TotalReceiveingOrderQuantity,
								RO_HT.TotalCost, RO_HT.OutStorehouseQuantity, RO_HT.SuitabilityQuantity,
								RO_HT.UnInspectionQuantity, RO_HT.RemainderQuantity, RO_HT.OrderNum,
								RO_HT.DeliveryPlace, RO_HT.VolumNum, RO_HT.ProgressCondition,
								RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID,
								RO_HT.RegistrationDate, RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, 
								RO_HT.UpdatingDate, RO_HT.ReceivingOrderHistoryIndex 
								FROM RO_HT
								INNER JOIN II_MT ON RO_HT.ItemNum = II_MT.ItemNum
								Inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								INNER JOIN PUC_MT ON II_MT.Unit = PUC_MT.SmallClassificationCode
								left outer join PUC_MT PUC on II_MT.ItemState = PUC.SmallClassificationCode
								
								Where CI_MT.RecodingState = 1 and II_MT.RecodingState = '1' and RO_HT.RegistrationDate = @today and RO_HT.ProgressCondition = '대기'";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "CollectMoneyRegistration":	//수금등록 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "PaymentPlanResultRegistration":	//지급등록 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "GoodsBuyingRequestAdd":			//상품구매의뢰 유효성 검사
							{
								str = @"Select SmallClassificationName as ItemState, BR_HT.* 
								From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and II_MT.PropertyClassification = '상품' and ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null ";
								//str = "Select * From BR_HT where PropertyClassification = '상품' and ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "RowMaterialRequriementAdd":		//원자재구매의뢰 유효성 검사
							{
								//str = "Select * From BR_HT where PropertyClassification = '원자재' and ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null ";
								str = "Select BR_HT.*, PUC_MT.SmallClassificationName as Unit From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode where PUC_MT.RecodingState =1 and II_MT.RecodingState = 1 and II_MT.PropertyClassification = '원자재' and BR_HT.ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "BuyingOrderAdd":					//구매발주추가 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "ProductionRequestAdd":		//생산의뢰추가 유효성 검사
							{
								str = @"Select SmallClassificationName as ItemState, PR_HT.* 
								From PR_HT inner join II_MT on PR_HT.ItemNum = II_MT.ItemNum 
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PR_HT.RegistrationDate = @today and volumnum is null ";
								//str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "ProductionPlanAdd":		//생산계획추가 유효성 검사
							{
								str = @"Select SmallClassificationName as ItemState, PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanHistorySourceCode,
								ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate,DeliveryDate,
								case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation,
								VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID,
								PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate,
								HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT
								inner join II_MT on PP_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PP_HT.RegistrationDate = @today and volumnum is null ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "ClaimRegistration":		//클레임등록 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "Claim":		//지급 클레임등록 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "SubBuyingOrder":		//부자재 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and  RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}	
							case "EtcClaim":		//지급 클레임등록 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							default :
								break;

						}
						HttpContext hc = HttpContext.Current;
						hc.Response.Write("<script language=javascript>");
						hc.Response.Write("alert('등록되지 않은 품목입니다.');");
						hc.Response.Write("</script>");

					}
					else
					{

						RowDelete(conn,tr);
						switch(m_PageName)
						{
							case "ReceiveingRegistration":		//수주등록 유효성 검사
							{
								str = @"SELECT isnull(PUC.SmallClassificationName,'') as ItemState,   II_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName,
								II_MT.PropertyClassification, PUC_MT.SmallClassificationName as Unit, II_MT.Standard , 
								II_MT.Standard, RO_HT.CompanyName, RO_HT.BusinessRegistrationNum, CompanyPersonInCharge, CI_MT.TelephoneNum, 
								RO_HT.ReceivingOrderDate, 
								RO_HT.ApplyUnitCost, RO_HT.DeliveryRequestQuantity1, 
								CASE RO_HT.ProductionRequestDivision WHEN '1' THEN '예' ELSE '아니오' END AS ProductionRequestDivision,
								RO_HT.DeliveryRequestDate1, RO_HT.DeliveryRequestQuantity2, 
								RO_HT.DeliveryRequestDate2, RO_HT.DeliveryRequestQuantity3,
								RO_HT.DeliveryRequestDate3, RO_HT.DeliveryRequestQuantity4, 
								RO_HT.DeliveryRequestDate4, RO_HT.DeliveryRequestQuantity5,
								RO_HT.DeliveryRequestDate5, RO_HT.TotalReceiveingOrderQuantity,
								RO_HT.TotalCost, RO_HT.OutStorehouseQuantity, RO_HT.SuitabilityQuantity,
								RO_HT.UnInspectionQuantity, RO_HT.RemainderQuantity, RO_HT.OrderNum,
								RO_HT.DeliveryPlace, RO_HT.VolumNum, RO_HT.ProgressCondition,
								RO_HT.RegistrationPerson, RO_HT.RegistrationPersonID,
								RO_HT.RegistrationDate, RO_HT.UpdatingPerson, RO_HT.UpdatingPersonID, 
								RO_HT.UpdatingDate, RO_HT.ReceivingOrderHistoryIndex 
								FROM RO_HT
								INNER JOIN II_MT ON RO_HT.ItemNum = II_MT.ItemNum
								Inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								INNER JOIN PUC_MT ON II_MT.Unit = PUC_MT.SmallClassificationCode
								left outer join PUC_MT PUC on II_MT.ItemState = PUC.SmallClassificationCode
								
								Where CI_MT.RecodingState = 1 and II_MT.RecodingState = '1' and RO_HT.RegistrationDate = @today and RO_HT.ProgressCondition = '대기'";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "CollectMoneyRegistration":	//수금등록 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "PaymentPlanResultRegistration":	//지급등록 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "GoodsBuyingRequestAdd":			//상품구매의뢰 유효성 검사
							{
								str = @"Select SmallClassificationName as ItemState, BR_HT.* 
								From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and II_MT.PropertyClassification = '상품' and ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null";
								//str = "Select * From BR_HT where PropertyClassification = '상품' and ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "RowMaterialRequriementAdd":		//원자재구매의뢰 유효성 검사
							{
								//str = "Select * From BR_HT where PropertyClassification = '원자재' and ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
								str = "Select BR_HT.*, PUC_MT.SmallClassificationName as Unit From BR_HT inner join II_MT on BR_HT.ItemNum = II_MT.ItemNum inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode where PUC_MT.RecodingState =1 and II_MT.RecodingState = 1 and II_MT.PropertyClassification = '원자재' and BR_HT.ProgressCondition = '대기' and BR_HT.RegistrationDate = @today and volumnum is null";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "BuyingOrderAdd":					//구매발주추가 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "ProductionRequestAdd":		//생산의뢰추가 유효성 검사
							{
								str = @"Select SmallClassificationName as ItemState, PR_HT.* 
								From PR_HT inner join II_MT on PR_HT.ItemNum = II_MT.ItemNum 
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PR_HT.RegistrationDate = @today and volumnum is null";
								//str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and RegistrationDate = @today and volumnum is null";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "ProductionPlanAdd":		//생산계획추가 유효성 검사
							{
								str = @"Select SmallClassificationName as ItemState, PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanHistorySourceCode,
								ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate,DeliveryDate,
								case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation,
								VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID,
								PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate,
								HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT
								inner join II_MT on PP_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PP_HT.RegistrationDate = @today and volumnum is null";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "ClaimRegistration":		//클레임등록 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "Claim":		//지급 클레임등록 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "SubBuyingOrder":		//부자재 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where ProgressCondition = '대기' and  RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "RowItemExhaust":			//원자재 소진 페이지
							{
								str = @"Select E_HT.* , PUC.SmallClassificationName as ExhaustUnit, II.Standard as ExhaustStandard,
							PUC1.SmallClassificationName as CreateUnit, I.Standard as CreateStandard 
							From E_HT inner join II_MT II on E_HT.ExhaustItemNum = II.ItemNum
							inner join II_MT I on E_HT.CreateItemNum = I.ItemNum
							inner join PUC_MT PUC on II.Unit = PUC.SmallClassificationCode
							inner join PUC_MT PUC1 on I.Unit = PUC1.SmallClassificationCode
							Where I.RecodingState = 1 and II.RecodingState = 1 and PUC.RecodingState = 1 and PUC1.RecodingState = 1 and
							E_HT.RegistrationDate = @today order by ExhaustHistoryIndex desc";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							case "EtcClaim":		//지급 클레임등록 유효성 검사
							{
								str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today ";
								comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
								break;
							}
							default :
								break;

						}
//						if(m_PageName.ToString() == "CollectMoneyRegistration" || m_PageName.ToString() == "PaymentPlanResultRegistration" || m_PageName.ToString() == "ClaimRegistration" || m_PageName.ToString() == "Claim")
//							str = "Select * From "+m_Table.ToString()+ " where RegistrationDate = @today";						
//						else if(m_PageName == "ProductionPlanAdd")
//							str = @"Select SmallClassificationName as ItemState, PP_HT.ItemNum, PP_HT.ItemDrawNum, PP_HT.ItemName, ProductionPlanHistorySourceCode,
//								ProductionPlanHistorySource, ProductionPlanQuantity, ProductionBeginDate,
//								case RowMaterialCalculation when 1 then '산출' else '미산출' end as RowMaterialCalculation,
//								VolumNum, ProgressCondition, PP_HT.RegistrationPerson, PP_HT.RegistrationPersonID,
//								PP_HT.RegistrationDate, PP_HT.UpdatingPerson, PP_HT.UpdatingPersonID, PP_HT.UpdatingDate,
//								HistoryIndex, HistorySection, ProductionPlanHistoryIndex From PP_HT
//								inner join II_MT on PP_HT.ItemNum = II_MT.ItemNum
//								inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
//								Where II_MT.RecodingState = 1  and ProgressCondition = '대기' and PP_HT.RegistrationDate = @today and volumnum is null";
//						else
//							str = "Select SmallClassificationName as ItemState, "+m_Table+".* From "+m_Table+ " inner join II_MT on "+m_Table+".ItemNum = II_MT.ItemNum inner join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode	Where II_MT.RecodingState = 1  and  ProgressCondition = '대기' and "+m_Table+".RegistrationDate = @today and volumnum is null";
//						comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					}		
				}
				comm.CommandText = str;
				SqlDataAdapter da = new SqlDataAdapter(comm);
				
				da.Fill(ds);
				tr.Commit();
			}
			catch(Exception ee)
			{
				HttpContext hc = HttpContext.Current;
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('"+ee.Message+"');");
				hc.Response.Write("</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
			return ds;
		}

		/// <summary>
		/// 실제 삭제함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void RowDelete(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			string str = "";
					
			switch(m_PageName)
			{
				case "ReceiveingRegistration":		//수주등록페이지
					str = "Delete From RO_HT where ReceivingOrderHistoryIndex = @Index";
					break;
				case "CollectMoneyRegistration":	//수금등록페이지
					str = "Delete From CM_HT where CollectMoneyHistoryIndex = @Index";
					break;
				case "PaymentPlanResultRegistration":	//지급등록페이지
					str = "Delete From P_HT where PaymentHistoryIndex = @Index";
					break;
				case "GoodsBuyingRequestAdd":			//상품구매의뢰추가페이지
					str = "Delete From BR_HT where BuyingRequestHistoryIndex = @Index";
					break;
				case "RowMaterialRequriementAdd":		//원자재구매의뢰추가페이지
					str = "Delete From BR_HT where BuyingRequestHistoryIndex = @Index";
					break;
				case "BuyingOrderAdd":					//구매발주추가페이지
					str = "Delete From BO_HT where BuyingOrderHistoryIndex = @Index";
					break;
				case "ProductionRequestAdd":		//생산의뢰추가페이지
					str = "Delete From PR_HT where ProductionRequestHistoryIndex = @Index";
					break;
				case "ProductionPlanAdd":		//생산계획추가페이지
					str = "Delete From PP_HT where ProductionPlanHistoryIndex = @Index";
					break;
				case "ClaimRegistration":		//클레임등록
					str = "Delete From CL_HT where ClameHistoryIndex = @Index";
					break;
				case "Claim":		//클레임등록
					str = "Delete From PCL_HT where ClameHistoryIndex = @Index";
					break;
				case "SubBuyingOrder":		//부자재 유효성 검사
					str = "Delete From SBO_HT where SubBuyingOrderHistoryIndex = @Index";
					break;
				case "RowItemExhaust":
					str = "Delete From E_HT where ExhaustHistoryIndex = @Index";
					break;
				case "EtcClaim":		//클레임등록
					str = "Delete From ECL_HT where EtcClameHistoryIndex = @Index";
					break;
				default:
					
					break;
			}
					
			comm.CommandText = str;
			comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Index.ToString());
			comm.ExecuteNonQuery();

			comm.Parameters.Clear();
				
			switch(m_PageName)
			{
					

				case "CollectMoneyRegistration":	//수금등록페이지
				{
					int year = DateTime.Parse(m_InputList[2].ToString()).Year;
					int mon = DateTime.Parse(m_InputList[2].ToString()).Month;
					Table table = new Table(year,mon);			//수금일의 월
					str = table.CollectMoneyDiminutionUnCollectMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[1].ToString());//수금액
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();				//거래처사업자등록번호
					comm.Parameters.Add("@year",year);
					comm.CommandText = str;
					comm.ExecuteNonQuery();
				}
					break;	
				case "PaymentPlanResultRegistration":	//지급등록페이지
				{
					int year = DateTime.Parse(m_InputList[2].ToString()).Year;
					int mon = DateTime.Parse(m_InputList[3].ToString()).Month;
					Table table1 = new Table(year,mon);			//지급일의 월
					str = table1.UNCollectMoneyDiminutionPaymentMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[1].ToString());//지급액
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();				//거래처사업자등록번호
					comm.Parameters.Add("@year",year);
					comm.CommandText = str;
					comm.ExecuteNonQuery();
				}
					break;
				case "ClaimRegistration":	//크레임등록페이지
				{
					int year = DateTime.Parse(m_InputList[13].ToString()).Year;
					int mon = DateTime.Parse(m_InputList[13].ToString()).Month;
					Table table2 = new Table(year,mon);				//접수일의 월
					str = table2.ClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_InputList[12].ToString());//클레임금액
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();				//거래처사업자등록번호
					comm.Parameters.Add("@year",year);
					comm.CommandText = str;
					comm.ExecuteNonQuery();
				}
					break;
				case "Claim":	//지급 크레임등록페이지
				{
					int year = DateTime.Parse(m_InputList[13].ToString()).Year;
					int mon = DateTime.Parse(m_InputList[13].ToString()).Month;
					Table table3 = new Table(year,mon);						//접수일의 월
					str = table3.PayClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_InputList[12].ToString());//클레임금액
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[4].ToString();				//거래처사업자등록번호
					comm.Parameters.Add("@year",year);
					comm.CommandText = str;
					comm.ExecuteNonQuery();
				}
					break;
				case "RowItemExhaust":		// 원자재 소진 페이지
				{
					Table table1 = new Table(int.Parse(m_InputList[10].ToString()),int.Parse(m_InputList[11].ToString()));
					comm.Parameters.Add("@num",m_InputList[0].ToString());
					comm.Parameters.Add("@quantity",-decimal.Parse(m_InputList[9].ToString()));
					comm.Parameters.Add("@cost",TotalCost(-decimal.Parse(m_InputList[9].ToString()),m_InputList[0].ToString()));
					comm.Parameters.Add("@year",int.Parse(m_InputList[10].ToString()));
					comm.CommandText = table1.OutRowTable();//원자재 창고 소진수량 마이너스감소
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}
					break;
				case "EtcClaim":	//지급 크레임등록페이지
				{
					int year = DateTime.Parse(m_InputList[6].ToString()).Year;
					int mon = DateTime.Parse(m_InputList[6].ToString()).Month;
					Table table3 = new Table(year,mon);						//접수일의 월
					str = table3.PayClaimMoneyIncreaseTable();
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -decimal.Parse(m_InputList[5].ToString());//클레임금액
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();				//거래처사업자등록번호
					comm.Parameters.Add("@year",year);
					comm.CommandText = str;
					comm.ExecuteNonQuery();
				}
					break;
				default:
					break;
			}
			
			HttpContext hc = HttpContext.Current;
			hc.Response.Write("<script language=javascript>");
			hc.Response.Write("alert('삭제 되었습니다.');");
			hc.Response.Write("</script>");
			
		}

		private bool Validated(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			string str = "";
					
			if(m_PageName == "CollectMoneyRegistration" || m_PageName == "PaymentPlanResultRegistration" || m_PageName == "ClaimRegistration" || m_PageName == "Claim" || m_PageName == "EtcClaim")
				return true;
			else
			{ 
				str = @"select count(*) From II_MT Where RecodingState = 1 and ItemNum = @itemnum and ItemDrawNum = @itemdrawnum and ItemName = @itemname";

				comm.Parameters.Add("@itemnum",SqlDbType.NVarChar).Value = m_InputList[0].ToString();
				comm.Parameters.Add("@itemdrawnum",SqlDbType.NVarChar).Value = m_InputList[1].ToString();
				comm.Parameters.Add("@itemname",SqlDbType.NVarChar).Value = m_InputList[2].ToString();
				comm.CommandText = str;
				
				int Exist = int.Parse(comm.ExecuteScalar().ToString());

				if(Exist == 0)
					throw new Exception("품목을 정확히 입력하세요!");
				comm.Parameters.Clear();

				if(m_PageName == "ReceiveingRegistration")
				{
					if(m_InputList[25].ToString().Trim() != "")
					{
						//발주번호가 동일한 것이 있는지 파악
						comm.CommandText = @"Select count(OrderNum) From RO_HT where OrderNum = @OrderNum and ItemNum = @ItemNum";
						comm.Parameters.Add("@OrderNum", m_InputList[25].ToString().Trim());
						comm.Parameters.Add("@ItemNum", m_InputList[0].ToString().Trim());
						Exist = int.Parse(comm.ExecuteScalar().ToString());
						comm.Parameters.Clear();

						if(Exist != 0)
							throw new Exception("동일한 발주번호가 존재합니다!");
					}
				}
			}
			return true;
			
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


		private decimal TotalCost(decimal Quantity, string ItemNum)
		{
			decimal aa = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select StandardUnitCost From II_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();
			conn.Close();

			return (aa * Quantity);
		}	
	

		/// <summary>
		/// 공통소진원 인덱스 구하는 함수
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <returns></returns>
		private int MaxE_HT(SqlConnection conn, SqlTransaction tr)
		{
			string str  = "Select Max(ExhaustHistoryIndex) From E_HT";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			int max = int.Parse(comm.ExecuteScalar().ToString());
			return max;
		}
	}
}
