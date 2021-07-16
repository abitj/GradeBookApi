using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GradeBookApi.Models;

namespace GradeBookApi.Controllers.Portal
{
    public class ExamsController : Controller
    {
        private GradeBookContext db = new GradeBookContext();

        // GET: Exams
        public ActionResult Index()
        {
            var exam = db.exam.Include(e => e.student).Include(e => e.subject);
            return View(exam.ToList());
        }

        // GET: Exams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.exam.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: Exams/Create
        public ActionResult Create()
        {
            ViewBag.studentID = new SelectList(db.student, "ID", "Name");
            ViewBag.subjectID = new SelectList(db.subject, "Id", "Course");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,studentID,subjectID,Marks,ClassDay")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.exam.Add(exam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.studentID = new SelectList(db.student, "ID", "Name", exam.studentID);
            ViewBag.subjectID = new SelectList(db.subject, "Id", "Course", exam.subjectID);
            return View(exam);
        }

        [HttpGet]
        public ActionResult UploadFromCSV()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFromCSV(HttpPostedFileBase file)
        {
            string filePath = string.Empty;
            //if (postedFile != null)
            //{
            string path = Server.MapPath("~/uploadedfiles/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            filePath = path + Path.GetFileName(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            file.SaveAs(filePath);

            //Read the contents of CSV file.
            string csvData = System.IO.File.ReadAllText(filePath);
            int countinsert = 0;
            int countupdate = 0;
            //Execute a loop over the rows.
            int count = 0;
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    if (count == 0)
                    {
                        count++;
                    }
                    else
                    {
                        Exam e = new Exam();
                        e.ID = int.Parse(row.Split(',')[0].ToString());
                        e.studentID = int.Parse(row.Split(',')[1].ToString());
                        e.subjectID = int.Parse(row.Split(',')[2].ToString());
                        e.Marks = float.Parse(row.Split(',')[3].ToString());
                        db.exam.Add(e);
                        db.SaveChanges();
                    }
                }
            }
            return View();
        }




        // GET: Exams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.exam.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            ViewBag.studentID = new SelectList(db.student, "ID", "Name", exam.studentID);
            ViewBag.subjectID = new SelectList(db.subject, "Id", "Course", exam.subjectID);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,studentID,subjectID,Marks,ClassDay")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.studentID = new SelectList(db.student, "ID", "Name", exam.studentID);
            ViewBag.subjectID = new SelectList(db.subject, "Id", "Course", exam.subjectID);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = db.exam.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exam exam = db.exam.Find(id);
            db.exam.Remove(exam);
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
