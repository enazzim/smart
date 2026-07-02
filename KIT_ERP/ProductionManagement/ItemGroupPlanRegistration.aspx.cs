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

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// ItemGroupPlanRegistration에 대한 요약 설명입니다.
	/// </summary>
	public class ItemGroupPlanRegistration : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList ddlItemClassification1;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMinDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcMaxDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.HtmlControls.HtmlInputHidden chkAll;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Button btnOK;
		protected System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
		protected System.Data.SqlClient.SqlCommand sqlSelectCommand1;
		protected System.Data.SqlClient.SqlConnection sqlConnection1;
		protected KIT_ERP.ProductionManagement.dsPlanQuantityResult dsPlanQuantityResult1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			//			if(Session["ID"] == null)
			//			{	
			//				Session.Abandon();
			//				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			//			}
			
			ItemSearchControl1.Phantom = true;

			if(!Page.IsPostBack)
			{
				//	*************************************************
				//	**  품목분류1 드롭다운리스트... 데이타바인딩...**		
				//	*************************************************
				//코드분류표에서 대분류명이 품목분류1인 소분류명을 가지고 옴.
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				string str_ItemClassification1 = "Select SmallClassificationName, SmallClassificationCode from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and LargeClassificationCode = '0210'" ;
				SqlCommand comm_ItemClassification1 = new SqlCommand(str_ItemClassification1,conn);
				SqlDataAdapter da_ItemClassification1 = new SqlDataAdapter(comm_ItemClassification1) ;
				DataSet ds_ItemClassification1 = new DataSet() ;
				da_ItemClassification1.Fill(ds_ItemClassification1);
				
				ddlItemClassification1.DataSource = ds_ItemClassification1;
				ddlItemClassification1.DataTextField = ds_ItemClassification1.Tables[0].Columns[0].ToString();
				ddlItemClassification1.DataValueField = ds_ItemClassification1.Tables[0].Columns[1].ToString();
				ddlItemClassification1.DataBind();
				ddlItemClassification1.Items.Insert(0, "-선택하세요-") ;
				ddlItemClassification1.Items[0].Value = "";

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
			this.sqlDataAdapter1 = new System.Data.SqlClient.SqlDataAdapter();
			this.sqlSelectCommand1 = new System.Data.SqlClient.SqlCommand();
			this.sqlConnection1 = new System.Data.SqlClient.SqlConnection();
			this.dsPlanQuantityResult1 = new KIT_ERP.ProductionManagement.dsPlanQuantityResult();
			((System.ComponentModel.ISupportInitialize)(this.dsPlanQuantityResult1)).BeginInit();
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// sqlDataAdapter1
			// 
			this.sqlDataAdapter1.SelectCommand = this.sqlSelectCommand1;
			this.sqlDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									  new System.Data.Common.DataTableMapping("Table", "TempGroupViewPP_HT", new System.Data.Common.DataColumnMapping[] {
																																																							new System.Data.Common.DataColumnMapping("Idx", "Idx"),
																																																							new System.Data.Common.DataColumnMapping("ItemNum", "ItemNum"),
																																																							new System.Data.Common.DataColumnMapping("ItemName", "ItemName"),
																																																							new System.Data.Common.DataColumnMapping("OrderRemainQuantity", "OrderRemainQuantity"),
																																																							new System.Data.Common.DataColumnMapping("PlanRemainQuantity", "PlanRemainQuantity"),
																																																							new System.Data.Common.DataColumnMapping("InsufficiencyQuantity", "InsufficiencyQuantity"),
																																																							new System.Data.Common.DataColumnMapping("StockQuantity", "StockQuantity"),
																																																							new System.Data.Common.DataColumnMapping("ThisPlanQuantity", "ThisPlanQuantity"),
																																																							new System.Data.Common.DataColumnMapping("NeedQuantity", "NeedQuantity"),
																																																							new System.Data.Common.DataColumnMapping("ProductionCompleteDate", "ProductionCompleteDate"),
																																																							new System.Data.Common.DataColumnMapping("ProductionBeginDate", "ProductionBeginDate")})});
			// 
			// sqlSelectCommand1
			// 
			this.sqlSelectCommand1.CommandText = @"SELECT TempGroupViewPP_HT.Idx, TempGroupViewPP_HT.ItemNum, II_MT.ItemName, TempGroupViewPP_HT.OrderRemainQuantity, TempGroupViewPP_HT.PlanRemainQuantity, TempGroupViewPP_HT.InsufficiencyQuantity, TempGroupViewPP_HT.StockQuantity, TempGroupViewPP_HT.ThisPlanQuantity, TempGroupViewPP_HT.NeedQuantity, TempGroupViewPP_HT.ProductionCompleteDate, TempGroupViewPP_HT.ProductionBeginDate FROM TempGroupViewPP_HT INNER JOIN II_MT ON TempGroupViewPP_HT.ItemNum = II_MT.ItemNum WHERE (II_MT.RecodingState = 1)";
			this.sqlSelectCommand1.Connection = this.sqlConnection1;
			// 
			// sqlConnection1
			//
			this.sqlConnection1.ConnectionString = "workstation id=\"EZSYS-ERP01\";packet size=4096;user id=sa;data source=\"220.66.115.9\";persist " +
				"security info=True;initial catalog=Sindong_ERP;password=\"ezsys#0509\"";
			//this.sqlConnection1.ConnectionString = "workstation id=FREESRV05;packet size=4096;user id=sa;data source=\"220.66.115.10\";" +
			//	"persist security info=False;password=\"ezsys#0509\";initial catalog=Shindong_ERP";
			// 
			// dsPlanQuantityResult1
			// 
			this.dsPlanQuantityResult1.DataSetName = "dsPlanQuantityResult";
			this.dsPlanQuantityResult1.Locale = new System.Globalization.CultureInfo("ko-KR");
			this.Load += new System.EventHandler(this.Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.dsPlanQuantityResult1)).EndInit();

		}
		#endregion

		private DataSet search()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = @"Select isnull(SmallClassificationName,'') as ItemState, II_MT.ItemName, II_MT.ItemNum, Quantity, DeliveryDate, ProgressCondition,ItemGroupIndex
							From ItemGroup_HT 
							inner join II_MT on ItemGroup_HT.ItemGroup = II_MT.ItemNum 
							left outer join PUC_MT on II_MT.ItemState = PUC_MT.SmallClassificationCode
							Where II_MT.RecodingState = 1 and ProgressCondition = '대기' 
							and II_MT.ItemNum Like @ItemNum and II_MT.ItemName Like @ItemName 
							and II_MT.ItemDrawNum Like @ItemDrawNum and ItemClassification1 like @Classification1";
			SqlDataAdapter da = new SqlDataAdapter(comm);
			comm.Parameters.Add("@ItemNum",'%'+ItemSearchControl1.ItemNum+"%");
			comm.Parameters.Add("@ItemDrawNum",'%'+ItemSearchControl1.ItemDrawNum+"%");
			comm.Parameters.Add("@ItemName",'%'+ItemSearchControl1.ItemName+"%");
			comm.Parameters.Add("@Classification1",'%'+ddlItemClassification1.SelectedItem.Value.Trim()+"%");
			DataSet ds = new DataSet();
			da.Fill(ds);

			return ds;			
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			UltraWebGrid1.DataSource = search();
			UltraWebGrid1.DataBind();

			Session["ItemGroupUWG"] = UltraWebGrid1;
		}

		//수립버튼
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			conn.Open();
			string str = "";

			DataSet ds = new DataSet();
			ds = search();

			//TempPP_HTGroup테이블 삭제
			str = "SPDeleteTempPP_HTGroup";
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.ExecuteNonQuery();

			if(ds.Tables[0].Rows.Count != 0)
			{
				foreach(DataRow dr in ds.Tables[0].Rows)
				{
					str = "SPItemGroupBOM";
					comm.CommandText = str;
					comm.CommandType = CommandType.StoredProcedure;
					comm.Parameters.Add("@ItemGroup",dr["ItemNum"].ToString());
					comm.Parameters.Add("@Quantity",decimal.Parse(dr["Quantity"].ToString()));
					comm.Parameters.Add("@Date",DateTime.Parse(dr["DeliveryDate"].ToString()).ToShortDateString());
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
				}

				str = "SPItemGroupAdd";
				comm.CommandText = str;
				comm.CommandType = CommandType.StoredProcedure;
				comm.ExecuteNonQuery();
		
			}

			str = "SPProdecutionPlanResult";
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			DataSet dataset = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(comm);
			da.Fill(dataset);

			foreach(DataRow dr in dataset.Tables[0].Rows)
			{					
				DataRow row = dsPlanQuantityResult1.Tables["TempGroupViewPP_HT"].NewRow();

				string  Classification = PropertyClassification(dr["ItemNum"].ToString());
				if(Classification == "제품")
				{
					row["Type"] = Type(dr["ItemNum"].ToString());
				}
				row["PropertyClassification"] = Classification;
				row["ItemNum"] = dr["ItemNum"].ToString();
				row["ItemName"] = ItemName(dr["ItemNum"].ToString());
				row["OrderRemainQuantity"]  = decimal.Parse(dr["OrderRemainQuantity"].ToString());		//수주잔량
				row["PlanRemainQuantity"] = decimal.Parse(dr["PlanRemainQuantity"].ToString());			//생산계획잔
				row["InsufficiencyQuantity"] = decimal.Parse(dr["OrderRemainQuantity"].ToString())-decimal.Parse(dr["PlanRemainQuantity"].ToString());	//과부족량
				row["StockQuantity"] = StockQuantity(dr["ItemNum"].ToString());							//현재고
				row["ThisPlanQuantity"] = decimal.Parse(dr["ThisPlanQuantity"].ToString());		//금번계획량
				
				// 과부족량 + 금번계획량
				decimal NeedQuantity = decimal.Parse(row["InsufficiencyQuantity"].ToString()) + decimal.Parse(row["ThisPlanQuantity"].ToString());
				if(NeedQuantity <= 0)
					row["NeedQuantity"] = 0;		//필요량
				else
					row["NeedQuantity"] = NeedQuantity;		//필요량

				if(dr["ProductionCompleteDate"] == null || dr["ProductionCompleteDate"].ToString().Trim() == "")
					row["ProductionCompleteDate"] = Convert.DBNull;
				else
					row["ProductionCompleteDate"] = DateTime.Parse(dr["ProductionCompleteDate"].ToString()).ToShortDateString();

				
				if(dr["ProductionBeginDate"] == null || dr["ProductionBeginDate"].ToString().Trim() == "")
					row["ProductionBeginDate"] = Convert.DBNull;
				else
					row["ProductionBeginDate"] = DateTime.Parse(dr["ProductionBeginDate"].ToString()).ToShortDateString();

				

				dsPlanQuantityResult1.Tables["TempGroupViewPP_HT"].Rows.Add(row);
			}

			Session["dataSet"] = dsPlanQuantityResult1;

			conn.Close();
			Page.RegisterClientScriptBlock("SEND", "<script>window.open('./PopupWindows/ItemGroup.aspx','','width=910px,height=370px,center=yes,help=no,resizable=no,status=no,menubar=no,toolbar=no');</script>");
		}


		private string Type(string ItemNum)
		{
			string ParentItemNum = "";
			string str = @"Select ParentItemNum From IOI_MT where RecodingState = 1 and ChildItemNum = @ItemNum";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ParentItemNum = dr["ParentItemNum"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();
			conn.Close();

			return ParentItemNum;
		}

		private string PropertyClassification(string ItemNum)
		{
			string Classification = "";
			string str = @"Select PropertyClassification From II_MT where RecodingState = 1 and ItemNum = @ItemNum";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Classification = dr["PropertyClassification"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();
			conn.Close();

			return Classification;
		}


		private string ItemName(string Item)
		{
			string ItemName = "";
			string str = @"Select ItemName From II_MT where RecodingState = 1 and ItemNum = @ItemNum";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",Item);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				ItemName = dr["ItemName"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();
			conn.Close();

			return ItemName;

		}
		private decimal StockQuantity(string Item)
		{

			decimal Quantity = 0;
			string str ="";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;

			if(Division(Item))
			{
				str = @"Select Sum(StockQuantity12) as StockQuantity12 From BS_MT where ItemNum = @ItemNum and RecodingState = 1 and [Year] = @year";
				comm.CommandText = str;
				comm.Parameters.Add("@ItemNum",Item);
				comm.Parameters.Add("@year",DateTime.Now.Year);
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					Quantity = Quantity + decimal.Parse(dr["StockQuantity12"].ToString());
				}
				dr.Close();
				comm.Parameters.Clear();
			}

			str = @"Select StockQuantity12 From PS_MT where ItemNum = @ItemNum and RecodingState = 1 and ProcessSequenceNum = (Select Max(ProcessSequenceNum) From PSI_MT where RecodingState = 1 and ItemNum = @ItemNum  and [Year] = @year)";
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",Item);
			comm.Parameters.Add("@year",DateTime.Now.Year);
			SqlDataReader dr1 = comm.ExecuteReader();
			while(dr1.Read())
			{
				Quantity = Quantity + decimal.Parse(dr1["StockQuantity12"].ToString());
			}
			dr1.Close();
			comm.Parameters.Clear();

			conn.Close();

			return Quantity;
		}

		private bool Division(string ItemNum)
		{
			string PropertyClassification = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = "select PropertyClassification From II_MT where ItemNum = @ItemNum and RecodingState = 1";
			comm.Parameters.Add("@ItemNum",ItemNum);
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				PropertyClassification = dr["PropertyClassification"].ToString();
			}
			dr.Close();
			comm.Parameters.Clear();

			if(PropertyClassification == "제품")
			{
				return true;
			}
			else
				return false;

		}
	}



}
