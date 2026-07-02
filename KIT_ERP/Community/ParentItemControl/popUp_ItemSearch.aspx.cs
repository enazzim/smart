using System;
using System.Configuration;
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

namespace KIT_ERP.Community.ParentItemControl
{
	/// <summary>
	/// popUp_ItemSearch에 대한 요약 설명입니다.
	/// </summary>
	public class popUp_ItemSearch : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			Bind();
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
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			Bind();
		}

		private void Bind()
		{
			try
			{
				string strConnectionString = ConfigurationSettings.AppSettings["DSN"];
				string strTag = Request["tag"].ToString();			// 검색 항목
				string strValue = Request["value"].ToString();		// 검색값
				string[] arrStrValue = new String[5];
				string strBusinessRegistrationNum = Request["businessregistrationnum"];	// 거래처명
				string strUnitCostDistinction = Request["unitcostdistinction"];
				arrStrValue = Request["propertyClassification"].ToString().Split(';');;		// 검색값
				
				
				SqlDataAdapter adap = new SqlDataAdapter("ItemSearch_Popup", strConnectionString);
				adap.SelectCommand.CommandType = CommandType.StoredProcedure;
				adap.SelectCommand.Parameters.Add("@Tag", SqlDbType.VarChar, 30).Value = strTag;
				adap.SelectCommand.Parameters.Add("@Value", SqlDbType.VarChar, 50).Value = strValue;

				adap.SelectCommand.Parameters.Add("@상품", SqlDbType.Bit).Value = Convert.ToBoolean( arrStrValue[0]);
				adap.SelectCommand.Parameters.Add("@제품", SqlDbType.Bit).Value = Convert.ToBoolean( arrStrValue[1]);
				adap.SelectCommand.Parameters.Add("@팬텀", SqlDbType.Bit).Value = Convert.ToBoolean( arrStrValue[2]);
				adap.SelectCommand.Parameters.Add("@반제품", SqlDbType.Bit).Value = Convert.ToBoolean( arrStrValue[3]);
				adap.SelectCommand.Parameters.Add("@원자재", SqlDbType.Bit).Value = Convert.ToBoolean( arrStrValue[4]);
				
				if ( strBusinessRegistrationNum != "" )
					adap.SelectCommand.Parameters.Add("@BusinessRegistrationNum", SqlDbType.VarChar, 50 ).Value = strBusinessRegistrationNum;
				if ( strUnitCostDistinction != "" )
					adap.SelectCommand.Parameters.Add("@UnitCostDistinction", SqlDbType.VarChar, 20 ).Value = strUnitCostDistinction;

				DataSet ds = new DataSet();

				adap.Fill(ds);
            
				UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
				UltraWebGrid1.DataBind();
			}
			catch( Exception Ex )
			{
				Response.Write("<script>alert(\"" + Ex.Message + "\"); window.close();</script>");
			}
		}
	}
}
