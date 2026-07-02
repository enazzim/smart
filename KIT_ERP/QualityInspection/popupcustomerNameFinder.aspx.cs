using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

namespace KIT_ERP.QualityInspection
{
	/// <summary>
	/// popupcustomerNameFinder에 대한 요약 설명입니다.
	/// </summary>
	public class popupcustomerNameFinder :  KIT_ERP.QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Literal Literal1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 회사명, 대표자명, 사업자등록번호 가져오기

			StringBuilder sb = new StringBuilder("select CompanyName, PresidentName, BusinessCompanyNum from CI_MT ", 50);

			if(Request.QueryString["customerName"] != "")	// 입력된 거래처명이 들어가는 모든 항목
			{
				string companyName = Request.QueryString["customerName"].ToString();
				sb.Append(" where CI_CompanyName like '%" );
				sb.Append(companyName);
				sb.Append("%' ");

				SqlDataAdapter adap = new SqlDataAdapter(sb.ToString() , GetConnectionString);
				DataSet ds = new DataSet();
				adap.Fill(ds);

				string recordCount = Convert.ToString(ds.Tables[0].Rows.Count);
				Literal1.Text = "\" <b>" + companyName + "</b> \" 로 검색한 결과 : " +  recordCount + "건의 데이터가 검색 되었음";

				DataGrid1.DataSource = ds.Tables[0].DefaultView;
				// 사업자 등록번호를 키 필드 로 지정
				DataGrid1.DataKeyField = "BusinessCompanyNum";
				DataGrid1.DataBind();
				
			}
			else	// * 데이터
			{
				SqlDataAdapter adap = new SqlDataAdapter(sb.ToString(), GetConnectionString);
				DataSet ds = new DataSet();
				adap.Fill(ds);
				
				// 레코드 갯수 가져옴
				string recordCount = Convert.ToString(ds.Tables[0].Rows.Count);
				Literal1.Text = "<b>" + recordCount + "</b> 개의 데이터가 존재";

				DataGrid1.DataSource = ds.Tables[0].DefaultView;
				// 사업자 등록번호를 키 필드 로 지정
				DataGrid1.DataKeyField = "BusinessCompanyNum";
				DataGrid1.DataBind();
				
			}

			
		}

		#region Web Form 디자이너에서 생성한 코드
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 이 호출은 ASP.NET Web Form 디자이너에 필요합니다.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
