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
using Infragistics.WebUI.WebCombo;
using Infragistics.WebUI.WebSchedule;

namespace mlim_ERP
{
	/// <summary>
	/// BusinessRegister에 대한 요약 설명입니다.
	/// </summary>
	public class BusinessRegister
	{
		private string m_PageName;		//요청페이지 담는 변수
		private string m_UserID;		//사용자 아이디 저장
		private string m_UserName;		//사용자 이름 저장
		private int m_Index=0;			//레코드 인덱스값 저장
		private string m_Distinction;	//업무구분(모듈구분) 월마감에 사용되어진다
		private UltraWebGrid m_grid;	//웹그리드 저장
		private ArrayList m_InputList = new ArrayList();
		


		/// <summary>
		/// 수주등록, 제품선납품 등록 생성자
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="InputList"></param>
		/// <param name="UserID"></param>
		/// <param name="UserName"></param>
		public BusinessRegister(string PageName, ArrayList InputList, string UserID , string UserName)
		{
			m_PageName = PageName;
			m_InputList = InputList;
			m_UserID = UserID;
			m_UserName = UserName;

			m_Distinction = "영업";
		}

		/// <summary>
		/// 수주등록 페이지 수정 생성자
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="InputList"></param>
		/// <param name="UserID"></param>
		/// <param name="UserName"></param>
		/// <param name="Index"></param>
		public BusinessRegister(string PageName, ArrayList InputList, string UserID , string UserName, int Index)
		{
			m_PageName = PageName;
			m_InputList = InputList;
			m_UserID = UserID;
			m_UserName = UserName;
			m_Distinction = "영업";	
			m_Index = Index;
		}

		/// <summary>
		/// 수주등록 페이지 삭제 생성자
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="Index"></param>
		public BusinessRegister(string PageName,int Index)
		{
			m_PageName = PageName;
			m_Distinction = "영업";	
			m_Index = Index;
		}

		/// <summary>
		/// 제품출고 페이지 생성자
		/// </summary>
		/// <param name="PageName"></param>
		/// <param name="grid"></param>
		/// <param name="list"></param>
		/// <param name="UserName"></param>
		/// <param name="UserID"></param>
		public BusinessRegister(string PageName, UltraWebGrid grid, ArrayList list, string UserName, string UserID)
		{
			m_PageName = PageName;
			m_Distinction = "영업";	
			m_grid = grid;
			m_InputList = list;
			m_UserID = UserID;
			m_UserName = UserName;
			m_Index = int.Parse(list[4].ToString());
		}


		/// <summary>
		/// 수주등록 페이지 등록 함수
		/// </summary>
		/// <returns></returns>
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
					str = @"Select  RO_HT.*, TelephoneNum, CompanyPersonInCharge, SmallClassificationName as Unit , Standard  From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode
								inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								Where II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and ProgressCondition != '완료' and RO_HT.RegistrationDate = @today ";
					comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					
				}
				else
				{
					if(Validated(conn,tr))
					{
						if(ReceiveingRegistrationValidate() == true)
						{
							int index = Regist(conn,tr);		// 수주원장 등록
							Relation(conn,tr, index);			// 생산의뢰원장, 상품의뢰원장 등록 등록-수주원장 진행상태 변경(진행)
								
						}
					}
					str = @"Select  RO_HT.*, TelephoneNum, CompanyPersonInCharge, SmallClassificationName as Unit , Standard  From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode
								inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								Where II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and ProgressCondition != '완료' and RO_HT.RegistrationDate = @today ";
					comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
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
		/// 수주원장 기록함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		private int Regist(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			string 	str = "Insert into RO_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,PropertyClassification,ProductionRequestDivision,ReceivingOrderDate,"+
				"ApplyUnitCost,DeliveryRequestQuantity1,DeliveryRequestDate1,DeliveryRequestQuantity2,DeliveryRequestDate2,DeliveryRequestQuantity3,"+
				"DeliveryRequestDate3,DeliveryRequestQuantity4,DeliveryRequestDate4,DeliveryRequestQuantity5,DeliveryRequestDate5,TotalReceiveingOrderQuantity,"+
				"TotalCost,OutStorehouseQuantity,SuitabilityQuantity,UnInspectionQuantity,RemainderQuantity,OrderNum,DeliveryPlace,ProgressCondition,"+
				"RegistrationPerson,RegistrationPersonID,RegistrationDate) values("+
				"@itemnum, @itemdrawnum,@itemname,@com,@comnum,@classification,@division,@receivingdate,@cost,@quantity1,@date1,@quantity2,@date2,@quantity3,@date3,"+
				"@quantity4,@date4,@quantity5,@date5,@total,@totalcost,@storequantity,@suitquantity,@unquantity,@remainquantity,@ordernum,@place,'대기',"+
				"@Person, @PersonID, @Date)";

			comm.Parameters.Add("@itemnum",m_InputList[0].ToString());
			comm.Parameters.Add("@itemdrawnum", m_InputList[1].ToString());
			comm.Parameters.Add("@itemname", m_InputList[2].ToString());
			comm.Parameters.Add("@com", m_InputList[3].ToString());
			comm.Parameters.Add("@comnum",m_InputList[4].ToString());
			comm.Parameters.Add("@classification",m_InputList[5].ToString());
			if(m_InputList[5].ToString().Trim() == "상품")
				comm.Parameters.Add("@division","0");
			else
				comm.Parameters.Add("@division",1);

			comm.Parameters.Add("@receivingdate", DateTime.Parse(m_InputList[6].ToString()).ToShortDateString());
			comm.Parameters.Add("@cost",decimal.Parse(m_InputList[7].ToString()));

			comm.Parameters.Add("@quantity1", decimal.Parse(m_InputList[8].ToString()));
			comm.Parameters.Add("@date1",DateTime.Parse(m_InputList[9].ToString()).ToShortDateString());
			comm.Parameters.Add("@quantity2", "0");
			comm.Parameters.Add("@date2", Convert.DBNull);
			comm.Parameters.Add("@quantity3", "0");
			comm.Parameters.Add("@date3",Convert.DBNull);
			comm.Parameters.Add("@quantity4", "0");
			comm.Parameters.Add("@date4",Convert.DBNull);
			comm.Parameters.Add("@quantity5", "0");
			comm.Parameters.Add("@date5",Convert.DBNull);
			comm.Parameters.Add("@total", decimal.Parse(m_InputList[8].ToString()));
			comm.Parameters.Add("@totalcost",decimal.Parse(m_InputList[10].ToString()));
			comm.Parameters.Add("@storequantity","0");
			comm.Parameters.Add("@suitquantity","0");
			comm.Parameters.Add("@unquantity","0");
			comm.Parameters.Add("@remainquantity",decimal.Parse(m_InputList[8].ToString()));
			comm.Parameters.Add("@ordernum",m_InputList[11].ToString());
			comm.Parameters.Add("@place", m_InputList[12].ToString());
			comm.Parameters.Add("@Person",m_UserName.ToString());
			comm.Parameters.Add("@PersonID", m_UserID.ToString());
			comm.Parameters.Add("@Date",DateTime.Now.ToShortDateString());
			comm.CommandText = str;
			comm.ExecuteNonQuery();

			comm.Parameters.Clear();
			
			str = @"Select Max(ReceivingOrderHistoryIndex) From RO_HT";
			comm.CommandText = str;

			return int.Parse(comm.ExecuteScalar().ToString());

		}

		
		/// <summary>
		/// 수주원장 등록후 자산분류에 따라 생산의뢰원장과 구매의뢰원장에 기록
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void Relation(SqlConnection con, SqlTransaction trans,int count)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			string str = "";
			if(m_InputList[5].ToString() =="제품")
			{
				str="Insert into PR_HT (ItemNum,ItemDrawNum,ItemName,CompanyName,BusinessRegistrationNum,ProductionRequestSourceCode,ProductionRequestSource,"+
					"PropertyClassification,ProductionRequestQuantity,RequestQuantity1,RequestDate1,RequestQuantity2,"+
					"RequestDate2,RequestQuantity3,RequestDate3,RequestQuantity4,RequestDate4,RequestQuantity5,RequestDate5,ApplyUnitCost,"+
					"VolumNum,ProgressCondition, RegistrationPerson,RegistrationPersonID, RegistrationDate,ReceivingOrderHistoryIndex) values("+
					"@itemnum, @itemdrawnum,@itemname,@com, @comnum,@sourcecode,@source,@classification,@total,@quantity1,@date1,@quantity2,@date2,"+
					"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@cost,@num,'대기', @Person, @PersonID, @Date,@idx)";

				comm.Parameters.Add("@itemnum",m_InputList[0].ToString());
				comm.Parameters.Add("@itemdrawnum",m_InputList[1].ToString());
				comm.Parameters.Add("@itemname",m_InputList[2].ToString());
				comm.Parameters.Add("@com",m_InputList[3].ToString());
				comm.Parameters.Add("@comnum",m_InputList[4].ToString());
				comm.Parameters.Add("@sourcecode","03100010");
				comm.Parameters.Add("@source", "정상");
				comm.Parameters.Add("@classification",m_InputList[5].ToString());
				comm.Parameters.Add("@total", decimal.Parse(m_InputList[8].ToString()));
			
				comm.Parameters.Add("@quantity1", decimal.Parse(m_InputList[8].ToString()));
				comm.Parameters.Add("@date1",DateTime.Parse(m_InputList[9].ToString()).ToShortDateString());
				
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = 0;
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = 0;
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = 0;
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = 0;
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
				comm.Parameters.Add("@cost",decimal.Parse(m_InputList[7].ToString()));
			
				comm.Parameters.Add("@num",Convert.DBNull);
				comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
				comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserName.ToString();				
				comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
				comm.Parameters.Add("@idx",count);
			}
			else
			{
				str="Insert into BR_HT (ItemNum,ItemDrawNum,ItemName,PropertyClassification,BuyingRequestSourceCode,BuyingRequestSource,"+
					"FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,"+
					"ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,"+
					"FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,ApplyUnitCost,TotalCost,VolumNum,ProgressCondition,"+
					"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
					"@itemnum, @itemdrawnum,@itemname,@classification,@sourcecode,@source,@quantity1,@date1,@quantity2,@date2,"+
					"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,@num,'대기', @Person, @PersonID, @Date,@idx,@section)";

				comm.Parameters.Add("@itemnum",m_InputList[0].ToString());
				comm.Parameters.Add("@itemdrawnum",m_InputList[1].ToString());
				comm.Parameters.Add("@itemname",m_InputList[2].ToString());
				comm.Parameters.Add("@sourcecode","03300010");
				comm.Parameters.Add("@source", "정상");
				comm.Parameters.Add("@classification",m_InputList[5].ToString());
				comm.Parameters.Add("@total", decimal.Parse(m_InputList[8].ToString()));
				comm.Parameters.Add("@cost", decimal.Parse(m_InputList[7].ToString()));
				comm.Parameters.Add("@totalcost", decimal.Parse(m_InputList[10].ToString()));
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
				comm.Parameters.Add("@quantity2","0");
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
				comm.Parameters.Add("@quantity3","0");
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
				comm.Parameters.Add("@quantity4","0");
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
				comm.Parameters.Add("@quantity5","0");
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;					
				comm.Parameters.Add("@num",Convert.DBNull);
				comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
				comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();				
				comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
				comm.Parameters.Add("@idx",count);
				comm.Parameters.Add("@section",SqlDbType.VarChar).Value = "수주";
			}
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//수주원장 진행상태 변경
			str = @"Update RO_HT Set ProgressCondition ='진행' where ReceivingOrderHistoryIndex = @idx";
			comm.CommandText =str;
			comm.Parameters.Add("@idx",count);
			comm.ExecuteNonQuery();

		}



		/// <summary>
		/// 수주등록 페이지 수정함수
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
					str = @"Select  RO_HT.*, TelephoneNum, CompanyPersonInCharge, SmallClassificationName as Unit , Standard  From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode
								inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								Where II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and ProgressCondition = '대기' and RO_HT.RegistrationDate = @today ";
					comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					
				}
				else
				{
					if(Validated(conn,tr))
					{
						if(ReceiveingRegistrationValidate() == true)
						{
							Update(conn,tr);		// 수주원장 수정
							RelationUpdate(conn,tr);			// 생산의뢰원장, 상품의뢰원장 등록 등록-수주원장 진행상태 변경(진행)
								
						}
					}
					str = @"Select  RO_HT.*, TelephoneNum, CompanyPersonInCharge, SmallClassificationName as Unit , Standard  From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode
								inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								Where II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and ProgressCondition != '완료' and RO_HT.RegistrationDate = @today ";
					comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
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
		/// 수주원장 수정함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void Update(SqlConnection con, SqlTransaction trans)
		{
			string Classification = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			string str1 = @"Select PropertyClassification From RO_HT where ReceivingOrderHistoryIndex = @Index";
			comm.CommandText = str1;
			comm.Parameters.Add("@Index",m_Index);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Classification = dr["PropertyClassification"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();

			string str = "";
			if(Classification == "제품")
			{
				if(Classification != m_InputList[5].ToString())
				{
					str = "Delete From PR_HT Where ReceivingOrderHistoryIndex = @Index";
					comm.CommandText = str;
					comm.Parameters.Add("@Index",m_Index);
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					str="Insert into BR_HT (ItemNum,ItemDrawNum,ItemName,PropertyClassification,BuyingRequestSourceCode,BuyingRequestSource,"+
						"FirstDeliveryDemandQuantity,FirstDeliveryDemandDate,SecondDeliveryDemandQuantity,SecondDeliveryDemandDate,"+
						"ThirdDeliveryDemandQuantity,ThirdDeliveryDemandDate,FourthDeliveryDemandQuantity,FourthDeliveryDemandDate,"+
						"FifthDeliveryDemandQuantity,FifthDeliveryDemandDate,OrderQuantity,ApplyUnitCost,TotalCost,VolumNum,ProgressCondition,"+
						"RegistrationPerson,RegistrationPersonID,RegistrationDate,HistoryIndex,HistorySection) values("+
						"@itemnum, @itemdrawnum,@itemname,@classification,@sourcecode,@source,@quantity1,@date1,@quantity2,@date2,"+
						"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@total,@cost,@totalcost,@num,'대기', @Person, @PersonID, @Date,@idx,@section)";

					comm.Parameters.Add("@itemnum",m_InputList[0].ToString());
					comm.Parameters.Add("@itemdrawnum",m_InputList[1].ToString());
					comm.Parameters.Add("@itemname",m_InputList[2].ToString());
					comm.Parameters.Add("@sourcecode","03300010");
					comm.Parameters.Add("@source", "정상");
					comm.Parameters.Add("@classification",m_InputList[5].ToString());
					comm.Parameters.Add("@total", decimal.Parse(m_InputList[8].ToString()));
					comm.Parameters.Add("@cost", decimal.Parse(m_InputList[7].ToString()));
					comm.Parameters.Add("@totalcost", decimal.Parse(m_InputList[10].ToString()));
					comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
					comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
					comm.Parameters.Add("@quantity2","0");
					comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					comm.Parameters.Add("@quantity3","0");
					comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					comm.Parameters.Add("@quantity4","0");
					comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					comm.Parameters.Add("@quantity5","0");
					comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;					
					comm.Parameters.Add("@num",Convert.DBNull);
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();				
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@idx",m_Index);
					comm.Parameters.Add("@section",SqlDbType.VarChar).Value = "수주";

					comm.CommandText = str;
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}	
				
			}
			else
			{
				if(Classification != m_InputList[5].ToString())
				{
					str = "Delete From BR_HT Where Index = @Index and HistorySection = '수주'";
					comm.CommandText = str;
					comm.Parameters.Add("@Index",m_Index);
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					str="Insert into PR_HT (ItemNum,ItemDrawNum,ItemName,CompanyName, BusinessRegistrationNum,ProductionRequestSourceCode,ProductionRequestSource,"+
						"PropertyClassification,ProductionRequestQuantity,RequestQuantity1,RequestDate1,RequestQuantity2,"+
						"RequestDate2,RequestQuantity3,RequestDate3,RequestQuantity4,RequestDate4,RequestQuantity5,RequestDate5,ApplyUnitCost,"+
						"VolumNum,ProgressCondition, RegistrationPerson,RegistrationPersonID, RegistrationDate,ReceivingOrderHistoryIndex) values("+
						"@itemnum, @itemdrawnum,@itemname,@com, @conmum,@sourcecode,@source,@classification,@total,@quantity1,@date1,@quantity2,@date2,"+
						"@quantity3,@date3,@quantity4,@date4,@quantity5,@date5,@cost,@num,'대기', @Person, @PersonID, @Date,@idx)";

					comm.Parameters.Add("@itemnum",m_InputList[0].ToString());
					comm.Parameters.Add("@itemdrawnum",m_InputList[1].ToString());
					comm.Parameters.Add("@itemname",m_InputList[2].ToString());
					comm.Parameters.Add("@com",m_InputList[3].ToString());
					comm.Parameters.Add("@comnum",m_InputList[4].ToString());
					comm.Parameters.Add("@sourcecode","03100010");
					comm.Parameters.Add("@source", "정상");
					comm.Parameters.Add("@classification",m_InputList[5].ToString());
					comm.Parameters.Add("@total", decimal.Parse(m_InputList[8].ToString()));
			
					comm.Parameters.Add("@quantity1", decimal.Parse(m_InputList[8].ToString()));
					comm.Parameters.Add("@date1", DateTime.Parse(m_InputList[9].ToString()).ToShortDateString());
					comm.Parameters.Add("@quantity2","0");
					comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					comm.Parameters.Add("@quantity3","0");
					comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					comm.Parameters.Add("@quantity4","0");
					comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
					comm.Parameters.Add("@quantity5","0");
					comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;	
					comm.Parameters.Add("@cost",decimal.Parse(m_InputList[7].ToString()));
			
					comm.Parameters.Add("@num",Convert.DBNull);
					comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
					comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserName.ToString();				
					comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					comm.Parameters.Add("@idx",m_Index);

					comm.CommandText = str;
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}
				
			}

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
			if(m_InputList[5].ToString() == "상품")
				comm.Parameters.Add("@division","0");
			else
				comm.Parameters.Add("@division",1);
			comm.Parameters.Add("@receivingdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[6].ToString()).ToShortDateString();
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());

			comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
			comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
			comm.Parameters.Add("@quantity2","0");
			comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
			comm.Parameters.Add("@quantity3","0");
			comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
			comm.Parameters.Add("@quantity4","0");
			comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
			comm.Parameters.Add("@quantity5","0");
			comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
			
			comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[10].ToString());
			comm.Parameters.Add("@storequantity","0");
			comm.Parameters.Add("@suitquantity","0");
			comm.Parameters.Add("@unquantity","0");
			comm.Parameters.Add("@remainquantity",decimal.Parse(m_InputList[10].ToString()));
			comm.Parameters.Add("@ordernum",m_InputList[11].ToString());
			comm.Parameters.Add("@place",m_InputList[12].ToString());
			comm.Parameters.Add("@Person", m_UserName.ToString());
			comm.Parameters.Add("@PersonID", m_UserID.ToString());				
			comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());
			comm.Parameters.Add("@Index",m_Index);
			comm.CommandText = str;
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 수주원장 수정시 동시에 수정되는 생산의뢰원장 혹은 구매의뢰원장 수정
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void RelationUpdate(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			string str = "";

			if(m_InputList[5].ToString() =="제품")
			{
				str = "Update PR_HT set ItemNum=@itemnum,ItemDrawNum=@itemdrawnum,ItemName=@itemname,"+
					"CompanyName = @com, BusinessRegistrationNum = @comnum,"+
					"PropertyClassification=@classification, ApplyUnitCost=@cost,"+
					"RequestQuantity1=@quantity1,RequestDate1=@date1,RequestQuantity2=@quantity2,RequestDate2=@date2,"+
					"RequestQuantity3=@quantity3,RequestDate3=@date3,RequestQuantity4=@quantity4,RequestDate4=@date4,"+
					"RequestQuantity5=@quantity5,RequestDate5=@date5,ProductionRequestQuantity=@total, "+
					"UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date Where ReceivingOrderHistoryIndex = @Index";

				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
				comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
				comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
				comm.Parameters.Add("@com",m_InputList[3].ToString());
				comm.Parameters.Add("@comnum",m_InputList[4].ToString());
				comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_InputList[5].ToString();
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());

				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
				comm.Parameters.Add("@quantity2","0");
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
				comm.Parameters.Add("@quantity3","0");
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
				comm.Parameters.Add("@quantity4","0");
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
				comm.Parameters.Add("@quantity5","0");
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
			
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
				comm.Parameters.Add("@Person", m_UserName.ToString());
				comm.Parameters.Add("@PersonID", m_UserID.ToString());				
				comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());
				comm.Parameters.Add("@Index",m_Index);
			}
			else
			{
				str = "Update BR_HT set ItemNum=@itemnum,ItemDrawNum=@itemdrawnum,ItemName=@itemname, "+
					"PropertyClassification=@classification, ApplyUnitCost=@cost,"+
					"FirstDeliveryDemandQuantity=@quantity1,FirstDeliveryDemandDate1=@date1,SecondDeliveryDemandQuantity=@quantity2,SecondDeliveryDemandDate=@date2,"+
					"ThirdDeliveryDemandQuantity=@quantity3,SecondDeliveryDemandDate=@date3,FourthDeliveryDemandQuantity=@quantity4,SecondDeliveryDemandRequestDate=@date4,"+
					"FifthDeliveryDemandQuantity=@quantity5,SecondDeliveryDemandDate=@date5,OrderQuantity=@total,ApplyUnitCost = @cost, TotalCost=@totalcost,"+
					"UpdatingPerson=@Person,UpdatingPersonID=@PersonID,UpdatingDate=@Date Where HistoryIndex = @Index and HistorySection = '수주'";

				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_InputList[0].ToString();
				comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_InputList[1].ToString();
				comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_InputList[2].ToString();
				comm.Parameters.Add("@classification",SqlDbType.VarChar).Value = m_InputList[5].ToString();
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[7].ToString());
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
				comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[9].ToString()).ToShortDateString();
				comm.Parameters.Add("@quantity2","0");
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
				comm.Parameters.Add("@quantity3","0");
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
				comm.Parameters.Add("@quantity4","0");
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
				comm.Parameters.Add("@quantity5","0");
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull ;
				comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[8].ToString());
				comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[10].ToString());
				comm.Parameters.Add("@ordernum",m_InputList[11].ToString());
				comm.Parameters.Add("@place",m_InputList[12].ToString());
				comm.Parameters.Add("@Person", m_UserName.ToString());
				comm.Parameters.Add("@PersonID", m_UserID.ToString());				
				comm.Parameters.Add("@Date", DateTime.Now.ToShortDateString());
				comm.Parameters.Add("@Index",m_Index);
			}
			comm.CommandText = str;
			comm.ExecuteNonQuery();
		}



		/// <summary>
		/// 수주등록 페이지 삭제함수
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
					str = @"Select  RO_HT.*, TelephoneNum, CompanyPersonInCharge, SmallClassificationName as Unit , Standard  From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode
								inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								Where II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and ProgressCondition = '대기' and RO_HT.RegistrationDate = @today ";
					comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
					
				}
				else
				{
					
					Delete(conn,tr);					// 수주원장 삭제
					RelationDelete(conn,tr);			// 생산의뢰원장, 상품의뢰원장 삭제
					
					str = @"Select  RO_HT.*, TelephoneNum, CompanyPersonInCharge, SmallClassificationName as Unit , Standard  From RO_HT inner join II_MT on RO_HT.ItemNum = II_MT.ItemNum
								inner join PUC_MT on II_MT.Unit = PUC_MT.SmallClassificationCode
								inner join CI_MT on RO_HT.BusinessRegistrationNum = CI_MT.BusinessRegistrationNum
								Where II_MT.RecodingState = 1 and CI_MT.RecodingState = 1 and ProgressCondition != '완료' and RO_HT.RegistrationDate = @today ";
					comm.Parameters.Add("@today",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
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
		/// 수주원장 삭제함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void Delete(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			string str = @"Delete From RO_HT where  ReceivingOrderHistoryIndex = @index";
			comm.CommandText =str;
			comm.Parameters.Add("@index",m_Index);
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 수주원장 삭제시 동시에 삭제되는 생산의뢰 상품의뢰원장
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void RelationDelete(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			string str = @"Delete From BR_HT where  HistoryIndex = @index and HistorySection = '수주'";
			comm.CommandText =str;
			comm.Parameters.Add("@index",m_Index);
			comm.ExecuteNonQuery();

			str = @"Delete From PR_HT where ReceivingOrderHistoryIndex = @index";
			comm.CommandText =str;
			comm.ExecuteNonQuery();
            
		}
		
		/// <summary>
		/// 수주등록 유효성 검사 함수
		/// </summary>
		/// <returns></returns>
		private bool ReceiveingRegistrationValidate()
		{
			HttpContext hc = HttpContext.Current;

			if(m_InputList[3].ToString() == null ||m_InputList[3].ToString().Trim() =="" )
			{
				hc.Response.Write("<script language='JavaScript'>");
				hc.Response.Write("alert('거래처를 선택해 주세요.');");
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
		/// 품목을 정확히 입력했는지 판단하는 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <returns></returns>
		private bool Validated(SqlConnection con, SqlTransaction trans)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			string str = "";
					
			str = @"select count(*) From II_MT Where RecodingState = 1 and ItemNum = @itemnum and ItemDrawNum = @itemdrawnum and ItemName = @itemname";

			comm.Parameters.Add("@itemnum",SqlDbType.NVarChar).Value = m_InputList[0].ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.NVarChar).Value = m_InputList[1].ToString();
			comm.Parameters.Add("@itemname",SqlDbType.NVarChar).Value = m_InputList[2].ToString();
			comm.CommandText = str;
				
			int Exist = int.Parse(comm.ExecuteScalar().ToString());

			if(Exist == 0)
				throw new Exception("품목을 정확히 입력하세요!");
			
			return true;
			
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
			else
			{
				if(month < int.Parse(DateTime.Now.ToShortDateString().Substring(5,2).Trim()))
					return true;
				else
					return false;
				
			}		
		}



		/// <summary>
		/// 제품출고 페이지 납품함수
		/// </summary>
		public void RowRegistration()
		{
//			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
//			conn.Open();
//			SqlTransaction tr = conn.BeginTransaction();
//			DataSet ds = new DataSet();
//			try
//			{
//				SqlCommand comm = new SqlCommand();
//				comm.Connection = conn;
//				comm.Transaction = tr;
//
//				SaleRegist(conn,tr,m_Index);						// 납품원장 등록
//				SaleReceivingOrderUpdate(conn,tr, m_Index);			// 수주원장 수량변경
//				S_HTTableUpdate(conn,tr, m_Index);					// 창고수량 변경
//				S_HTBuyingSaleTableUpdate(conn,tr, m_Index);			// 매입매출 테이블 금액 변경
//				
//				tr.Commit();
//			}
//			catch(Exception ee)
//			{
//				HttpContext hc = HttpContext.Current;
//				hc.Response.Write("<script language='JavaScript'>");
//				hc.Response.Write("alert('"+ee.Message+"');");
//				hc.Response.Write("</script>");
//				tr.Rollback();
//			}
//			finally
//			{
//				conn.Close();
//			}
//			 return ds;
		 }

		/// <summary>
		/// 납품원장 등록 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void SaleRegist(SqlConnection con, SqlTransaction trans,int count)
		{
			string str= "Insert into S_HT (ItemNum, ItemDrawNum, ItemName, CompanyName, BusinessRegistrationNum, OutStorehouseQuantity, SuitabilityQuantity, ApplyUnitCost, TotalCost, SupplementaryValueTaxRate,SaleDate,RegistrationPerson, RegistrationPersonID, RegistrationDate, ReceivingOrderHistoryIndex) values ("+
				"@itemnum, @itemdrawnum,@itemname,@com,@businessNum,@quantity,@suitabilityquantity, @applyunitcost, @totalcost, @tax, @SaleDate,"+
				"@Person, @PersonID, @Date, @index1)";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@itemdrawnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemDrawNum").Value.ToString();
			comm.Parameters.Add("@itemname",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemName").Value.ToString();
			comm.Parameters.Add("@com",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("CompanyName").Value.ToString();//거래처명
			comm.Parameters.Add("@businessNum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("BusinessRegistrationNum").Value.ToString();//사업자등록번호
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[0].ToString());//출고수량
			comm.Parameters.Add("@suitabilityquantity",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[0].ToString());//합격수량
			comm.Parameters.Add("@applyunitcost",SqlDbType.Decimal).Value = decimal.Parse(m_grid.Rows[count].Cells.FromKey("ApplyUnitCost").Value.ToString());//적용단가  
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[1].ToString()) * decimal.Parse(m_grid.Rows[count].Cells.FromKey("ApplyUnitCost").Value.ToString());//총금액
			comm.Parameters.Add("@tax",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[3].ToString());//부가세율
			comm.Parameters.Add("@SaleDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_InputList[2].ToString());//매출일자
			comm.Parameters.Add("@Person",SqlDbType.VarChar).Value = m_UserName.ToString();
			comm.Parameters.Add("@PersonID",SqlDbType.VarChar).Value = m_UserID.ToString();				
			comm.Parameters.Add("@Date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@index1",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 수주원장 수량변경
		/// </summary>
		/// <param name="count"></param>
		private void SaleReceivingOrderUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			//수주원장에는 누적시켜야 하므로 일단 
			//수주원장에 있는 수량들을 가지고 와야 한다.
			decimal quantity1=0;//합격수량
			decimal quantity2=0;//미검수량

			string str_RO= "Select * From RO_HT Where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm_RO = new SqlCommand(str_RO,conn);
			comm_RO.Transaction = tr;
			comm_RO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			SqlDataReader dr = comm_RO.ExecuteReader();
			while(dr.Read())
			{
				quantity1 = decimal.Parse(dr["SuitabilityQuantity"].ToString());
				quantity2 = decimal.Parse(dr["UnInspectionQuantity"].ToString());
			}
			dr.Close();
			comm_RO.Parameters.Clear();
			


			//실제 수주원장에 수량 업데이트
			//잔량이 0 이하면 진행상태를 완료로 바꾼다.

			string str = "Update RO_HT Set SuitabilityQuantity = SuitabilityQuantity + @quantity1, RemainderQuantity= RemainderQuantity - @quantity1 Where ReceivingOrderHistoryIndex = @index";

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_InputList[0].ToString());//매출원장 합격수량
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			// 업데이트 한 뒤 잔량이 0이하이면 진행상태를 완료로 바꾼다.
			decimal quantity = 0;
			str = "Select RemainderQuantity From RO_HT where ReceivingOrderHistoryIndex = @index";
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_grid.Rows[count].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장번호
			comm.CommandText = str;
			SqlDataReader dr_RO = comm.ExecuteReader();
			while(dr_RO.Read())
			{
				quantity = decimal.Parse(dr_RO["RemainderQuantity"].ToString());
			}
			dr_RO.Close();
			
			if(quantity <= 0)
			{
				str = "Update RO_HT Set ProgressCondition = '완료' Where ReceivingOrderHistoryIndex = @index";
				comm.CommandText = str;
				comm.ExecuteNonQuery();
			}
			comm.Parameters.Clear();
		}


		/// <summary>
		/// 매출원장 등록후 영업창고에 출고수량 증가
		/// </summary>
		/// <param name="count"></param>
		private void S_HTTableUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,conn);
			comm_cost.Transaction = tr;
			comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			int year = DateTime.Parse(m_InputList[2].ToString()).Year;
			int mon = DateTime.Parse(m_InputList[2].ToString()).Month;

			comm.Parameters.Add("@quantity", decimal.Parse(m_InputList[0].ToString()));//납품창고에는 합격이든 불합격이든 검사수량 전량을 떨어준다
			comm.Parameters.Add("@num", m_grid.Rows[count].Cells.FromKey("ItemNum").Text);
			comm.Parameters.Add("@cost",  decimal.Parse(m_InputList[0].ToString()) * cost);
			comm.Parameters.Add("@year",year);
			comm.Parameters.Add("@storenum", int.Parse(m_InputList[1].ToString()));
			
			Table table = new Table(year,mon);
			comm.CommandText= table.OutBusinessTable();
			comm.ExecuteNonQuery();
		}


		/// <summary>
		/// 매출원장 등록후 매입매출테이블에 매출금액 증가
		/// </summary>
		/// <param name="count"></param>
		private void S_HTBuyingSaleTableUpdate(SqlConnection conn, SqlTransaction tr, int count)
		{
			int year = DateTime.Parse(m_InputList[2].ToString()).Year;
			int mon = DateTime.Parse(m_InputList[2].ToString()).Month;


			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_grid.Rows[count].Cells.FromKey("BusinessRegistrationNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_InputList[0].ToString()) *decimal.Parse(m_grid.Rows[count].Cells.FromKey("ApplyUnitCost").Text) ;//총금액
			comm.Parameters.Add("@year",year);
			
			Table table = new Table(year,mon);
			comm.CommandText = table.CollectMoneyIncreaseTable();
			comm.ExecuteNonQuery();
		}
	}
}
