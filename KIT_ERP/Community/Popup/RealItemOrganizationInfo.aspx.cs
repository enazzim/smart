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


namespace KIT_ERP.Community.Popup
{
	/// <summary>
	/// RealItemOrganizationInfo에 대한 요약 설명입니다.
	/// </summary>
	public class RealItemOrganizationInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		private string MaterialsIndex;
		private string ProductIndex;
		private string DevelopmentIndex;
		protected System.Web.UI.WebControls.Button btSearch;
		protected KIT_ERP.Community.ParentItemControl.ParentItemSearch ISC1;
		protected KIT_ERP.Community.ItemControl.ItemSearch ISC2;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			MaterialsIndex = Request.QueryString["MaterialsIndex"];
			ProductIndex = Request.QueryString["ProductIndex"];
			DevelopmentIndex = Request.QueryString["DevelopmentIndex"];

			ISC1.lbDraw = "모도면번호";
			ISC1.lbNum = "모품목번호";
			ISC1.lbName = "모품목명";
			ISC1.Products = true;
			ISC1.HalfFinishedProducts= true;
			ISC1.Phantom = true;
			ISC2.lbNum = "자품목번호";
			ISC2.lbDraw = "자도면번호";
			ISC2.lbName = "자품목명";
			ISC2.ChildProducts = true;
			ISC2.ChildHalfFinishedProducts= true;
			ISC2.ChildRawMaterials = true;
			

			

			
			if(!IsPostBack)
			{
				Label1.Text = "<진품목구성 정보>";
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand("select count(*) From RIOI_MT where RecodingState = 1",Con);
				Con.Open();
				int RecordCount = int.Parse(Cmd.ExecuteScalar().ToString());
				Con.Close();
				DataGrid1.VirtualItemCount = RecordCount;
				DataGridBind();				
			}
		}


		private void DataGridBind()
		{
			int PageNo, PageCount;

			PageNo = DataGrid1.CurrentPageIndex + 1;
			PageCount = (int)(DataGrid1.VirtualItemCount / DataGrid1.PageSize) + 1;

			int TopCount = PageNo * DataGrid1.PageSize;
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText= @"select Top "+TopCount.ToString()+ @" RealItemOrganizationInfoIndex, 
				ParentItemNum, ChildItemNum, NeedQuantityNumerator, NeedQuantityDenominator 
				FROM RIOI_MT inner join II_MT II_MT1 on RIOI_MT.ParentItemNum = II_MT1.ItemNum 
				inner join II_MT II_MT2 on RIOI_MT.ChildItemNum = II_MT2.ItemNum
				WHERE RIOI_MT.RecodingState = 1 and II_MT1.RecodingState = 1 and II_MT2.RecodingState = 1
				and II_MT1.ItemNum Like @ParentItemNum and II_MT1.ItemDrawNum like @ParentItemDrawNum and II_MT1.ItemName Like @ParentItemName
				and II_MT2.ItemNum Like @ChildItemNum and II_MT2.ItemDrawNum like @ChildItemDrawNum and II_MT2.ItemName Like @ChildItemName
				ORDER BY ParentItemNum,ChildItemNum";
			Cmd.Parameters.Add("@ParentItemNum","%"+ISC1.ItemNum+"%");
			Cmd.Parameters.Add("@ParentItemDrawNum","%"+ISC1.ItemDrawNum+"%");
			Cmd.Parameters.Add("@ParentItemName","%"+ISC1.ItemName+"%");
			Cmd.Parameters.Add("@ChildItemNum","%"+ISC2.ChildItemNum+"%");
			Cmd.Parameters.Add("@ChildItemDrawNum","%"+ISC2.ChildItemDrawNum+"%");
			Cmd.Parameters.Add("@ChildItemName","%"+ISC2.ChildItemName+"%");

			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();

			int StartRecord = DataGrid1.CurrentPageIndex * DataGrid1.PageSize;
			adp.Fill(ds,StartRecord, DataGrid1.PageSize, "RealItemOrganizationInfo");

			adp.Fill(ds);
			DataGrid1.DataSource = ds;
			DataGrid1.DataMember = "RealItemOrganizationInfo";
			DataGrid1.DataBind();
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
			this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
			this.DataGrid1.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// 창닫기 버튼...
		private void Button2_Click(object sender, System.EventArgs e)
		{
			Response.Write("<script>window.close();</script>");
		}



		// 등록버튼...
		private void Button1_Click(object sender, System.EventArgs e)
		{
			bool count = false;    // 체크된 항목이 없을경우 사용하기 위해...
			
			foreach(DataGridItem item in DataGrid1.Items)
			{
				CheckBox cb = ((CheckBox)item.FindControl("CheckBox1"));
				if(cb.Checked)
				{
					count = true;
					string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
					SqlConnection Con = new SqlConnection(ConnectStr);
					Con.Open();
					SqlCommand Cmd = new SqlCommand();
					SqlTransaction trans = Con.BeginTransaction();
					Cmd.Connection = Con;
					Cmd.Transaction = trans;
					
					try
					{
						if(MaterialsIndex != null)
						{
							Cmd.CommandText = "INSERT CR_T VALUES('진품목구성정보', "+ item.Cells[1].Text +", 'T_Materials', "+ MaterialsIndex +")";
							Cmd.ExecuteNonQuery();
							trans.Commit();
							Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../MaterialsContent.aspx?seq=" +MaterialsIndex+ "'; self.close();  </script>");
						}
						else if(ProductIndex != null)
						{
							Cmd.CommandText = "INSERT CR_T VALUES('진품목구성정보', "+ item.Cells[1].Text +", 'T_Product', "+ ProductIndex +")";
							Cmd.ExecuteNonQuery();
							trans.Commit();
							Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../ProductContent.aspx?seq=" +ProductIndex+ "'; self.close();  </script>");
						}
						else
						{
							Cmd.CommandText = "INSERT CR_T VALUES('진품목구성정보', "+ item.Cells[1].Text +", 'T_Development', "+ DevelopmentIndex +")";
							Cmd.ExecuteNonQuery();
							trans.Commit();
							Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../DevelopmentContent.aspx?seq=" +DevelopmentIndex+ "'; self.close();  </script>");
						}
					}
					catch(Exception ex)
					{
						Response.Write(ex.Message);
						trans.Rollback();
					}
					finally
					{
						Con.Close();
					}
				}				
			}
			
			// 체크된 항목이 없을경우...
			if(!count)
				Response.Write("<script>alert('선택된 내용이 없습니다.');</script>");
		}




		// 마우스 오버시 색깔...
		private void DataGrid1_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				e.Item.Attributes["onMouseOver"] = "this.style.backgroundColor = '#e6e6fa'";
				e.Item.Attributes["onMouseOut"] = "this.style.backgroundColor = '#FFFFFF'";
			}
		}

		private void DataGrid1_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			DataGrid1.CurrentPageIndex = e.NewPageIndex;
			DataGridBind();
		}

		private void btSearch_Click(object sender, System.EventArgs e)
		{
			DataGrid1.CurrentPageIndex  = 0;
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = @"select Count(RealItemOrganizationInfoIndex) FROM RIOI_MT inner join II_MT II_MT1 on RIOI_MT.ParentItemNum = II_MT1.ItemNum 
				inner join II_MT II_MT2 on RIOI_MT.ChildItemNum = II_MT2.ItemNum
				WHERE RIOI_MT.RecodingState = 1 and II_MT1.RecodingState = 1 and II_MT2.RecodingState = 1
				and II_MT1.ItemNum Like @ParentItemNum and II_MT1.ItemDrawNum like @ParentItemDrawNum and II_MT1.ItemName Like @ParentItemName
				and II_MT2.ItemNum Like @ChildItemNum and II_MT2.ItemDrawNum like @ChildItemDrawNum and II_MT2.ItemName Like @ChildItemName";
			Cmd.Parameters.Add("@ParentItemNum","%"+ISC1.ItemNum+"%");
			Cmd.Parameters.Add("@ParentItemDrawNum","%"+ISC1.ItemDrawNum+"%");
			Cmd.Parameters.Add("@ParentItemName","%"+ISC1.ItemName+"%");
			Cmd.Parameters.Add("@ChildItemNum","%"+ISC2.ChildItemNum+"%");
			Cmd.Parameters.Add("@ChildItemDrawNum","%"+ISC2.ChildItemDrawNum+"%");
			Cmd.Parameters.Add("@ChildItemName","%"+ISC2.ChildItemName+"%");

	
			
			Con.Open();
			int RecordCount = int.Parse(Cmd.ExecuteScalar().ToString());
			Con.Close();
			DataGrid1.VirtualItemCount = RecordCount;
			DataGridBind();
		}
	}
}
