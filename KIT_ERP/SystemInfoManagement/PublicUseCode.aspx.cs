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
using KIT_ERP.BasisInformation;
using KIT_ERP.BasisInformation.Popup;

namespace KIT_ERP.SystemInfoManagement
{
	/// <summary>
	/// PublicUseCode에 대한 요약 설명입니다.
	/// </summary>
	public class PublicUseCode : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid wg_Small;
		protected System.Web.UI.WebControls.TextBox tb_SmallClassificationName;
		protected System.Web.UI.WebControls.TextBox tb_LargeClassificationName;
		protected System.Web.UI.WebControls.TextBox tb_SmallClassificationCode;
		protected System.Web.UI.WebControls.Button bt_Registration;
		protected System.Web.UI.WebControls.TextBox tb_LargeClassificationCode;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid wg_Large;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_Index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden lb_ReferenceTable;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid2;
		protected System.Web.UI.WebControls.Button bt_Update;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Label Label4;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
//			if(Session["ID"] == null)
//			{	
//				Session.Abandon();
//				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
//			}
			
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(!Page.IsPostBack)
			{
				//Session["ID"] = "admin";
				//Session["UserName"] = "관리자";
				////Response.Write("<script language='javascript'>document.parentWindow.parent.ContentsTitle.location = '../ContentsTitle.aspx?title=∵ 기준정보 > 공용코드정보';</script>");

				bt_Registration.Attributes.Add("onClick", "return OK('입력한 항목을 등록');");
				bt_Update.Attributes.Add("onClick", "return OK('선택한 항목을 수정');");

				page_Load();
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
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.bt_Update.Click += new System.EventHandler(this.bt_Update_Click);
			this.bt_Registration.Click += new System.EventHandler(this.bt_Registration_Click);
			this.wg_Large.SelectedRowsChange += new Infragistics.WebUI.UltraWebGrid.SelectedRowsChangeEventHandler(this.wg_Large_SelectedRowsChange);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Ultrawebgrid2.DblClick += new Infragistics.WebUI.UltraWebGrid.ClickEventHandler(this.Ultrawebgrid2_DblClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	

		private void page_Load()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str = "SELECT DISTINCT LargeClassificationCode, LargeClassificationName From PUC_MT ORDER BY LargeClassificationName";
			SqlCommand comm = new SqlCommand(str,conn);
			SqlDataReader dr = comm.ExecuteReader();
			wg_Large.DataSource = dr;
			wg_Large.DataBind();
			conn.Close();
		}

		/// <summary>
		/// 대분류 선택시 소분류 가져오기
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void wg_Large_SelectedRowsChange(object sender, Infragistics.WebUI.UltraWebGrid.SelectedRowsEventArgs e)
		{
			
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str  = "select SmallClassificationCode, SmallClassificationName,PublicUseCodeIndex from PUC_MT where SmallClassificationName != '' and RecodingState =1 and LargeClassificationCode = @code and SmallClassificationName != @name order by SmallClassificationCode";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = tb_LargeClassificationCode.Text;
			comm.Parameters.Add("@name",SqlDbType.VarChar).Value = " ";
			SqlDataReader dr = comm.ExecuteReader();
			wg_Small.DataSource = dr;
			wg_Small.DataBind();
			conn.Close();

			tb_SmallClassificationCode.Text = "";
			tb_SmallClassificationName.Text = "";
			lb_Index.Value = "0";
		}


		/// <summary>
		/// 등록버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Registration_Click(object sender, System.EventArgs e)
		{
			if(tb_LargeClassificationCode.Text == "")
			{
				RegisterStartupScript("","<script>alert('대분류를 선택해주세요');</script>");
			}
			else if(tb_SmallClassificationName.Text.Trim() == "")
			{
				RegisterStartupScript("","<script>alert('소분류명을 입력해주세요');</script>");
			}
			else if(lb_Index.Value.Trim() != "0")
			{
				RegisterStartupScript("","<script>alert('이미등록한 항목입니다');</script>");
			}
			else
			{

				tb_SmallClassificationCode.Text = "";
				string small = tb_LargeClassificationCode.Text + code();

				ArrayList InputList = new ArrayList();

				InputList.Add(tb_LargeClassificationCode.Text);
				InputList.Add(tb_LargeClassificationName.Text);
				InputList.Add(small);
				InputList.Add(tb_SmallClassificationName.Text);

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD("PublicUseCode", "Registration", "PUC_MT", (int)PageName.PublicUseCodeInfo, Session["ID"].ToString(), InputList,"낱개입력");
				rud.ActivateRUD();

				Data_Bind();
				tb_SmallClassificationCode.Text = small;
			}
		}

		/// <summary>
		/// 소분류 코드 생성
		/// </summary>
		/// <returns></returns>
		private string code()
		{
			string cc="";
			string MaxCode="0000";
			for(int i=0;i< wg_Small.Rows.Count; i++)
			{
				if(wg_Small.Rows[i].Cells.FromKey("SmallClassificationCode").Text == null || wg_Small.Rows[i].Cells.FromKey("SmallClassificationCode").Text.Trim()=="")
				{
					MaxCode = "0000";
				}
				else
				{
					if(int.Parse(wg_Small.Rows[i].Cells.FromKey("SmallClassificationCode").Text.Substring(4,4)) < 9999)
					{
						MaxCode = wg_Small.Rows[i].Cells.FromKey("SmallClassificationCode").Text.Substring(4,4);
					}
				}
			}
			
			if(int.Parse(MaxCode.ToString()) < 9)
			{
				cc= "00" + (int.Parse(MaxCode.ToString()) + 10).ToString();
			}
			else if(int.Parse(MaxCode.ToString()) < 90)
			{
				cc= "00" + (int.Parse(MaxCode.ToString()) + 10).ToString();
			}
			else if(int.Parse(MaxCode.ToString()) < 990)
			{
				cc= "0" + (int.Parse(MaxCode.ToString()) + 10).ToString();
			}
			else
				cc= (int.Parse(MaxCode.ToString()) + 10).ToString();
			return cc.Substring(0,4).ToString();
		}

		private void Data_Bind()
		{
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();

			string str  = "select SmallClassificationCode, SmallClassificationName,PublicUseCodeIndex from PUC_MT where SmallClassificationName != '' and RecodingState =1 and SmallClassificationName != @small and LargeClassificationCode = @code order by SmallClassificationCode";

			SqlCommand comm = new SqlCommand(str,conn);
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = tb_LargeClassificationCode.Text;
			comm.Parameters.Add("@small",SqlDbType.VarChar).Value = "";

			SqlDataReader dr = comm.ExecuteReader();
			
			wg_Small.DataSource = dr;
			wg_Small.DataBind();
			dr.Close();

			str = "Select Max(SmallClassificationCode) from PUC_MT where SmallClassificationName != '' and RecodingState =1 and LargeClassificationCode = @large";
			SqlCommand comm1 = new SqlCommand(str,conn);
			comm1.Parameters.Add("@large",SqlDbType.VarChar).Value = tb_LargeClassificationCode.Text;
			tb_SmallClassificationCode.Text = Convert.ToString(comm1.ExecuteScalar().ToString());
            
			conn.Close();
		}
        


		/// <summary>
		/// 수정버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bt_Update_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value.ToString().Trim() == "" || lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('항목을 선택후 수정해 주세요!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "14000000")
			{
				RegisterStartupScript("","<script>alert('소재공정은 수정할수 없습니다!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "14009999")
			{
				RegisterStartupScript("","<script>alert('최종공정은 수정할수 없습니다!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "07200010")
			{
				RegisterStartupScript("","<script>alert('공구는 수정할수 없습니다!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "07200020")
			{
				RegisterStartupScript("","<script>alert('치구는 수정할수 없습니다!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "03200010")
			{
				RegisterStartupScript("","<script>alert('정상은 수정할수 없습니다!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "03200020")
			{
				RegisterStartupScript("","<script>alert('실행계획은 수정할수 없습니다!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "03200030")
			{
				RegisterStartupScript("","<script>alert('기종생산은 수정할수 없습니다!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "03100010")
			{
				RegisterStartupScript("","<script>alert('정상은 수정할수 없습니다!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "03300010")
			{
				RegisterStartupScript("","<script>alert('정상은 수정할수 없습니다!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "16000000")
			{
				RegisterStartupScript("","<script>alert('초기등록은 수정할수 없습니다!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "16000010")
			{
				RegisterStartupScript("","<script>alert('삭제는 수정할수 없습니다!');</script>");
			}
			else if(tb_SmallClassificationCode.Text.Trim() == "06300010")
			{
				RegisterStartupScript("","<script>alert('양산품코드는 수정할수 없습니다!');</script>");
			}
			else
			{


				ArrayList InputList = new ArrayList();

				InputList.Add(tb_LargeClassificationCode.Text);
				InputList.Add(tb_LargeClassificationName.Text);
				InputList.Add(tb_SmallClassificationCode.Text);
				InputList.Add(tb_SmallClassificationName.Text);
				InputList.Add(lb_Index.Value);

				MasterInfoRecordRUD rud = new MasterInfoRecordRUD("PublicUseCode", "Update", "PUC_MT", (int)PageName.PublicUseCodeInfo, Session["ID"].ToString(), InputList,"낱개입력");
				rud.ActivateRUD();

				lb_Index.Value = "0";

				Data_Bind();
			}
		}

		
		//참고보기 버튼 클릭
		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(lb_Index.Value == "0")
			{
				RegisterStartupScript("","<script>alert('관련 소분류 항목을 선택해 주세요!');</script>");
			}
			else
			{
				Reference rf = new Reference("공정코드정보", int.Parse(lb_Index.Value));
				Ultrawebgrid2.DataSource = rf.reference();
				Ultrawebgrid2.DataBind();
			}
		}

		private void Ultrawebgrid2_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			string table = lb_ReferenceTable.Value;
			string index = lb_ReferenceIndex.Value;

			if(table == "일반자료실")
				table = "CGD_T";
			else if(table =="기술자료실")
				table = "CTD_T";
			else
				table = "CCS_T";
			
			RegisterStartupScript("","<script>window.open('./Popup/View.aspx?table=" + table + "&index=" + index + "','Reference'," + "'width=510" + "," + "height=435px');</script>");
		
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			RegisterStartupScript("","<script language='JavaScript'>window.open('../BasisInformation/Popup/PublicCode.aspx','PublicCode'," + "'width=830" + "," + "height=500px');</script>");
		}

		
	}
}

