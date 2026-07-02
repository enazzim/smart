using System;
using System.Data;
using System.Data.SqlClient;

namespace KIT_ERP.ManagementInfomation
{
	/// <summary>
	/// QualityInspection_Edit에 대한 요약 설명입니다.
	/// </summary>
	public class QualityInspection_Edit : KIT_ERP.QualityInspection.QualityInspection_BaseClass
	{
		string[] strArrValues;

		/// <summary>
		/// 물리적 DB 에 Update될 값
		/// </summary>
		/// <param name="strArrValues">// 원장키 ; 적합수량 ; 부적합수량 ; 부적합원인 ;부적합원인코드; 부적합현상; 부적합현상코드; 부적합판정 ;부적합판정코드; 부적합세부내용 ; 부적합금액</param>
		public QualityInspection_Edit(string[] strArrValues)
		{
			this.strArrValues = strArrValues;
		}

		public void Edit()
		{
			SqlConnection con = new SqlConnection(base.GetConnectionString);
			SqlCommand cmd = new SqlCommand("QualityInspection_Edit");
			cmd.CommandType = CommandType.StoredProcedure;
			SqlTransaction tran = con.BeginTransaction();
			cmd.Transaction = tran;
			

			cmd.Parameters.Add("@Q_idx", SqlDbType.Int).Value = Convert.ToInt32(strArrValues[0]);
			cmd.Parameters.Add("@S_quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(strArrValues[1]);
			cmd.Parameters.Add("@U_quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(strArrValues[2]);
			cmd.Parameters.Add("@UnSuitabilityCause", SqlDbType.VarChar, 30).Value = strArrValues[3];
			cmd.Parameters.Add("@UnSuitabilityCauseCode", SqlDbType.VarChar, 15).Value = strArrValues[4];
			cmd.Parameters.Add("@UnSuitabilityStatus", SqlDbType.VarChar, 30).Value = strArrValues[5];
			cmd.Parameters.Add("@UnSuitabilityStatusCode", SqlDbType.VarChar, 15).Value = strArrValues[6];
			cmd.Parameters.Add("@InspectionDecision", SqlDbType.VarChar, 30).Value = strArrValues[7];
			cmd.Parameters.Add("@InspectionDecisionCode", SqlDbType.VarChar, 15).Value = strArrValues[8];
			cmd.Parameters.Add("@UnSuitabilityDetailMeaning", SqlDbType.VarChar, 100).Value = strArrValues[9];
			cmd.Parameters.Add("@UnSuitabilityCost", SqlDbType.Decimal).Value = Convert.ToDecimal(strArrValues[10]);
			con.Open();

			try
			{
				//cmd.ExecuteNonQuery();
				//tran.Commit();
			}
			catch(Exception e)
			{
				Alert(e.Message);
			}
			finally
			{
				con.Close();
			}

		}


	}
}
