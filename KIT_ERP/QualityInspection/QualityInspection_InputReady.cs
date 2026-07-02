using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using KIT_ERP.QualityInspection;
using Infragistics.WebUI.WebCombo;
using KIT_ERP.BuyingOutside;

namespace KIT_ERP.QualityInspection
{
	public class QualityInspection_InputReady : QualityInspection_BaseClass
	{
		

		private PageNames PageNames;
		private object obj;

		/// <summary>
		/// 입력 그룹의 텍스트박스 에 바인딩
		/// </summary>
		/// <param name="PageNames">페이지구분</param>
		/// <param name="key">그리드의 인덱스 키 값( 실제 primary key )</param>
		/// <param name="obj">본 클래스를 생성한 객체(인스턴스)</param>
		public QualityInspection_InputReady(PageNames PageNames, object obj)
		{
			this.PageNames = PageNames;
			this.obj = obj;
			
			// 품질검사 등록에서 생성 하였다면.
			if(PageNames == PageNames.품질검사등록)
			{
				// 부적합현상 , 부적합원인, 검사판정 드롭다운 리스트에 바인딩
				Get_QualityInspectionRegistration_Load();
			}
			
		}

		
		public QualityInspection_InputReady()
		{}



		public DataSet DDL_DataSource()
		{

			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource" , base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			adap.SelectCommand.Parameters.Add("@IncongruityCause", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@IncongruityPhenomenon", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@IncongruityDecision", SqlDbType.Bit).Value = 1;
			DataSet ds  = new DataSet();
			adap.Fill(ds);

			return ds;
		}


		// 드롭다운 리스트에 각 항목들 바인딩
		private void Get_QualityInspectionRegistration_Load()
		{
			
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource" , base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			adap.SelectCommand.Parameters.Add("@ItemNum", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@IncongruityCause", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@IncongruityPhenomenon", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@IncongruityDecision", SqlDbType.Bit).Value = 1;

			DataSet ds = new DataSet();
			
			adap.Fill(ds);


			WebCombo wcItemNum = (WebCombo)((KIT_ERP.BuyingOutside.QualityInspectionRegister)obj).FindControl("wcItemNum");	
			wcItemNum.DataSource = ds.Tables[0].DefaultView;
			wcItemNum.DataBind();

			
			WebCombo wcCustomerName = (WebCombo)((KIT_ERP.BuyingOutside.QualityInspectionRegister)obj).FindControl("wcCustomerName");	
			wcCustomerName.DataSource = ds.Tables[1].DefaultView;
			wcCustomerName.DataBind();

	
			DropDownList ddlIncongruityCause = (DropDownList)((KIT_ERP.BuyingOutside.QualityInspectionRegister)obj).FindControl("ddlIncongruityCause");				
			ListItem li = ddlIncongruityCause.Items[0];

			ddlIncongruityCause.DataSource=ds.Tables[2].DefaultView;
			ddlIncongruityCause.DataTextField="SmallClassificationName";		// 소분류명
			ddlIncongruityCause.DataValueField="SmallClassificationCode";	// 소분류코드
			ddlIncongruityCause.DataBind();
			ddlIncongruityCause.Items.Insert(0,li);

			

			DropDownList ddlIncongruityPhenomenon = (DropDownList)((QualityInspectionRegister)obj).FindControl("ddlIncongruityPhenomenon");				
			li = ddlIncongruityPhenomenon.Items[0];

			ddlIncongruityPhenomenon.DataSource=ds.Tables[3].DefaultView;
			ddlIncongruityPhenomenon.DataTextField="SmallClassificationName";
			ddlIncongruityPhenomenon.DataValueField="SmallClassificationCode";
			ddlIncongruityPhenomenon.DataBind();
			ddlIncongruityPhenomenon.Items.Insert(0,li);

			

			DropDownList ddlIncongruityDecision = (DropDownList)((QualityInspectionRegister)obj).FindControl("ddlIncongruityDecision");				
			li = ddlIncongruityDecision.Items[0];

			ddlIncongruityDecision.DataSource=ds.Tables[4].DefaultView;
			ddlIncongruityDecision.DataTextField="SmallClassificationName";
			ddlIncongruityDecision.DataValueField="SmallClassificationCode";
			ddlIncongruityDecision.DataBind();
			ddlIncongruityDecision.Items.Insert(0,li);

			
				WebCombo ddlInspectionPost = (WebCombo)((QualityInspectionRegister)obj).FindControl("ddlInspectionPost");				
				//li = ddlInspectionPost.Items[0];

				ddlInspectionPost.DataSource=ds.Tables[5].DefaultView;
				ddlInspectionPost.DataTextField="Name";
				//ddlInspectionPost.DataValueField="UserInfoIndex";
			
				ddlInspectionPost.DataBind();
				//ddlInspectionPost.Items.Insert(0,li);

		}





	}
}
