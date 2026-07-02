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

using Microsoft.Win32;
using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.ProductionManagement
{
	/// <summary>
	/// RowItemExhaust에 대한 요약 설명입니다.
	/// </summary>
	public class RowItemExhaust : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox tbRowUnit;
		protected System.Web.UI.WebControls.TextBox tbRowStandard;
		protected System.Web.UI.WebControls.TextBox tbExhaustQuantity;
		protected System.Web.UI.WebControls.TextBox tbCreateUnit;
		protected System.Web.UI.WebControls.TextBox tbCreateStandard;
		protected System.Web.UI.WebControls.TextBox tbCreateQuantity;
		protected System.Web.UI.WebControls.Button btnAdd;
		
		protected System.Web.UI.WebControls.LinkButton LinkButton3;
		protected System.Web.UI.WebControls.LinkButton Linkbutton4;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcExhaustDate;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwgE_HT;
		protected KIT_ERP.ProductionManagement.ItemSearch.ItemSearch ItemSearch1;
		protected System.Web.UI.WebControls.TextBox txtUse;
		protected System.Web.UI.WebControls.TextBox txtStartHour;
		protected System.Web.UI.WebControls.TextBox txtStartMinute;
		protected System.Web.UI.WebControls.TextBox txtEndHour;
		protected System.Web.UI.WebControls.TextBox txtEndMinute;
		protected System.Web.UI.WebControls.DropDownList ddlWorker;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			ItemSearchControl1.RawMaterials =  true; //원자재 바인딩
			ItemSearchControl1.IsDoPost = true;
			ItemSearchControl1.lbItemNum.Text = "소진품번";
			ItemSearchControl1.lbItemNum.ForeColor = System.Drawing.Color.Red;



			ItemSearch1.ChildRawMaterials = true; //원자재 바인딩	
			ItemSearch1.ChildIsDoPost = true;
			ItemSearch1.lbChildItemNum.Text = "생성품번";
			ItemSearch1.lbChildItemNum.ForeColor = System.Drawing.Color.Red;

			if(!Page.IsPostBack)
			{
				tbExhaustQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tbExhaustQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");
				
				tbCreateQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Currency(this);");
				tbCreateQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");	
			
				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				

				//	*************************************************
				//	**  작업자 드롭다운리스트... 데이타바인딩... **
				//	*************************************************
				// 사용자정보에서 가지고 옴.
				string str_Worker = "Select Name, ID from UI_MT where RecodingState = 1 and BusinessRegistrationNum = '' and [ID] != 'admin' order by Name";
				SqlCommand comm_Worker = new SqlCommand(str_Worker,conn);
				SqlDataAdapter da_Worker = new SqlDataAdapter(comm_Worker) ;
				DataSet ds_Worker = new DataSet() ;
				da_Worker.Fill(ds_Worker);
				
				ddlWorker.DataSource = ds_Worker;
				ddlWorker.DataTextField = ds_Worker.Tables[0].Columns[0].ToString();
				ddlWorker.DataValueField = ds_Worker.Tables[0].Columns[1].ToString();
				ddlWorker.DataBind();
				ddlWorker.Items.Insert(0, "-선택-") ;
				ddlWorker.Items[0].Value = "";

				
				
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
			this.LinkButton3.Click += new System.EventHandler(this.LinkButton3_Click);
			this.Linkbutton4.Click += new System.EventHandler(this.Linkbutton4_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void LinkButton3_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Unit, Standard From II_MT Where ItemNum = @num and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemSearchControl1.ItemNum;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				tbRowUnit.Text = dr["Unit"].ToString();
				tbRowStandard.Text = dr["Standard"].ToString();
				
			}
			dr.Close();
			
			//단위 입력
			string str1 = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code" ;
			SqlCommand comm1 = new SqlCommand(str1,conn);
			comm1.Parameters.Add("@code",SqlDbType.VarChar).Value = tbRowUnit.Text;
			SqlDataReader dr1 = comm1.ExecuteReader();
			while(dr1.Read())
			{
				tbRowUnit.Text = dr1["SmallClassificationName"].ToString();
			}
			dr1.Close();
			conn.Close();
		}

		private void Linkbutton4_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select Unit, Standard From II_MT Where ItemNum = @num and RecodingState = 1";
			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemSearch1.ChildItemNum;
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				tbCreateUnit.Text = dr["Unit"].ToString();
				tbCreateStandard.Text = dr["Standard"].ToString();
				
			}
			dr.Close();
			
			//단위 입력
			string str1 = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code" ;
			SqlCommand comm1 = new SqlCommand(str1,conn);
			comm1.Parameters.Add("@code",SqlDbType.VarChar).Value = tbCreateUnit.Text;
			SqlDataReader dr1 = comm1.ExecuteReader();
			while(dr1.Read())
			{
				tbCreateUnit.Text = dr1["SmallClassificationName"].ToString();
			}
			dr1.Close();
			conn.Close();
		}

		/// <summary>
		/// 등록버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			
				
				if(Validation() == 1)
				{
					RegisterStartupScript("","<script language='JavaScript'>alert('소진품목을 정확히 입력해 주세요!');</script>");
				}
				else if(Validation() == 2)
				{
					RegisterStartupScript("","<script language='JavaScript'>alert('생성품목을 정확히 입력해 주세요!');</script>");
				}
				else
				{
					if(tbExhaustQuantity.Text.Trim() == "")
					{
						tbExhaustQuantity.Text = "0";
					}
					else if(tbCreateQuantity.Text.Trim() == "" )
					{
						tbCreateQuantity.Text = "0";
					}
					else if(ddlWorker.SelectedIndex == 0)
					{
						RegisterStartupScript("","<script language='JavaScript'>alert('작업자를 선택해 주세요!');</script>");
					}
					else if(wdcExhaustDate.Text.Trim() == "")
					{
						RegisterStartupScript("","<script language='JavaScript'>alert('소진일자를 입력해 주세요!');</script>");
					}
					else
					{
						ArrayList InputList = new ArrayList();
						InputList.Add(ItemSearchControl1.ItemNum.ToString());
						InputList.Add(ItemSearchControl1.ItemDrawNum.ToString());
						InputList.Add(ItemSearchControl1.ItemName.ToString());
						InputList.Add(tbExhaustQuantity.Text);
						InputList.Add(wdcExhaustDate.Text.Trim());
						InputList.Add(ItemSearch1.ChildItemNum.ToString());
						InputList.Add(ItemSearch1.ChildItemDrawNum.ToString());
						InputList.Add(ItemSearch1.ChildItemName.ToString());
						InputList.Add(tbCreateQuantity.Text);
						InputList.Add(txtUse.Text.Trim());
						InputList.Add(txtStartHour.Text + "시 " + txtStartMinute.Text+"분");
						InputList.Add(txtEndHour.Text + "시 " + txtEndMinute.Text+"분");
						InputList.Add(ddlWorker.SelectedItem.Value);
						if(ddlWorker.SelectedIndex == 0)
							InputList.Add("");
						else
							InputList.Add(ddlWorker.SelectedItem.Text);

						Registration rg = new Registration("RowItemExhaust",Session["ID"].ToString(),"",InputList);
						this.uwgE_HT.DataSource = rg.MainTableRegistration();
						this.uwgE_HT.DataBind();
					}
				}
		}

		private int Validation()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();	
			string str = @"Select Count(*) From II_MT where ItemNum= @ItemNum and ItemDrawNum = @ItemDrawNum and ItemName = @ItemName and RecodingState =1";
            SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
			comm.Parameters.Add("@ItemDrawNum",ItemSearchControl1.ItemDrawNum);
			comm.Parameters.Add("@ItemName",ItemSearchControl1.ItemName);
			int count = Int32.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",ItemSearch1.ChildItemNum);
			comm.Parameters.Add("@ItemDrawNum",ItemSearch1.ChildItemDrawNum);
			comm.Parameters.Add("@ItemName",ItemSearch1.ChildItemName);
			int count1 = Int32.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			conn.Close();

			if(count==0)
			{
			
				return 1;
			}
			else if(count1 == 0)
			{
				return 2;
			}
			else
				return 0;
		}
	}
}
