using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.BusinessLayer;
using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using System.Data;

namespace Hospital
{
    public partial class frmBedStatus : System.Web.UI.Page
    {
        BedStatusBLL mobjStatus = new BedStatusBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                DataTable ldt = new DataTable();
                DataTable ldtBedDetail = new DataTable();
                int lintCounter = 0;
                int lintSecondCounter = 0;
                ldtBedDetail = mobjStatus.GetAllBedMasterDetail();
                ldt = mobjStatus.GetAllBedMasters();

                for (int j = 0; j < ldtBedDetail.Rows.Count; j++)
                {
                    string lstrFloor = ldtBedDetail.Rows[j]["Floor"].ToString();
                    int linTotalBed = Convert.ToInt32(ldtBedDetail.Rows[j]["TotalBed"]);
                    string lstrWardName = ldtBedDetail.Rows[j]["RoomCategory"].ToString();
                    for (int i = 1; i <= linTotalBed; i++)
                    {
                        if (lstrFloor == Convert.ToString(Floor.FirstFloor))
                        {
                            int lintBedNo = 0;

                            string lstrPatientName = string.Empty;

                            DataRow[] dr = ldt.Select("BedNo = " + i + "");
                            if (dr.Length != 0)
                            {
                                foreach (DataRow ldr in dr)
                                {
                                    lintBedNo = Convert.ToInt32(ldr["BedNo"]);
                                    lstrPatientName = ldr["PatientName"].ToString();
                                    if (i == lintBedNo)
                                    {
                                        lintCounter = 1;
                                        Label lblName = new Label();
                                        lblName.ForeColor = System.Drawing.Color.Red;
                                        lblName.Font.Bold = true;
                                        lblName.Font.Size = 13;

                                        Literal Name = new Literal();
                                        Image img = new Image();
                                        img.Height = 100;
                                        img.Width = 100;
                                        img.ImageUrl = "~/images/BedImages/bed3.png";
                                        img.Attributes.Add("style", "padding:10px 10px");
                                        img.ToolTip = "Occupied" + Environment.NewLine + lstrPatientName.ToUpper();
                                        lblName.Text = Convert.ToString(i);
                                        Name.Text = Convert.ToString(i);
                                        pnlFirst.Controls.Add(img);
                                        pnlFirst.ToolTip = lstrWardName;
                                        pnlFirst.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                        pnlFirst.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                        pnlFirst.Controls.Add(lblName);

                                        lintCounter = lintCounter + i;
                                        break;
                                    }

                                }
                            }
                            else if (lintCounter == 0 && i >= 1)
                            {

                                Label lblName = new Label();
                                Literal Name = new Literal();
                                Image img1 = new Image();
                                img1.Height = 100;
                                img1.Width = 100;
                                img1.ImageUrl = "~/images/BedImages/bed4.png";
                                img1.Attributes.Add("style", "padding:10px 10px");
                                img1.ToolTip = "Un-Occupied";
                                lblName.Text = Convert.ToString(i);
                                Name.Text = Convert.ToString(i);
                                pnlFirst.Controls.Add(img1);
                                pnlFirst.ToolTip = lstrWardName;
                                pnlFirst.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                pnlFirst.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                img1.Style.Add(HtmlTextWriterStyle.PaddingLeft, "40");
                                pnlFirst.Controls.Add(Name);

                                //UserControls_BedImageControl userBed = (UserControls_BedImageControl)Page.LoadControl("~/UserControls/BedImageControl.ascx");
                                //userBed.Url = "~/images/BedImages/bed4.png";
                                //userBed.Name = Convert.ToString(i);
                                //userBed.Width = 100;
                                //userBed.Height = 100;
                                //userBed.Attributes.Add("style", "padding:10px 10px");                            
                                //pnlFirst.Controls.Add(userBed);     

                            }
                            else
                            {
                                Label lblName = new Label();
                                Literal Name = new Literal();
                                Image img1 = new Image();
                                img1.Height = 100;
                                img1.Width = 100;
                                img1.ImageUrl = "~/images/BedImages/bed4.png";
                                img1.Attributes.Add("style", "padding:10px 10px");
                                img1.ToolTip = "Un-Occupied";
                                lblName.Text = Convert.ToString(i);
                                Name.Text = Convert.ToString(i);
                                pnlFirst.Controls.Add(img1);
                                pnlFirst.ToolTip = lstrWardName;
                                pnlFirst.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                pnlFirst.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                img1.Style.Add(HtmlTextWriterStyle.PaddingLeft, "40");
                                pnlFirst.Controls.Add(Name);
                            }
                        }

                        else if (lstrFloor == Convert.ToString(Floor.SecondFloor))
                        {
                            string lstrPatientName = string.Empty;
                            int lintBedNo = 0;
                            string lstrSecondFloor = string.Empty;


                            DataRow[] dr = ldt.Select("Floor = '" + Convert.ToString(Floor.SecondFloor) + "'");
                            if (dr.Length != 0)
                            {
                                foreach (DataRow ldr in dr)
                                {
                                    lstrSecondFloor = ldr["Floor"].ToString();
                                }
                            }

                            if (i < ldt.Rows.Count && lstrSecondFloor == Convert.ToString(Floor.SecondFloor))
                            {
                                lintSecondCounter = 1;
                                lintBedNo = Convert.ToInt32(ldt.Rows[i]["BedNo"]);
                                lstrPatientName = ldt.Rows[i]["PatientName"].ToString();
                                if (i + 1 == lintBedNo)
                                {
                                    Label lblName = new Label();
                                    lblName.ForeColor = System.Drawing.Color.Red;
                                    lblName.Font.Bold = true;
                                    lblName.Font.Size = 13;
                                    Literal Name = new Literal();
                                    Image img = new Image();
                                    img.Height = 100;
                                    img.Width = 100;
                                    img.ImageUrl = "~/images/BedImages/bed3.png";
                                    img.Attributes.Add("style", "padding:10px 10px");
                                    img.ToolTip = "Occupied" + Environment.NewLine + lstrPatientName.ToUpper();
                                    lblName.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                    Name.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                    pnlSecond.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                    pnlSecond.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                    pnlSecond.Controls.Add(img);
                                    pnlSecond.ToolTip = lstrWardName;
                                    pnlSecond.Controls.Add(lblName);
                                }
                                lintSecondCounter = lintSecondCounter + i;
                            }

                            if (lintSecondCounter == 0 && i >= 1)
                            {
                                Label lblName = new Label();
                                Literal Name = new Literal();
                                Image img1 = new Image();
                                img1.Height = 100;
                                img1.Width = 100;
                                img1.ImageUrl = "~/images/BedImages/bed4.png";
                                img1.Attributes.Add("style", "padding:10px 10px");
                                img1.ToolTip = "Un-Occupied";
                                lblName.Text = Convert.ToString(i);
                                Name.Text = Convert.ToString(i);
                                pnlSecond.Controls.Add(img1);
                                pnlSecond.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                pnlSecond.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                pnlSecond.ToolTip = lstrWardName;
                                pnlSecond.Controls.Add(Name);
                            }
                            else if (i > lintSecondCounter)
                            {
                                Label lblName = new Label();
                                Literal Name = new Literal();
                                Image img1 = new Image();
                                img1.Height = 100;
                                img1.Width = 100;
                                img1.ImageUrl = "~/images/BedImages/bed4.png";
                                img1.Attributes.Add("style", "padding:10px 10px");
                                img1.ToolTip = "Un-Occupied";
                                lblName.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                Name.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                pnlSecond.Controls.Add(img1);
                                pnlSecond.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                pnlSecond.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                pnlSecond.ToolTip = lstrWardName;
                                pnlSecond.Controls.Add(Name);
                            }
                        }
                        else if (lstrFloor == Convert.ToString(Floor.ThirdFloor))
                        {

                            string lstrPatientName = string.Empty;
                            int lintBedNo = 0;
                            string lstrSecondFloor = string.Empty;

                            DataRow[] dr = ldt.Select("Floor = '" + Convert.ToString(Floor.ThirdFloor) + "'");
                            if (dr.Length != 0)
                            {
                                foreach (DataRow ldr in dr)
                                {
                                    lstrSecondFloor = ldr["Floor"].ToString();
                                }
                            }

                            if (i < ldt.Rows.Count && lstrSecondFloor == Convert.ToString(Floor.ThirdFloor))
                            {
                                lintSecondCounter = 1;
                                lintBedNo = Convert.ToInt32(ldt.Rows[i]["BedNo"]);
                                lstrPatientName = ldt.Rows[i]["PatientName"].ToString();
                                if (i + 1 == lintBedNo)
                                {
                                    Label lblName = new Label();
                                    lblName.ForeColor = System.Drawing.Color.Red;
                                    lblName.Font.Bold = true;
                                    lblName.Font.Size = 13;
                                    Literal Name = new Literal();
                                    Image img = new Image();
                                    img.Height = 100;
                                    img.Width = 100;
                                    img.ImageUrl = "~/images/BedImages/bed3.png";
                                    img.Attributes.Add("style", "padding:10px 10px");
                                    img.ToolTip = "Occupied" + Environment.NewLine + lstrPatientName.ToUpper();
                                    lblName.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                    Name.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                    pnlThird.Controls.Add(img);
                                    pnlThird.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                    pnlThird.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                    pnlThird.ToolTip = lstrWardName;
                                    pnlThird.Controls.Add(lblName);
                                }
                                lintSecondCounter = lintSecondCounter + i;
                            }

                            if (lintSecondCounter == 0 && i >= 1)
                            {
                                Label lblName = new Label();
                                Literal Name = new Literal();
                                Image img1 = new Image();
                                img1.Height = 100;
                                img1.Width = 100;
                                img1.ImageUrl = "~/images/BedImages/bed4.png";
                                img1.Attributes.Add("style", "padding:10px 10px");
                                img1.ToolTip = "Un-Occupied";
                                lblName.Text = Convert.ToString(i);
                                Name.Text = Convert.ToString(i);
                                pnlThird.Controls.Add(img1);
                                pnlThird.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                pnlThird.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                pnlThird.ToolTip = lstrWardName;
                                pnlThird.Controls.Add(Name);
                            }
                            else if (i > lintSecondCounter)
                            {
                                Label lblName = new Label();
                                Literal Name = new Literal();
                                Image img1 = new Image();
                                img1.Height = 100;
                                img1.Width = 100;
                                img1.ImageUrl = "~/images/BedImages/bed4.png";
                                img1.Attributes.Add("style", "padding:10px 10px");
                                img1.ToolTip = "Un-Occupied";
                                lblName.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                Name.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                pnlThird.Controls.Add(img1);
                                pnlThird.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                pnlThird.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                pnlThird.ToolTip = lstrWardName;
                                pnlThird.Controls.Add(Name);
                            }
                        }
                        else if (lstrFloor == Convert.ToString(Floor.FourthFloor))
                        {

                            string lstrPatientName = string.Empty;
                            int lintBedNo = 0;
                            string lstrSecondFloor = string.Empty;

                            DataRow[] dr = ldt.Select("Floor = '" + Convert.ToString(Floor.FourthFloor) + "'");
                            if (dr.Length != 0)
                            {
                                foreach (DataRow ldr in dr)
                                {
                                    lstrSecondFloor = ldr["Floor"].ToString();
                                }
                            }

                            if (i < ldt.Rows.Count && lstrSecondFloor == Convert.ToString(Floor.FourthFloor))
                            {
                                lintSecondCounter = 1;
                                lintBedNo = Convert.ToInt32(ldt.Rows[i]["BedNo"]);
                                lstrPatientName = ldt.Rows[i]["PatientName"].ToString();
                                if (i + 1 == lintBedNo)
                                {
                                    Label lblName = new Label();
                                    lblName.ForeColor = System.Drawing.Color.Red;
                                    lblName.Font.Bold = true;
                                    lblName.Font.Size = 13;
                                    Literal Name = new Literal();
                                    Image img = new Image();
                                    img.Height = 100;
                                    img.Width = 100;
                                    img.ImageUrl = "~/images/BedImages/bed3.png";
                                    img.Attributes.Add("style", "padding:10px 10px");
                                    img.ToolTip = "Occupied" + Environment.NewLine + lstrPatientName.ToUpper();
                                    lblName.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                    Name.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                    pnlFourth.Controls.Add(img);
                                    pnlFourth.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                    pnlFourth.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                    pnlFourth.ToolTip = lstrWardName;
                                    pnlFourth.Controls.Add(lblName);
                                }
                                lintSecondCounter = lintSecondCounter + i;
                            }

                            if (lintSecondCounter == 0 && i >= 1)
                            {
                                Label lblName = new Label();
                                Literal Name = new Literal();
                                Image img1 = new Image();
                                img1.Height = 100;
                                img1.Width = 100;
                                img1.ImageUrl = "~/images/BedImages/bed4.png";
                                img1.Attributes.Add("style", "padding:10px 10px");
                                img1.ToolTip = "Un-Occupied";
                                lblName.Text = Convert.ToString(i);
                                Name.Text = Convert.ToString(i);
                                pnlFourth.Controls.Add(img1);
                                pnlFourth.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                pnlFourth.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                pnlFourth.ToolTip = lstrWardName;
                                pnlFourth.Controls.Add(Name);
                            }
                            else if (i > lintSecondCounter)
                            {
                                Label lblName = new Label();
                                Literal Name = new Literal();
                                Image img1 = new Image();
                                img1.Height = 100;
                                img1.Width = 100;
                                img1.ImageUrl = "~/images/BedImages/bed4.png";
                                img1.Attributes.Add("style", "padding:10px 10px");
                                img1.ToolTip = "Un-Occupied";
                                lblName.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                Name.Text = Convert.ToString(i + 2 - lintSecondCounter);
                                pnlFourth.Controls.Add(img1);
                                pnlFourth.Style.Add(HtmlTextWriterStyle.FontFamily, "Verdana");
                                pnlFourth.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                                pnlFourth.ToolTip = lstrWardName;
                                pnlFourth.Controls.Add(Name);
                            }
                        }
                    }
                }
            }
        }

        public enum Floor
        {
            GroundFloor,
            FirstFloor,
            SecondFloor,
            ThirdFloor,
            FourthFloor,
            FifthFloor,
            SixthFloor,
        }
    }
}