using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SendGrid;
using Swetugg.Web.Models;

namespace Swetugg.Web.Areas.Tickets.Controllers
{
    [RequireHttps]
    [RouteArea("Tickets", AreaPrefix = "tickets")]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _useSendGrid;

        public InvoiceController(ApplicationDbContext dbContext)
        {
            var str = ConfigurationManager.AppSettings["SendGrid_Messaging_Enabled"];

            if (!string.IsNullOrEmpty(str) && str.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                _useSendGrid = true;

            _dbContext = dbContext;
        }

        [Route("")]
        public ActionResult Index()
        {
            var request = new InvoiceRequest();

            return View(request);
        }

        public ActionResult Thanks()
        {
            return View();
        }



        public async Task<ActionResult> InvoiceRequest(InvoiceRequest request)
        {

            var m = $"";
            if (ModelState.IsValid)
            {
                _dbContext.Entry(request).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();
            }

            if (_useSendGrid)
            //https://azure.microsoft.com/sv-se/documentation/articles/sendgrid-dotnet-how-to-send-email/
            {

                var username = System.Environment.GetEnvironmentVariable("SENDGRID_USER");
                var pswd = System.Environment.GetEnvironmentVariable("SENDGRID_PASS");
                var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");

                var message = new SendGridMessage();
                //lägga beställarens mail här? om den är fel, hur blir det då?
                message.From = new MailAddress("info@swetugg.se");
                message.AddTo("info@swetugg.se");
                message.Subject = "Fakturaförfrågan";
                message.Text = $"";

                //Credentials
                var credentials = new NetworkCredential(username, pswd);
                var transportWeb = new SendGrid.Web(credentials);

                //API keys
                //var apiKey = "your_sendgrid_api_key";
                //var transportWeb = new SendGrid.Web(apiKey);

                await transportWeb.DeliverAsync(message);

            }
            return RedirectToAction("Thanks");
        }
    }
}