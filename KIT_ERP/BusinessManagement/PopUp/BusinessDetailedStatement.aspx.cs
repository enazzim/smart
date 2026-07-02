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
using Infragistics.WebUI.UltraWebGrid;

namespace KIT_ERP.BusinessManagement.PopUp
{
	/// <summary>
	/// BusinessDetailedStatement에 대한 요약 설명입니다.
	/// </summary>
	public class BusinessDetailedStatement : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
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
		protected System.Web.UI.WebControls.Label Label25;
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
		protected System.Web.UI.WebControls.Label Label42;
		protected System.Web.UI.WebControls.Label Label43;
		protected System.Web.UI.WebControls.Label Label44;
		protected System.Web.UI.WebControls.Label Label45;
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
		protected System.Web.UI.WebControls.Label Label78;
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
		protected System.Web.UI.WebControls.Label Label24;		
		protected System.Web.UI.WebControls.Label Label97;
		protected System.Web.UI.WebControls.Label Label26;
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

		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid uwg;
		protected Infragistics.WebUI.WebDataInput.WebNumericEdit WebNumericEdit1;
		protected Infragistics.WebUI.WebDataInput.WebNumericEdit WebNumericEdit2;
	
		private static int cnt;
		
		private void Page_Load(object sender, System.EventArgs e)
		{			
			if(!IsPostBack)
			{				
				Supplier();
				Supplied();
				Listing();
			}
		}
        


		/// <summary>
		/// 공급자
		/// </summary>
		private void Supplier()
		{
			
			
			string ConnectStr = ConfigurationSettings.AppSettings["Day"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT * FROM UCI_T";				
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

			if(reader.Read())
			{
				Label1.Text = reader["BusinessCompanyNum"].ToString();			// 사업자등록번호
				Label2.Text = reader["CompanyName"].ToString();					// 상호
				Label3.Text = reader["PresidentName"].ToString();				// 대표자
				Label4.Text = reader["BusinessBillAddress"].ToString();			// 주소
				Label5.Text = reader["BusinessClassification"].ToString();		// 업종
				Label6.Text = reader["BusinessItem"].ToString();				// 종목

				Label13.Text = Label1.Text;
				Label14.Text = Label2.Text;
				Label15.Text = Label3.Text;
				Label16.Text = Label4.Text;
				Label17.Text = Label5.Text;
				Label18.Text = Label6.Text;
			}

			reader.Close();
			Con.Close();
		}



		/// <summary>
		/// 공급받는자
		/// </summary>
		private void Supplied()
		{
			uwg = (Infragistics.WebUI.UltraWebGrid.UltraWebGrid)Session["Grid"];	
			cnt = uwg.Rows.Count;
			Label7.Text = uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString();			// 사업자등록번호
			Label8.Text = uwg.Rows[0].Cells.FromKey("CompanyName").ToString();						// 상호

			Label19.Text = Label7.Text;
			Label20.Text = Label8.Text;



			string ConnectStr = ConfigurationSettings.AppSettings["DSN"].ToString();
			SqlConnection Con = new SqlConnection(ConnectStr);
			SqlCommand Cmd = new SqlCommand();
			Cmd.Connection = Con;
			Con.Open();
			Cmd.CommandText = "SELECT * FROM CI_MT WHERE BusinessRegistrationNum = @BusinessRegistrationNum";
			Cmd.Parameters.Add("@BusinessRegistrationNum", SqlDbType.NVarChar).Value = Label7.Text;
			SqlDataReader reader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);

			if(reader.Read())
			{
				Label9.Text = reader["PresidentName"].ToString();
				Label10.Text = reader["BusinessCompanyAddress"].ToString();
				Label11.Text = reader["BusinessClassification"].ToString();
				Label12.Text = reader["BusinessItem"].ToString();

				Label21.Text = Label9.Text;
				Label22.Text = Label10.Text;
				Label23.Text = Label11.Text;
				Label24.Text = Label12.Text;
			}

			reader.Close();
			Con.Close();
		}
		


		/// <summary>
		/// 항목
		/// </summary>
		private void Listing()
		{
			if(cnt == 1)
			{				
				Label25.Text = "1";
				Label26.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
				Label27.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();
				Label28.Text = uwg.Rows[0].Cells.FromKey("OutStorehouseQuantity").ToString();
				double OSQ = double.Parse(Label28.Text);			
				//Label29.Text = 단위
				Label30.Text = uwg.Rows[0].Cells.FromKey("ApplyUnitCost").ToString();
				double AC = double.Parse(Label30.Text);
				double result = OSQ*AC;
				Label31.Text = Convert.ToString(result);
				Label32.Text = Convert.ToString(result*0.1);
				double totalsum = result+(result*0.1);
				Label97.Text = Convert.ToString(totalsum);
			}

			else if(cnt == 2)
			{				
				if(uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[1].Cells.FromKey("BusinessRegistrationNum").ToString())
				{		
					Label25.Text = "1";					
					Label26.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
					Label27.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();
					Label28.Text = uwg.Rows[0].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ = double.Parse(Label28.Text);			
					//Label29.Text = 단위
					Label30.Text = uwg.Rows[0].Cells.FromKey("ApplyUnitCost").ToString();;
					double AC = double.Parse(Label30.Text);
					double result = OSQ*AC;
					Label31.Text = Convert.ToString(result);
					Label32.Text = Convert.ToString(result*0.1);
					double totalsum = result+(result*0.1);

					Label33.Text = "2";					
					Label34.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
					Label35.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();
					Label36.Text = uwg.Rows[1].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ1 = double.Parse(Label36.Text);			
					//Label37.Text = 단위
					Label38.Text = uwg.Rows[1].Cells.FromKey("ApplyUnitCost").ToString();
					double AC1 = double.Parse(Label38.Text);
					double result1 = OSQ1*AC1;
					Label39.Text = Convert.ToString(result1);
					Label40.Text = Convert.ToString(result1*0.1);
					double totalsum1 = result1+(result1*0.1);
					Label97.Text = Convert.ToString(totalsum+totalsum1);
				}				
			}

			else if(cnt == 3)
			{
				if(uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[1].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[2].Cells.FromKey("BusinessRegistrationNum").ToString())
				{
					Label25.Text = "1";					
					Label26.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
					Label27.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();
					Label28.Text = uwg.Rows[0].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ = double.Parse(Label28.Text);			
					//Label29.Text = 단위
					Label30.Text =uwg.Rows[0].Cells.FromKey("ApplyUnitCost").ToString();
					double AC = double.Parse(Label30.Text);
					double result = OSQ*AC;
					Label31.Text = Convert.ToString(result);
					Label32.Text = Convert.ToString(result*0.1);
					double totalsum = result+(result*0.1);


					Label33.Text = "2";					
					Label34.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
					Label35.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();
					Label36.Text = uwg.Rows[1].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ1 = double.Parse(Label36.Text);			
					//Label37.Text = 단위
					Label38.Text = uwg.Rows[1].Cells.FromKey("ApplyUnitCost").ToString();
					double AC1 = double.Parse(Label38.Text);
					double result1 = OSQ1*AC1;
					Label39.Text = Convert.ToString(result1);
					Label40.Text = Convert.ToString(result1*0.1);
					double totalsum1 = result1+(result1*0.1);


					Label41.Text = "3";					
					Label42.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();
					Label43.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();
					Label44.Text = uwg.Rows[2].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ2 = double.Parse(Label44.Text);			
					//Label45.Text = 단위
					Label46.Text = uwg.Rows[2].Cells.FromKey("ApplyUnitCost").ToString();
					double AC2 = double.Parse(Label46.Text);
					double result2 = OSQ2*AC2;
					Label47.Text = Convert.ToString(result2);
					Label48.Text = Convert.ToString(result2*0.1);
					double totalsum2 = result2+(result2*0.1);
					Label97.Text = Convert.ToString(totalsum+totalsum1+totalsum2);
				}
			}

			else if(cnt == 4)
			{
				if(uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[1].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[2].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[3].Cells.FromKey("BusinessRegistrationNum").ToString())
				{
					Label25.Text = "1";					
					Label26.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
					Label27.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();
					Label28.Text = uwg.Rows[0].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ = double.Parse(Label28.Text);			
					//Label29.Text = 단위
					Label30.Text = uwg.Rows[0].Cells.FromKey("ApplyUnitCost").ToString();
					double AC = double.Parse(Label30.Text);
					double result = OSQ*AC;
					Label31.Text = Convert.ToString(result);
					Label32.Text = Convert.ToString(result*0.1);
					double totalsum = result+(result*0.1);


					Label33.Text = "2";					
					Label34.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
					Label35.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();
					Label36.Text = uwg.Rows[1].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ1 = double.Parse(Label36.Text);			
					//Label37.Text = 단위
					Label38.Text =uwg.Rows[1].Cells.FromKey("ApplyUnitCost").ToString();
					double AC1 = double.Parse(Label38.Text);
					double result1 = OSQ1*AC1;
					Label39.Text = Convert.ToString(result1);
					Label40.Text = Convert.ToString(result1*0.1);
					double totalsum1 = result1+(result1*0.1);


					Label41.Text = "3";					
					Label42.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();
					Label43.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();
					Label44.Text = uwg.Rows[2].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ2 = double.Parse(Label44.Text);			
					//Label45.Text = 단위
					Label46.Text = uwg.Rows[2].Cells.FromKey("ApplyUnitCost").ToString();
					double AC2 = double.Parse(Label46.Text);
					double result2 = OSQ2*AC2;
					Label47.Text = Convert.ToString(result2);
					Label48.Text = Convert.ToString(result2*0.1);
					double totalsum2 = result2+(result2*0.1);


					Label49.Text = "4";					
					Label50.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();
					Label51.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();
					Label52.Text = uwg.Rows[3].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ3 = double.Parse(Label52.Text);			
					//Label53.Text = 단위
					Label54.Text = uwg.Rows[3].Cells.FromKey("ApplyUnitCost").ToString();
					double AC3 = double.Parse(Label54.Text);
					double result3 = OSQ3*AC3;
					Label55.Text = Convert.ToString(result3);
					Label56.Text = Convert.ToString(result3*0.1);
					double totalsum3 = result3+(result3*0.1);
					Label97.Text = Convert.ToString(totalsum+totalsum1+totalsum2+totalsum3);
				}
			}

			else if(cnt == 5)
			{
				if(uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[1].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[2].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[3].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[4].Cells.FromKey("BusinessRegistrationNum").ToString())
				{
					Label25.Text = "1";					
					Label26.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
					Label27.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();
					Label28.Text = uwg.Rows[0].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ = double.Parse(Label28.Text);			
					//Label29.Text = 단위
					Label30.Text = uwg.Rows[0].Cells.FromKey("ApplyUnitCost").ToString();
					double AC = double.Parse(Label30.Text);
					double result = OSQ*AC;
					Label31.Text = Convert.ToString(result);
					Label32.Text = Convert.ToString(result*0.1);
					double totalsum = result+(result*0.1);


					Label33.Text = "2";					
					Label34.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
					Label35.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();
					Label36.Text = uwg.Rows[1].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ1 = double.Parse(Label36.Text);			
					//Label37.Text = 단위
					Label38.Text = uwg.Rows[1].Cells.FromKey("ApplyUnitCost").ToString();
					double AC1 = double.Parse(Label38.Text);
					double result1 = OSQ1*AC1;
					Label39.Text = Convert.ToString(result1);
					Label40.Text = Convert.ToString(result1*0.1);
					double totalsum1 = result1+(result1*0.1);


					Label41.Text = "3";					
					Label42.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();
					Label43.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();
					Label44.Text = uwg.Rows[2].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ2 = double.Parse(Label44.Text);			
					//Label45.Text = 단위
					Label46.Text = uwg.Rows[2].Cells.FromKey("ApplyUnitCost").ToString();
					double AC2 = double.Parse(Label46.Text);
					double result2 = OSQ2*AC2;
					Label47.Text = Convert.ToString(result2);
					Label48.Text = Convert.ToString(result2*0.1);
					double totalsum2 = result2+(result2*0.1);


					Label49.Text = "4";					
					Label50.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();
					Label51.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();
					Label52.Text = uwg.Rows[3].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ3 = double.Parse(Label52.Text);			
					//Label53.Text = 단위
					Label54.Text = uwg.Rows[3].Cells.FromKey("ApplyUnitCost").ToString();
					double AC3 = double.Parse(Label54.Text);
					double result3 = OSQ3*AC3;
					Label55.Text = Convert.ToString(result3);
					Label56.Text = Convert.ToString(result3*0.1);
					double totalsum3 = result3+(result3*0.1);


					Label57.Text = "5";					
					Label58.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();
					Label59.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();
					Label60.Text = uwg.Rows[4].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ4 = double.Parse(Label60.Text);			
					//Label61.Text = 단위
					Label62.Text = uwg.Rows[4].Cells.FromKey("ApplyUnitCost").ToString();
					double AC4 = double.Parse(Label62.Text);
					double result4 = OSQ4*AC4;
					Label63.Text = Convert.ToString(result4);
					Label64.Text = Convert.ToString(result4*0.1);
					double totalsum4 = result4+(result4*0.1);
					Label97.Text = Convert.ToString(totalsum+totalsum1+totalsum2+totalsum3+totalsum4);
				}
			}

			else if(cnt == 6)
			{
				if(uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[1].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[2].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[3].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[4].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[5].Cells.FromKey("BusinessRegistrationNum").ToString())
				{
					Label25.Text = "1";					
					Label26.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
					Label27.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();
					Label28.Text = uwg.Rows[0].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ = double.Parse(Label28.Text);			
					//Label29.Text = 단위
					Label30.Text = uwg.Rows[0].Cells.FromKey("ApplyUnitCost").ToString();
					double AC = double.Parse(Label30.Text);
					double result = OSQ*AC;
					Label31.Text = Convert.ToString(result);
					Label32.Text = Convert.ToString(result*0.1);
					double totalsum = result+(result*0.1);


					Label33.Text = "2";					
					Label34.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
					Label35.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();
					Label36.Text = uwg.Rows[1].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ1 = double.Parse(Label36.Text);			
					//Label37.Text = 단위
					Label38.Text = uwg.Rows[1].Cells.FromKey("ApplyUnitCost").ToString();
					double AC1 = double.Parse(Label38.Text);
					double result1 = OSQ1*AC1;
					Label39.Text = Convert.ToString(result1);
					Label40.Text = Convert.ToString(result1*0.1);
					double totalsum1 = result1+(result1*0.1);


					Label41.Text = "3";					
					Label42.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();
					Label43.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();
					Label44.Text = uwg.Rows[2].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ2 = double.Parse(Label44.Text);			
					//Label45.Text = 단위
					Label46.Text = uwg.Rows[2].Cells.FromKey("ApplyUnitCost").ToString();
					double AC2 = double.Parse(Label46.Text);
					double result2 = OSQ2*AC2;
					Label47.Text = Convert.ToString(result2);
					Label48.Text = Convert.ToString(result2*0.1);
					double totalsum2 = result2+(result2*0.1);


					Label49.Text = "4";					
					Label50.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();
					Label51.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();
					Label52.Text = uwg.Rows[3].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ3 = double.Parse(Label52.Text);			
					//Label53.Text = 단위
					Label54.Text = uwg.Rows[3].Cells.FromKey("ApplyUnitCost").ToString();
					double AC3 = double.Parse(Label54.Text);
					double result3 = OSQ3*AC3;
					Label55.Text = Convert.ToString(result3);
					Label56.Text = Convert.ToString(result3*0.1);
					double totalsum3 = result3+(result3*0.1);


					Label57.Text = "5";					
					Label58.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();
					Label59.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();
					Label60.Text = uwg.Rows[4].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ4 = double.Parse(Label60.Text);			
					//Label61.Text = 단위
					Label62.Text = uwg.Rows[4].Cells.FromKey("ApplyUnitCost").ToString();
					double AC4 = double.Parse(Label62.Text);
					double result4 = OSQ4*AC4;
					Label63.Text = Convert.ToString(result4);
					Label64.Text = Convert.ToString(result4*0.1);
					double totalsum4 = result4+(result4*0.1);


					Label65.Text = "6";					
					Label66.Text = uwg.Rows[5].Cells.FromKey("ItemNum").ToString();
					Label67.Text = uwg.Rows[5].Cells.FromKey("ItemName").ToString();
					Label68.Text = uwg.Rows[5].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ5 = double.Parse(Label68.Text);			
					//Label69.Text = 단위
					Label70.Text = uwg.Rows[5].Cells.FromKey("ApplyUnitCost").ToString();
					double AC5 = double.Parse(Label70.Text);
					double result5 = OSQ5*AC5;
					Label71.Text = Convert.ToString(result5);
					Label72.Text = Convert.ToString(result5*0.1);
					double totalsum5 = result5+(result5*0.1);
					Label97.Text = Convert.ToString(totalsum+totalsum1+totalsum2+totalsum3+totalsum4+totalsum5);
				}
			}

			else if(cnt == 7)
			{
				if(uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[1].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[2].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[3].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[4].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[5].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[6].Cells.FromKey("BusinessRegistrationNum").ToString())
				{
					Label25.Text = "1";					
					Label26.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
					Label27.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();
					Label28.Text = uwg.Rows[0].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ = double.Parse(Label28.Text);			
					//Label29.Text = 단위
					Label30.Text = uwg.Rows[0].Cells.FromKey("ApplyUnitCost").ToString();
					double AC = double.Parse(Label30.Text);
					double result = OSQ*AC;
					Label31.Text = Convert.ToString(result);
					Label32.Text = Convert.ToString(result*0.1);
					double totalsum = result+(result*0.1);


					Label33.Text = "2";					
					Label34.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
					Label35.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();
					Label36.Text = uwg.Rows[1].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ1 = double.Parse(Label36.Text);			
					//Label37.Text = 단위
					Label38.Text = uwg.Rows[1].Cells.FromKey("ApplyUnitCost").ToString();
					double AC1 = double.Parse(Label38.Text);
					double result1 = OSQ1*AC1;
					Label39.Text = Convert.ToString(result1);
					Label40.Text = Convert.ToString(result1*0.1);
					double totalsum1 = result1+(result1*0.1);


					Label41.Text = "3";					
					Label42.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();
					Label43.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();
					Label44.Text = uwg.Rows[2].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ2 = double.Parse(Label44.Text);			
					//Label45.Text = 단위
					Label46.Text = uwg.Rows[2].Cells.FromKey("ApplyUnitCost").ToString();
					double AC2 = double.Parse(Label46.Text);
					double result2 = OSQ2*AC2;
					Label47.Text = Convert.ToString(result2);
					Label48.Text = Convert.ToString(result2*0.1);
					double totalsum2 = result2+(result2*0.1);


					Label49.Text = "4";					
					Label50.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();
					Label51.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();
					Label52.Text = uwg.Rows[3].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ3 = double.Parse(Label52.Text);			
					//Label53.Text = 단위
					Label54.Text = uwg.Rows[3].Cells.FromKey("ApplyUnitCost").ToString();
					double AC3 = double.Parse(Label54.Text);
					double result3 = OSQ3*AC3;
					Label55.Text = Convert.ToString(result3);
					Label56.Text = Convert.ToString(result3*0.1);
					double totalsum3 = result3+(result3*0.1);


					Label57.Text = "5";					
					Label58.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();
					Label59.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();
					Label60.Text = uwg.Rows[4].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ4 = double.Parse(Label60.Text);			
					//Label61.Text = 단위
					Label62.Text = uwg.Rows[4].Cells.FromKey("ApplyUnitCost").ToString();
					double AC4 = double.Parse(Label62.Text);
					double result4 = OSQ4*AC4;
					Label63.Text = Convert.ToString(result4);
					Label64.Text = Convert.ToString(result4*0.1);
					double totalsum4 = result4+(result4*0.1);


					Label65.Text = "6";					
					Label66.Text = uwg.Rows[5].Cells.FromKey("ItemNum").ToString();
					Label67.Text = uwg.Rows[5].Cells.FromKey("ItemName").ToString();
					Label68.Text = uwg.Rows[5].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ5 = double.Parse(Label68.Text);			
					//Label69.Text = 단위
					Label70.Text = uwg.Rows[5].Cells.FromKey("ApplyUnitCost").ToString();
					double AC5 = double.Parse(Label70.Text);
					double result5 = OSQ5*AC5;
					Label71.Text = Convert.ToString(result5);
					Label72.Text = Convert.ToString(result5*0.1);
					double totalsum5 = result5+(result5*0.1);


					Label73.Text = "7";					
					Label74.Text = uwg.Rows[6].Cells.FromKey("ItemNum").ToString();
					Label75.Text = uwg.Rows[6].Cells.FromKey("ItemName").ToString();
					Label76.Text = uwg.Rows[6].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ6 = double.Parse(Label76.Text);			
					//Label77.Text = 단위
					Label78.Text = uwg.Rows[6].Cells.FromKey("ApplyUnitCost").ToString();
					double AC6 = double.Parse(Label78.Text);
					double result6 = OSQ6*AC6;
					Label79.Text = Convert.ToString(result6);
					Label80.Text = Convert.ToString(result6*0.1);
					double totalsum6 = result6+(result6*0.1);
					Label97.Text = Convert.ToString(totalsum+totalsum1+totalsum2+totalsum3+totalsum4+totalsum5+totalsum6);
				}
			}

			else if(cnt == 8)
			{
				if(uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[1].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[2].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[3].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[4].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[5].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[6].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[7].Cells.FromKey("BusinessRegistrationNum").ToString())
				{
					Label25.Text = "1";					
					Label26.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
					Label27.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();
					Label28.Text = uwg.Rows[0].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ = double.Parse(Label28.Text);			
					//Label29.Text = 단위
					Label30.Text = uwg.Rows[0].Cells.FromKey("ApplyUnitCost").ToString();
					double AC = double.Parse(Label30.Text);
					double result = OSQ*AC;
					Label31.Text = Convert.ToString(result);
					Label32.Text = Convert.ToString(result*0.1);
					double totalsum = result+(result*0.1);


					Label33.Text = "2";					
					Label34.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
					Label35.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();
					Label36.Text = uwg.Rows[1].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ1 = double.Parse(Label36.Text);			
					//Label37.Text = 단위
					Label38.Text = uwg.Rows[1].Cells.FromKey("ApplyUnitCost").ToString();
					double AC1 = double.Parse(Label38.Text);
					double result1 = OSQ1*AC1;
					Label39.Text = Convert.ToString(result1);
					Label40.Text = Convert.ToString(result1*0.1);
					double totalsum1 = result1+(result1*0.1);


					Label41.Text = "3";					
					Label42.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();
					Label43.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();
					Label44.Text = uwg.Rows[2].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ2 = double.Parse(Label44.Text);			
					//Label45.Text = 단위
					Label46.Text = uwg.Rows[2].Cells.FromKey("ApplyUnitCost").ToString();
					double AC2 = double.Parse(Label46.Text);
					double result2 = OSQ2*AC2;
					Label47.Text = Convert.ToString(result2);
					Label48.Text = Convert.ToString(result2*0.1);
					double totalsum2 = result2+(result2*0.1);


					Label49.Text = "4";					
					Label50.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();
					Label51.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();
					Label52.Text = uwg.Rows[3].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ3 = double.Parse(Label52.Text);			
					//Label53.Text = 단위
					Label54.Text = uwg.Rows[3].Cells.FromKey("ApplyUnitCost").ToString();
					double AC3 = double.Parse(Label54.Text);
					double result3 = OSQ3*AC3;
					Label55.Text = Convert.ToString(result3);
					Label56.Text = Convert.ToString(result3*0.1);
					double totalsum3 = result3+(result3*0.1);


					Label57.Text = "5";					
					Label58.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();
					Label59.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();
					Label60.Text = uwg.Rows[4].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ4 = double.Parse(Label60.Text);			
					//Label61.Text = 단위
					Label62.Text = uwg.Rows[4].Cells.FromKey("ApplyUnitCost").ToString();
					double AC4 = double.Parse(Label62.Text);
					double result4 = OSQ4*AC4;
					Label63.Text = Convert.ToString(result4);
					Label64.Text = Convert.ToString(result4*0.1);
					double totalsum4 = result4+(result4*0.1);


					Label65.Text = "6";					
					Label66.Text = uwg.Rows[5].Cells.FromKey("ItemNum").ToString();
					Label67.Text = uwg.Rows[5].Cells.FromKey("ItemName").ToString();
					Label68.Text = uwg.Rows[5].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ5 = double.Parse(Label68.Text);			
					//Label69.Text = 단위
					Label70.Text = uwg.Rows[5].Cells.FromKey("ApplyUnitCost").ToString();
					double AC5 = double.Parse(Label70.Text);
					double result5 = OSQ5*AC5;
					Label71.Text = Convert.ToString(result5);
					Label72.Text = Convert.ToString(result5*0.1);
					double totalsum5 = result5+(result5*0.1);


					Label73.Text = "7";					
					Label74.Text = uwg.Rows[6].Cells.FromKey("ItemNum").ToString();
					Label75.Text = uwg.Rows[6].Cells.FromKey("ItemName").ToString();
					Label76.Text = uwg.Rows[6].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ6 = double.Parse(Label76.Text);			
					//Label77.Text = 단위
					Label78.Text = uwg.Rows[6].Cells.FromKey("ApplyUnitCost").ToString();
					double AC6 = double.Parse(Label78.Text);
					double result6 = OSQ6*AC6;
					Label79.Text = Convert.ToString(result6);
					Label80.Text = Convert.ToString(result6*0.1);
					double totalsum6 = result6+(result6*0.1);						


					Label81.Text = "8";					
					Label82.Text = uwg.Rows[7].Cells.FromKey("ItemNum").ToString();
					Label83.Text = uwg.Rows[7].Cells.FromKey("ItemName").ToString();
					Label84.Text = uwg.Rows[7].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ7 = double.Parse(Label84.Text);			
					//Label85.Text = 단위
					Label86.Text = uwg.Rows[7].Cells.FromKey("ApplyUnitCost").ToString();
					double AC7 = double.Parse(Label86.Text);
					double result7 = OSQ7*AC7;
					Label87.Text = Convert.ToString(result7);
					Label88.Text = Convert.ToString(result7*0.1);
					double totalsum7 = result7+(result7*0.1);
					Label97.Text = Convert.ToString(totalsum+totalsum1+totalsum2+totalsum3+totalsum4+totalsum5+totalsum6+totalsum7);
				}
			}

			else if(cnt == 9)
			{
				if(uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[1].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[2].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[3].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[4].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[5].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[6].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[7].Cells.FromKey("BusinessRegistrationNum").ToString() && uwg.Rows[0].Cells.FromKey("BusinessRegistrationNum").ToString() == uwg.Rows[8].Cells.FromKey("BusinessRegistrationNum").ToString())
				{
					Label25.Text = "1";					
					Label26.Text = uwg.Rows[0].Cells.FromKey("ItemNum").ToString();
					Label27.Text = uwg.Rows[0].Cells.FromKey("ItemName").ToString();
					Label28.Text = uwg.Rows[0].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ = double.Parse(Label28.Text);			
					//Label29.Text = 단위
					Label30.Text = uwg.Rows[0].Cells.FromKey("ApplyUnitCost").ToString();
					double AC = double.Parse(Label30.Text);
					double result = OSQ*AC;
					Label31.Text = Convert.ToString(result);
					Label32.Text = Convert.ToString(result*0.1);
					double totalsum = result+(result*0.1);


					Label33.Text = "2";					
					Label34.Text = uwg.Rows[1].Cells.FromKey("ItemNum").ToString();
					Label35.Text = uwg.Rows[1].Cells.FromKey("ItemName").ToString();
					Label36.Text = uwg.Rows[1].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ1 = double.Parse(Label36.Text);			
					//Label37.Text = 단위
					Label38.Text = uwg.Rows[1].Cells.FromKey("ApplyUnitCost").ToString();
					double AC1 = double.Parse(Label38.Text);
					double result1 = OSQ1*AC1;
					Label39.Text = Convert.ToString(result1);						
					Label40.Text = Convert.ToString(result1*0.1);
					double totalsum1 = result1+(result1*0.1);


					Label41.Text = "3";					
					Label42.Text = uwg.Rows[2].Cells.FromKey("ItemNum").ToString();
					Label43.Text = uwg.Rows[2].Cells.FromKey("ItemName").ToString();
					Label44.Text = uwg.Rows[2].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ2 = double.Parse(Label44.Text);			
					//Label45.Text = 단위
					Label46.Text = uwg.Rows[2].Cells.FromKey("ApplyUnitCost").ToString();
					double AC2 = double.Parse(Label46.Text);
					double result2 = OSQ2*AC2;
					Label47.Text = Convert.ToString(result2);
					Label48.Text = Convert.ToString(result2*0.1);
					double totalsum2 = result2+(result2*0.1);


					Label49.Text = "4";					
					Label50.Text = uwg.Rows[3].Cells.FromKey("ItemNum").ToString();
					Label51.Text = uwg.Rows[3].Cells.FromKey("ItemName").ToString();
					Label52.Text = uwg.Rows[3].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ3 = double.Parse(Label52.Text);			
					//Label53.Text = 단위
					Label54.Text = uwg.Rows[3].Cells.FromKey("ApplyUnitCost").ToString();
					double AC3 = double.Parse(Label54.Text);
					double result3 = OSQ3*AC3;
					Label55.Text = Convert.ToString(result3);
					Label56.Text = Convert.ToString(result3*0.1);
					double totalsum3 = result3+(result3*0.1);


					Label57.Text = "5";					
					Label58.Text = uwg.Rows[4].Cells.FromKey("ItemNum").ToString();
					Label59.Text = uwg.Rows[4].Cells.FromKey("ItemName").ToString();
					Label60.Text = uwg.Rows[4].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ4 = double.Parse(Label60.Text);			
					//Label61.Text = 단위
					Label62.Text = uwg.Rows[4].Cells.FromKey("ApplyUnitCost").ToString();
					double AC4 = double.Parse(Label62.Text);
					double result4 = OSQ4*AC4;
					Label63.Text = Convert.ToString(result4);
					Label64.Text = Convert.ToString(result4*0.1);
					double totalsum4 = result4+(result4*0.1);


					Label65.Text = "6";					
					Label66.Text = uwg.Rows[5].Cells.FromKey("ItemNum").ToString();
					Label67.Text = uwg.Rows[5].Cells.FromKey("ItemName").ToString();
					Label68.Text = uwg.Rows[5].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ5 = double.Parse(Label68.Text);			
					//Label69.Text = 단위
					Label70.Text = uwg.Rows[5].Cells.FromKey("ApplyUnitCost").ToString();
					double AC5 = double.Parse(Label70.Text);
					double result5 = OSQ5*AC5;
					Label71.Text = Convert.ToString(result5);
					Label72.Text = Convert.ToString(result5*0.1);
					double totalsum5 = result5+(result5*0.1);


					Label73.Text = "7";					
					Label74.Text = uwg.Rows[6].Cells.FromKey("ItemNum").ToString();
					Label75.Text = uwg.Rows[6].Cells.FromKey("ItemName").ToString();
					Label76.Text = uwg.Rows[6].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ6 = double.Parse(Label76.Text);			
					//Label77.Text = 단위
					Label78.Text = uwg.Rows[6].Cells.FromKey("ApplyUnitCost").ToString();
					double AC6 = double.Parse(Label78.Text);
					double result6 = OSQ6*AC6;
					Label79.Text = Convert.ToString(result6);
					Label80.Text = Convert.ToString(result6*0.1);
					double totalsum6 = result6+(result6*0.1);


					Label81.Text = "8";					
					Label82.Text = uwg.Rows[7].Cells.FromKey("ItemNum").ToString();
					Label83.Text = uwg.Rows[7].Cells.FromKey("ItemName").ToString();
					Label84.Text = uwg.Rows[7].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ7 = double.Parse(Label84.Text);			
					//Label85.Text = 단위
					Label86.Text = uwg.Rows[7].Cells.FromKey("ApplyUnitCost").ToString();
					double AC7 = double.Parse(Label86.Text);
					double result7 = OSQ7*AC7;
					Label87.Text = Convert.ToString(result7);
					Label88.Text = Convert.ToString(result7*0.1);
					double totalsum7 = result7+(result7*0.1);


					Label89.Text = "9";					
					Label90.Text = uwg.Rows[8].Cells.FromKey("ItemNum").ToString();
					Label91.Text = uwg.Rows[8].Cells.FromKey("ItemName").ToString();
					Label92.Text = uwg.Rows[8].Cells.FromKey("OutStorehouseQuantity").ToString();
					double OSQ8 = double.Parse(Label92.Text);			
					//Label93.Text = 단위
					Label94.Text = uwg.Rows[8].Cells.FromKey("ApplyUnitCost").ToString();
					double AC8 = double.Parse(Label94.Text);
					double result8 = OSQ8*AC8;
					Label95.Text = Convert.ToString(result8);
					Label96.Text = Convert.ToString(result8*0.1);
					double totalsum8 = result8+(result8*0.1);
					Label97.Text = Convert.ToString(totalsum+totalsum1+totalsum2+totalsum3+totalsum4+totalsum5+totalsum6+totalsum7+totalsum8);
				}
			}							
			copy();
		}

		private void copy()
		{
			// 순번
			Label98.Text = Label25.Text;
			Label99.Text = Label33.Text;
			Label100.Text = Label41.Text;
			Label101.Text = Label49.Text;
			Label102.Text = Label57.Text;
			Label103.Text = Label65.Text;
			Label104.Text = Label73.Text;
			Label105.Text = Label81.Text;
			Label106.Text = Label89.Text;

			// 품번
			Label107.Text = Label26.Text;
			Label108.Text = Label34.Text;
			Label109.Text = Label42.Text;
			Label110.Text = Label50.Text;
			Label111.Text = Label58.Text;
			Label112.Text = Label66.Text;
			Label113.Text = Label74.Text;
			Label114.Text = Label82.Text;
			Label115.Text = Label90.Text;

			// 품목명
			Label116.Text = Label27.Text;
			Label117.Text = Label35.Text;
			Label118.Text = Label43.Text;
			Label119.Text = Label51.Text;
			Label120.Text = Label59.Text;
			Label121.Text = Label67.Text;
			Label122.Text = Label75.Text;
			Label123.Text = Label83.Text;
			Label124.Text = Label91.Text;

			//수량(중량)
			Label125.Text = Label28.Text;
			Label126.Text = Label36.Text;				
			Label127.Text = Label44.Text;
			Label128.Text = Label52.Text;
			Label129.Text = Label60.Text;
			Label130.Text = Label68.Text;
			Label131.Text = Label76.Text;
			Label132.Text = Label84.Text;
			Label133.Text = Label92.Text;

			// 단위
			Label134.Text = Label29.Text;
			Label135.Text = Label37.Text;				
			Label136.Text = Label45.Text;
			Label137.Text = Label53.Text;
			Label138.Text = Label61.Text;
			Label139.Text = Label69.Text;
			Label140.Text = Label77.Text;
			Label141.Text = Label85.Text;
			Label142.Text = Label93.Text;

			// 단가
			Label143.Text = Label30.Text;
			Label144.Text = Label38.Text;				
			Label145.Text = Label46.Text;
			Label146.Text = Label54.Text;
			Label147.Text = Label62.Text;
			Label148.Text = Label70.Text;
			Label149.Text = Label78.Text;
			Label150.Text = Label86.Text;
			Label151.Text = Label94.Text;

			// 공급가액
			Label152.Text = Label31.Text;
			Label153.Text = Label39.Text;				
			Label154.Text = Label47.Text;
			Label155.Text = Label55.Text;
			Label156.Text = Label63.Text;
			Label157.Text = Label71.Text;
			Label158.Text = Label79.Text;
			Label159.Text = Label87.Text;
			Label160.Text = Label95.Text;

			// 세액
			Label161.Text = Label32.Text;
			Label162.Text = Label40.Text;				
			Label163.Text = Label48.Text;
			Label164.Text = Label56.Text;
			Label165.Text = Label64.Text;
			Label166.Text = Label72.Text;
			Label167.Text = Label80.Text;
			Label168.Text = Label88.Text;
			Label169.Text = Label96.Text;

			// 총금액
			Label170.Text = Label97.Text;
			
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
