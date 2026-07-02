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
	/// Cancel에 대한 요약 설명입니다.
	/// </summary>
	public class Cancel
	{
		private string m_PageName;		//요청페이지를 담는 필드
		private string m_Table;			//테이블명을 담는필드
		private string m_Table1;		//이전테이블명을 담는필드
		private string m_Index;			//이전원장 레코드인덱스 담는 필드
		private string m_Index1;			//이전원장 레코드인덱스 담는 필드
		private string m_Distinction ;	//업무구분(모듈구분)
		private string m_Vol;			//취소할 볼륨번호 담는 필드
		private int m_True = 0;			//성공적인 취소를 했는지 구분하는 필드
		private UltraWebGrid m_Grid;	//넘어온 그리드 저장 필드

		/// <summary>
		/// 취소객체 생성자
		/// </summary>
		/// <param name="grid">요청페이지에서 넘어온 그리드</param>
		/// <param name="PageName">요청페이지명</param>
		/// <param name="Vol">볼륨번호</param>
		public Cancel(UltraWebGrid grid, string PageName, string Vol)
		{
			m_Grid = grid;
			m_PageName = PageName.ToString();
			m_Vol = Vol.ToString();


			switch(m_PageName)
			{
				case "ReceiveingPC":						// 수주현황
					m_Table = "RO_HT";
					m_Distinction = "영업";
					break;				
				case "ProductionRequestPC":					// 생산의뢰현황
					m_Table = "PR_HT";
					m_Table1 = "RO_HT";
					m_Distinction = "영업";
					m_Index = "ReceivingOrderHistoryIndex";
					break;
				case "GoodsBuyingRequestPC":				// 상품구매의뢰현황
					m_Table = "BR_HT";
					m_Table1 = "RO_HT";
					m_Distinction = "영업";
					m_Index = "ReceivingOrderHistoryIndex";
					break;
				case "ProductionPlanPC":					//생산계획현황
					m_Table = "PP_HT";
					m_Table1 = "PR_HT";
					m_Distinction = "생산";
					m_Index = "ProductionRequestHistoryIndex";
					m_Index1 = "HistoryIndex";
					break;
				case "RowMaterialRequirementCalculatePC":	//자재소요산출현황
					m_Table1 = "PP_HT";
					m_Distinction = "생산";
					m_Index = "ProductionPlanHistoryIndex";
					break;
				case "RowMaterialRequriementPC":			//원자재구매의뢰현황
					m_Table = "BR_HT";
					m_Table1 = "MRC_HT";
					m_Distinction = "생산";
					m_Index = "MaterialRequirementCalculateHistoryIndex";
					break;
				case "OutsideRequestPC":					//외주의뢰현황
					m_Table = "OOR_HT";
					m_Table1 = "WDWP_HT";
					m_Distinction = "생산";
					m_Index = "ProductionPlanHistoryIndex";
					break;
				case "BuyingOrderPC":						//구매발주현황
					m_Table = "BO_HT";
					m_Table1 = "BR_HT";
					m_Distinction = "구매";
					m_Index = "BuyingRequestHistoryIndex";
					break;
				case "OutSideOrderPC":						//외주발주현황
					m_Table = "OO_HT";
					m_Table1 = "OOR_HT";
					m_Distinction = "외주";
					m_Index = "OutSideOrderRequestHistoryIndex";
					break;
				case "WCPlanPC":
					m_Table = "WDWP_HT";
					m_Table1 = "PP_HT";
					m_Distinction = "생산";
					m_Index = "ProductionPlanHistoryIndex";
					break;
				default:
					break;
			}
		}


		/// <summary>
		/// 월마감여부 
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


        /// <summary>
        /// 각 요청페이지에서 사용할 취소함수
        /// </summary>
        /// <returns></returns>
		public UltraWebGrid MainRowCancel()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();

			try
			{
				// 월마감 체크후 취소가 가능한지 확인한다
				if(MonthClosing() == true)
				{
				
					//진행상태가 대기일때만 취소 가능
					for(int i = 0; i < m_Grid.Rows.Count; i++)
					{
						if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Text != "대기")
							throw new Exception(m_Grid.Rows[i].Cells.FromKey("ItemName").Text + " 품목의 진행상태가 대기가 아니어서 취소가 불가능 합니다!");
						else
							RowCancel(conn,tr,i);					
					}
					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('취소 했습니다!');");
					hc.Response.Write("</script>");
					tr.Commit();
				}
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

		/// <summary>
		/// 자재소요산출현황페이지에서 사용할 취소함수
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		public int MainRowCancel(int a)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			try
			{
				// 월마감 체크후 취소가 가능한지 확인한다
				if(MonthClosing() == true)
				{
				
					//진행상태가 대기일때만 취소 가능
					for(int i = 0; i < m_Grid.Rows.Count; i++)
					{
						if(m_Grid.Rows[i].Cells.FromKey("ProgressCondition").Text != "대기" )
							throw new Exception(m_Grid.Rows[i].Cells.FromKey("ItemName").Text + " 품목의 작업계획이 수립되어 취소가 불가능 합니다!");
						else if(m_Grid.Rows[i].Cells.FromKey("RowMaterialCalculation").Text != "산출")
							throw new Exception(m_Grid.Rows[i].Cells.FromKey("ItemName").Text + " 품목은 자재소요산출을 하지 않아 취소가 불가능 합니다!");
						else
							RowCancel(conn,tr,i);
					}

					HttpContext hc = HttpContext.Current;
					hc.Response.Write("<script language=javascript>");
					hc.Response.Write("alert('취소 했습니다!');");
					hc.Response.Write("</script>");

					tr.Commit();
				}
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
            
			return m_True;
		}



		/// <summary>
		/// 실제 취소함수
		/// </summary>
		/// <param name="con"></param>
		/// <param name="trans"></param>
		/// <param name="count"></param>
		private void RowCancel(SqlConnection con, SqlTransaction trans, int count)
		{	

			switch(m_PageName)
			{

				case "ReceiveingPC":				// 수주현황
					TableCancel(con,trans,count);			// 수주원장 삭제함수
					break;				
				case "ProductionRequestPC":			// 생산의뢰현황
					TableUpdate(con,trans,count);			// 수주원장 진행상태 변경함수
					TableCancel(con,trans,count);			// 생산의뢰원장 삭제함수
					break;
				case "ProductionPlanPC":			// 생산계획현황
					if(m_Grid.Rows[0].Cells.FromKey("HistorySection").Text == "생산의뢰")
						TableUpdate(con,trans,count);			// 생산의뢰원장 진행상태 변경함수
					else if(m_Grid.Rows[0].Cells.FromKey("HistorySection").Text == "생산의뢰")
						TableUpdate6(con,trans,count);			// 기종별원장 진행상태 변경함수
					else
						TableUpdate3(con,trans,count);			// 실행계획테이블 State필드 변경함수
                    TableCancel(con,trans,count);			// 생산계획원장 삭제함수
					break;
				case "RowMaterialRequirementCalculatePC":	//자재소요량 산출현황
					TableUpdate2(con,trans,count);			// 생산계획원장 자재소요산출여부 변경함수
					TableUpdate4(con,trans,count);			//자재소요원장 동일볼륨 삭제함수
					break;
				case "GoodsBuyingRequestPC":		//상품구매의뢰현황
					TableUpdate1(con,trans,count);			// 수주원장 진행상태 변경함수			
					TableCancel(con,trans,count);			// 구매의뢰원장 삭제함수
					break;
				case "RowMaterialRequriementPC":	//원자재구매의뢰현황
					TableUpdate1(con,trans,count);			// 자재소요원장 진행상태 변경함수
					TableCancel(con,trans,count);			// 구매의뢰원장 삭제함수
					break;
				case "OutsideRequestPC":			//외주의뢰현황
					TableUpdate5(con,trans,count);			// WC작업계획원장 진행상태 변경함수
					TableCancel(con,trans,count);			// 외주의뢰원장 삭제함수
					break;
				case "BuyingOrderPC":				//구매발주현황
					TableUpdate(con,trans,count);			// 구매의뢰원장 진행상태 변경함수
					TableCancel(con,trans,count);			// 구매발주원장 삭제함수
					break;
				case "OutSideOrderPC":				//외주발주현황
					TableUpdate(con,trans,count);			// 외주의뢰원장 진행상태 변경함수
					TableCancel(con,trans,count);			// 외주발주원장 삭제함수
					break;
				case "WCPlanPC":				// 작업계획현황
					TableUpdate(con,trans,count);			// 생산계획원장 진행상태 변경함수
					TableCancel(con,trans,count);			// 작업계획 원장  삭제함수
					break;			
				default :
					break;
			}
			
		}
		
		/// <summary>
		/// 자신의 원장 취소 함수
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void TableCancel(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str="Delete "+m_Table.ToString()+" Where VolumNum = @num";

			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@num", SqlDbType.Int).Value = int.Parse(m_Vol.ToString());
			comm.ExecuteNonQuery();
		}

		/// <summary>
		/// 이전원장 업데이트 함수
		/// 생산의뢰현황(수주원장), 생산계획현황(생산의뢰원장), 외주의뢰현황(WC작업계획원장),
		/// 구매발주현황(구매의뢰원장), 외주발주현황(외주의뢰원장)에서 사용
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void TableUpdate(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			if(m_PageName == "ProductionPlanPC")
			{
				string str = "UPDATE "+m_Table1.ToString()+" SET ProgressCondition = '대기' WHERE "+m_Index.ToString()+ " = @idx";
				SqlCommand comm = new SqlCommand(str, conn);
				comm.Transaction = tr;
				comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey(m_Index1.ToString()).Text);
				comm.ExecuteNonQuery();
			}
			else
			{
				string str = "UPDATE "+m_Table1.ToString()+" SET ProgressCondition = '대기' WHERE "+m_Index.ToString()+ " = @idx";
				SqlCommand comm = new SqlCommand(str, conn);
				comm.Transaction = tr;
				if(m_Grid.Rows[rowcount].Cells.FromKey(m_Index.ToString()).Text == null ||m_Grid.Rows[rowcount].Cells.FromKey(m_Index.ToString()).Text == "")
					comm.Parameters.Add("@idx", SqlDbType.Int).Value = 0;
				else
					comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey(m_Index.ToString()).Text);
				comm.ExecuteNonQuery();
			}
		}

		
		/// <summary>
		/// 이전원장 업데이트 함수
		/// 상품구매의뢰현황(수주원장), 원자재구매의뢰현황(자재소요원장)에서사용
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void TableUpdate1(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str = "UPDATE "+m_Table1.ToString()+" SET ProgressCondition = '대기' WHERE "+m_Index.ToString()+ " = @idx";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex").Text);

			if(m_Grid.Rows[rowcount].Cells.FromKey("ProgressCondition").Text != "대기")
			{
				throw new Exception("취소할수 없는 항목이 있습니다!");
			}
			else
			{			
				comm.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// 생산계획원장 업데이트 함수
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void TableUpdate2(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
		
			string str = "UPDATE PP_HT SET ProgressCondition = '대기', RowMaterialCalculation = 0, MaterialRequirementVolumNum=0 WHERE ProductionPlanHistoryIndex = @idx";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("ProductionPlanHistoryIndex").Text);
			comm.ExecuteNonQuery();
		}

		private void TableUpdate4(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str1 = "Select * From MRC_HT WHERE VolumNum = @vol";
			SqlCommand comm1 = new SqlCommand(str1,conn);
			comm1.Transaction = tr;
			comm1.Parameters.Add("@vol",SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("MaterialRequirementVolumNum").Text);

			SqlDataAdapter da = new SqlDataAdapter(comm1);
			DataSet ds = new DataSet();
			da.Fill(ds);

			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				if(dr["ProgressCondition"].ToString() == "완료")
					throw new Exception("품목 " + dr["ItemName"].ToString() + "이 이미 구매의뢰되어 취소가 불가능 합니다!");
			}

			string str = "Delete From MRC_HT WHERE VolumNum = @vol";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@vol", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("MaterialRequirementVolumNum").Text);
			comm.ExecuteNonQuery();
		}

		private void TableUpdate5(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str = "";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			if(m_Grid.Rows[rowcount].Cells.FromKey("ProductionPlanHistoryIndex").Value == null)
			{
				str = "UPDATE WDWP_HT SET ProgressCondition = '대기' WHERE WCDailyWorkPlanHistoryIndex = @WCDailyWorkPlanHistoryIndex and WorkDistinction !='자가' ";
				comm.CommandText = str;
				comm.Parameters.Add("@WCDailyWorkPlanHistoryIndex", m_Grid.Rows[rowcount].Cells.FromKey("WCDailyWorkPlanHistoryIndex").Text);
			}
			else
			{
				str = "UPDATE WDWP_HT SET ProgressCondition = '대기' WHERE ProductionPlanHistoryIndex = @idx and WorkDistinction !='자가' ";
				comm.CommandText = str;
				comm.Parameters.Add("@idx",int.Parse(m_Grid.Rows[rowcount].Cells.FromKey(m_Index.ToString()).Text));
			}
			comm.Transaction = tr;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}
		
		/// <summary>
		/// 실행계획테이블 업데이트 함수
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		/// <param name="rowcount"></param>
		private void TableUpdate3(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str = "UPDATE EPI_MT SET State = '0' WHERE RecodingState = 1 and ExecutionPlanInfoIndex = @idx";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey("HistoryIndex").Text);
			comm.ExecuteNonQuery();
		}

		private void TableUpdate6(SqlConnection conn, SqlTransaction tr, int rowcount)
		{
			string str = "UPDATE ItemGroup_HT SET ProgressCondition = '대기' WHERE ProductionPlanHistoryIndex = @idx and WorkDistinction !='자가' ";
			SqlCommand comm = new SqlCommand(str, conn);
			comm.Transaction = tr;
			comm.Parameters.Add("@idx", SqlDbType.Int).Value = int.Parse(m_Grid.Rows[rowcount].Cells.FromKey(m_Index.ToString()).Text);
			comm.ExecuteNonQuery();
		}

		
	}
}
