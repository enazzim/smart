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
using Infragistics.WebUI.UltraWebGrid;

using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.BusinessManagement.PopUp
{
	/// <summary>
	/// Tex에 대한 요약 설명입니다.
	/// </summary>
	public class Tex : System.Web.UI.Page
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid WebGrid;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label14;
		protected System.Web.UI.WebControls.Label Label15;
		protected System.Web.UI.WebControls.Label Label16;
		protected System.Web.UI.WebControls.Label Label17;
		protected System.Web.UI.WebControls.Label Label18;
		protected System.Web.UI.WebControls.Label Label19;
		protected System.Web.UI.WebControls.Label Label20;
		protected System.Web.UI.WebControls.Label Label21;
		protected System.Web.UI.WebControls.Label Label22;
		protected System.Web.UI.WebControls.Label Label23;
		protected System.Web.UI.WebControls.Label Label24;
		protected System.Web.UI.WebControls.Label Label25;
		protected System.Web.UI.WebControls.Label Label26;
		protected System.Web.UI.WebControls.Label Label27;
		protected System.Web.UI.WebControls.Label Label28;
		protected System.Web.UI.WebControls.Label Label29;
		protected System.Web.UI.WebControls.Label Label30;
		protected System.Web.UI.WebControls.Label Label31;
		protected System.Web.UI.WebControls.Label Label32;
		protected System.Web.UI.WebControls.Label Label33;
		protected System.Web.UI.WebControls.Label Label34;
		protected System.Web.UI.WebControls.Label Label35;
		protected System.Web.UI.WebControls.Label Label36;
		protected System.Web.UI.WebControls.Label Label37;
		protected System.Web.UI.WebControls.Label Label38;
		protected System.Web.UI.WebControls.Label Label39;
		protected System.Web.UI.WebControls.Label Label40;
		protected System.Web.UI.WebControls.Label Label41;
		protected System.Web.UI.WebControls.Label Label43;
		protected System.Web.UI.WebControls.Label Label44;
		protected System.Web.UI.WebControls.Label Label45;
		protected System.Web.UI.WebControls.Label Label42;
		protected System.Web.UI.WebControls.Label Label46;
		protected System.Web.UI.WebControls.Label Label47;
		protected System.Web.UI.WebControls.Label Label48;
		protected System.Web.UI.WebControls.Label Label49;
		protected System.Web.UI.WebControls.Label Label50;
		protected System.Web.UI.WebControls.Label Label51;
		protected System.Web.UI.WebControls.Label Label52;
		protected System.Web.UI.WebControls.Label Label53;
		protected System.Web.UI.WebControls.Label Label54;
		protected System.Web.UI.WebControls.Label Label55;
		protected System.Web.UI.WebControls.Label Label56;
		protected System.Web.UI.WebControls.Label Label57;
		protected System.Web.UI.WebControls.Label Label58;
		protected System.Web.UI.WebControls.Label Label59;
		protected System.Web.UI.WebControls.Label Label60;
		protected System.Web.UI.WebControls.Label Label61;
		protected System.Web.UI.WebControls.Label Label62;
		protected System.Web.UI.WebControls.Label Label63;
		protected System.Web.UI.WebControls.Label Label64;
		protected System.Web.UI.WebControls.Label Label65;
		protected System.Web.UI.WebControls.Label Label66;
		protected System.Web.UI.WebControls.Label Label67;
		protected System.Web.UI.WebControls.Label Label68;
		protected System.Web.UI.WebControls.Label Label69;
		protected System.Web.UI.WebControls.Label Label70;
		protected System.Web.UI.WebControls.Label Label71;
		protected System.Web.UI.WebControls.Label Label72;
		protected System.Web.UI.WebControls.Label Label73;
		protected System.Web.UI.WebControls.Label Label74;
		protected System.Web.UI.WebControls.Label Label75;
		protected System.Web.UI.WebControls.Label Label76;
		protected System.Web.UI.WebControls.Label Label77;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label79;
		protected System.Web.UI.WebControls.Label Label80;
		protected System.Web.UI.WebControls.Label Label81;
		protected System.Web.UI.WebControls.Label Label82;
		protected System.Web.UI.WebControls.Label Label83;
		protected System.Web.UI.WebControls.Label Label84;
		protected System.Web.UI.WebControls.Label Label85;
		protected System.Web.UI.WebControls.Label Label86;
		protected System.Web.UI.WebControls.Label Label87;
		protected System.Web.UI.WebControls.Label Label88;
		protected System.Web.UI.WebControls.Label Label89;
		protected System.Web.UI.WebControls.Label Label90;
		protected System.Web.UI.WebControls.Label Label91;
		protected System.Web.UI.WebControls.Label Label92;
		protected System.Web.UI.WebControls.Label Label93;
		protected System.Web.UI.WebControls.Label Label94;
		protected System.Web.UI.WebControls.Label Label95;
		protected System.Web.UI.WebControls.Label Label96;
		protected System.Web.UI.WebControls.Label Label97;
		protected System.Web.UI.WebControls.Label Label98;
		protected System.Web.UI.WebControls.Label Label99;
		protected System.Web.UI.WebControls.Label Label100;
		protected System.Web.UI.WebControls.Label Label101;
		protected System.Web.UI.WebControls.Label Label102;
		protected System.Web.UI.WebControls.Label Label103;
		protected System.Web.UI.WebControls.Label Label104;
		protected System.Web.UI.WebControls.Label Label105;
		protected System.Web.UI.WebControls.Label Label106;
		protected System.Web.UI.WebControls.Label Label107;
		protected System.Web.UI.WebControls.Label Label108;
		protected System.Web.UI.WebControls.Label Label109;
		protected System.Web.UI.WebControls.Label Label110;
		protected System.Web.UI.WebControls.Label Label111;
		protected System.Web.UI.WebControls.Label Label112;
		protected System.Web.UI.WebControls.Label Label113;
		protected System.Web.UI.WebControls.Label Label114;
		protected System.Web.UI.WebControls.Label Label115;
		protected System.Web.UI.WebControls.Label Label116;
		protected System.Web.UI.WebControls.Label Label117;
		protected System.Web.UI.WebControls.Label Label118;
		protected System.Web.UI.WebControls.Label Label119;
		protected System.Web.UI.WebControls.Label Label120;
		protected System.Web.UI.WebControls.Label Label121;
		protected System.Web.UI.WebControls.Label Label122;
		protected System.Web.UI.WebControls.Label Label123;
		protected System.Web.UI.WebControls.Label Label124;
		protected System.Web.UI.WebControls.Label Label125;
		protected System.Web.UI.WebControls.Label Label126;
		protected System.Web.UI.WebControls.Label Label127;
		protected System.Web.UI.WebControls.Label Label128;
		protected System.Web.UI.WebControls.Label Label129;
		protected System.Web.UI.WebControls.Label Label130;
		protected System.Web.UI.WebControls.Label Label131;
		protected System.Web.UI.WebControls.Label Label132;
		protected System.Web.UI.WebControls.Label Label133;
		protected System.Web.UI.WebControls.Label Label134;
		protected System.Web.UI.WebControls.Label Label135;
		protected System.Web.UI.WebControls.Label Label136;
		protected System.Web.UI.WebControls.Label Label137;
		protected System.Web.UI.WebControls.Label Label138;
		protected System.Web.UI.WebControls.Label Label139;
		protected System.Web.UI.WebControls.Label Label140;
		protected System.Web.UI.WebControls.Label Label141;
		protected System.Web.UI.WebControls.Label Label142;
		protected System.Web.UI.WebControls.Label Label143;
		protected System.Web.UI.WebControls.Label Label144;
		protected System.Web.UI.WebControls.Label Label145;
		protected System.Web.UI.WebControls.Label Label146;
		protected System.Web.UI.WebControls.Label Label147;
		protected System.Web.UI.WebControls.Label Label148;
		protected System.Web.UI.WebControls.Label Label149;
		protected System.Web.UI.WebControls.Label Label150;
		protected System.Web.UI.WebControls.Label Label151;
		protected System.Web.UI.WebControls.Label Label152;
		protected System.Web.UI.WebControls.Label Label153;
		protected System.Web.UI.WebControls.Label Label154;
		protected System.Web.UI.WebControls.Label Label155;
		protected System.Web.UI.WebControls.Label Label156;
		protected System.Web.UI.WebControls.Label Label157;
		protected System.Web.UI.WebControls.Label Label158;
		protected System.Web.UI.WebControls.Label Label159;
		protected System.Web.UI.WebControls.Label Label160;
		protected System.Web.UI.WebControls.Label Label161;
		protected System.Web.UI.WebControls.Label Label162;
		protected System.Web.UI.WebControls.Label Label163;
		protected System.Web.UI.WebControls.Label Label164;
		protected System.Web.UI.WebControls.Label Label165;
		protected System.Web.UI.WebControls.Label Label166;
		protected System.Web.UI.WebControls.Label Label167;
		protected System.Web.UI.WebControls.Label Label168;
		protected System.Web.UI.WebControls.Label Label169;
		protected System.Web.UI.WebControls.Label Label170;
		protected System.Web.UI.WebControls.Label Label171;
		protected System.Web.UI.WebControls.Label Label172;
		protected System.Web.UI.WebControls.Label Label173;
		protected System.Web.UI.WebControls.Label Label174;
		protected System.Web.UI.WebControls.Label Label175;
		protected System.Web.UI.WebControls.Label Label176;
		protected System.Web.UI.WebControls.Label Label177;
		protected System.Web.UI.WebControls.Label Label178;
		protected System.Web.UI.WebControls.Label Label179;
		protected System.Web.UI.WebControls.Label Label180;
		protected System.Web.UI.WebControls.Label Label181;
		protected System.Web.UI.WebControls.Label Label182;
		protected System.Web.UI.WebControls.Label Label183;
		protected System.Web.UI.WebControls.Label Label184;
		protected System.Web.UI.WebControls.Label Label185;
		protected System.Web.UI.WebControls.Label Label186;
		protected System.Web.UI.WebControls.Label Label187;
		protected System.Web.UI.WebControls.Label Label188;
		protected System.Web.UI.WebControls.Label Label78;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.

			if(!Page.IsPostBack)
			{
				WebGrid = (Infragistics.WebUI.UltraWebGrid.UltraWebGrid)Session["UWG"];

				int Cost = 0;
				//int TaxCost = int.Parse(WebGrid.Rows[0].Cells.FromKey("SupplementaryValueTaxRate").Value.ToString());
				foreach(Infragistics.WebUI.UltraWebGrid.UltraGridRow row in WebGrid.Rows)
				{
					Cost += Convert.ToInt32(decimal.Parse(row.Cells.FromKey("TotalCost").Text));
				}

				//사업자관련
				SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
				con.Open();
				string str = "Select * From UCI_T";
				SqlCommand comm = new SqlCommand(str,con);
				SqlDataReader dr = comm.ExecuteReader();
				while(dr.Read())
				{
					Label106.Text = dr["BusinessCompanyNum"].ToString().Substring(0,1).ToString().Trim();
					Label107.Text = dr["BusinessCompanyNum"].ToString().Substring(1,1).ToString().Trim();
					Label108.Text = dr["BusinessCompanyNum"].ToString().Substring(2,1).ToString().Trim();
					Label109.Text = dr["BusinessCompanyNum"].ToString().Substring(4,1).ToString().Trim();
					Label110.Text = dr["BusinessCompanyNum"].ToString().Substring(5,1).ToString().Trim();
					Label111.Text = dr["BusinessCompanyNum"].ToString().Substring(7,1).ToString().Trim();
					Label112.Text = dr["BusinessCompanyNum"].ToString().Substring(8,1).ToString().Trim();
					Label113.Text = dr["BusinessCompanyNum"].ToString().Substring(9,1).ToString().Trim();
					Label114.Text = dr["BusinessCompanyNum"].ToString().Substring(10,1).ToString().Trim();
					Label115.Text = dr["BusinessCompanyNum"].ToString().Substring(11,1).ToString().Trim();

					Label126.Text = dr["BusinessCompanyNum"].ToString().Substring(0,1).ToString().Trim();
					Label127.Text = dr["BusinessCompanyNum"].ToString().Substring(1,1).ToString().Trim();
					Label128.Text = dr["BusinessCompanyNum"].ToString().Substring(2,1).ToString().Trim();
					Label129.Text = dr["BusinessCompanyNum"].ToString().Substring(4,1).ToString().Trim();
					Label130.Text = dr["BusinessCompanyNum"].ToString().Substring(5,1).ToString().Trim();
					Label131.Text = dr["BusinessCompanyNum"].ToString().Substring(7,1).ToString().Trim();
					Label132.Text = dr["BusinessCompanyNum"].ToString().Substring(8,1).ToString().Trim();
					Label133.Text = dr["BusinessCompanyNum"].ToString().Substring(9,1).ToString().Trim();
					Label134.Text = dr["BusinessCompanyNum"].ToString().Substring(10,1).ToString().Trim();
					Label135.Text = dr["BusinessCompanyNum"].ToString().Substring(11,1).ToString().Trim();

					Label1.Text = dr["CompanyName"].ToString().Trim();
					Label2.Text = dr["PresidentName"].ToString().Trim();
					Label3.Text = dr["TaxBillAddress"].ToString().Substring(6).Trim();
					Label4.Text = dr["BusinessClassification"].ToString().Trim();
					Label5.Text = dr["BusinessItem"].ToString().Trim();

					Label28.Text = dr["CompanyName"].ToString().Trim();
					Label29.Text = dr["PresidentName"].ToString().Trim();
					Label30.Text = dr["TaxBillAddress"].ToString().Substring(6).Trim();
					Label31.Text = dr["BusinessClassification"].ToString().Trim();
					Label32.Text = dr["BusinessItem"].ToString().Trim();
				}
				dr.Close();
				con.Close();


				//년-월-일
				Label80.Text = DateTime.Now.Year.ToString();
				Label146.Text = DateTime.Now.Year.ToString();

				if(DateTime.Now.Month.ToString().Length == 1)
				{
					Label81.Text = "0";
					Label82.Text = DateTime.Now.Month.ToString();

					Label147.Text = "0";
					Label148.Text = DateTime.Now.Month.ToString();
				}
				else
				{
					Label81.Text = DateTime.Now.Month.ToString().Substring(0,1);
					Label82.Text = DateTime.Now.Month.ToString().Substring(1,1);

					Label147.Text = DateTime.Now.Month.ToString().Substring(0,1);
					Label148.Text = DateTime.Now.Month.ToString().Substring(1,1);

				}
				if(DateTime.Now.Day.ToString().Length == 1)
				{
					Label83.Text = "0";
					Label84.Text = DateTime.Now.Day.ToString();

					Label149.Text = "0";
					Label150.Text = DateTime.Now.Day.ToString();
				}
				else
				{
					Label83.Text = DateTime.Now.Day.ToString().Substring(0,1);
					Label84.Text = DateTime.Now.Day.ToString().Substring(1,1);

					Label149.Text = DateTime.Now.Day.ToString().Substring(0,1);
					Label150.Text = DateTime.Now.Day.ToString().Substring(1,1);
				}

				//품목
				if(WebGrid.Rows.Count == 1)
				{
					Label39.Text = WebGrid.Rows[0].Cells.FromKey("ItemName").Text;
					Label42.Text = WebGrid.Rows[0].Cells.FromKey("ItemName").Text;
				}
				else
				{
					Label39.Text = WebGrid.Rows[0].Cells.FromKey("ItemName").Text + " 외 "+ (WebGrid.Rows.Count-1);
					Label42.Text = WebGrid.Rows[0].Cells.FromKey("ItemName").Text + " 외 "+ (WebGrid.Rows.Count-1);
				}

				//공급가액
				Label20.Text = Cost.ToString();
				Label54.Text = Cost.ToString();
				

				//세액
				Label24.Text = Convert.ToString((Cost/10));
				Label58.Text = Convert.ToString((Cost/10));

				//합계금액
				Label11.Text = Convert.ToString(Cost + (Cost/10));
				Label38.Text = Convert.ToString(Cost + (Cost/10));


				//거래처등록번호
				Label116.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(0,1).ToString().Trim();
				Label117.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(1,1).ToString().Trim();
				Label118.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(2,1).ToString().Trim();
				Label119.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(4,1).ToString().Trim();
				Label120.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(5,1).ToString().Trim();
				Label121.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(7,1).ToString().Trim();
				Label122.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(8,1).ToString().Trim();
				Label123.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(9,1).ToString().Trim();
				Label124.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(10,1).ToString().Trim();
				Label125.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(11,1).ToString().Trim();

				Label136.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(0,1).ToString().Trim();
				Label137.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(1,1).ToString().Trim();
				Label138.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(2,1).ToString().Trim();
				Label139.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(4,1).ToString().Trim();
				Label140.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(5,1).ToString().Trim();
				Label141.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(7,1).ToString().Trim();
				Label142.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(8,1).ToString().Trim();
				Label143.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(9,1).ToString().Trim();
				Label144.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(10,1).ToString().Trim();
				Label145.Text = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value.ToString().Substring(11,1).ToString().Trim();

				//거래처명
				Label6.Text = WebGrid.Rows[0].Cells.FromKey("CompanyName").Value.ToString();
				Label36.Text = WebGrid.Rows[0].Cells.FromKey("CompanyName").Value.ToString();

				SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
				conn.Open();
				str = "Select * From CI_MT Where BusinessRegistrationNum = @num and RecodingState = 1";
				SqlCommand comm1 = new SqlCommand(str,conn);
				comm1.Parameters.Add("@num",SqlDbType.VarChar).Value = WebGrid.Rows[0].Cells.FromKey("BusinessRegistrationNum").Value;
				SqlDataReader dr1 = comm1.ExecuteReader();
				while(dr1.Read())
				{
					//대표자명
					Label7.Text = dr1["PresidentName"].ToString();
					Label37.Text = dr1["PresidentName"].ToString();

					//주소
					Label8.Text = dr1["BusinessCompanyAddress"].ToString();
					Label35.Text = dr1["BusinessCompanyAddress"].ToString();

					//업태
					Label9.Text = dr1["BusinessClassification"].ToString();
					Label33.Text = dr1["BusinessClassification"].ToString();

					//업종
					Label10.Text = dr1["BusinessItem"].ToString();
					Label34.Text = dr1["BusinessItem"].ToString();
				}			  
				dr1.Close();
				conn.Close();

				//금액
				int count = Cost.ToString().Length;
				

				Label181.Text = DateTime.Now.Month.ToString();
				Label182.Text = DateTime.Now.Day.ToString();

				Label173.Text = DateTime.Now.Month.ToString();
				Label174.Text = DateTime.Now.Day.ToString();

				int Tex = Convert.ToInt32(Cost/10);
				if(count == 1)
				{
					Label85.Text = "10";
					Label96.Text = Cost.ToString(); 

					Label151.Text = "10";
					Label162.Text = Cost.ToString();

				}
				else if(count == 2)
				{
					Label85.Text = "9";
					Label95.Text = Cost.ToString().Substring(0,1); 
					Label96.Text = Cost.ToString().Substring(1,1); 

					Label79.Text = Tex.ToString();

					Label151.Text = "9";
					Label161.Text = Cost.ToString().Substring(0,1); 
					Label162.Text = Cost.ToString().Substring(1,1); 

					Label172.Text = Tex.ToString();
				}
				else if(count == 3)
				{
					Label85.Text = "8";
					Label94.Text = Cost.ToString().Substring(0,1); 
					Label95.Text = Cost.ToString().Substring(1,1); 
					Label96.Text = Cost.ToString().Substring(2,1); 

					Label105.Text = Tex.ToString().Substring(0,1);
					Label79.Text = Tex.ToString().Substring(1,1);

					Label151.Text = "8";
					Label160.Text = Cost.ToString().Substring(0,1); 
					Label161.Text = Cost.ToString().Substring(1,1); 
					Label162.Text = Cost.ToString().Substring(2,1); 

					Label171.Text = Tex.ToString().Substring(0,1);
					Label172.Text = Tex.ToString().Substring(1,1);
				}
				else if(count == 4)
				{
					Label85.Text = "7";
					Label93.Text = Cost.ToString().Substring(0,1); 
					Label94.Text = Cost.ToString().Substring(1,1); 
					Label95.Text = Cost.ToString().Substring(2,1); 
					Label96.Text = Cost.ToString().Substring(3,1); 

					Label104.Text = Tex.ToString().Substring(0,1);
					Label105.Text = Tex.ToString().Substring(1,1);
					Label79.Text = Tex.ToString().Substring(2,1);

					Label151.Text = "7";
					Label159.Text = Cost.ToString().Substring(0,1);
					Label160.Text = Cost.ToString().Substring(1,1); 
					Label161.Text = Cost.ToString().Substring(2,1); 
					Label162.Text = Cost.ToString().Substring(3,1); 

					Label170.Text = Tex.ToString().Substring(0,1);
					Label171.Text = Tex.ToString().Substring(1,1);
					Label172.Text = Tex.ToString().Substring(2,1);
				}
				else if(count == 5)
				{
					Label85.Text = "6";
					Label92.Text = Cost.ToString().Substring(0,1); 
					Label93.Text = Cost.ToString().Substring(1,1); 
					Label94.Text = Cost.ToString().Substring(2,1); 
					Label95.Text = Cost.ToString().Substring(3,1); 
					Label96.Text = Cost.ToString().Substring(4,1);

					Label103.Text = Tex.ToString().Substring(0,1);
					Label104.Text = Tex.ToString().Substring(1,1);
					Label105.Text = Tex.ToString().Substring(2,1);
					Label79.Text = Tex.ToString().Substring(3,1);
 
					Label151.Text = "6";
					Label158.Text = Cost.ToString().Substring(0,1);
					Label159.Text = Cost.ToString().Substring(1,1);
					Label160.Text = Cost.ToString().Substring(2,1); 
					Label161.Text = Cost.ToString().Substring(3,1); 
					Label162.Text = Cost.ToString().Substring(4,1); 

					Label169.Text = Tex.ToString().Substring(0,1);
					Label170.Text = Tex.ToString().Substring(1,1);
					Label171.Text = Tex.ToString().Substring(2,1);
					Label172.Text = Tex.ToString().Substring(3,1);
				}
				else if(count == 6)
				{
					Label85.Text = "5";
					Label91.Text = Cost.ToString().Substring(0,1); 
					Label92.Text = Cost.ToString().Substring(1,1); 
					Label93.Text = Cost.ToString().Substring(2,1); 
					Label94.Text = Cost.ToString().Substring(3,1); 
					Label95.Text = Cost.ToString().Substring(4,1); 
					Label96.Text = Cost.ToString().Substring(5,1); 

					Label102.Text = Tex.ToString().Substring(0,1);
					Label103.Text = Tex.ToString().Substring(1,1);
					Label104.Text = Tex.ToString().Substring(2,1);
					Label105.Text = Tex.ToString().Substring(3,1);
					Label79.Text = Tex.ToString().Substring(4,1);

					Label151.Text = "5";
					Label157.Text = Cost.ToString().Substring(0,1);
					Label158.Text = Cost.ToString().Substring(1,1);
					Label159.Text = Cost.ToString().Substring(2,1);
					Label160.Text = Cost.ToString().Substring(3,1); 
					Label161.Text = Cost.ToString().Substring(4,1); 
					Label162.Text = Cost.ToString().Substring(5,1); 

					Label168.Text = Tex.ToString().Substring(0,1);
					Label169.Text = Tex.ToString().Substring(1,1);
					Label170.Text = Tex.ToString().Substring(2,1);
					Label171.Text = Tex.ToString().Substring(3,1);
					Label172.Text = Tex.ToString().Substring(4,1);
				}
				else if(count == 7)
				{
					Label85.Text = "4";
					Label90.Text = Cost.ToString().Substring(0,1); 
					Label91.Text = Cost.ToString().Substring(1,1); 
					Label92.Text = Cost.ToString().Substring(2,1); 
					Label93.Text = Cost.ToString().Substring(3,1); 
					Label94.Text = Cost.ToString().Substring(4,1); 
					Label95.Text = Cost.ToString().Substring(5,1); 
					Label96.Text = Cost.ToString().Substring(6,1); 

					Label101.Text = Tex.ToString().Substring(0,1);
					Label102.Text = Tex.ToString().Substring(1,1);
					Label103.Text = Tex.ToString().Substring(2,1);
					Label104.Text = Tex.ToString().Substring(3,1);
					Label105.Text = Tex.ToString().Substring(4,1);
					Label79.Text = Tex.ToString().Substring(5,1);

					Label151.Text = "4";
					Label156.Text = Cost.ToString().Substring(0,1);
					Label157.Text = Cost.ToString().Substring(1,1);
					Label158.Text = Cost.ToString().Substring(2,1);
					Label159.Text = Cost.ToString().Substring(3,1);
					Label160.Text = Cost.ToString().Substring(4,1); 
					Label161.Text = Cost.ToString().Substring(5,1); 
					Label162.Text = Cost.ToString().Substring(6,1);
 
					Label167.Text = Tex.ToString().Substring(0,1);
					Label168.Text = Tex.ToString().Substring(1,1);
					Label169.Text = Tex.ToString().Substring(2,1);
					Label170.Text = Tex.ToString().Substring(3,1);
					Label171.Text = Tex.ToString().Substring(4,1);
					Label172.Text = Tex.ToString().Substring(5,1);
				}
				else if(count == 8)
				{
					Label85.Text = "3";
					Label89.Text = Cost.ToString().Substring(0,1); 
					Label90.Text = Cost.ToString().Substring(1,1); 
					Label91.Text = Cost.ToString().Substring(2,1); 
					Label92.Text = Cost.ToString().Substring(3,1); 
					Label93.Text = Cost.ToString().Substring(4,1); 
					Label94.Text = Cost.ToString().Substring(5,1); 
					Label95.Text = Cost.ToString().Substring(6,1); 
					Label96.Text = Cost.ToString().Substring(7,1);  

					Label100.Text = Tex.ToString().Substring(0,1);
					Label101.Text = Tex.ToString().Substring(1,1);
					Label102.Text = Tex.ToString().Substring(2,1);
					Label103.Text = Tex.ToString().Substring(3,1);
					Label104.Text = Tex.ToString().Substring(4,1);
					Label105.Text = Tex.ToString().Substring(5,1);
					Label79.Text = Tex.ToString().Substring(6,1);


					Label151.Text = "3";
					Label155.Text = Cost.ToString().Substring(0,1);
					Label156.Text = Cost.ToString().Substring(1,1);
					Label157.Text = Cost.ToString().Substring(2,1);
					Label158.Text = Cost.ToString().Substring(3,1);
					Label159.Text = Cost.ToString().Substring(4,1);
					Label160.Text = Cost.ToString().Substring(5,1); 
					Label161.Text = Cost.ToString().Substring(6,1); 
					Label162.Text = Cost.ToString().Substring(7,1); 

					Label166.Text = Tex.ToString().Substring(0,1);
					Label167.Text = Tex.ToString().Substring(1,1);
					Label168.Text = Tex.ToString().Substring(2,1);
					Label169.Text = Tex.ToString().Substring(3,1);
					Label170.Text = Tex.ToString().Substring(4,1);
					Label171.Text = Tex.ToString().Substring(5,1);
					Label172.Text = Tex.ToString().Substring(6,1);
				}			
				else if(count == 9)
				{
					Label85.Text = "2";
					Label88.Text = Cost.ToString().Substring(0,1); 
					Label89.Text = Cost.ToString().Substring(1,1); 
					Label90.Text = Cost.ToString().Substring(2,1); 
					Label91.Text = Cost.ToString().Substring(3,1); 
					Label92.Text = Cost.ToString().Substring(4,1); 
					Label93.Text = Cost.ToString().Substring(5,1); 
					Label94.Text = Cost.ToString().Substring(6,1); 
					Label95.Text = Cost.ToString().Substring(7,1); 
					Label96.Text = Cost.ToString().Substring(8,1); 

					Label99.Text = Tex.ToString().Substring(0,1);
					Label100.Text = Tex.ToString().Substring(1,1);
					Label101.Text = Tex.ToString().Substring(2,1);
					Label102.Text = Tex.ToString().Substring(3,1);
					Label103.Text = Tex.ToString().Substring(4,1);
					Label104.Text = Tex.ToString().Substring(5,1);
					Label105.Text = Tex.ToString().Substring(6,1);
					Label79.Text = Tex.ToString().Substring(7,1);

 
					Label151.Text = "2";
					Label154.Text = Cost.ToString().Substring(0,1);
					Label155.Text = Cost.ToString().Substring(1,1);
					Label156.Text = Cost.ToString().Substring(2,1);
					Label157.Text = Cost.ToString().Substring(3,1);
					Label158.Text = Cost.ToString().Substring(4,1);
					Label159.Text = Cost.ToString().Substring(5,1);
					Label160.Text = Cost.ToString().Substring(6,1); 
					Label161.Text = Cost.ToString().Substring(7,1); 
					Label162.Text = Cost.ToString().Substring(8,1); 

					Label165.Text = Tex.ToString().Substring(0,1);
					Label166.Text = Tex.ToString().Substring(1,1);
					Label167.Text = Tex.ToString().Substring(2,1);
					Label168.Text = Tex.ToString().Substring(3,1);
					Label169.Text = Tex.ToString().Substring(4,1);
					Label170.Text = Tex.ToString().Substring(5,1);
					Label171.Text = Tex.ToString().Substring(6,1);
					Label172.Text = Tex.ToString().Substring(7,1);
				}
				else if(count == 10)
				{
					Label85.Text = "1";
					Label87.Text = Cost.ToString().Substring(0,1); 
					Label88.Text = Cost.ToString().Substring(1,1); 
					Label89.Text = Cost.ToString().Substring(2,1); 
					Label90.Text = Cost.ToString().Substring(3,1); 
					Label91.Text = Cost.ToString().Substring(4,1); 
					Label92.Text = Cost.ToString().Substring(5,1); 
					Label93.Text = Cost.ToString().Substring(6,1); 
					Label94.Text = Cost.ToString().Substring(7,1); 
					Label95.Text = Cost.ToString().Substring(8,1); 
					Label96.Text = Cost.ToString().Substring(9,1);  

					Label98.Text = Tex.ToString().Substring(0,1);
					Label99.Text = Tex.ToString().Substring(1,1);
					Label100.Text = Tex.ToString().Substring(2,1);
					Label101.Text = Tex.ToString().Substring(3,1);
					Label102.Text = Tex.ToString().Substring(4,1);
					Label103.Text = Tex.ToString().Substring(5,1);
					Label104.Text = Tex.ToString().Substring(6,1);
					Label105.Text = Tex.ToString().Substring(7,1);
					Label79.Text = Tex.ToString().Substring(8,1);


					Label151.Text = "1";
					Label153.Text = Cost.ToString().Substring(0,1);
					Label154.Text = Cost.ToString().Substring(1,1);
					Label155.Text = Cost.ToString().Substring(2,1);
					Label156.Text = Cost.ToString().Substring(3,1);
					Label157.Text = Cost.ToString().Substring(4,1);
					Label158.Text = Cost.ToString().Substring(5,1);
					Label159.Text = Cost.ToString().Substring(6,1);
					Label160.Text = Cost.ToString().Substring(7,1); 
					Label161.Text = Cost.ToString().Substring(8,1); 
					Label162.Text = Cost.ToString().Substring(9,1); 

					Label164.Text = Tex.ToString().Substring(0,1);
					Label165.Text = Tex.ToString().Substring(1,1);
					Label166.Text = Tex.ToString().Substring(2,1);
					Label167.Text = Tex.ToString().Substring(3,1);
					Label168.Text = Tex.ToString().Substring(4,1);
					Label169.Text = Tex.ToString().Substring(5,1);
					Label170.Text = Tex.ToString().Substring(6,1);
					Label171.Text = Tex.ToString().Substring(7,1);
					Label172.Text = Tex.ToString().Substring(8,1);
				}
				else if(count == 11)
				{
					Label85.Text = "0";
					Label86.Text = Cost.ToString().Substring(0,1); 
					Label87.Text = Cost.ToString().Substring(1,1); 
					Label88.Text = Cost.ToString().Substring(2,1); 
					Label89.Text = Cost.ToString().Substring(3,1); 
					Label90.Text = Cost.ToString().Substring(4,1); 
					Label91.Text = Cost.ToString().Substring(5,1); 
					Label92.Text = Cost.ToString().Substring(6,1); 
					Label93.Text = Cost.ToString().Substring(7,1); 
					Label94.Text = Cost.ToString().Substring(8,1); 
					Label95.Text = Cost.ToString().Substring(8,1); 
					Label96.Text = Cost.ToString().Substring(10,1);  

					Label97.Text = Tex.ToString().Substring(0,1);
					Label98.Text = Tex.ToString().Substring(1,1);
					Label99.Text = Tex.ToString().Substring(2,1);
					Label100.Text = Tex.ToString().Substring(3,1);
					Label101.Text = Tex.ToString().Substring(4,1);
					Label102.Text = Tex.ToString().Substring(5,1);
					Label103.Text = Tex.ToString().Substring(6,1);
					Label104.Text = Tex.ToString().Substring(7,1);
					Label105.Text = Tex.ToString().Substring(8,1);
					Label79.Text = Tex.ToString().Substring(9,1);

					Label151.Text = "0";
					Label152.Text = Cost.ToString().Substring(0,1);
					Label153.Text = Cost.ToString().Substring(1,1);
					Label154.Text = Cost.ToString().Substring(2,1);
					Label155.Text = Cost.ToString().Substring(3,1);
					Label156.Text = Cost.ToString().Substring(4,1);
					Label157.Text = Cost.ToString().Substring(5,1);
					Label158.Text = Cost.ToString().Substring(6,1);
					Label159.Text = Cost.ToString().Substring(7,1);
					Label160.Text = Cost.ToString().Substring(8,1); 
					Label161.Text = Cost.ToString().Substring(9,1); 
					Label162.Text = Cost.ToString().Substring(10,1); 

					Label163.Text = Tex.ToString().Substring(0,1);
					Label164.Text = Tex.ToString().Substring(1,1);
					Label165.Text = Tex.ToString().Substring(2,1);
					Label166.Text = Tex.ToString().Substring(3,1);
					Label167.Text = Tex.ToString().Substring(4,1);
					Label168.Text = Tex.ToString().Substring(5,1);
					Label169.Text = Tex.ToString().Substring(6,1);
					Label170.Text = Tex.ToString().Substring(7,1);
					Label171.Text = Tex.ToString().Substring(8,1);
					Label172.Text = Tex.ToString().Substring(9,1);
				}

			}

			Session.Remove("UWG");
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
