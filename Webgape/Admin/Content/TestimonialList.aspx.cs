using Castle.Web.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebgapeClass;

namespace Webgape.Admin.Content
{
    public partial class TestimonialList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master.HeadTitle("BeIndian - Testimonial List", "BeIndian.in - Testimonial List, Admin Testimonial List", "BeIndian.in - Testimonial List by Admin");
            }
        }
    }
}