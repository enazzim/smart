using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


using System.Configuration;
using System.Data.SqlClient;


namespace KIT_ERP.ProductionManagement.PopupWindows
{
	/// <summary>
	/// UseTool에 대한 요약 설명입니다.
	/// </summary>
	public class UseTool : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.DropDownList dl_UseTool1;
		protected System.Web.UI.WebControls.DropDownList dl_UseJig1;
		protected System.Web.UI.WebControls.DropDownList dl_UseTool2;
		protected System.Web.UI.WebControls.DropDownList dl_UseJig2;
		protected System.Web.UI.WebControls.DropDownList dl_UseTool3;
		protected System.Web.UI.WebControls.DropDownList dl_UseJig3;
		protected ArrayList arr ;
		protected System.Web.UI.WebControls.DropDownList dl_WCName;
		protected System.Web.UI.WebControls.DropDownList dl_Worker;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected ArrayList list;


		/// <summary>
		/// 작업일보등록시 사용할 필드
		/// </summary>
		private string Child = "";				//창고출고품목 담는 필드
		private decimal NeedQuantity=0;			//창고출고량 담는 필드
		

		private DataSet UnderProcess()
		{
			int Sequence = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = @"Select Max(ProcessSequenceNum) From PSI_MT where ItemNum = @ItemNum and ProcessSequenceNum < @Sequence and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@ItemNum",arr[0].ToString().Trim());
			comm.Parameters.Add("@Sequence",int.Parse(arr[3].ToString().Trim()));
			conn.Open();
			Sequence = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();
			comm.Parameters.Clear();

			str = @"Select II_MT.ItemNum,II_MT.ItemDrawNum,II_MT.ItemName, PSI_MT.ProcessSequenceNum, 
						(Select ProcessCode From PSI_MT Where ItemNum = @ItemNum and ProcessSequenceNum = @Sequence and RecodingState = 1) as ProcessCode,
						(select SmallClassificationName 
							From PSI_MT Inner join  PUC_MT on PSI_MT.ProcessCode = SmallClassificationCode
							Where PSI_MT.RecodingState = 1 and PUC_MT.RecodingState =1 and PSI_MT.ItemNum = @ItemNum and PSI_MT.ProcessSequenceNum = @Sequence   ) as ProcessName, 
					@WorkQuantity as WorkPlanQuantity,  @WorkQuantity as UseQuantity
					From II_MT inner join PSI_MT on II_MT.ItemNum = PSI_MT.ItemNum
					Inner join PUC_MT on PSI_MT.ProcessCode = SmallClassificationCode
					Where II_MT.ItemNum = @ItemNum and  ProcessSequenceNum = @Sequence 
					and II_MT.RecodingState = 1 and PSI_MT.RecodingState = 1 and PUC_MT.RecodingState = 1";
			
			comm.CommandText=str;
			comm.Parameters.Add("@ItemNum",arr[0].ToString().Trim());
			comm.Parameters.Add("@Sequence",Sequence);
			comm.Parameters.Add("@WorkQuantity",decimal.Parse(arr[14].ToString().Trim()));

			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds =new DataSet();
			da.Fill(ds);
			comm.Parameters.Clear();
			return ds;
		}

		private DataTable UnderItem()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select ChildItemNum,NeedQuantityNumerator,NeedQuantityDenominator From IOI_MT where RecodingState = 1 and ParentItemNum = @itemnum order by ChildItemNum";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value =arr[0].ToString();
			SqlDataAdapter da1 = new SqlDataAdapter(comm);
			DataSet ds1 = new DataSet();
			da1.Fill(ds1);
			comm.Parameters.Clear();

			DataTable dt = new DataTable();
			DataColumn dc = new DataColumn("chk", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemNum", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemDrawNum", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemName", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ProcessSequenceNum", typeof(int));
			dt.Columns.Add(dc);
			dc = new DataColumn("ProcessCode", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ProcessName", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("WorkPlanQuantity", typeof(Decimal));
			dt.Columns.Add(dc);			

			foreach(DataRow drow in ds1.Tables[0].Rows)
			{
				Child = drow["ChildItemNum"].ToString();
				NeedQuantity = decimal.Parse(drow["NeedQuantityNumerator"].ToString())/decimal.Parse(drow["NeedQuantityDenominator"].ToString())* decimal.Parse(arr[14].ToString());//소요량* 작업완료수량 => 창고에서 떨어낼 량
				DataRow dr = dt.NewRow();
				//창고에서 떨어줘야 한다
				if(Division(Child)==1)//원자재이면
				{
					
					dr["chk"] = Convert.DBNull;
					dr["ItemNum"] = drow["ChildItemNum"].ToString();
					dr["ItemDrawNum"] = ItemDraw(drow["ChildItemNum"].ToString());
					dr["ItemName"] = ItemName(drow["ChildItemNum"].ToString());
					dr["ProcessSequenceNum"] = 0;
					dr["ProcessCode"] = "14000000";
					dr["ProcessName"] = "소재";
					if(ItemDivision(drow["ChildItemNum"].ToString()))
						dr["WorkPlanQuantity"] = decimal.Round(NeedQuantity,3);
					else
						dr["WorkPlanQuantity"] = 0;
					
				
					dt.Rows.Add(dr);
					
				}
				else//반제품이면
				{

					if(ExistProcess(Child) == 0)
					{
						dr["chk"] = Convert.DBNull;
						dr["ItemNum"] = drow["ChildItemNum"].ToString();
						dr["ItemDrawNum"] = ItemDraw(drow["ChildItemNum"].ToString());
						dr["ItemName"] = ItemName(drow["ChildItemNum"].ToString());
						if(Process(drow["ChildItemNum"].ToString()) == 0)
							dr["ProcessSequenceNum"] = 0;//Process(drow["ChildItemNum"].ToString());
						dr["ProcessCode"] = ProcessCode(drow["ChildItemNum"].ToString(),Process(drow["ChildItemNum"].ToString()));
						dr["ProcessName"] = ProcessName(drow["ChildItemNum"].ToString(),Process(drow["ChildItemNum"].ToString()));
						if(ItemDivision(drow["ChildItemNum"].ToString()))
							dr["WorkPlanQuantity"] = decimal.Round(NeedQuantity,3);
						else
							dr["WorkPlanQuantity"] = 0;

					}
					else
					{
						//DataRow dr = dt.NewRow();
						dr["chk"] = Convert.DBNull;
						dr["ItemNum"] = drow["ChildItemNum"].ToString();
						dr["ItemDrawNum"] = ItemDraw(drow["ChildItemNum"].ToString());
						dr["ItemName"] = ItemName(drow["ChildItemNum"].ToString());
						dr["ProcessSequenceNum"] = Process(drow["ChildItemNum"].ToString());
						dr["ProcessCode"] = ProcessCode(drow["ChildItemNum"].ToString(),Process(drow["ChildItemNum"].ToString()));
						dr["ProcessName"] = ProcessName(drow["ChildItemNum"].ToString(),Process(drow["ChildItemNum"].ToString()));
						if(ItemDivision(drow["ChildItemNum"].ToString()))
							dr["WorkPlanQuantity"] = decimal.Round(NeedQuantity,3);
						else
							dr["WorkPlanQuantity"] = 0;
					}
					dt.Rows.Add(dr);
				}
			}
			comm.Parameters.Clear();

			return dt;
		}


		private int ExistProcess(string Item)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select count(*) From PSI_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@itemnum",Item);
			conn.Open();
			int count = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();
			comm.Parameters.Clear();
			return count;

		}


		

		private int Division(string ItemNum)
		{
			string aa="";

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemNum.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			conn.Close();

			if(aa.ToString() == "원자재")
				return 1;
			else if(aa.ToString() =="반제품")
				return 2;
			else if(aa.ToString() =="제품")
				return 3;
			else if(aa.ToString() =="상품")
				return 4;
			else
				return 5;
		}


	
		private bool ItemDivision(string ItemNum)
		{
			bool aa = false ;

			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select StockManagable From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemNum.ToString();
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = bool.Parse(dr["StockManagable"].ToString());
			}
			conn.Close();

			return aa;
		}

		private string ItemDraw(string Item)
		{
			string ItemDrawNum = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select ItemDrawNum From II_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@ItemNum",Item);
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ItemDrawNum = dr["ItemDrawNum"].ToString();
			}
			conn.Close();
			
			return ItemDrawNum;
			
		}

		private string ItemName(string Item)
		{
			string ItemName = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select ItemName From II_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@ItemNum",Item);
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ItemName = dr["ItemName"].ToString();
			}
			conn.Close();
			
			return ItemName;
		}

		private int Process(string Item)
		{
			int Num = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select isnull(Max(ProcessSequenceNum),0) From PSI_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@ItemNum",Item);
			conn.Open();
			Num = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();
			
			return Num;
		}


		private string ProcessCode(string Item, int Sequence)
		{
			string ProcessCode = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select ProcessCode  From PSI_MT where ItemNum = @ItemNum and ProcessSequenceNum = @num and PSI_MT.RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@ItemNum",Item);
			comm.Parameters.Add("@num",Sequence);
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ProcessCode = dr["ProcessCode"].ToString();
			}
			conn.Close();
			
			return ProcessCode;
		}


		private string ProcessName(string Item, int Sequence)
		{
			string ProcessName = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select SmallClassificationName as ProcessName From PSI_MT Inner join PUC_MT on ProcessCode = SmallClassificationCode where ItemNum = @ItemNum and ProcessSequenceNum = @num and PSI_MT.RecodingState = 1 and PUC_MT.RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@ItemNum",Item);
			comm.Parameters.Add("@num",Sequence);
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ProcessName = dr["ProcessName"].ToString();
			}
			conn.Close();
			
			return ProcessName;
		}

		private bool MinProcess(string Item, int sequence)
		{
			int Num = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str ="Select Min(ProcessSequenceNum) From PSI_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection= conn;
			comm.CommandText=str;
			comm.Parameters.Add("@ItemNum",Item);
			conn.Open();
			Num = int.Parse(comm.ExecuteScalar().ToString());
			conn.Close();

			if(sequence == Num)
				return true;
			else
				return false;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			

			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				arr = (ArrayList)Session["arr"];
				list = (ArrayList)Session["list"];
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();

				//	*************************************************
				//	**  WC명 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				//WC정보테이블에서에서 WC명과 WC정보번호를 가지고옴
				string str_WC = "Select WCName, WCInfoIndex from WCI_MT where RecodingState = 1 order by WCName";
				SqlCommand comm_WC = new SqlCommand(str_WC,conn);
				SqlDataAdapter da_WC = new SqlDataAdapter(comm_WC) ;
				DataSet ds_WC = new DataSet() ;
				da_WC.Fill(ds_WC);
				
				dl_WCName.DataSource = ds_WC;
				dl_WCName.DataTextField = ds_WC.Tables[0].Columns[0].ToString();
				dl_WCName.DataValueField = ds_WC.Tables[0].Columns[1].ToString();
				dl_WCName.DataBind();
				dl_WCName.Items.Insert(0, "-선택-") ;
				dl_WCName.Items[0].Value = "";
				int count = 0;
				for(int a = 0 ; a <dl_WCName.Items.Count; a++)
				{
					if(dl_WCName.Items[a].Text == list[0].ToString())
					{
						dl_WCName.Items[a].Selected  = true;
						count = 1;
						break;
					}
				}
				if(count == 0)
				{
					dl_WCName.Items[0].Selected  = true;
				}
				else
					count = 0;



				//	*************************************************
				//	**  사용공구1 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				// 설비정보에서 설비분류가 공구인것을 가지고 옴.
				string str_Tool1 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200010' order by EquipmentName" ;
				SqlCommand comm_Tool1 = new SqlCommand(str_Tool1,conn);
				SqlDataAdapter da_Tool1 = new SqlDataAdapter(comm_Tool1) ;
				DataSet ds_Tool1 = new DataSet() ;
				da_Tool1.Fill(ds_Tool1);
				
				dl_UseTool1.DataSource = ds_Tool1;
				dl_UseTool1.DataTextField = ds_Tool1.Tables[0].Columns[1].ToString();
				dl_UseTool1.DataValueField = ds_Tool1.Tables[0].Columns[0].ToString();
				dl_UseTool1.DataBind();
				dl_UseTool1.Items.Insert(0, "-선택-") ;
				dl_UseTool1.Items[0].Value = "";

				for(int a = 0; a<dl_UseTool1.Items.Count; a++)
				{
					if(dl_UseTool1.Items[a].Text == list[1].ToString())
					{
						dl_UseTool1.Items[a].Selected = true;
						count = 1;
						break;
					}
				}
				if(count == 0)
				{
					dl_UseTool1.Items[0].Selected  = true;
				}
				else
					count = 0;

				//	*************************************************
				//	**  사용공구2 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				// 설비정보에서 설비분류가 공구인것을 가지고 옴.
				string str_Tool2 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200010' order by EquipmentName" ;
				SqlCommand comm_Tool2 = new SqlCommand(str_Tool2,conn);
				SqlDataAdapter da_Tool2 = new SqlDataAdapter(comm_Tool2) ;
				DataSet ds_Tool2 = new DataSet() ;
				da_Tool2.Fill(ds_Tool2);
				
				dl_UseTool2.DataSource = ds_Tool2;
				dl_UseTool2.DataTextField = ds_Tool2.Tables[0].Columns[1].ToString();
				dl_UseTool2.DataValueField = ds_Tool2.Tables[0].Columns[0].ToString();
				dl_UseTool2.DataBind();
				dl_UseTool2.Items.Insert(0, "-선택-") ;
				dl_UseTool2.Items[0].Value = "";

				for(int a = 0; a<dl_UseTool2.Items.Count; a++)
				{
					if(dl_UseTool2.Items[a].Text == list[3].ToString())
					{
						dl_UseTool2.Items[a].Selected = true;
						count = 1;
						break;
					}
				}
				if(count == 0)
				{
					dl_UseTool2.Items[0].Selected  = true;
				}
				else
					count = 0;

				//	*************************************************
				//	**  사용공구3 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				// 설비정보에서 설비분류가 공구인것을 가지고 옴.
				string str_Tool3 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200010' order by EquipmentName" ;
				SqlCommand comm_Tool3 = new SqlCommand(str_Tool3,conn);
				SqlDataAdapter da_Tool3 = new SqlDataAdapter(comm_Tool3) ;
				DataSet ds_Tool3 = new DataSet() ;
				da_Tool3.Fill(ds_Tool3);
				
				dl_UseTool3.DataSource = ds_Tool3;
				dl_UseTool3.DataTextField = ds_Tool3.Tables[0].Columns[1].ToString();
				dl_UseTool3.DataValueField = ds_Tool3.Tables[0].Columns[0].ToString();
				dl_UseTool3.DataBind();
				dl_UseTool3.Items.Insert(0, "-선택-") ;
				dl_UseTool3.Items[0].Value = "";

				for(int a = 0; a<dl_UseTool3.Items.Count; a++)
				{
					if(dl_UseTool3.Items[a].Text == list[5].ToString())
					{
						dl_UseTool3.Items[a].Selected = true;
						count = 1;
						break;
					}
				}
				if(count == 0)
					dl_UseTool3.Items[0].Selected  = true;
				else
					count = 0;

				//	*************************************************
				//	**  사용치구1 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				// 설비정보에서 설비분류가 치구인것을 가지고 옴.
				string str_Jig1 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200020' order by EquipmentName" ;
				SqlCommand comm_Jig1 = new SqlCommand(str_Jig1,conn);
				SqlDataAdapter da_Jig1 = new SqlDataAdapter(comm_Jig1) ;
				DataSet ds_Jig1 = new DataSet() ;
				da_Jig1.Fill(ds_Jig1);
				
				dl_UseJig1.DataSource = ds_Jig1;
				dl_UseJig1.DataTextField = ds_Jig1.Tables[0].Columns[1].ToString();
				dl_UseJig1.DataValueField = ds_Jig1.Tables[0].Columns[0].ToString();
				dl_UseJig1.DataBind();
				dl_UseJig1.Items.Insert(0, "-선택-") ;
				dl_UseJig1.Items[0].Value = "";

				for(int a = 0; a<dl_UseJig1.Items.Count; a++)
				{
					if(dl_UseJig1.Items[a].Text == list[2].ToString())
					{
						dl_UseJig1.Items[a].Selected = true;
						count = 1;
						break;
					}
				}
				if(count == 0)
					dl_UseJig1.Items[0].Selected  = true;
				else
					count = 0;

				

				//	*************************************************
				//	**  사용치구2 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				// 설비정보에서 설비분류가 치구인것을 가지고 옴.
				string str_Jig2 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200020' order by EquipmentName" ;
				SqlCommand comm_Jig2 = new SqlCommand(str_Jig2,conn);
				SqlDataAdapter da_Jig2 = new SqlDataAdapter(comm_Jig2);
				DataSet ds_Jig2 = new DataSet();
				da_Jig2.Fill(ds_Jig2);
				
				dl_UseJig2.DataSource = ds_Jig2;
				dl_UseJig2.DataTextField = ds_Jig2.Tables[0].Columns[1].ToString();
				dl_UseJig2.DataValueField = ds_Jig2.Tables[0].Columns[0].ToString();
				dl_UseJig2.DataBind();
				dl_UseJig2.Items.Insert(0, "-선택-") ;
				dl_UseJig2.Items[0].Value = "";

				for(int a = 0; a<dl_UseJig2.Items.Count; a++)
				{
					if(dl_UseJig2.Items[a].Text == list[4].ToString())
					{
						dl_UseJig2.Items[a].Selected = true;
						count = 1;
						break;
					}
				}
				if(count == 0)
					dl_UseJig2.Items[0].Selected  = true;
				else
					count = 0;

				//	*************************************************
				//	**  사용치구3 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				// 설비정보에서 설비분류가 치구인것을 가지고 옴.
				string str_Jig3 = "Select EquipmentNum, EquipmentName from EI_MT where RecodingState = 1 and EquipmentClassification = '07200020' order by EquipmentName" ;
				SqlCommand comm_Jig3 = new SqlCommand(str_Jig3,conn);
				SqlDataAdapter da_Jig3 = new SqlDataAdapter(comm_Jig3) ;
				DataSet ds_Jig3 = new DataSet() ;
				da_Jig3.Fill(ds_Jig3);
				
				dl_UseJig3.DataSource = ds_Jig3;
				dl_UseJig3.DataTextField = ds_Jig3.Tables[0].Columns[1].ToString();
				dl_UseJig3.DataValueField = ds_Jig3.Tables[0].Columns[0].ToString();
				dl_UseJig3.DataBind();
				dl_UseJig3.Items.Insert(0, "-선택-") ;
				dl_UseJig3.Items[0].Value = "";

				for(int a = 0; a<dl_UseJig3.Items.Count; a++)
				{
					if(dl_UseJig3.Items[a].Text == list[6].ToString())
					{
						dl_UseJig3.Items[a].Selected = true;
						count = 1;
						break;
					}
				}
				if(count == 0)
					dl_UseJig3.Items[0].Selected  = true;
				else
					count = 0;




				//	*************************************************
				//	**  작업자 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				// 사용자정보에서 가지고 옴.
				string str_Worker = "Select Name, ID from UI_MT where RecodingState = 1 and BusinessRegistrationNum = ''";
				SqlCommand comm_Worker = new SqlCommand(str_Worker,conn);
				SqlDataAdapter da_Worker = new SqlDataAdapter(comm_Worker) ;
				DataSet ds_Worker = new DataSet() ;
				da_Worker.Fill(ds_Worker);
				
				dl_Worker.DataSource = ds_Worker;
				dl_Worker.DataTextField = ds_Worker.Tables[0].Columns[0].ToString();
				dl_Worker.DataValueField = ds_Worker.Tables[0].Columns[1].ToString();
				dl_Worker.DataBind();
				dl_Worker.Items.Insert(0, "-선택-") ;
				dl_Worker.Items[0].Value = "";

				for(int a = 0; a<ds_Worker.Tables[0].Rows.Count; a++)
				{
					if(ds_Worker.Tables[0].Rows[a]["ID"].ToString() == list[7].ToString())
					{
						dl_Worker.Items[a+1].Selected = true;
						count = 1;
						break;
					}
				}
				if(count == 0)
					dl_Worker.Items[0].Selected  = true;
				else
					count = 0;


				

				conn.Close();

				Session["arr"] = arr;
				Session["list"] = list;

				bool WorkPlan3 = false;
				SqlConnection conn1 = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				conn1.Open();

				string strSQL = @"SELECT PresentRowMaterialUsed, SafeyRowMaterialUsed, OrderNonInStorehouseUsed, 
								  OrderGapInStorehouseUsed, MinimumGapUsed,OrderRequestStandbyUsed,OutOrderUsed,
								  BundlingUsed,WorkPlan1, WorkPlan2, WorkPlan3,InWorkConsidering,MinusStockAddUsed FROM SCS_T";

				SqlCommand comm = new SqlCommand(strSQL, conn1);

				SqlDataReader reader = comm.ExecuteReader();

				while(reader.Read())
				{
					if(bool.Parse(reader["WorkPlan3"].ToString()))
						WorkPlan3 = true;

				}
				reader.Close();
				conn1.Close();			

				if(WorkPlan3)
				{
//					UltraWebGrid1.DataSource = RowMaterial(arr[0].ToString(),decimal.Parse(arr[14].ToString()));
//					UltraWebGrid1.DataBind();
//
//					if(PropertyClassification(arr[0].ToString(),int.Parse(arr[3].ToString())))
//					{
//
//						for(int a =0; a< UltraWebGrid1.Rows.Count; a++)
//						{
//							UltraWebGrid1.Rows[a].Cells.FromKey("chk").Value = true;
//						}
//					}

					if(!MinProcess(arr[0].ToString(),int.Parse(arr[3].ToString())))
					{
						UltraWebGrid1.DataSource = UnderProcess();				
					}
					else
					{
						UltraWebGrid1.DataSource = UnderItem();			
					}
					UltraWebGrid1.DataBind();

					if(StoreManage(arr[0].ToString()) == "예")
					{
						for(int a =0; a< UltraWebGrid1.Rows.Count; a++)
						{
							UltraWebGrid1.Rows[a].Cells.FromKey("chk").Value = true;
						}
					}
					
				}
				else
				{

					if(!MinProcess(arr[0].ToString(),int.Parse(arr[3].ToString())))
					{
						UltraWebGrid1.DataSource = UnderProcess();				
					}
					else
					{
						UltraWebGrid1.DataSource = UnderItem();			
					}
					UltraWebGrid1.DataBind();

					if(PropertyClassification(arr[0].ToString(),int.Parse(arr[3].ToString())))
					{
						for(int a =0; a< UltraWebGrid1.Rows.Count; a++)
						{
							UltraWebGrid1.Rows[a].Cells.FromKey("chk").Value = true;
						}
					}
					else if(StoreManage(arr[0].ToString()) == "예")
					{
						for(int a =0; a< UltraWebGrid1.Rows.Count; a++)
						{
							UltraWebGrid1.Rows[a].Cells.FromKey("chk").Value = true;
						}
					}
				}
				
			}
			else
			{
				arr=(ArrayList)Session["arr"];
				list=(ArrayList)Session["list"];
			}

			
		}


		/// <summary>
		/// 재고관리여부
		/// </summary>
		/// <param name="ItemNum"></param>
		/// <returns></returns>
		private string StoreManage(string ItemNum)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string store = "아니오";
			string str = "Select Unit4 From II_MT Where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);			
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				store = dr["Unit4"].ToString();				
			}
			dr.Close();	
			comm.Parameters.Clear();
			conn.Close();

			return store;
		}


		private bool PropertyClassification(string ItemNum, int Seq)
		{
			string Classification = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "Select PropertyClassification From II_MT where ItemNum = @ItemNum and RecodingState  =1";
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Classification = dr["PropertyClassification"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();

			if(Classification == "제품")
			{
				comm.CommandText = "Select Max(ProcessSequenceNum) From PSI_MT where ItemNum = @ItemNum and RecodingState = 1";
				comm.Parameters.Add("@ItemNum",ItemNum);
				int Num = int.Parse(comm.ExecuteScalar().ToString());
				comm.Parameters.Clear();
				conn.Close();
				if(Num == Seq)
					return true;
				else
					return false;
			}
			else
			{
				conn.Close();
				return false;
			}
		}
		private DataTable RowMaterial(string ItemNum, decimal Quantity)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			SqlCommand comm = new SqlCommand("dbo.WorkRowBomTree", conn);
			comm.CommandType = CommandType.StoredProcedure;

			comm.Parameters.Add("@ItemNum", SqlDbType.VarChar).Value = ItemNum;
			comm.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Quantity;
			comm.Parameters.Add("@Tree", SqlDbType.Bit).Value = 0;

			SqlDataAdapter adapter = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
				
			adapter.Fill(ds);

			DataTable dt = new DataTable();
			DataColumn dc = new DataColumn("chk", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemNum", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemDrawNum", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ItemName", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ProcessSequenceNum", typeof(int));
			dt.Columns.Add(dc);
			dc = new DataColumn("ProcessCode", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("ProcessName", typeof(string));
			dt.Columns.Add(dc);
			dc = new DataColumn("WorkPlanQuantity", typeof(Decimal));
			dt.Columns.Add(dc);			

			foreach(DataRow drow in ds.Tables[0].Rows)
			{
				DataRow dr = dt.NewRow();
				
				dr["chk"] = Convert.DBNull;
				dr["ItemNum"] = drow["ItemNum"].ToString();
				dr["ItemDrawNum"] = ItemDraw(drow["ItemNum"].ToString());
				dr["ItemName"] = ItemName(drow["ItemNum"].ToString());
				dr["ProcessSequenceNum"] = 0;
				dr["ProcessCode"] = "14000000";
				dr["ProcessName"] = "소재";
				if(ItemDivision(drow["ItemNum"].ToString()))
                    dr["WorkPlanQuantity"] = decimal.Parse(drow["Quantity"].ToString());			
				else
					dr["WorkPlanQuantity"] = 0;
				
				dt.Rows.Add(dr);
				
			}

			return dt;

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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		//
		private void Button1_Click(object sender, System.EventArgs e)
		{
			ArrayList list1 = new ArrayList();

			
			

			if(dl_WCName.SelectedItem.Value == "")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('작업장을 선택하세요!');");
				Response.Write("</script>");
				Session["arr"] = arr;

			}
			else if(dl_Worker.SelectedItem.Value =="")
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('작업자를 선택하세요!');");
				Response.Write("</script>");
				Session["arr"] = arr;
			}			
			else
			{
				for( int i = UltraWebGrid1.Rows.Count-1 ; i >= 0 ; i--)
				{
					if (!Convert.ToBoolean( UltraWebGrid1.Rows[i].Cells.FromKey("chk").Value ))
					{
						UltraWebGrid1.Rows[i].Delete();
					}
				}
				
				list1.Add(dl_UseTool1.SelectedItem.Value);
				list1.Add(dl_UseJig1.SelectedItem.Value);
				list1.Add(dl_UseTool2.SelectedItem.Value);
				list1.Add(dl_UseJig2.SelectedItem.Value);
				list1.Add(dl_UseTool3.SelectedItem.Value);
				list1.Add(dl_UseJig3.SelectedItem.Value);
				list1.Add(dl_WCName.SelectedItem.Value);
				list1.Add(dl_WCName.SelectedItem.Text);
				list1.Add(dl_Worker.SelectedItem.Value);
				list1.Add(dl_Worker.SelectedItem.Text);

				Register reg = new Register(arr,list1,Session["ID"].ToString(),UltraWebGrid1);
				if(reg.WDR_Register())
				{
					Session.Remove("arr");
					Session.Remove("list");

					RegisterStartupScript("","<script>Register();</script>");	
				}
				else
				{
					Session.Remove("arr");
					Session.Remove("list");
					RegisterStartupScript("","<script>Register1();</script>");	
				}
			}
		}
	}
}
