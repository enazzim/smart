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
	/// 수정을 위한 객체
	/// </summary>
	public class Update
	{
		private string m_Page;//요청페이지 담는 필드
		private ArrayList m_List;//업데이트 할 값을 담을 ArrayList
		private UltraWebGrid m_Grid; //요청그리드
		private string UserName;
		private string UserID;
		
		public Update(string user)
		{
			UserID = user;

			// 사용자 이름을 입력
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = UserID.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				UserName = dr["Name"].ToString();
			}
			conn.Close();


		}
		

		/// <summary>
		/// 수주현황페이지 생성자
		/// 영업창고입고현황페이지 생성자
		/// 창고이동현황페이지 생성자
		/// 보용품 입고현황페이지 생성자
		/// </summary>
		/// <param name="RequestPage">요청페이지</param>
		/// <param name="list">업데이트할 필드 맨마지막에는 그리드 인덱스값을 넣어둔다</param>
		/// <param name="grid">요청페이지의 그리드</param>
		public Update(string RequestPage, ArrayList list, UltraWebGrid grid,string user)
		{
			m_Page = RequestPage;
			m_List = list;
			m_Grid = grid;
			UserID = user;

			// 사용자 이름을 입력
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Name From UI_MT Where RecodingState = 1 and ID = @id";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@id",SqlDbType.VarChar).Value = UserID.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				UserName = dr["Name"].ToString();
			}
			conn.Close();

		}

				
		/////////////////////////////////////////////////////////////////
		//
		//  수정창에 데이터 채우기위한 속성
		//
		//
		//////////////////////////////////////////////////////////////////
		



		//////////////////////////////////////////////////////////////////////////////////
		// 구매외주관리에서 사용
		/////////////////////////////////////////////////////////////////////////////////
		
		public DataSet dsBank()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='0920'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("bank");
			da.Fill(ds.Tables["bank"]);
			DataRow row = table.NewRow();
			row["SmallClassificationCode"]="";
			row["SmallClassificationName"]="-선 택-";
			table.Rows.InsertAt(row,0);
			
			conn.Close();
			
			return ds;
		}


		public DataSet dsDecision()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "select * from PUC_MT where SmallClassificationName != '' and Recodingstate = 1 and LargeClassificationCode = '0910'";
			SqlCommand comm = new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			conn.Close();

			return ds;
		}


		//////////////////////////////////////////////////////////////////////////////////
		// 구매외주관리에서 사용
		/////////////////////////////////////////////////////////////////////////////////

















		/// <summary>
		/// 입출고사유를 위한 코드 등록
		/// </summary>
		/// <returns></returns>
		public DataSet Store()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='1500'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			
			

			conn.Close();


			
			return ds;

		}

		/// <summary>
		/// 보용품입고사유를 위한 코드 등록
		/// </summary>
		/// <returns></returns>
		public DataSet AddItemStore()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='1700'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);;

			return ds;

		}


		/// <summary>
		/// 구매의뢰원천 코드 등록
		/// </summary>
		/// <returns></returns>
		public DataSet Source()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='0330'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			
			conn.Close();
			
			return ds;
		}

		/// <summary>
		/// 생산의뢰원천 코드 등록
		/// </summary>
		/// <returns></returns>
		public DataSet ProSource()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='0310'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
					
			conn.Close();
			
			return ds;
		}


		/// <summary>
		/// 결재은행
		/// </summary>
		/// <returns></returns>
		public DataSet Bank()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='0920'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("bank");
			da.Fill(ds.Tables["bank"]);
			DataRow row = table.NewRow();
			row["SmallClassificationCode"]="";
			row["SmallClassificationName"]="-선 택-";
			table.Rows.InsertAt(row,0);
			
			conn.Close();
			
			return ds;
		}

		/// <summary>
		/// 결재방법
		/// </summary>
		/// <returns></returns>
		public DataSet Method()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='0910'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			
			conn.Close();
			
			return ds;
		}



		/// <summary>
		/// 부적합원인
		/// </summary>
		/// <returns></returns>
		public DataSet UnSuitabilityCause()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='1010'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("Cause");
			da.Fill(ds.Tables["Cause"]);
			DataRow row = table.NewRow();
			row["SmallClassificationCode"]="";
			row["SmallClassificationName"]="-선 택-";
			table.Rows.InsertAt(row,0);
			
			conn.Close();
			
			return ds;
		}

		/// <summary>
		/// 부적합현상
		/// </summary>
		/// <returns></returns>
		public DataSet UnSuitabilityStatus()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='1000'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("Status");
			da.Fill(ds.Tables["Status"]);
			DataRow row = table.NewRow();
			row["SmallClassificationCode"]="";
			row["SmallClassificationName"]="-선 택-";
			table.Rows.InsertAt(row,0);
			
			conn.Close();
			
			return ds;
		}


		/// <summary>
		/// 기타구매 코드 등록
		/// </summary>
		/// <returns></returns>
		public DataSet EtcBuy()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='1800'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			
			conn.Close();
			
			return ds;
		}


		/// <summary>
		/// 클레임원인
		/// </summary>
		/// <returns></returns>
		public DataSet ClaimCause()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='1010'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("Cause");
			da.Fill(ds.Tables["Cause"]);
			conn.Close();
			
			return ds;
		}

		/// <summary>
		/// 클레임현상
		/// </summary>
		/// <returns></returns>
		public DataSet ClaimStatus()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='1000'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("Status");
			da.Fill(ds.Tables["Status"]);
			conn.Close();
			
			return ds;
		}



		/// <summary>
		/// 검사판정
		/// </summary>
		/// <returns></returns>
		public DataSet InspectionDecision()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='1310'";
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("Decision");
			da.Fill(ds.Tables["Decision"]);
			DataRow row = table.NewRow();
			row["SmallClassificationCode"]="";
			row["SmallClassificationName"]="-선 택-";
			table.Rows.InsertAt(row,0);
			
			conn.Close();
			
			return ds;
		}

		/// <summary>
		/// 비작업사유
		/// </summary>
		/// <returns></returns>
		public DataSet NonTime()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "select SmallClassificationCode, SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode='1210'";
			SqlCommand comm = new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			DataTable table = ds.Tables.Add("NonTime");
			da.Fill(ds.Tables["NonTime"]);
			DataRow row = table.NewRow();
			row["SmallClassificationCode"]="";
			row["SmallClassificationName"]="-선 택-";
			table.Rows.InsertAt(row,0);

			conn.Close();
			
			return ds;
		}

		/// <summary>
		/// 사용공구
		/// </summary>
		/// <returns></returns>
		public DataSet Tool()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200010' order by EquipmentName" ;
			SqlCommand comm = new SqlCommand(str,conn);
			SqlDataAdapter da = new SqlDataAdapter(comm) ;
			DataSet ds = new DataSet() ;
			DataTable table = ds.Tables.Add("Tool");
			da.Fill(ds.Tables["Tool"]);
			DataRow row = table.NewRow();
			row["EquipmentNum"]="";
			row["EquipmentName"]="-선 택-";
			table.Rows.InsertAt(row,0);

			conn.Close();
			
			return ds;
		}

		/// <summary>
		/// 사용치구
		/// </summary>
		/// <returns></returns>
		public DataSet Jig()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200020' order by EquipmentName" ;
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("Jig");
			da.Fill(ds.Tables["Jig"]);
			DataRow row = table.NewRow();
			row["EquipmentNum"]="";
			row["EquipmentName"]="-선 택-";
			table.Rows.InsertAt(row,0);
			
			conn.Close();
			
			return ds;
		}


		/// <summary>
		/// 작업자
		/// </summary>
		/// <returns></returns>
		public DataSet Worker()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select Name as Worker, ID as WorkerID from UI_MT where RecodingState = 1 and BusinessRegistrationNum = @num and [ID] != 'admin' order by Name" ;
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = "";
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("Worker");
			da.Fill(ds.Tables["Worker"]);
			conn.Close();
			
			return ds;
		}

		/// <summary>
		/// 작업장
		/// </summary>
		/// <returns></returns>
		public DataSet WCName()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "Select WCName, WCInfoIndex from WCI_MT where RecodingState = 1" ;
			SqlCommand comm = new SqlCommand(str, conn);
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			DataTable table = ds.Tables.Add("WCName");
			da.Fill(ds.Tables["WCName"]);
			DataRow row = table.NewRow();
			conn.Close();
			
			return ds;
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
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "영업";
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



		/// <summary>
		/// 수정 함수
		/// </summary>
		public void MainTableUpdate()
		{
			HttpContext hc = HttpContext.Current;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();

			try
			{

				if(MonthClosing() == false)
				{
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('월마감이되어 등록이 불가능합니다.');");
					hc.Response.Write("</script>");
				}
				else
				{
					switch(m_Page)
					{
						case "ReceiveingPC":		//수주등록현황 유효성 검사
						{
							if(ReceiveingPCValidate(int.Parse(m_List[m_List.Count-1].ToString())) == true)//일차납입수량,일차납기요구일, 수주일자, 적용단가 를 검사해서 null이면 입력하지 않는다
							{
								//수주원장변경
								RO_HTUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//그리드의 RowIndex

								hc.Response.Write("<script language=javascript>");
								//hc.Response.Write("window.status='수정 되었습니다!';");
								hc.Response.Write("alert('수정 되었습니다.');");
								hc.Response.Write("</script>");
							}
						}
							break;
						case "CollectMoneyRegistrationPC":	//수금현황 유효성 검사
						{
							CollectMoneyRegistrationUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString())); // 수금원장 업데이트
							BSI_MTTableUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));				//매입매출테이블업데이트

							hc.Response.Write("<script language=javascript>");
							//hc.Response.Write("window.status='수정 되었습니다!';");
							hc.Response.Write("alert('수정 되었습니다.');");
							hc.Response.Write("</script>");
					
						}
							break;
						case "BusinessStorehouseInStorehousePC":	//영업창고 입고원장 수정
						{
						
							//입고원장 변경
							IS_HTUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));
							//입고의뢰 원장 수정
							ISR_HTUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));
							//품질검사 원장 수정
							//QI_HTUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));
							//창고수량변경
							IS_HTTableUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));

							hc.Response.Write("<script language=javascript>");
							//hc.Response.Write("window.status='수정 되었습니다!';");
							hc.Response.Write("alert('수정 되었습니다.');");
							hc.Response.Write("</script>");
						}
							break;
						case "GoodsBuyingRequestPC":			//상품구매의뢰현황 유효성 검사
						{
							if(GoodsBuyingRequestPCValidate(int.Parse(m_List[m_List.Count-1].ToString()))==true)
							{
								GoodsBuyingRequestPCUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));

								hc.Response.Write("<script language=javascript>");
								//hc.Response.Write("window.status='수정 되었습니다!';");
								hc.Response.Write("alert('수정 되었습니다.');");
								hc.Response.Write("</script>");
							}
						}
							break;
						case "GoodsManufactureOutStorehousePC":		//상품제품출고현황 유효성 검사
						{
							if(GoodsManufactureOutStorehousePCValidate(int.Parse(m_List[m_List.Count-1].ToString()))==true)
							{
								if(m_Grid.Rows[int.Parse(m_List[m_List.Count-1].ToString())].Cells.FromKey("ReceivingOrderHistoryIndex").Value != null)
								{
									GoodsManufactureOutStorehouseRO_Update(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//수주원장 미검수량 변경
								}
								GoodsManufactureOutStorehousePCUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));// 상품제품출고원장변경
								GoodsManufactureOutStorehousePCHistory(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));// 이력원장 수정

								if(StoreManage(conn, tr, m_Grid.Rows[int.Parse(m_List[m_List.Count-1].ToString())].Cells.FromKey("ItemNum").Value.ToString()) == "아니오")
								{
									GoodsManufactureOutStorehousePCSubHistory(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));// 이력원장 수정
								}
								//하위품목 삭제후 추가

								OS_HTTableUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//창고테이블 수량변경
								hc.Response.Write("<script language=javascript>");
								//hc.Response.Write("window.status='수정 되었습니다!';");
								hc.Response.Write("alert('수정 되었습니다.');");
								hc.Response.Write("</script>");
							}
						}
							break;
						case "SaleHistoryRegistrationPC":					//매출원장현황 유효성 검사
						{
							if(SaleHistoryRegistrationPCValidate(int.Parse(m_List[m_List.Count-1].ToString()))==true)
							{
								SaleHistoryRegistrationPCUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//매출원장변경
								SaleHistoryRegistrationS_HTUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//매입매출테이블금액수정
								SaleHistoryRegistrationOS_HTUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//출고원장 수량변경
								if(m_Grid.Rows[int.Parse(m_List[m_List.Count-1].ToString())].Cells.FromKey("ReceivingOrderHistoryIndex").Value != null)
								{
									SaleHistoryRegistrationRO_HTUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//수주원장 수량변경
								}

								hc.Response.Write("<script language=javascript>");
								//hc.Response.Write("window.status='수정 되었습니다!';");
								hc.Response.Write("alert('수정 되었습니다.');");
								hc.Response.Write("</script>");
							}
						}
							break;
						case "ProductionRequestPC":		//생산의뢰현황 유효성 검사(추가생산의뢰된것만 가능)
						{
							if(ProductionRequestPCValidate(int.Parse(m_List[m_List.Count-1].ToString()))==true)
							{
								ProductionRequestPCUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//생산의뢰원장 변경
							
								hc.Response.Write("<script language=javascript>");
								//hc.Response.Write("window.status='수정 되었습니다!';");
								hc.Response.Write("alert('수정 되었습니다.');");
								hc.Response.Write("</script>");
							}
						
						}
							break;
						case "StorehouseMovingPC":		//창고이동현황 유효성 검사
						{
							StorehouseMovingUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));
							BS_MTTableUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));

							hc.Response.Write("<script language=javascript>");
							//hc.Response.Write("window.status='수정 되었습니다!';");
							hc.Response.Write("alert('수정 되었습니다.');");
							hc.Response.Write("</script>");
						}
							break;
						case "WCPlanPC":		//작업계획현황 
						{
							WDWPHTUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//그리드의 RowIndex

							hc.Response.Write("<script language=javascript>");
							//hc.Response.Write("window.status='수정 되었습니다!';");
							hc.Response.Write("alert('수정 되었습니다.');");
							hc.Response.Write("</script>");
							
						}
							break;
						case "WorkDailyReportRegistrationPC":		//작업일보등록현황
						{
							WorkDailyReportUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//작업일보원장변경
							//PP_HTUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));// 생산계획원장 수정
							//WDWP_HTUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));	//WC작업계획원장의 작업완료수량 변경
							//StoreTableUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//창고업데이트
							//WorkDailyReportUpdateHistory(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//이력원장 업데이트
							EI_MTMinusUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//사용공구치구변경시 누적샷변경
							EI_MTUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//사용공구치구변경시 누적샷변경

							hc.Response.Write("<script language=javascript>");
							//hc.Response.Write("window.status='수정 되었습니다!';");
							hc.Response.Write("alert('수정 되었습니다.');");
							hc.Response.Write("</script>");
						}
							break;
						case "OutsideRequestPC":		//외주의뢰현황
						{
							OutsideRequestUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//작업일보원장변경

							hc.Response.Write("<script language=javascript>");
							//hc.Response.Write("window.status='수정 되었습니다!';");
							hc.Response.Write("alert('수정 되었습니다.');");
							hc.Response.Write("</script>");
						}
							break;
						case "ClaimRegistrationPC":			//클레임등록현황
						{
							if(ClaimValidate(int.Parse(m_List[m_List.Count-1].ToString())))
							{
								CL_HTUpDate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));		//클레임원장변경
								BSI_MTTableUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));		//매출테이블금액변경
								hc.Response.Write("<script language=javascript>");
								//hc.Response.Write("window.status='수정 되었습니다!';");
								hc.Response.Write("alert('수정 되었습니다.');");
								hc.Response.Write("</script>");
							}
							break;
						}
						case "ClaimPC":			//클레임현황
						{
							if(ClaimValidate(int.Parse(m_List[m_List.Count-1].ToString())))
							{
								PCL_HTUpDate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));		//클레임원장변경
								BSI_MTTableUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));		//매출테이블금액변경
								hc.Response.Write("<script language=javascript>");
								//hc.Response.Write("window.status='수정 되었습니다!';");
								hc.Response.Write("alert('수정 되었습니다.');");
								hc.Response.Write("</script>");
							}
							break;
						}
						case "AddItemInStorePC":	//보용품입고현황
						{
							AddItemInStorePCUpdae(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//보용품입고원장 수정
							AddItemInStorePCHistory(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//보용품 입고량 수정시 이력원장 수정
							AddItemInStorePCTableUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));// 테이블의 수량 수정
							hc.Response.Write("<script language=javascript>");
							//hc.Response.Write("window.status='수정 되었습니다!';");
							hc.Response.Write("alert('수정 되었습니다.');");
							hc.Response.Write("</script>");
							break;
						}
						case "EtcClaimPC":			//클레임현황
						{
							if(EtcClaimValidate(int.Parse(m_List[m_List.Count-1].ToString())))
							{
								ECL_HTUpDate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));		//클레임원장변경
								BSI_MTTableUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));		//매출테이블금액변경
								hc.Response.Write("<script language=javascript>");
								hc.Response.Write("alert('수정 되었습니다.');");
								hc.Response.Write("</script>");
							}
							
						}
							break;
						default :		//기타입출고현황 유효성 검사
						{
							OtherInOutStorehousePCUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//기타입출고원장 수정
							OtherInOutStorehousePCHistory(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));//기타입출고 수정시 이력원장 수정
							OIOS_HTTableUpdate(conn,tr,int.Parse(m_List[m_List.Count-1].ToString()));// 창고테이블의 수량 수정

							hc.Response.Write("<script language=javascript>");
							//hc.Response.Write("window.status='수정 되었습니다!';");
							hc.Response.Write("alert('수정 되었습니다.');");
							hc.Response.Write("</script>");
							
						}
							break;
					}
				}
				tr.Commit();
			}
			catch(Exception ee)
			{
				hc.Response.Write("<script language=javascript>");
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
		/// 작업계획원장 수정함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="Index"></param>
		private void WDWPHTUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			string str=@"Update WDWP_HT set WorkPlanQuantity = @WorkPlanQuantity, DeliveryDate = @DeliveryDate, WCName = @WCName, WorkDistinction = @WorkDistinction,
						OrderLeadTime = @OrderLeadTime, WorkDate = @WorkDate, WorkDenotationDate = @WorkDenotationDate, 
						UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @date Where WCDailyWorkPlanHistoryIndex = @index";

			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@WorkPlanQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkPlanQuantity").Value.ToString());//작업계획수량
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text == "")
				comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Text);//납기요구일

			//comm.Parameters.Add("@DeliveryDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryDate").Value.ToString());//납기요구일
			if(m_Grid.Rows[rowcount].Cells.FromKey("WorkDistinction").Text == "외주")
				comm.Parameters.Add("@WCName",SqlDbType.VarChar).Value = "";//Convert.DBNull;//작업장명
			else
				comm.Parameters.Add("@WCName",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("WCName").Value.ToString();//작업장명
			comm.Parameters.Add("@WorkDistinction",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("WorkDistinction").Value.ToString();//작업구분
			comm.Parameters.Add("@OrderLeadTime",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OrderLeadTime").Value.ToString());//외주리드타임
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("WorkDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("WorkDate").Text == "")
				comm.Parameters.Add("@WorkDate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@WorkDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDate").Text);//작업시작일
			
			//comm.Parameters.Add("@WorkDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDate").Text);//작업시작일
			if(m_Grid.Rows[rowcount].Cells.FromKey("WorkDenotationDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("WorkDenotationDate").Text == "")
				comm.Parameters.Add("@WorkDenotationDate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@WorkDenotationDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDenotationDate").Text);//작업지시일
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = UserID;
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// WC작업계획원장 작업완료수량 업데이트 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="rowcount"></param>
		private void WDWP_HTUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			decimal CompletionQuantity = 0;
			decimal PlanyQuantity = 0;
			string Process = "";
			int index = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Value.ToString());
			string str = "select * From WDWP_HT Where WCDailyWorkPlanHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@idx",index);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				CompletionQuantity = decimal.Parse(dr["WorkCompletionQuantity"].ToString());
				PlanyQuantity = decimal.Parse(dr["WorkPlanQuantity"].ToString());
				
				Process = dr["ProgressCondition"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();


			//수정후의 금번작업완료수량과 수정전의 금번작업수량을 비교
			decimal difference = decimal.Parse(m_List[0].ToString()) - decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Value.ToString());

			// difference가 0이면  진행상태는 그대로이고 다르면 진행상태가 진행으로 변경된다.
			if(difference == 0)
				str = "UPDATE WDWP_HT SET WorkCompletionQuantity = @quan WHERE WCDailyWorkPlanHistoryIndex = @idx";
			else
				str = "UPDATE WDWP_HT SET WorkCompletionQuantity = @quan, ProgressCondition = '진행' WHERE WCDailyWorkPlanHistoryIndex = @idx";
		
			
			SqlCommand comm1 = new SqlCommand(str,con);
			comm1.Transaction = trans;
			comm1.Parameters.Add("@idx",index);

			decimal a = 0;
			if(Process == "완료" || (PlanyQuantity-CompletionQuantity >0))
				a = CompletionQuantity - difference;
			else
				a = CompletionQuantity + difference;

			comm1.Parameters.Add("@quan", SqlDbType.Decimal).Value = a;
			comm1.ExecuteNonQuery();
			comm1.Parameters.Clear();
			

			if(a == PlanyQuantity)
			{
				str = "UPDATE WDWP_HT SET ProgressCondition = '완료' WHERE WCDailyWorkPlanHistoryIndex = @idx";
				SqlCommand comm3 = new SqlCommand(str,con);
				comm3.Transaction = trans;
				comm3.Parameters.Add("@idx",index);
				comm3.ExecuteNonQuery();
				comm3.Parameters.Clear();
			}
			else if(a > PlanyQuantity)
			{
				decimal RemainQuantity = 0;
				str = "Select * From WDR_HT Where WorkDailyReportHistoryIndex = @index";
				SqlCommand comm4 = new SqlCommand(str,con);
				comm4.Transaction = trans;
				comm4.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
				SqlDataReader dr1 = comm4.ExecuteReader();
				while(dr1.Read())
				{
					RemainQuantity = decimal.Parse(dr1["RemainQuantity"].ToString());
				}
				dr1.Close();
				comm4.Parameters.Clear();

				if(RemainQuantity < 0)
				{
					if((CompletionQuantity - decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Value.ToString())) < 0)
						throw new Exception("입력하신 금번작업수량이 완료수량보다 큽니다!");
				}
			}
				

		}


		/// <summary>
		/// 기타공제 등록현황 유효성 검사
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private bool EtcClaimValidate(int rowcount)
		{
			HttpContext hc = HttpContext.Current;

			if(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Value.ToString().Trim() == "")//접수일
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('접수일을 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value.ToString().Trim() == "0" 
				|| m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value.ToString().Trim() == "" )//크레임금액
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('공제 금액을 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("EtcClaimRegion").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("EtcClaimRegion").Value.ToString().Trim() == "" )//기타공제사유
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('기타공제 사유를 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}			
			else
				return true;
		}


		/// <summary>
		/// 기타공제 수정함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="rowcount"></param>
		private void ECL_HTUpDate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			string str=@"Update ECL_HT set ReceiptDate = @receiptdate, EtcClaimRegion = @EtcClaimRegion, ClaimCost = @claimcost, 
						UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @date Where EtcClameHistoryIndex = @index";

			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@receiptDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Value.ToString()).ToShortDateString();
			comm.Parameters.Add("@EtcClaimRegion",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("EtcClaimRegion").Value.ToString();
			comm.Parameters.Add("@claimcost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value.ToString());
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = UserID;
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("EtcClameHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 클레임원장 업데이트
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="rowcount"></param>
		private void CL_HTUpDate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			string str=@"Update CL_HT set ReceiptDate = @receiptdate, ClaimQuantity = @claimquantity, ClaimCost = @claimcost, ClaimStatusMeaning = @claimstatusmeaning,
						ClaimStatusCode = @claimstatuscode, ClaimCauseMeaning = @claimcausemeaning, ClaimCauseCode = @claimcausecode, 
						UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @date Where ClameHistoryIndex = @index";

			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@receiptDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Value.ToString()).ToShortDateString();
			comm.Parameters.Add("@claimquantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClaimQuantity").Value.ToString());
			comm.Parameters.Add("@claimcost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value.ToString());
			comm.Parameters.Add("@claimstatusmeaning",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ClaimStatusMeaning").Value.ToString();
			comm.Parameters.Add("@claimstatuscode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ClaimStatusCode").Value.ToString();
			comm.Parameters.Add("@claimcausemeaning",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ClaimCauseMeaning").Value.ToString();
			comm.Parameters.Add("@claimcausecode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ClaimCauseCode").Value.ToString();
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = UserID;
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClameHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
		}


		

		/// <summary>
		/// 클레임원장 유효성 검사
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private bool ClaimValidate(int rowcount)
		{
			HttpContext hc = HttpContext.Current;

			if(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Value.ToString().Trim() == "")//접수일
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('접수일을 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value.ToString().Trim() == "0" 
				|| m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value.ToString().Trim() == "" )//크레임금액
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('클레임금액을 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("ClaimQuantity").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("ClaimQuantity").Value.ToString().Trim() == "" 
				|| m_Grid.Rows[rowcount].Cells.FromKey("ClaimQuantity").Value.ToString().Trim() == "0")//클레임수량
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('클레임 수량을 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("ClaimStatusCode").Value == null ||m_Grid.Rows[rowcount].Cells.FromKey("ClaimStatusCode").Value.ToString().Trim() == "" )//클레임현상
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('클레임현상을 선택해 주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCauseCode").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("ClaimCauseCode").Value.ToString().Trim() == "")//클레임원인
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('클레임원인을 선택해 주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else
				return true;
		}

		private void PCL_HTUpDate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			string str=@"Update PCL_HT set ReceiptDate = @receiptdate, ClaimQuantity = @claimquantity, ClaimCost = @claimcost, ClaimStatusMeaning = @claimstatusmeaning,
						ClaimStatusCode = @claimstatuscode, ClaimCauseMeaning = @claimcausemeaning, ClaimCauseCode = @claimcausecode, 
						UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @date Where ClameHistoryIndex = @index";

			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@receiptDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Value.ToString()).ToShortDateString();
			comm.Parameters.Add("@claimquantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClaimQuantity").Value.ToString());
			comm.Parameters.Add("@claimcost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value.ToString());
			comm.Parameters.Add("@claimstatusmeaning",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ClaimStatusMeaning").Value.ToString();
			comm.Parameters.Add("@claimstatuscode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ClaimStatusCode").Value.ToString();
			comm.Parameters.Add("@claimcausemeaning",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ClaimCauseMeaning").Value.ToString();
			comm.Parameters.Add("@claimcausecode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ClaimCauseCode").Value.ToString();
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = UserID;
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@Index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClameHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 수주원장변경 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool ReceiveingPCValidate(int rowcount)
		{
			HttpContext hc = HttpContext.Current;

			if(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderDate").Value.ToString().Trim() == "" || m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderDate").Value.ToString().Trim() == null)//수주일자
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('수주일자를 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString().Trim() == "" || m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString().Trim() == "0" )//적용단가
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('적용단가를 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Value.ToString().Trim() == "" || m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Value.ToString().Trim() == null 
				|| m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Value.ToString().Trim() == "0")//1차납기요구량
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('1차납기요구량을 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value.ToString().Trim() == "" || m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value.ToString().Trim() == null)//1차납기요구일
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('1차납기요구일을 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("ProgressCondition").Value.ToString().Trim() != "대기")
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('진행상태가 대기인 항목만 수정이 가능합니다!');");
				hc.Response.Write("</script>");
				return false;
			}
			else
				return true;
		}

		/// <summary>
		/// 수주원장변경함수
		/// 수주원장은 수량과 일자만 변경이 가능하다.
		/// </summary>
		/// <param name="rowcount"></param>
		private void RO_HTUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			string str = @" UPDATE RO_HT SET ReceivingOrderDate = @receiving, ApplyUnitCost = @cost,
				DeliveryRequestQuantity1 = @quantity1, DeliveryRequestDate1 = @date1, 
				DeliveryRequestQuantity2 = @quantity2, DeliveryRequestDate2 = @date2, 
				DeliveryRequestQuantity3 = @quantity3, DeliveryRequestDate3 = @date3, 
				DeliveryRequestQuantity4 = @quantity4, DeliveryRequestDate4 = @date4, 
				DeliveryRequestQuantity5 = @quantity5, DeliveryRequestDate5 = @date5, 
				TotalReceiveingOrderQuantity = @total, TotalCost = @totalcost, RemainderQuantity = @remain,
				OrderNum = @ordernum, DeliveryPlace = @place,UpdatingPerson = @person,UpdatingPersonID=@personid,UpdatingDate = @date
				Where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str, con);
			comm.Transaction = trans;
			comm.Parameters.Add("@receiving",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderDate").Value.ToString());
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString());
			comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity1").Value.ToString());
			if(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Text == "")
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity2").Value.ToString());

			if(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Text == "")
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity3").Value.ToString());

			if(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Text == "")
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity4").Value.ToString());

			if(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Text == "")
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = Convert.DBNull;
			else 
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity5").Value.ToString());

			comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate1").Value.ToString());
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate2").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate2").Text == "")
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value =Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate2").Value.ToString());

			if(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate3").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate3").Text == "")
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate3").Value.ToString());
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate4").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate4").Text == "")
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate4").Value.ToString());
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate5").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate5").Text == "")
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value =Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("DeliveryRequestDate5").Value.ToString());

			comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());//총수량
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[1].ToString());//총금액
			comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = decimal.Parse(m_List[2].ToString());//잔량
			comm.Parameters.Add("@ordernum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("OrderNum").Value.ToString().Trim();//발주번호
			comm.Parameters.Add("@place",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("DeliveryPlace").Value.ToString().Trim();//납품장소
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = UserID;
			comm.Parameters.Add("@date",SqlDbType.DateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//발주원장번호 decimal.Parse(m_List[3].ToString());//잔량

			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 영업창고입고원장 변경
		/// 수량은 변경이 불가능하며 창고만 변경이 가능하다.
		/// 입고의뢰량과 입고량이 틀리면 입력자체를 하지 못하게 해야한다.
		/// </summary>
		/// <param name="rowcount"></param>
		private void IS_HTUpdate(SqlConnection con,SqlTransaction trans, int rowcount)
		{
			//			string str = @"Update IS_HT Set BusinessStorehouseNum = @num, 
			//						UpdatingPerson = @person, UpdatingPersonID=@personid, UpdatingDate = @date  where InStorehouseHistoryIndex = @index";
			//			SqlCommand comm = new SqlCommand(str,con);
			//			comm.Transaction = trans;
			//			
			//			comm.Parameters.Add("@num",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Value.ToString());
			//			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			//			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = UserID;
			//			comm.Parameters.Add("@date",SqlDbType.DateTime).Value = DateTime.Now.ToShortDateString();
			//			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseHistoryIndex").Value.ToString());
			//
			//			comm.ExecuteNonQuery();

			//신광화학 입고일자 수정
			string str = @"Update IS_HT Set BusinessStorehouseNum = @num, InStoreDate = @InStoreDate,
						UpdatingPerson = @person, UpdatingPersonID=@personid, UpdatingDate = @date  where InStorehouseHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@num",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Value.ToString());
			comm.Parameters.Add("@InStoreDate",SqlDbType.DateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStoreDate").Value.ToString());
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = UserID;
			comm.Parameters.Add("@date",SqlDbType.DateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseHistoryIndex").Value.ToString());

			comm.ExecuteNonQuery();

		}

		/// <summary>
		/// 입고원장 변경시 입고의뢰원장 변경
		/// (입고의뢰원장이 변경이 되면 문제가 있음,
		/// 입고의뢰전에 수정을 한뒤 변경이 가능해야 함...
		/// 그렇지 않으면 수량이 제대로 맞지 않게됨..
		/// 입고의뢰량과 입고량이 맞지 않으면 애초부터
		/// 입력이 불가능 하도록 해야 함. 
		/// </summary>
		/// <param name="rowcount"></param>
		private void ISR_HTUpdate(SqlConnection con,SqlTransaction trans,int rowcount)
		{
			//			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			//			conn.Open();
			//
			//			string str = "Update ISR_HT Set InStorehouseQuantity = @quantity where InStorehouseRequestHistoryIndex = @index";
			//			SqlCommand comm = new SqlCommand(str,conn);
			//			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[0].Cells.FromKey("InStorehouseQuantity").Value.ToString());
			//			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[0].Cells.FromKey("InStorehouseRequestHistoryIndex").Value.ToString());
			//
			//			comm.ExecuteNonQuery();
			//			conn.Close();
		}
		
		/// <summary>
		/// 품질검사원장의 진행상태만 변경이 가능하므로
		/// 실제 수량이 변경되는 것은 불가능하다.
		/// 진행상태 변경은 입고원장의 항목을 삭제해야만
		/// 가능하므로 수정에서는 있을수가 없다.
		/// </summary>
		/// <param name="rowcount"></param>
		private void QI_HTUpdate(SqlConnection con,SqlTransaction trans,int rowcount)
		{
			switch(m_Page)
			{
				case "WorkDailyReportRegistrationPC":		//작업일보현황
					//넘어온 작업일보원장번호와 동일한 품질검사원장의 진행상태가 대기인것만 수정이 가능하다.
					string str = "Select * From QI_HT Where HistoryIndex1 = @index and HistorySection1 = '작업일보'";
					string Condition = "";
					string Item = "";
					SqlCommand comm = new SqlCommand();
					comm.CommandText = str;
					comm.Connection = con;
					comm.Transaction = trans;
					comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
					SqlDataAdapter da = new SqlDataAdapter(comm);
					DataSet ds = new DataSet();
					da.Fill(ds);
					comm.Parameters.Clear();
					foreach(DataRow dr in ds.Tables[0].Rows)
					{
						Condition = dr["ProgressCondition"].ToString();
						Item = dr["ItemNum"].ToString();
					}

					//검사품인경우
					if(Check(m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString()))
					{
						if(Condition == "대기")
						{
							str = "Update QI_HT Set RequestQuantity = @quantity where HistoryIndex1 = @index and HistorySection1 = '작업일보'";
							comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString());
							comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
							comm.CommandText = str;
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();
						}
						else if( Condition != "")
						{
							throw new Exception("이미 품질검사완료된 품목입니다!");
						}
					}
					else//무검사인경우 진행상태가 입고완료가 아닌이상 수정가능
					{//ThisWorkCompletionQuantity
						if(decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Value.ToString()) != decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString()))
						{
							throw new Exception("무검사품은 금번작업수량과 적합수량이 동일해야 합니다!");
						}
						else
						{
						
							if(Condition != "입고완료")
							{
								str = "Update QI_HT Set RequestQuantity = @quantity, SuitabilityQuantity = @quantity where IHistoryIndex1 = @index and HistorySection1 = '작업일보'";
								comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString());
								comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
								comm.CommandText = str;
								comm.ExecuteNonQuery();
								comm.Parameters.Clear();
							}
							else if( Condition != "")
							{
								throw new Exception("이미 입고완료된 품목입니다!");
							}
						}
					}
					break;
				default :
					break;
			}
		}

		/// <summary>
		/// 입고창고를 변경할수 있다.
		/// </summary>
		/// <param name="rowcount"></param>
		private void IS_HTTableUpdate(SqlConnection con,SqlTransaction trans,int rowcount)
		{
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,con);
			comm_cost.Transaction = trans;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////


			// 일단 기존의 창고에서는 입고수량에 마이너스 증가를 시킨다
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			int year = int.Parse(m_List[2].ToString());
			int mon = int.Parse(m_List[3].ToString());
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();//마이너스 변경할 품목
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));//기존수량
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) * cost); //기존금액
			switch(int.Parse(m_List[1].ToString()))
			{
				case 1:
					comm.Parameters.Add("@storenum",SqlDbType.Int).Value = 1;
					break;
				case 2:
					comm.Parameters.Add("@storenum",SqlDbType.Int).Value = 2;
					break;
				default :
					comm.Parameters.Add("@storenum",SqlDbType.Int).Value = 3;
					break;
			}
			comm.Parameters.Add("@year",year);
			Table table = new Table(year,mon);
			comm.CommandText = table.InBusinessTable();
			comm.ExecuteNonQuery();				
			

			////////////////////////////////////////////
			// 변경되는 창고는 어느창고든 상관없이    //
			// 입고수량을 증가시킨다.                 //
			////////////////////////////////////////////
			
			year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStoreDate").Text).Year;
			mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStoreDate").Text).Month;
			Table table1 = new Table(year,mon);
			SqlCommand comm1 = new SqlCommand();
			comm1.Connection = con;
			comm1.Transaction = trans;
			comm1.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();//변경할 품목
			comm1.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseQuantity").Value.ToString());//변경수량
			comm1.Parameters.Add("@storenum",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Value.ToString());//변경창고번호
			comm1.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InStorehouseQuantity").Value.ToString()) * cost;//변경금액(수량*기준단가)
			comm1.Parameters.Add("@year",year);
			
			comm1.CommandText = table.InBusinessTable();
			comm1.ExecuteNonQuery();
		}


		/// <summary>
		/// 창고이동원장 수량및 창고변경 함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void StorehouseMovingUpdate(SqlConnection con,SqlTransaction trans,int rowcount)
		{
			string str = "Update SM_HT Set MovingQuantity = @quantity, MovingStorehoseName = @name, BusinessStorehouseNum2 = @num where StorehouseMovingHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("MovingQuantity").Value.ToString());// 이동수량
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List[1].ToString();//이동창고명
			comm.Parameters.Add("@num",SqlDbType.Int).Value = int.Parse(m_List[2].ToString());//이동창고번호
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseMovingHistoryIndex").Value.ToString());

			comm.ExecuteNonQuery();
		}


		private void BS_MTTableUpdate(SqlConnection con,SqlTransaction trans,int rowcount)
		{
			int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RegistrationDate").Text).Year;
			int mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RegistrationDate").Text).Month;
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
			SqlCommand comm_cost = new SqlCommand(str_cost,con);
			comm_cost.Transaction = trans;
			comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////


			// 일단 기존의 창고에서는 입고수량에 마이너스 증가를 시킨다
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();//마이너스 변경할 품목
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));//이전이동수량
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) * cost); //이전금액
			comm.Parameters.Add("@year",year);
			switch(int.Parse(m_List[2].ToString()))
			{
				case 1:
					comm.Parameters.Add("@storenum",SqlDbType.Int).Value = 1;
					break;
				case 2:
					comm.Parameters.Add("@storenum",SqlDbType.Int).Value = 2;
					break;
				default :
					comm.Parameters.Add("@storenum",SqlDbType.Int).Value = 3;
					break;
			}
			Table table = new Table(year,mon);
			comm.CommandText = table.InBusinessTable();
			comm.ExecuteNonQuery();	
			comm.Parameters.Clear();
			

			////////////////////////////////////////////
			// 변경되는 창고 입고수량을 증가시킨다.   //
			////////////////////////////////////////////
			
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();//변경할 품목
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("MovingQuantity").Value.ToString());//변경수량
			comm.Parameters.Add("@storenum",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum2").Value.ToString());//변경창고번호
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("MovingQuantity").Value.ToString()) * cost;//변경금액(수량*기준단가)
			comm.Parameters.Add("@year",year);
			comm.CommandText = table.InBusinessTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//			//마이너스 재고허용 여부
			//			if(MinusStore() == false)
			//			{
			//				string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum";
			//				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();//변경할 품목
			//				comm.Parameters.Add("@storenum", SqlDbType.Int).Value = int.Parse(m_List[2].ToString());//원 창고번호
			//				comm.CommandText = strSQL;
			//				SqlDataReader reader = comm.ExecuteReader();
			//						
			//				string StockQuantity = "False";
			//				string ItemName = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();//변경할 품목
			//				if(reader.Read())
			//				{
			//					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
			//					{
			//						StockQuantity = "True";
			//					}
			//				}
			//				reader.Close();
			//
			//				if(StockQuantity == "True")
			//				{
			//					throw new Exception(ItemName + " 의 재고량 부족으로 처리할 수 없습니다.");
			//				}
			//			}

		}



		

		/// <summary>
		/// 수금원장 변경함수
		/// </summary>
		/// <param name="rowcount"></param>
		private void CollectMoneyRegistrationUpdate(SqlConnection con,SqlTransaction trans,int rowcount)
		{
			string str = @"Update CM_HT Set CollectMoneyDate=@collectdate, ItemPaymentCost=@cost, SupplementaryValueTaxPaymentCost=@tax, DecisionMethodCode=@methodcode,
				DecisionMethod=@method, BillNum1=@billnum1, BillPaymentDate1=@billdate1, 
				BankCode1=@bankcode1, BankName1=@bank1, BillNum2=@billnum2, BillPaymentDate2=@billdate2, BankCode2=@bankcode2, BankName2=@bank2, 
				BillNum3=@billnum3, BillPaymentDate3=@billdate3, BankCode3=@bankcode3, BankName3=@bank3,UpdatingPerson= @person,UpdatingPersonID=@personid,UpdatingDate = @date
				Where CollectMoneyHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@collectdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("CollectMoneyDate").Value.ToString());// 수금일자
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ItemPaymentCost").Value.ToString());// 물품금액
			comm.Parameters.Add("@tax",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SupplementaryValueTaxPaymentCost").Value.ToString());// 부가세
			comm.Parameters.Add("@methodcode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("DecisionMethodCode").Value.ToString();//결재방법코드
			comm.Parameters.Add("@method",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("DecisionMethod").Value.ToString();//결재방법
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("BillNum1").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("BillNum1").Text.Trim() == "")
				comm.Parameters.Add("@billnum1",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@billnum1",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BillNum1").Value.ToString();//어음번호
			
			//if(m_Grid.Rows[rowcount].Cells.FromKey("BillPaymentDate1").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("BillPaymentDate1").Text.Trim() == "")
			if(m_List[2].ToString().Trim() == null || m_List[2].ToString().Trim() == "")
				comm.Parameters.Add("@billdate1",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@billdate1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BillPaymentDate1").Value.ToString());//결재일자
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("BankCode1").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("BankCode1").Text.Trim() == "")
			{

				comm.Parameters.Add("@bankcode1",SqlDbType.VarChar).Value = Convert.DBNull;//은행코드
				comm.Parameters.Add("@bank1",SqlDbType.VarChar).Value = Convert.DBNull;//은행코드
			}
			else
			{
				comm.Parameters.Add("@bankcode1",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BankCode1").Value.ToString();//은행코드
				comm.Parameters.Add("@bank1",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BankName1").Value.ToString();//은행명
			}
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("BillNum2").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("BillNum2").Text.Trim() == "")
				comm.Parameters.Add("@billnum2",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@billnum2",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BillNum2").Value.ToString();//어음번호
			
			//if(m_Grid.Rows[rowcount].Cells.FromKey("BillPaymentDate2").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("BillPaymentDate2").Text.Trim() == "" )
			if(m_List[3].ToString().Trim() == null || m_List[3].ToString().Trim() == "")
				comm.Parameters.Add("@billdate2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@billdate2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BillPaymentDate2").Value.ToString());//결재일자
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("BankCode2").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("BankCode2").Text.Trim() == "")
			{

				comm.Parameters.Add("@bankcode2",SqlDbType.VarChar).Value = Convert.DBNull;//은행코드
				comm.Parameters.Add("@bank2",SqlDbType.VarChar).Value = Convert.DBNull;//은행코드
			}
			else
			{
				comm.Parameters.Add("@bankcode2",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BankCode2").Value.ToString();
				comm.Parameters.Add("@bank2",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BankName2").Value.ToString();
			}
			
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("BillNum3").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("BillNum3").Text.Trim() == "")
				comm.Parameters.Add("@billnum3",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@billnum3",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BillNum3").Value.ToString();//어음번호
			
			//if(m_Grid.Rows[rowcount].Cells.FromKey("BillPaymentDate3").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("BillPaymentDate2").Text.Trim() == "" )
			if(m_List[4].ToString().Trim() == null || m_List[4].ToString().Trim() == "")
				comm.Parameters.Add("@billdate3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@billdate3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BillPaymentDate3").Value.ToString());//결재일자
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("BankCode3").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("BankCode3").Text.Trim() == "")
			{

				comm.Parameters.Add("@bankcode3",SqlDbType.VarChar).Value = Convert.DBNull;//은행코드
				comm.Parameters.Add("@bank3",SqlDbType.VarChar).Value = Convert.DBNull;//은행코드
			}
			else
			{
				comm.Parameters.Add("@bankcode3",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BankCode3").Value.ToString();
				comm.Parameters.Add("@bank3",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BankName3").Value.ToString();
			}
			
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = UserID;
			comm.Parameters.Add("@date",SqlDbType.DateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("CollectMoneyHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 수금원장 변경후 매출테이블 업데이트
		/// </summary>
		/// <param name="rowcount"></param>
		private void BSI_MTTableUpdate(SqlConnection con,SqlTransaction trans,int rowcount)
		{
			
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			int year;
			int mon;

			switch(m_Page)
			{
				case "ClaimRegistrationPC":
					//기존테이블에서 이전물품액을 마이너스 증가
					year = int.Parse(m_List[1].ToString());
					mon = int.Parse(m_List[2].ToString());
					Table table = new Table(year,mon);

					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));
					comm.Parameters.Add("@year",year);
					comm.CommandText = table.ClaimMoneyIncreaseTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//새로변경되는 물품액 증가
					year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year;
					mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Month;
					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value.ToString());// 물품금액
					comm.Parameters.Add("@year",year);
					Table table1 = new Table(year,mon);
					comm.CommandText = table1.ClaimMoneyIncreaseTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					break;
				case "ClaimPC":			//클레임
					year = int.Parse(m_List[1].ToString());
					mon = int.Parse(m_List[2].ToString());
					Table table2 = new Table(year,mon);
					//기존테이블에서 이전물품액을 마이너스 증가
					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));
					comm.Parameters.Add("@year",year);

					Table table3 = new Table(year,mon);
					comm.CommandText = table3.PayClaimMoneyIncreaseTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//새로변경되는 물품액 증가
					year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year;
					mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Month;
					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value.ToString());// 물품금액
					comm.Parameters.Add("@year",year);
					Table table4 = new Table(year,mon);
					comm.CommandText = table4.PayClaimMoneyIncreaseTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					break;
				case "EtcClaimPC":			//클레임
					year = int.Parse(m_List[1].ToString());
					mon = int.Parse(m_List[2].ToString());
					//기존테이블에서 이전금액을 마이너스 증가
					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));
					comm.Parameters.Add("@year",year);

					Table table7 = new Table(year,mon);
					comm.CommandText = table7.PayClaimMoneyIncreaseTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//새로변경되는 금액 증가
					year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Year;
					mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceiptDate").Text).Month;
					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ClaimCost").Value.ToString());// 공제금액
					comm.Parameters.Add("@year",year);
					Table table8 = new Table(year,mon);
					comm.CommandText = table8.PayClaimMoneyIncreaseTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					break;
				default ://수금등록
					//기존테이블에서 이전물품액을 마이너스 증가
					year = int.Parse(m_List[5].ToString());
					mon = int.Parse(m_List[6].ToString());
					Table table5 = new Table(year,mon);

					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (decimal.Parse(m_List[0].ToString()));
					comm.Parameters.Add("@year",year);
					comm.CommandText = table5.CollectMoneyDiminutionUnCollectMoneyIncreaseTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//새로변경되는 물품액 증가
					year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("CollectMoneyDate").Text).Year;
					mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("CollectMoneyDate").Text).Month;
					comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ItemPaymentCost").Value.ToString());// 물품금액
					comm.Parameters.Add("@year",year);

					Table table6 = new Table(year,mon);
					comm.CommandText = table6.CollectMoneyIncreaseUnCollectMoneyDiminutionTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					break;
			}
		}




		/// <summary>
		/// 상품구매의뢰원장 유효성 검사
		/// </summary>
		/// <returns></returns>
		private bool GoodsBuyingRequestPCValidate(int rowcount)
		{
			HttpContext hc = HttpContext.Current;
			if(m_Grid.Rows[rowcount].Cells.FromKey("ProgressCondition").Value.ToString() != "대기")
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('진행상태가 대기인것만 수정이 가능합니다!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandQuantity").Value.ToString() == "" || m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandQuantity").Value.ToString() == null || m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandQuantity").Value.ToString() == "0")
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('1차납기요구량을 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Value.ToString() == "" || m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Value.ToString() == null || m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Value.ToString() == "0")
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('1차납기요구일을 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else
				return true;

		}
		/// <summary>
		/// 상품구매의뢰 수량수정(추가만 수정가능)
		/// </summary>
		private void GoodsBuyingRequestPCUpdate(SqlConnection con,SqlTransaction trans, int rowcount)
		{
			string str = "Update BR_HT Set BuyingRequestSourceCode = @code, BuyingRequestSource = @source, "+
				" FirstDeliveryDemandQuantity = @quantity1, FirstDeliveryDemandDate = @date1,"+
				" SecondDeliveryDemandQuantity = @quantity2, SecondDeliveryDemandDate = @date2, "+
				" ThirdDeliveryDemandQuantity = @quantity3, ThirdDeliveryDemandDate = @date3, "+
				" FourthDeliveryDemandQuantity = @quantity4, FourthDeliveryDemandDate = @date4, "+
				" FifthDeliveryDemandQuantity = @quantity5, FifthDeliveryDemandDate = @date5, "+
				" OrderQuantity = @totalquantity, ApplyUnitCost = @cost, TotalCost = @totalcost, "+
				" UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @Update "+
				"where BuyingRequestHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BuyingRequestSourceCode").Value.ToString();// 구매의뢰원천코드
			comm.Parameters.Add("@source",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BuyingRequestSource").Value.ToString();// 구매의뢰원천
			comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandQuantity").Value.ToString());
			comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Value.ToString());
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandQuantity").Value.ToString() == "" || m_Grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandQuantity").Value.ToString() == null)
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandQuantity").Value.ToString());

			if(m_Grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandQuantity").Value.ToString() == "" || m_Grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandQuantity").Value.ToString() == null)
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandQuantity").Value.ToString());
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandQuantity").Value.ToString() == "" || m_Grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandQuantity").Value.ToString() == null)
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandQuantity").Value.ToString());


			if(m_Grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandQuantity").Value.ToString() == "" || m_Grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandQuantity").Value.ToString() == null)
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandQuantity").Value.ToString());


			if(m_Grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Text.Trim() == "")
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Value.ToString());
          
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Text.Trim() == "")
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Value.ToString());
          
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Text.Trim() == "")
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Value.ToString());
          
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Text.Trim() == "")
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Value.ToString());
			
			comm.Parameters.Add("@totalquantity",SqlDbType.Decimal).Value = decimal.Parse(m_List[0].ToString());// 총의뢰수량
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString());//요구단가
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_List[1].ToString());// 총금액
			
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value =UserID;
			comm.Parameters.Add("@Update",SqlDbType.VarChar).Value = DateTime.Now.ToShortDateString();

			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BuyingRequestHistoryIndex").Value.ToString());

			comm.ExecuteNonQuery();
		}



		

		/// <summary>
		/// 기타입출고원장 수정(이동수량, 사유, 세부내용, 입출고구분
		/// </summary>
		/// <param name="rowcount"></param>
		private void OtherInOutStorehousePCUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{

			string str = "Update OIOS_HT Set StorehouseName = @store, "+
				" BusinessStorehouseNum = @storenum, " +
				" InOutStorehouseDistinction = @distinction, " +
				" InOutStorehouseQuantity = @quantity, " +
				" InOutStorehouseReasonCode = @code, " +
				" InOutStorehouseReason = @reason, " +
				" InOutStorehouseDetailReason = @detail, " +
				" InOutDate = @InOutDate, "+
				" UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @Update "+
				"where OtherInOutStorehouseHistoryIndex = @index";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@store",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString();//창고명
			comm.Parameters.Add("@storenum",SqlDbType.Int).Value = int.Parse(m_List[7].ToString());//창고번호
			if(int.Parse(m_List[4].ToString()) == 0)//입출고구분(1 출고 0 입고)
				comm.Parameters.Add("@distinction",SqlDbType.Bit).Value = false; 
			else
				comm.Parameters.Add("@distinction",SqlDbType.Bit).Value = true;
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());//수량
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[5].ToString();//입출고사유코드
			comm.Parameters.Add("@reason",SqlDbType.VarChar).Value = m_List[6].ToString();//입출고사유
			comm.Parameters.Add("@detail",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDetailReason").Value.ToString();//세부사유
			comm.Parameters.Add("@InOutDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).ToShortDateString();// 입출고일자
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value =UserID;
			comm.Parameters.Add("@Update",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OtherInOutStorehouseHistoryIndex").Value.ToString());//기타입출고원장 인덱스
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}


		private void OtherInOutStorehousePCHistory(SqlConnection con,SqlTransaction trans, int rowcount)
		{
			string str = "Update SH_HT Set Quantity=@Quantity,[Date] = @date, Division = @Division where HistoryDivision='기타입출고' and  HistoryIndex=@index ";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@date", DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Value.ToString()).ToShortDateString());
			comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()));//합격수량
			if(int.Parse(m_List[4].ToString()) == 0)//입출고구분(0 출고 1 입고)
				comm.Parameters.Add("@Division", 0); 
			else
				comm.Parameters.Add("@Division", 1);
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OtherInOutStorehouseHistoryIndex").Value.ToString());//기타입출고원장인덱스
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 기타입출고원장 등록시 각 창고테이블에 수량변경
		/// </summary>
		private void OIOS_HTTableUpdate(SqlConnection con,SqlTransaction trans, int rowcount)
		{
			string m_store;//창고테이블 명 
			switch(m_List[1].ToString().Trim())
			{
				case "원자재창고":
					m_store = "RMS_MT";
					break;
				case "생산창고":
					m_store = "PS_MT";
					break;
				case "외주창고":
					m_store = "OS_MT";
					break;
				case "납품창고":
					m_store = "DS_MT";
					break;
				default:
					m_store = "BS_MT";
					break;
			}

			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			int year = int.Parse(m_List[8].ToString());
			int mon = int.Parse(m_List[9].ToString());

			Table table = new Table(year,mon);

			switch(m_store)
			{
				case "RMS_MT":
				{
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					decimal cost = 0;
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
					SqlCommand comm_cost = new SqlCommand(str_cost,con);
					comm_cost.Transaction = trans;
					comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					SqlDataReader dr = comm_cost.ExecuteReader();
					if(dr.Read())
						cost = decimal.Parse(dr["StandardUnitCost"].ToString());
					dr.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////
				
					//일단 원창고에서 입고수량을 마이너스 증가시킨다(원래수량)
					comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));
					comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) * cost);
					comm.Parameters.Add("@year",year);
			
					if(m_List[3].ToString() == "1") //입고 이므로 입고수량증가
					{
						comm.CommandText= table.InRowTable();
					}
					else // 출고이므로 출고수량 증가
					{
						comm.CommandText= table.OutRowTable();
					}
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					
					Table table1 = new Table(DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Year, DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Month);

					//변경된 수량 변경
					if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "원자재창고")
					{
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InRowTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutRowTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "생산창고")
					{
						Decimal m_progressrate = 0;//진척율
						int m_prcesssequence = 0;//공정순서
						
						//////////////////////////////////////////
						// 품목의 진척율, 공정순서을 위해		//
						// 품목정보테이블에서					//
						// 기준단가를 가져와 계산한다.   -시작- //
						//////////////////////////////////////////
						string str_rate = "Select ProgressRate,ProcessSequenceNum,ProcessCode From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
						SqlCommand comm_rate = new SqlCommand(str_rate,con);
						comm_rate.Transaction = trans;
			
						comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						SqlDataReader dr_rate = comm_rate.ExecuteReader();
						if(dr_rate.Read())
						{
							m_progressrate = decimal.Parse(dr_rate["ProgressRate"].ToString());
							m_prcesssequence = int.Parse(dr_rate["ProcessSequenceNum"].ToString());
						}
						dr_rate.Close();
						//////////////////////////////////////////
						// 품목의 진척율, 공정순서을 위해		//
						// 품목정보테이블에서					//
						// 기준단가를 가져와 계산한다.   -끝-   //
						//////////////////////////////////////////
						
						//변경된 수량 변경
						
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost * decimal.Parse(m_progressrate.ToString())/100;
						comm.Parameters.Add("@year",year);


						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InProductionTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutProductionTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessSequenceNum = @Sequence and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@Sequence",m_prcesssequence);
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "외주창고")
					{
						Decimal m_progressrate = 0;//진척율
						int m_prcesssequence = 0;//공정순서
					
						//////////////////////////////////////////
						// 품목의 진척율, 공정순서을 위해		//
						// 품목정보테이블에서					//
						// 기준단가를 가져와 계산한다.   -시작- //
						//////////////////////////////////////////
						string str_rate = "Select ProgressRate,ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
						SqlCommand comm_rate = new SqlCommand(str_rate,con);
						comm_rate.Transaction = trans;
			
						comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						SqlDataReader dr_rate = comm_rate.ExecuteReader();
						if(dr_rate.Read())
						{
							m_progressrate = decimal.Parse(dr_rate["ProgressRate"].ToString());
							m_prcesssequence = int.Parse(dr_rate["ProcessSequenceNum"].ToString());
						}
						dr_rate.Close();
						//////////////////////////////////////////
						// 품목의 진척율, 공정순서을 위해		//
						// 품목정보테이블에서					//
						// 기준단가를 가져와 계산한다.   -끝-   //
						//////////////////////////////////////////

						// 변경수량 업데이트
						comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();

						if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
							comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
						else
							comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());

						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();//
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost * decimal.Parse(m_progressrate.ToString())/100;
						comm.Parameters.Add("@year",year);
					
					
						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessSequenceNum = @Sequence and BusinessRegistrationNum = @Business and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@Sequence",m_prcesssequence);//BusinessRegistrationNum
							comm.Parameters.Add("@Business",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "납품창고")
					{				
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InDeliveryTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutDeliveryTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM DS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
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
					else
					{
						// 변경수량 업데이트
						
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString())*cost;
						comm.Parameters.Add("@storenum", SqlDbType.Decimal).Value = int.Parse(m_List[2].ToString());//창고번호
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InBusinessTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutBusinessTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@storenum", SqlDbType.Decimal).Value = int.Parse(m_List[2].ToString());//창고번호
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
				}
					break;
				case "PS_MT":
				{
					Decimal m_cost = 0; //금액
					Decimal m_progressrate = 0;//진척율
					
			
			
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
					SqlCommand comm_cost = new SqlCommand(str_cost,con);
					comm_cost.Transaction = trans;
					comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					SqlDataReader dr_cost = comm_cost.ExecuteReader();
					
					if(dr_cost.Read())
						m_cost = decimal.Parse(dr_cost["StandardUnitCost"].ToString());
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
					SqlCommand comm_rate = new SqlCommand(str_rate,con);
					comm_rate.Transaction = trans;		
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

					if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
					else
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));

					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(decimal.Parse(m_cost.ToString())) * decimal.Parse(m_progressrate.ToString())/100;
					comm.Parameters.Add("@year",year);
					
					
					
					if(m_List[3].ToString() == "1") //입고 이므로 입고수량증가
					{
						comm.CommandText= table.InProductionTable();
					}
					else // 출고이므로 출고수량 증가
					{
						comm.CommandText= table.OutProductionTable();
					}
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();	
				
					Table table1 = new Table(DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Year,DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Month);


					//변경된 수량 변경
					if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "원자재창고")
					{
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * m_cost;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InRowTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutRowTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "생산창고")
					{
						//변경된 수량 변경
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * decimal.Parse(m_cost.ToString()) * decimal.Parse(m_progressrate.ToString())/100;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InProductionTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutProductionTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @ProcessCode and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@ProcessCode",m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString());
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "외주창고")
					{
						// 변경수량 업데이트
						
						comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();

						if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
							comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
						else
							comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());

						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();//
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * decimal.Parse(m_cost.ToString()) * decimal.Parse(m_progressrate.ToString())/100;
						comm.Parameters.Add("@year",year);
					
					
						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and BusinessRegistrationNum = @Business and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
							comm.Parameters.Add("@Business",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "납품창고")
					{
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * m_cost;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InDeliveryTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutDeliveryTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM DS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
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
					else
					{
						// 변경수량 업데이트
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString())*m_cost;
						comm.Parameters.Add("@storenum", SqlDbType.Decimal).Value = int.Parse(m_List[2].ToString());//창고번호
						comm.Parameters.Add("@year",year);

					
					
						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InBusinessTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutBusinessTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@storenum", SqlDbType.Decimal).Value = int.Parse(m_List[2].ToString());//창고번호
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
				}
					break;
				case "BS_MT":
				{
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					decimal cost = 0;
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @ItemNum";
					SqlCommand comm_cost = new SqlCommand(str_cost,con);
					comm_cost.Transaction = trans;
					comm_cost.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					SqlDataReader dr = comm_cost.ExecuteReader();
					if(dr.Read())
						cost = decimal.Parse(dr["StandardUnitCost"].ToString());
					dr.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////


					// 기존수량 마이너스 증가
					comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString())*cost);
					comm.Parameters.Add("@storenum", SqlDbType.Decimal).Value = int.Parse(m_List[2].ToString());//창고번호
					comm.Parameters.Add("@year",year);
			
					
					if(m_List[3].ToString() == "1") //입고 이므로 입고수량증가
					{
						comm.CommandText= table.InBusinessTable();
					}
					else // 출고이므로 출고수량 증가
					{
						comm.CommandText= table.OutBusinessTable();
					}
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					Table table1 = new Table(DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Year, DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Month);

					//변경된 수량 변경
					if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "원자재창고")
					{
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InRowTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutRowTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "생산창고")
					{
						Decimal m_progressrate = 0;//진척율
						int m_prcesssequence = 0;//공정순서
					
						//////////////////////////////////////////
						// 품목의 진척율, 공정순서을 위해		//
						// 품목정보테이블에서					//
						// 기준단가를 가져와 계산한다.   -시작- //
						//////////////////////////////////////////
						string str_rate = "Select ProgressRate,ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
						SqlCommand comm_rate = new SqlCommand(str_rate,con);
						comm_rate.Transaction = trans;
			
						comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						SqlDataReader dr_rate = comm_rate.ExecuteReader();
						if(dr_rate.Read())
						{
							m_progressrate = decimal.Parse(dr_rate["ProgressRate"].ToString());
							m_prcesssequence = int.Parse(dr_rate["ProcessSequenceNum"].ToString());
						}
						dr_rate.Close();
						//////////////////////////////////////////
						// 품목의 진척율, 공정순서을 위해		//
						// 품목정보테이블에서					//
						// 기준단가를 가져와 계산한다.   -끝-   //
						//////////////////////////////////////////
						
						//변경된 수량 변경
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost * decimal.Parse(m_progressrate.ToString())/100;
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();		//공정코드
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InProductionTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutProductionTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessSequenceNum = @Sequence and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@Sequence",m_prcesssequence);
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "외주창고")
					{
						decimal m_progressrate = 0;//진척율

						// 변경수량 업데이트
						comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();

						if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
							comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
						else
							comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());

						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();//
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost * decimal.Parse(m_progressrate.ToString())/100;
						comm.Parameters.Add("@year",year);
					
					
						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @ProcessCode and BusinessRegistrationNum = @Business and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@ProcessCode",m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text);//
							comm.Parameters.Add("@Business",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "납품창고")
					{				
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InDeliveryTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutDeliveryTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM DS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
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
					else
					{
						// 변경수량 업데이트
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString())*cost;
						comm.Parameters.Add("@storenum", SqlDbType.Decimal).Value = int.Parse(m_List[2].ToString());//창고번호
						comm.Parameters.Add("@year",year);
					
						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InBusinessTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutBusinessTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@storenum", SqlDbType.Decimal).Value = int.Parse(m_List[2].ToString());//창고번호
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



				}
					break;
				case "OS_MT":
				{
					Decimal m_cost = 0; //금액
					Decimal m_progressrate = 0;//진척율
					int m_prcesssequence = 0;//공정순서
					
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
					SqlCommand comm_cost = new SqlCommand(str_cost,con);
					comm_cost.Transaction = trans;
					comm_cost.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					SqlDataReader dr_cost = comm_cost.ExecuteReader();
					if(dr_cost.Read())
						m_cost = decimal.Parse(dr_cost["StandardUnitCost"].ToString());
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
					SqlCommand comm_rate = new SqlCommand(str_rate,con);
					comm_rate.Transaction = trans;
					comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
					SqlDataReader dr_rate = comm_rate.ExecuteReader();
					if(dr_rate.Read())
					{
						m_progressrate = decimal.Parse(dr_rate["ProgressRate"].ToString());
						m_prcesssequence = int.Parse(dr_rate["ProcessSequenceNum"].ToString());
					}
					dr_rate.Close();
					//////////////////////////////////////////
					// 품목의 진척율, 공정순서을 위해		//
					// 품목정보테이블에서					//
					// 기준단가를 가져와 계산한다.   -끝-   //
					//////////////////////////////////////////

					
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();

					if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
					else
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));

					comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
					comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();//
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) * decimal.Parse(m_cost.ToString())) * decimal.Parse(m_progressrate.ToString())/100;
					comm.Parameters.Add("@year",year);
					
					
					if(m_List[3].ToString() == "1") //입고 이므로 입고수량마이너스증가
					{
						comm.CommandText= table.InTable();
					}
					else // 출고이므로 출고수량 마이너스증가
					{
						comm.CommandText= table.OutTable();
					}
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();


					Table table1 = new Table(DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Year, DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Month);

					//변경된 수량 변경
					if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "원자재창고")
					{
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * m_cost;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InRowTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutRowTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "생산창고")
					{	
						//변경된 수량 변경
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * decimal.Parse(m_cost.ToString()) * decimal.Parse(m_progressrate.ToString())/100;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InProductionTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutProductionTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessSequenceNum = @Sequence and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@year",DateTime.Now.Year);
							comm.Parameters.Add("@Sequence",m_prcesssequence);
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "외주창고")
					{
						// 변경수량 업데이트
						comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();

						if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
							comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
						else
							comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());

						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();//
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * decimal.Parse(m_cost.ToString()) * decimal.Parse(m_progressrate.ToString())/100;
						comm.Parameters.Add("@year",year);
					
					
						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessSequenceNum = @Sequence and BusinessRegistrationNum = @Business and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@Sequence",m_prcesssequence);//BusinessRegistrationNum
							comm.Parameters.Add("@Business",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "납품창고")
					{			
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * m_cost;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InDeliveryTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutDeliveryTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();


						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM DS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
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
					else
					{
						// 변경수량 업데이트
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString())*m_cost;
						comm.Parameters.Add("@storenum", SqlDbType.Decimal).Value = int.Parse(m_List[2].ToString());//창고번호
						comm.Parameters.Add("@year",year);
					
						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InBusinessTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutBusinessTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@storenum", SqlDbType.Decimal).Value = int.Parse(m_List[2].ToString());//창고번호
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


				}
					break;
				default:			// 납품창고
				{
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -시작-  //
					///////////////////////////////////////////
					decimal cost = 0;
					string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
					SqlCommand comm_cost = new SqlCommand(str_cost,con);
					comm_cost.Transaction = trans;
					comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					SqlDataReader dr = comm_cost.ExecuteReader();
					if(dr.Read())
						cost = decimal.Parse(dr["StandardUnitCost"].ToString());
					dr.Close();
					///////////////////////////////////////////
					// 창고의 금액을 위해 품목정보테이블에서 //
					// 기준단가를 가져와 계산한다.   -끝-    //
					///////////////////////////////////////////
				
					comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(m_List[0].ToString());
					comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -decimal.Parse(m_List[0].ToString()) * cost;
					comm.Parameters.Add("@year",year);
					
					if(m_List[3].ToString() == "1") //입고 이므로 입고수량증가
					{
						comm.CommandText= table.InDeliveryTable();
					}
					else // 출고이므로 출고수량 증가
					{
						comm.CommandText= table.OutDeliveryTable();
					}
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					
					Table table1 = new Table(DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Year, DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutDate").Text).Month);

					//변경된 수량 변경
					if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "원자재창고")
					{
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InRowTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutRowTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "생산창고")
					{
						Decimal m_progressrate = 0;//진척율
						int m_prcesssequence = 0;//공정순서
					
						//////////////////////////////////////////
						// 품목의 진척율, 공정순서을 위해		//
						// 품목정보테이블에서					//
						// 기준단가를 가져와 계산한다.   -시작- //
						//////////////////////////////////////////
						string str_rate = "Select ProgressRate,ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
						SqlCommand comm_rate = new SqlCommand(str_rate,con);
						comm_rate.Transaction = trans;			
						comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						SqlDataReader dr_rate = comm_rate.ExecuteReader();
						if(dr_rate.Read())
						{
							m_progressrate = decimal.Parse(dr_rate["ProgressRate"].ToString());
							m_prcesssequence = int.Parse(dr_rate["ProcessSequenceNum"].ToString());
						}
						dr_rate.Close();
						//////////////////////////////////////////
						// 품목의 진척율, 공정순서을 위해		//
						// 품목정보테이블에서					//
						// 기준단가를 가져와 계산한다.   -끝-   //
						//////////////////////////////////////////
						
						//변경된 수량 변경
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost * decimal.Parse(m_progressrate.ToString())/100;
						comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();		//공정코드
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InProductionTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutProductionTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessSequenceNum = @Sequence and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@Sequence",m_prcesssequence);
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "외주창고")
					{
						Decimal m_progressrate = 0;//진척율
						int m_prcesssequence = 0;//공정순서

						//////////////////////////////////////////
						// 품목의 진척율, 공정순서을 위해		//
						// 품목정보테이블에서					//
						// 기준단가를 가져와 계산한다.   -시작- //
						//////////////////////////////////////////
						string str_rate = "Select ProgressRate,ProcessSequenceNum From PSI_MT Where RecodingState = 1 and ItemNum = @num and ProcessCode = @code";
						SqlCommand comm_rate = new SqlCommand(str_rate,con);
						comm_rate.Transaction = trans;			
						comm_rate.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
						comm_rate.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();

						SqlDataReader dr_rate = comm_rate.ExecuteReader();
						if(dr_rate.Read())
						{
							m_progressrate = decimal.Parse(dr_rate["ProgressRate"].ToString());
							m_prcesssequence = int.Parse(dr_rate["ProcessSequenceNum"].ToString());
						}
						dr_rate.Close();
						//////////////////////////////////////////
						// 품목의 진척율, 공정순서을 위해		//
						// 품목정보테이블에서					//
						// 기준단가를 가져와 계산한다.   -끝-   //
						//////////////////////////////////////////

						// 변경수량 업데이트
						comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();

						if(m_List[0].ToString() == null || m_List[0].ToString().Trim() == "")
							comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = 0;
						else
							comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());

						comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString();
						comm.Parameters.Add("@comnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Value.ToString();//
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost * decimal.Parse(m_progressrate.ToString())/100;
						comm.Parameters.Add("@year",year);
					
					
						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();


						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM OS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessSequenceNum = @Sequence and BusinessRegistrationNum = @Business and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@Sequence",m_prcesssequence);//BusinessRegistrationNum
							comm.Parameters.Add("@Business",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
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
					else if(m_Grid.Rows[rowcount].Cells.FromKey("StorehouseName").Value.ToString()== "납품창고")
					{				
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString()) * cost;
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InDeliveryTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutDeliveryTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM DS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
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
					else
					{
						// 변경수량 업데이트
						comm.Parameters.Add("@num", SqlDbType.VarChar).Value =m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
						comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString());
						comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseQuantity").Value.ToString())*cost;
						comm.Parameters.Add("@storenum", SqlDbType.Decimal).Value = int.Parse(m_List[2].ToString());//창고번호
						comm.Parameters.Add("@year",year);

						if(m_Grid.Rows[rowcount].Cells.FromKey("InOutStorehouseDistinction").Text == "1") //입고 이므로 입고수량증가
						{
							comm.CommandText= table1.InBusinessTable();
						}
						else // 출고이므로 출고수량 증가
						{
							comm.CommandText= table1.OutBusinessTable();
						}
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						//마이너스 재고허용 여부
						if(MinusStore() == false)
						{
							string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
							comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
							comm.Parameters.Add("@storenum", SqlDbType.Decimal).Value = int.Parse(m_List[2].ToString());//창고번호
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
				}
					break;
			}
			
		}


		/// <summary>
		/// 보용품 입고 원장 수정
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="rowcount"></param>
		private void AddItemInStorePCUpdae(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			string str = "Update AS_HT Set InstoreQuantity = @quantity, " +
				" ReasonCode = @code, " +
				" ReasonName = @reason, " +
				" InstoreDate = @InstoreDate, "+
				" UpdatingPerson = @person, UpdatingPersonID = @personid, UpdatingDate = @Update "+
				"where AddItemInStoreIndex = @index";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString());//수량
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_List[1].ToString();//입고사유코드
			comm.Parameters.Add("@reason",SqlDbType.VarChar).Value = m_List[2].ToString();//입고사유
			comm.Parameters.Add("@InstoreDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Text).ToShortDateString();// 입고일자
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value =UserID;
			comm.Parameters.Add("@Update",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("AddItemInStoreIndex").Value.ToString());//보용품입고원장 번호
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}


		/// <summary>
		/// 보용품 입고현황 수량 수정시 이력원장 수정(보용품창고와 원자재 혹은 생산창고 동시에 수정이 됨)
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="rowcount"></param>
		private void AddItemInStorePCHistory(SqlConnection con,SqlTransaction trans, int rowcount)
		{
			string str = "Update SH_HT Set Quantity=@Quantity , [Date] = @date where HistoryDivision='보용품입고' and  HistoryIndex=@index ";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()));//합격수량
			comm.Parameters.Add("@date", DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Value.ToString()).ToShortDateString());//입고일자
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("AddItemInStoreIndex").Value.ToString());//보용품 입고원장인덱스
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//해당 품목이 원자재면 그대로 처리
			//해당 품목이 원자재가 아니면 레코드를 가져와 돌면서 해당 수량 수
		}

		/// <summary>
		/// 보용품 입고원장 수정후 창고 테이블 수량 수정
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="rowcount"></param>
		private void AddItemInStorePCTableUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			int year = int.Parse(m_List[3].ToString());
			int mon = int.Parse(m_List[4].ToString());

			Table table = new Table(year,mon);


			if(m_Grid.Rows[rowcount].Cells.FromKey("StoreName").Text.Trim() == "원자재창고")
			{
				///////////////////////////////////////////
				// 창고의 금액을 위해 품목정보테이블에서 //
				// 기준단가를 가져와 계산한다.   -시작-  //
				///////////////////////////////////////////
				decimal cost = 0;
				string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
				SqlCommand comm_cost = new SqlCommand(str_cost,con);
				comm_cost.Transaction = trans;
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
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) * cost);
				comm.Parameters.Add("@year",year);
			
				comm.CommandText= table.OutRowTable();
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				//보용품 창고에 입고수량을 마이너스 증가시킨다.
				comm.CommandText= table.InAddItemTable();
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				//comm.Parameters.Add("@code", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) * cost);
				comm.Parameters.Add("@year",year);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				
				//변경된 수량 변경
				Table table1 = new Table(DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Text).Year, DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Text).Month);

				
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString());
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()) * cost;
				comm.Parameters.Add("@year",DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Text).Year);

				comm.CommandText= table1.OutRowTable();
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				//보용품 창고에 입고수량을 마이너스 증가시킨다.
				comm.CommandText= table.InAddItemTable();
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString());
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				//comm.Parameters.Add("@code", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()) * cost;
				comm.Parameters.Add("@year",DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Text).Year);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();


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
			else
			{
				Decimal cost = 0; //금액
				Decimal m_progressrate = 0;//진척율
					
			
			
				///////////////////////////////////////////
				// 창고의 금액을 위해 품목정보테이블에서 //
				// 기준단가를 가져와 계산한다.   -시작-  //
				///////////////////////////////////////////
				string str_cost = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num";
				SqlCommand comm_cost = new SqlCommand(str_cost,con);
				comm_cost.Transaction = trans;
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
				SqlCommand comm_rate = new SqlCommand(str_rate,con);
				comm_rate.Transaction = trans;		
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
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(decimal.Parse(cost.ToString()))* decimal.Parse(m_List[0].ToString()) * decimal.Parse(m_progressrate.ToString())/100;
				comm.Parameters.Add("@year",year);
					
				comm.CommandText= table.OutProductionTable();
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
	
				//보용품 창고에 입고수량을 마이너스 증가시킨다.
				comm.CommandText= table.InAddItemTable();
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()));
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				//comm.Parameters.Add("@code", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) * cost);
				comm.Parameters.Add("@year",year);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Text).Year;
				mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreDate").Text).Month;
				Table table1 = new Table(year,mon);

				//생산창고 출고수량을 증가시킨다.
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString());
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				comm.Parameters.Add("@code", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()) * cost;
				comm.Parameters.Add("@year",year);
				comm.CommandText= table.OutProductionTable();
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
				//보용품 창고에 입고수량을 증가시킨다.
				comm.CommandText= table.InAddItemTable();
				comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString());
				comm.Parameters.Add("@num", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
				//comm.Parameters.Add("@code", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text;
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("InstoreQuantity").Value.ToString()) * cost;
				comm.Parameters.Add("@year",year);
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();


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
				
		}


		/// <summary>
		/// 상품제품출고원장 유효성검사
		/// </summary>
		/// <param name="rowcount"></param>
		private bool GoodsManufactureOutStorehousePCValidate(int rowcount)
		{
			HttpContext hc = HttpContext.Current;

			if(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString().Trim() == "0" || m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString().Trim() == "")
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('수량을 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else
				return true;
		}





		/// <summary>
		/// 상품제품출고원장 변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void GoodsManufactureOutStorehousePCUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			//			string str = "Update OS_HT Set OutStorehouseQuantity=@storequantity, UnInspectionQuantity=@uninspection, " +
			//				" OutStoreDate = @outdate, LotNum = @LotNum, " + 
			//				" SuitabilityQuantity=@suitability, UnSuitabilityQuantity=@unsuitability, " +
			//				" UnSuitabilityCauseCode=@causecode, UnSuitabilityCauseMeaning=@cause, " +
			//				" UnSuitabilityStatusCode=@statuscode, UnSuitabilityStatusMeaning=@status, " +
			//				" UnSuitabilityDetailMeaning=@detail, UnSuitabilityCost=@unsuitabilitycost, " +
			//				" InspectionDecisionCode=@decisioncode, InspectionDecisionMeaning=@decision, " +
			//				" BusinessStorehouseNum=@num, UpdatingPerson=@person, UpdatingPersonID=@personid, " +
			//				" UpdatingDate=@date where OutStorehouseHistoryIndex=@index ";
			string str = "Update OS_HT Set OutStorehouseQuantity=@storequantity, UnInspectionQuantity=@uninspection, " +
				" OutStoreDate = @outdate, " + 
				" SuitabilityQuantity=@suitability, UnSuitabilityQuantity=@unsuitability, " +
				" UnSuitabilityCauseCode=@causecode, UnSuitabilityCauseMeaning=@cause, " +
				" UnSuitabilityStatusCode=@statuscode, UnSuitabilityStatusMeaning=@status, " +
				" UnSuitabilityDetailMeaning=@detail, UnSuitabilityCost=@unsuitabilitycost, " +
				" InspectionDecisionCode=@decisioncode, InspectionDecisionMeaning=@decision, " +
				" BusinessStorehouseNum=@num, UpdatingPerson=@person, UpdatingPersonID=@personid, " +
				" UpdatingDate=@date where OutStorehouseHistoryIndex=@index ";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@storequantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString());// 출고수량
			//comm.Parameters.Add("@uninspection",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("UnInspectionQuantity").Value.ToString());//미검수량
			if(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Text == "0")
			{
				comm.Parameters.Add("@uninspection",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//미검수량
				comm.Parameters.Add("@suitability",SqlDbType.Decimal).Value = Convert.DBNull;//합격수량
				comm.Parameters.Add("@unsuitability",SqlDbType.Decimal).Value = Convert.DBNull;//부적합수량
			}
			else
			{
				//comm.Parameters.Add("@uninspection",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("UnInspectionQuantity").Value.ToString());//미검수량
				comm.Parameters.Add("@uninspection",SqlDbType.Decimal).Value = 0;//미검수량
				comm.Parameters.Add("@suitability",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString());//합격수량
				comm.Parameters.Add("@unsuitability",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityQuantity").Value.ToString());//부적합수량
			}
			comm.Parameters.Add("@outdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Value.ToString()).ToShortDateString();//외주출고일자
			if(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityCauseCode").Value == null)
				comm.Parameters.Add("@causecode",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@causecode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityCauseCode").Value.ToString();//부적합원인코드
			if(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityCauseMeaning").Value == null)
				comm.Parameters.Add("@cause",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@cause",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityCauseMeaning").Value.ToString();//부적합원인
			if(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityStatusCode").Value == null)
				comm.Parameters.Add("@statuscode",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@statuscode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityStatusCode").Value.ToString();//부적합현상코드
			if(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityStatusMeaning").Value == null)
				comm.Parameters.Add("@status",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@status",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityStatusMeaning").Value.ToString();// 부적합현상
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityDetailMeaning").Value == null)
				comm.Parameters.Add("@detail",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@detail",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityDetailMeaning").Text;//부적합세부사유
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityCost").Text == null)
				comm.Parameters.Add("@unsuitabilitycost",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@unsuitabilitycost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityCost").Value.ToString());//부적합금액
			if(m_Grid.Rows[rowcount].Cells.FromKey("InspectionDecisionCode").Value == null)
				comm.Parameters.Add("@decisioncode",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@decisioncode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("InspectionDecisionCode").Value.ToString();//검사판정코드
			if(m_Grid.Rows[rowcount].Cells.FromKey("InspectionDecisionMeaning").Value == null)
				comm.Parameters.Add("@decision",SqlDbType.VarChar).Value= Convert.DBNull;
			else
				comm.Parameters.Add("@decision",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("InspectionDecisionMeaning").Value.ToString();//검사판정


			//comm.Parameters.Add("@LotNum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("LotNum").Value.ToString();

			comm.Parameters.Add("@num",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Value.ToString());//창고번호
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value =UserID;
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();//수정일
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Value.ToString());//출고원장인덱스
			int a = comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 제품출고수정시 이력원장 수정
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="rowcount"></param>
		private void GoodsManufactureOutStorehousePCHistory(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			string str = "Update SH_HT Set Quantity=@Quantity, [Date] = @Date where HistoryDivision='제품출고' and  HistoryIndex=@index ";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString()));//출고수량
			comm.Parameters.Add("@Date", DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Value.ToString()).ToShortDateString());//외주출고일자
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Value.ToString());//출고원장인덱스
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			str = "Delete From SH_HT where StoreName = '원자재창고' and HistoryDivision='제품출고' and  HistoryIndex=@index ";
		}


		/// <summary>
		/// 제품출고수정시 하위품목 이력원장 삭제후 수량변경 추가
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="rowcount"></param>
		private void GoodsManufactureOutStorehousePCSubHistory(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			string str = "Select ItemNum, Quantity From SH_HT where StoreName = '원자재창고' and HistoryDivision='제품출고' and  HistoryIndex=@index ";//
			SqlCommand comm = new SqlCommand();
			comm.CommandText = str;
			comm.Connection = con;
			comm.Transaction = trans;	
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Value.ToString());//출고원장인덱스

			SqlDataAdapter adapter = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
				
			adapter.Fill(ds);

			
			decimal Cost = 0;
			string Child = "";
			for(int a = 0; a < ds.Tables[0].Rows.Count; a++)
			{
				string itemnum = ds.Tables[0].Rows[a]["ItemNum"].ToString();
				int year = int.Parse(m_List[5].ToString());
				int mon = int.Parse(m_List[6].ToString());
				//해당 원자재 수량 취소
				Table table = new Table(year,mon);

			
				SqlCommand comm2 = new SqlCommand();
				comm2.Connection = con;
				comm2.Transaction = trans;
				//창고에서 떨어줄 금액
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
							
				comm2.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = itemnum;
				comm2.CommandText = str;
				SqlDataReader dr3 = comm2.ExecuteReader();
				while(dr3.Read())
				{
					Cost = decimal.Parse(dr3["StandardUnitCost"].ToString());
				}
				dr3.Close();
				comm2.Parameters.Clear();
				//창고에서 떨어줘야 한다
				str = table.OutRowTable();
				decimal quantity = 0;
				quantity = decimal.Parse(ds.Tables[0].Rows[a]["Quantity"].ToString());

				comm2.Parameters.Add("@quantity",-quantity);
				comm2.Parameters.Add("@cost",Cost*(-quantity));
				comm2.Parameters.Add("@num",itemnum);
				comm2.Parameters.Add("@year",year);
				comm2.CommandText = str;
				comm2.ExecuteNonQuery();
				comm2.Parameters.Clear();


				//마이너스재고를 허용하지 않으면 
				if(MinusStore() == false)
				{
					string strSQL="";
					strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = Child;
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.CommandText = strSQL;
					comm.CommandType = CommandType.Text;;
					SqlDataReader reader = comm.ExecuteReader();
						
					string StockQuantity = "False";
					string ItemName = "";
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
							ItemName = Child;
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 출고처리할 수 없습니다.");
					}
					comm.Parameters.Clear();

				}
			}
			comm.Parameters.Clear();

			str = "Delete From SH_HT where StoreName = '원자재창고' and HistoryDivision='제품출고' and  HistoryIndex=@index ";
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Value.ToString());//출고원장인덱스
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			SubItemBOM_Quantity(con,trans, rowcount);
			
		}

		/// <summary>
		/// 제품출고시 원자재 떨어주는 함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private void SubItemBOM_Quantity(SqlConnection con, SqlTransaction trans,int count)
		{
			
			SqlCommand comm = new SqlCommand();
			comm.Transaction = trans;
			comm.Connection = con;

			string str = "", Child="";
			
			
			int year = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("OutStoreDate").Value.ToString()).Year;
			int mon = DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("OutStoreDate").Value.ToString()).Month;
			Table table = new Table(year,mon);

			
			comm.CommandText = "WorkRowBomTree";
			comm.CommandType = CommandType.StoredProcedure;

			comm.Parameters.Add("@ItemNum", SqlDbType.VarChar).Value = m_Grid.Rows[count].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@Quantity",decimal.Parse(m_Grid.Rows[count].Cells.FromKey("OutStorehouseQuantity").Value.ToString()));//출고수량
			comm.Parameters.Add("@Tree", SqlDbType.Bit).Value = 0;

			SqlDataAdapter adapter = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
				
			adapter.Fill(ds);

			decimal Cost = 0;
			for(int a = 0; a < ds.Tables[0].Rows.Count; a++)
			{
				Child = ds.Tables[0].Rows[a]["ItemNum"].ToString();
				SqlCommand comm2 = new SqlCommand();
				comm2.Connection = con;
				comm2.Transaction = trans;
				//창고에서 떨어줄 금액
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
							
				comm2.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = Child;
				comm2.CommandText = str;
				SqlDataReader dr3 = comm2.ExecuteReader();
				while(dr3.Read())
				{
					Cost = decimal.Parse(dr3["StandardUnitCost"].ToString());
				}
				dr3.Close();
				comm2.Parameters.Clear();
				//창고에서 떨어줘야 한다
				str = table.OutRowTable();
				decimal quantity = 0;
				quantity = decimal.Parse(ds.Tables[0].Rows[a]["Quantity"].ToString());

				comm2.Parameters.Add("@quantity",quantity);
				comm2.Parameters.Add("@cost",Cost*quantity);
				comm2.Parameters.Add("@num",Child);
				comm2.Parameters.Add("@year",year);
				comm2.CommandText = str;
				comm2.ExecuteNonQuery();
				comm2.Parameters.Clear();

				//원자재가 출고되는 이력을 기록한다.
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','원자재창고','0',@Quantity,@Date,'제품출고',@HistoryIndex)";
				
						
				comm2.Parameters.Add("@itemnum",ds.Tables[0].Rows[a]["ItemNum"].ToString().Trim());									//품목번호
				comm2.Parameters.Add("@itemdrawnum", ds.Tables[0].Rows[a]["ItemDrawNum"].ToString().Trim());						//도면번호
				comm2.Parameters.Add("@itemname",ds.Tables[0].Rows[a]["ItemName"].ToString().Trim());							//품목명
				comm2.Parameters.Add("@Quantity",quantity);			//입출고 수량
				comm2.Parameters.Add("@Date",DateTime.Parse(m_Grid.Rows[count].Cells.FromKey("OutStoreDate").Value.ToString()).ToShortDateString());	//입출고일
				comm2.Parameters.Add("@HistoryIndex",int.Parse(m_Grid.Rows[count].Cells.FromKey("OutStorehouseHistoryIndex").Value.ToString()));//출고원장인덱스);									//원장번호
				comm2.CommandText = str;
				comm2.ExecuteNonQuery();
				comm2.Parameters.Clear();
				

				//마이너스재고를 허용하지 않으면 
				if(MinusStore() == false)
				{
					string strSQL="";
					strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = Child;
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.CommandText = strSQL;
					comm.CommandType = CommandType.Text;;
					SqlDataReader reader = comm.ExecuteReader();
						
					string StockQuantity = "False";
					string ItemName = "";
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
							ItemName = Child;
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 출고처리할 수 없습니다.");
					}
					comm.Parameters.Clear();

				}
			}
			comm.Parameters.Clear();
			
		}

		
		
		/// <summary>
		/// 수주원장 미검수량변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void GoodsManufactureOutStorehouseRO_Update(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			if(m_Grid.Rows[rowcount].Cells.FromKey("ProgressCondition").Value.ToString() == "대기")
			{
				//먼저 이번출고수량을 출고수량필드에서 빼준다. 미검수량필드도 이번 미검수량을 뺀다.
				string str = @"Update RO_HT Set OutStorehouseQuantity = OutStorehouseQuantity + @OutStorehouseQuantity , UnInspectionQuantity = UnInspectionQuantity + @UnInspectionQuantity where ReceivingOrderHistoryIndex=@index";
				SqlCommand com = new SqlCommand();
				com.Connection = con;
				com.CommandText = str;
				com.Parameters.Add("@OutStorehouseQuantity",-decimal.Parse(m_List[0].ToString()));
				com.Parameters.Add("@UnInspectionQuantity",-decimal.Parse(m_List[1].ToString()));
				com.Parameters.Add("@index",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString()));
				com.Transaction = trans;
				com.ExecuteNonQuery();
				com.Parameters.Clear();

				// 변경된 출고수량과 미검수량을 업데이트 한다
				str = @"Update RO_HT Set OutStorehouseQuantity = OutStorehouseQuantity + @OutStorehouseQuantity , UnInspectionQuantity = UnInspectionQuantity + @UnInspectionQuantity where ReceivingOrderHistoryIndex=@index";
				com.Parameters.Add("@OutStorehouseQuantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString()));
				com.Parameters.Add("@UnInspectionQuantity",decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString()));
				com.Parameters.Add("@index",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString()));
				com.CommandText = str;
				com.ExecuteNonQuery();
				com.Parameters.Clear();
			}
			else
			{
				//이전에 누적된 수량들을 빼고 방금 입력된 수량들을 누적시킨다.
				decimal Total = 0;//총수주량
			
				string str_RO = "Select TotalReceiveingOrderQuantity from RO_HT where ReceivingOrderHistoryIndex = @index";
				SqlCommand comm_RO = new SqlCommand(str_RO,con);
				comm_RO.Transaction = trans;
				comm_RO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());
				SqlDataReader dr_RO = comm_RO.ExecuteReader();
				while(dr_RO.Read())
				{
					Total = decimal.Parse(dr_RO["TotalReceiveingOrderQuantity"].ToString());
				}
				dr_RO.Close();

			
				// 이전수량을 수주원장에 전부 빼준다/
				string str = "Update RO_HT Set OutStorehouseQuantity=@storequantity, UnInspectionQuantity=@uninspection, " +
					" SuitabilityQuantity=@suitability, RemainderQuantity=@remain where ReceivingOrderHistoryIndex=@index ";
				SqlCommand comm = new SqlCommand();
				comm.Connection = con;
				comm.Transaction = trans;
			
				comm.Parameters.Add("@storequantity",SqlDbType.Decimal).Value = -decimal.Parse(m_List[0].ToString());// 이전출고수량
				comm.Parameters.Add("@suitability",SqlDbType.Decimal).Value = -decimal.Parse(m_List[2].ToString());//이전합격수량
				comm.Parameters.Add("@uninspection",SqlDbType.Decimal).Value = -decimal.Parse(m_List[1].ToString());//이전미검수량
				if(m_List[2].ToString() != "0")
					comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = Total-decimal.Parse(m_List[2].ToString());//이전잔량(총수주량에서 이전합격수량을 빼면 이전잔량이 된다)  
				else
					comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = Total-decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//합격수량 입력전의 이전잔량은 현출고량을 빼면나온다
				comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장인덱스
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();


				// 다시 변경되는 수량을 업데이트 해준다.
				str = "Update RO_HT Set OutStorehouseQuantity=@storequantity, UnInspectionQuantity=@uninspection, " +
					" SuitabilityQuantity=@suitability, RemainderQuantity=@remain where ReceivingOrderHistoryIndex=@index ";
				SqlCommand comm1 = new SqlCommand(str,con);
				comm1.Transaction = trans;

				//decimal quantity = 0;
				comm1.Parameters.Add("@storequantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString());// 변경된 출고수량
				if(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Text == null || m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Text == "0")
				{
					comm1.Parameters.Add("@suitability",SqlDbType.Decimal).Value = 0;
					comm1.Parameters.Add("@uninspection",SqlDbType.Decimal).Value = Total - decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//변경된 미검수량은 출고수량이된다
				}
				else
				{
					comm1.Parameters.Add("@suitability",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString());
					comm1.Parameters.Add("@uninspection",SqlDbType.Decimal).Value = 0;//변경된 미검수량 
				}
				if(m_List[2].ToString() == "0")
					comm1.Parameters.Add("@remain",SqlDbType.Decimal).Value = Total;//변경된 잔량(총수주량에서 변경된 합격수량을 빼면 변경잔량이 된다)
				else
					comm1.Parameters.Add("@remain",SqlDbType.Decimal).Value = Total-decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString());//합격수량 입력전의 이전잔량은 현출고량을 빼면나온다
				comm1.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());//수주원장인덱스
				comm1.CommandText = str;
				int a = comm1.ExecuteNonQuery();
				comm1.Parameters.Clear();
			}
		}

		/// <summary>
		/// 출고원장 변경시 수정되는 납품,영업창고
		/// </summary>
		/// <param name="rowcount"></param>
		private void OS_HTTableUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;

			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -시작-  //
			///////////////////////////////////////////
			decimal cost = 0;
			string str_cost = "Select StandardUnitCost From II_MT Where RecodingState =1 and ItemNum = @itemnum";
			SqlCommand comm_cost = new SqlCommand(str_cost,con);
			comm_cost.Transaction = trans;
			comm_cost.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
			SqlDataReader dr = comm_cost.ExecuteReader();
			if(dr.Read())
				cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			dr.Close();
			///////////////////////////////////////////
			// 창고의 금액을 위해 품목정보테이블에서 //
			// 기준단가를 가져와 계산한다.   -끝-    //
			///////////////////////////////////////////
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("ProgressCondition").Value.ToString() == "대기" )
			{
				int year = int.Parse(m_List[5].ToString());
				int mon = int.Parse(m_List[6].ToString());


				Table table = new Table(year,mon);
				
				if(int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Value.ToString()) == 7)
				{
					//일단 보용창고에서 출고수량을 마이너스 증가시킨다(원래수량)
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_List[0].ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) *cost);
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@year",year);
					comm.CommandText = table.OutAddItemTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();


					//보용창고에 변경된 출고수량을 증가시킨다.
					year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Year;
					mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Month;
					Table table2 = new Table(year,mon);
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString()) *cost;
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@year",year);
					comm.CommandText = table2.OutAddItemTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

				}
				else
				{
					//일단 영업창고에서 출고수량을 마이너스 증가시킨다(원래수량)
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_List[0].ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) *cost);
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@storenum",SqlDbType.Int).Value = int.Parse(m_List[4].ToString());
					comm.Parameters.Add("@year",year);
					comm.CommandText = table.OutBusinessTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					// 납품창고 입고수량을 마이너스 증가시킨다(원래수량)
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_List[0].ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) *cost);
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@year",year);
					comm.CommandText = table.InDeliveryTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//영업창고에 변경된 출고수량을 증가시킨다.
					year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Year;
					mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Month;
					Table table1 = new Table(year,mon);
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString()) *cost;
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@storenum",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Value.ToString());
					comm.Parameters.Add("@year",year);
					comm.CommandText = table1.OutBusinessTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//납품창고에 변경된 출고수량을 증가시킨다.
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString()) *cost;
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@year",year);
					comm.CommandText = table1.InDeliveryTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}	
				

			}
			else //이미 매출이 기록된 것에서 부적합 수량을 변경시키는 것
			{
				if(int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Value.ToString()) != 7)
				{
					//일단 영업창고에서 출고수량을 마이너스 증가시킨다(원래수량)
					int year = int.Parse(m_List[5].ToString());
					int mon = int.Parse(m_List[6].ToString());

					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_List[0].ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) *cost);
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@storenum",SqlDbType.Int).Value = int.Parse(m_List[4].ToString());
					comm.Parameters.Add("@year",year);
					Table table = new Table(year,mon);
					comm.CommandText = table.OutBusinessTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					// 납품창고 입고수량을 마이너스 증가시킨다(원래수량)
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(m_List[0].ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[0].ToString()) *cost);
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@year",year);
					comm.CommandText = table.InDeliveryTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//납품창고의 출고수량을 마이너스 증가시킨다 (원래 합격수량+부적합수량)
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[2].ToString())+decimal.Parse(m_List[2].ToString()));
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value =-((decimal.Parse(m_List[2].ToString())+decimal.Parse(m_List[2].ToString())) *cost);
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@year",year);
					comm.CommandText = table.OutDeliveryTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//영업창고에 변경된 출고수량을 증가시킨다.
					year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Year;
					mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Month;
					Table table1 = new Table(year,mon);
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString()) *cost;
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@storenum",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Value.ToString());
					comm.Parameters.Add("@year",year);
					comm.CommandText = table1.OutBusinessTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					//납품창고에 변경된 입고수량을 증가시킨다.
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString());
					comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseQuantity").Value.ToString()) *cost;
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@year",year);
					comm.CommandText = table1.InDeliveryTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
            
					//납품창고에 변경된 출고수량을 증가시킨다.(변경된 합격수량+부적합수량)
					if(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value == null)
					{
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityQuantity").Value.ToString());
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = (decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityQuantity").Value.ToString()))*cost;
					}
					else
					{
						comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString())+decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityQuantity").Value.ToString());
						comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = (decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString()) + decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityQuantity").Value.ToString()))*cost;
					}
					comm.Parameters.Add("@num",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@year",year);
					comm.CommandText = table1.OutDeliveryTable();
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();	
				}
			}
		

			if(int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Value.ToString()) == 7)
			{
				//보용품창고 마이너스 여부
				if(MinusStore() == false)
				{
					int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Year;
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
			else
			{
				//영업창고 마이너스 여부
				if(MinusStore() == false)
				{
					int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Year;
					string strSQL = "SELECT StockQuantity12 FROM BS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and BusinessStorehouseNum = @storenum and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text;
					comm.Parameters.Add("@storenum",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("BusinessStorehouseNum").Value.ToString());
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

				//납품창고 마이너스 여부
				if(MinusStore() == false)
				{
					int year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStoreDate").Text).Year;
					string strSQL = "SELECT StockQuantity12 FROM DS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
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

			
			
		}

		
		
		
		/// <summary>
		/// 매출현황 유효성검사
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private bool SaleHistoryRegistrationPCValidate(int rowcount)
		{
			HttpContext hc = HttpContext.Current;

			if(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString() == "0" || m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString() == "")
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('합격수량을 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else
				return true;
		}

		/// <summary>
		/// 매출원장변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void SaleHistoryRegistrationPCUpdate(SqlConnection con, SqlTransaction trans, int rowcount)//매출원장변경
		{
			string str = "Update S_HT Set SuitabilityQuantity=@suit, ApplyUnitCost = @ApplyUnitCost, TotalCost=@totalcost, SupplementaryValueTaxRate = @rate,"+
				"SupplementaryValueTaxFloatationDate=@taxdate, SaleDate = @SaleDate, UpdatingPerson=@person,UpdatingPersonID=@personid, UpdatingDate=@date "+
				"where SaleHistoryIndex=@index ";
			
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@suit",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString());//합격수량
			comm.Parameters.Add("@ApplyUnitCost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString());//단가
			//comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString())* decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString());//총금액(단가*적합수량)
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("TotalCost").Value.ToString());//총금액
			comm.Parameters.Add("@rate",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SupplementaryValueTaxRate").Value.ToString());//부가세율
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("SupplementaryValueTaxFloatationDate").Text == "" || m_Grid.Rows[rowcount].Cells.FromKey("SupplementaryValueTaxFloatationDate").Value == null)
				comm.Parameters.Add("@taxdate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@taxdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SupplementaryValueTaxFloatationDate").Value.ToString());//계산서발행일
			
			comm.Parameters.Add("@SaleDate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleDate").Value.ToString());//매출일					
			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = UserID;
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();//수정일
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleHistoryIndex").Value.ToString());//매출원장인덱스
			comm.ExecuteNonQuery();
			//			string str = "Update S_HT Set SuitabilityQuantity=@suit, TotalCost=@totalcost, SupplementaryValueTaxRate = @rate,"+
			//				"SupplementaryValueTaxFloatationDate=@taxdate, UpdatingPerson=@person,UpdatingPersonID=@personid, UpdatingDate=@date "+
			//				"where SaleHistoryIndex=@index ";
			//			
			//			SqlCommand comm = new SqlCommand(str,con);
			//			comm.Transaction = trans;
			//			
			//			comm.Parameters.Add("@suit",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString());//합격수량
			//			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("TotalCost").Value.ToString());//총금액
			//			comm.Parameters.Add("@rate",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SupplementaryValueTaxRate").Value.ToString());//부가세율
			//			
			//			if(m_Grid.Rows[rowcount].Cells.FromKey("SupplementaryValueTaxFloatationDate").Text == "" || m_Grid.Rows[rowcount].Cells.FromKey("SupplementaryValueTaxFloatationDate").Value == null)
			//				comm.Parameters.Add("@taxdate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			//			else
			//				comm.Parameters.Add("@taxdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SupplementaryValueTaxFloatationDate").Value.ToString());//계산서발행일
			//			
			//			comm.Parameters.Add("@person",SqlDbType.VarChar).Value = UserName;
			//			comm.Parameters.Add("@personid",SqlDbType.VarChar).Value = UserID;
			//			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();//수정일
			//			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleHistoryIndex").Value.ToString());//매출원장인덱스
			//			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 매입매출테이블 미수금액변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void SaleHistoryRegistrationS_HTUpdate(SqlConnection con, SqlTransaction trans,int rowcount)//매입매출테이블금액수정
		{
			int year = int.Parse(m_List[3].ToString());
			int mon = int.Parse(m_List[4].ToString());
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			//이전의 총금액을 마이너스 증가
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
				
			if(m_Grid.Rows[rowcount].Cells.FromKey("TotalCost").Text.Trim() == null || m_List[1].ToString().Trim() == "")
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = 0;
			else
				comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(m_List[1].ToString()));//이전총금액
			comm.Parameters.Add("@year",year);

			Table table = new Table(year,mon);
			comm.CommandText = table.CollectMoneyIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleDate").Text).Year;
			mon =  DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SaleDate").Text).Month;

			//변경금액 증가
			comm.Parameters.Add("@comnum", SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("BusinessRegistrationNum").Text;
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("TotalCost").Text);//총금액
			comm.Parameters.Add("@year",year);
			Table table1 = new Table(year,mon);
			comm.CommandText = table1.CollectMoneyIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}

		/// <summary>
		/// 수주원장 합격수량 잔량,미검수량변경
		/// 합격수량은 기존의 수주원장 합격수량에 매출원장에 이전에기록된 합격수량을 빼고 새로운 합격수량을 입력 
		/// 잔량은 총수주량에 변경된 합격수량을 누적한다.
		/// </summary>
		/// <param name="rowcount"></param>
		private void SaleHistoryRegistrationRO_HTUpdate(SqlConnection con, SqlTransaction trans,int rowcount)
		{
			//이전 합격수량을 빼고 잔량에 합격수량을 더한다.
			string str = "Update RO_HT Set SuitabilityQuantity = SuitabilityQuantity - @suit, RemainderQuantity = RemainderQuantity + @suit Where ReceivingOrderHistoryIndex = @index";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction =trans;
			comm.Parameters.Add("@suit",SqlDbType.Decimal).Value =  decimal.Parse(m_List[0].ToString());
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//그리고 새로운 합격수량을 더하고 과 잔량에 새합격수량을 뺀다.
			str = "Update RO_HT Set SuitabilityQuantity = SuitabilityQuantity + @suit, RemainderQuantity = RemainderQuantity - @suit Where ReceivingOrderHistoryIndex = @index";
			comm.Parameters.Add("@suit",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString());
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//			//일단 기존의 합격수량과 잔량을 빼놓는다.
			//			decimal suit = 0;//합격수량
			//			decimal remain = 0;//잔량
			//			string str_RO = "Select SuitabilityQuantity,RemainderQuantity from RO_HT  Where ReceivingOrderHistoryIndex = @index";
			//			SqlCommand comm_RO = new SqlCommand(str_RO, con);
			//			comm_RO.Transaction = trans;
			//			comm_RO.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());
			//			SqlDataReader dr_RO = comm_RO.ExecuteReader();
			//			while(dr_RO.Read())
			//			{
			//				suit = decimal.Parse(dr_RO["SuitabilityQuantity"].ToString());
			//				remain =decimal.Parse(dr_RO["RemainderQuantity"].ToString());
			//			}
			//			dr_RO.Close();
			//
			//			// 
			//			string str = "Update RO_HT Set SuitabilityQuantity = @suit, RemainderQuantity = @remain Where ReceivingOrderHistoryIndex = @index";
			//            SqlCommand comm = new SqlCommand();
			//			comm.Connection = con;
			//			comm.Transaction =trans;
			//			comm.Parameters.Add("@suit",SqlDbType.Decimal).Value = suit - decimal.Parse(m_List[0].ToString());
			//			comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = remain + decimal.Parse(m_List[0].ToString());
			//			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());
			//			comm.CommandText = str;
			//			comm.ExecuteNonQuery();
			//			comm.Parameters.Clear();
			//
			//			//그리고 새로운 합격수량과 잔량을 더한다.
			//			str = "Update RO_HT Set SuitabilityQuantity = @suit, RemainderQuantity = @remain Where ReceivingOrderHistoryIndex = @index";
			//			comm.Parameters.Add("@suit",SqlDbType.Decimal).Value = suit + decimal.Parse(m_List[0].ToString());
			//			comm.Parameters.Add("@remain",SqlDbType.Decimal).Value = remain - decimal.Parse(m_List[0].ToString());
			//			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ReceivingOrderHistoryIndex").Value.ToString());
			//			comm.CommandText = str;
			//			comm.ExecuteNonQuery();
			//			comm.Parameters.Clear();
		}

		/// <summary>
		/// 출고원장의 합격수량과 부적함수량변경
		/// 출고원장의 출고량에 변경된 합격수량을 뺀것이 부적함 수량
		/// 
		/// </summary>
		/// <param name="rowcount"></param>
		private void SaleHistoryRegistrationOS_HTUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			//일단 기존의 출고량을 가져온다
			decimal outstore = 0;//기존출고량
			string str_OS = "Select OutStorehouseQuantity from OS_HT  Where OutStorehouseHistoryIndex = @index";
			SqlCommand comm_OS = new SqlCommand(str_OS, con);
			comm_OS.Transaction = trans;
			comm_OS.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Value.ToString());
			SqlDataReader dr_OS = comm_OS.ExecuteReader();
			while(dr_OS.Read())
			{
				outstore =decimal.Parse(dr_OS["OutStorehouseQuantity"].ToString());
				
			}
			dr_OS.Close();

			
			//변경된 합격수량과 불합격수량을 적용한다
			string str = "Update OS_HT Set SuitabilityQuantity=@suit, UnSuitabilityQuantity=@unsuit where OutStorehouseHistoryIndex=@index ";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@suit",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString());//합격수량
			comm.Parameters.Add("@unsuit",SqlDbType.Decimal).Value = outstore - (decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString()));//부적합수량
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutStorehouseHistoryIndex").Value.ToString());//출고원장인덱스
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 생산의뢰현황 유효성 검사
		/// </summary>
		/// <param name="rowcount"></param>
		/// <returns></returns>
		private bool ProductionRequestPCValidate(int rowcount)
		{
			HttpContext hc = HttpContext.Current;

			//if(m_Grid.Rows[rowcount].Cells.FromKey("ProductionRequestSourceCode").Value.ToString().Trim() == "" || m_Grid.Rows[rowcount].Cells.FromKey("ProductionRequestSource").Value.ToString().Trim() == "정상")
			if(m_Grid.Rows[rowcount].Cells.FromKey("ProductionRequestSourceCode").Value.ToString().Trim() == "")
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('의뢰원천을 올바르게 선택 해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity1").Value.ToString() == "0" || m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity1").Value.ToString() == "")
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('1차 납기요구량은 반드시 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else if(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate1").Text == "" || m_Grid.Rows[rowcount].Cells.FromKey("RequestDate1").Value == null)
			{
				hc.Response.Write("<script language=javascript>");
				hc.Response.Write("alert('1차 납기요구일은 반드시 입력해주세요!');");
				hc.Response.Write("</script>");
				return false;
			}
			else

				return true;
		}

		/// <summary>
		/// 생산의뢰원장 변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void ProductionRequestPCUpdate(SqlConnection con, SqlTransaction trans,int rowcount)
		{

			string str = " UPDATE PR_HT SET ProductionRequestSourceCode = @code, ProductionRequestSource = @source, "+
				" RequestQuantity1 = @quantity1, RequestDate1 = @date1, " +
				" RequestQuantity2 = @quantity2, RequestDate2 = @date2, " +
				" RequestQuantity3 = @quantity3, RequestDate3 = @date3, " +
				" RequestQuantity4 = @quantity4, RequestDate4 = @date4, " +
				" RequestQuantity5 = @quantity5, RequestDate5 = @date5, " +
				" ProductionRequestQuantity = @total Where ProductionRequestHistoryIndex = @index";

			SqlCommand comm = new SqlCommand(str, con);
			comm.Transaction = trans;
			
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProductionRequestSourceCode").Value.ToString().Trim();
			comm.Parameters.Add("@source",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProductionRequestSource").Value.ToString().Trim();

			if(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity1").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity1").Text == "")
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@quantity1",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity1").Value.ToString().Trim());

			if(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity2").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity2").Text == "")
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@quantity2",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity2").Value.ToString().Trim());

			if(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity3").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity3").Text == "")
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@quantity3",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity3").Value.ToString().Trim());

			if(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity4").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity4").Text == "")
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@quantity4",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity4").Value.ToString().Trim());

			if(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity5").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity5").Text == "")
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = Convert.DBNull;
			else 
				comm.Parameters.Add("@quantity5",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestQuantity5").Value.ToString().Trim());

			comm.Parameters.Add("@date1",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate1").Value.ToString().Trim());
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate2").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("date2").Text == "")
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value =Convert.DBNull;
			else
				comm.Parameters.Add("@date2",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate2").Value.ToString().Trim());

			if(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate3").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("date3").Text == "")
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date3",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate3").Value.ToString().Trim());
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate4").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("date4").Text == "")
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@date4",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate4").Value.ToString().Trim());
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate5").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("date5").Text == "")
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value =Convert.DBNull;
			else
				comm.Parameters.Add("@date5",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RequestDate5").Value.ToString().Trim());

			comm.Parameters.Add("@total",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ProductionRequestQuantity").Value.ToString());
			comm.Parameters.Add("@index",SqlDbType.Decimal).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ProductionRequestHistoryIndex").Value.ToString());

			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 작업일보원장 수정
		/// </summary>
		/// <param name="rowcount"></param>
		private void WorkDailyReportUpdate(SqlConnection con, SqlTransaction trans,int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;


			string str = @" UPDATE WDR_HT SET WCInfoIndex=@WCInfoIndex, WCName=@WCName, WorkPlanQuantity=@WorkPlanQuantity, 
				 WorkCompletionQuantity=@WorkCompletionQuantity,ThisWorkCompletionQuantity=@ThisWorkCompletionQuantity, 
				 RemainQuantity=@RemainQuantity, SuitabilityQuantity=@SuitabilityQuantity, 
				 UnSuitabilityQuantity=@UnSuitabilityQuantity, UnSuitabilityCost=@UnSuitabilityCost, 
				 WorkBeginTime = @WorkBeginTime, WorkEndTime = @WorkEndTime, Worker=@Worker, WorkerID=@WorkerID, 
				 NonWorkTime1=@NonWorkTime1, NonWorkTimeCode1=@NonWorkTimeCode1, NonWorkTimeReason1=@NonWorkTimeReason1, 
				 NonWorkTime2=@NonWorkTime2, NonWorkTimeCode2=@NonWorkTimeCode2, NonWorkTimeReason2=@NonWorkTimeReason2, 
				 NonWorkTime3=@NonWorkTime3, NonWorkTimeCode3=@NonWorkTimeCode3, NonWorkTimeReason3=@NonWorkTimeReason3, 
				 UnSuitabilityStatusCode=@UnSuitabilityStatusCode, UnSuitabilityStatusMeaning=@UnSuitabilityStatusMeaning, 
				 UnSuitabilityDetailMeaning=@UnSuitabilityDetailMeaning, UnSuitabilityCauseCode=@UnSuitabilityCauseCode, 
				 UnSuitabilityCauseMeaning=@UnSuitabilityCauseMeaning, InspectionDecisionCode=@InspectionDecisionCode, 
				 InspectionDecision=@InspectionDecision, 
				 EtcNum1 = @EtcNum1, EtcNum2 = @EtcNum2,
				 UseTool1=@UseTool1, UseJig1=@UseJig1, UseTool2=@UseTool2, UseJig2=@UseJig2, UseTool3=@UseTool3, UseJig3=@UseJig3, 
				 UpdatingPerson=@UpdatingPerson, UpdatingPersonID=@UpdatingPersonID, UpdatingDate=@UpdatingDate 
				 Where WorkDailyReportHistoryIndex=@WorkDailyReportHistoryIndex ";

			comm.CommandText = str;
			
			comm.Parameters.Add("@WCInfoIndex",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WCInfoIndex").Value.ToString());
			comm.Parameters.Add("@WCName",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("WCName").Value.ToString().Trim();
			comm.Parameters.Add("@WorkPlanQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkPlanQuantity").Value.ToString());
			comm.Parameters.Add("@WorkCompletionQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkCompletionQuantity").Value.ToString());
			comm.Parameters.Add("@ThisWorkCompletionQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Value.ToString());
			comm.Parameters.Add("@RemainQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("RemainQuantity").Value.ToString());
			comm.Parameters.Add("@SuitabilityQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString());
			comm.Parameters.Add("@UnSuitabilityQuantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityQuantity").Value.ToString());
			comm.Parameters.Add("@UnSuitabilityCost",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityCost").Value.ToString());
			comm.Parameters.Add("@WorkBeginTime",SqlDbType.SmallDateTime).Value = Convert.ToDateTime(m_List[10].ToString()).ToString();
			comm.Parameters.Add("@WorkEndTime",SqlDbType.SmallDateTime).Value = Convert.ToDateTime(m_List[11].ToString()).ToString();
			comm.Parameters.Add("@Worker",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("Worker").Value.ToString().Trim();
			comm.Parameters.Add("@WorkerID",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("WorkerID").Value.ToString().Trim();
			comm.Parameters.Add("@NonWorkTime1",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTime1").Value.ToString());
			if(m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTimeCode1").Value == null)
			{
				comm.Parameters.Add("@NonWorkTimeCode1",SqlDbType.VarChar).Value = Convert.DBNull;
				comm.Parameters.Add("@NonWorkTimeReason1",SqlDbType.VarChar).Value = Convert.DBNull;
			}
			else
			{
				comm.Parameters.Add("@NonWorkTimeCode1",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTimeCode1").Value.ToString();
				comm.Parameters.Add("@NonWorkTimeReason1",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTimeReason1").Value.ToString();
			}
			comm.Parameters.Add("@NonWorkTime2",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTime2").Value.ToString());
			if(m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTimeCode2").Value == null)
			{
				comm.Parameters.Add("@NonWorkTimeCode2",SqlDbType.VarChar).Value = Convert.DBNull;
				comm.Parameters.Add("@NonWorkTimeReason2",SqlDbType.VarChar).Value = Convert.DBNull;
			}
			else
			{
				comm.Parameters.Add("@NonWorkTimeCode2",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTimeCode2").Value.ToString();
				comm.Parameters.Add("@NonWorkTimeReason2",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTimeReason2").Value.ToString();
			}
			comm.Parameters.Add("@NonWorkTime3",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTime3").Value.ToString());
			if(m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTimeCode3").Value == null)
			{
				comm.Parameters.Add("@NonWorkTimeCode3",SqlDbType.VarChar).Value = Convert.DBNull;
				comm.Parameters.Add("@NonWorkTimeReason3",SqlDbType.VarChar).Value = Convert.DBNull;
			}
			else
			{
				comm.Parameters.Add("@NonWorkTimeCode3",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTimeCode3").Value.ToString().Trim();
				comm.Parameters.Add("@NonWorkTimeReason3",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("NonWorkTimeReason3").Value.ToString();
			}

			if(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityStatusCode").Value == null)
			{
				comm.Parameters.Add("@UnSuitabilityStatusCode",SqlDbType.VarChar).Value = Convert.DBNull;
				comm.Parameters.Add("@UnSuitabilityStatusMeaning",SqlDbType.VarChar).Value = Convert.DBNull;
			}
			else
			{
				comm.Parameters.Add("@UnSuitabilityStatusCode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityStatusCode").Value.ToString().Trim();
				comm.Parameters.Add("@UnSuitabilityStatusMeaning",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityStatusMeaning").Value.ToString().Trim();
			}
			
			comm.Parameters.Add("@UnSuitabilityDetailMeaning",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityDetailMeaning").Value.ToString();
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityCauseCode").Value == null)
			{
				comm.Parameters.Add("@UnSuitabilityCauseCode",SqlDbType.VarChar).Value = Convert.DBNull;
				comm.Parameters.Add("@UnSuitabilityCauseMeaning",SqlDbType.VarChar).Value = Convert.DBNull;
			}
			else
			{
				comm.Parameters.Add("@UnSuitabilityCauseCode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityCauseCode").Value.ToString();
				comm.Parameters.Add("@UnSuitabilityCauseMeaning",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UnSuitabilityCauseMeaning").Value.ToString();
			}
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("InspectionDecisionCode").Value == null)
			{
				comm.Parameters.Add("@InspectionDecisionCode",SqlDbType.VarChar).Value = Convert.DBNull;
				comm.Parameters.Add("@InspectionDecision",SqlDbType.VarChar).Value = Convert.DBNull;
			}
			else
			{
				comm.Parameters.Add("@InspectionDecisionCode",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("InspectionDecisionCode").Value.ToString();
				comm.Parameters.Add("@InspectionDecision",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("InspectionDecision").Value.ToString();
			}

			if(m_Grid.Rows[rowcount].Cells.FromKey("EtcNum1").Text == null)
				comm.Parameters.Add("@EtcNum1",Convert.DBNull);
			else
				comm.Parameters.Add("@EtcNum1",m_Grid.Rows[rowcount].Cells.FromKey("EtcNum1").Text);
			if(m_Grid.Rows[rowcount].Cells.FromKey("EtcNum2").Text == null)
				comm.Parameters.Add("@EtcNum2",Convert.DBNull);
			else
				comm.Parameters.Add("@EtcNum2",m_Grid.Rows[rowcount].Cells.FromKey("EtcNum2").Text);
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseTool1").Text == null)
				comm.Parameters.Add("@UseTool1",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@UseTool1",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseTool1").Value.ToString();
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseTool2").Text == null)
				comm.Parameters.Add("@UseTool2",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@UseTool2",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseTool2").Value.ToString();
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseTool3").Text == null)
				comm.Parameters.Add("@UseTool3",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@UseTool3",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseTool3").Value.ToString();
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseJig1").Text == null)
				comm.Parameters.Add("@UseJig1",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@UseJig1",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseJig1").Value.ToString();
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseJig2").Text == null)
				comm.Parameters.Add("@UseJig2",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@UseJig2",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseJig2").Value.ToString();
			
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseJig3").Text == null)
				comm.Parameters.Add("@UseJig3",SqlDbType.VarChar).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@UseJig3",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("UseJig3").Value.ToString();
			
			comm.Parameters.Add("@UpdatingPerson",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@UpdatingPersonID",SqlDbType.VarChar).Value = UserID;
			comm.Parameters.Add("@UpdatingDate",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@WorkDailyReportHistoryIndex",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			
			//지금수정한 품목이 최종공정이고 검사품인경우 품질검사원장의 검사의뢰수량도 변경해야한다
			// 또한 무검사인경우는 영업창고입고의뢰원장의 의뢰수량도 변경해야 한다.

			// 지금공정이 검사이고 최종공정이면 품질검사원장에 레코드를 등록해야 한다.
			// 최종공정은 해당품목의 지금공정순서보다 큰게 없으면 최종공정이다.
			str = "Select count(*) From PSI_MT Where RecodingState = 1 and ItemNum = @itemnum and ProcessSequenceNum > @num";
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@num",SqlDbType.TinyInt).Value = m_Grid.Rows[rowcount].Cells.FromKey("ProcessSequenceNum").Value.ToString();
			comm.CommandText = str;
			int a = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			
			if(Check(m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString()))//검사품목이면서
			{
				if(m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString() == m_Grid.Rows[rowcount].Cells.FromKey("ProductItemNum").Value.ToString() && a == 0 )//최종공정인경우 품질검사원장에 수량변경
				{
					
					//품질검사원장의 이전의뢰수량을 변경
					str = "Update QI_HT Set RequestQuantity = @quantity Where HistoryIndex1 = @index and HistorySection1 = '작업일보'";
					comm.CommandText = str;
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString());
					comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					
				}
			}
			else//무검사품목이면서
			{
				if(m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString() == m_Grid.Rows[rowcount].Cells.FromKey("ProductItemNum").Value.ToString() && a == 0)//최종공정인경우 입고의뢰원장에 수량변경
				{
					
					//영업창고입고의뢰원장의 의뢰수량을 변경
					str = "Update ISR_HT Set InstorehouseRequestQuantity = @quantity Where HistoryIndex = @index and HistorySection1 = '작업일보'";
					comm.CommandText = str;
					comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkCompletionQuantity").Value.ToString());
					comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkDailyReportHistoryIndex").Value.ToString());
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}
			}
		}

		

		/// <summary>
		/// 창고 업데이트
		/// </summary>
		private void StoreTableUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			//이전의 완료수량만큼 창고에서 빼고 지금 수정된 수량만큼 창고에 입력한다.
			//이때 떨어준 수량도 이전에 떨어준수량은 더하고 지금 떨어준 수량을 빼준다.
			
			int year  = int.Parse(m_List[12].ToString());
			int mon = int.Parse(m_List[13].ToString());
			

			decimal Cost = 0;

			////////////////////////////////////////////////////////////////////////////////////
			// 이전 하위품목이 원자재이면 원자재 창고의 출고수량(BOM에 맞는수량) 마이너스증가 //
			// 반제품 이면 생산창고의 출고수량(BOM에 맞는수량) 마이너스증가   -시작-          //
			////////////////////////////////////////////////////////////////////////////////////
					
			string str = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			comm.CommandType = CommandType.StoredProcedure;
			

			str = "SPTotalNeedCalculation";
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString());
			comm.Parameters.Add("@Quantity", decimal.Parse(m_List[2].ToString()));
			comm.Parameters.Add("@Tree", SqlDbType.Bit).Value = 1;
			
			SqlDataAdapter da = new SqlDataAdapter(comm);

			DataSet ds = new DataSet();

			da.Fill(ds);
			comm.Parameters.Clear();

			
			Table table = new Table(year,mon);

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				SqlCommand comm2 = new SqlCommand();
				comm2.Connection = con;
				comm2.Transaction = trans;

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
				//창고에서 떨어줘야 한다
				str = table.OutRowTable();
				comm2.Parameters.Add("@quantity",-(decimal.Parse(dr["Quantity"].ToString())));
				comm2.Parameters.Add("@cost",-(Cost*decimal.Parse(dr["Quantity"].ToString())));
				comm2.Parameters.Add("@num",dr["ItemNum"].ToString());
				comm2.Parameters.Add("@year",year);
				comm2.CommandText = str;
				comm2.ExecuteNonQuery();
				comm2.Parameters.Clear();



				//마이너스재고를 허용하지 않으면 
				if(MinusStore() == false)
				{
					string strSQL="SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = dr["ItemNum"].ToString();
					comm.Parameters.Add("@year",DateTime.Now.Year);
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
						
					string StockQuantity = "False";
					string ItemName = "";
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
							ItemName = dr["ItemNum"].ToString();
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 입고처리할 수 없습니다.");
					}
					comm.Parameters.Clear();

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
			decimal rate = ProgressRate(m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString(),m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text,con,trans);
			// 가공된 지금 품목의 생산창고 입고
			str = table.InProductionTable();
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -(decimal.Parse(m_List[2].ToString()));
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(Cost*decimal.Parse(m_List[2].ToString())*rate);
			comm.Parameters.Add("@num",m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString());
			comm.Parameters.Add("@code",m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value.ToString());
			comm.Parameters.Add("@year",year);
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			////////////////////////////////////////////////////////////////////////////
			// 작업한 하위품목이 원자재이면 원자재 창고의 출고수량(BOM에 맞는수량)증가//
			// 반제품 이면 생산창고의 출고수량(BOM에 맞는수량)증가   -끝-             //
			////////////////////////////////////////////////////////////////////////////


			//////////////////////////////////////////////////////////////
			// 변경된 작업완료수량만큼 떨어주고 입고시키는 작업	-시작-	//
			//////////////////////////////////////////////////////////////
			
			year = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkEndTime").Value.ToString()).Year;
			mon = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("WorkEndTime").Value.ToString()).Month;

			Table table1 = new Table(year,mon);

			
			

			str = "SPTotalNeedCalculation";
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Parameters.Add("@ItemNum", m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value.ToString());
			comm.Parameters.Add("@Quantity", decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Value.ToString()));
			comm.Parameters.Add("@Tree", SqlDbType.Bit).Value = 1;
			
			SqlDataAdapter da1 = new SqlDataAdapter(comm);

			DataSet ds1 = new DataSet();

			da1.Fill(ds1);
			comm.Parameters.Clear();

			
			
			foreach(DataRow dr in ds1.Tables[0].Rows)
			{
				SqlCommand comm2 = new SqlCommand();
				comm2.Connection = con;
				comm2.Transaction = trans;

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
				//창고에서 떨어줘야 한다
				str = table1.OutRowTable();
				comm2.Parameters.Add("@quantity",decimal.Parse(dr["Quantity"].ToString()));
				comm2.Parameters.Add("@cost",Cost*decimal.Parse(dr["Quantity"].ToString()));
				comm2.Parameters.Add("@num",dr["ItemNum"].ToString());
				comm2.Parameters.Add("@year",year);
				comm2.CommandText = str;
				comm2.ExecuteNonQuery();
				comm2.Parameters.Clear();



				//마이너스재고를 허용하지 않으면 
				if(MinusStore() == false)
				{
					string strSQL="SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and [Year] = @year";
					comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = dr["ItemNum"].ToString();
					comm.Parameters.Add("@year",year);
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
						
					string StockQuantity = "False";
					string ItemName = "";
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
							ItemName = dr["ItemNum"].ToString();
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량 부족으로 입고처리할 수 없습니다.");
					}
					comm.Parameters.Clear();

				}


				
			}

			//제품 창고입고품목의 창고금액
			Cost = 0;
			str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
			comm.Parameters.Add("@itemnum",m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value);
			comm.CommandText = str;
			SqlDataReader dr_Cost1 = comm.ExecuteReader();
			while(dr_Cost1.Read())
			{
				Cost = decimal.Parse(dr_Cost1["StandardUnitCost"].ToString());
			}
			dr_Cost1.Close();
			comm.Parameters.Clear();
			decimal rate1 = ProgressRate(m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Text, m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Text, con,trans);
			// 가공된 지금 품목의 생산창고 입고
			str = table1.InProductionTable();
			comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Text);
			comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = Cost*decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SuitabilityQuantity").Text)*rate1;
			comm.Parameters.Add("@num",m_Grid.Rows[rowcount].Cells.FromKey("ItemNum").Value);
			comm.Parameters.Add("@code",m_Grid.Rows[rowcount].Cells.FromKey("ProcessCode").Value);
			comm.Parameters.Add("@year",year);
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			//////////////////////////////////////////////////////////////
			// 변경된 작업완료수량만큼 떨어주고 입고시키는 작업	-종료-	//
			//////////////////////////////////////////////////////////////
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
		private void EI_MTMinusUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			string str = "";	
			//설비정보에 공구1의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200010'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List[4].ToString();
			comm.CommandText = str;
			SqlDataAdapter daShot = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilder = new SqlCommandBuilder(daShot) ;
			DataSet dsShot = new DataSet() ;
			daShot.Fill(dsShot) ;
			if(dsShot.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp = Math.Round((decimal.Parse(m_List[0].ToString()) / decimal.Parse(dsShot.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsShot.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot.Tables[0].Rows[0]["TotalShot"].ToString())-tmp;
				dsShot.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot.Tables[0].Rows[0]["WorkShot"].ToString())-tmp;

				daShot.Update(dsShot) ;
			}
			//설비정보에 공구1의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();

			//설비정보에 치구1의 작업shot을 누적하는 부분......시작
					
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200020'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List[5].ToString();
			comm.CommandText = str;
			SqlDataAdapter daJig = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilderJig = new SqlCommandBuilder(daJig) ;
			DataSet dsJig = new DataSet() ;
			daJig.Fill(dsJig) ;
			if(dsJig.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig = Math.Round((decimal.Parse(m_List[0].ToString()) / decimal.Parse(dsJig.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsJig.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsJig.Tables[0].Rows[0]["TotalShot"].ToString())-tmpJig;
				dsJig.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsJig.Tables[0].Rows[0]["WorkShot"].ToString())-tmpJig;
				daJig.Update(dsJig) ;
			}
			//설비정보에 치구1의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();
					
			//설비정보에 공구2의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200010'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List[6].ToString();
			comm.CommandText = str;				
			SqlDataAdapter daShot1 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilder1 = new SqlCommandBuilder(daShot1) ;
			DataSet dsShot1 = new DataSet() ;
			daShot1.Fill(dsShot1) ;
			if(dsShot1.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp1 = Math.Round((decimal.Parse(m_List[0].ToString()) / decimal.Parse(dsShot1.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsShot1.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot1.Tables[0].Rows[0]["TotalShot"].ToString())-tmp1;
				dsShot1.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot1.Tables[0].Rows[0]["WorkShot"].ToString())-tmp1;
						
				daShot1.Update(dsShot1);
			}
			//설비정보에 공구2의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();

			//설비정보에 치구2의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200020'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List[7].ToString();
			comm.CommandText = str;
			SqlDataAdapter daJig1 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilderJig1 = new SqlCommandBuilder(daJig1) ;
			DataSet dsJig1 = new DataSet() ;
			daJig1.Fill(dsJig1) ;
			if(dsJig1.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig1 = Math.Round((decimal.Parse(m_List[0].ToString()) / decimal.Parse(dsJig1.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsJig1.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsJig1.Tables[0].Rows[0]["TotalShot"].ToString()) -tmpJig1;
				dsJig1.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsJig1.Tables[0].Rows[0]["WorkShot"].ToString()) -tmpJig1;
				daJig1.Update(dsJig1) ;
			}
			//설비정보에 치구2의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();


			//설비정보에 공구3의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200010'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List[8].ToString();
			comm.CommandText = str;				
			SqlDataAdapter daShot2 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilder2 = new SqlCommandBuilder(daShot2) ;
			DataSet dsShot2 = new DataSet() ;
			daShot2.Fill(dsShot2) ;
			if(dsShot2.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp2 = Math.Round((decimal.Parse(m_List[0].ToString()) / decimal.Parse(dsShot2.Tables[0].Rows[0]["Cavity"].ToString())));
				dsShot2.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot2.Tables[0].Rows[0]["TotalShot"].ToString()) -tmp2;
				dsShot2.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot2.Tables[0].Rows[0]["WorkShot"].ToString())-tmp2;
				daShot2.Update(dsShot2) ;
			}
			//설비정보에 공구3의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();

			//설비정보에 치구3의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200020'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = m_List[9].ToString();
			comm.CommandText = str;
			SqlDataAdapter daJig2 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilderJig2 = new SqlCommandBuilder(daJig2) ;
			DataSet dsJig2 = new DataSet() ;
			daJig2.Fill(dsJig2) ;
			if(dsJig2.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig2 = Math.Round((decimal.Parse(m_List[0].ToString()) / decimal.Parse(dsJig2.Tables[0].Rows[0]["Cavity"].ToString())));
				dsJig2.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsJig2.Tables[0].Rows[0]["TotalShot"].ToString())-tmpJig2;
				dsJig2.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsJig2.Tables[0].Rows[0]["WorkShot"].ToString())-tmpJig2;
				daJig2.Update(dsJig2) ;
			}
			//설비정보에 치구3의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();
		}


		/// <summary>
		/// 설비정보내의 사용공구,치구변경시 누적샷 변경
		/// </summary>
		private void EI_MTUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			comm.Transaction = trans;
			string str = "";	
			string Tool1 = "";
			string Tool2 ="";
			string Tool3 = "";
			string Jig1="";
			string Jig2="";
			string Jig3="";
			//설비정보에 공구1의 작업shot을 누적하는 부분......시작
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseTool1").Text != null)
				Tool1 = m_Grid.Rows[rowcount].Cells.FromKey("UseTool1").Value.ToString();
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseTool2").Text != null)
				Tool2 = m_Grid.Rows[rowcount].Cells.FromKey("UseTool2").Value.ToString();
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseTool3").Text != null)
				Tool3 = m_Grid.Rows[rowcount].Cells.FromKey("UseTool3").Value.ToString();
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseJig1").Text != null)
				Jig1 = m_Grid.Rows[rowcount].Cells.FromKey("UseJig1").Value.ToString();
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseJig2").Text != null)
				Jig2 = m_Grid.Rows[rowcount].Cells.FromKey("UseJig2").Value.ToString();
			if(m_Grid.Rows[rowcount].Cells.FromKey("UseJig3").Text != null)
				Jig3 = m_Grid.Rows[rowcount].Cells.FromKey("UseJig3").Value.ToString();
				
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200010'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = Tool1;
			comm.CommandText = str;
			SqlDataAdapter daShot = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilder = new SqlCommandBuilder(daShot) ;
			DataSet dsShot = new DataSet() ;
			daShot.Fill(dsShot) ;
			if(dsShot.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Text) / decimal.Parse(dsShot.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsShot.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot.Tables[0].Rows[0]["WorkShot"].ToString()) + tmp;
				dsShot.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot.Tables[0].Rows[0]["TotalShot"].ToString()) + decimal.Parse(dsShot.Tables[0].Rows[0]["WorkShot"].ToString());
				daShot.Update(dsShot) ;
			}
			//설비정보에 공구1의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();

			//설비정보에 치구1의 작업shot을 누적하는 부분......시작
					
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200020'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = Jig1;
			comm.CommandText = str;
			SqlDataAdapter daJig = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilderJig = new SqlCommandBuilder(daJig) ;
			DataSet dsJig = new DataSet() ;
			daJig.Fill(dsJig) ;
			if(dsJig.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Text) / decimal.Parse(dsJig.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsJig.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsJig.Tables[0].Rows[0]["WorkShot"].ToString()) + tmpJig;
				dsJig.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsJig.Tables[0].Rows[0]["TotalShot"].ToString()) + decimal.Parse(dsJig.Tables[0].Rows[0]["WorkShot"].ToString());
				daJig.Update(dsJig) ;
			}
			//설비정보에 치구1의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();
					
			//설비정보에 공구2의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200010'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = Tool2;
			comm.CommandText = str;				
			SqlDataAdapter daShot1 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilder1 = new SqlCommandBuilder(daShot1) ;
			DataSet dsShot1 = new DataSet() ;
			daShot1.Fill(dsShot1) ;
			if(dsShot1.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp1 = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Text) / decimal.Parse(dsShot1.Tables[0].Rows[0]["Cavity"].ToString())));
				dsShot1.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot1.Tables[0].Rows[0]["WorkShot"].ToString()) + tmp1;
				dsShot1.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot1.Tables[0].Rows[0]["TotalShot"].ToString()) + decimal.Parse(dsShot1.Tables[0].Rows[0]["WorkShot"].ToString());
						
				daShot1.Update(dsShot1);
			}
			//설비정보에 공구2의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();

			//설비정보에 치구2의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200020'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = Jig2;
			comm.CommandText = str;
			SqlDataAdapter daJig1 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilderJig1 = new SqlCommandBuilder(daJig1) ;
			DataSet dsJig1 = new DataSet() ;
			daJig1.Fill(dsJig1) ;
			if(dsJig1.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig1 = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Text) / decimal.Parse(dsJig1.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsJig1.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsJig1.Tables[0].Rows[0]["WorkShot"].ToString()) + tmpJig1;
				dsJig1.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsJig1.Tables[0].Rows[0]["TotalShot"].ToString()) + decimal.Parse(dsJig1.Tables[0].Rows[0]["WorkShot"].ToString());
				daJig1.Update(dsJig1) ;
			}
			//설비정보에 치구2의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();


			//설비정보에 공구3의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200010'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = Tool3;
			comm.CommandText = str;				
			SqlDataAdapter daShot2 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilder2 = new SqlCommandBuilder(daShot2) ;
			DataSet dsShot2 = new DataSet() ;
			daShot2.Fill(dsShot2) ;
			if(dsShot2.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 공구의 횟수를 알수 있다.
				decimal tmp2 = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Text) / decimal.Parse(dsShot2.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsShot2.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsShot2.Tables[0].Rows[0]["WorkShot"].ToString()) + tmp2;
				dsShot2.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsShot2.Tables[0].Rows[0]["TotalShot"].ToString()) + decimal.Parse(dsShot2.Tables[0].Rows[0]["WorkShot"].ToString());
				daShot2.Update(dsShot2) ;
			}
			//설비정보에 공구3의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();

			//설비정보에 치구3의 작업shot을 누적하는 부분......시작
			str = "select * from EI_MT where EquipmentNum = @name and EquipmentClassification = '07200020'" ;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = Jig3;
			comm.CommandText = str;
			SqlDataAdapter daJig2 = new SqlDataAdapter(comm) ;
			SqlCommandBuilder CommandBuilderJig2 = new SqlCommandBuilder(daJig2) ;
			DataSet dsJig2 = new DataSet() ;
			daJig2.Fill(dsJig2) ;
			if(dsJig2.Tables[0].Rows.Count>0)
			{
				//생산수량(불량포함)에 설비정보의 cavity값을 나눈다..그날 사용한 치구의 횟수를 알수 있다.
				decimal tmpJig2 = Math.Round((decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThisWorkCompletionQuantity").Text) / decimal.Parse(dsJig2.Tables[0].Rows[0]["Cavity"].ToString()))) ;
				dsJig2.Tables[0].Rows[0]["WorkShot"] = decimal.Parse(dsJig2.Tables[0].Rows[0]["WorkShot"].ToString()) + tmpJig2;
				dsJig2.Tables[0].Rows[0]["TotalShot"] = decimal.Parse(dsJig2.Tables[0].Rows[0]["TotalShot"].ToString()) + decimal.Parse(dsJig2.Tables[0].Rows[0]["WorkShot"].ToString());
				daJig2.Update(dsJig2) ;
			}
			//설비정보에 치구3의 작업shot을 누적하는 부분......끝
			comm.Parameters.Clear();
		}



		/// <summary>
		/// 외주의뢰원장변경
		/// </summary>
		/// <param name="rowcount"></param>
		private void OutsideRequestUpdate(SqlConnection con, SqlTransaction trans, int rowcount)
		{
			string str = "Update OOR_HT Set FirstDeliveryDemandQuantity=@first, FirstDeliveryDemandDate=@firstdate, " +
				" SecondDeliveryDemandQuantity=@second, SecondDeliveryDemandDate=@seconddate, " +
				" ThirdDeliveryDemandQuantity=@third, ThirdDeliveryDemandDate=@thirddate, " +
				" FourthDeliveryDemandQuantity=@fourth, FourthDeliveryDemandDate=@fourthdate, " +
				" FifthDeliveryDemandQuantity=@fifth, FifthDeliveryDemandDate=@fifthdate, " +
				" ApplyUnitCost=@applycost, OrderQuantity=@totalquantity, TotalCost=@totalcost, " +
				" UpdatingPerson=@updateperson, UpdatingPersonID=@updatepersonid, UpdatingDate=@date "+
				" Where OutSideOrderRequestHistoryIndex=@index ";
			SqlCommand comm = new SqlCommand(str,con);
			comm.Transaction = trans;
			comm.Parameters.Add("@first",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OrderQuantity").Text);
			comm.Parameters.Add("@firstdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FirstDeliveryDemandDate").Text);
			comm.Parameters.Add("@second",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandQuantity").Text);
			if(m_Grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Text == "")
				comm.Parameters.Add("@seconddate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@seconddate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("SecondDeliveryDemandDate").Text);
			
			comm.Parameters.Add("@third",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandQuantity").Text);
			if(m_Grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Text == "")
				comm.Parameters.Add("@thirddate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@thirddate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ThirdDeliveryDemandDate").Text);
			
			comm.Parameters.Add("@fourth",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandQuantity").Text);
			if(m_Grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Text == "")
				comm.Parameters.Add("@fourthdate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@fourthdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FourthDeliveryDemandDate").Text);
			
			comm.Parameters.Add("@fifth",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandQuantity").Text);
			if(m_Grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Value == null || m_Grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Text == "")
				comm.Parameters.Add("@fifthdate",SqlDbType.SmallDateTime).Value = Convert.DBNull;
			else
				comm.Parameters.Add("@fifthdate",SqlDbType.SmallDateTime).Value = DateTime.Parse(m_Grid.Rows[rowcount].Cells.FromKey("FifthDeliveryDemandDate").Text);
			comm.Parameters.Add("@applycost",SqlDbType.Decimal).Value = Convert.DBNull;
			comm.Parameters.Add("@totalquantity",SqlDbType.Decimal).Value = decimal.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OrderQuantity").Text);
			comm.Parameters.Add("@totalcost",SqlDbType.Decimal).Value = Convert.DBNull;
			comm.Parameters.Add("@updateperson",SqlDbType.VarChar).Value = UserName;
			comm.Parameters.Add("@updatepersonid",SqlDbType.VarChar).Value = UserID;
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			comm.Parameters.Add("@index",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("OutSideOrderRequestHistoryIndex").Text);

			comm.ExecuteNonQuery();
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
		/// 검사품인지 무검사인지 판단하는 함수 
		/// 0이면 무검사,1이면 검사
		/// </summary>
		/// <param name="itemnum"></param>
		/// <returns></returns>
		private bool Check(string itemnum)
		{
			string aa = "0";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select CheckDistinction From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = itemnum;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["CheckDistinction"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "0")
				return false;
			else
				return true;
		}


		/// <summary>
		/// 하위품목의 최상위공정순서 찾는 함수
		/// </summary>
		/// <param name="Item"></param>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		private string Process(string Item, SqlConnection conn,SqlTransaction tr)
		{
			string str = @"select * From PSI_MT where RecodingState = 1 and ItemNum=@num and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From  PSI_MT where RecodingState = 1 and ItemNum=@num)";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num",Item);
			SqlDataReader dr = comm.ExecuteReader();
			string code = "";
			while(dr.Read())
			{
				code = dr["ProcessCode"].ToString();
			}
			dr.Close();

			if(code == "")
				throw new Exception("품목번호 "+Item+"인 품목의 공정정보가 입력되어 있지 않습니다!");
			else
				return code;
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



		/// <summary>
		/// 재고관리여부
		/// </summary>
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

	}
}
