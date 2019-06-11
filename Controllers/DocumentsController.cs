using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Question7.Models;

namespace Question7.Controllers
{
    public class DocumentsController : Controller
    {
        private MarkdownEntities db = new MarkdownEntities();

        // GET: Documents
        public async Task<ActionResult> Index()
        {
            var documents = await db.Documents.ToListAsync();
            List<DocumentViewModel> models = new List<DocumentViewModel>();
            foreach(var doc in documents)
            {
                DocumentViewModel docVm = GetViewModel(doc);
                models.Add(docVm);
            }

            return View(models);
        }
        private DocumentViewModel GetViewModel(Document document)
        {
            DocumentViewModel model = new DocumentViewModel();
            model.Id = document.Id;
            model.DocumentText = document.DocumentText;
            model.CreationDate = document.CreationDate;
            model.DocumentName = document.DocumentName;
            return model;
        }
        // GET: Documents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            DocumentViewModel model = GetViewModel(document);
            return View(model);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            DocumentViewModel model = new DocumentViewModel();
            return View(model);
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Document document = new Document();
                document.DocumentName = model.DocumentName;
                document.DocumentText = model.DocumentText;
                document.CreationDate = DateTime.Now;
                db.Documents.Add(document);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Documents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            DocumentViewModel model = GetViewModel(document);
            return View(model);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Document document = await db.Documents.FindAsync(model.Id);
                document.DocumentName = model.DocumentName;
                document.DocumentText = model.DocumentText;
                db.Entry(document).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Documents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            DocumentViewModel model = GetViewModel(document);
            return View(model);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Document document = await db.Documents.FindAsync(id);
            db.Documents.Remove(document);
            await db.SaveChangesAsync();
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
