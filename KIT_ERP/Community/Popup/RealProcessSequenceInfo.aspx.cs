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

namespace KIT_ERP.Community.Popup
{
	/// <summary>
	/// RealRealProcessSequenceInfo에 대한 요약 설명입니다.
	/// </summary>
	public class RealRealProcessSequenceInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btSearch;
		private string MaterialsIndex;
		private string ProductIndex;
		private string DevelopmentIndex;
		protected KIT_ERP.Community.ItemControl.ItemSearch ISC1;
		
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			MaterialsIndex = Request.QueryString["MaterialsIndex"];
			ProductIndex = Request.QueryString["ProductIndex"];
			DevelopmentIndex = Request.QueryString["DevelopmentIndex"];
			ISC1.ChildProducts = true;
			ISC1.ChildHalfFinishedProducts = true;

			if(!IsPostBack)
			{
				Label1.Text = "<진공정순서 정보>";
				string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
				SqlConnection Con = new SqlConnection(ConnectStr);
				SqlCommand Cmd = new SqlCommand("select count(*) From RPSI_MT where RecodingState = 1",Con);
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
			Cmd.CommandText= @"select Top "+TopCount.ToString()+ @" RPSI_MT.RealProcessSequenceInfoIndex AS RealProcessSequenceInfoIndex,
									RPSI_MT.ItemNum AS ItemNum, 
									II_MT.ItemName AS ItemName,
									RPSI_MT.ProcessSequenceNum AS ProcessSequenceNum, 
									PUC_MT.SmallClassificationName AS SmallClassificationName, 
									RPSI_MT.WorkDistinction AS WorkDistinction
								FROM     RPSI_MT INNER JOIN
									PUC_MT ON RPSI_MT.ProcessCode = PUC_MT.SmallClassificationCode
									Inner join II_MT on RPSI_MT.ItemNum = II_MT.ItemNum
								WHERE   (RPSI_MT.RecodingState = 1) and PUC_MT.RecodingState = 1 and II_MT.RecodingState = 1
									and II_MT.ItemNum Like @ItemNum and II_MT.ItemDrawNum Like @ItemDrawNum and II_MT.ItemName Like @ItemName
									order by ItemNum, ProcessSequenceNum";
			Cmd.Parameters.Add("@ItemNum","%"+ISC1.ChildItemNum+"%");
			Cmd.Parameters.Add("@ItemDrawNum","%"+ISC1.ChildItemDrawNum+"%");
			Cmd.Parameters.Add("@ItemName","%"+ISC1.ChildItemName+"%");

			SqlDataAdapter adp = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();

			int StartRecord = DataGrid1.CurrentPageIndex * DataGrid1.PageSize;
			adp.Fill(ds,StartRecord, DataGrid1.PageSize, "RealProcessSequenceInfo");

			adp.Fill(ds);
			DataGrid1.DataSource = ds;
			DataGrid1.DataMember = "RealProcessSequenceInfo";
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
							Cmd.CommandText = "INSERT CR_T VALUES('진공정순서정보', "+ item.Cells[1].Text +", 'T_Materials', "+ MaterialsIndex +")";
							Cmd.ExecuteNonQuery();
							trans.Commit();
							Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../MaterialsContent.aspx?seq=" +MaterialsIndex+ "'; self.close();  </script>");
						}
						else if(ProductIndex != null)
						{
							Cmd.CommandText = "INSERT CR_T VALUES('진공정순서정보', "+ item.Cells[1].Text +", 'T_Product', "+ ProductIndex +")";
							Cmd.ExecuteNonQuery();
							trans.Commit();
							Response.Write("<script>alert('등록 되었습니다.'); window.opener.location.href='../ProductContent.aspx?seq=" +ProductIndex+ "'; self.close();  </script>");
						}
						else
						{
							Cmd.CommandText = "INSERT CR_T VALUES('진공정순서정보', "+ item.Cells[1].Text +", 'T_Development', "+ DevelopmentIndex +")";
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
			DataGrid1.CurrentPageIndex = 0;
			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Cmd.CommandText = @"select Count(RealProcessSequenceInfoIndex) FROM RPSI_MT inner join II_MT on RPSI_MT.ItemNum = II_MT.ItemNum
				WHERE RPSI_MT.RecodingState = 1 and II_MT.RecodingState = 1
				and II_MT.ItemNum Like @ItemNum and II_MT.ItemDrawNum like @ItemDrawNum and II_MT.ItemName Like @ItemName";
			Cmd.Parameters.Add("@ItemNum","%"+ISC1.ChildItemNum+"%");
			Cmd.Parameters.Add("@ItemDrawNum","%"+ISC1.ChildItemDrawNum+"%");
			Cmd.Parameters.Add("@ItemName","%"+ISC1.ChildItemName+"%");

	
			
			Con.Open();
			int RecordCount = int.Parse(Cmd.ExecuteScalar().ToString());
			Con.Close();
			DataGrid1.VirtualItemCount = RecordCount;
			DataGridBind();
		}
	}
}
