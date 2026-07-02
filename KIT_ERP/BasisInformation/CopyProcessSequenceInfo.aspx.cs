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

namespace KIT_ERP.BasisInformation
{
	/// <summary>
	/// CopyProcessSequenceInfo에 대한 요약 설명입니다.
	/// </summary>
	public class CopyProcessSequenceInfo : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
		protected KIT_ERP.Common.ItemSearchControl.ItemSearchControl ItemSearchControl1;//복사품목
		protected KIT_ERP.BasisInformation.ItemSearch.ItemSearch ItemSearch1;//원품목
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			ItemSearchControl1.HalfFinishedProducts = true;
			ItemSearchControl1.Products = true;
			ItemSearch1.ChildProducts = true;//원품목
			ItemSearch1.ChildHalfFinishedProducts = true;//복사품
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e)
		{
			if(Validate(ItemSearch1.ChildItemNum,ItemSearch1.ChildItemDrawNum,ItemSearch1.ChildItemName))
			{
				if(Validate(ItemSearchControl1.ItemNum,ItemSearchControl1.ItemDrawNum,ItemSearchControl1.ItemName))
				{
					CopyProcess();
				}
			}
		}

		private void CopyProcess()
		{
			bool test = false;
			SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings["DSN"]);
			conn.Open();
			SqlCommand comm = new SqlCommand();
			SqlTransaction tr = conn.BeginTransaction();
			comm.Connection = conn;
			comm.Transaction = tr;

			try
			{
				string str = "Select Count(*) From PSI_MT Where ItemNum = @ItemNum and RecodingState = 1";
				comm.CommandText =str;
				comm.Parameters.Add("@ItemNum",ItemSearch1.ChildItemNum);
				int count = int.Parse(comm.ExecuteScalar().ToString());
				comm.Parameters.Clear();
				if(count !=0)
				{
					throw new Exception(ItemSearch1.ChildItemNum+" 품목의 공정이 존재합니다!");
				}
				else
				{
					str = @"Select ItemNum, ProcessSequenceNum, ProcessCode, WorkDistinction, WCName, OutsideOrderRate, ProgressRate, LeadTime, EtcText, RegistrationPerson, RegistrationPersonID, RegistrationDate From PSI_MT Where ItemNum = @ItemNum and RecodingState =1";
					comm.CommandText = str;
					comm.Parameters.Add("@ItemNum",ItemSearchControl1.ItemNum);
					SqlDataAdapter da = new SqlDataAdapter(comm);
					DataSet ds = new DataSet();
					da.Fill(ds);
					comm.Parameters.Clear();
					foreach(DataRow dr in ds.Tables[0].Rows)
					{
						
						test = true;
						str = @"Insert into PSI_MT (ItemNum, ProcessSequenceNum, ProcessCode, WorkDistinction, WCName, OutsideOrderRate, ProgressRate, LeadTime, EtcText, RecodingState, RegistrationPerson, RegistrationPersonID, RegistrationDate)
							values (@ItemNum, @ProcessSequenceNum, @ProcessCode, @WorkDistinction, @WCName, @OutsideOrderRate, @ProgressRate, @LeadTime, @EtcText, '1', @RegistrationPerson, @RegistrationPersonID, @RegistrationDate)";
						comm.CommandText = str;
						comm.Parameters.Add("@ItemNum",ItemSearch1.ChildItemNum);
						comm.Parameters.Add("@ProcessSequenceNum",int.Parse(dr["ProcessSequenceNum"].ToString()));
						comm.Parameters.Add("@ProcessCode", dr["ProcessCode"].ToString());
						comm.Parameters.Add("@WorkDistinction",dr["WorkDistinction"].ToString());
						comm.Parameters.Add("@WCName",dr["WCName"].ToString());
						comm.Parameters.Add("@OutsideOrderRate",decimal.Parse(dr["OutsideOrderRate"].ToString()));
						comm.Parameters.Add("@ProgressRate",decimal.Parse(dr["ProgressRate"].ToString()));
						comm.Parameters.Add("@LeadTime",dr["LeadTime"].ToString());
						comm.Parameters.Add("@EtcText",dr["EtcText"].ToString());
						comm.Parameters.Add("@RegistrationPerson",Session["UserName"].ToString());
						comm.Parameters.Add("@RegistrationPersonID",Session["ID"].ToString());
						comm.Parameters.Add("@RegistrationDate",DateTime.Now.ToShortDateString());
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

						str = @"Insert into PS_MT (ItemNum, ProcessSequenceNum, ProcessCode,RecodingState, [Year]) values (@ItemNum, @ProcessSequenceNum, @ProcessCode, '1', @year)";
						comm.CommandText = str;
						comm.Parameters.Add("@ItemNum",ItemSearch1.ChildItemNum);
						comm.Parameters.Add("@ProcessSequenceNum",int.Parse(dr["ProcessSequenceNum"].ToString()));
						comm.Parameters.Add("@ProcessCode", dr["ProcessCode"].ToString());
						comm.Parameters.Add("@year", DateTime.Now.Year);
						comm.ExecuteNonQuery();
						comm.Parameters.Clear();

					}

					if(test)
					{
						Response.Write("<script language=javascript>");
						Response.Write("window.status='복사되었습니다!';");
						Response.Write("</script>");
						tr.Commit();
					}
					else
					{
						throw new Exception(ItemSearchControl1.ItemNum+" 품목의 공정이 존재하지 않아 복사할 수 없습니다!");
					}


					
				}
			}
			catch(Exception ee)
			{
				Response.Write("<script language=javascript>");
				Response.Write("alert('"+ee.Message+"');");
				Response.Write("</script>");
				tr.Rollback();
			}
			finally
			{
				conn.Close();
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
					throw new Exception("품목번호를 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemDrawNum From II_MT Where RecodingState = 1 and ItemDrawNum = @draw";
				comm.Parameters.Add("@draw",SqlDbType.VarChar).Value = draw;
				comm.CommandText = str;
				SqlDataAdapter da1 = new SqlDataAdapter(comm);
				DataSet ds1 = new DataSet();
				da1.Fill(ds1);

				if(ds1.Tables[0].Rows.Count == 0 )
					throw new Exception("도면번호를 정확하게 입력해주세요!");

				comm.Parameters.Clear();

				str = @"Select ItemName From II_MT Where RecodingState = 1 and ItemName = @name";
				comm.Parameters.Add("@name",SqlDbType.VarChar).Value = name;
				comm.CommandText = str;
				SqlDataAdapter da2 = new SqlDataAdapter(comm);
				DataSet ds2 = new DataSet();
				da2.Fill(ds2);

				if(ds2.Tables[0].Rows.Count == 0 )
					throw new Exception("품목명을 정확하게 입력해주세요!");

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
					throw new Exception("품목번호, 도면번호, 품목명을 다시 확인하세요.");

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
