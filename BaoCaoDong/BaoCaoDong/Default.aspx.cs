using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BaoCaoDong
{
    public partial class _Default : Page
    {
        public static List<String> dsBang = new List<string>(); 
        public static List<String> dsBangDaChon = new List<string>();
        public static List<String> dsCot = new List<string>(); // những cột thuộc những bảng đã chọn
        public static DataTable dt = new DataTable();
        public static DataTable dtFK = new DataTable();
        public static int sohang = 1;
        public static String[,] tam;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.LayDSBang();
                this.load_ForeignKeys();
                dt.Rows.Add();
                GridView.DataSource = dt;
                GridView.DataBind();
            }
        }
        private void LayDSBang()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["PETSHOPConnectionString"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            string query = "SELECT ROW_NUMBER() OVER (ORDER BY NAME) AS VALUE, name as TABLE_NAME fROM sys.TABLES where name != 'sysdiagrams'";

            cmd.CommandText = query;
            cmd.Connection = conn;
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                ListItem item = new ListItem();
                item.Text = sdr["TABLE_NAME"].ToString();
                item.Value = sdr["VALUE"].ToString();
                dsBang.Add(sdr["TABLE_NAME"].ToString());
                CheckBoxList_Bang.Items.Add(item);
                CheckBoxList_Bang.AutoPostBack = true;
            }
            conn.Close();
        }
        private void layDSCot(String tableName)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["PETSHOPConnectionString"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + tableName + "' AND COLUMN_NAME NOT LIKE 'rowguid%'";

                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ListItem item = new ListItem();
                            item.Text = sdr["COLUMN_NAME"].ToString();
                            item.Value = tableName.ToString();
                            dsCot.Add(tableName + "." + item.Text);
                        }
                    }
                    conn.Close();
                }
            }
        }

        protected void CheckBoxList_Bang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dsCot.Count != 0)
                CopyGridView();
            
            dsCot.Clear();
            dsBangDaChon.Clear();
            dsBangDaChon.Add("Non_Selected");
            dsCot.Add("Non_Selected");
            foreach (ListItem item in CheckBoxList_Bang.Items)
            {
                if (item.Selected)
                {
                    dsBangDaChon.Add(item.Text);
                    dsCot.Add(item.Text + ".*");
                    layDSCot(item.Text);

                }
            }
            
            load_DropDownList();
            if (tam == null) return;
            for (int i = 0; i < sohang; i++)
            {
                DropDownList dsT = (DropDownList)GridView.Rows[i].FindControl("DropDownList_Truong");
                
                if (dsT.Items.Contains(new ListItem(tam[i, 0])) == true)
                {
                    dsT.SelectedValue = tam[i, 0];
                }
                else
                {
                    DropDownList dsSX = (DropDownList)GridView.Rows[i].FindControl("DropDownList_SapXep");
                    CheckBox hien = (CheckBox)GridView.Rows[i].FindControl("Checked_HienThi");
                    TextBox dk = (TextBox)GridView.Rows[i].FindControl("TextBox_DieuKien");
                    TextBox hoac = (TextBox)GridView.Rows[i].FindControl("TextBox_Hoac");
                    dsSX.SelectedIndex = 0;
                    hien.Checked = false;
                    dk.Text = "";
                    hoac.Text = "";
                }
            }


        }
        private void load_ForeignKeys()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager
                        .ConnectionStrings["PETSHOPConnectionString"].ConnectionString;
            conn.Open();
            string cmd = "select  parent_object = (select name from sys.tables where object_id = x.parent_object_id) , " +
        "parent_column = (select name from sys.columns where object_id = x.parent_object_id and column_id = x.parent_column_id)," +
        "referenced_object = (select name from sys.tables where object_id = x.referenced_object_id)," +
        "referenced_column = (select name from sys.columns where object_id = x.referenced_object_id and column_id = x.referenced_column_id)" +
        "from sys.foreign_key_columns x ";

            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dtFK);
            conn.Close();
        }
        private void load_DropDownList()
        {
            for (int i = 0; i < sohang; i++)
            {
                DropDownList ddlDST = (DropDownList)GridView.Rows[i].FindControl("DropDownList_Truong");
                
                ddlDST.DataSource = dsCot;
                ddlDST.DataBind();
            }

        }

        protected void BtnTaoTruyVan_Click(object sender, EventArgs e)
        {
            if (dsCot.Count == 0) return;
            CopyGridView();
            string select = "";
            string from = "";
            string orderby = "";
            string groupby = "";
            string where = "";
            if (dsBangDaChon.Count > 2)
            {
                for(int i =1; i < dsBangDaChon.Count; i++)
                {
                    for(int j = 1; j < dsBangDaChon.Count; j++)
                    {
                        if (i == j) continue;
                        for(int m = 0; m < dtFK.Rows.Count; m++)
                        {
                            if(dsBangDaChon[i].Equals(dtFK.Rows[m]["parent_object"]) && dsBangDaChon[j].Equals( dtFK.Rows[m]["referenced_object"]))
                            {
                                if (!where.Equals("")) where = where + " AND " + dtFK.Rows[m]["parent_object"].ToString() + "." + dtFK.Rows[m]["parent_column"].ToString() + " = " + dtFK.Rows[m]["referenced_object"].ToString() + "." + dtFK.Rows[m]["referenced_column"].ToString();
                                else where = dtFK.Rows[m]["parent_object"].ToString() + "." + dtFK.Rows[m]["parent_column"].ToString() + " = " + dtFK.Rows[m]["referenced_object"].ToString() + "." + dtFK.Rows[m]["referenced_column"].ToString();
                            }

                        }
                    }
                }
            }
            for(int i=0; i<sohang; i++)
            {
                if (tam[i, 0].Equals("Non_Selected")) continue;
                if (tam[i, 2].Equals("True"))
                {
                    if(tam[i,1].Equals("Count") || tam[i,1].Equals("Sum")|| tam[i, 1].Equals("Min") || tam[i, 1].Equals("Max"))
                    {
                        if (!select.Equals("")) select = select + ", "+tam[i,1]+"(" + tam[i, 0]+")";
                        else select = tam[i, 1] + "(" + tam[i, 0] + ")";
                    }
                    else
                    {
                        if (!select.Equals("")) select = select + ", " + tam[i, 0];
                        else select = tam[i, 0];
                    }
                }
                if (tam[i, 1].Equals("ASC") || tam[i, 1].Equals("DESC"))
                {
                    if (!orderby.Equals("")) orderby = orderby + ", " + tam[i, 0] + " " + tam[i, 1];
                    else orderby = tam[i, 0] + " " + tam[i, 1];
                }
                else if(tam[i, 1].Equals("Group by"))
                {
                    if (!groupby.Equals("")) groupby = groupby + ", " + tam[i, 0];
                    else groupby = tam[i, 0];
                }
                
                if (!tam[i, 4].Equals(""))
                {
                    if(tam[i, 3].Equals(""))
                    {
                        if (!where.Equals("")) where = where + " AND " + tam[i, 0] + tam[i, 4];
                        else where = tam[i, 0] + tam[i, 4];
                    }
                    else
                    {

                        if (!where.Equals("")) where = where + " AND ( " + tam[i, 0] + tam[i, 3] +" OR "+ tam[i, 0] + tam[i, 4]+" )";
                        else where = " ( " + tam[i, 0] + tam[i, 3] + " OR " + tam[i, 0] + tam[i, 4] + " )";
                    }
                    
                }
                else if(tam[i, 4].Equals("")&& !tam[i, 3].Equals(""))
                {
                    if (!where.Equals("")) where = where + " AND " + tam[i, 0] + tam[i, 3];
                    else where = tam[i, 0] + tam[i, 3];
                }

            }
            from = string.Join(", ", dsBangDaChon);
            from = from.Substring(13);
            if (select.Equals("")) select = "*";
            string sql = "SELECT " + select + " FROM " + from;
            if (!where.Equals("")) sql = sql + " WHERE " + where;
            if (!groupby.Equals("")) sql = sql + " GROUP BY " + groupby;
            if(!orderby.Equals("")) sql = sql + " ORDER BY " + orderby;
            TextBoxSQL.Text = sql;

        }
        protected void CopyGridView()
        {
            tam = new String[sohang, 5];
            for(int i=0; i < sohang; i++)
            {
                DropDownList dsT = (DropDownList)GridView.Rows[i].FindControl("DropDownList_Truong");
                
                DropDownList dsSX = (DropDownList)GridView.Rows[i].FindControl("DropDownList_SapXep");
                CheckBox hien = (CheckBox)GridView.Rows[i].FindControl("Checked_HienThi");
                TextBox dk = (TextBox)GridView.Rows[i].FindControl("TextBox_DieuKien");
                TextBox hoac = (TextBox)GridView.Rows[i].FindControl("TextBox_Hoac");
                tam[i, 0] = dsT.SelectedValue;
                tam[i, 1] = dsSX.SelectedValue;
                tam[i, 2] = hien.Checked.ToString();
                tam[i, 3] = dk.Text;
                tam[i, 4] = hoac.Text;
            }
        }
        protected void PasteGridView()
        {
            for (int i = 1; i < sohang; i++)
            {
                DropDownList dsT = (DropDownList)GridView.Rows[i].FindControl("DropDownList_Truong");
                DropDownList dsSX = (DropDownList)GridView.Rows[i].FindControl("DropDownList_SapXep");
                CheckBox hien = (CheckBox)GridView.Rows[i].FindControl("Checked_HienThi");
                TextBox dk = (TextBox)GridView.Rows[i].FindControl("TextBox_DieuKien");
                TextBox hoac = (TextBox)GridView.Rows[i].FindControl("TextBox_Hoac");
                
                if( dsT.Items.Contains(new ListItem( tam[i-1, 0])) == true)
                {
                    dsT.SelectedValue = tam[i-1, 0];
                    dsSX.SelectedValue = tam[i-1, 1];
                    if (tam[i-1, 2].Equals("True")) hien.Checked = true;
                    else hien.Checked = false;
                    dk.Text = tam[i-1, 3];
                    hoac.Text = tam[i-1, 4];
                }
                
            }
        }

        protected void BtnThemHang_Click(object sender, EventArgs e)
        {
            if(dsCot.Count == 0)
            {
                sohang++;
                dt.Rows.Add();
                GridView.DataSource = dt;
                GridView.DataBind();
                return;

            }
            CopyGridView();
            dt.Rows.Add();
            GridView.DataSource = dt;
            GridView.DataBind();
            sohang++;
            load_DropDownList();
            if(tam!=null)PasteGridView();

        }

        protected void BtnXuatBaoCao_Click(object sender, EventArgs e)
        {
            if (TextBoxSQL.Text.Equals("")) return;
            Session["query"] = TextBoxSQL.Text;
            Session["title"] = TextBoxTuaDe.Text;
            SqlConnection cnn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                String connect = ConfigurationManager.ConnectionStrings["PETSHOPConnectionString"].ConnectionString;
                cnn.ConnectionString = connect;
                cnn.Open();
                DataSet dt = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = new SqlCommand(TextBoxSQL.Text, cnn);
                da.Fill(dt);
                Response.Redirect("Report.aspx");
                Server.Execute("Report.aspx");
            }
            catch (Exception x)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Câu truy vấn không hợp lệ !')", true);
            }
            
        }
    }
}