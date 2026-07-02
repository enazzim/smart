using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;


namespace KIT_ERP.QualityInspection
{
	public class QualityInspection_Edit : QualityInspection_BaseClass
	{
		string[] strArrValue;

		public QualityInspection_Edit(string[] strArrValue)
		{
			this.strArrValue = strArrValue;
		}

		private StringBuilder Registeration_OutSideStoreHouse()
		{
			// 외주는 1 : 1 과 1 : n 으로 나뉜다.
			StringBuilder sB_SQL = new StringBuilder( 100 );
			OutsiderOrder_Out_Item OOI_Search = 
				new OutsiderOrder_Out_Item
				(
					strArrValue[17]					// 품목번호
					, Convert.ToInt32( strArrValue[22] )	// 시작 공정 순서 번호
					, Convert.ToInt32( strArrValue[27] )	// 차이 수량 
					, strArrValue[20]					// 사업자 등록번호
				) ;

			// 해당 품목의 이전 공정 품목이 있는경우는 dt_OutsiderOrder_Out_Items.Rows[3] 필드에  
			// ( 진척률 * 단가 * 수량 ) 이 출력되어 나오고.
			// 하위 BOM 을 풀어 계산한 경우는 dt_OutsiderOrder_Out_Items.Rows[3] 필드에  
			// ( 진척률 * 단가 ) 만 출력 되므로 수량(dt_OutsiderOrder_Out_Items[2]) 을 곱하면 Total 금액이 된다.

			DataTable dt_OutsiderOrder_Out_Items = OOI_Search.getData();
			
			/*
				 * ==================================================================
				 *					외주창고 업데이트
				 * ==================================================================
			*/

			// 출고품목이 BOM 이나 이전공정 순서에 존재하면 ( 출고된 품목이 존재하면 또는 정상적인 품목이라면 )
			if ( dt_OutsiderOrder_Out_Items.Rows.Count > 0 )
			{
				decimal successQuantity = Convert.ToDecimal( strArrValue[1] );
				decimal deTotalCost = ( successQuantity * Convert.ToDecimal( strArrValue[21] ) );	// 생산창고에 입고 될 금액	[21] 은 적용단가
				
				foreach ( DataRow drow_item in dt_OutsiderOrder_Out_Items.Rows )
				{
					int int_OS_MT_MonthCount = int.Parse(strArrValue[28]);
					string strItemNum = drow_item[0].ToString();
					string strProcessSEQNum = drow_item[1].ToString();
					string strQuantity = drow_item[2].ToString();
					string strTotalCost = Convert.ToString( ( Convert.ToDecimal( drow_item[3] ) * Convert.ToDecimal( strQuantity ) ) );
			
					sB_SQL.Append(" update OS_MT SET ");
					sB_SQL.Append(" OutStorehouseQuantity");
					sB_SQL.Append( int_OS_MT_MonthCount );
					sB_SQL.Append(" = ");
					sB_SQL.Append("OutStorehouseQuantity");
					sB_SQL.Append( int_OS_MT_MonthCount );
					sB_SQL.Append(" + "); 
					sB_SQL.Append( strQuantity );
					sB_SQL.Append(" , ");
					sB_SQL.Append("OutStorehouseCost");
					sB_SQL.Append( int_OS_MT_MonthCount );
					sB_SQL.Append(" = ");
					sB_SQL.Append("OutStorehouseCost");
					sB_SQL.Append( int_OS_MT_MonthCount );
					sB_SQL.Append(" + "); 
					sB_SQL.Append( strTotalCost );

					for ( int_OS_MT_MonthCount ++ ; int_OS_MT_MonthCount < 13 ; int_OS_MT_MonthCount ++ )
					{
						sB_SQL.Append(" , OutStorehouseQuantity");
						sB_SQL.Append( int_OS_MT_MonthCount );
						sB_SQL.Append(" = ");
						sB_SQL.Append("OutStorehouseQuantity");
						sB_SQL.Append( int_OS_MT_MonthCount );
						sB_SQL.Append(" + "); 
						sB_SQL.Append( strQuantity );
						sB_SQL.Append(" , ");
						sB_SQL.Append("OutStorehouseCost");
						sB_SQL.Append( int_OS_MT_MonthCount );
						sB_SQL.Append(" = ");
						sB_SQL.Append("OutStorehouseCost");
						sB_SQL.Append( int_OS_MT_MonthCount );
						sB_SQL.Append(" + "); 
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
				}


				if(!WorkPlan3())
				{
					int int_PS_MT_MonthCount = System.DateTime.Now.Month;

					sB_SQL.Append("   update PS_MT set ");
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


					for ( int_PS_MT_MonthCount ++ ; int_PS_MT_MonthCount < 13 ; int_PS_MT_MonthCount ++ )
					{
						sB_SQL.Append(" ,  InStorehouseQuantity");
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
					}

					sB_SQL.Append( " where itemnum = '" );
					sB_SQL.Append( strArrValue[16] );
					sB_SQL.Append( "' and " );
					sB_SQL.Append( " processsequencenum =  " );
					sB_SQL.Append( strArrValue[22] );
					sB_SQL.Append( " and " );
					sB_SQL.Append( " recodingstate =  1 " );
				}
			}
			else
			{
				sB_SQL.Append("X");
			}

			return sB_SQL;
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




		public void Edit()
		{
			
				SqlConnection con=  new SqlConnection( base.GetConnectionString );
				SqlCommand cmd = new SqlCommand("", con);
				con.Open();

				SqlTransaction tran = con.BeginTransaction();
				cmd.Transaction = tran;

				
				try
				{
					
					cmd.CommandText = "QualityInspection_Edit";
					cmd.CommandType = CommandType.StoredProcedure;
			
					cmd.Parameters.Add("@key",SqlDbType.Int).Value = Convert.ToInt32(strArrValue[0]);								// index key
					cmd.Parameters.Add("@successQuantity", SqlDbType.Decimal).Value = Convert.ToDecimal(strArrValue[1]);		// 적합수량
					cmd.Parameters.Add("@incongruityQuantity", SqlDbType.Decimal).Value = Convert.ToDecimal(strArrValue[2]);		// 부적합수량

					if ( strArrValue[26].ToString() != "0" )
						cmd.Parameters.Add("@incongruityDecision", SqlDbType.VarChar, 30).Value = strArrValue[3];						// 검사판정

					if ( strArrValue[5].ToString() != "0" )
					{
						cmd.Parameters.Add("@incongruityCause", SqlDbType.VarChar, 20).Value = strArrValue[4];							// 부적합원인
						cmd.Parameters.Add("@incongruityCauseCode", SqlDbType.VarChar, 20).Value = strArrValue[5];						// 부적합원인 코드
					}

					if ( strArrValue[7].ToString() != "0" )
					{
						cmd.Parameters.Add("@incongruityPhenomenon", SqlDbType.VarChar, 20).Value = strArrValue[6];					// 부적합 현상
						cmd.Parameters.Add("@incongruityPhenomenonCode", SqlDbType.VarChar, 20).Value = strArrValue[7];
					}

					cmd.Parameters.Add("@incongruityDetailContent", SqlDbType.VarChar, 50).Value = strArrValue[8];
					cmd.Parameters.Add("@inspectionPost", SqlDbType.VarChar, 15).Value = strArrValue[9];
					cmd.Parameters.Add("@inspectionPostID", SqlDbType.VarChar, 20).Value = strArrValue[10];							// 검사자 ID
			
					if ( strArrValue[11].ToString() != "" )
						cmd.Parameters.Add("@incongruityMoney", SqlDbType.Decimal).Value = Convert.ToDecimal(strArrValue[11]);
					else
						cmd.Parameters.Add("@incongruityMoney", SqlDbType.Decimal).Value = 0;

					cmd.Parameters.Add("@hs1", SqlDbType.VarChar, 10).Value = strArrValue[12];
					cmd.Parameters.Add("@hindex1", SqlDbType.VarChar,10).Value = strArrValue[13];
					cmd.Parameters.Add("@hs2", SqlDbType.VarChar, 10).Value = strArrValue[14];
					cmd.Parameters.Add("@hindex2", SqlDbType.VarChar,10).Value = strArrValue[15];
					cmd.Parameters.Add("@ItemName" ,SqlDbType.VarChar, 30).Value = strArrValue[16];
					cmd.Parameters.Add("@ItemNumber" ,SqlDbType.VarChar, 30).Value = strArrValue[17];
					cmd.Parameters.Add("@ItemDrawNum" ,SqlDbType.VarChar, 30).Value = strArrValue[18];
					cmd.Parameters.Add("@CompanyName" ,SqlDbType.VarChar, 50).Value = strArrValue[19];
					cmd.Parameters.Add("@BusinessRegistrationNum" ,SqlDbType.VarChar, 30).Value = strArrValue[20];
					cmd.Parameters.Add("@applyUnitCost",SqlDbType.Decimal).Value = Convert.ToDecimal(strArrValue[21]);

					if ( strArrValue[22].ToString() == "원자재" || strArrValue[22].ToString() == "상품")
						cmd.Parameters.Add("@BeginProcessSequenceNum",SqlDbType.TinyInt).Value = 0;
					else
						cmd.Parameters.Add("@BeginProcessSequenceNum",SqlDbType.TinyInt).Value = Convert.ToInt16(strArrValue[22]);

					if ( strArrValue[23].ToString() == "원자재" || strArrValue[23].ToString() == "상품" )
						cmd.Parameters.Add("@EndProcessSequenceNum",SqlDbType.TinyInt).Value = 0;//Convert.ToInt32(strArrValue[22]);
					else
						cmd.Parameters.Add("@EndProcessSequenceNum",SqlDbType.TinyInt).Value = Convert.ToInt16(strArrValue[23]);

					cmd.Parameters.Add("@BeginProcessNameCode" ,SqlDbType.VarChar, 20).Value = strArrValue[24]; 
					cmd.Parameters.Add("@EndProcessName" ,SqlDbType.VarChar, 20).Value = strArrValue[25]; 
					cmd.Parameters.Add("@EndProcessNameCode" ,SqlDbType.VarChar, 20).Value = strArrValue[26]; 

					// 창고업데이트용수량
					cmd.Parameters.Add("@DifferenceQuantity" ,SqlDbType.Decimal).Value = strArrValue[27]; 
					cmd.Parameters.Add("@DeliveryYear", strArrValue[30]);
					cmd.Parameters.Add("@DeliveryMonth", strArrValue[31]);

					cmd.ExecuteNonQuery();

					tran.Commit();
					Alert("수정 되었습니다!");
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
		
	}
}
