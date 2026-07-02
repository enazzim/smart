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
using System.IO;
using System.Data.OleDb;
using Microsoft.Win32;

namespace KIT_ERP.ManagementInfomation
{
	/// <summary>
	/// ExecutionPlanInfo에 대한 요약 설명입니다.
	/// </summary>
	public class ExecutionPlanInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button bt_Register;
		protected System.Web.UI.WebControls.HyperLink HyperLink1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputFile Find;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
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
			this.bt_Register.Click += new System.EventHandler(this.bt_Register_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			UltraWebGrid1.DataSource = Bind();
			UltraWebGrid1.DataBind();
		}

		/// <summary>
		/// 등록버튼 클릭시
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Register_Click(object sender, System.EventArgs e)
		{
			if(Find.Value != "")
			{
				string FileName = System.IO.Path.GetDirectoryName(Find.PostedFile.FileName) + System.IO.Path.GetFileName(Find.PostedFile.FileName);
				
				if(FileName.ToString().Substring(FileName.ToString().Length - 3) == "xls")
				{
					string upLoadFile = Path.GetTempFileName();
					Find.PostedFile.SaveAs(upLoadFile);

					OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + upLoadFile + ";Extended Properties=Excel 8.0;");
			
					conn.Open();
			
					OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", conn);
						
					DataSet dsExcel = new DataSet();
					adapter.Fill(dsExcel);
					conn.Close();
			

					
					SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
					con.Open();

					SqlCommand com = new SqlCommand();
					SqlTransaction tr = con.BeginTransaction();
					com.Connection = con;
					com.Transaction = tr;
					try
					{
						
            
						string str = "";
						for(int i = 0; i < dsExcel.Tables[0].Rows.Count; i++)
						{
							decimal UnitCost = SaleCost(dsExcel.Tables[0].Rows[i]["품목번호"].ToString(),con,tr);

							str = @"Insert into EPI_MT (ItemNum, PlanDate, PlanQuantity, SaleUnitCost,
										PlanTotalCost, State, RecodingState, RegistrationPerson, RegistrationPersonID, RegistrationDate)
										values(@ItemNum, @PlanDate, @PlanQuantity, @SaleUnitCost,
										@PlanTotalCost, 0, 1, @RegistrationPerson, @RegistrationPersonID, @RegistrationDate)";
							com.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value = dsExcel.Tables[0].Rows[i]["품목번호"].ToString();
							if(dsExcel.Tables[0].Rows[i]["날짜"].ToString().Trim().Length == 8)
							{
								//수주일자...
								string year = dsExcel.Tables[0].Rows[i]["날짜"].ToString().Trim().Substring(0,4).ToString();
								string month = dsExcel.Tables[0].Rows[i]["날짜"].ToString().Trim().Substring(4,2).ToString();
								string day = dsExcel.Tables[0].Rows[i]["날짜"].ToString().Trim().Substring(6,2).ToString();
								string Date = year+"-"+month+"-"+day;
								com.Parameters.Add("@PlanDate",SqlDbType.SmallDateTime).Value = Convert.ToDateTime(Date);
							}
							else
							{
								com.Parameters.Add("@PlanDate",SqlDbType.SmallDateTime).Value = Convert.ToDateTime(dsExcel.Tables[0].Rows[i]["날짜"].ToString());
							}
							com.Parameters.Add("@PlanQuantity",SqlDbType.Decimal).Value = decimal.Parse(dsExcel.Tables[0].Rows[i]["계획수량"].ToString());
							com.Parameters.Add("@SaleUnitCost",SqlDbType.Decimal).Value = UnitCost;
							com.Parameters.Add("@PlanTotalCost",SqlDbType.Decimal).Value = UnitCost * decimal.Parse(dsExcel.Tables[0].Rows[i]["계획수량"].ToString());
							com.Parameters.Add("@RegistrationPerson",SqlDbType.VarChar).Value = Session["UserName"].ToString();
							com.Parameters.Add("@RegistrationPersonID",SqlDbType.VarChar).Value = Session["ID"].ToString();
							com.Parameters.Add("@RegistrationDate",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
							com.CommandText = str;
							com.ExecuteNonQuery();
							com.Parameters.Clear();
						}
						
						dsExcel.Clear();

						
							

//						str = @"Select EPI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, EPI_MT.PlanDate, EPI_MT.PlanQuantity, EPI_MT.SaleUnitCost,
//						EPI_MT.PlanTotalCost, case EPI_MT.State when 1 then '예' else '아니오' end as State,  EPI_MT.RecodingState, EPI_MT.RegistrationPerson, 
//						EPI_MT.RegistrationPersonID, EPI_MT.RegistrationDate, EPI_MT.UpdatingPerson, EPI_MT.UpdatingPersonID, EPI_MT.UpdatingDate 
//						FROM EPI_MT INNER JOIN II_MT ON EPI_MT.ItemNum = II_MT.ItemNum 
//						Where EPI_MT.RecodingState = 1 and II_MT.RecodingState = 1 and EPI_MT.RegistrationDate = @date order by PlanDate";//, EPI_MT.ItemNum";
//						com.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
//						com.CommandText = str;
//						SqlDataAdapter da = new SqlDataAdapter(com);
//						DataSet ds = new DataSet();
//						da.Fill(ds);
//						com.Parameters.Clear();
						UltraWebGrid1.DataSource = Bind(con,tr);							
						UltraWebGrid1.DataBind();
						
						tr.Commit();

						Response.Write("<script language=javascript>");
						Response.Write("alert('엑셀자료가 업로드 되었습니다.');");
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
						File.Delete(upLoadFile);
						con.Close();
					}
					
				}
				else
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('엑셀자료가 아닙니다.');");
					Response.Write("</script>");
				}
				
			}
			else
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('입력할 엑셀자료가 없습니다.');");
				Response.Write("</script>");
			}
		}


		private decimal SaleCost(string ItemNum,SqlConnection conn, SqlTransaction trans)
		{
			decimal Cost = 0;
			string str = @"Select StandardUnitCost From UCI_MT where ItemNum = @ItemNum  and RecodingState = 1 and UnitCostDistinction = '판매단가' and OrderRate = (select Max(OrderRate) From UCI_MT where ItemNum = @ItemNum and UnitCostDistinction = '판매단가')";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = trans;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Cost = decimal.Parse(dr["StandardUnitCost"].ToString());
			}
			dr.Close();

			return Cost;
		}


		private DataSet Bind(SqlConnection conn, SqlTransaction trans)
		{
			string str = @"Select EPI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, EPI_MT.PlanDate, EPI_MT.PlanQuantity, EPI_MT.SaleUnitCost,
						EPI_MT.PlanTotalCost, case EPI_MT.State when 1 then '예' else '아니오' end as State,  EPI_MT.RecodingState, EPI_MT.RegistrationPerson, 
						EPI_MT.RegistrationPersonID, EPI_MT.RegistrationDate, EPI_MT.UpdatingPerson, EPI_MT.UpdatingPersonID, EPI_MT.UpdatingDate 
						From EPI_MT  Inner Join II_MT on EPI_MT.ItemNum = II_MT.ItemNum 
						Where EPI_MT.RecodingState = 1 and II_MT.RecodingState = 1 and EPI_MT.RegistrationDate = @date order by PlanDate, EPI_MT.ItemNum";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Transaction = trans;
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			
			return ds;
		}
		
		private DataSet Bind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = @"Select EPI_MT.ItemNum, II_MT.ItemDrawNum, II_MT.ItemName, EPI_MT.PlanDate, EPI_MT.PlanQuantity, EPI_MT.SaleUnitCost,
						EPI_MT.PlanTotalCost, case EPI_MT.State when 1 then '예' else '아니오' end as State,  EPI_MT.RecodingState, EPI_MT.RegistrationPerson, 
						EPI_MT.RegistrationPersonID, EPI_MT.RegistrationDate, EPI_MT.UpdatingPerson, EPI_MT.UpdatingPersonID, EPI_MT.UpdatingDate 
						From EPI_MT  Inner Join II_MT on EPI_MT.ItemNum = II_MT.ItemNum 
						Where EPI_MT.RecodingState = 1 and II_MT.RecodingState = 1 and EPI_MT.RegistrationDate = @date order by PlanDate, EPI_MT.ItemNum";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@date",SqlDbType.SmallDateTime).Value = DateTime.Now.ToShortDateString();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			conn.Close();

			return ds;
		}
	}
}
