﻿using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Swetugg.Web.Models;
using Swetugg.Web.Services;

namespace Swetugg.Web.Areas.Swetugg2015.Controllers
{
    [RouteArea("Swetugg2015", AreaPrefix = "swetugg-2015")]
    public class ConferenceController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConferenceService conferenceService;

        private int conferenceId;
        private string conferenceSlug;
        private Conference conference;
        private string appInsightsInstrumentationKey;
        private string facebookAppId;

        public ConferenceController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.conferenceSlug = "swetugg-2015";
            this.conferenceService = new CachedConferenceService(new ConferenceService(dbContext));
            this.appInsightsInstrumentationKey = ConfigurationManager.AppSettings["ApplicationInsights.InstrumentationKey"];
            this.facebookAppId = ConfigurationManager.AppSettings["Facebook_Api_AppId"];
        }

        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ViewData["InstrumentationKey"] = appInsightsInstrumentationKey;
            ViewData["FacebookAppId"] = facebookAppId;
            base.OnResultExecuting(filterContext);
        }

        [Route("")]
        public ActionResult Index()
        {
            var speakers = conferenceService.GetSpeakers(ConferenceId);
            var sessions = conferenceService.GetSessions(ConferenceId);
            var slots = conferenceService.GetSlotsAndSessions(ConferenceId);
            var rooms = conferenceService.GetRooms(ConferenceId);
            var sponsors = conferenceService.GetSponsors(ConferenceId);

            ViewData["Speakers"] = speakers;
            ViewData["Sessions"] = sessions;
            ViewData["Slots"] = slots;
            ViewData["Rooms"] = rooms;
            ViewData["Sponsors"] = sponsors;
            ViewData["Conference"] = Conference;

            return View();
        }

        [Route("code-of-conduct")]
        public ActionResult CodeOfConduct()
        {
            ViewData["Conference"] = Conference;
            return View();
        }

        [Route("speakers/{speakerSlug}")]
        public ActionResult Speaker(string speakerSlug)
        {
            var speaker = conferenceService.GetSpeakerBySlug(ConferenceId, speakerSlug);
            return View(speaker);
        }

        protected override void OnActionExecuted(ActionExecutedContext context)
        {
            ViewBag.Conference = Conference;
            base.OnActionExecuted(context);
        }

        protected Conference Conference
        {
            get
            {
                if (conference != null)
                    return conference;
                return conference = dbContext.Conferences.Single(c => c.Slug == ConferenceSlug);
            }

        }
        protected int ConferenceId
        {
            get
            {
                if (conferenceId != 0)
                    return conferenceId;

                return conferenceId = Conference.Id;
            }
        }

        protected string ConferenceSlug
        {
            get
            {
                if (conferenceSlug != null)
                    return conferenceSlug;

                return conferenceSlug = (string)RouteData.Values["conferenceSlug"];
            }
        }

    }
}