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
using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP
{
	/// <summary>
	/// Menu1에 대한 요약 설명입니다.
	/// </summary>
	public class Menu1 : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebNavigator.UltraWebTree UltraWebTree1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = 'Login.aspx';</script>");
			}
			if(!Page.IsPostBack)
			{
				//사용자 정보 테이블에서 사용자의 등급을 찾아서 그 등급에 맞는 페이지만을 보여준다.
				SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				con.Open();
				string grade = "";
				string str = "Select UserRank From UI_MT Where RecodingState=1 and ID=@id";
				SqlCommand comm = new SqlCommand(str,con);
				comm.Parameters.Add("@id",SqlDbType.VarChar).Value = Session["ID"].ToString();
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					grade = dr["UserRank"].ToString().Substring(0,2).Trim();
				}
				dr.Close();
				con.Close();

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				string strSQL = "SELECT [Group] as 모듈, EngTitle, Title, Competence"+grade+" FROM CT_T Where Competence"+grade+" = 1 order by Num";
				//string strSQL = "SELECT [Group] as 모듈, EngTitle, Title, Competence"+grade+" FROM CT_T Where Competence"+grade+" = 1";
				SqlDataAdapter adapter = new SqlDataAdapter(strSQL, conn);
				DataSet dsPageAuthority = new DataSet();
				adapter.Fill(dsPageAuthority);

				UltraWebTree1.Nodes.Add("영업관리");
				UltraWebTree1.Nodes.Add("생산관리");
				UltraWebTree1.Nodes.Add("구매/외주관리");
				UltraWebTree1.Nodes.Add("경영정보관리");
				UltraWebTree1.Nodes.Add("통계및지표");
				UltraWebTree1.Nodes.Add("현황관리");
				UltraWebTree1.Nodes.Add("기준정보관리");
				//UltraWebTree1.Nodes.Add("협력사정보관리");
				UltraWebTree1.Nodes.Add("커뮤니티");
				UltraWebTree1.Nodes.Add("시스템정보관리");

				foreach(DataRow drow in dsPageAuthority.Tables[0].Rows)
				{

			
					switch(drow["모듈"].ToString())
					{
						case "영업" :
							UltraWebTree1.Nodes[0].Nodes.Add(drow["Title"].ToString());
							UltraWebTree1.Nodes[0].Nodes[UltraWebTree1.Nodes[0].Nodes.Count - 1].TargetFrame = "Contents";
							UltraWebTree1.Nodes[0].Nodes[UltraWebTree1.Nodes[0].Nodes.Count - 1].TargetUrl = "BusinessManagement/" + drow["EngTitle"].ToString() + ".aspx";
							break;
						case "생산" :
							UltraWebTree1.Nodes[1].Nodes.Add(drow["Title"].ToString());
							UltraWebTree1.Nodes[1].Nodes[UltraWebTree1.Nodes[1].Nodes.Count - 1].TargetFrame = "Contents";
							UltraWebTree1.Nodes[1].Nodes[UltraWebTree1.Nodes[1].Nodes.Count - 1].TargetUrl = "ProductionManagement/" + drow["EngTitle"].ToString() + ".aspx";
							break;
						case "구매/외주" :
							UltraWebTree1.Nodes[2].Nodes.Add(drow["Title"].ToString());
							UltraWebTree1.Nodes[2].Nodes[UltraWebTree1.Nodes[2].Nodes.Count - 1].TargetFrame = "Contents";
							UltraWebTree1.Nodes[2].Nodes[UltraWebTree1.Nodes[2].Nodes.Count - 1].TargetUrl = "BuyingOutside/" + drow["EngTitle"].ToString() + ".aspx";
							break;
						case "경영정보" :
							UltraWebTree1.Nodes[3].Nodes.Add(drow["Title"].ToString());
							UltraWebTree1.Nodes[3].Nodes[UltraWebTree1.Nodes[3].Nodes.Count - 1].TargetFrame = "Contents";
							UltraWebTree1.Nodes[3].Nodes[UltraWebTree1.Nodes[3].Nodes.Count - 1].TargetUrl = "ManagementInfomation/" + drow["EngTitle"].ToString() + ".aspx";
							break;
						case "통계및지표" :
							UltraWebTree1.Nodes[4].Nodes.Add(drow["Title"].ToString());
							UltraWebTree1.Nodes[4].Nodes[UltraWebTree1.Nodes[4].Nodes.Count - 1].TargetFrame = "Contents";
							UltraWebTree1.Nodes[4].Nodes[UltraWebTree1.Nodes[4].Nodes.Count - 1].TargetUrl = "QualityInspection/" + drow["EngTitle"].ToString() + ".aspx";
							break;
						case "현황관리" :
							UltraWebTree1.Nodes[5].Nodes.Add(drow["Title"].ToString());
							UltraWebTree1.Nodes[5].Nodes[UltraWebTree1.Nodes[5].Nodes.Count - 1].TargetFrame = "Contents";
							UltraWebTree1.Nodes[5].Nodes[UltraWebTree1.Nodes[5].Nodes.Count - 1].TargetUrl = "EtcPresentCondition/" + drow["EngTitle"].ToString() + ".aspx";
							break;
						case "기준정보" :
							UltraWebTree1.Nodes[6].Nodes.Add(drow["Title"].ToString());
							UltraWebTree1.Nodes[6].Nodes[UltraWebTree1.Nodes[6].Nodes.Count - 1].TargetFrame = "Contents";
							UltraWebTree1.Nodes[6].Nodes[UltraWebTree1.Nodes[6].Nodes.Count - 1].TargetUrl = "BasisInformation/" + drow["EngTitle"].ToString() + ".aspx";
							break;
//						case "협력사정보" :
//							UltraWebTree1.Nodes[7].Nodes.Add(drow["Title"].ToString());
//							UltraWebTree1.Nodes[7].Nodes[UltraWebTree1.Nodes[7].Nodes.Count - 1].TargetFrame = "Contents";
//							UltraWebTree1.Nodes[7].Nodes[UltraWebTree1.Nodes[7].Nodes.Count - 1].TargetUrl = "CooperationCompany/" + drow["EngTitle"].ToString() + ".aspx";
//							break;
						case "커뮤니티" :
							UltraWebTree1.Nodes[7].Nodes.Add(drow["Title"].ToString());
							UltraWebTree1.Nodes[7].Nodes[UltraWebTree1.Nodes[7].Nodes.Count - 1].TargetFrame = "Contents";
							UltraWebTree1.Nodes[7].Nodes[UltraWebTree1.Nodes[7].Nodes.Count - 1].TargetUrl = "Community/" + drow["EngTitle"].ToString() + ".aspx";
							break;
						case "시스템정보" :
							UltraWebTree1.Nodes[8].Nodes.Add(drow["Title"].ToString());
							UltraWebTree1.Nodes[8].Nodes[UltraWebTree1.Nodes[8].Nodes.Count - 1].TargetFrame = "Contents";
							UltraWebTree1.Nodes[8].Nodes[UltraWebTree1.Nodes[8].Nodes.Count - 1].TargetUrl = "SystemInfoManagement/" + drow["EngTitle"].ToString() + ".aspx";
							break;
						default:
							break;
					}
				}
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
