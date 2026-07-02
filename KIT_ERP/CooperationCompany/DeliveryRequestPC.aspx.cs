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
using System.Configuration;


namespace KIT_ERP.CooperationCompany
{
	/// <summary>
	/// DeliveryRequest에 대한 요약 설명입니다.
	/// </summary>
	public class DeliveryRequest : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.LinkButton linkUpdate;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hddivision;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRowIndex;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgDR_PC;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				//Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 협력사정보 > 납품의뢰수정';</script>");
					
				//저장프로시져를 이용해 로그인 사용자 테이블에서 로그인 아이디가 같고 사업자등록번호가 같은 회사를 Select 해서 그리드에 뿌려준다.
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				SqlDataAdapter da = new SqlDataAdapter("procDeliveryRequestPC 'ts'",conn);
				DataSet ds = new DataSet();
				da.Fill(ds);
				this.uwgDR_PC.DataSource = ds.Tables[0];
				this.uwgDR_PC.DataBind();			
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
			this.linkUpdate.Click += new System.EventHandler(this.linkUpdate_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//RowTemplate에서 수정을 하고 수정 버튼을 누르고 나면 LinkButton 이 동작하는 행동을 한다.
		//품목이 구매품인지..외주품인지 판단하고 각각의 납품의뢰원장을 수정한다.
		private void linkUpdate_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlTransaction tr = conn.BeginTransaction();
			
			try
			{		
				//구매납품의뢰원장 수정
				if(hddivision.Value.ToString() == "1")
				{
					BDR_HT_Update(conn,tr);//구매납품의뢰원장을 수정하는 함수 호출
				}
				else
				{
					ODR_HT_Update(conn,tr);//외주납품의뢰원장을 수정하는 함수
				}
			
				tr.Commit();
				Response.Write("<script language=javascript>");
				Response.Write("alert('수정되었습니다!');");
				Response.Write("</script>");
			}
			catch(Exception ee)
			{
				
				Response.Write("<script language=javascript>");
				Response.Write("alert('"+ee.Message+"');");
				Response.Write("</script>");

				tr.Rollback();
			}
			finally
			{
				conn.Close();
			}
		}

		//구매납품의뢰원장 수정함수
		private void BDR_HT_Update(SqlConnection con, SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
		
			//기존 품질검사원장에서 납품수량을 증가
			string str1 =" UPDATE BDR_HT SET DeliveryRequestQuantity = @quantity , TotalCost = @cost WHERE BuyingDeliveryRequestHistoryIndex= @INDEX";
			SqlCommand comm1 = new SqlCommand(str1,con);
			comm1.Transaction = trans;					
			comm1.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgDR_PC.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity").Value.ToString());
			comm1.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgDR_PC.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()) * decimal.Parse(uwgDR_PC.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity").Value.ToString());
			comm1.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgDR_PC.Rows[rowcount].Cells.FromKey("BuyingDeliveryRequestHistoryIndex").Value.ToString());
			comm1.ExecuteNonQuery();
		}

		//외주납품의뢰원장 수정함수
		private void ODR_HT_Update(SqlConnection con, SqlTransaction trans)
		{
			int rowcount = int.Parse(hdRowIndex.Value);
			//기존 품질검사원장에서 납품수량을 증가
			string str1 =" UPDATE ODR_HT SET DeliveryRequestQuantity = @quantity , TotalCost = @cost WHERE OutSideDeliveryRequestHistoryIndex= @INDEX";
			SqlCommand comm1 = new SqlCommand(str1,con);
			comm1.Transaction = trans;		
			comm1.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(uwgDR_PC.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity").Value.ToString());
			comm1.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(uwgDR_PC.Rows[rowcount].Cells.FromKey("ApplyUnitCost").Value.ToString()) * decimal.Parse(uwgDR_PC.Rows[rowcount].Cells.FromKey("DeliveryRequestQuantity").Value.ToString());
			comm1.Parameters.Add("@INDEX",SqlDbType.Int).Value = int.Parse(uwgDR_PC.Rows[rowcount].Cells.FromKey("OutSideDeliveryRequestHistoryIndex").Value.ToString());
			comm1.ExecuteNonQuery();
		}
	}
}
