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

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// UpdateRealItemOrganizationInfoHistory에 대한 요약 설명입니다.
	/// </summary>
	public class UpdateRealItemOrganizationInfoHistory : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label searchTitle;
		protected System.Web.UI.WebControls.Button bt_Search;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected System.Web.UI.WebControls.Label Label2;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
		protected KIT_ERP.BasisInformation.ItemSearch.ItemSearch ItemSearch1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			//ItemSearchControl1.Commodity = true; //상품 바인딩
			//ItemSearchControl1.RawMaterials = true; //원자재 바인딩	
			//ItemSearchControl1.Phantom = true; //팬텀 바인딩	
			ItemSearchControl1.HalfFinishedProducts =  true; //반제품 바인딩	
			ItemSearchControl1.Products = true; //제품 바인딩	
			ItemSearchControl1.IsDoPost = false;


			ItemSearch1.ChildRawMaterials = true; //원자재 바인딩	
			ItemSearch1.ChildHalfFinishedProducts =  true; //반제품 바인딩	
			//ItemSearch1.ChildProducts = true; //제품 바인딩	
			ItemSearch1.ChildIsDoPost = false;
			ItemSearch1.lbChildItemNum.Text = "자품목번호";
			ItemSearchControl1.lbNum = "모품목번호";
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
			this.bt_Search.Click += new System.EventHandler(this.bt_Search_Click);
			this.UltraWebGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.UltraWebGrid1_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void bt_Search_Click(object sender, System.EventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			if(ItemSearchControl1.ItemNum.Trim() == "" && ItemSearchControl1.ItemDrawNum.Trim() == "" && ItemSearchControl1.ItemName.Trim() =="")
			{
				UltraWebGrid1.DataSource = Search("1");
				UltraWebGrid1.DataBind();
			}
			else //if(Validate(ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName))
			{
				UltraWebGrid1.DataSource = Search("0");
				UltraWebGrid1.DataBind();
			}
		}

		private DataSet Search(string a)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			SqlCommand comm = new SqlCommand("SPUpdateReasonRIOI",conn);
			comm.CommandType=CommandType.StoredProcedure;
			comm.Parameters.Add("@ChildItemNum",ItemSearch1.ChildItemNum.Trim());
			comm.Parameters.Add("@ChildItemDrawNum",ItemSearch1.ChildItemDrawNum.Trim());
			comm.Parameters.Add("@ChildItemName",ItemSearch1.ChildItemName.Trim());
			comm.Parameters.Add("@ParentItemNum",ItemSearchControl1.ItemNum.Trim());
			comm.Parameters.Add("@ParentItemDrawNum",ItemSearchControl1.ItemDrawNum.Trim());
			comm.Parameters.Add("@ParentItemName",ItemSearchControl1.ItemName.Trim());
			comm.Parameters.Add("@Division",a);

			SqlDataAdapter da = new SqlDataAdapter(comm);

			DataSet ds = new DataSet();

			da.Fill(ds);

			return ds;
		}

		private void UltraWebGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			if(ItemSearchControl1.ItemNum.Trim() == "" && ItemSearchControl1.ItemDrawNum.Trim() == "" && ItemSearchControl1.ItemName.Trim() =="")
			{
				UltraWebGrid1.DataSource = Search("1");
				UltraWebGrid1.DataBind();
			}
			else //if(Validate(ItemSearchControl1.ItemNum, ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName))
			{
				UltraWebGrid1.DataSource = Search("0");
				UltraWebGrid1.DataBind();
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
					throw new Exception("모품목번호를 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemDrawNum From II_MT Where RecodingState = 1 and ItemDrawNum = @draw";
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = draw;
				comm.CommandText = str;
				SqlDataAdapter da1 = new SqlDataAdapter(comm);
				DataSet ds1 = new DataSet();
				da1.Fill(ds1);

				if(ds1.Tables[0].Rows.Count == 0 )
					throw new Exception("모도면번호를 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemName From II_MT Where RecodingState = 1 and ItemName = @name";
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = name;
				comm.CommandText = str;
				SqlDataAdapter da2 = new SqlDataAdapter(comm);
				DataSet ds2 = new DataSet();
				da2.Fill(ds2);

				if(ds2.Tables[0].Rows.Count == 0 )
					throw new Exception("모품목명을 정확하게 입력해주세요!");

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
					throw new Exception("모품목번호, 모도면번호, 모품목명을 다시 확인하세요.");

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

	}
}
