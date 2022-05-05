using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using recrutement.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace recrutement.Controllers
{
    public class JobController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Job
        public ActionResult Index()
        {
            var jobModels = db.JobModels.Include(j => j.Category);
            return View(jobModels.ToList());
        }

        // GET: Job/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobModel jobModel = db.JobModels.Find(id);
            if (jobModel == null)
            {
                return HttpNotFound();
            }
            return View(jobModel);
        }

        // GET: Job/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.CategoryModels, "Id", "CategoryName");
            return View();
        }

        // POST: Job/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JobModel jobModel,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                jobModel.JobImage = upload.FileName;
                jobModel.UserId = User.Identity.GetUserId();
                db.JobModels.Add(jobModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           

                ViewBag.CategoryId = new SelectList(db.CategoryModels, "Id", "CategoryName", jobModel.CategoryId);
            return View(jobModel);
        }

        // GET: Job/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobModel jobModel = db.JobModels.Find(id);
            if (jobModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.CategoryModels, "Id", "CategoryName", jobModel.CategoryId);
            return View(jobModel);
        }

        // POST: Job/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobModel jobModel, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), jobModel.JobImage);
                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    jobModel.JobImage = upload.FileName;
                }
                
                db.Entry(jobModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.CategoryModels, "Id", "CategoryName", jobModel.CategoryId);
            return View(jobModel);
        }

        // GET: Job/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobModel jobModel = db.JobModels.Find(id);
            if (jobModel == null)
            {
                return HttpNotFound();
            }
            return View(jobModel);
        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobModel jobModel = db.JobModels.Find(id);
            db.JobModels.Remove(jobModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
