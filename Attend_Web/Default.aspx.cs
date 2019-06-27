using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Attend_Web
{
    public partial class _Default : Page
    {
        public static SqlConnection conn = new SqlConnection();
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        DataTable datatable = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            DisxLab.Text = "";
            if (!this.IsPostBack)
            {
                GridDix.DataBind();
            }
        }

        protected void dp_MonthsList_Load(object sender, EventArgs e)
        {
            //DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
            //for (int i = 1; i < 13; i++)
            //{
            //    dp_MonthsList.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
            //}
        }

        protected void dp_EmpName_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings[2].ToString();
            string LoadEmp = "SELECT DISTINCT [EmployeeName] FROM[etimetracklite1].[dbo].[Employees]";
            conn = new SqlConnection(connStr);
            conn.Open();
            SqlCommand QueryChk = new SqlCommand(LoadEmp, conn);
            using (SqlDataReader oReader = QueryChk.ExecuteReader())
            {
                while (oReader.Read())
                {
                    string name = oReader[0].ToString();

                    char nname2 = name.ToCharArray().ElementAt(0);
                    Boolean result = char.IsNumber(nname2);
                    if (result == false)
                    {
                        char nname = name.ToCharArray().ElementAt(3);
                        string name1 = nname.ToString();
                        if (name1 != "_") { dp_EmpName.Items.Add(oReader[0].ToString()); }
                    }

                }
            }
            conn.Close();
        }

        protected void bttn_Submit_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings[2].ToString();
            conn = new SqlConnection(connStr);
            string from, to;
            if (rd_ByMonth.Checked == true)
            {
                try
                {
                    DisxLab.Text = "SUMMARY REPORT FOR THE DAY OF "+txt_Date.Text;
                    ViewState["Display"] = DisxLab.Text;
                    datatable.Columns.Add("Name", typeof(string));
                    datatable.Columns.Add("Location", typeof(string));
                    datatable.Columns.Add("In Time", typeof(string));
                    datatable.Columns.Add("Out Time", typeof(string));
                    DataRow dr = null;

                    DateTime From = Convert.ToDateTime(txt_Date.Text);
                    from = From.ToString("yyyy-MM-dd HH:mm:ss");
                    DateTime To = Convert.ToDateTime(txt_Date.Text);
                    To = To.AddDays(1);
                    to = To.ToString("yyyy-MM-dd HH:mm:ss");

                    string querybyEmp = "SELECT Employees.EmployeeName,AttendanceLogs.InDeviceId,AttendanceLogs.PunchRecords,AttendanceLogs.InTime, AttendanceLogs.OutTime FROM AttendanceLogs INNER JOIN Employees ON AttendanceLogs.EmployeeId = Employees.EmployeeId WHERE Intime>='" + from + "' AND OutTime<'" + to + "' ORDER BY EmployeeName ASC";
                    conn.Open();
                    SqlCommand QueryChk = new SqlCommand(querybyEmp, conn);
                    using (SqlDataReader oReader = QueryChk.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            string Name = oReader[0].ToString();
                            string Loc = oReader[1].ToString();
                            string Punch = oReader[2].ToString();
                            string InTime = oReader[3].ToString();
                            string OutTime = oReader[4].ToString();
                            //09:42:in(Head Office),18:00:out(SE),"18:00:out(SE)"08:39:in(Head Office),13:07:out(Head Office),18:15:in(Head Office),07:00:out(SE),
                            string[] PunchRec = Punch.Split(',');
                            int total = Convert.ToInt32(PunchRec.Length);
                            InTime = PunchRec[0].Substring(0, 5);

                            string outcheck = PunchRec[total - 2];
                            if (((outcheck == "18:00:out(SE)") || (outcheck == "07:00:out(SE)")) && total == 3) { OutTime = ""; } else { OutTime = PunchRec[total - 2].Substring(0, 5); }
                            if (((outcheck == "18:00:out(SE)") || (outcheck == "07:00:out(SE)")) && total > 3) { OutTime = PunchRec[total - 3].Substring(0, 5); }

                            dr = datatable.NewRow();
                            dr["Name"] = Name;
                            dr["Location"] = Loc;
                            dr["In Time"] = InTime;
                            dr["Out Time"] = OutTime;
                            datatable.Rows.Add(dr);
                        }
                    }


                    ViewState["CurrentTable"] = datatable;
                    GridDix.DataSource = datatable;
                    GridDix.DataBind();
                    conn.Close();
                }
                catch(Exception ex) { }
                
            }
            else if (rd_ByCustom.Checked == true)
            {
                datatable.Columns.Add("Date", typeof(string));
                datatable.Columns.Add("Location", typeof(string));
                datatable.Columns.Add("In Time", typeof(string));
                datatable.Columns.Add("Out Time", typeof(string));

                DataRow dr = null;
                try
                {
                    
                    DateTime From = Convert.ToDateTime(dt_From.Text);
                    from = From.ToString("yyyy-MM-dd HH:mm:ss");
                    DateTime To = Convert.ToDateTime(dt_To.Text);
                    to = To.ToString("yyyy-MM-dd HH:mm:ss");
                    DisxLab.Text = "SUMMARY REPORT FOR THE EMPLOYEE " + dp_EmpName.Text + " FOR THE DATES " + From.ToShortDateString() + " To " + To.ToShortDateString();
                    ViewState["Display"] = DisxLab.Text;
                    string querybyEmp = "SELECT AttendanceLogs.AttendanceDate,AttendanceLogs.InDeviceId,AttendanceLogs.PunchRecords,AttendanceLogs.InTime, AttendanceLogs.OutTime FROM AttendanceLogs INNER JOIN Employees ON AttendanceLogs.EmployeeId = Employees.EmployeeId WHERE EmployeeName = '" + dp_EmpName.Text + "' AND Intime>='" + from + "' AND OutTime<='" + to + "' ORDER BY InTime DESC";
                    conn.Open();
                    SqlCommand QueryChk = new SqlCommand(querybyEmp, conn);
                    using (SqlDataReader oReader = QueryChk.ExecuteReader())
                    {
                        while (oReader.Read())
                        {
                            string Date = oReader[0].ToString();
                            Date = Date.Substring(0, 10);
                            string Loc = oReader[1].ToString();
                            string Punch = oReader[2].ToString();
                            string InTime = oReader[3].ToString();
                            string OutTime = oReader[4].ToString();
                            //09:42:in(Head Office),18:00:out(SE),"18:00:out(SE)"08:39:in(Head Office),13:07:out(Head Office),18:15:in(Head Office),07:00:out(SE),
                            string[] PunchRec = Punch.Split(',');
                            int total = Convert.ToInt32(PunchRec.Length);
                            InTime = PunchRec[0].Substring(0, 5);

                            string outcheck = PunchRec[total - 2];
                            if (((outcheck == "18:00:out(SE)") || (outcheck == "07:00:out(SE)")) && total == 3) { OutTime = ""; } else { OutTime = PunchRec[total - 2].Substring(0, 5); }
                            if (((outcheck == "18:00:out(SE)") || (outcheck == "07:00:out(SE)")) && total > 3) { OutTime = PunchRec[total - 3].Substring(0, 5); }

                            dr = datatable.NewRow();
                            dr["Date"] = Date;
                            dr["Location"] = Loc;
                            dr["In Time"] = InTime;
                            dr["Out Time"] = OutTime;
                            datatable.Rows.Add(dr);

                        }
                    }
                    ViewState["CurrentTable"] = datatable;
                    GridDix.DataSource = datatable;
                    GridDix.DataBind();
                    conn.Close();
                }
                catch (Exception Ex) { }
                
            }

        }

        protected void dp_dayList_Load(object sender, EventArgs e)
        {
            
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DisxLab.Text = ViewState["Display"].ToString();
            GridDix.PageIndex = e.NewPageIndex;
            GridDix.DataSource = ViewState["CurrentTable"];
            GridDix.DataBind();
        }
    }
}