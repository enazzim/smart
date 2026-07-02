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
using Infragistics.WebUI.UltraWebGrid;
using System.Text;

using System.Configuration;

namespace KIT_ERP.BuyingOutside
{
	public class QualityInspectionPresentCondition : KIT_ERP.QualityInspection.QualityInspection_BaseClass
	{
		protected System.Web.UI.WebControls.TextBox txtSuccessQuantity;
		protected string strEditMsg;
		protected System.Web.UI.WebControls.Button Button3;
		protected System.Web.UI.WebControls.DropDownList ddlProcessDiv;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtStartDate;
		protected Infragistics.WebUI.WebSchedule.WebDateChooser txtEndDate;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid DataGrid1;
		protected System.Web.UI.WebControls.Literal Literal1;
		protected System.Web.UI.WebControls.Literal Literal2;
		protected System.Web.UI.WebControls.Literal Literal3;
		protected System.Web.UI.WebControls.Literal Literal4;
		protected System.Web.UI.WebControls.Literal Literal5;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnReset;	
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdSucessQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdIncongruityQuantity;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdIndex;
		protected ArrayList arrlist = new ArrayList();
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBeforeSucessQauntity;
		protected System.Web.UI.WebControls.Button btExcel;
		protected System.Web.UI.WebControls.Button btCancle;
		protected Infragistics.WebUI.UltraWebGrid.ExcelExport.UltraWebGridExcelExporter UltraWebGridExcelExporter1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdUpdateRegion;
		protected System.Web.UI.WebControls.DropDownList ddlDivision;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldYear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdOldMonth;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdBeforeApplyUintCost;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl  ItemSearchControl1;
		protected KIT_ERP.Common.CompanySearchControl.CompanySearchControl CSC1;
		protected int index;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdyear;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdmon;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdyear1;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdmon1;
		protected string[] vals ;
	
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
			CSC1.UnitCostDistinction ="구매외주";

			if (!IsPostBack) 
			{
				btnReset.Attributes.Add("onclick", "ResetTextBox();ResettxtBox();");
				btCancle.Attributes.Add("onclick","return OK('선택한 품목들에 대하여 검사를 취소');");
			}
			else 
			{
				
				Response.Write(" 	<script >  var MonthCloseing = \"0\";    function EditRegistCheck() { document.QualityInspectionPresentCondition.hdUpdateRegion.value = document.QualityInspectionPresentCondition.UpdateRegion.value;  if(document.QualityInspectionPresentCondition.txtSuccessQuantity.value == \"\") {   alert(\"적합수량에 수량이 기입되지 않았습니다.\");     return false;}   if(confirm(\"현재 품목을 수정 하시겠습니까?\")) return true; else return false; }</script> ");
				
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
			this.DataGrid1.PageIndexChanged += new Infragistics.WebUI.UltraWebGrid.PageIndexChangedEventHandler(this.DataGrid1_PageIndexChanged);
			this.btExcel.Click += new System.EventHandler(this.btExcel_Click);
			this.Button3.Click += new System.EventHandler(this.Button3_Click);
			this.btCancle.Click += new System.EventHandler(this.btCancle_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion





		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			DataGrid1.DisplayLayout.Pager.CurrentPageIndex = 1;
			SByte sbyteProcessDiv = Convert.ToSByte(ddlProcessDiv.SelectedItem.Value);
			
			// 검색 객체 생성
			KIT_ERP.QualityInspection.QualityInspection_Search qis = new KIT_ERP.QualityInspection.QualityInspection_Search
				(
				PageNames.품질검사현황
				, ItemSearchControl1.ItemNum
				, ItemSearchControl1.ItemDrawNum
				, ItemSearchControl1.ItemName
				, txtStartDate.Text
				, txtEndDate.Text
				, CSC1.Company
				, CSC1.BusinessRegistrationNum
				, sbyteProcessDiv
				, ddlDivision.SelectedItem.Value
				);

			DataTable dt_Source = qis.GetDataSet().Tables[0];
			
			// 그리드 하단 통계 출력 메소드 호출
			this.SUM_and_AVG_(dt_Source);
			
			DataGrid1.DataSource = dt_Source.DefaultView;
			DataGrid1.DataKeyField = "번호";
			DataGrid1.DataBind();

			Session["DT1"] = dt_Source;
		}	




		// 그리드 하단 레코드 통계 계산하여 출력하는 메소드
		private void SUM_and_AVG_(DataTable dt)
		{
			decimal dE_totalRequestQuantity = 0;
			decimal dE_totalSuitabilityQuantity = 0;
			decimal dE_totalUnSuitabilityQuantity = 0;
			decimal dE_TotalPPM = 0;

			foreach ( DataRow drow in dt.Rows )
			{
				dE_totalRequestQuantity += Convert.ToDecimal(drow[8]);
				dE_totalSuitabilityQuantity +=  Convert.ToDecimal(drow[22]);
				dE_totalUnSuitabilityQuantity +=  Convert.ToDecimal(drow[24]);
			}

			if ( dE_totalRequestQuantity != 0 )
			{
				dE_TotalPPM = ( dE_totalUnSuitabilityQuantity / dE_totalRequestQuantity ) * 1000000;

				Literal1.Text = "총 <b>\" " + dt.Rows.Count.ToString() + " \"</b> 건" ;
				Literal2.Text = "의뢰수량 : <b>" + dE_totalRequestQuantity.ToString().Substring(0, dE_totalRequestQuantity.ToString().IndexOf(".") + 2) + "</b>";
				Literal3.Text = "적합수량 : <b>" + dE_totalSuitabilityQuantity.ToString().Substring(0, dE_totalSuitabilityQuantity.ToString().IndexOf(".") + 2) + "</b>";
				Literal4.Text = "부적합수량 : <b>" + dE_totalUnSuitabilityQuantity.ToString().Substring(0, dE_totalUnSuitabilityQuantity.ToString().IndexOf(".") + 2 ) + "</b>";

			
				if (dE_TotalPPM > 0)
				{
					try
					{
						Literal5.Text = "<b>" + dE_TotalPPM.ToString().Substring( 0, (dE_TotalPPM.ToString()).IndexOf(".") + 3 ) + "</b> PPM";
					}
					catch
					{
						Literal5.Text = "<b>" + dE_TotalPPM + "</b> PPM";	
					}
				}
				else 
					Literal5.Text = "<b>0</b> PPM";
			}
		}


		// aspx파일 template Row 에서 사용
		protected DataSet DDL_DataSource
		{
			get
			{
				KIT_ERP.QualityInspection.QualityInspection_InputReady qi = new  KIT_ERP.QualityInspection.QualityInspection_InputReady();
				DataSet ds = qi.DDL_DataSource();
				
				return ds;
			}
		}



		private void DataGrid1_PageIndexChanged(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
		{
			DataGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
			DataGrid1.DataSource = ((DataTable)Session["DT1"]).DefaultView;
			DataGrid1.DataBind();
		}

		// 업데이트( __doPostBack 에 사용된 버튼 이벤트 )
		private void Button3_Click(object sender, System.EventArgs e)
		{
			index = int.Parse(hdIndex.Value);

			vals =  Request.Params["hidden_TemplateRow_UpdateValue"].ToString().Split('〉');
			
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			con.Open();
			
			SqlTransaction trans = con.BeginTransaction();
			
			try
			{
						
				if(MonthClosing())
				{
					//현재합격수량과 불량수량 저장
					arrlist.Add(hdBeforeSucessQauntity.Value);
					arrlist.Add(hdIncongruityQuantity.Value);

					//if(decimal.Parse(arrlist[0].ToString()) != decimal.Parse(hdSucessQuantity.Value))
					//{
					//구매관련수정처리
					if(vals[12].ToString() == "구매")
					{
						BuyingUpdate(con,trans);
						DeliveryHistoryUpdate(con,trans,vals);	
						BuyingDeliveryUpdate(con,trans,vals);					//구매가입고 금액수정
					}
					else if(vals[12].ToString() == "외주")
					{	//외주관련수정처리
						OutSideUpdate(con,trans);
						DeliveryHistoryUpdate(con,trans, vals);					//
						OutSideDeliveryUpdate(con,trans,vals);
					}
					else
					{
						//throw new Exception("자가품목은 검사취소가 불가능 합니다. 작업일보현황에서 삭제하여주세요!");
						//자가처리
						SelfUpdate(con,trans);				
					}
					//}

					KIT_ERP.QualityInspection.QualityInspection_Edit qiEdit = new  KIT_ERP.QualityInspection.QualityInspection_Edit(vals);
					qiEdit.Edit();
			

					//btnSearch_Click(this.btnSearch, null);
					Search();

					hdSucessQuantity.Value = "0";
					hdIncongruityQuantity.Value = "0";
					hdIndex.Value = "0";
					trans.Commit();
				}
				else
				{
					throw new Exception("월마감이 되어 수정이 불가능 합니다!");
				}

				
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

		}

		private void Search()
		{
			SByte sbyteProcessDiv = Convert.ToSByte(ddlProcessDiv.SelectedItem.Value);
			
			// 검색 객체 생성
			KIT_ERP.QualityInspection.QualityInspection_Search qis = new KIT_ERP.QualityInspection.QualityInspection_Search
				(
				PageNames.품질검사현황
				, ItemSearchControl1.ItemNum
				, ItemSearchControl1.ItemDrawNum
				, ItemSearchControl1.ItemName
				, txtStartDate.Text
				, txtEndDate.Text
				, CSC1.Company
				, CSC1.BusinessRegistrationNum
				, sbyteProcessDiv
				, ddlDivision.SelectedItem.Value
				);

			DataTable dt_Source = qis.GetDataSet().Tables[0];
			
			// 그리드 하단 통계 출력 메소드 호출
			this.SUM_and_AVG_(dt_Source);
			
			DataGrid1.DataSource = dt_Source.DefaultView;
			DataGrid1.DataKeyField = "번호";
			DataGrid1.DataBind();

			Session["DT1"] = dt_Source;
		}

		private void DeliveryHistoryUpdate(SqlConnection conn,SqlTransaction tr, string[] list)
		{
			SqlCommand comm = new SqlCommand();
			comm.Connection=conn;
			comm.Transaction = tr;

			string str = "";
			
			if(DataGrid1.Rows[index].Cells.FromKey("historysection1").Text == "구매")
			{
				if(Division(DataGrid1.Rows[index].Cells.FromKey("ItemNum").Text) == "원자재")
					str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason,[Date], UpdatePerson, UpdatePersonID, HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,0,'14000000','소재',@CompanyName, @BusinessRegistrationNum, @Quantity,@UnitCost,@UpdateHistoryReason,@Date,@Person, @PersonID, '구매납품',@HistoryIndex)";
				else
					str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason,[Date], UpdatePerson, UpdatePersonID, HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,99,'14009999','최종품',@CompanyName, @BusinessRegistrationNum, @Quantity,@UnitCost,@UpdateHistoryReason,@Date,@Person, @PersonID, '구매납품',@HistoryIndex)";

				comm.Parameters.Add("@itemnum",DataGrid1.Rows[index].Cells.FromKey("ItemNum").Text);									//품목번호
				comm.Parameters.Add("@itemdrawnum", DataGrid1.Rows[index].Cells.FromKey("ItemDrawNum").Text);								//도면번호
				comm.Parameters.Add("@itemname",DataGrid1.Rows[index].Cells.FromKey("ItemName").Text);									//품목명
				comm.Parameters.Add("@CompanyName", DataGrid1.Rows[index].Cells.FromKey("CompanyName").Text);								//업체명
				comm.Parameters.Add("@BusinessRegistrationNum",DataGrid1.Rows[index].Cells.FromKey("BusinessCompanyNum").Text);
				comm.Parameters.Add("@Quantity",decimal.Parse(list[1]));					//입출고 수량
				comm.Parameters.Add("@UnitCost",decimal.Parse(list[21]));					//입출고 수량
				comm.Parameters.Add("@UpdateHistoryReason",hdUpdateRegion.Value);
				comm.Parameters.Add("@Date",DateTime.Now.ToShortDateString());								//입출고일
				comm.Parameters.Add("@Person",Session["UserName"].ToString());
				comm.Parameters.Add("@PersonID",Session["ID"].ToString());
				comm.Parameters.Add("@HistoryIndex",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex1").Text));
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				//SH_HT원장 수정
				str = @"Update SH_HT Set Quantity = @Quantity where HistoryDivision ='품질검사' and HistoryIndex  = @HistoryIndex";
				comm.Parameters.Add("@Quantity",decimal.Parse(list[1]));					//입출고 수량
				comm.Parameters.Add("@HistoryIndex",int.Parse(list[0]));
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

			}
			else if(DataGrid1.Rows[index].Cells.FromKey("historysection1").Text == "외주")
			{
				str=@"Insert into BOS_HT (ItemNum,ItemDrawNum,ItemName,ProcessSequenceNum,ProcessCode,ProcessName,CompanyName, BusinessRegistrationNum,Quantity,UnitCost,UpdateHistoryReason,[Date], UpdatePerson, UpdatePersonID, HistoryDivision,HistoryIndex) values(
				@itemnum,@itemdrawnum,@itemname,@ProcessSequenceNum,@ProcessCode,@ProcessName,@CompanyName, @BusinessRegistrationNum, @Quantity,@UnitCost,@UpdateHistoryReason,@Date,@Person, @PersonID, '외주납품',@HistoryIndex)";

				comm.Parameters.Add("@itemnum",DataGrid1.Rows[index].Cells.FromKey("ItemNum").Text);									//품목번호
				comm.Parameters.Add("@itemdrawnum", DataGrid1.Rows[index].Cells.FromKey("ItemDrawNum").Text);								//도면번호
				comm.Parameters.Add("@itemname",DataGrid1.Rows[index].Cells.FromKey("ItemName").Text);									//품목명
				comm.Parameters.Add("@ProcessSequenceNum",int.Parse(DataGrid1.Rows[index].Cells.FromKey("종료공정순서").Text));											//공정순서
				comm.Parameters.Add("@ProcessCode",DataGrid1.Rows[index].Cells.FromKey("종료공정코드").Text);											//공정코드
				comm.Parameters.Add("@ProcessName",DataGrid1.Rows[index].Cells.FromKey("종료공정명").Text);												//공정명
				comm.Parameters.Add("@CompanyName", DataGrid1.Rows[index].Cells.FromKey("CompanyName").Text);								//업체명
				comm.Parameters.Add("@BusinessRegistrationNum",DataGrid1.Rows[index].Cells.FromKey("BusinessCompanyNum").Text);
				comm.Parameters.Add("@Quantity",decimal.Parse(list[1]));					//입출고 수량
				comm.Parameters.Add("@UnitCost",decimal.Parse(list[21]));	
				comm.Parameters.Add("@UpdateHistoryReason",hdUpdateRegion.Value);
				comm.Parameters.Add("@Date",DateTime.Now.ToShortDateString());								//입출고일
				comm.Parameters.Add("@Person",Session["UserName"].ToString());
				comm.Parameters.Add("@PersonID",Session["ID"].ToString());
				comm.Parameters.Add("@HistoryIndex",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex1").Text));
				
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				InsertSH_HT(conn,tr,vals);
			}
		}

		private void InsertSH_HT(SqlConnection conn, SqlTransaction tr, string[] list)
		{

			

			SqlCommand comm = new SqlCommand();
			comm.Connection=conn;
			comm.Transaction = tr;

			
			//지금 공정순서보다 아래에 공정순서가 있는지 없는지를 구한다
			int sequence1 = 0;	//공정순서
			string Code = "";	// 공정코드
			string Name = "";	// 공정명
			string str = "Select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ProcessSequenceNum < @num and ItemNum = @itemnum";
			
			comm.Parameters.Add("@num", int.Parse(DataGrid1.Rows[index].Cells.FromKey("종료공정순서").Text));
			comm.Parameters.Add("@itemnum",DataGrid1.Rows[index].Cells.FromKey("ItemNum").Text);
			comm.CommandText = str;
			if(comm.ExecuteScalar() == null || comm.ExecuteScalar().ToString().Trim() == "")
				sequence1 = 0;
			else
				sequence1 = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();
			
			str = "Select ProcessCode From PSI_MT Where RecodingState = 1 and ProcessSequenceNum = @num and ItemNum = @itemnum";
			comm.Parameters.Add("@num", sequence1);
			comm.Parameters.Add("@itemnum",DataGrid1.Rows[index].Cells.FromKey("ItemNum").Text);
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
			string st_ItemNum = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Text;

			if(sequence1 != 0)
			{
				str = @"Update SH_HT Set Quantity = @Quantity where HistoryDivision ='품질검사' and HistoryIndex  = @HistoryIndex";
				comm.Parameters.Add("@Quantity",decimal.Parse(list[1]));					//입출고 수량
				comm.Parameters.Add("@HistoryIndex",int.Parse(list[0]));
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

							str = @"Update SH_HT Set Quantity = @Quantity where ItemNum = @itemnum and  HistoryDivision ='품질검사' and HistoryIndex  = @HistoryIndex";
							comm.Parameters.Add("@itemnum",item);									//품목번호
							comm.Parameters.Add("@Quantity",decimal.Parse(list[1])*quantity);					//입출고 수량
							comm.Parameters.Add("@HistoryIndex",int.Parse(list[0]));
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
									str = @"Update SH_HT Set Quantity = @Quantity where ItemNum = @itemnum and  HistoryDivision ='품질검사' and HistoryIndex  = @HistoryIndex";
									comm.Parameters.Add("@itemnum",item);									//품목번호
									comm.Parameters.Add("@Quantity",decimal.Parse(list[1])*quantity);					//입출고 수량
									comm.Parameters.Add("@HistoryIndex",int.Parse(list[0]));
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
									comm.Parameters.Add("@com",SqlDbType.VarChar).Value = list[20].Trim() ;
									comm.CommandText = str;
									int find = int.Parse(comm.ExecuteScalar().ToString());
									comm.Parameters.Clear();
									

									// 동일회사가 아니면 출고
									if(find == 0)
									{
										// 데이터셋에 추가하기위한 Row
										str = @"Update SH_HT Set Quantity = @Quantity where ItemNum = @itemnum and  HistoryDivision ='품질검사' and HistoryIndex  = @HistoryIndex";
										comm.Parameters.Add("@itemnum",item);									//품목번호
										comm.Parameters.Add("@Quantity",decimal.Parse(list[1])*quantity);					//입출고 수량
										comm.Parameters.Add("@HistoryIndex",int.Parse(list[0]));
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
		private void SelfUpdate(SqlConnection conn,SqlTransaction tr)
		{

			int year = DateTime.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionCompleteDate").Value.ToString()).Year;
			int mon = DateTime.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionCompleteDate").Value.ToString()).Month;
			Table table = new Table(year,mon);

			SqlCommand comm = new SqlCommand();
			

			//작업계획의 완료수량을 변경한다
			string str = "Update WDWP_HT Set WorkCompletionQuantity = WorkCompletionQuantity - @WorkCompletionQuantity Where WCDailyWorkPlanHistoryIndex = @Index";
			
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@WorkCompletionQuantity",decimal.Parse(arrlist[0].ToString()));
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex2").Value.ToString()));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//현재 적합수량으로 변경한다.
			str = "Update WDWP_HT Set WorkCompletionQuantity = WorkCompletionQuantity + @WorkCompletionQuantity Where WCDailyWorkPlanHistoryIndex = @Index";
			comm.CommandText = str;
			comm.Parameters.Add("@WorkCompletionQuantity",decimal.Parse(hdSucessQuantity.Value));
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
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (-(decimal.Parse(arrlist[0].ToString()))*unitcost)* (rate/100);
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(arrlist[0].ToString()));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("종료공정코드").Value.ToString();
			comm.Parameters.Add("@year", year);
			comm.CommandText = table.InProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새로변경되는 수정한 수량을 증가
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(hdSucessQuantity.Value)*unitcost*(rate/100);
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(hdSucessQuantity.Value);
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("종료공정코드").Value.ToString();
			comm.Parameters.Add("@year", year);
			comm.CommandText = table.InProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			str = "select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ItemNum = @ItemNum";
			comm.Parameters.Add("@ItemNum",DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString());
			comm.CommandText = str;
			int ProSeq = int.Parse(comm.ExecuteScalar().ToString());

			//conn.Close();
			//해당 품목의 자산분류가 제품이면서 공정이 종료공정이면 입고의뢰원장의 합격수량도 변경한다
			if(Division(DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString()) == "제품")				
			{
				if(ProSeq == int.Parse(DataGrid1.Rows[index].Cells.FromKey("종료공정순서").Value.ToString()))
				{
					//conn.Open();
					str = "Update ISR_HT Set InstorehouseRequestQuantity = @InstorehouseRequestQuantity where HistoryIndex = @HistoryIndex and HistorySection = '작업일보'";
					comm.CommandText = str;
					comm.Parameters.Add("@InstorehouseRequestQuantity", decimal.Parse(hdSucessQuantity.Value));
					comm.Parameters.Add("@HistoryIndex", int.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionHistoryIndex").Value.ToString()));
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					//conn.Close();
				}
			}



			//작업일보 번호를 찾아 생산계획번호를 가져오고
			//이 생산계획번호를 가지고 완료수량을 누적한다.
			//누적완료수량이  생산계획수량보다 많으면 생산계획원장 진행상태를 완료로 처리

			int idx = 0;
			str = "Select HistoryIndex2 From QI_HT Where HistorySection2 = '작업계획' and QualityInspectionHistoryIndex = @index";
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
					str = "Update PP_HT Set  ProductionCompleteQuantity = ProductionCompleteQuantity - @quantity Where ProductionPlanHistoryIndex = @index";
					comm.CommandText = str;
					comm.Parameters.Add("@quantity",decimal.Parse(arrlist[0].ToString()));
					comm.Parameters.Add("@index",int.Parse(num));
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();

					str = "Update PP_HT Set  ProductionCompleteQuantity = ProductionCompleteQuantity + @quantity Where ProductionPlanHistoryIndex = @index";
					comm.CommandText = str;
					comm.Parameters.Add("@quantity",decimal.Parse(hdSucessQuantity.Value));
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


			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code";
				comm.Parameters.Add("@ItemNum",SqlDbType.VarChar).Value =  DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@code", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("종료공정코드").Value.ToString();
				comm.Parameters.Add("@year", year);
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
						
				string StockQuantity = "False";
				string ItemName = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량이 부족합니다.");
				}
			}

			
		}


		/// <summary>
		/// 외주관련 수정함수
		/// </summary>
		private void OutSideUpdate(SqlConnection conn,SqlTransaction tr)
		{
			//외주발주원장의 잔량누적변경을 한다
			//외주발주원장 이전합격수량을 잔량에서 더해준다
			string str = "Update OO_HT Set RemainQuantity = RemainQuantity + @RemainQuantity where OutSideOrderHistoryIndex = @index";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.CommandText = str;
			comm.Transaction = tr;
			comm.Parameters.Add("@RemainQuantity",decimal.Parse(arrlist[0].ToString()));
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex2").Value.ToString()));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			
			//외주발주원장 합격수량을 잔량에 빼준다
			str = "Update OO_HT Set RemainQuantity = RemainQuantity - @RemainQuantity where OutSideOrderHistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@RemainQuantity",decimal.Parse(hdSucessQuantity.Value));
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex2").Value.ToString()));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//잔량이 발주수량보다 작으면 진행상태를 '진행'으로 변경시킨다.
			decimal Order = 0;
			decimal Remain = 0;

			str = "Select OrderQuantity,RemainQuantity From OO_HT Where  OutSideOrderHistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex2").Value.ToString()));
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Order = decimal.Parse(dr["OrderQuantity"].ToString());
				Remain = decimal.Parse(dr["RemainQuantity"].ToString());
			}
			dr.Close();
						
			if(Remain > 0)
				str = "Update OO_HT Set ProgressCondition = '진행' Where  OutSideOrderHistoryIndex = @index";
			else
				str = "Update OO_HT Set ProgressCondition = '완료' Where  OutSideOrderHistoryIndex = @index";

			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();



			// 최종공정이면 생산계획원장에 완료수량을 변경
			// 진행상태 변경
			if(LastProcess(conn,tr))
			{
				int idx = 0;
				str = "Select HistoryIndex2 From QI_HT Where HistorySection2 = '외주발주' and QualityInspectionHistoryIndex = @index";
				comm.CommandText = str;
				comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionHistoryIndex").Value.ToString()));
				SqlDataReader reader = comm.ExecuteReader();
				while(reader.Read())
				{
					idx = int.Parse(reader["HistoryIndex2"].ToString());
				}
				reader.Close();
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

							str = "Update PP_HT Set  ProductionCompleteQuantity = ProductionCompleteQuantity - @quantity Where ProductionPlanHistoryIndex = @index";
							comm.CommandText = str;
							comm.Parameters.Add("@quantity",decimal.Parse(arrlist[0].ToString()));
							comm.Parameters.Add("@index",int.Parse(num));
							comm.ExecuteNonQuery();
							comm.Parameters.Clear();

							str = "Update PP_HT Set  ProductionCompleteQuantity = ProductionCompleteQuantity + @quantity Where ProductionPlanHistoryIndex = @index";
							comm.CommandText = str;
							comm.Parameters.Add("@quantity",decimal.Parse(hdSucessQuantity.Value));
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
			}



			int year = DateTime.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionCompleteDate").Value.ToString()).Year;
			int mon = DateTime.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionCompleteDate").Value.ToString()).Month;
				
			Table table = new Table(year,mon);


			//매입테이블의 금액을 수정시킨다(기존금액 마이너스입고,현재금액 플러스 입고)
			decimal BeforeCost = decimal.Parse(hdBeforeApplyUintCost.Value)*decimal.Parse(hdBeforeSucessQauntity.Value);
			comm.Parameters.Add("@comnum",DataGrid1.Rows[index].Cells.FromKey("BusinessCompanyNum").Text);
			comm.Parameters.Add("@year",year);
			comm.Parameters.Add("@cost", -BeforeCost);
			comm.CommandText = table.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			decimal NowCost = decimal.Parse(vals[1])*decimal.Parse(vals[21]);
			comm.Parameters.Add("@comnum",DataGrid1.Rows[index].Cells.FromKey("BusinessCompanyNum").Text);
			comm.Parameters.Add("@year",year);
			comm.Parameters.Add("@cost", NowCost);
			comm.CommandText = table.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();


			//생산창고수량 변경
			// 진척비율을 구해야 한다
			int sequence = int.Parse(DataGrid1.Rows[index].Cells.FromKey("종료공정순서").Value.ToString());
			
			decimal Rate = 100;	//진척비율(공정순서에 있는 진척비율을 넣어둔다.

			str = "Select OutsideOrderRate, ProgressRate From PSI_MT Where RecodingState = 1 and ItemNum = @item and ProcessSequenceNum = @num";
			comm.CommandText =str;
			comm.Parameters.Add("@item",SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@num",SqlDbType.Int).Value = sequence;
			
			SqlDataReader dr_Rate1 = comm.ExecuteReader();
			while(dr_Rate1.Read())
			{
				Rate = decimal.Parse(dr_Rate1["ProgressRate"].ToString());
			}
			dr_Rate1.Close();
			comm.Parameters.Clear();

			str = "select StandardUnitCost from II_MT where RecodingState = 1 and ItemNum= @itemnum";
			comm.Parameters.Add("@itemnum",DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString());
			comm.CommandText =str;	
			//품목정보테이블에서 현재 수정하고자하는 품목의 단가를 가지고있는 변수
			decimal UnitCost = Decimal.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			//기존테이블에서 이전수량을 마이너스 증가
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = (-(decimal.Parse(arrlist[0].ToString()))*UnitCost)* (Rate/100);
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = -(decimal.Parse(arrlist[0].ToString()));
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("종료공정코드").Value.ToString();
			comm.Parameters.Add("@year",year);
			comm.CommandText = table.InProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//새로변경되는 수정한 수량을 증가
			comm.Parameters.Add("@cost", SqlDbType.Decimal).Value = decimal.Parse(hdSucessQuantity.Value)*UnitCost*(Rate/100);
			comm.Parameters.Add("@quantity", SqlDbType.Decimal).Value = decimal.Parse(hdSucessQuantity.Value);
			comm.Parameters.Add("@num", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
			comm.Parameters.Add("@code", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("종료공정코드").Value.ToString();
			comm.Parameters.Add("@year",year);
			comm.CommandText = table.InProductionTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			str = "select Max(ProcessSequenceNum) From PSI_MT Where RecodingState = 1 and ItemNum = @ItemNum";
			comm.Parameters.Add("@ItemNum",DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString());
			comm.CommandText = str;
			int ProSeq = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			//conn.Close();
			
			//해당 품목의 자산분류가 제품이면서 공정이 종료공정이면 입고의뢰원장의 합격수량도 변경한다
			if(Division(DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString()) == "제품")				
			{
				

				if(ProSeq == int.Parse(DataGrid1.Rows[index].Cells.FromKey("종료공정순서").Value.ToString()))
				{
					//conn.Open();
					str = "Update ISR_HT Set InstorehouseRequestQuantity = @InstorehouseRequestQuantity where HistoryIndex = @HistoryIndex and HistorySection = '품질검사'";
					comm.CommandText = str;
					comm.Parameters.Add("@InstorehouseRequestQuantity", decimal.Parse(hdSucessQuantity.Value));
					comm.Parameters.Add("@HistoryIndex", int.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionHistoryIndex").Value.ToString()));
					comm.ExecuteNonQuery();
					comm.Parameters.Clear();
					//conn.Close();
				}
			}


			//마이너스 재고허용 여부
			if(MinusStore() == false)
			{
				string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @code and [Year] = @year";
				comm.Parameters.Add("@ItemNum", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@code", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("종료공정코드").Value.ToString();
				comm.Parameters.Add("@year",year);
									
				comm.CommandText = strSQL;
				SqlDataReader reader = comm.ExecuteReader();
				comm.Parameters.Clear();
						
				string StockQuantity = "False";
				string ItemName = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				if(reader.Read())
				{
					if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
					{
						StockQuantity = "True";
					}
				}
				reader.Close();

				if(StockQuantity == "True")
				{
					throw new Exception(ItemName + " 의 재고량이 부족합니다.");
				}
			}
		}

		/// <summary>
		/// 해당 품목의 공정이 최종공정인지 파악
		/// </summary>
		/// <returns></returns>
		private bool LastProcess(SqlConnection conn,SqlTransaction tr)
		{
			string str = "Select count(*) From PSI_MT Where RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = @Code and ProcessSequenceNum > @num";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@ItemNum",vals[17].ToString());
			comm.Parameters.Add("@Code",vals[26].ToString());
			comm.Parameters.Add("@num",int.Parse(vals[23].ToString()));
			int count = int.Parse(comm.ExecuteScalar().ToString());
			comm.Parameters.Clear();

			if(count == 0)
				return true;
			else
				return false;
		}



		/// <summary>
		/// 외주입고관련 금액 수정
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void OutSideDeliveryUpdate(SqlConnection conn,SqlTransaction tr,string[] list)
		{
			string str = "Update OSD_HT Set ApplyUnitCost = @ApplyUnitCost, TotalCost = @TotalCost, [Year] = @year, [Month] = @month where OutSideDeliveryHistoryIndex = @index";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@ApplyUnitCost",decimal.Parse(list[21]));
			comm.Parameters.Add("@TotalCost",decimal.Parse(list[21]) * decimal.Parse(DataGrid1.Rows[index].Cells.FromKey("적합수량").Value.ToString()));
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex1").Value.ToString()));
			comm.Parameters.Add("@year",list[30]);
			comm.Parameters.Add("@month",list[31]);
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();	
		}


		/// <summary>
		/// 구매입고관련 금액 수정
		/// </summary>
		/// <param name="conn"></param>
		/// <param name="tr"></param>
		private void BuyingDeliveryUpdate(SqlConnection conn,SqlTransaction tr,string[] list)
		{
			string str = "Update BD_HT Set ApplyUnitCost = @ApplyUnitCost, TotalCost = @TotalCost, [Year] = @year, [Month] = @month where BuyingDeliveryHistoryIndex = @index";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@ApplyUnitCost",decimal.Parse(list[21]));
			comm.Parameters.Add("@TotalCost",decimal.Parse(list[21]) * decimal.Parse(DataGrid1.Rows[index].Cells.FromKey("적합수량").Value.ToString()));
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex1").Value.ToString()));
			comm.Parameters.Add("@year",list[30].Trim());
			comm.Parameters.Add("@month",list[31].Trim());
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();	
		}

		/// <summary>
		/// 구매관련 수정함수
		/// </summary>
		private void BuyingUpdate(SqlConnection conn,SqlTransaction tr)
		{
			//구매발주원장의 잔량누적변경을 한다
			//또한자산분류에 따라 원자재인지,상품인가에 따라 각 창고별 수량도 수정한다.
			//			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			//			conn.Open();
			
			
			//구매발주원장 이전합격수량을 잔량에서 빼준다
			string str = "Update BO_HT Set RemainQuantity = RemainQuantity + @RemainQuantity where BuyingOrderHistoryIndex = @index";
			SqlCommand comm = new SqlCommand();
			comm.Connection = conn;
			comm.Transaction = tr;
			comm.CommandText = str;
			comm.Parameters.Add("@RemainQuantity",decimal.Parse(arrlist[0].ToString()));
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex2").Value.ToString()));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();			
			
			//구매발주원장 잔량필드에 총발주량에서 현재 합격수량을 뺀수량을 더한다
			str = "Update BO_HT Set RemainQuantity = RemainQuantity - @RemainQuantity where BuyingOrderHistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@RemainQuantity",decimal.Parse(hdSucessQuantity.Value));
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex2").Value.ToString()));
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();
			


			//잔량이 발주수량보다 작으면 진행상태를 '진행'으로 변경시킨다.
			decimal Order = 0;
			decimal Remain = 0;

			str = "Select OrderQuantity,RemainQuantity From BO_HT Where BuyingOrderHistoryIndex = @index";
			comm.CommandText = str;
			comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("HistoryIndex2").Value.ToString()));
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				Order = decimal.Parse(dr["OrderQuantity"].ToString());
				Remain = decimal.Parse(dr["RemainQuantity"].ToString());
			}
			dr.Close();
						
			if(Remain > 0)
				str = "Update BO_HT Set ProgressCondition = '진행' Where  BuyingOrderHistoryIndex = @index";
			else
				str = "Update BO_HT Set ProgressCondition = '완료' Where  BuyingOrderHistoryIndex = @index";

			comm.CommandText = str;
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			int year = int.Parse(hdyear.Value);
			int mon = int.Parse(hdmon.Value);
				
			Table table = new Table(year,mon);


			//매입테이블의 금액을 수정시킨다(기존금액 마이너스입고,현재금액 플러스 입고)
			decimal BeforeCost = decimal.Parse(hdBeforeApplyUintCost.Value)*decimal.Parse(hdBeforeSucessQauntity.Value);
			comm.Parameters.Add("@comnum",DataGrid1.Rows[index].Cells.FromKey("BusinessCompanyNum").Text);
			comm.Parameters.Add("@year",year);
			comm.Parameters.Add("@cost", -BeforeCost);
			comm.CommandText = table.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

			//year = int.Parse(DataGrid1.Rows[index].Cells.FromKey("Year").Text);
			//mon = int.Parse(DataGrid1.Rows[index].Cells.FromKey("Month").Text);

			year = int.Parse(hdyear1.Value);
			mon = int.Parse(hdmon1.Value);

			Table table1 = new Table(year,mon);
			decimal NowCost = decimal.Parse(vals[1])*decimal.Parse(vals[21]);
			comm.Parameters.Add("@comnum",DataGrid1.Rows[index].Cells.FromKey("BusinessCompanyNum").Text);
			comm.Parameters.Add("@year",year);
			comm.Parameters.Add("@cost", NowCost);
			comm.CommandText = table1.PaymentIncreaseTable();
			comm.ExecuteNonQuery();
			comm.Parameters.Clear();

	
			year = DateTime.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionCompleteDate").Value.ToString()).Year;
			mon = DateTime.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionCompleteDate").Value.ToString()).Month;
				
			//해당품목의 자산분류에 따라 생산창고와 원자재창고에 수량을 변경시킨다.
			if(Division(DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString()) == "원자재")
			{
				
				//원자재 창고의 입고수량을 마이너스 증가시킨다
				
				//원자재창고 품목의 창고금액
				decimal Cost = 0;
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				comm.CommandText = str;
				SqlDataReader dr3 = comm.ExecuteReader();
				while(dr3.Read())
				{
					Cost = decimal.Parse(dr3["StandardUnitCost"].ToString());
				}
				dr3.Close();
				comm.Parameters.Clear();

				// 원자재창고 입고수량 마이너스 증가
				str = table.InRowTable();
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(arrlist[0].ToString());
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(Cost*decimal.Parse(arrlist[0].ToString()));
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@year",year);
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				// 원자재창고 입고수량 증가
				str = table.InRowTable();
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(hdSucessQuantity.Value);
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = (Cost*decimal.Parse(hdSucessQuantity.Value));
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@year",year);
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				//마이너스 재고허용 여부
				if(MinusStore() == false)
				{
					string strSQL = "SELECT StockQuantity12 FROM RMS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14000000' and [Year] = @year";
					comm.Parameters.Add("@ItemNum", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
					comm.Parameters.Add("@year",year);
									
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
					comm.Parameters.Clear();	
					string StockQuantity = "False";
					string ItemName = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량이 부족합니다.");
					}
				}

				


				
				
			}
			else//상품인경우 입고의뢰원장에도 입고의뢰수량을 변경시킨다.
			{	
				
				//생산 창고의 입고수량을 마이너스 증가시킨다
				
				//원자재창고 품목의 창고금액
				decimal Cost = 0;
				str = "Select StandardUnitCost From II_MT Where RecodingState = 1 and ItemNum = @itemnum";
				comm.Parameters.Add("@itemnum",SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				comm.CommandText = str;
				SqlDataReader dr3 = comm.ExecuteReader();
				while(dr3.Read())
				{
					Cost = decimal.Parse(dr3["StandardUnitCost"].ToString());
				}
				dr3.Close();
				comm.Parameters.Clear();
				// 생산창고 입고수량 마이너스 증가
				str = table.InProductionTable();
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = -decimal.Parse(arrlist[0].ToString());
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = -(Cost*decimal.Parse(arrlist[0].ToString()));
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = "14009999";
				comm.Parameters.Add("@year",year);
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				// 생산창고 입고수량 증가
				str = table.InProductionTable();
				comm.Parameters.Add("@quantity",SqlDbType.Decimal).Value = decimal.Parse(hdSucessQuantity.Value);
				comm.Parameters.Add("@cost",SqlDbType.Decimal).Value = (Cost*decimal.Parse(hdSucessQuantity.Value));
				comm.Parameters.Add("@num",SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
				comm.Parameters.Add("@code",SqlDbType.VarChar).Value = "14009999";
				comm.Parameters.Add("@year",year);
				comm.CommandText = str;
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();

				//마이너스 재고허용 여부
				if(MinusStore() == false)
				{
					string strSQL = "SELECT StockQuantity12 FROM PS_MT WHERE RecodingState = 1 and ItemNum = @ItemNum and ProcessCode = '14009999' and [Year] = @year";
					comm.Parameters.Add("@ItemNum", SqlDbType.VarChar).Value = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
									
					comm.CommandText = strSQL;
					SqlDataReader reader = comm.ExecuteReader();
					comm.Parameters.Clear();
					string StockQuantity = "False";
					string ItemName = DataGrid1.Rows[index].Cells.FromKey("ItemNum").Value.ToString();
					if(reader.Read())
					{
						if(float.Parse(reader["StockQuantity12"].ToString()) < 0)
						{
							StockQuantity = "True";
						}
					}
					reader.Close();

					if(StockQuantity == "True")
					{
						throw new Exception(ItemName + " 의 재고량이 부족합니다.");
					}
				}


				//입고의뢰원장 입고의뢰수량 변경
				str = "Update ISR_HT Set InstorehouseRequestQuantity = @InstorehouseRequestQuantity where HistoryIndex = @index and HistorySection = '품질검사'";
				comm.CommandText = str;
				comm.Parameters.Add("@InstorehouseRequestQuantity",decimal.Parse(hdSucessQuantity.Value));
				comm.Parameters.Add("@index",int.Parse(DataGrid1.Rows[index].Cells.FromKey("QualityInspectionHistoryIndex").Value.ToString()));
				comm.ExecuteNonQuery();
				comm.Parameters.Clear();
				
			}

			


		}


		private string Division(string Item)
		{
			
			string aa="";
			string str = "Select PropertyClassification From II_MT Where RecodingState = 1 and ItemNum = @num";
			SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			con.Open();
			SqlCommand comm =  new SqlCommand(str,con);
				
			comm.Parameters.Add("@num",SqlDbType.VarChar).Value = Item;
			SqlDataReader dr = comm.ExecuteReader();
			if(dr.Read())
			{
				aa = dr["PropertyClassification"].ToString();
			}
			dr.Close();
			con.Close();
			
			return aa;
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

		private void btCancle_Click(object sender, System.EventArgs e)
		{
			
			Delete del = new Delete(DataGrid1,"QualityInspectionPC",Session["UserName"].ToString(), Session["ID"].ToString());
			del.MainRowDelete();


			
			
			SByte sbyteProcessDiv = Convert.ToSByte(ddlProcessDiv.SelectedItem.Value);
			
			// 검색 객체 생성
			KIT_ERP.QualityInspection.QualityInspection_Search qis = new KIT_ERP.QualityInspection.QualityInspection_Search
				(
				PageNames.품질검사현황
				, ItemSearchControl1.ItemNum
				, ItemSearchControl1.ItemDrawNum
				, ItemSearchControl1.ItemName
				, txtStartDate.Text
				, txtEndDate.Text
				, CSC1.Company
				, CSC1.BusinessRegistrationNum
				, sbyteProcessDiv
				, ddlDivision.SelectedItem.Value
				);

			DataTable dt_Source = qis.GetDataSet().Tables[0];
			
			// 그리드 하단 통계 출력 메소드 호출
			this.SUM_and_AVG_(dt_Source);
			
			DataGrid1.DataSource = dt_Source.DefaultView;
			DataGrid1.DataKeyField = "번호";
			DataGrid1.DataBind();

			Session["DT1"] = dt_Source;
		}

		private void btExcel_Click(object sender, System.EventArgs e)
		{
			SByte sbyteProcessDiv = Convert.ToSByte(ddlProcessDiv.SelectedItem.Value);
			
			// 검색 객체 생성
			KIT_ERP.QualityInspection.QualityInspection_Search qis = new KIT_ERP.QualityInspection.QualityInspection_Search
				(
				PageNames.품질검사현황
				, ItemSearchControl1.ItemNum
				, ItemSearchControl1.ItemDrawNum
				, ItemSearchControl1.ItemName
				, txtStartDate.Text
				, txtEndDate.Text
				, CSC1.Company
				, CSC1.BusinessRegistrationNum
				, sbyteProcessDiv
				, ddlDivision.SelectedItem.Value
				);

			DataTable dt_Source = qis.GetDataSet().Tables[0];

			UltraWebGrid uwg_QI = new UltraWebGrid();
			uwg_QI = DataGrid1;
			uwg_QI.DisplayLayout.Pager.AllowPaging = false;
			uwg_QI.Columns.FromKey("chk").Hidden = true;
			uwg_QI.DataSource = dt_Source;
			uwg_QI.DataSource = dt_Source.DefaultView;
			uwg_QI.DataKeyField = "번호";
			uwg_QI.DataBind();
			UltraWebGridExcelExporter1.Export(uwg_QI);
		
		}


		/// <summary>
		/// 월마감 여부를 확인하는 함수
		/// </summary>
		/// <returns></returns>
		private bool MonthClosing()
		{
			int count = int.Parse(hdIndex.Value);
			int year = 0;
			int month = 0;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			string str = "Select ClosingYear, ClosingMonth From MCI_MT Where AffairDistinction = @Distinction";
			SqlCommand comm =  new SqlCommand(str,conn);
			comm.Parameters.Add("@Distinction",SqlDbType.VarChar).Value = "구매";
			SqlDataReader dr = comm.ExecuteReader();
			while(dr.Read())
			{
				year = int.Parse(dr[0].ToString());
				month = int.Parse(dr[1].ToString());
			}
			conn.Close();

			int CloseYear = 0;
			int CloseMonth = 0;
			
			
			CloseYear = int.Parse(DataGrid1.Rows[count].Cells.FromKey("Year").Text);
			CloseMonth = int.Parse(DataGrid1.Rows[count].Cells.FromKey("Month").Text);
			
			if(year < CloseYear)
			{
				return true;
			}
			else if(year == CloseYear)
			{
				if(month < CloseMonth)
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}		
		}
	}
}
