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
using System.IO;
using System.Data.OleDb;
using System.Text;

namespace KIT_ERP.SystemInfoManagement
{
	/// <summary>
	/// InitialStockInput에 대한 요약 설명입니다.
	/// </summary>
	public class InitialStockInput : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgStock;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DropDownList DropDownList1;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.HtmlControls.HtmlInputFile File1;
		protected System.Web.UI.WebControls.DropDownList Dropdownlist2;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		private string m_strStockDiv;

		private void Page_Load(object sender, System.EventArgs e)
		{

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
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.uwgStock.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.uwgStock_PageIndexChanged);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void uwgStock_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			uwgStock.Columns.Clear();
			uwgStock.Rows.Clear();
			uwgStock.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			this.Button3_Click(sender, e);
		}

		// 엑셀파일 다운로드.,
		private void Button2_Click(object sender, System.EventArgs e)
		{
			if ( uwgStock.Rows.Count > 0 )
			{
				uwgStock.DisplayLayout.Pager.AllowPaging = false;
				this.Button3_Click(sender, e);
				UltraWebGridExcelExporter1.Export(uwgStock);
				uwgStock.DisplayLayout.Pager.AllowPaging = true;
			}
			else
				Response.Write("<script>alert('Excel 파일로 다운받을 자료가 없습니다.')</script>");
		}

		// 적용버튼 클릭 이벤트 핸들러
		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(File1.Value != "")
			{
				m_strStockDiv = Dropdownlist2.SelectedItem.Text;
				string FileName = System.IO.Path.GetFileName(File1.PostedFile.FileName);
				string strFileDiv = FileName.ToString().Substring(FileName.LastIndexOf("."));

				if(strFileDiv != ".xls" && strFileDiv != ".XLS")
				{
					Response.Write("<script language=javascript>");
					Response.Write("alert('엑셀 양식의 파일이 아닙니다.');");
					Response.Write("</script>");
				}
				else
				{
					string upLoadFile = Path.GetTempFileName();
					// Temp 파일로 Excel 파일저장 
					File1.PostedFile.SaveAs(upLoadFile);

					// Excel 자료를 DataSet 으로 받아낸다.
					OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + upLoadFile + "';Extended Properties=Excel 8.0;");
					con.Open();
					OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [Stock$]", con);
					DataSet dsExcel = new DataSet();
					adapter.Fill(dsExcel);
					con.Close();

					this.DB_Regist(dsExcel);

					Button3_Click(this,null);
				}
			}
			else
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('입력할 엑셀자료가 없습니다.');");
				Response.Write("</script>");
			}
		}

		// 현재창고보기 버튼 클릭시.
		private void Button3_Click(object sender, System.EventArgs e)
		{
			uwgStock.Columns.Clear();
			uwgStock.Rows.Clear();
			string strQuery = "";
			switch ( DropDownList1.SelectedItem.Text )
			{
				case "영업1창고":
					strQuery = @"select 
								II_MT.ItemNum as [품목번호], ItemDrawNum as [도면번호], ItemName as [품목명]
								, puc_mt.SmallClassificationName as [자산분류]
								,StockQuantity12 as [수량], BusinessStorehouseIndex
								from bs_mt join II_MT on bs_MT.ItemNum = II_MT.ItemNum
								join puc_mt on puc_mt.smallclassificationCode = bs_mt.ProcessCode
								where (puc_mt.recodingstate = 1 and bs_mt.recodingstate = 1 and II_MT.RecodingState = 1) and BusinessStorehouseNum = 1 and [Year] = "+DateTime.Now.Year+"";
					break;
				case "영업2창고":
					strQuery = @"select 
								II_MT.ItemNum as [품목번호], ItemDrawNum as [도면번호], ItemName as [품목명]
								, puc_mt.SmallClassificationName as [자산분류]
								,StockQuantity12 as [수량], BusinessStorehouseIndex
								from bs_mt join II_MT on bs_MT.ItemNum = II_MT.ItemNum
								join puc_mt on puc_mt.smallclassificationCode = bs_mt.ProcessCode
								where (puc_mt.recodingstate = 1 and bs_mt.recodingstate = 1 and II_MT.RecodingState = 1) and BusinessStorehouseNum = 2 and [Year] = "+DateTime.Now.Year+"";
					break;
				case "영업3창고":
					strQuery = @"select 
								II_MT.ItemNum as [품목번호], ItemDrawNum as [도면번호], ItemName as [품목명]
								, puc_mt.SmallClassificationName as [자산분류]
								,StockQuantity12 as [수량], BusinessStorehouseIndex
								from bs_mt join II_MT on bs_MT.ItemNum = II_MT.ItemNum
								join puc_mt on puc_mt.smallclassificationCode = bs_mt.ProcessCode
								where (puc_mt.recodingstate = 1 and bs_mt.recodingstate = 1 and II_MT.RecodingState = 1) and BusinessStorehouseNum = 3 and [Year] = "+DateTime.Now.Year+"";
					break;
				case "생산창고":
					strQuery = @"select 
								II_MT.ItemNum as [품목번호], ItemDrawNum as [도면번호], ItemName as [품목명], ProcessSequenceNum as [공정순서번호]
								, puc_mt.SmallClassificationName as [공정명]
								, StockQuantity12 as [수량], ProductionStorehouseIndex
								from ps_mt join II_MT on ps_MT.ItemNum = II_MT.ItemNum
								join puc_mt on puc_mt.smallclassificationCode = ps_mt.ProcessCode
								where (puc_mt.recodingstate = 1 and ps_mt.recodingstate = 1 and II_MT.RecodingState = 1 and [Year] = "+DateTime.Now.Year+")";
					break;
				case "원자재창고":
					strQuery = @"select 
								II_MT.ItemNum as [품목번호], ItemDrawNum as [도면번호], ItemName as [품목명], puc_mt.SmallClassificationName as [자산분류],
								StockQuantity12 as [수량], RowMetarialStorehouseIndex
								from rms_mt join II_MT on rms_MT.ItemNum = II_MT.ItemNum
								join puc_mt on puc_mt.smallclassificationCode = rms_mt.ProcessCode
								where (puc_mt.recodingstate = 1 and rms_mt.recodingstate = 1 and II_MT.RecodingState = 1 and [Year] = "+DateTime.Now.Year+")";
					break;
				case "외주창고":
					strQuery = @"select 
								II_MT.ItemNum as [품목번호], ItemDrawNum as [도면번호], ItemName as [품목명]
								, ci_mt.CompanyName as [거래처명], os_mt.Businessregistrationnum as [사업자등록번호], ProcessSequenceNum as [공정순서번호], puc_mt.SmallClassificationName as [품목분류],
								StockQuantity12 as [수량], OutSideStorehouseIndex
								from os_mt join II_MT on os_MT.ItemNum = II_MT.ItemNum
								join puc_mt on puc_mt.smallclassificationCode = os_mt.ProcessCode
								join ci_mt on ci_mt.Businessregistrationnum = os_mt.Businessregistrationnum
								where (puc_mt.recodingstate = 1 and os_mt.recodingstate = 1 and ci_mt.recodingstate = 1 and II_MT.RecodingState = 1 and [Year] = "+DateTime.Now.Year+")";
					break;
				case "납품창고":
					strQuery = @"select 
								II_MT.ItemNum as [품목번호], ItemDrawNum as [도면번호], ItemName as [품목명]
								, puc_mt.SmallClassificationName as [자산분류],
								StockQuantity12 as [수량], DeliveryStorehouseIndex
								from ds_mt join II_MT on ds_MT.ItemNum = II_MT.ItemNum
								join puc_mt on puc_mt.smallclassificationCode = ds_mt.ProcessCode
								where (puc_mt.recodingstate = 1 and ds_mt.recodingstate = 1 and II_MT.RecodingState = 1 and [Year] = "+DateTime.Now.Year+")";
					break;
				case "매입매출":
					strQuery = @" select companyname as [거래처명],bsi_mt.businessregistrationnum as [사업자등록번호], totalsalecost12 as 매출금액, CollectMoney12 as 수금액,UncollectMoney12 as 미수금액, totalbuyingcost12 as 매입금액, PaymentMoney12 as 지급금액,UnPaymentMoney12 as 미지급금액, case saledistinction when 1 then '예' else '아니오'  end as 판매거래처
								,  case buyingdistinction when 1 then '예' else '아니오' end as 구매거래처 
								from bsi_mt join ci_mt on ci_mt.businessregistrationnum = bsi_mt.businessregistrationnum
								where bsi_mt.recodingstate = 1 and ci_mt.recodingstate = 1 and [Year] = "+DateTime.Now.Year+"";
					break;
				case "보용품창고":
					strQuery = @"select  II_MT.ItemNum as [품목번호], ItemDrawNum as [도면번호], ItemName as [품목명]
								, PropertyClassification as [자산분류],
								StockQuantity12 as [수량], AddItemStorehouseIndex
								from As_mt join II_MT on As_MT.ItemNum = II_MT.ItemNum
								join puc_mt on puc_mt.smallclassificationCode = As_mt.ProcessCode
								where (puc_mt.recodingstate = 1 and AS_mt.recodingstate = 1 and II_MT.RecodingState = 1 and [Year] = "+DateTime.Now.Year+")";
					break;

			}

			SqlDataAdapter adap = new SqlDataAdapter(strQuery, ConfigurationSettings.AppSettings["DSN"]);
			DataSet ds = new DataSet();
			adap.Fill(ds);
			uwgStock.DataSource = ds.Tables[0].DefaultView;
			try
			{
				uwgStock.DataBind();
			}
			catch
			{
				uwgStock.DisplayLayout.Pager.CurrentPageIndex = 1;
				uwgStock.DataBind();
			}

			uwgStock.Columns[0].Width = 200;
			if ( DropDownList1.SelectedItem.Text == "외주창고" )
			{
				uwgStock.Columns[1].Width = 150;		//도면번호
				uwgStock.Columns[2].Width = 150;		//품목명
				uwgStock.Columns[5].CellStyle.HorizontalAlign = HorizontalAlign.Right;	//공정순서
				uwgStock.Columns[5].CellStyle.Padding.Right = 5;						//공정순서

				uwgStock.Columns[uwgStock.Columns.Count - 1].Hidden = true;
				uwgStock.Columns[uwgStock.Columns.Count - 2].Format = "###,###,##0.00";
				uwgStock.Columns[uwgStock.Columns.Count - 2].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				uwgStock.Columns[uwgStock.Columns.Count - 2].CellStyle.Padding.Right = 5;

			}
			else if ( DropDownList1.SelectedItem.Text == "생산창고" )
			{
				uwgStock.Columns[1].Width = 150;		//도면번호
				uwgStock.Columns[2].Width = 150;		//품목명
				uwgStock.Columns[3].CellStyle.HorizontalAlign = HorizontalAlign.Right;	//공정순서
				uwgStock.Columns[3].CellStyle.Padding.Right = 5;	//공정순서

				uwgStock.Columns[uwgStock.Columns.Count - 1].Hidden = true;
				uwgStock.Columns[uwgStock.Columns.Count - 2].Format = "###,###,##0.00";
				uwgStock.Columns[uwgStock.Columns.Count - 2].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				uwgStock.Columns[uwgStock.Columns.Count - 2].CellStyle.Padding.Right = 5;
				uwgStock.Columns[uwgStock.Columns.Count - 2].Width = 80;

			}
			else if ( DropDownList1.SelectedItem.Text == "매입매출"  )
			{
				uwgStock.Columns[0].Width = 180;
				uwgStock.Columns[2].Format = "###,###,##0.00";
				uwgStock.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				uwgStock.Columns[2].CellStyle.Padding.Right = 5;

				uwgStock.Columns[3].Format = "###,###,##0.00";
				uwgStock.Columns[3].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				uwgStock.Columns[3].CellStyle.Padding.Right = 5;

				uwgStock.Columns[4].Format = "###,###,##0.00";
				uwgStock.Columns[4].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				uwgStock.Columns[4].CellStyle.Padding.Right = 5;

				uwgStock.Columns[5].Format = "###,###,##0.00";
				uwgStock.Columns[5].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				uwgStock.Columns[5].CellStyle.Padding.Right = 5;

				uwgStock.Columns[6].Format = "###,###,##0.00";
				uwgStock.Columns[6].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				uwgStock.Columns[6].CellStyle.Padding.Right = 5;

				uwgStock.Columns[7].Format = "###,###,##0.00";
				uwgStock.Columns[7].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				uwgStock.Columns[7].CellStyle.Padding.Right = 5;
			}
			else
			{
				uwgStock.Columns[1].Width = 150;		//도면번호
				uwgStock.Columns[2].Width = 150;		//품목명

				uwgStock.Columns[uwgStock.Columns.Count - 1].Hidden = true;
				uwgStock.Columns[uwgStock.Columns.Count - 2].Format = "###,###,##0.00";
				uwgStock.Columns[uwgStock.Columns.Count - 2].CellStyle.HorizontalAlign = HorizontalAlign.Right;
				uwgStock.Columns[uwgStock.Columns.Count - 2].CellStyle.Padding.Right = 5;
				uwgStock.Columns[uwgStock.Columns.Count - 2].Width = 80;
			}


		}


		private void DB_Regist(DataSet dsExcel)
		{

			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand cmd = new SqlCommand("", con);
			int nThisMonth = 1;
			
			
			int j = 0;
			int nCommit = 1;
			switch ( m_strStockDiv )
			{
				case "영업1창고":
					foreach( DataRow drow in dsExcel.Tables[0].Rows )
					{
						j = 0;
						StringBuilder sbQuery = new StringBuilder("", 300);

						//금액은 품목정보의 기준단가를 가져와 곱해준다.
						SqlCommand comm = new SqlCommand("Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num",con);
						comm.Parameters.Add("@num", drow["품목번호"].ToString());
						con.Open();
						decimal deStandardUnitCost = Convert.ToDecimal(comm.ExecuteScalar());
						con.Close();
						

						sbQuery.Append( " update bs_mt set ");
						sbQuery.Append( " LastYearTransferQuantity = @StockQuantity" + nThisMonth + j +" , LastYearTransferCost = @StockCost" + nThisMonth + j +", ");
						for( int i = nThisMonth ; i < 13 ; i ++ )
						{
							if(i < 12)
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j + "," );
							}
							else
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j );
							}

							cmd.Parameters.Add("@StockQuantity" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString());
							cmd.Parameters.Add("@StockCost" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString()) 
								* deStandardUnitCost;
						}

						sbQuery.Append(" where itemnum = @itemnum" + j + " and businessstorehousenum = 1 and recodingstate = 1 and [Year] = @year" );
						cmd.Parameters.Add("@itemnum" + j, SqlDbType.VarChar).Value = drow["품목번호"];
						cmd.Parameters.Add("@year",DateTime.Now.Year);
						j++;


						cmd.CommandText = sbQuery.ToString();
						SqlTransaction tran = null;

						try
						{
							con.Open();
							tran = con.BeginTransaction();
							cmd.Transaction = tran;

							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							cmd.CommandText = "Insert into SS_HT (UserName, UserID, Date, Division, Resion) values( @name, @id, @date, @division, '시스템일괄등록')";
							cmd.Parameters.Add("@name", Session["UserName"].ToString());
							cmd.Parameters.Add("@id", Session["ID"].ToString());
							cmd.Parameters.Add("@division", m_strStockDiv.ToString().Trim());
							cmd.Parameters.Add("@date",System.DateTime.Parse(DateTime.Now.ToShortDateString()));
							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							tran.Commit();
							nCommit = 0;
							
						}
						catch ( Exception ex )
						{
							Response.Write("<script language=javascript>");
							Response.Write("alert(\"" + ex.Message + "\");");
							Response.Write("</script>");
							tran.Rollback();
						}
						finally
						{
							con.Close();
						}

					}
					if(nCommit == 0)
						Response.Write("<script>alert('등록했습니다');</script>");

					break;
				case "영업2창고":
					//j = 0;

					foreach( DataRow drow in dsExcel.Tables[0].Rows )
					{
						j = 0;
						StringBuilder sbQuery = new StringBuilder("", 300);

						//금액은 품목정보의 기준단가를 가져와 곱해준다.
						SqlCommand comm = new SqlCommand("Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num",con);
						comm.Parameters.Add("@num", drow["품목번호"].ToString());
						con.Open();
						decimal deStandardUnitCost = Convert.ToDecimal(comm.ExecuteScalar());
						con.Close();
						

						sbQuery.Append( " update bs_mt set ");
						sbQuery.Append( " LastYearTransferQuantity = @StockQuantity" + nThisMonth + j +" , LastYearTransferCost = @StockCost" + nThisMonth + j +", ");
						for( int i = nThisMonth ; i < 13 ; i ++ )
						{
							if(i < 12)
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j + "," );
							}
							else
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j );
							}

							cmd.Parameters.Add("@StockQuantity" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString());
							cmd.Parameters.Add("@StockCost" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString()) 
								* deStandardUnitCost;
						}

						sbQuery.Append(" where itemnum = @itemnum" + j + " and businessstorehousenum = 2 and recodingstate = 1 and [Year] = @year" );
						cmd.Parameters.Add("@itemnum" + j, SqlDbType.VarChar).Value = drow["품목번호"];
						cmd.Parameters.Add("@year",DateTime.Now.Year);
						j++;


						cmd.CommandText = sbQuery.ToString();
						SqlTransaction tran = null;

						try
						{
							con.Open();
							tran = con.BeginTransaction();
							cmd.Transaction = tran;

							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							cmd.CommandText = "Insert into SS_HT (UserName, UserID, Date, Division, Resion) values( @name, @id, @date, @division, '시스템일괄등록')";
							cmd.Parameters.Add("@name", Session["UserName"].ToString());
							cmd.Parameters.Add("@id", Session["ID"].ToString());
							cmd.Parameters.Add("@division", m_strStockDiv.ToString().Trim());
							cmd.Parameters.Add("@date",System.DateTime.Parse(DateTime.Now.ToShortDateString()));
							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							
							tran.Commit();
							nCommit = 0;
							
						}
						catch ( Exception ex )
						{
							Response.Write("<script language=javascript>");
							Response.Write("alert(\"" + ex.Message + "\");");
							Response.Write("</script>");
							tran.Rollback();
						}
						finally
						{
							con.Close();
						}

					}
					if(nCommit == 0)
						Response.Write("<script>alert('등록했습니다');</script>");

					break;

				case "영업3창고":

					foreach( DataRow drow in dsExcel.Tables[0].Rows )
					{
						j = 0;
						StringBuilder sbQuery = new StringBuilder("", 300);

						//금액은 품목정보의 기준단가를 가져와 곱해준다.
						SqlCommand comm = new SqlCommand("Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num",con);
						comm.Parameters.Add("@num", drow["품목번호"].ToString());
						con.Open();
						decimal deStandardUnitCost = Convert.ToDecimal(comm.ExecuteScalar());
						con.Close();
						

						sbQuery.Append( " update bs_mt set ");
						sbQuery.Append( " LastYearTransferQuantity = @StockQuantity" + nThisMonth + j +" , LastYearTransferCost = @StockCost" + nThisMonth + j +", ");
						//sbQuery.Append( " LastYearTransferQuantity = @StockQuantity10, LastYearTransferCost = @StockCost10, ");
						for( int i = nThisMonth ; i < 13 ; i ++ )
						{
							if(i < 12)
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j + "," );
							}
							else
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j );
							}

							cmd.Parameters.Add("@StockQuantity" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString());
							cmd.Parameters.Add("@StockCost" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString()) 
								* deStandardUnitCost;
						}

						sbQuery.Append(" where itemnum = @itemnum" + j + " and businessstorehousenum = 3 and recodingstate = 1 and [Year] = @year" );
						cmd.Parameters.Add("@itemnum" + j, SqlDbType.VarChar).Value = drow["품목번호"];
						cmd.Parameters.Add("@year",DateTime.Now.Year);
						j++;


						cmd.CommandText = sbQuery.ToString();
						SqlTransaction tran = null;

						try
						{
							con.Open();
							tran = con.BeginTransaction();
							cmd.Transaction = tran;

							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							cmd.CommandText = "Insert into SS_HT (UserName, UserID, Date, Division, Resion) values( @name, @id, @date, @division, '시스템일괄등록')";
							cmd.Parameters.Add("@name", Session["UserName"].ToString());
							cmd.Parameters.Add("@id", Session["ID"].ToString());
							cmd.Parameters.Add("@division", m_strStockDiv.ToString().Trim());
							cmd.Parameters.Add("@date",System.DateTime.Parse(DateTime.Now.ToShortDateString()));
							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							
							tran.Commit();
							nCommit = 0;
							
						}
						catch ( Exception ex )
						{
							Response.Write("<script language=javascript>");
							Response.Write("alert(\"" + ex.Message + "\");");
							Response.Write("</script>");
							tran.Rollback();
						}
						finally
						{
							con.Close();
						}

					}
					if(nCommit == 0)
						Response.Write("<script>alert('등록했습니다');</script>");
					break;


				case "생산창고":
					foreach( DataRow drow in dsExcel.Tables[0].Rows )
					{
						j = 0;
						StringBuilder sbQuery = new StringBuilder("", 300);

						//금액은 품목정보의 기준단가를 가져와 곱해준다.
						SqlCommand comm = new SqlCommand("Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num select ProgressRate from psi_mt where RecodingState = 1 and ItemNum = @num and processsequencenum =  @processsequencenum", con);
						comm.Parameters.Add("@num", drow["품목번호"].ToString());
						comm.Parameters.Add("@processsequencenum", SqlDbType.Int).Value = drow["공정순서번호"].ToString();
						con.Open();
						SqlDataReader dr = comm.ExecuteReader();
						decimal deStandardUnitCost = 0;
						decimal deProgressRate = 0;

						if ( dr.Read() )
						{
							deStandardUnitCost = Convert.ToDecimal( dr[0] );
						}

						if ( dr.NextResult() )
						{
							if ( dr.Read() )
							{
								deProgressRate = Convert.ToDecimal( dr[0] );
							}
						}
						
						
						con.Close();
						

						sbQuery.Append( " update ps_mt set ");
						sbQuery.Append( " LastYearTransferQuantity = @StockQuantity" + nThisMonth + j +" , LastYearTransferCost = @StockCost" + nThisMonth + j +", ");
						//sbQuery.Append( " LastYearTransferQuantity = @StockQuantity10, LastYearTransferCost = @StockCost10, ");
						for( int i = nThisMonth ; i < 13 ; i ++ )
						{
							if(i < 12)
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j + "," );
							}
							else
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j );
							}

							cmd.Parameters.Add("@StockQuantity" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString());
							cmd.Parameters.Add("@StockCost" + i + j, SqlDbType.Decimal).Value = 
								(deStandardUnitCost * deProgressRate / 100) * decimal.Parse(drow["수량"].ToString()) ;
						}

						sbQuery.Append(" where itemnum = @itemnum" + j + " and processsequencenum = @processsequencenum" + j + "   and recodingstate = 1 and [Year] = @year" );
						cmd.Parameters.Add("@itemnum" + j, SqlDbType.VarChar).Value = drow["품목번호"];
						cmd.Parameters.Add("@processsequencenum" + j , SqlDbType.Int).Value = drow["공정순서번호"];
						cmd.Parameters.Add("@year",DateTime.Now.Year);
						j++;

						cmd.CommandText = sbQuery.ToString();
						SqlTransaction tran = null;

						try
						{
							con.Open();
							tran = con.BeginTransaction();
							cmd.Transaction = tran;

							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							cmd.CommandText = "Insert into SS_HT (UserName, UserID, Date, Division, Resion) values( @name, @id, @date, @division, '시스템일괄등록')";
							cmd.Parameters.Add("@name", Session["UserName"].ToString());
							cmd.Parameters.Add("@id", Session["ID"].ToString());
							cmd.Parameters.Add("@division", m_strStockDiv.ToString().Trim());
							cmd.Parameters.Add("@date",System.DateTime.Parse(DateTime.Now.ToShortDateString()));
							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							
							tran.Commit();
							nCommit = 0;
							
						}
						catch ( Exception ex )
						{
							Response.Write("<script language=javascript>");
							Response.Write("alert(\"" + ex.Message + "\");");
							Response.Write("</script>");
							tran.Rollback();
						}
						finally
						{
							con.Close();
						}

					}
					if(nCommit == 0)
						Response.Write("<script>alert('등록했습니다');</script>");



					break;
				case "원자재창고":

					foreach( DataRow drow in dsExcel.Tables[0].Rows )
					{
						j = 0;
						StringBuilder sbQuery = new StringBuilder("", 300);

						//금액은 품목정보의 기준단가를 가져와 곱해준다.
						SqlCommand comm = new SqlCommand("Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num",con);
						comm.Parameters.Add("@num", drow["품목번호"].ToString());
						con.Open();
						decimal deStandardUnitCost = Convert.ToDecimal(comm.ExecuteScalar());
						con.Close();
						

						sbQuery.Append( " update rms_mt set ");
						sbQuery.Append( " LastYearTransferQuantity = @StockQuantity" + nThisMonth + j +" , LastYearTransferCost = @StockCost" + nThisMonth + j +", ");
						//sbQuery.Append( " LastYearTransferQuantity = @StockQuantity10, LastYearTransferCost = @StockCost10, ");
						for( int i = nThisMonth ; i < 13 ; i ++ )
						{
							if(i < 12)
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j + "," );
							}
							else
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j );
							}

							cmd.Parameters.Add("@StockQuantity" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString());
							cmd.Parameters.Add("@StockCost" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString()) 
								* deStandardUnitCost;
						}

						sbQuery.Append(" where itemnum = @itemnum" + j + " and recodingstate = 1 and [Year] = @year" );

						
						

						cmd.Parameters.Add("@itemnum" + j, SqlDbType.VarChar).Value = drow["품목번호"];
						cmd.Parameters.Add("@year",DateTime.Now.Year);
						j++;


						cmd.CommandText = sbQuery.ToString();
						//int year = DateTime.Now.Year;
						//int mon = DateTime.Now.Month;
						//Table table = new Table(year,mon);
						//cmd.CommandText = table.InRowTable();
						SqlTransaction tran = null;

						try
						{
							con.Open();
							tran = con.BeginTransaction();
							cmd.Transaction = tran;

							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							cmd.CommandText = "Insert into SS_HT (UserName, UserID, Date, Division, Resion) values( @name, @id, @date, @division, '시스템일괄등록')";
							cmd.Parameters.Add("@name", Session["UserName"].ToString());
							cmd.Parameters.Add("@id", Session["ID"].ToString());
							cmd.Parameters.Add("@division", m_strStockDiv.ToString().Trim());
							cmd.Parameters.Add("@date",System.DateTime.Parse(DateTime.Now.ToShortDateString()));
							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							
							tran.Commit();
							nCommit = 0;
							
						}
						catch ( Exception ex )
						{
							Response.Write("<script language=javascript>");
							Response.Write("alert(\"" + ex.Message + "\");");
							Response.Write("</script>");
							tran.Rollback();
						}
						finally
						{
							con.Close();
						}

					}
					if(nCommit == 0)
						Response.Write("<script>alert('등록했습니다');</script>");
					break;
				case "외주창고":
					foreach( DataRow drow in dsExcel.Tables[0].Rows )
					{
						j = 0;
						StringBuilder sbQuery = new StringBuilder("", 300);

						//금액은 품목정보의 기준단가를 가져와 곱해준다.
						SqlCommand comm = new SqlCommand("Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num select ProgressRate from psi_mt where RecodingState = 1 and ItemNum = @num and processsequencenum =  @processsequencenum", con);
						comm.Parameters.Add("@num", drow["품목번호"].ToString());
						comm.Parameters.Add("@processsequencenum", SqlDbType.Int).Value = drow["공정순서번호"].ToString();
						con.Open();
						SqlDataReader dr = comm.ExecuteReader();
						decimal deStandardUnitCost = 0;
						decimal deProgressRate = 0;

						if ( dr.Read() )
						{
							deStandardUnitCost = Convert.ToDecimal( dr[0] );
						}

						if ( dr.NextResult() )
						{
							if ( dr.Read() )
							{
								deProgressRate = Convert.ToDecimal( dr[0] );
							}
						}
						
						
						con.Close();
						

						sbQuery.Append( " update os_mt set ");
						sbQuery.Append( " LastYearTransferQuantity = @StockQuantity" + nThisMonth + j +" , LastYearTransferCost = @StockCost" + nThisMonth + j +", ");
						//sbQuery.Append( " LastYearTransferQuantity = @StockQuantity10, LastYearTransferCost = @StockCost10, ");
						for( int i = nThisMonth ; i < 13 ; i ++ )
						{
							if(i < 12)
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j + "," );
							}
							else
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j );
							}

							cmd.Parameters.Add("@StockQuantity" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString());
							cmd.Parameters.Add("@StockCost" + i + j, SqlDbType.Decimal).Value = 
								(deStandardUnitCost * deProgressRate / 100) * decimal.Parse(drow["수량"].ToString()) ;
						}

						sbQuery.Append(" where itemnum = @itemnum" + j + " and processsequencenum = @processsequencenum" + j + "  and businessregistrationnum = @businessregistrationnum" + j + " and recodingstate = 1 and [Year] = @year" );
						cmd.Parameters.Add("@itemnum" + j, SqlDbType.VarChar).Value = drow["품목번호"];
						cmd.Parameters.Add("@processsequencenum" + j , SqlDbType.Int).Value = drow["공정순서번호"];
						cmd.Parameters.Add("@businessregistrationnum" + j, SqlDbType.VarChar).Value = drow["사업자등록번호"];
						cmd.Parameters.Add("@year",DateTime.Now.Year);
						j++;

						cmd.CommandText = sbQuery.ToString();
						SqlTransaction tran = null;

						try
						{
							con.Open();
							tran = con.BeginTransaction();
							cmd.Transaction = tran;

							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							cmd.CommandText = "Insert into SS_HT (UserName, UserID, Date, Division, Resion) values( @name, @id, @date, @division, '시스템일괄등록')";
							cmd.Parameters.Add("@name", Session["UserName"].ToString());
							cmd.Parameters.Add("@id", Session["ID"].ToString());
							cmd.Parameters.Add("@division", m_strStockDiv.ToString().Trim());
							cmd.Parameters.Add("@date",System.DateTime.Parse(DateTime.Now.ToShortDateString()));
							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							
							tran.Commit();
							nCommit = 0;
							
						}
						catch ( Exception ex )
						{
							Response.Write("<script language=javascript>");
							Response.Write("alert(\"" + ex.Message + "\");");
							Response.Write("</script>");
							tran.Rollback();
						}
						finally
						{
							con.Close();
						}

					}
					if(nCommit == 0)
						Response.Write("<script>alert('등록했습니다');</script>");
					break;
				case "납품창고":

					foreach( DataRow drow in dsExcel.Tables[0].Rows )
					{
						j = 0;
						StringBuilder sbQuery = new StringBuilder("", 300);

						//금액은 품목정보의 기준단가를 가져와 곱해준다.
						SqlCommand comm = new SqlCommand("Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num",con);
						comm.Parameters.Add("@num", drow["품목번호"].ToString());
						con.Open();
						decimal deStandardUnitCost = Convert.ToDecimal(comm.ExecuteScalar());
						con.Close();
						

						sbQuery.Append( " update ds_mt set ");
						sbQuery.Append( " LastYearTransferQuantity = @StockQuantity" + nThisMonth + j +" , LastYearTransferCost = @StockCost" + nThisMonth + j +", ");
						for( int i = nThisMonth ; i < 13 ; i ++ )
						{
							if(i < 12)
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j + "," );
							}
							else
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j );
							}

							cmd.Parameters.Add("@StockQuantity" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString());
							cmd.Parameters.Add("@StockCost" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString()) 
								* deStandardUnitCost;
						}

						sbQuery.Append(" where itemnum = @itemnum" + j + " and recodingstate = 1 and [Year] = @year" );
						cmd.Parameters.Add("@itemnum" + j, SqlDbType.VarChar).Value = drow["품목번호"];
						cmd.Parameters.Add("@year",DateTime.Now.Year);
						j++;


						cmd.CommandText = sbQuery.ToString();
						SqlTransaction tran = null;

						try
						{
							con.Open();
							tran = con.BeginTransaction();
							cmd.Transaction = tran;

							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							cmd.CommandText = "Insert into SS_HT (UserName, UserID, Date, Division, Resion) values( @name, @id, @date, @division, '시스템일괄등록')";
							cmd.Parameters.Add("@name", Session["UserName"].ToString());
							cmd.Parameters.Add("@id", Session["ID"].ToString());
							cmd.Parameters.Add("@division", m_strStockDiv.ToString().Trim());
							cmd.Parameters.Add("@date",System.DateTime.Parse(DateTime.Now.ToShortDateString()));
							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							
							tran.Commit();
							nCommit = 0;
							
						}
						catch ( Exception ex )
						{
							Response.Write("<script language=javascript>");
							Response.Write("alert(\"" + ex.Message + "\");");
							Response.Write("</script>");
							tran.Rollback();
						}
						finally
						{
							con.Close();
						}

					}
					if(nCommit == 0)
						Response.Write("<script>alert('등록했습니다');</script>"); 
					break;
				case "매입매출":
					// 파라미터를 식별하기 위한 변수
					j = 0;	
					foreach( DataRow drow in dsExcel.Tables[0].Rows )
					{
						StringBuilder sbQuery = new StringBuilder("",800);
						
						sbQuery.Append( " update bsi_mt set ");
						sbQuery.Append( " LastYearSaleTransferCost = @TotalSaleCost10, LastYearBuyingTransferCost = @TotalBuyingCost10, ");
						for( int i = nThisMonth ; i < 13 ; i ++ )
						{
							
							if(i < 12)
							{

								sbQuery.Append( " TotalSaleCost");
								sbQuery.Append(i);
								sbQuery.Append(" = @TotalSaleCost" + i + j );
								sbQuery.Append(", CollectMoney" + i );
								sbQuery.Append(" = @CollectMoney" + i + j );
								sbQuery.Append(", UncollectMoney" + i );
								sbQuery.Append(" = @UncollectMoney" + i + j + "," );

								sbQuery.Append( " TotalBuyingCost");
								sbQuery.Append(i);
								sbQuery.Append(" = @TotalBuyingCost" + i + j );
								sbQuery.Append(", PaymentMoney" + i );
								sbQuery.Append(" = @PaymentMoney" + i + j );
								sbQuery.Append(", UnPaymentMoney" + i );
								sbQuery.Append(" = @UnPaymentMoney" + i + j + "," );
							
							}
							else
							{
								sbQuery.Append( " TotalSaleCost");
								sbQuery.Append(i);
								sbQuery.Append(" = @TotalSaleCost" + i + j );
								sbQuery.Append(", CollectMoney" + i );
								sbQuery.Append(" = @CollectMoney" + i + j);
								sbQuery.Append(", UncollectMoney" + i );
								sbQuery.Append(" = @UncollectMoney" + i + j + "," );

								sbQuery.Append( " TotalBuyingCost");
								sbQuery.Append(i);
								sbQuery.Append(" = @TotalBuyingCost" + i + j );
								sbQuery.Append(", PaymentMoney" + i );
								sbQuery.Append(" = @PaymentMoney" + i + j);
								sbQuery.Append(", UnPaymentMoney" + i );
								sbQuery.Append(" = @UnPaymentMoney" + i + j );
							}
							cmd.Parameters.Add("@TotalSaleCost" + i + j , SqlDbType.VarChar).Value = drow["매출금액"];
							cmd.Parameters.Add("@CollectMoney" + i + j , SqlDbType.VarChar).Value = drow["수금액"];
							cmd.Parameters.Add("@UncollectMoney" + i + j , SqlDbType.VarChar).Value = drow["미수금액"];
							cmd.Parameters.Add("@TotalBuyingCost" + i + j , SqlDbType.VarChar).Value = drow["매입금액"];
							cmd.Parameters.Add("@PaymentMoney" + i + j , SqlDbType.VarChar).Value = drow["지급금액"];
							cmd.Parameters.Add("@UnPaymentMoney" + i + j , SqlDbType.VarChar).Value = drow["미지급금액"];
						}
												
						sbQuery.Append( " where businessregistrationnum = @businessregistrationnum" + j + " and recodingstate = 1 and [Year] = @year" );
						cmd.Parameters.Add("@businessregistrationnum" + j , SqlDbType.VarChar).Value = drow["사업자등록번호"];
						cmd.Parameters.Add("@year",DateTime.Now.Year);
						j++;


						cmd.CommandText = sbQuery.ToString();
						SqlTransaction tran = null;

						try
						{
							con.Open();
							tran = con.BeginTransaction();
							cmd.Transaction = tran;

							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							cmd.CommandText = "Insert into SS_HT (UserName, UserID, Date, Division, Resion) values( @name, @id, @date, @division, '시스템일괄등록')";
							cmd.Parameters.Add("@name", Session["UserName"].ToString());
							cmd.Parameters.Add("@id", Session["ID"].ToString());
							cmd.Parameters.Add("@division", m_strStockDiv.ToString().Trim());
							cmd.Parameters.Add("@date",System.DateTime.Parse(DateTime.Now.ToShortDateString()));
							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							
							tran.Commit();
							nCommit = 0;
							
						}
						catch ( Exception ex )
						{
							Response.Write("<script language=javascript>");
							Response.Write("alert(\"" + ex.Message + "\");");
							Response.Write("</script>");
							tran.Rollback();
						}
						finally
						{
							con.Close();
						}
					} 
					if(nCommit == 0)
						Response.Write("<script>alert('등록했습니다');</script>");
					break;
				case "보용품창고":

					foreach( DataRow drow in dsExcel.Tables[0].Rows )
					{
						j = 0;
						StringBuilder sbQuery = new StringBuilder("", 300);
						//금액은 품목정보의 기준단가를 가져와 곱해준다.
						SqlCommand comm = new SqlCommand("Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @num",con);
						comm.Parameters.Add("@num", drow["품목번호"].ToString());
						con.Open();
						decimal deStandardUnitCost = Convert.ToDecimal(comm.ExecuteScalar());
						con.Close();
						

						sbQuery.Append( " update AS_mt set ");
						sbQuery.Append( " LastYearTransferQuantity = @StockQuantity, LastYearTransferCost = @StockCost, ");
						for( int i = nThisMonth ; i < 13 ; i ++ )
						{
							if(i < 12)
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j + "," );
							}
							else
							{
								sbQuery.Append( " StockQuantity");
								sbQuery.Append(i);
								sbQuery.Append(" = @StockQuantity" + i + j );
								sbQuery.Append(", StockCost" + i );
								sbQuery.Append(" = @StockCost" + i + j );
							}

							cmd.Parameters.Add("@StockQuantity" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString());
							cmd.Parameters.Add("@StockCost" + i + j, SqlDbType.Decimal).Value = decimal.Parse(drow["수량"].ToString()) 
								* deStandardUnitCost;
						}

						sbQuery.Append(" where itemnum = @itemnum" + j + " and recodingstate = 1 and [Year] = @year" );

						
						

						cmd.Parameters.Add("@itemnum" + j, SqlDbType.VarChar).Value = drow["품목번호"];
						cmd.Parameters.Add("@year",DateTime.Now.Year);
						j++;


						cmd.CommandText = sbQuery.ToString();
						SqlTransaction tran = null;

						try
						{
							con.Open();
							tran = con.BeginTransaction();
							cmd.Transaction = tran;

							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							cmd.CommandText = "Insert into SS_HT (UserName, UserID, Date, Division, Resion) values( @name, @id, @date,@division, '시스템일괄등록')";
							cmd.Parameters.Add("@name", Session["UserName"].ToString());
							cmd.Parameters.Add("@id", Session["ID"].ToString());
							cmd.Parameters.Add("@division", m_strStockDiv.ToString().Trim());
							cmd.Parameters.Add("@date",System.DateTime.Parse(DateTime.Now.ToShortDateString()));
							cmd.ExecuteNonQuery();
							cmd.Parameters.Clear();

							
							tran.Commit();
							nCommit = 0;
							
						}
						catch ( Exception ex )
						{
							Response.Write("<script language=javascript>");
							Response.Write("alert(\"" + ex.Message + "\");");
							Response.Write("</script>");
							tran.Rollback();
						}
						finally
						{
							con.Close();
						}

					}
					if(nCommit == 0)
						Response.Write("<script>alert('등록했습니다');</script>");
					break;
			}
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language=javascript>window.open('./PopupWindows/History.aspx','SystemInfoManagement'," + "'width=850" + "," + "height=500px');</script>");
		}
	}
}
