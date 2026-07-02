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

namespace KIT_ERP.BasisInformation.Popup
{
	/// <summary>
	/// RealReversalBOM에 대한 요약 설명입니다.
	/// </summary>
	public class RealReversalBOM : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButton rbNext;
		protected System.Web.UI.WebControls.RadioButton rbEnd;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid2;
		protected System.Web.UI.WebControls.Button Button1;

		protected KIT_ERP.BasisInformation.Popup.ReversalItem.ItemSearch ItemSearch1;

		private string ChildItemNum;
		private string ChildItemDrawNum;
		private string ChildItemName;

		private Stack st = new Stack(); 
		private Stack st1 = new Stack(); 
		public struct St_Item 
		{
			public string Name;
			public decimal Need;
		}
		private DataTable dt = new DataTable();
		private DataColumn dc;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearch1.ChildICommodity = false;
			ItemSearch1.ChildHalfFinishedProducts=true;
			ItemSearch1.ChildProducts = true;
			ItemSearch1.ChildRawMaterials = true;
			ItemSearch1.ChildPhantom = false;
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				UltraWebGrid1.Visible= true;
				UltraWebGrid2.Visible= false;
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			ChildItemNum = ItemSearch1.ChildItemNum;
			ChildItemDrawNum = ItemSearch1.ChildItemDrawNum;
			ChildItemName = ItemSearch1.ChildItemName;
			
			if(Validate(ChildItemNum,ChildItemDrawNum,ChildItemName))
			{
				if(rbEnd.Checked)
				{
					UltraWebGrid1.DataSource = Table_Add(ChildItemNum);
					UltraWebGrid1.DataBind();

					UltraWebGrid1.Visible= true;
					UltraWebGrid2.Visible= false;
				}
				else
				{
					UltraWebGrid1.Visible= false;
					UltraWebGrid2.Visible= true;

					UltraWebGrid2.DataSource = Table_Add1(ChildItemNum);
					UltraWebGrid2.DataBind();
				}
			}
		}
		private bool Validate(string item, string draw, string name)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
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
			
		

		private DataTable Table_Add1(string Item)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "SPRealReversalBOM";
			SqlCommand comm = new SqlCommand();
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Connection = conn;
			comm.Parameters.Add("@ItemNum",Item);
			comm.Parameters.Add("@Bit","0");
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			comm.Parameters.Clear();

			return ds.Tables[0];
		}



		private DataTable Table_Add(string Item)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "SPRealReversalBOM";
			SqlCommand comm = new SqlCommand();
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Connection = conn;
			comm.Parameters.Add("@ItemNum",Item);
			comm.Parameters.Add("@Bit","1");
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			comm.Parameters.Clear();
			
			Stack stack1 = new Stack();
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				St_Item stItem = new St_Item();
				stItem.Name = dr["ParentItemNum"].ToString();
				stItem.Need = decimal.Parse(dr["NeedQuantityNumerator"].ToString())/decimal.Parse(dr["NeedQuantityDenominator"].ToString());
				stack1.Push(stItem);
			}

			dc = new DataColumn("ChildItemNum", typeof(string));//품목번호
			dt.Columns.Add(dc);
			dc = new DataColumn("ChildItemName", typeof(string));//품목명
			dt.Columns.Add(dc);
			dc = new DataColumn("EndItemNum", typeof(string));//품목명
			dt.Columns.Add(dc);
			dc = new DataColumn("EndItemName", typeof(string));//품목명
			dt.Columns.Add(dc);
			dc = new DataColumn("NeedQuantity", typeof(double));//필요량
			dt.Columns.Add(dc);

			//List에서 하나씩 꺼낸다음 해당 품목을 가지고 모품목이 있는지 조사해서 있으면 다시 Array에 넣어둔다.
			if(stack1.Count == 1)
			{
				St_Item st = (St_Item)stack1.Pop();
				DataRow dr = dt.NewRow();
				dr["ChildItemNum"] = ChildItemNum;
				dr["ChildItemName"] = ItemName(ChildItemNum);
				dr["EndItemNum"] = st.Name;;
				dr["EndItemName"] = ItemName(st.Name);
				dr["NeedQuantity"] = st.Need;;
					
				dt.Rows.Add(dr);
			}
			else
			{
				while(stack1.Count > 0)
				{
					St_Item st = (St_Item)stack1.Pop();
					AddItem(st);
				}
			}

			//기존TempTable 레코드 삭제
			string strDel = "Delete From TBRealReversalBOM";
			SqlCommand com = new SqlCommand(strDel,conn);
			conn.Open();
			com.ExecuteNonQuery();
			conn.Close();

			foreach(DataRow dr in dt.Rows)
			{
				str = "SPInsertRealReversalBOM";
				comm.CommandType = CommandType.StoredProcedure;
				comm.CommandText = str;
				comm.Parameters.Add("@ChildItemNum",dr["ChildItemNum"].ToString());
				comm.Parameters.Add("@ChildItemName",dr["ChildItemName"].ToString());
				comm.Parameters.Add("@EndItemNum",dr["EndItemNum"].ToString());
				comm.Parameters.Add("@EndItemName",dr["EndItemName"].ToString());
				comm.Parameters.Add("@NeedQuantity",decimal.Parse(dr["NeedQuantity"].ToString()));
				conn.Open();
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
				conn.Close();
			}

			str = "SPSelectRealReversalBOM";
			comm.CommandType = CommandType.StoredProcedure;
			comm.CommandText = str;
			SqlDataAdapter sqlda = new SqlDataAdapter(comm);
			DataSet sqlds = new DataSet();
			sqlda.Fill(sqlds);

			return sqlds.Tables[0];
			
		}

		private void AddItem(St_Item Item)
		{
			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "SPRealReversalBOM";
			SqlCommand comm = new SqlCommand();
			comm.CommandText = str;
			comm.CommandType = CommandType.StoredProcedure;
			comm.Connection = conn;
			comm.Parameters.Add("@ItemNum",Item.Name);
			comm.Parameters.Add("@Bit","1");
			SqlDataAdapter da = new SqlDataAdapter(comm);
			DataSet ds = new DataSet();
			da.Fill(ds);
			comm.Parameters.Clear();

			Stack stack1 = new Stack();
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				St_Item stItem = new St_Item();
				stItem.Name = dr["ParentItemNum"].ToString();
				stItem.Need = (decimal.Parse(dr["NeedQuantityNumerator"].ToString())/decimal.Parse(dr["NeedQuantityDenominator"].ToString()))*Item.Need;
				stack1.Push(stItem);
			}


			if(stack1.Count == 0)
			{
				DataRow dr = dt.NewRow();
				dr["ChildItemNum"] = ChildItemNum;
				dr["ChildItemName"] = ItemName(ChildItemNum);
				dr["EndItemNum"] = Item.Name;;
				dr["EndItemName"] = ItemName(Item.Name);
				dr["NeedQuantity"] = Item.Need;;
					
				dt.Rows.Add(dr);
			}
			else
			{

				while(stack1.Count > 0)
				{
					St_Item st = (St_Item)stack1.Pop();

					if(ExistParentItem(st.Name))//상위품목이 존재하지 않으면 입력
					{
					
						DataRow dr = dt.NewRow();
						dr["ChildItemNum"] = ChildItemNum;
						dr["ChildItemName"] = ItemName(ChildItemNum);
						dr["EndItemNum"] = st.Name;;
						dr["EndItemName"] = ItemName(st.Name);
						dr["NeedQuantity"] = st.Need;;
					
						dt.Rows.Add(dr);
					}
					else
						AddItem(st);
				}
			}
			
		}

		private bool ExistParentItem(string Num)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select count(*) From IOI_MT where ChildItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",Num);
			conn.Open();
			int count = int.Parse(comm.ExecuteScalar().ToString());

			if(count == 0)
				return true;
			else
				return false;
		}

		private string ItemName(string Num)
		{
			string Name = "";
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			string str = "Select ItemName From II_MT where ItemNum = @ItemNum and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@ItemNum",Num);
			conn.Open();
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Name = dr["ItemName"].ToString();
			}
			conn.Close();

			return Name;
		}

		
	}
}
