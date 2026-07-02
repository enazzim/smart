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
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace KIT_ERP.BuyingOutside
{
	public class QualityInspectionRegister : KIT_ERP.QualityInspection.QualityInspection_BaseClass
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected System.Web.UI.WebControls.Button btnRegist;
		protected System.Web.UI.WebControls.TextBox txtIncongruityMoney;
		protected System.Web.UI.WebControls.TextBox txtIncongruityDetailContent_Result;
		protected System.Web.UI.WebControls.DropDownList ddlIncongruityCause;
		protected System.Web.UI.WebControls.DropDownList ddlIncongruityPhenomenon;
		protected System.Web.UI.WebControls.DropDownList ddlIncongruityDecision;
		protected System.Web.UI.WebControls.TextBox txtSuccessQuantity;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtEndDate;
		protected System.Web.UI.WebControls.TextBox txtIncongruityQuantity;
		protected Infragistics.WebUI.WebCombo.WebCombo wcCompanyName;
		protected System.Web.UI.WebControls.TextBox txtPerson_Result;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdRequestQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdIndex;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdseq;
		protected string[] strArrValue;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser wdcQualityInspectionCompleteDate;
		protected System.Web.UI.WebControls.DropDownList ddlMon;
		protected System.Web.UI.WebControls.DropDownList ddlYear;
		
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if(Session["ID"] == null)
			{	
				Session.Abandon();
				Page.RegisterClientScriptBlock("SEND", "<script>parent.location.href = '../Login.aspx';</script>");
			}

			
			ItemSearchControl1.Commodity = true;
			ItemSearchControl1.Products = true;
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.RawMaterials = true;

			if( !IsPostBack )
			{
				foreach(ListItem list in ddlYear.Items)
				{
					if(list.Value == DateTime.Now.Year.ToString())
					{
						list.Selected = true;
					}
				}

				for(int a = 0; a < ddlMon.Items.Count; a++)
				{
					if(DateTime.Now.Month == 12)
					{
						if(DateTime.Now.Day > 25)
						{
							for(int b = 0; b< ddlYear.Items.Count;b++)
							{
								ddlYear.Items[b].Selected = false;
								if(int.Parse(ddlYear.Items[b].Value) == DateTime.Now.Year + 1)
									ddlYear.Items[b].Selected = true;
							}
							ddlMon.Items[0].Selected = true;
						}
					}
					else
					{
						if(ddlMon.Items[a].Value == DateTime.Now.Month.ToString() )
						{
							if(DateTime.Now.Day > 25)
								ddlMon.Items[a+1].Selected = true;
							else
								ddlMon.Items[a].Selected = true;
						}
					}
				}
				txtPerson_Result.Text = Session["UserName"].ToString();			
				this.Load_SettingJavaScript();
				this.Load_WebCombo_Bind();

				wdcQualityInspectionCompleteDate.NullDateLabel = DateTime.Now.ToShortDateString();
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnRegist.Click += new System.EventHandler(this.btnRegist_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		// !PostBack 때 호출
		private void Load_SettingJavaScript()
		{
			// 등록 버튼 클릭시 수행될 폼 체크
			btnRegist.Attributes.Add("OnClick", "return RegistCheck()");

			txtSuccessQuantity.Attributes.Add("OnKeyDown", "OnKeyDown_Float(this);");
			// onkeyup 이벤트가 발생하면 콤마를 찍고 불량수량을 자동으로 계산하는 자바스크립트 함수 매핑
			txtSuccessQuantity.Attributes.Add("OnKeyUp", "Process(); OnKeyUp_Currency(this);");

			// focus를 얻으면 자동으로 select()
			txtSuccessQuantity.Attributes.Add("OnFocus", "OnFocus_Obj(this);");

			// focus를 잃으면 다시 입력란을 체크
			txtSuccessQuantity.Attributes.Add("OnBlur", "OnBlur_Cur(this);");
			txtIncongruityMoney.Attributes.Add("OnKeyDown","OnKeyDown_Float(this);");
			txtIncongruityMoney.Attributes.Add("OnKeyUp", "OnKeyUp_Currency(this)");
			txtIncongruityMoney.Attributes.Add("onblur","OnBlur_Cur(this)");
			txtIncongruityMoney.Attributes.Add("onfocus","OnFocus_Obj(this)");
		}


		private void Load_WebCombo_Bind()
		{
			ListItem li = new ListItem(" - 선택하세요 - " ,"0");
			
			SqlDataAdapter adap = new SqlDataAdapter("PageLoad_GetDataSource", base.GetConnectionString);
			adap.SelectCommand.CommandType = CommandType.StoredProcedure;
			
			//			adap.SelectCommand.Parameters.Add("@ItemNum", SqlDbType.Bit).Value = 0;
			adap.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@IncongruityCause", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@IncongruityPhenomenon", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@IncongruityDecision", SqlDbType.Bit).Value = 1;
			adap.SelectCommand.Parameters.Add("@UserName", SqlDbType.Bit).Value = 1;
			
			DataSet ds = new DataSet();
			adap.Fill(ds);

			//			wcItemNum.DataSource = ds.Tables[0].DefaultView;
			wcCompanyName.DataSource = ds.Tables[0].DefaultView;

			ddlIncongruityCause.DataSource = ds.Tables[1].DefaultView;
			ddlIncongruityCause.DataTextField="SmallClassificationName";		// 소분류명
			ddlIncongruityCause.DataValueField="SmallClassificationCode";	// 소분류코드
			
			ddlIncongruityPhenomenon.DataSource = ds.Tables[2].DefaultView;
			ddlIncongruityPhenomenon.DataTextField="SmallClassificationName";
			ddlIncongruityPhenomenon.DataValueField="SmallClassificationCode";
						
			ddlIncongruityDecision.DataSource = ds.Tables[3].DefaultView;
			ddlIncongruityDecision.DataTextField="SmallClassificationName";
			ddlIncongruityDecision.DataValueField="SmallClassificationCode";
			
			this.DataBind();

			ddlIncongruityCause.Items.Insert(0,li);
			ddlIncongruityPhenomenon.Items.Insert(0,li);
			ddlIncongruityDecision.Items.Insert(0,li);

			wcCompanyName.Columns[0].Width = 150;
			wcCompanyName.Columns[1].Width = 80;
			wcCompanyName.Columns[2].Width = 115;
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{						
			search();	
		}

		private void search()
		{
			 KIT_ERP.QualityInspection.QualityInspection_Search qis = new  KIT_ERP.QualityInspection.QualityInspection_Search ( 
				PageNames.품질검사등록
				,  ItemSearchControl1.ItemNum
				,  ItemSearchControl1.ItemDrawNum
				,  ItemSearchControl1.ItemName
				,  wcCompanyName.DisplayValue
				,  txtStartDate.Text
				,  txtEndDate.Text
				) ;

			// 객체의 메소드 호출
			DataTable dTbl = qis.GetDataSet().Tables[0] ;
			DataGrid1.DataSource = dTbl.DefaultView ;
			// 키 필드지정
			DataGrid1.DataKeyField = "QualityInspectionHistoryIndex";
			DataGrid1.DataBind();	
		}

		// 등록버튼
		private void btnRegist_Click(object sender, System.EventArgs e)
		{		
			string A = Request["hidden_DataValue"].ToString();
			if(wdcQualityInspectionCompleteDate.Text.Trim() =="")
			{
				A += DateTime.Now.Year + "ㅙ";
				A += DateTime.Now.Month + "ㅙ";
				A += DateTime.Now.Day;
			}
			else
			{
				A += DateTime.Parse(wdcQualityInspectionCompleteDate.Text).Year + "ㅙ";
				A += DateTime.Parse(wdcQualityInspectionCompleteDate.Text).Month + "ㅙ";
				A += DateTime.Parse(wdcQualityInspectionCompleteDate.Text).Day;
			}

			strArrValue = A.Split('ㅙ');
			string strKey = strArrValue[0];
			
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			con.Open();

			SqlCommand comm = new SqlCommand();
			comm.Connection = con;
			SqlTransaction trans = con.BeginTransaction();
			comm.Transaction = trans;
			try
			{
				int index  =int.Parse(hdIndex.Value.Substring(12,1));	

				KIT_ERP.QualityInspection.QualityInspection_Register qr = new KIT_ERP.QualityInspection.QualityInspection_Register(strArrValue);
				if(qr.Registeration())
				{
					if((strArrValue[11].ToString() == "자가"))
					{
						//작업일보 번호를 찾아 생산계획번호를 가져오고
						//이 생산계획번호를 가지고 완료수량을 누적한다.
						//누적완료수량이  생산계획수량보다 많으면 생산계획원장 진행상태를 완료로 처리

						int idx = 0;
						string str = "Select HistoryIndex2 From QI_HT Where HistorySection2 = '작업계획' and QualityInspectionHistoryIndex = @index";
						comm.CommandText = str;
						comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionHistoryIndex").Value.ToString()));
						SqlDataReader dr = comm.ExecuteReader();
						while(dr.Read())
						{
							idx = int.Parse(dr["HistoryIndex2"].ToString());
						}
						dr.Close();
						comm.Parameters.Clear();

						if(idx != 0)
						{
							string num = "";
							str = "Select ProductionPlanHistoryIndex From WDWP_HT where WCDailyWorkPlanHistoryIndex = @index";
							comm.CommandText = str;
							comm.Parameters.Add("@index",idx);
							SqlDataReader dr1 = comm.ExecuteReader();
							while(dr1.Read())
							{
								num = dr1["ProductionPlanHistoryIndex"].ToString();
							}
							dr1.Close();
							comm.Parameters.Clear();
							if(num != "")
							{
								str = "Update PP_HT Set  ProductionCompleteQuantity = ProductionCompleteQuantity + @quantity Where ProductionPlanHistoryIndex = @index";
								comm.CommandText = str;
								comm.Parameters.Add("@quantity",decimal.Parse(txtSuccessQuantity.Text));
								comm.Parameters.Add("@index",int.Parse(num));
								comm.ExecuteNonQuery();
								comm.Parameters.Clear();

								str = "Select (ProductionPlanQuantity - ProductionCompleteQuantity) as Quantity From PP_HT Where ProductionPlanHistoryIndex = @index";
								comm.CommandText = str;
								comm.Parameters.Add("@index",int.Parse(num));

								decimal Quantity1 = 0;
								SqlDataReader dr2 = comm.ExecuteReader();
								while(dr2.Read())
								{
									Quantity1 = decimal.Parse(dr2["Quantity"].ToString());
								}
								dr2.Close();
								comm.Parameters.Clear();
								//
								if(Quantity1 > 0)
								{
									str = "Update PP_HT Set ProgressCondition = '진행'  Where ProductionPlanHistoryIndex = @index";
									comm.CommandText = str;
									comm.Parameters.Add("@index",int.Parse(num));
									comm.ExecuteNonQuery();
									comm.Parameters.Clear();
								}
								else
								{
									str = "Update PP_HT Set ProgressCondition = '완료'  Where ProductionPlanHistoryIndex = @index";
									comm.CommandText = str;
									comm.Parameters.Add("@index",int.Parse(num));
									comm.ExecuteNonQuery();
									comm.Parameters.Clear();
								}
							}
						}

						//자가처리
						if(int.Parse(txtIncongruityQuantity.Text) != 0)
						{
							if(SelfUpdate(con,trans))
							{

								RegisterStartupScript("","<script>window.status='등록되었습니다!';</script>");
							}
						}
					}
					else
					{
						if(strArrValue[11].ToString() == "외주")
						{
							//외주이면서 최종공정인 경우
							//생산계획원장에 완료수량 누적
							//완료수량이 계획수량보다 많으면 진행상태 완료

							int idx = 0;
							string str = "Select HistoryIndex2 From QI_HT Where HistorySection2 = '외주발주' and QualityInspectionHistoryIndex = @index";
							comm.CommandText = str;
							comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionHistoryIndex").Value.ToString()));
							SqlDataReader dr = comm.ExecuteReader();
							while(dr.Read())
							{
								idx = int.Parse(dr["HistoryIndex2"].ToString());
							}
							dr.Close();
							comm.Parameters.Clear();

							if(idx != 0)
							{
								string number = "";
								str = "Select OutSideOrderRequestHistoryIndex From OO_HT where OutSideOrderHistoryIndex = @index";
								comm.CommandText = str;
								comm.Parameters.Add("@index",idx);
								SqlDataReader dr1 = comm.ExecuteReader();
								while(dr1.Read())
								{
									number = dr1["OutSideOrderRequestHistoryIndex"].ToString();
								}
								dr1.Close();
								comm.Parameters.Clear();
								if(number != "")
								{
									str = "select ProductionPlanHistoryIndex From OOR_HT where OutSideOrderRequestHistoryIndex = @index";
									comm.CommandText = str;
									comm.Parameters.Add("@index",number);
									SqlDataReader dr3 = comm.ExecuteReader();
									string num = "";
									while(dr3.Read())
									{
										num = dr3["ProductionPlanHistoryIndex"].ToString();
									}
									dr3.Close();
									comm.Parameters.Clear();

									if(num != "")
									{

										str = "Update PP_HT Set  ProductionCompleteQuantity = ProductionCompleteQuantity + @quantity Where ProductionPlanHistoryIndex = @index";
										comm.CommandText = str;
										comm.Parameters.Add("@quantity",decimal.Parse(txtSuccessQuantity.Text));
										comm.Parameters.Add("@index",int.Parse(num));
										comm.ExecuteNonQuery();
										comm.Parameters.Clear();

										str = "Select (ProductionPlanQuantity - ProductionCompleteQuantity) as Quantity From PP_HT Where ProductionPlanHistoryIndex = @index";
										comm.CommandText = str;
										comm.Parameters.Add("@index",int.Parse(num));

										decimal Quantity1 = 0;
										SqlDataReader dr2 = comm.ExecuteReader();
										while(dr2.Read())
										{
											Quantity1 = decimal.Parse(dr2["Quantity"].ToString());
										}
										dr2.Close();
										comm.Parameters.Clear();
										//
										if(Quantity1 <= 0)
										{
											str = "Update PP_HT Set ProgressCondition = '완료'  Where ProductionPlanHistoryIndex = @index";
											comm.CommandText = str;
											comm.Parameters.Add("@index",int.Parse(num));
											comm.ExecuteNonQuery();
											comm.Parameters.Clear();
										}
									}
								}
							}


							//외주창고 출고수량 이력원장 기록
							//if(!WorkPlan3())
							//InsertSH_HT(con,trans);
						}
						else
						{
							//SH_HTInsert(con,trans);
						}
						RegisterStartupScript("","<script>window.status='등록되었습니다!';</script>");
					}
				
					//외주면 하위품 외주창고 출고 기록하고 입고품 생산창고 입고 기록하고
					
					//구매및 외주에 관한 품목 입고 이력원장에 기록
					DeliveryHistoryRegister(con,trans);
				}
				trans.Commit();
			}
			catch(Exception ee)
			{
				Response.Write("<script>alert('"+ee.Message +"');</script>");

				trans.Rollback();
			}
			finally
			{
				con.Close();
			}


			search();
		}

		


		private void DeliveryHistoryRegister(SqlConnection conn, SqlTransaction tr)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection=conn;
			comm.Transaction = tr;

			string str = "";
			
			if(strArrValue[11].ToString() == "구매")
			{
				if(Division(strArrValue[16].ToString(),conn,tr) == 1)
					str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason,[Date], UpdatePerson, UpdatePersonID, HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,0,'14000000','소재',@CompanyName, @BusinessRegistrationNum, @Quantity,@UnitCost,'등록',@Date,@Person, @PersonID, '구매납품',@HistoryIndex)";
				else
					str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason,[Date], UpdatePerson, UpdatePersonID, HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,99,'14009999','최종품',@CompanyName, @BusinessRegistrationNum, @Quantity,@UnitCost,'등록',@Date,@Person, @PersonID, '구매납품',@HistoryIndex)";

				comm.Parameters.Add("@itemnum",strArrValue[16].ToString());									//품목번호
				comm.Parameters.Add("@itemdrawnum", strArrValue[17].ToString());								//도면번호
				comm.Parameters.Add("@itemname",strArrValue[15].ToString());									//품목명
				comm.Parameters.Add("@CompanyName", strArrValue[18].ToString());								//업체명
				comm.Parameters.Add("@BusinessRegistrationNum",strArrValue[19].ToString());
				comm.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1].ToString()));					//입출고 수량
				comm.Parameters.Add("@UnitCost",decimal.Parse(strArrValue[20].ToString()));					//입출고 수량
				comm.Parameters.Add("@Date",DateTime.Now.ToShortDateString());								//입출고일
				comm.Parameters.Add("@Person",Session["UserName"].ToString());
				comm.Parameters.Add("@PersonID",Session["ID"].ToString());
				comm.Parameters.Add("@HistoryIndex",int.Parse(strArrValue[12].ToString()));
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
			else if(strArrValue[11].ToString() == "외주")
			{
				str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason,[Date], UpdatePerson, UpdatePersonID, HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,@CompanyName, @BusinessRegistrationNum, @Quantity,@UnitCost,'등록',@Date,@Person, @PersonID, '외주납품',@HistoryIndex)";

				comm.Parameters.Add("@itemnum",strArrValue[16].ToString());									//품목번호
				comm.Parameters.Add("@itemdrawnum", strArrValue[17].ToString());								//도면번호
				comm.Parameters.Add("@itemname",strArrValue[15].ToString());									//품목명
				comm.Parameters.Add("@ProcessSequenceNum",strArrValue[22].ToString());											//공정순서
				comm.Parameters.Add("@ProcessCode",strArrValue[25].ToString());											//공정코드
				comm.Parameters.Add("@ProcessName",strArrValue[24].ToString());												//공정명
				comm.Parameters.Add("@CompanyName", strArrValue[18].ToString());								//업체명
				comm.Parameters.Add("@BusinessRegistrationNum",strArrValue[19].ToString());
				comm.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1].ToString()));					//입출고 수량
				comm.Parameters.Add("@UnitCost",decimal.Parse(strArrValue[20].ToString()));					//입출고 수량
				comm.Parameters.Add("@Date",DateTime.Now.ToShortDateString());								//입출고일
				comm.Parameters.Add("@Person",Session["UserName"].ToString());
				comm.Parameters.Add("@PersonID",Session["ID"].ToString());
				comm.Parameters.Add("@HistoryIndex",int.Parse(strArrValue[12].ToString()));	
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
			}
							
		}


		private void SH_HTInsert(SqlConnection conn, SqlTransaction tr)
		{
			int index  =int.Parse(hdIndex.Value.Substring(12,1));//RegistrationDate	

			SqlCommand comm = new SqlCommand();
			comm.Connection=conn;
			comm.Transaction = tr;

			string str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'원자재창고','1',@Quantity,@Date,'품질검사',@HistoryIndex)";
				

			comm.Parameters.Add("@itemnum",strArrValue[16].ToString());									//품목번호
			comm.Parameters.Add("@itemdrawnum", strArrValue[17].ToString());								//도면번호
			comm.Parameters.Add("@itemname",strArrValue[15].ToString());									//품목명
			comm.Parameters.Add("@ProcessSequenceNum","0");										//공정순서
			comm.Parameters.Add("@ProcessCode",strArrValue[25].ToString());												//공정코드
			comm.Parameters.Add("@ProcessName",strArrValue[24].ToString());												//공정명
			comm.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1].ToString()));					//입출고 수량
			comm.Parameters.Add("@Date",DateTime.Parse(wdcQualityInspectionCompleteDate.Text.Trim()).ToShortDateString());								//입출고일
			//comm.Parameters.Add("@HistoryDivision","구매납품");										//원장구분
			comm.Parameters.Add("@HistoryIndex",int.Parse(strArrValue[0].ToString()));	
			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
		}
		
		private void InsertSH_HT(SqlConnection conn, SqlTransaction tr)
		{

			int index  =int.Parse(hdIndex.Value.Substring(12,1));//RegistrationDate	

			SqlCommand comm = new SqlCommand();
			comm.Connection=conn;
			comm.Transaction = tr;

			
			//지금 공정순서보다 아래에 공정순서가 있는지 없는지를 구한다
			int sequence1 = 0;	//공정순서
			string Code = "";	// 공정코드
			string Name = "";	// 공정명
			string str = "Select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ProcessSequenceNum < @num and ItemNum = @itemnum";
			
			comm.Parameters.Add("@num", int.Parse(hdseq.Value));
			comm.Parameters.Add("@itemnum",strArrValue[17].ToString());
			comm.CommandText = str;
			if(comm.ExecuteScalar() == null || comm.ExecuteScalar().ToString().Trim() == "")
				sequence1 = 0;
			else
				sequence1 = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			
			str = "Select ProcessCode From PSI_MT Where RecodingState = 1 and ProcessSequenceNum = @num and ItemNum = @itemnum";
			comm.Parameters.Add("@num",SqlDbType.Int).Value = sequence1;
			comm.Parameters.Add("@itemnum",strArrValue[17].ToString());
			comm.CommandText = str;
			SqlDataReader dr2 = comm.ExecuteReader();
			while(dr2.Read())
			{
				Code = dr2["ProcessCode"].ToString();
			}
			dr2.Close();
			comm.Parameters.Clear();

			str = "Select SmallClassificationName from PUC_MT where SmallClassificationName != '' and RecodingState = 1 and SmallClassificationCode = @code";
			comm.Parameters.Add("@code",SqlDbType.VarChar).Value = Code;
			comm.CommandText = str;
			SqlDataReader dr3 = comm.ExecuteReader();
			while(dr3.Read())
			{
				Name = dr3["SmallClassificationName"].ToString();
			}
			dr3.Close();
			comm.Parameters.Clear();
		

			

			// 아래 공정순서번호가 존재할때는(공정순서가 0이 아닌 경우) 바로 그 품목을 출고시킨다.
			// 지금 들어있는 공정순서번호와 품목을 출고시킨다.
			string st_ItemNum = strArrValue[17].ToString();

			if(sequence1 != 0)
			{
				str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'외주창고','0',@Quantity,@Date,'품질검사',@HistoryIndex)";
				

				comm.Parameters.Add("@itemnum",strArrValue[16].ToString());									//품목번호
				comm.Parameters.Add("@itemdrawnum", strArrValue[17].ToString());								//도면번호
				comm.Parameters.Add("@itemname",strArrValue[15].ToString());									//품목명
				comm.Parameters.Add("@ProcessSequenceNum",sequence1);											//공정순서
				comm.Parameters.Add("@ProcessCode",Code);											//공정코드
				comm.Parameters.Add("@ProcessName",Name);												//공정명
				comm.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1].ToString()));					//입출고 수량
				comm.Parameters.Add("@Date",DateTime.Parse(wdcQualityInspectionCompleteDate.Text.Trim()).ToShortDateString());								//입출고일
				//comm.Parameters.Add("@HistoryDivision","구매납품");										//원장구분
				comm.Parameters.Add("@HistoryIndex",int.Parse(strArrValue[0].ToString()));	
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

			}
			else
			{
				// 순서번호가 0 이면 하위품목을 찾아 스택에 Push한다
				Stack st = new Stack();	

				string item = "";
				//하위품목을 찾기위해서는 품목구성정보에 지금 품목을 모품으로 하는 모든 레코드를 스택에 넣는다
				//str = "Select ChildItemNum, ItemDrawNum, Item From IOI_MT JOIN ON II_MT Where RecodingState = 1 and ParentItemNum = @itemnum";
				str = @"SELECT ItemNum, ItemDrawNum, ItemName, NeedQuantityNumerator, NeedQuantityDenominator FROM II_MT INNER JOIN IOI_MT ON II_MT.ItemNum = IOI_MT.ChildItemNum 	WHERE (II_MT.RecodingState = 1) AND (IOI_MT.RecodingState = 1) AND (IOI_MT.ParentItemNum = @itemnum) ";
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = st_ItemNum;
				comm.CommandText=str;
				SqlDataReader dr_Child = comm.ExecuteReader();
				while(dr_Child.Read())
				{
					st.Push(dr_Child["ItemNum"].ToString());
				}
				dr_Child.Close();
				comm.Parameters.Clear();
				
				
				// 스택에 품목이 있는지 확인한다.
				if(st.Count != 0)
				{
					// 스택의 내용이 있으면 다 꺼낼때까지 계속 Pop한다
					while(st.Count > 0)
					{	
						item =st.Pop().ToString();
						decimal quantity = 1;

						// 꺼낸품목의 공정순서를 스택1에 넣는다
						Stack st1 = new Stack();					
						int sequencenum=0;

						//꺼낸 품목의 공정순서를 넣어둔다.
						str = "Select ProcessSequenceNum from PSI_MT where RecodingState = 1 AND ItemNum = @itemnum  order by ProcessSequenceNum";
						comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
						comm.CommandText = str;
						SqlDataReader dr_Sequence = comm.ExecuteReader();
						while(dr_Sequence.Read())
						{
							//공정순서
							st1.Push(int.Parse(dr_Sequence["ProcessSequenceNum"].ToString()));
							
						}
						dr_Sequence.Close();
						comm.Parameters.Clear();

						//꺼낸품목의 소요량을 넣어둔다
						str = "select * From IOI_MT Where ParentItemNum = @num and ChildItemNum = @item and RecodingState = 1 ";
						comm.Parameters.Add("@num",st_ItemNum);
						comm.Parameters.Add("@item",item);
						comm.CommandText = str;
						SqlDataReader dr1 = comm.ExecuteReader();
						while(dr1.Read())
						{
							quantity = decimal.Round(decimal.Parse(dr1["NeedQuantityNumerator"].ToString())/decimal.Parse(dr1["NeedQuantityDenominator"].ToString()),3);
						}
						dr1.Close();
						comm.Parameters.Clear();

						//공정순서가 없으면 지금 품목이 원자재이므로 이를 출고시킨다.
						if(st1.Count == 0)
						{
							//품번,도번,품명 구한다
							string itemdraw=""; string name="";
							str = "Select * From II_MT Where ItemNum = @num and RecodingState = 1";
							comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
							comm.CommandText = str;
							SqlDataReader dr_Item = comm.ExecuteReader();
							while(dr_Item.Read())
							{
								itemdraw = dr_Item["ItemDrawNum"].ToString();
								name = dr_Item["ItemName"].ToString();
							}
							dr_Item.Close();
							comm.Parameters.Clear();

							str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
								@itemnum,@itemdrawnum,@itemname,0,'14000000','소재','외주창고','0',@Quantity,@Date,'품질검사',@HistoryIndex)";
				

							comm.Parameters.Add("@itemnum",item);									//품목번호
							comm.Parameters.Add("@itemdrawnum", itemdraw);								//도면번호
							comm.Parameters.Add("@itemname",name);									//품목명
							//comm.Parameters.Add("@ProcessSequenceNum",strArrValue[17].ToString());											//공정순서
							//comm.Parameters.Add("@ProcessCode",strArrValue[17].ToString());											//공정코드
							//comm.Parameters.Add("@ProcessName",strArrValue[17].ToString());												//공정명
							//comm.Parameters.Add("@Division",0);														//입출고구분
							comm.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1].ToString())*quantity);					//입출고 수량
							comm.Parameters.Add("@Date",DateTime.Parse(wdcQualityInspectionCompleteDate.Text.Trim()).ToShortDateString());								//입출고일
							//comm.Parameters.Add("@HistoryDivision","구매납품");										//원장구분
							comm.Parameters.Add("@HistoryIndex",int.Parse(strArrValue[0].ToString()));	
							comm.CommandText = str;
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();
							
						}
						else //그렇지 않으면 그 품목의 최상위 공정순서를 찾아서 그 공정이 외주인지 판단한다. 카운트가 0이면 외주가 아니므로 출고시킨다.
						{
							// 스택1에 있는 공정순서를 하나씩꺼낸다.
							while(st1.Count > 0)
							{
								sequencenum = int.Parse(st1.Pop().ToString());
								str = " SELECT  count(*) FROM PSI_MT WHERE RecodingState = 1 and WorkDistinction <> '외주' and ItemNum = @item and ProcessSequenceNum = @num";
								comm.Parameters.Add("@item",SqlDbType.VarChar).Value = item;//품목번호
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;//공정순서
								comm.CommandText=str;
								int aaa = int.Parse(comm.ExecuteScalar().ToString());
								comm.Parameters.Clear();
								

								//품번,도번,품명 구한다
								string itemdraw=""; string name="";
								str = "Select * From II_MT Where ItemNum = @num";
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = item;
								comm.CommandText =str;
								SqlDataReader dr_Item = comm.ExecuteReader();
								while(dr_Item.Read())
								{
									itemdraw = dr_Item["ItemDrawNum"].ToString();
									name = dr_Item["ItemName"].ToString();
								}
								dr_Item.Close();
								comm.Parameters.Clear();

								//공정코드와 공정명을 구한다
								string processcode="";string processname="";
								str = " SELECT  ProcessSequenceNum, ProcessCode, SmallClassificationName " +
									" FROM      PSI_MT INNER JOIN " +
									" PUC_MT ON ProcessCode = SmallClassificationCode " +
									" WHERE PSI_MT.RecodingState = 1 AND ItemNum = @itemnum and ProcessSequenceNum = @num";
								comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
								comm.Parameters.Add("@num",SqlDbType.VarChar).Value = sequencenum;
								comm.CommandText=str;
								SqlDataReader dr_code = comm.ExecuteReader();
								while(dr_code.Read())
								{
									processcode = dr_code["ProcessCode"].ToString();//공정코드
									processname = dr_code["SmallClassificationName"].ToString();//공정명
								}
								dr_code.Close();
								comm.Parameters.Clear();

								// 외주가 아니면 출고
								if(aaa != 0)
								{
									str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
									@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'외주창고','0',@Quantity,@Date,'품질검사',@HistoryIndex)";
				

									comm.Parameters.Add("@itemnum",item);									//품목번호
									comm.Parameters.Add("@itemdrawnum", itemdraw);								//도면번호
									comm.Parameters.Add("@itemname",name);									//품목명
									comm.Parameters.Add("@ProcessSequenceNum",sequencenum);											//공정순서
									comm.Parameters.Add("@ProcessCode",processcode);											//공정코드
									comm.Parameters.Add("@ProcessName",processname);												//공정명
									//comm.Parameters.Add("@Division",0);														//입출고구분
									comm.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1].ToString())*quantity);					//입출고 수량
									comm.Parameters.Add("@Date",DateTime.Parse(wdcQualityInspectionCompleteDate.Text.Trim()).ToShortDateString());								//입출고일
									//comm.Parameters.Add("@HistoryDivision","구매납품");										//원장구분
									comm.Parameters.Add("@HistoryIndex",int.Parse(strArrValue[0].ToString()));	
									comm.CommandText = str;
									comm.ExecuteNonQuery();
									comm.Parameters.Clear();
									
									st1.Clear();
									break;
								}
								else//외주이면 외주단가에서 지금 찾은 공정이랑 종료공정이 동일한것이 있는지 살펴본다. 있으면 출고시키고 없으면 이전공정을 찾는다
								{
									// 외주단가에서 동일회사 외주공정인지 파악한다.
									str = "select count(*) From UCI_MT Where RecodingState = 1 and ItemNum = @itemnum and EndProcessCode = @code and BusinessRegistrationNum = @com";
									comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = item;
									comm.Parameters.Add("@code",SqlDbType.VarChar).Value = processcode;
									comm.Parameters.Add("@com",SqlDbType.VarChar).Value = strArrValue[19].ToString() ;
									comm.CommandText = str;
									int find = int.Parse(comm.ExecuteScalar().ToString());
									comm.Parameters.Clear();
									

									// 동일회사가 아니면 출고
									if(find == 0)
									{
										// 데이터셋에 추가하기위한 Row
										str=@"Insert into SH_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,StoreName,Division,Quantity,[Date],HistoryDivision,HistoryIndex) values(
									@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,'외주창고','0',@Quantity,@Date,'품질검사',@HistoryIndex)";
				

										comm.Parameters.Add("@itemnum",item);									//품목번호
										comm.Parameters.Add("@itemdrawnum", itemdraw);								//도면번호
										comm.Parameters.Add("@itemname",name);									//품목명
										comm.Parameters.Add("@ProcessSequenceNum",sequencenum);											//공정순서
										comm.Parameters.Add("@ProcessCode",processcode);											//공정코드
										comm.Parameters.Add("@ProcessName",processname);													//공정명
										//comm.Parameters.Add("@Division",0);														//입출고구분
										comm.Parameters.Add("@Quantity",decimal.Parse(strArrValue[1].ToString())*quantity);					//입출고 수량
										comm.Parameters.Add("@Date",DateTime.Parse(wdcQualityInspectionCompleteDate.Text.Trim()).ToShortDateString());								//입출고일
										//comm.Parameters.Add("@HistoryDivision","구매납품");										//원장구분
										comm.Parameters.Add("@HistoryIndex",int.Parse(strArrValue[0].ToString()));	
										comm.CommandText = str;
										comm.ExecuteNonQuery();
										comm.Parameters.Clear();

										st1.Clear();
										break;
									}
								}
							}
						}
					}
				}
			}
		}


		/// <summary>
		/// 자가관련 수정함수
		/// </summary>
		private bool SelfUpdate(SqlConnection conn, SqlTransaction tr)
		{
			string StockQuantity = "False";

			SqlCommand comm = new SqlCommand();
			comm.Connection=conn;
			comm.Transaction = tr;

			
			int mon = DateTime.Now.Month;
			Table table = new Table(mon);
			int index  =int.Parse(hdIndex.Value.Substring(12,1));			


			//작업계획의 완료수량을 변경한다
			string str = "Update WDWP_HT Set WorkCompletionQuantity = WorkCompletionQuantity - @WorkCompletionQuantity Where WCDailyWorkPlanHistoryIndex = @Index";
			
			comm.CommandText = str;
			comm.Parameters.Add("@WorkCompletionQuantity",decimal.Parse(hdRequestQuantity.Value));
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex2").Value.ToString()));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//현재 적합수량으로 변경한다.
			str = "Update WDWP_HT Set WorkCompletionQuantity = WorkCompletionQuantity + @WorkCompletionQuantity Where WCDailyWorkPlanHistoryIndex = @Index";
			comm.CommandText = str;
			comm.Parameters.Add("@WorkCompletionQuantity",decimal.Parse(txtSuccessQuantity.Text));
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex2").Value.ToString()));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			//완료수량이 계획수량보다 작으면 진행상태를 '진행'으로 변경시킨다.
			decimal Plan = 0;
			decimal Complete = 0;

			str = "Select WorkPlanQuantity,WorkCompletionQuantity From WDWP_HT Where  WCDailyWorkPlanHistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex2").Value.ToString()));
			SqlDataReader dr_Quantity = comm.ExecuteReader();
			while(dr_Quantity.Read())
			{
				Plan = decimal.Parse(dr_Quantity["WorkPlanQuantity"].ToString());
				Complete = decimal.Parse(dr_Quantity["WorkCompletionQuantity"].ToString());
			}
			dr_Quantity.Close();
						
			if(Complete - Plan < 0)
				str = "Update OO_HT Set ProgressCondition = '진행' Where  OutSideOrderHistoryIndex = @index";
			else
				str = "Update OO_HT Set ProgressCondition = '완료' Where  OutSideOrderHistoryIndex = @index";

			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();



			


			//생산창고수량 변경
			// 진척비율을 구해야 한다
			decimal rate = 100;	//진척비율(공정순서에 있는 진척비율을 넣어둔다.

			str = "Select OutsideOrderRate, ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @item and ProcessSequenceNum = @num";
			comm.CommandText =str;
			comm.Parameters.Add("@item",DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString());
			comm.Parameters.Add("@num",int.Parse(DataGrid1.Rows[index].Cells.FromKey("종료공정순서").Value.ToString()));
			SqlDataReader dr_Rate = comm.ExecuteReader();
			while(dr_Rate.Read())
			{
				rate = decimal.Parse(dr_Rate["ProgressRate"].ToString());
			}
			dr_Rate.Close();
			comm.Parameters.Clear();

			str = "select StandardUnitCost from II_MT where RecodingState = 1 and ItemNum= @itemnum";
			comm.Parameters.Add("@itemnum",DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString());
			comm.CommandText =str;	
			//품목정보테이블에서 현재 수정하고자하는 품목의 단가를 가지고있는 변수
			decimal unitcost = Decimal.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			//기존테이블에서 이전수량을 마이너스 증가
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = -(decimal.Parse(hdRequestQuantity.Value)*unitcost)* (rate/100);
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -decimal.Parse(hdRequestQuantity.Value);
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("종료공정코드").Value.ToString();
			comm.CommandText = table.InProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새로변경되는 수정한 수량을 증가
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(txtSuccessQuantity.Text)*unitcost*(rate/100);
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(txtSuccessQuantity.Text);
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("종료공정코드").Value.ToString();
			comm.CommandText = table.InProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			//생산창고 입고 수정수량을 품질검사 수정한 수량만큼 업데이트
			str = "Update SH_HT Set Quantity = @Quantity Where HistoryDivision = '작업일보' and HistoryIndex = @index";
			comm.Parameters.Add("@Quantity",decimal.Parse(txtSuccessQuantity.Text));
			comm.Parameters.Add("@index", int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex1").Value.ToString()));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();



			str = "select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ItemNum = @ItemNum";
			comm.Parameters.Add("@ItemNum",DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString());
			comm.CommandText = str;
			int ProSeq = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			

				

			//마이너스재고를 허용하지 않으면 
			if(MinusStore() == false)
			{
				string strSQL="";
				strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code";
				comm.Parameters.Add("@ItemNum", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@code", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("종료공정코드").Value.ToString();
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
				comm.Parameters.Clear();
						
				
				string ItemName = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						StockQuantity = "True";
				}
				reader.Close();

				if(StockQuantity == "True")
					throw new Exception(ItemName + " 의 재고량이 부족합니다.");
			}
			

			if(StockQuantity == "True")
				return false;
			else
				return true;
		}

		/// <summary>
		/// 마이너스 재고 허용여부
		/// </summary>
		/// <returns></returns>
		private bool MinusStore()
		{
			bool aa = true;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["Day"]);
			conn.Open();
			string str = "Select MinusRowMaterialPermissionUsed From SCS_T";
			SqlCommand comm =  new SqlCommand(str,conn);
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = bool.Parse(dr["MinusRowMaterialPermissionUsed"].ToString());
			}
			dr.Close();
			conn.Close();

			return aa;
		}


		private int Division(string ItemNum, SqlConnection con, SqlTransaction trans)
		{
			string aa="";

			SqlCommand comm = new SqlCommand();
			comm.Connection=con;
			comm.Transaction = trans;

			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = ItemNum.ToString();
			comm.CommandText = str;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			dr.Close();

			if(aa.ToString() == "원자재")
				return 1;
			else if(aa.ToString() =="상품")
				return 2;
			else 
				throw new Exception(ItemNum+"의 자산분류가 명확하지 않습니다!");
		}

		

	}
}
