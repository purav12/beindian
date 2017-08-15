using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using WebgapeClass;

namespace Webgape
{
    public partial class Visitor : System.Web.UI.Page
    {
        DataSet dsEntityInfo;
        CommonDAC cmddac = new CommonDAC();
        VisitorComponent vistComp = new VisitorComponent();
        public string CountryChart, CountryChartTitle, CityChart, CityChartTitle, YaxeName;
        protected void Page_Load(object sender, EventArgs e)
        {
            //FillCountryChart();
            //FillCityChart();
            if (!IsPostBack)
            {
                //BindEntityIdentifier(); //Temporary Hide
            }
        }


        /// <summary>
        /// Bind Entity Identifier
        /// </summary>
        private void BindEntityIdentifier()
        {
            if (Request.QueryString["EId"] != null && Request.QueryString["Ename"] != null)
            {
                string EntityName = "";
                int EntityId = 0;

                EntityId = Convert.ToInt32(Request.QueryString["EId"]);
                EntityName = Request.QueryString["Ename"].ToString();
                dsEntityInfo = vistComp.GetEntityInfo(EntityId, EntityName);
                if (dsEntityInfo != null && dsEntityInfo.Tables.Count > 0 && dsEntityInfo.Tables[0].Rows.Count > 0)
                {
                    ltrentity.Text = "<a target='_blank' href='" + dsEntityInfo.Tables[0].Rows[0]["EntityIdentifierLink"].ToString() + "'>" + dsEntityInfo.Tables[0].Rows[0]["EntityIdentifier"].ToString() + "</a>";
                }
                else
                {
                    ltrentity.Text = "<a target='_blank' href='http://BeIndian.in'>BeIndian.in</a>";
                }

            }
            else
            {
                ltrentity.Text = "<a target='_blank' href='http://BeIndian.in'>BeIndian.in</a>";
            }
        }

        /// <summary>
        /// Bind Country
        /// </summary>
        private void FillCountryDropdown(DataSet DsData)
        {
            if (!IsPostBack)
            {
                ddlCountry.Items.Clear();
                ddlCountry.DataSource = DsData;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryName";
                ddlCountry.DataBind();

                ddlCountry.Items.Insert(0, new ListItem("Select", "0"));

                ddlcity.Items.Clear();
                ddlcity.DataSource = DsData;
                ddlcity.DataTextField = "CountryName";
                ddlcity.DataValueField = "CountryName";
                ddlcity.DataBind();

                ddlcity.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlcity.SelectedValue = ddlCountry.SelectedValue;
            FillCityChart();
        }

        protected void ddlcity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCountry.SelectedValue = ddlcity.SelectedValue;
            FillCityChart();
        }

        public void FillCountryChart()
        {
            try
            {
                string EntityName = "";
                DataSet dsPieCountryChart = new DataSet();
                int EntityId = 0;
                if (Request.QueryString["EId"] != null)
                {
                    EntityId = Convert.ToInt32(Request.QueryString["EId"]);
                }

                if (Request.QueryString["Ename"] != null)
                {
                    EntityName = Request.QueryString["Ename"].ToString();
                }


                //dsPieCountryChart = vistComp.GetCountryDetails(EntityId, EntityName);
                dsPieCountryChart = vistComp.GetCountryDetails(0, "");
                if (dsPieCountryChart != null && dsPieCountryChart.Tables.Count > 0 && dsPieCountryChart.Tables[0].Rows.Count > 0)
                {
                    FillCountryDropdown(dsPieCountryChart);
                }
                if (dsPieCountryChart != null && dsPieCountryChart.Tables.Count > 0 && dsPieCountryChart.Tables[0].Rows.Count > 0)
                {
                    grdCountryVisitor.DataSource = dsPieCountryChart;
                    grdCountryVisitor.DataBind();
                    string[] RandColor = { "109618", "ff9900", "dc3912", "3366cc", "a83bed" };
                    Random random = new Random();
                    CountryChart = "['CountryName', 'TotalCount'],";
                    for (int i = 0; i < dsPieCountryChart.Tables[0].Rows.Count; i++)
                    {
                        CountryChart += "[";
                        string Status = dsPieCountryChart.Tables[0].Rows[i]["CountryName"].ToString() + " - " + Convert.ToString(dsPieCountryChart.Tables[0].Rows[i]["TotalCount"]);
                        if (Status.Length > 35) Status = Status.Substring(0, 35) + "...";
                        CountryChart += "'" + Status + "'" + "," + Convert.ToString(dsPieCountryChart.Tables[0].Rows[i]["TotalCount"]);
                        CountryChart += "],";
                    }
                    CountryChart = CountryChart.TrimEnd(',');
                    CountryChartTitle = "'Country Chart - '";
                    YaxeName = "Visitor Count";
                }
            }
            catch { }
        }
        public void FillCityChart()
        {
            try
            {
                string EntityName = "";
                DataSet dsPieCityChart = new DataSet();
                int EntityId = 0;
                if (Request.QueryString["EId"] != null)
                {
                    EntityId = Convert.ToInt32(Request.QueryString["EId"]);
                }

                if (Request.QueryString["Ename"] != null)
                {
                    EntityName = Request.QueryString["Ename"].ToString();
                }


                dsPieCityChart = vistComp.GetCityDetails(0, "", ddlCountry.SelectedValue);
                //dsPieCityChart = vistComp.GetCityDetails(EntityId, EntityName, ddlCountry.SelectedValue);
                if (dsPieCityChart != null && dsPieCityChart.Tables.Count > 0 && dsPieCityChart.Tables[0].Rows.Count > 0)
                {
                    grdCityVisitor.DataSource = dsPieCityChart;
                    grdCityVisitor.DataBind();
                    string[] RandColor = { "109618", "ff9900", "dc3912", "3366cc", "a83bed" };
                    Random random = new Random();
                    CityChart = "['City', 'TotalCount'],";
                    for (int i = 0; i < dsPieCityChart.Tables[0].Rows.Count; i++)
                    {
                        CityChart += "[";
                        string Status = dsPieCityChart.Tables[0].Rows[i]["City"].ToString() + " - " + Convert.ToString(dsPieCityChart.Tables[0].Rows[i]["TotalCount"]);
                        if (Status.Length > 35) Status = Status.Substring(0, 35) + "...";
                        CityChart += "'" + Status + "'" + "," + Convert.ToString(dsPieCityChart.Tables[0].Rows[i]["TotalCount"]);
                        CityChart += "],";
                    }
                    CityChart = CityChart.TrimEnd(',');
                    CityChartTitle = "'City Chart - '";
                    YaxeName = "Visitor Count";
                }
            }
            catch { }
        }

        protected void grdCountryVisitor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCountryVisitor.PageIndex = e.NewPageIndex;
            FillCountryChart();
        }

        protected void grdCityVisitor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCityVisitor.PageIndex = e.NewPageIndex;
            FillCityChart();
        }
    }
}