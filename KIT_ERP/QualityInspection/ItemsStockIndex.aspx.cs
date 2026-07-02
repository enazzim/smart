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

namespace KIT_ERP.ManagementInfomation
{
	public class ItemsStockIndex :  KIT_ERP.QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button tb_Search;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;


		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.Commodity = true;
			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.RawMaterials = true;

			if ( !IsPostBack )
			{				
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
			this.tb_Search.Click += new System.EventHandler(this.tb_Search_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	
		private void tb_Search_Click(object sender, System.EventArgs e)
		{
			if (Validate(ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum, ItemSearchControl1.ItemName))
			{
				string strItemNum = ItemSearchControl1.ItemNum;
				SqlDataAdapter adap = new SqlDataAdapter("ItemStock_Search", base.GetConnectionString);
				adap.SelectCommand.Parameters.Add("@itemnum", SqlDbType.VarChar, 150).Value = strItemNum;
				adap.SelectCommand.CommandType = CommandType.StoredProcedure;
				adap.SelectCommand.CommandTimeout = 0;
			
				DataSet ds = new DataSet();
				adap.Fill(ds);

				if ( ds.Tables[0].Rows.Count > 0 )
				{
					for(int count = 0; count < ds.Tables[0].Rows.Count ;count ++)
					{
						if(Division(ds.Tables[0].Rows[count]["ItemNum"].ToString(),int.Parse(ds.Tables[0].Rows[count]["공정순서"].ToString())))
						{
							ds.Tables[0].Rows[count]["영업창고"] = BSQauntity(ds.Tables[0].Rows[count]["ItemNum"].ToString());
							ds.Tables[0].Rows[count]["납품창고"] = DSQauntity(ds.Tables[0].Rows[count]["ItemNum"].ToString());
							ds.Tables[0].Rows[count]["금액"] = Cost(ds.Tables[0].Rows[count]["ItemNum"].ToString());
						}
					}
					UltraWebGrid1.DataSource = ds.Tables[0].DefaultView;
					UltraWebGrid1.DataBind();
				}
				else
				{
					Alert("검색된 결과가 없습니다.");
					UltraWebGrid1.DataBind();
				}
			}
		}

		private bool Division(string item, int seq)
		{
			string aa=""; 
			SqlConnection conn = new SqlConnection(base.GetConnectionString);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @item";
			SqlCommand comm = new SqlCommand();
			comm.Parameters.Add("@item",item.Trim());
			comm.CommandText = str;
			comm.Connection = conn;

			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			dr.Close();
	
						
			str = "select Count(*) From PSI_MT Where ItemNum = @item and ProcessSequenceNum > @seq and RecodingState = 1";
			comm.Parameters.Add("@seq",seq);
			comm.CommandText = str;
			
			int a = int.Parse(comm.ExecuteScalar().ToString());
	

			conn.Close();

			if(aa.ToString() =="제품" && a == 0)
				return true;
			else
				return false;
			

		}


		/// <summary>
		/// 영업창고 수량
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private decimal BSQauntity(string ItemNum)
		{
			decimal Quantity = 0;
			SqlConnection conn = new SqlConnection(base.GetConnectionString);
			conn.Open();
			string str = "Select ItemNum, Sum(StockQuantity12) as Quantity From BS_MT where ItemNum = @ItemNum and RecodingState = 1 and [Year] = @year Group by ItemNum";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum.Trim());
			comm.Parameters.Add("@year",DateTime.Now.Year);
            SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["Quantity"].ToString());
			}
			conn.Close();
			return Quantity;
		}


		/// <summary>
		/// 납품창고수량
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private decimal DSQauntity(string ItemNum)
		{
			decimal Quantity = 0;
			SqlConnection conn = new SqlConnection(base.GetConnectionString);
			conn.Open();
			string str = "Select ItemNum, Sum(StockQuantity12) as Quantity From DS_MT where ItemNum = @ItemNum and RecodingState = 1 and [Year] = @year Group by ItemNum";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",ItemNum.Trim());
			comm.Parameters.Add("@year",DateTime.Now.Year);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Quantity = decimal.Parse(dr["Quantity"].ToString());
			}
			conn.Close();
			return Quantity;
		}


		/// <summary>
		/// 금액
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private decimal Cost(string ItemNum)
		{
			decimal Cost = 0;
			SqlConnection conn = new SqlConnection(base.GetConnectionString);
			conn.Open();
			string str = @"Select ItemNum, Sum(StockCost12) as PSCost From BS_MT where ItemNum = @ItemNum and RecodingState = 1 and [Year] = @Year Group by ItemNum";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum.Trim());
			comm.Parameters.Add("@Year",DateTime.Now.Year);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Cost = decimal.Parse(dr["PSCost"].ToString()) ;
			}
			dr.Close();
			
			str = "Select  StockCost12 From PS_MT where ItemNum = @ItemNum and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PS_MT where ItemNum = @ItemNum and RecodingState = 1) and [Year] = @Year and RecodingState = 1";
			comm.CommandText = str;
			SqlDataReader dr1 = comm.ExecuteReader();
			while(dr1.Read())
			{
				Cost += decimal.Parse(dr1["StockCost12"].ToString());
			}
			dr1.Close();

			str = "Select  StockCost12 From DS_MT where ItemNum = @ItemNum and RecodingState = 1 and [Year] = @Year";
			comm.CommandText = str;
			SqlDataReader dr2 = comm.ExecuteReader();
			while(dr2.Read())
			{
				Cost += decimal.Parse(dr2["StockCost12"].ToString());
			}
			dr2.Close();
			conn.Close();

			return Cost;
		}

		private bool Validate(string item, string draw, string name)
		{
			SqlConnection conn = new SqlConnection(base.GetConnectionString);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			SqlTransaction tr = conn.BeginTransaction();
			comm.Connection = conn;
			comm.Transaction = tr;
			int a = 0;

			try
			{
				string str = @"Select ItemNum From II_MT Where RecodingState = 1 and ItemNum = @num";
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
				comm.CommandText = str;
				SqlDataAdapter da = new SqlDataAdapter(comm);
				DataSet ds = new DataSet();
				da.Fill(ds);

				if(ds.Tables[0].Rows.Count == 0 )
					throw new Exception("품목번호를 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemDrawNum From II_MT Where RecodingState = 1 and ItemDrawNum = @draw";
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = draw;
				comm.CommandText = str;
				SqlDataAdapter da1 = new SqlDataAdapter(comm);
				DataSet ds1 = new DataSet();
				da1.Fill(ds1);

				if(ds1.Tables[0].Rows.Count == 0 )
					throw new Exception("도면번호를 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemName From II_MT Where RecodingState = 1 and ItemName = @name";
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = name;
				comm.CommandText = str;
				SqlDataAdapter da2 = new SqlDataAdapter(comm);
				DataSet ds2 = new DataSet();
				da2.Fill(ds2);

				if(ds2.Tables[0].Rows.Count == 0 )
					throw new Exception("품목명을 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemName From II_MT Where RecodingState = 1 and ItemNum = @num and ItemDrawNum = @draw and ItemName = @name";
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = draw;
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = name;

				comm.CommandText = str;
				SqlDataAdapter da3 = new SqlDataAdapter(comm);
				DataSet ds3 = new DataSet();
				da3.Fill(ds3);

				if(ds3.Tables[0].Rows.Count == 0 )
					throw new Exception("품목번호, 도면번호, 품목명을 다시 확인하세요.");

				tr.Commit();
			}
			catch(Exception ee)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('"+ee.Message+"');");
				Response.Write("</script>");
				tr.Rollback();
				a = 1;
			}
			finally
			{
				conn.Close();
			}
			if(a == 0)
				return true;
			else
				return false;
		}


		private void Button2_Click(object sender, System.EventArgs e)
		{
			if (UltraWebGrid1.Rows.Count > 0)
				UltraWebGridExcelExporter1.Export(UltraWebGrid1);
			else
				Alert("Excel 로 출력될 검색결과가 없습니다.");
		}







	}
}
