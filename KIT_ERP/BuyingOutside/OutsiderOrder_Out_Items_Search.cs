using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


namespace KIT_ERP.BuyingOutside
{
	/// <summary>
	/// 출고될 하위 품목을 검색
	/// </summary>
	public class OutsiderOrder_Out_Item : KIT_ERP.QualityInspection.QualityInspection_BaseClass
	{
		// 품목번호
		private string m_itemNum;
		// 시작공정 순서번호
		private int m_BeginProcessSequenceNum;
		// 합격 수량
		private decimal m_Quantity ;
		// 거래처 사업자 등록번호
		private string m_BusinessRagistrationNum ; 

		private static int m_Level = 2 ;
		private DataRow tmpItem_DataRow ;	// TempTow
		private DataRow tmpSeq_DataRow ;	// TempTow

		// 리턴될 데이터 테이블 선언
		private DataTable dt_Result ;
		
		// 생성자 
		public OutsiderOrder_Out_Item
									(
									  string strItemNum						// 품목명
									, int intBeginProcessSequenceNum		// 시작공정 순서 번호
									, decimal intQuantity						// 수량(합격수량)
									, string strBusinessRagistrationNum		// 거래처 사업자 등록번호
									)
		{
			// 각 맴버변수에 대입
			this.m_itemNum = strItemNum;
			this.m_BeginProcessSequenceNum = intBeginProcessSequenceNum;
			this.m_Quantity = intQuantity;
			this.m_BusinessRagistrationNum = strBusinessRagistrationNum;

			// 리턴될 데이터 테이블 생성
			dt_Result = new DataTable();

			// 품목명(itemnum) ,  공정순서번호(ProcessSequenceNum) ,  수량(Quantity) ,  금액(Cost) 
			// 4개의 컬럼을 가지는 테이블을 생성한다.
			DataColumn dc = new DataColumn("itemnum", typeof(string));
			dt_Result.Columns.Add(dc);
			dc = new DataColumn("ProcessSequenceNum", typeof(int));
			dt_Result.Columns.Add(dc);
			dc = new DataColumn("Quantity", typeof(Decimal));
			dt_Result.Columns.Add(dc);
			dc = new DataColumn("Cost", typeof(Decimal));
			dt_Result.Columns.Add(dc);
		}


		/// <summary>
		/// 현재 품목의 하위품목 출고정보를 리턴한다.
		/// </summary>
		/// <returns>하위품목 정보를 DataTable로 돌려준다.</returns>
		public DataTable getData()
		{
			SqlConnection con = new SqlConnection( base.GetConnectionString );
			SqlCommand cmd = new SqlCommand( "PreSequenceNumExists" ,  con );
			cmd.CommandType = CommandType.StoredProcedure;
			
			cmd.Parameters.Add( "@ItemNum", SqlDbType.VarChar, 20).Value = this.m_itemNum;
			cmd.Parameters.Add( "@BeginProcessSequenceNum", SqlDbType.Int).Value = this.m_BeginProcessSequenceNum;
			cmd.Parameters.Add( "@quantity", SqlDbType.Decimal).Value = this.m_Quantity;

			con.Open();
			SqlDataReader dr = cmd.ExecuteReader();

			//   PreSequenceNumExists Stored Procedure는 이전 공정 번호를 알아온다.
			//	PreSequenceNumExists 'ABCD', 10, 1 실행 결과는 다음과 같다.
			//	
			//	ItemNum	ProcessSequenceNum	Cost
			//  ------------------------------------------
			//	ABCD				10				5000


			if ( dr.Read() )	// 이전 공정이 있다면(1:1 인경우에 해당됨.)
			{
				tmpItem_DataRow = dt_Result.NewRow();
				tmpItem_DataRow[0] = dr[0].ToString();
				tmpItem_DataRow[1]= Convert.ToInt32( dr[1].ToString() );
				tmpItem_DataRow[2]= 1;
				tmpItem_DataRow[3]= Convert.ToDecimal( dr[2].ToString() );

				dt_Result.Rows.Add( tmpItem_DataRow );

				dr.Close();
				con.Close();
			}
			else	// 이전 공정이 없으면 품목구성을 푼다.
			{
				dr.Close();
				con.Close();

				Stack st_ChildItem = new Stack();
				this.Search(st_ChildItem, this.m_itemNum);
			}

			return dt_Result;
		}




//============================================================================================================
//=============================|	아래 부터는 모두 내부 메서드로 사용됨		|=================================
//============================================================================================================

		private Stack Process_FillStack( string strItemNum, string strNeedQuantity )
		{
			DataTable dt_ProcessSequenceItem =  this.get_ChildItem_ProcessSequenceNum( strItemNum, strNeedQuantity );
			Stack st = new Stack();

			foreach( DataRow sequence_item in dt_ProcessSequenceItem.Rows )	// 스택 2	채우기
			{
				st.Push(sequence_item);
			}

			return st;
		}


		private DataTable get_ChildItem_ProcessSequenceNum ( string strItemNum, string strNeedQuantity )
		{
			SqlDataAdapter adap = new SqlDataAdapter("Item_ProcessSequence", base.GetConnectionString );
			adap.SelectCommand.Parameters.Add("@ItemNum", SqlDbType.VarChar, 25).Value = strItemNum;
			adap.SelectCommand.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = strNeedQuantity;
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;

			DataSet ds = new DataSet();
			adap.Fill(ds);

			return ds.Tables[0];
		}


		// Search 메소드에서 인자로 넘어온 ItemNum 의 공정순서 품목을 스택으로 리턴
		private Stack ChildItem_FillStack( string strItemNum )
		{
			DataTable dt_Childitem = get_ChildItem(m_Level, strItemNum);
			Stack st = new Stack();

			foreach( DataRow item in dt_Childitem.Rows )	// 스택 1	채우기
			{
				st.Push(item);
			}

			return st;
		}


		private DataTable get_ChildItem(int level, string ItemNum)
		{
			SqlDataAdapter adap = new SqlDataAdapter("OutOrder_ChildItem_BOM", base.GetConnectionString );
			adap.SelectCommand.Parameters.Add("@ItemNum", SqlDbType.VarChar, 25).Value = ItemNum;
			adap.SelectCommand.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = this.m_Quantity;
			adap.SelectCommand.Parameters.Add("@Level", SqlDbType.Int).Value = level;
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;

			DataSet ds = new DataSet();
			adap.Fill(ds);

			return ds.Tables[0];
		}


		// 스택 1 입력
		private void Search(Stack st_ChildItem,  string ItemNum)
		{
			// ItemNum 이 있는 재귀 호출이면 인자로 넘어온 스택 위에 엎어쓰기
			// ItemNum 이 없는 재귀 호출이면 인자로 넘어온 스택으로 검사.

			if ( ItemNum != null )	// 맨 처음 호출시 , 엎어쓰기
			{

			////////////////////////////////////////////////////////////////////////////////////////
			//																				
			//	하위 품목을 st_ChildItem 스택1에 채우기	( 품목별 )							
			//	Stored Procedure 명 : Item_ProcessSequence							
			//																
			///////////////////////////////////////////////////////////////////////////////////////
				
				DataTable dt_Childitem = get_ChildItem(m_Level, ItemNum);
				foreach( DataRow item in dt_Childitem.Rows )	
				{
					st_ChildItem.Push(item);
				}
			}


			for ( int i = 0 ; i < st_ChildItem.Count ; i ++ )			// 스택 1 이 다 종료 될때 까지..
			{
				tmpItem_DataRow = (DataRow)st_ChildItem.Pop();

				// 현재 스택1의 품목이 원자재 면 여지없이 출고
				if ( tmpItem_DataRow[2].ToString()  ==  "원자재" )
				{
					// 출고 테이블에 저장
					this.SaveItem(tmpItem_DataRow);
				}
				else		// 반제품이면...
				{

				////////////////////////////////////////////////////////////////////////////////////////
				//																
				//	하위 품목의 공정순서를 st_ProcessSequenceItem 스택 2 채우기 ( 공정순서 별 )		
				//	Stored Procedure 명 : Item_ProcessSequence							
				//																
				///////////////////////////////////////////////////////////////////////////////////////

					Stack st_ProcessSequenceItem = Process_FillStack
						( 
							tmpItem_DataRow[0].ToString()			// 품목번호
							, tmpItem_DataRow[3].ToString()			// 필요수량
						);
						
					for ( int j = 0 ; j < st_ProcessSequenceItem.Count ; j ++ )	// 스택 2 가 종료 될때 까지
					{
						// 공정순서 Item 뽑아내기
						tmpSeq_DataRow = ( DataRow )st_ProcessSequenceItem.Pop();
						
						// 조건이 만족하면 출고하고 스택2 종료
						if (  tmpSeq_DataRow[2].ToString() !=  m_BusinessRagistrationNum )
						{
							// 출고 테이블에 저장
							this.SaveItem( tmpSeq_DataRow );

							// 스택 2 초기화
							st_ProcessSequenceItem.Clear();

							// 현재 스택1의 품목은 종료되고 다음 스택1의 품목 검사
							Search( st_ChildItem ,  null);
						}
					} // for 스택 2 가 종료 될때 까지

				}// 스택1의 품목중 반제품인 경우
			}// 스택 1 	
		}


		/// <summary>
		/// 매개변수에 지정된 품목을 출고 DataTable에 입력한다.
		/// </summary>
		/// <param name="drow_item"></param>
		private void SaveItem(DataRow drow_item)
		{
			DataRow dd = dt_Result.NewRow();
			dd[0] = drow_item[0].ToString();
			dd[1] = drow_item[1].ToString();
			dd[2] = drow_item[3].ToString();
			dd[3] = drow_item[4].ToString();

			dt_Result.Rows.Add( dd );
		}

	}
}
