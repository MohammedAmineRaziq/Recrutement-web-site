using Microsoft.AspNet.Identity;
using recrutement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.CategoryModels.ToList());
        }

        public ActionResult Details(int JobId)
        {
            var job = db.JobModels.Find(JobId);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobID"] = JobId;
            return View(job);
        }

        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Apply(string Message , HttpPostedFileBase cv)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];

            var check = db.JobApplicationModels.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();

            if (check.Count < 1)
            {
                var jobApplication = new JobApplicationModels();

                string path = Path.Combine(Server.MapPath("~/Uploads"), cv.FileName);
                cv.SaveAs(path);
                jobApplication.CV = cv.FileName;
                jobApplication.UserId = UserId;
                jobApplication.JobId = JobId;
                jobApplication.Message = Message;
                jobApplication.ApplyDate = DateTime.Now;

                db.JobApplicationModels.Add(jobApplication);
                db.SaveChanges();
                ViewBag.Result="success";

            }
            else
            {
                ViewBag.Result="failed";
            }
            return View();
        }


        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.JobApplicationModels.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }

        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();

            var Jobs = from application in db.JobApplicationModels
                       join job in db.JobModels
                       on application.JobId equals job.Id
                       where job.User.Id == UserID
                       select application;

            var grouped = from j in Jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsAppViewModels
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());
        }


        public ActionResult Edit(int id)
        {
            var job = db.JobApplicationModels.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        public ActionResult Edit(JobApplicationModels job, HttpPostedFileBase CV)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), job.CV);
                if (CV != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), CV.FileName);
                    CV.SaveAs(path);
                    job.CV = CV.FileName;
                }

                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }

        public ActionResult Delete(int id)
        {

            var job = db.JobApplicationModels.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(JobApplicationModels job)
        {
            // TODO: Add delete logic here
            var myJob = db.JobApplicationModels.Find(job.Id);
            db.JobApplicationModels.Remove(myJob);
            db.SaveChanges();
            return RedirectToAction("GetJobsByUser");

        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.JobModels.Where(a => a.JobTitle.Contains(searchName)
            || a.JobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();

            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}