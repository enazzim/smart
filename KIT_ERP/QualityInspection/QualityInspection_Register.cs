//
//		종류 - 클래스
//		이름 - QualityInspection_Register
//		부모클래스상속 - QualityInspection_BaseClass
//		용도 - 품질검사 모듈의 품질검사등록 페이지(QualityInspectionRegistration.aspx)에서 등록버튼 클릭시 동작
//
//		구성맴버 변수 - DB에 등록하기 위한 필요한 정보들을 저장하기 위한 string[] strArrValue
//		구성맴버 메서드

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;

namespace KIT_ERP.QualityInspection
{
	public class QualityInspection_Register : QualityInspection_BaseClass
	{
		// DB에 등록하기 위한 필요한 정보들을 저장
		string[] strArrValue;

		/// <summary>
		/// 생성자 - 매개변수로 넘어온 DB에 입력될 정보들을 현재의 맴버변수인 strArrValue에 대입
		/// </summary>
		/// <param name="strArrValue">string[] 형태의 DB에 입력될 정보</param>
		public QualityInspection_Register(string[] strArrValue)
		{
			this.strArrValue = strArrValue;

			/*
			====================================================================
			==========================|	strArrValue 정보	|=======================
			====================================================================
			
			0 - DB의 Index Key
			1 - 검사합격수량
			2 - 부적합수량
			3 - 검사판정
			4 - 부적합원인
			5 - 부적합원인 코드
			6 - 부적합현상
			7 - 부적합현상 코드
			8 - 부적합 세부 내용
			9 - 검사자 이름
			10 - 부적합 금액
			11 - 원장구분 = ("구매", "외주", "자가" 중 1) - 구매납품의뢰, 외주납품의뢰, 작업일보원장 구분
			12 - 구분된 원장1의 인덱스
			13 - 원장구분2 - (구매발주원장, 외주발주원장)  -외주나 구매인경우만 해당
			14 - 원장구분2 인덱스						 -외주나 구매인경우만 해당
			15 - 품목명
			16 - 품목번호
			17 - 도면번호
			18 - 거래처명
			19 - 거래처의 사업자 등록번호
			20 - 단가
			21 - 시작공정 순서번호
			22 - 종료공정 순서번호
			23 - 시작공정코드
			24 - 종료공정명
			25 - 종료공정코드
			26 - 검사판정 DropDownList 선택 여부 - 사용자가 선택한 경우 0 이 아닌 값을 가짐
			27 - 날짜
			28 - 매입년
			29 - 매입월
			30 - 검사년도
			31 - 검사월
			32 - 검사일*/

		}


		/// <summary>
		///  외주인 경우에 외주창고와 생산창고의 정보를 수정하는 SQL Query 를 리턴
		/// </summary>
		/// <returns>창고 수정 SQL Query</returns>
		private StringBuilder Registeration_OutSideStoreHouse()
		{
			// 본 메서드는 현재 클래스내에서 외주인 경우에 호출되며
			// 외주창고와 생산창고의 정보를 수정하는 SQL Query 를 리턴한다.

			// 외주는 1 : 1 과 1 : n 으로 나뉜다.
			// 즉 하나의 품목이 출고되어 하나의 품목으로 가공되어 입고 되는 경우와
			// 여러개의 품목이 출고되어 하나의 품목으로 입고되는 경우의 두 가지가 존재

			// 외주창고에 떨어낼 수량은 적합수량이 아니라 의뢰수량(적합수량+부적합수량)임
			// 리턴될 비어있는 StringBuilder 생성
			StringBuilder sB_SQL = new StringBuilder( 100 );

			// 현재 품목의 하위품목 출고정보를 제공하는 클래스를 생성
			OutsiderOrder_Out_Item OOI_Search = 
									new OutsiderOrder_Out_Item
									(
										strArrValue[16]						// 품목번호
										, Convert.ToInt32( strArrValue[21] )	// 시작 공정 순서 번호
										, 1		//Convert.ToInt32( strArrValue[1] )	// 적합 수량
										, strArrValue[19]						// 사업자 등록번호
									) ;


			// 해당 품목의 이전 공정 품목이 있는경우는 dt_OutsiderOrder_Out_Items.Rows[3] 필드에  
			// ( 진척률 * 단가 * 수량 ) 이 출력되어 나오고.
			// 하위 BOM 을 풀어 계산한 경우는 dt_OutsiderOrder_Out_Items.Rows[3] 필드에  
			// ( 진척률 * 단가 ) 만 출력 되므로 수량(dt_OutsiderOrder_Out_Items[2]) 을 곱하면 Total 금액이 된다.

			// 하위품목 출고정보 데이터 받아오기
			DataTable dt_OutsiderOrder_Out_Items = OOI_Search.getData();

			/*
				 * ==================================================================
				 *					외주창고 업데이트 쿼리 생성
				 * ==================================================================
			*/

			// 출고품목이 BOM 이나 이전공정 순서에 존재하면 ( 출고된 품목이 존재하면 또는 정상적인 품목이라면 )
			if ( dt_OutsiderOrder_Out_Items.Rows.Count > 0 )
			{
				decimal successQuantity = Convert.ToDecimal( strArrValue[1] );
				decimal deTotalCost = ( successQuantity * Cost(strArrValue[16].ToString(), strArrValue[22].ToString()) );	// 생산창고에 입고 될 금액	[16] 은 품목번호,[22]는 종료공정순서번호
				int YearCount = int.Parse(strArrValue[30]);
				foreach ( DataRow drow_item in dt_OutsiderOrder_Out_Items.Rows )
				{
					
					int int_OS_MT_MonthCount = int.Parse(strArrValue[31]);
					string strItemNum = drow_item[0].ToString();
					string strProcessSEQNum = drow_item[1].ToString();
					string strQuantity = Convert.ToString( ( Convert.ToDecimal( drow_item[2] ) * successQuantity) );
					string strOutQuantity = Convert.ToString( ( Convert.ToDecimal( drow_item[2] ) *(successQuantity + Convert.ToDecimal( strArrValue[2] ) ) ) );
					string strTotalCost = Convert.ToString( ( Convert.ToDecimal( drow_item[3] ) * Convert.ToDecimal( strQuantity ) ) );
					
					// 외주창고 업데이트 쿼리작성
					sB_SQL.Append(" update OS_MT SET ");
					sB_SQL.Append(" OutStorehouseQuantity");
					sB_SQL.Append( int_OS_MT_MonthCount );
					sB_SQL.Append(" = ");
					sB_SQL.Append("OutStorehouseQuantity");
					sB_SQL.Append( int_OS_MT_MonthCount );
					sB_SQL.Append(" + "); 
					sB_SQL.Append( strOutQuantity );
					sB_SQL.Append(" , ");
					sB_SQL.Append("OutStorehouseCost");
					sB_SQL.Append( int_OS_MT_MonthCount );
					sB_SQL.Append(" = ");
					sB_SQL.Append("OutStorehouseCost");
					sB_SQL.Append( int_OS_MT_MonthCount );
					sB_SQL.Append(" + "); 
					sB_SQL.Append( strTotalCost );

					for (  ; int_OS_MT_MonthCount < 13 ; int_OS_MT_MonthCount ++ )
					{
						sB_SQL.Append(" , StockQuantity");
						sB_SQL.Append( int_OS_MT_MonthCount );
						sB_SQL.Append(" = ");
						sB_SQL.Append("StockQuantity");
						sB_SQL.Append( int_OS_MT_MonthCount );
						sB_SQL.Append(" - "); 
						sB_SQL.Append( strOutQuantity );
						sB_SQL.Append(" , ");
						sB_SQL.Append("StockCost");
						sB_SQL.Append( int_OS_MT_MonthCount );
						sB_SQL.Append(" = ");
						sB_SQL.Append("StockCost");
						sB_SQL.Append( int_OS_MT_MonthCount );
						sB_SQL.Append(" - "); 
						sB_SQL.Append( strTotalCost );
					}


					sB_SQL.Append( " where itemnum = '" );
					sB_SQL.Append( strItemNum );
					sB_SQL.Append( "' and " );
					sB_SQL.Append( " processsequencenum =  " );
					sB_SQL.Append( strProcessSEQNum );
					sB_SQL.Append(" and BusinessRegistrationNum = '");
					sB_SQL.Append(strArrValue[19]);
					sB_SQL.Append("' ");
					sB_SQL.Append( " and " );
					sB_SQL.Append( " recodingstate =  1 " );
					sB_SQL.Append( " and " );
					sB_SQL.Append( " [Year] = " );
					sB_SQL.Append( YearCount+"" );

					for(int a = YearCount+1; a <= DateTime.Now.Year; a++)
					{
						sB_SQL.Append("; UPDATE OS_MT SET ");
						
						for(int Month = 1; Month <13 ; Month++)
						{
							sB_SQL.Append(" StockQuantity");
							sB_SQL.Append( Month );
							sB_SQL.Append(" = ");
							sB_SQL.Append("StockQuantity");
							sB_SQL.Append( Month );
							sB_SQL.Append(" - "); 
							sB_SQL.Append( strOutQuantity );
							sB_SQL.Append(" , ");
							sB_SQL.Append("StockCost");
							sB_SQL.Append( Month );
							sB_SQL.Append(" = ");
							sB_SQL.Append("StockCost");
							sB_SQL.Append( Month );
							sB_SQL.Append(" - "); 
							sB_SQL.Append( strTotalCost );
							if(Month != 12)
								sB_SQL.Append(", "); 
						}
						
						//sB_SQL.Append(" , ");
						sB_SQL.Append( " where itemnum = '" );
						sB_SQL.Append( strItemNum );
						sB_SQL.Append( "' and " );
						sB_SQL.Append( " processsequencenum =  " );
						sB_SQL.Append( strProcessSEQNum );
						sB_SQL.Append(" and BusinessRegistrationNum = '");
						sB_SQL.Append(strArrValue[19]);
						sB_SQL.Append("' ");
						sB_SQL.Append( " and " );
						sB_SQL.Append( " recodingstate =  1 " );
						sB_SQL.Append( " and " );
						sB_SQL.Append( " [Year] = " );
						sB_SQL.Append( a+"" );
					}
				}



				// 마이너스 재고 수량 체크 하기위해 외주 창고 수량 빼고 재고 수량을 가져온다.
				foreach ( DataRow drow_item in dt_OutsiderOrder_Out_Items.Rows )
				{
					string strItemNum = drow_item[0].ToString();
					string strProcessSEQNum = drow_item[1].ToString();

					sB_SQL.Append("; select StockQuantity12 from OS_MT  ");
					sB_SQL.Append( " where itemnum = '" );
					sB_SQL.Append( strItemNum );
					sB_SQL.Append( "' and " );
					sB_SQL.Append( " processsequencenum =  " );
					sB_SQL.Append( strProcessSEQNum );
					sB_SQL.Append(" and BusinessRegistrationNum = '");
					sB_SQL.Append(strArrValue[19]);
					sB_SQL.Append("' ");
					sB_SQL.Append( " and " );
					sB_SQL.Append( " recodingstate =  1 " );
					sB_SQL.Append( " and " );
					sB_SQL.Append( " [Year] = " );
					sB_SQL.Append( YearCount+"" );
				}

				

				if(!WorkPlan3() || (StoreManage(strArrValue[16]) == "예"))
				{
					
					//공정이 외주가 아니거나 또는 (자산분류는 제품이며 최종공정)이 아닐때는 생산창고에 누적한다.
					if ((strArrValue[11].Trim() != "외주") || (!Division(Convert.ToInt16(strArrValue[22]),strArrValue[16])))
					{
						// 생산창고 업데이트 쿼리 작성
						int int_PS_MT_MonthCount = int.Parse(strArrValue[29]);

						sB_SQL.Append(";   update PS_MT set ");
						sB_SQL.Append("InStorehouseQuantity");
						sB_SQL.Append( int_PS_MT_MonthCount );
						sB_SQL.Append(" = ");
						sB_SQL.Append("InStorehouseQuantity");
						sB_SQL.Append( int_PS_MT_MonthCount );
						sB_SQL.Append(" + "); 
						sB_SQL.Append( successQuantity );
						sB_SQL.Append(" , ");
						sB_SQL.Append("InStorehouseCost");
						sB_SQL.Append( int_PS_MT_MonthCount );
						sB_SQL.Append(" = ");
						sB_SQL.Append("InStorehouseCost");
						sB_SQL.Append( int_PS_MT_MonthCount );
						sB_SQL.Append(" + "); 
						sB_SQL.Append( deTotalCost );

						for ( ; int_PS_MT_MonthCount < 13 ; int_PS_MT_MonthCount ++ )
						{
							sB_SQL.Append(" ,  StockQuantity");
							sB_SQL.Append( int_PS_MT_MonthCount );
							sB_SQL.Append(" = ");
							sB_SQL.Append("StockQuantity");
							sB_SQL.Append( int_PS_MT_MonthCount );
							sB_SQL.Append(" + "); 
							sB_SQL.Append( successQuantity );
							sB_SQL.Append(" , ");
							sB_SQL.Append("StockCost");
							sB_SQL.Append( int_PS_MT_MonthCount );
							sB_SQL.Append(" = ");
							sB_SQL.Append("StockCost");
							sB_SQL.Append( int_PS_MT_MonthCount );
							sB_SQL.Append(" + "); 
							sB_SQL.Append( deTotalCost );
						}

						sB_SQL.Append( " where itemnum = '" );
						sB_SQL.Append( strArrValue[16] );
						sB_SQL.Append( "' and " );
						sB_SQL.Append( " processsequencenum =  " );
						sB_SQL.Append( strArrValue[22] );
						sB_SQL.Append( " and " );
						sB_SQL.Append( " recodingstate =  1 " );
						sB_SQL.Append( " and " );
						sB_SQL.Append( " [Year] = " );
						sB_SQL.Append( YearCount+"" );	
				
						for(int a = YearCount+1; a <= DateTime.Now.Year; a++)
						{
							sB_SQL.Append("; UPDATE PS_MT SET ");
						
							for(int Month = 1; Month <13 ; Month++)
							{
								sB_SQL.Append(" StockQuantity");
								sB_SQL.Append( Month );
								sB_SQL.Append(" = ");
								sB_SQL.Append("StockQuantity");
								sB_SQL.Append( Month );
								sB_SQL.Append(" + "); 
								sB_SQL.Append( successQuantity );
								sB_SQL.Append(" , ");
								sB_SQL.Append("StockCost");
								sB_SQL.Append( Month );
								sB_SQL.Append(" = ");
								sB_SQL.Append("StockCost");
								sB_SQL.Append( Month );
								sB_SQL.Append(" + "); 
								sB_SQL.Append( deTotalCost );
								if(Month != 12)
									sB_SQL.Append(", "); 
							}
						
							sB_SQL.Append( " where itemnum = '" );
							sB_SQL.Append( strArrValue[16] );
							sB_SQL.Append( "' and " );
							sB_SQL.Append( " processsequencenum =  " );
							sB_SQL.Append( strArrValue[22] );
							sB_SQL.Append( " and " );
							sB_SQL.Append( " recodingstate =  1 " );
							sB_SQL.Append( " and " );
							sB_SQL.Append( " [Year] = " );
							sB_SQL.Append( a+"" );
						}
					}
					
					
					
				}
				else
				{
					//자산분류가 제품이고 최종공정일때 생산창고 누적
					if(Division(Convert.ToInt16(strArrValue[22]),strArrValue[16]))
					{
						// 생산창고 업데이트 쿼리 작성
						int int_PS_MT_MonthCount = int.Parse(strArrValue[29]);

						sB_SQL.Append(";   update PS_MT set ");
						sB_SQL.Append("InStorehouseQuantity");
						sB_SQL.Append( int_PS_MT_MonthCount );
						sB_SQL.Append(" = ");
						sB_SQL.Append("InStorehouseQuantity");
						sB_SQL.Append( int_PS_MT_MonthCount );
						sB_SQL.Append(" + "); 
						sB_SQL.Append( successQuantity );
						sB_SQL.Append(" , ");
						sB_SQL.Append("InStorehouseCost");
						sB_SQL.Append( int_PS_MT_MonthCount );
						sB_SQL.Append(" = ");
						sB_SQL.Append("InStorehouseCost");
						sB_SQL.Append( int_PS_MT_MonthCount );
						sB_SQL.Append(" + "); 
						sB_SQL.Append( deTotalCost );

						for ( ; int_PS_MT_MonthCount < 13 ; int_PS_MT_MonthCount ++ )
						{
							sB_SQL.Append(" ,  StockQuantity");
							sB_SQL.Append( int_PS_MT_MonthCount );
							sB_SQL.Append(" = ");
							sB_SQL.Append("StockQuantity");
							sB_SQL.Append( int_PS_MT_MonthCount );
							sB_SQL.Append(" + "); 
							sB_SQL.Append( successQuantity );
							sB_SQL.Append(" , ");
							sB_SQL.Append("StockCost");
							sB_SQL.Append( int_PS_MT_MonthCount );
							sB_SQL.Append(" = ");
							sB_SQL.Append("StockCost");
							sB_SQL.Append( int_PS_MT_MonthCount );
							sB_SQL.Append(" + "); 
							sB_SQL.Append( deTotalCost );
						}

						sB_SQL.Append( " where itemnum = '" );
						sB_SQL.Append( strArrValue[16] );
						sB_SQL.Append( "' and " );
						sB_SQL.Append( " processsequencenum =  " );
						sB_SQL.Append( strArrValue[22] );
						sB_SQL.Append( " and " );
						sB_SQL.Append( " recodingstate =  1 " );
						sB_SQL.Append( " and " );
						sB_SQL.Append( " [Year] = " );
						sB_SQL.Append( YearCount+"" );	
				
						for(int a = YearCount+1; a <= DateTime.Now.Year; a++)
						{
							sB_SQL.Append("; UPDATE PS_MT SET ");
						
							for(int Month = 1; Month <13 ; Month++)
							{
								sB_SQL.Append(" StockQuantity");
								sB_SQL.Append( Month );
								sB_SQL.Append(" = ");
								sB_SQL.Append("StockQuantity");
								sB_SQL.Append( Month );
								sB_SQL.Append(" + "); 
								sB_SQL.Append( successQuantity );
								sB_SQL.Append(" , ");
								sB_SQL.Append("StockCost");
								sB_SQL.Append( Month );
								sB_SQL.Append(" = ");
								sB_SQL.Append("StockCost");
								sB_SQL.Append( Month );
								sB_SQL.Append(" + "); 
								sB_SQL.Append( deTotalCost );
								if(Month != 12)
									sB_SQL.Append(", "); 
							}
						
							sB_SQL.Append( " where itemnum = '" );
							sB_SQL.Append( strArrValue[16] );
							sB_SQL.Append( "' and " );
							sB_SQL.Append( " processsequencenum =  " );
							sB_SQL.Append( strArrValue[22] );
							sB_SQL.Append( " and " );
							sB_SQL.Append( " recodingstate =  1 " );
							sB_SQL.Append( " and " );
							sB_SQL.Append( " [Year] = " );
							sB_SQL.Append( a+"" );
						}
					}
					
				}

			}
			else
			{
				// 출고된 정보가 없을 경우 "X" 를 리턴
				sB_SQL.Append("X");
			}

			return sB_SQL;
		}


		/// <summary>
		/// 재고관리여부
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private string StoreManage(string ItemNum)
		{
			string store = "아니오";
			string str = "Select Unit4 From II_MT Where ItemNum = @ItemNum and RecodingState = 1";
			SqlConnection con=  new SqlConnection( base.GetConnectionString );
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandText = str;
			cmd.Parameters.Add("@ItemNum",ItemNum);
			con.Open();			
			SqlDataReader dr = cmd.ExecuteReader();
			while(dr.Read())
			{
				store = dr["Unit4"].ToString();				
			}
			con.Close();


			return store;
			

			
		}

		private bool Division(int seq, string ItemNum)
		{
			//자산분류가 제품이고 최종공정인경우
			string str = "Select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ItemNum = @ItemNum";
			SqlConnection con=  new SqlConnection( base.GetConnectionString );
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = con;
			cmd.CommandText = str;
			cmd.Parameters.Add("@ItemNum",ItemNum);
			con.Open();
			int count = int.Parse(cmd.ExecuteScalar().ToString());
			str = @"Select count(*) From II_MT where PropertyClassification = '제품' and ItemNum = @ItemNum and RecodingState = 1";
			cmd.CommandText = str;
			int count1 = int.Parse(cmd.ExecuteScalar().ToString());
			con.Close();

			if(seq == count && count1 != 0)
			{
				return true;
			}
			else
				return false;
			
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


		private decimal Cost(string ItemNum, string ProSeq)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			//생산창고수량 변경
			// 진척비율을 구해야 한다
			decimal rate = 100;	//진척비율(공정순서에 있는 진척비율을 넣어둔다.

			string str = "Select OutsideOrderRate, ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @item and ProcessSequenceNum = @num";
			SqlCommand comm = new SqlCommand();
			comm.CommandText = str;
			comm.Connection = conn;
			comm.CommandText =str;
			comm.Parameters.Add("@item",ItemNum);
			comm.Parameters.Add("@num",int.Parse(ProSeq));
			SqlDataReader dr_Rate = comm.ExecuteReader();
			while(dr_Rate.Read())
			{
				rate = decimal.Parse(dr_Rate["ProgressRate"].ToString());
			}
			dr_Rate.Close();
			comm.Parameters.Clear();

			str = "select StandardUnitCost from II_MT where RecodingState = 1 and ItemNum= @itemnum";
			comm.Parameters.Add("@itemnum",ItemNum);
			comm.CommandText =str;	
			//품목정보테이블에서 현재 수정하고자하는 품목의 단가를 가지고있는 변수
			decimal unitcost = Decimal.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			conn.Close();

			return (unitcost*rate/100);
		}









		/// <summary>
		/// 물리적인 DB에 품질검사 완료 정보를 저장 - QualityInspectionRegistration.aspx.cs 파일에서 호출됨.
		/// </summary>
		public bool Registeration()
		{
			bool bResult = false;
			string str = "";

			
			SqlConnection con=  new SqlConnection( base.GetConnectionString );
			SqlCommand cmd = new SqlCommand("", con);
			StringBuilder sB_Query = new StringBuilder();
				

			// 외주인 경우에만 Registeration_OutSideStoreHouse() 호출하여 SQL Query 를 받아오고
			// 그 외의 경우는 공백으로 세팅
			if ( strArrValue[11].Trim() == "외주" )
			{
				sB_Query = this.Registeration_OutSideStoreHouse();
			}

			// 외주 출고 품목이 정상적으로 출고된 품목이라면
			if ( sB_Query.ToString() != "X" )
			{
				con.Open();
				SqlTransaction tran = con.BeginTransaction();
				cmd.Transaction = tran;
				cmd.CommandText = sB_Query.ToString() ;
				SqlDataReader dr;

				try
				{				
					// 외주인경우 외주창고 정보를 수정 할 SQL Query를 실행한다.
					// 만일 외주가 아닌경우(구매, 생산) 공백이 샐행되며 외주 창고정보는 Stored Procedure 에서 수정되고
					// 생산창고정보는 품질검사 의뢰시 수정되어 있다.
						
					if ( strArrValue[11].Trim() == "외주" )
					{
						dr = cmd.ExecuteReader();
						MinusCheck(dr) ;
						dr.Close();
					}

					// QualityInspection_Register Stored Procedure에 각 매개변수들을 세팅한다.
					cmd.CommandText = "QualityInspection_Register";
					cmd.CommandType = CommandType.StoredProcedure;
			
					// index key
					cmd.Parameters.Add("@key",SqlDbType.Int).Value = Convert.ToInt32( strArrValue[0] );									//0						

					// 적합수량
					cmd.Parameters.Add("@successQuantity", SqlDbType.Decimal).Value = Convert.ToDecimal( strArrValue[1] );				//1						
		
					// 부적합수량
					cmd.Parameters.Add("@incongruityQuantity", SqlDbType.Decimal).Value = Convert.ToDecimal( strArrValue[2] );			//2

					// 검사판정 DropDownList가 선택된 경우 값을 세팅하고 
					// 선택하지 않은 경우 NULL 값으로 세팅한다. 
					if ( strArrValue[26].ToString() != "0")
					{
						cmd.Parameters.Add("@incongruityDecision", SqlDbType.VarChar, 30).Value = strArrValue[3];						//3
					}
					else
						cmd.Parameters.Add("@incongruityDecision", SqlDbType.VarChar, 30).Value = null;									


					// 부적합원인 DropDownList가 선택된 경우 값을 세팅하고 
					// 선택하지 않은 경우 NULL 값으로 세팅한다. 
					// (DropDownList의 "- 선택하세요 -" 라는 기본 값은 "0" 값을 가진다)
					if ( strArrValue[5].ToString() != "0" )
					{
						// 부적합원인
						cmd.Parameters.Add("@incongruityCause", SqlDbType.VarChar, 20).Value = strArrValue[4];							//4
						// 부적합원인 코드
						cmd.Parameters.Add("@incongruityCauseCode", SqlDbType.VarChar, 20).Value = strArrValue[5];						//5
					}
					else
					{
						// 부적합원인
						cmd.Parameters.Add("@incongruityCause", SqlDbType.VarChar, 20).Value = null;
						// 부적합원인 코드
						cmd.Parameters.Add("@incongruityCauseCode", SqlDbType.VarChar, 20).Value = null;
					}
					
					// 부적합현상 DropDownList가 선택된 경우 값을 세팅하고 
					// 선택하지 않은 경우 NULL 값으로 세팅한다. 
					// (DropDownList의 "- 선택하세요 -" 라는 기본 값은 "0" 값을 가진다)
					if ( strArrValue[7].ToString() != "0" )
					{
						// 부적합 현상
						cmd.Parameters.Add("@incongruityPhenomenon", SqlDbType.VarChar, 20).Value = strArrValue[6];						//6

						// 부적합 현상 코드
						cmd.Parameters.Add("@incongruityPhenomenonCode", SqlDbType.VarChar, 20).Value = strArrValue[7];					//7
					}
					else
					{
						cmd.Parameters.Add("@incongruityPhenomenon", SqlDbType.VarChar, 20).Value = null;	
						cmd.Parameters.Add("@incongruityPhenomenonCode", SqlDbType.VarChar, 20).Value = null;
					}

					// 부적합 세부 내용 
					cmd.Parameters.Add("@incongruityDetailContent", SqlDbType.VarChar, 50).Value = strArrValue[8];						//8

					// 검사자 이름
					cmd.Parameters.Add("@inspectionPost", SqlDbType.VarChar, 15).Value = strArrValue[9];								//9

					// 검사자 ID - Session으로 받아온다.
					cmd.Parameters.Add("@inspectionPostID", SqlDbType.VarChar, 20).Value = Session["ID"].ToString();					//10 검사자 ID

					// 부적합 금액을 입력한 경우에는 그 금액을 세팅하고 그렇지 않으면 0 을 세팅한다.
					if ( strArrValue[10].ToString() != "" )
						cmd.Parameters.Add("@incongruityMoney", SqlDbType.Decimal).Value = Convert.ToDecimal( strArrValue[10] );		//11
					else
						cmd.Parameters.Add("@incongruityMoney", SqlDbType.Decimal).Value = 0;							

					// 원장구분1(외주, 자가, 구매) 세팅
					cmd.Parameters.Add("@hs1", SqlDbType.VarChar, 10).Value = strArrValue[11];											//12

					// 구분된 원장1의 인덱스
					cmd.Parameters.Add("@hindex1", SqlDbType.VarChar,10).Value = strArrValue[12];										//13

					// 원장구분2(구매발주원장, 외주발주원장) 세팅
					cmd.Parameters.Add("@hs2", SqlDbType.VarChar, 10).Value = strArrValue[13];											//14

					// 구분된 원장2의 인덱스
					cmd.Parameters.Add("@hindex2", SqlDbType.VarChar,10).Value = strArrValue[14];										//15

					// 품목명
					cmd.Parameters.Add("@ItemName" ,SqlDbType.VarChar, 30).Value = strArrValue[15];										//16

					// 품목 번호
					cmd.Parameters.Add("@ItemNumber" ,SqlDbType.VarChar, 30).Value = strArrValue[16];									//17

					// 도면번호
					cmd.Parameters.Add("@ItemDrawNum" ,SqlDbType.VarChar, 30).Value = strArrValue[17];									//18
					cmd.Parameters.Add("@CompanyName" ,SqlDbType.VarChar, 50).Value = strArrValue[18];									//19
					cmd.Parameters.Add("@BusinessRegistrationNum" ,SqlDbType.VarChar, 30).Value = strArrValue[19];						//20

					// 단가
					cmd.Parameters.Add("@applyUnitCost",SqlDbType.Decimal).Value = Convert.ToDecimal(strArrValue[20]);					//21

					// 원자재나 상품인 경우 시작공정 순서번호는 0 이고 그렇지 않으면 시작공정 순서번호를 세팅
					if ( strArrValue[21].ToString() == "원자재" || strArrValue[21].ToString() == "상품")
						cmd.Parameters.Add("@BeginProcessSequenceNum",SqlDbType.TinyInt).Value = 0;										//22
					else
						cmd.Parameters.Add("@BeginProcessSequenceNum",SqlDbType.TinyInt).Value = Convert.ToInt16(strArrValue[21]);

					// 원자재나 상품인 경우 종료공정 순서번호는 0 이고 그렇지 않으면 종료공정 순서번호를 세팅
					if ( strArrValue[22].ToString() == "원자재" || strArrValue[22].ToString() == "상품" )								//23
						cmd.Parameters.Add("@EndProcessSequenceNum",SqlDbType.TinyInt).Value = 0;										
					else
						cmd.Parameters.Add("@EndProcessSequenceNum",SqlDbType.TinyInt).Value = Convert.ToInt16(strArrValue[22]);
					
					// 시작공정 코드
					cmd.Parameters.Add("@BeginProcessNameCode" ,SqlDbType.VarChar, 20).Value = strArrValue[23];							//24

					// 종료 공정명
					cmd.Parameters.Add("@EndProcessName" ,SqlDbType.VarChar, 20).Value = strArrValue[24];								//25

					// 종료공정 코드
					cmd.Parameters.Add("@EndProcessNameCode" ,SqlDbType.VarChar, 20).Value = strArrValue[25];							//26

					string month = strArrValue[31];
					if(month.Length == 1)
						month = '0'+month;
					string day = strArrValue[32];
					if(day.Length == 1)
						day = '0'+day;

					cmd.Parameters.Add("@CompleteDate",strArrValue[30]+'-'+month+'-'+day);												//27

					cmd.Parameters.Add("@DeliveryYear",strArrValue[28]);																//28
					cmd.Parameters.Add("@DeliveryMonth",strArrValue[29]);																//29


					// 실행
					cmd.ExecuteNonQuery();		
					cmd.Parameters.Clear();




					if(!WorkPlan3())
					{


						//

						//이력원장 등록
						if(strArrValue[22].ToString() == "원자재")
						{
							str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','원자재창고','1',@Quantity,@Date,'품질검사',@HistoryIndex)";
				
							cmd.CommandText = str;
							cmd.CommandType = CommandType.Text;
							cmd.Transaction =tran;			
							cmd.Parameters.Add("@itemnum", strArrValue[16]);
							cmd.Parameters.Add("@itemdrawnum",strArrValue[17]);
							cmd.Parameters.Add("@itemname",strArrValue[15]);
							//cmd.Parameters.Add("@ProcessSequenceNum",0);
							//cmd.Parameters.Add("@ProcessCode", "14000000");
							//cmd.Parameters.Add("@ProcessName","소재");
							//cmd.Parameters.Add("@Division",1);												//입출고구분(입고)
							cmd.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1]));						//입출고 수량
							cmd.Parameters.Add("@Date",DateTime.Parse(strArrValue[30].ToString() +"-"+ strArrValue[31].ToString() +"-"+ strArrValue[32].ToString()).ToShortDateString());	//입출고일
							//cmd.Parameters.Add("@HistoryDivision","품질검사");								//원장구분
							cmd.Parameters.Add("@HistoryIndex",Convert.ToInt32(strArrValue[0]));				//원장번호
							int a = cmd.ExecuteNonQuery();	
							cmd.Parameters.Clear();
						}
						else if(strArrValue[22].ToString() == "상품")
						{
							str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,99,'14009999','최종품','생산창고','1',@Quantity,@Date,'품질검사',@HistoryIndex)";
				
							cmd.CommandText = str;
							cmd.CommandType = CommandType.Text;
							cmd.Transaction =tran;			
							cmd.Parameters.Add("@itemnum", strArrValue[16]);
							cmd.Parameters.Add("@itemdrawnum",strArrValue[17]);
							cmd.Parameters.Add("@itemname",strArrValue[15]);
							//cmd.Parameters.Add("@ProcessSequenceNum",99);
							//cmd.Parameters.Add("@ProcessCode", "14009999");
							//cmd.Parameters.Add("@ProcessName","최종품");
							//cmd.Parameters.Add("@Division",1);												//입출고구분(입고)
							cmd.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1]));						//입출고 수량
							cmd.Parameters.Add("@Date",DateTime.Parse(strArrValue[30].ToString() +"-"+ strArrValue[31].ToString() +"-"+ strArrValue[32].ToString()).ToShortDateString());	//입출고일
							//cmd.Parameters.Add("@HistoryDivision","품질검사");								//원장구분
							cmd.Parameters.Add("@HistoryIndex",Convert.ToInt32(strArrValue[0]));				//원장번호
							cmd.ExecuteNonQuery();	
							cmd.Parameters.Clear();
						}
						else if(strArrValue[11].Trim() == "외주")
						{
								/*

							// 외주품중 제품이면서 최종공정인 경우 영업창고로 입고
							if(Division(Convert.ToInt16(strArrValue[22]),strArrValue[16]))
							{
								str = str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'영업창고','1',@Quantity,@Date,'품질검사',@HistoryIndex)";
							}
							else
							{
								str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'생산창고','1',@Quantity,@Date,'품질검사',@HistoryIndex)";
							}
							
							*/

							str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'생산창고','1',@Quantity,@Date,'품질검사',@HistoryIndex)";

						

							cmd.CommandText = str;
							cmd.CommandType = CommandType.Text;
							cmd.Transaction =tran;			
							cmd.Parameters.Add("@itemnum", strArrValue[16]);
							cmd.Parameters.Add("@itemdrawnum",strArrValue[17]);
							cmd.Parameters.Add("@itemname",strArrValue[15]);
							cmd.Parameters.Add("@ProcessSequenceNum",Convert.ToInt16(strArrValue[22]));
							cmd.Parameters.Add("@ProcessCode", strArrValue[25]);
							cmd.Parameters.Add("@ProcessName",strArrValue[24]);
							cmd.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1]));						//입출고 수량
							cmd.Parameters.Add("@Date",DateTime.Parse(strArrValue[30].ToString() +"-"+ strArrValue[31].ToString() +"-"+ strArrValue[32].ToString()).ToShortDateString());	//입출고일
							//cmd.Parameters.Add("@HistoryDivision","품질검사");								//원장구분
							cmd.Parameters.Add("@HistoryIndex",Convert.ToInt32(strArrValue[0]));				//원장번호
							cmd.ExecuteNonQuery();	
							cmd.Parameters.Clear();
						}
						else if(Division(strArrValue[16],Convert.ToInt16(strArrValue[22])))
						{
							if(strArrValue[2].Trim() != "0")
							{
								str = @"Update  SH_HT Set Quantity = @Quantity Where HistoryDivision = '작업일보' and HistoryIndex=@HistoryIndex and ItemNum = @itemnum and ProcessSequenceNum = @ProcessSequenceNum";
								cmd.CommandText = str;
								cmd.CommandType = CommandType.Text;
								cmd.Transaction =tran;			
								cmd.Parameters.Add("@itemnum", strArrValue[16]);
								cmd.Parameters.Add("@ProcessSequenceNum",Convert.ToInt16(strArrValue[22]));
								cmd.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1]));						//입출고 수량
								//cmd.Parameters.Add("@HistoryDivision","품질검사");								//원장구분
								cmd.Parameters.Add("@HistoryIndex",Convert.ToInt32(strArrValue[12]));				//원장번호
								cmd.ExecuteNonQuery();	
								cmd.Parameters.Clear();
							}
						}
					}
					else
					{
						//

						//이력원장 등록
						if(strArrValue[22].ToString() == "원자재")
						{
							str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','원자재창고','1',@Quantity,@Date,'품질검사',@HistoryIndex)";
				
							cmd.CommandText = str;
							cmd.CommandType = CommandType.Text;
							cmd.Transaction =tran;			
							cmd.Parameters.Add("@itemnum", strArrValue[16]);
							cmd.Parameters.Add("@itemdrawnum",strArrValue[17]);
							cmd.Parameters.Add("@itemname",strArrValue[15]);
							//cmd.Parameters.Add("@ProcessSequenceNum",0);
							//cmd.Parameters.Add("@ProcessCode", "14000000");
							//cmd.Parameters.Add("@ProcessName","소재");
							//cmd.Parameters.Add("@Division",1);												//입출고구분(입고)
							cmd.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1]));						//입출고 수량
							cmd.Parameters.Add("@Date",DateTime.Parse(strArrValue[30].ToString() +"-"+ strArrValue[31].ToString() +"-"+ strArrValue[32].ToString()).ToShortDateString());	//입출고일
							//cmd.Parameters.Add("@HistoryDivision","품질검사");								//원장구분
							cmd.Parameters.Add("@HistoryIndex",Convert.ToInt32(strArrValue[0]));				//원장번호
							int a = cmd.ExecuteNonQuery();	
							cmd.Parameters.Clear();
						}
						else if(strArrValue[22].ToString() == "상품")
						{
							str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,99,'14009999','최종품','생산창고','1',@Quantity,@Date,'품질검사',@HistoryIndex)";
				
							cmd.CommandText = str;
							cmd.CommandType = CommandType.Text;
							cmd.Transaction =tran;			
							cmd.Parameters.Add("@itemnum", strArrValue[16]);
							cmd.Parameters.Add("@itemdrawnum",strArrValue[17]);
							cmd.Parameters.Add("@itemname",strArrValue[15]);
							//cmd.Parameters.Add("@ProcessSequenceNum",99);
							//cmd.Parameters.Add("@ProcessCode", "14009999");
							//cmd.Parameters.Add("@ProcessName","최종품");
							//cmd.Parameters.Add("@Division",1);												//입출고구분(입고)
							cmd.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1]));						//입출고 수량
							cmd.Parameters.Add("@Date",DateTime.Parse(strArrValue[30].ToString() +"-"+ strArrValue[31].ToString() +"-"+ strArrValue[32].ToString()).ToShortDateString());	//입출고일
							//cmd.Parameters.Add("@HistoryDivision","품질검사");								//원장구분
							cmd.Parameters.Add("@HistoryIndex",Convert.ToInt32(strArrValue[0]));				//원장번호
							cmd.ExecuteNonQuery();	
							cmd.Parameters.Clear();
						}
						else if(strArrValue[11].Trim() == "외주")
						{

							if(StoreManage(strArrValue[16]) == "예")
							{

								str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'생산창고','1',@Quantity,@Date,'품질검사',@HistoryIndex)";
								cmd.CommandText = str;
								cmd.CommandType = CommandType.Text;
								cmd.Transaction =tran;			
								cmd.Parameters.Add("@itemnum", strArrValue[16]);
								cmd.Parameters.Add("@itemdrawnum",strArrValue[17]);
								cmd.Parameters.Add("@itemname",strArrValue[15]);
								cmd.Parameters.Add("@ProcessSequenceNum",Convert.ToInt16(strArrValue[22]));
								cmd.Parameters.Add("@ProcessCode", strArrValue[25]);
								cmd.Parameters.Add("@ProcessName",strArrValue[24]);
								cmd.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1]));						//입출고 수량
								cmd.Parameters.Add("@Date",DateTime.Parse(strArrValue[30].ToString() +"-"+ strArrValue[31].ToString() +"-"+ strArrValue[32].ToString()).ToShortDateString());	//입출고일
								//cmd.Parameters.Add("@HistoryDivision","품질검사");								//원장구분
								cmd.Parameters.Add("@HistoryIndex",Convert.ToInt32(strArrValue[0]));				//원장번호
								cmd.ExecuteNonQuery();	
								cmd.Parameters.Clear();
							}


							InsertSH_HT(con,tran);
						}
						else if(Division(strArrValue[16],Convert.ToInt16(strArrValue[22])))
						{
							if(strArrValue[2].Trim() != "0")
							{
								str = @"Update  SH_HT Set Quantity = @Quantity Where HistoryDivision = '작업일보' and HistoryIndex=@HistoryIndex and ItemNum = @itemnum and ProcessSequenceNum = @ProcessSequenceNum";
								cmd.CommandText = str;
								cmd.CommandType = CommandType.Text;
								cmd.Transaction =tran;			
								cmd.Parameters.Add("@itemnum", strArrValue[16]);
								cmd.Parameters.Add("@ProcessSequenceNum",Convert.ToInt16(strArrValue[22]));
								cmd.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1]));						//입출고 수량
								//cmd.Parameters.Add("@HistoryDivision","품질검사");								//원장구분
								cmd.Parameters.Add("@HistoryIndex",Convert.ToInt32(strArrValue[12]));				//원장번호
								cmd.ExecuteNonQuery();	
								cmd.Parameters.Clear();
							}
						}
					}
					
					bResult = true;
						
					tran.Commit();
				}
				catch(Exception ex)
				{
					Alert(ex.Message);
					tran.Rollback();
				}
				finally
				{
					con.Close();
				}
			}
			else  // 외주 출고 품목이 정상적으로 출고되지 않은 품목인경우
			{
				Alert(" 현재 검사완료된 제품의 이전 출고 정보가 없습니다. ");
			}
			

			return bResult;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <param name="seq"></param>
		/// <returns></returns>
		private bool Division(string ItemNum, int seq)
		{
			string str = "Select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ItemNum = @ItemNum";
			SqlConnection con=  new SqlConnection( base.GetConnectionString );
			SqlCommand cmd = new SqlCommand(str, con);
			cmd.Parameters.Add("@ItemNum",ItemNum);
			con.Open();
			int count = int.Parse(cmd.ExecuteScalar().ToString());
			con.Close();
			if(seq == count)
				return false;
			else
				return true;
			
		}
		
		/// <summary>
		/// 납품잔량을 체크하여 이번 납품이 마이너스가 되지 않으면 true를 리턴한다. - 외주, 구매인경우 체크되고 자가의 경우 납품이 아니므로 MinusCheck() 메서드에서 마이너스 재고 체크만 한다.
		/// </summary>
		/// <returns>등록 가능 여부</returns>
		private bool RegisterationCheck()
		{
			bool bResult = false;
			
			if (  strArrValue[11] == "자가" )
			{
				bResult = true;
			}
			else
			{
				decimal dSuccessQuantity =  Convert.ToDecimal( strArrValue[1] );
				int nHindexNum = int.Parse(strArrValue[14]);
				bool bHistoryFalg ;

				if ( strArrValue[11] == "구매"  )
					bHistoryFalg = false;
				else 
					bHistoryFalg = true;

				SqlConnection con=  new SqlConnection( base.GetConnectionString );
				SqlCommand cmd = new SqlCommand("QualityInspection_RegisterCheck", con);
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add("@historyFlag", SqlDbType.Bit).Value = bHistoryFalg;
				cmd.Parameters.Add("@hindex", SqlDbType.Int).Value = nHindexNum;
				cmd.Parameters.Add("@quantity", SqlDbType.Decimal).Value = dSuccessQuantity;

				try
				{
					con.Open();
					bResult = Convert.ToBoolean( cmd.ExecuteScalar() );
				}
				catch(Exception ex)
				{
					Alert(ex.Message);
				}
				finally
				{
					con.Close();
				}
			}

			//return bResult;
			return true;
		}


	

		/// <summary>
		///  마이너스 재고를 허용하지 않는 상태에서 매개변수로 지정된 DataReader 를 조사하여 마이너스 재고로 조사되면 Exception 을 throw 한다.
		/// </summary>
		/// <param name="dr">조사할 DataReader</param>
		private void MinusCheck( SqlDataReader dr )
		{
			// 매개변수 dr은 자품목수 만큼의 Record Result를 가지고 있다.
			bool bResult = false;

			SqlConnection con=  new SqlConnection( ConfigurationSettings.AppSettings["Day"] );
			SqlCommand cmd = new SqlCommand("select MinusRowMaterialPermissionUsed from scs_t", con);
			
			try
			{
				con.Open();
				bResult = Convert.ToBoolean( cmd.ExecuteScalar() );		
	
				// 마이너스 재고를 허용하지 않는다면 조사한다.
				if ( !bResult  )
				{
					do
					{
						if ( dr.Read() )
						{
							if ( Convert.ToDecimal( dr[0] ) < 0 )
							{
								throw new Exception("재고량 부족으로 등록할 수 없습니다.");
							}
						}

					}while( dr.NextResult() );
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				dr.Close();
				con.Close();
			}
		}


		private void InsertSH_HT(SqlConnection conn, SqlTransaction tr)
		{
/*
			0 - DB의 Index Key
			1 - 검사합격수량
			2 - 부적합수량
			3 - 검사판정
			4 - 부적합원인
			5 - 부적합원인 코드
			6 - 부적합현상
			7 - 부적합현상 코드
			8 - 부적합 세부 내용
			9 - 검사자 이름
			10 - 부적합 금액
			11 - 원장구분 = ("구매", "외주", "자가" 중 1) - 구매납품의뢰, 외주납품의뢰, 작업일보원장 구분
			12 - 구분된 원장1의 인덱스
			13 - 원장구분2 - (구매발주원장, 외주발주원장)  -외주나 구매인경우만 해당
			14 - 원장구분2 인덱스						 -외주나 구매인경우만 해당
			15 - 품목명
			16 - 품목번호
			17 - 도면번호
			18 - 거래처명
			19 - 거래처의 사업자 등록번호
			20 - 단가
			21 - 시작공정 순서번호
			22 - 종료공정 순서번호
			23 - 시작공정코드
			24 - 종료공정명
			25 - 종료공정코드
			26 - 검사판정 DropDownList 선택 여부 - 사용자가 선택한 경우 0 이 아닌 값을 가짐
			27 - 날짜
			28 - 매입년
			29 - 매입월
			30 - 검사년도
			31 - 검사월
			32 - 검사일
*/

			SqlCommand comm = new SqlCommand();
			comm.Connection=conn;
			comm.Transaction = tr;






			
			
			//지금 공정순서보다 아래에 공정순서가 있는지 없는지를 구한다
			int sequence1 = 0;	//공정순서
			string Code = "";	// 공정코드
			string Name = "";	// 공정명
			
			string str = "Select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ProcessSequenceNum < @num and ItemNum = @itemnum";
			
			comm.Parameters.Add("@num", int.Parse(strArrValue[22].ToString()));
			comm.Parameters.Add("@itemnum",strArrValue[17].ToString());
			comm.CommandText = str;
			if(comm.ExecuteScalar() == null || comm.ExecuteScalar().ToString().Trim() == "")
				sequence1 = 0;
			else
				sequence1 = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			
			str = "Select ProcessCode From PSI_MT Where RecodingState = 1 and ProcessSequenceNum = @num and ItemNum = @itemnum";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = sequence1;
			comm.Parameters.Add("@itemnum",strArrValue[17].ToString());
			comm.CommandText = str;
			SqlDataReader dr2 = comm.ExecuteReader();
			while(dr2.Read())
			{
				Code = dr2["ProcessCode"].ToString();
			}
			dr2.Close();
			comm.Parameters.Clear();

			str = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code";
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = Code;
			comm.CommandText = str;
			SqlDataReader dr3 = comm.ExecuteReader();
			while(dr3.Read())
			{
				Name = dr3["SmallClassificationName"].ToString();
			}
			dr3.Close();
			comm.Parameters.Clear();
		

			

			// 아래 공정순서번호가 존재할때는(공정순서가 0이 아닌 경우) 바로 그 품목을 출고시킨다.
			// 지금 들어있는 공정순서번호와 품목을 출고시킨다.
			string st_ItemNum = strArrValue[17].ToString();

			if(sequence1 != 0)
			{
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'외주창고','0',@Quantity,@Date,'품질검사',@HistoryIndex)";
				

				comm.Parameters.Add("@itemnum",strArrValue[16].ToString());									//품목번호
				comm.Parameters.Add("@itemdrawnum", strArrValue[17].ToString());								//도면번호
				comm.Parameters.Add("@itemname",strArrValue[15].ToString());									//품목명
				comm.Parameters.Add("@ProcessSequenceNum",sequence1);											//공정순서
				comm.Parameters.Add("@ProcessCode",Code);											//공정코드
				comm.Parameters.Add("@ProcessName",Name);												//공정명
				comm.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1].ToString()));					//입출고 수량
				comm.Parameters.Add("@Date",DateTime.Parse(strArrValue[30].ToString() +"-"+ strArrValue[31].ToString() +"-"+ strArrValue[32].ToString()).ToShortDateString());	//입출고일
				//comm.Parameters.Add("@HistoryDivision","구매납품");										//원장구분
				comm.Parameters.Add("@HistoryIndex",int.Parse(strArrValue[0].ToString()));	
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

			}
			else
			{
				// 순서번호가 0 이면 하위품목을 찾아 스택에 Push한다
				Stack st = new Stack();	

				string item = "";
				//하위품목을 찾기위해서는 품목구성정보에 지금 품목을 모품으로 하는 모든 레코드를 스택에 넣는다
				//str = "Select ChildItemNum, ItemDrawNum, Item From IOI_MT JOIN ON II_MT Where RecodingState = 1 and ParentItemNum = @itemnum";
				str = @"SELECT ItemNum, ItemDrawNum, ItemName, NeedQuantityNumerator, NeedQuantityDenominator FROM II_MT INNER JOIN IOI_MT ON II_MT.ItemNum = IOI_MT.ChildItemNum 	WHERE (II_MT.RecodingState = 1) AND (IOI_MT.RecodingState = 1) AND (IOI_MT.ParentItemNum = @itemnum) ";
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
				comm.CommandText=str;
				SqlDataReader dr_Child = comm.ExecuteReader();
				while(dr_Child.Read())
				{
					st.Push(dr_Child["ItemNum"].ToString());
				}
				dr_Child.Close();
				comm.Parameters.Clear();
				
				
				// 스택에 품목이 있는지 확인한다.
				if(st.Count != 0)
				{
					// 스택의 내용이 있으면 다 꺼낼때까지 계속 Pop한다
					while(st.Count > 0)
					{	
						item =st.Pop().ToString();
						decimal quantity = 1;

						// 꺼낸품목의 공정순서를 스택1에 넣는다
						Stack st1 = new Stack();					
						int sequencenum=0;

						//꺼낸 품목의 공정순서를 넣어둔다.
						str = "Select ProcessSequenceNum from PSI_MT where RecodingState = 1 AND ItemNum = @itemnum  order by ProcessSequenceNum";
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
						comm.CommandText = str;
						SqlDataReader dr_Sequence = comm.ExecuteReader();
						while(dr_Sequence.Read())
						{
							//공정순서
							st1.Push(int.Parse(dr_Sequence["ProcessSequenceNum"].ToString()));
							
						}
						dr_Sequence.Close();
						comm.Parameters.Clear();

						//꺼낸품목의 소요량을 넣어둔다
						str = "select * From IOI_MT Where ParentItemNum = @num and ChildItemNum = @item and RecodingState = 1 ";
						comm.Parameters.Add("@num",st_ItemNum);
						comm.Parameters.Add("@item",item);
						comm.CommandText = str;
						SqlDataReader dr1 = comm.ExecuteReader();
						while(dr1.Read())
						{
							quantity = decimal.Round((decimal.Parse(dr1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr1["NeedQuantityDenominator"].ToString())),3);
						}
						dr1.Close();
						comm.Parameters.Clear();

						//공정순서가 없으면 지금 품목이 원자재이므로 이를 출고시킨다.
						if(st1.Count == 0)
						{
							//품번,도번,품명 구한다
							string itemdraw=""; string name="";
							str = "Select * From II_MT Where ItemNum = @num and RecodingState = 1";
							comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
							comm.CommandText = str;
							SqlDataReader dr_Item = comm.ExecuteReader();
							while(dr_Item.Read())
							{
								itemdraw = dr_Item["ItemDrawNum"].ToString();
								name = dr_Item["ItemName"].ToString();
							}
							dr_Item.Close();
							comm.Parameters.Clear();

							str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','외주창고','0',@Quantity,@Date,'품질검사',@HistoryIndex)";
				

							comm.Parameters.Add("@itemnum",item);									//품목번호
							comm.Parameters.Add("@itemdrawnum", itemdraw);								//도면번호
							comm.Parameters.Add("@itemname",name);									//품목명
							//comm.Parameters.Add("@ProcessSequenceNum",strArrValue[17].ToString());											//공정순서
							//comm.Parameters.Add("@ProcessCode",strArrValue[17].ToString());											//공정코드
							//comm.Parameters.Add("@ProcessName",strArrValue[17].ToString());												//공정명
							//comm.Parameters.Add("@Division",0);														//입출고구분
							comm.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1].ToString())*quantity);					//입출고 수량
							comm.Parameters.Add("@Date",DateTime.Parse(strArrValue[30].ToString() +"-"+ strArrValue[31].ToString() +"-"+ strArrValue[32].ToString()).ToShortDateString());	//입출고일
							//comm.Parameters.Add("@HistoryDivision","구매납품");										//원장구분
							comm.Parameters.Add("@HistoryIndex",int.Parse(strArrValue[0].ToString()));	
							comm.CommandText = str;
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();							
						}
						else //그렇지 않으면 그 품목의 최상위 공정순서를 찾아서 그 공정이 외주인지 판단한다. 카운트가 0이면 외주가 아니므로 출고시킨다.
						{
							// 스택1에 있는 공정순서를 하나씩꺼낸다.
							while(st1.Count > 0)
							{
								sequencenum = int.Parse(st1.Pop().ToString());
								str = " SELECT  count(*) FROM PSI_MT WHERE RecodingState = 1 and WorkDistinction <> '외주' and ItemNum = @item and ProcessSequenceNum = @num";
								comm.Parameters.Add("@item",SqlDbType.VarChar).Value = item;//품목번호
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;//공정순서
								comm.CommandText=str;
								int aaa = int.Parse(comm.ExecuteScalar().ToString());
								comm.Parameters.Clear();
								

								//품번,도번,품명 구한다
								string itemdraw=""; string name="";
								str = "Select * From II_MT Where ItemNum = @num";
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
								comm.CommandText =str;
								SqlDataReader dr_Item = comm.ExecuteReader();
								while(dr_Item.Read())
								{
									itemdraw = dr_Item["ItemDrawNum"].ToString();
									name = dr_Item["ItemName"].ToString();
								}
								dr_Item.Close();
								comm.Parameters.Clear();

								//공정코드와 공정명을 구한다
								string processcode="";string processname="";
								str = " SELECT  ProcessSequenceNum, ProcessCode, SmallClassificationName " +
									" FROM      PSI_MT INNER JOIN " +
									" PUC_MT ON ProcessCode = SmallClassificationCode " +
									" WHERE PSI_MT.RecodingState = 1 AND ItemNum = @itemnum and ProcessSequenceNum = @num";
								comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;
								comm.CommandText=str;
								SqlDataReader dr_code = comm.ExecuteReader();
								while(dr_code.Read())
								{
									processcode = dr_code["ProcessCode"].ToString();//공정코드
									processname = dr_code["SmallClassificationName"].ToString();//공정명
								}
								dr_code.Close();
								comm.Parameters.Clear();

								// 외주가 아니면 출고
								if(aaa != 0)
								{
									str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
									@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'외주창고','0',@Quantity,@Date,'품질검사',@HistoryIndex)";
				

									comm.Parameters.Add("@itemnum",item);									//품목번호
									comm.Parameters.Add("@itemdrawnum", itemdraw);								//도면번호
									comm.Parameters.Add("@itemname",name);									//품목명
									comm.Parameters.Add("@ProcessSequenceNum",sequencenum);											//공정순서
									comm.Parameters.Add("@ProcessCode",processcode);											//공정코드
									comm.Parameters.Add("@ProcessName",processname);												//공정명
									//comm.Parameters.Add("@Division",0);														//입출고구분
									comm.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1].ToString())*quantity);					//입출고 수량
									comm.Parameters.Add("@Date",DateTime.Parse(strArrValue[30].ToString() +"-"+ strArrValue[31].ToString() +"-"+ strArrValue[32].ToString()).ToShortDateString());	//입출고일
									//comm.Parameters.Add("@HistoryDivision","구매납품");										//원장구분
									comm.Parameters.Add("@HistoryIndex",int.Parse(strArrValue[0].ToString()));	
									comm.CommandText = str;
									comm.ExecuteNonQuery();
									comm.Parameters.Clear();
									
									st1.Clear();
									break;
								}
								else//외주이면 외주단가에서 지금 찾은 공정이랑 종료공정이 동일한것이 있는지 살펴본다. 있으면 출고시키고 없으면 이전공정을 찾는다
								{
									// 외주단가에서 동일회사 외주공정인지 파악한다.
									str = "select count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @itemnum and EndProcessCode = @code and BusinessRegistrationNum = @com";
									comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
									comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;
									comm.Parameters.Add("@com",SqlDbType.VarChar).Value = strArrValue[19].ToString() ;
									comm.CommandText = str;
									int find = int.Parse(comm.ExecuteScalar().ToString());
									comm.Parameters.Clear();
									

									// 동일회사가 아니면 출고
									if(find == 0)
									{
										// 데이터셋에 추가하기위한 Row
										str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
									@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'외주창고','0',@Quantity,@Date,'품질검사',@HistoryIndex)";
				

										comm.Parameters.Add("@itemnum",item);									//품목번호
										comm.Parameters.Add("@itemdrawnum", itemdraw);								//도면번호
										comm.Parameters.Add("@itemname",name);									//품목명
										comm.Parameters.Add("@ProcessSequenceNum",sequencenum);											//공정순서
										comm.Parameters.Add("@ProcessCode",processcode);											//공정코드
										comm.Parameters.Add("@ProcessName",processname);													//공정명
										//comm.Parameters.Add("@Division",0);														//입출고구분
										comm.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1].ToString())*quantity);					//입출고 수량
										comm.Parameters.Add("@Date",DateTime.Parse(strArrValue[30].ToString() +"-"+ strArrValue[31].ToString() +"-"+ strArrValue[32].ToString()).ToShortDateString());	//입출고일
										//comm.Parameters.Add("@HistoryDivision","구매납품");										//원장구분
										comm.Parameters.Add("@HistoryIndex",int.Parse(strArrValue[0].ToString()));	
										comm.CommandText = str;
										comm.ExecuteNonQuery();
										comm.Parameters.Clear();

										st1.Clear();
										break;
									}
								}
							}
						}
					}
				}
			}




		}














	}
}
