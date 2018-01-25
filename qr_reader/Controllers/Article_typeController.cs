using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qr_reader.Models;

namespace qr_reader.Controllers
{
    public class Article_typeController : Controller
    {
        private readonly DbEntity db;

        public Article_typeController(DbEntity context)
        {
            db = context;
        }

        // GET: Article_type
        public async Task<IActionResult> Index()
        {
            return View(await db.Article_types.ToListAsync());
        }

        // GET: Article_type/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article_type = await db.Article_types
                .SingleOrDefaultAsync(m => m.Id == id);
            if (article_type == null)
            {
                return NotFound();
            }

            return View(article_type);
        }

        // GET: Article_type/Create
        public IActionResult Create()
        {
        
            return View();
        }

        // POST: Article_type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Article_type article_type)
        {
            if (ModelState.IsValid)
            {
                db.Add(article_type);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article_type);
        }

        // GET: Article_type/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article_type = await db.Article_types.SingleOrDefaultAsync(m => m.Id == id);
            if (article_type == null)
            {
                return NotFound();
            }
            return View(article_type);
        }

        // POST: Article_type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Article_type article_type)
        {
            if (id != article_type.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(article_type);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Article_typeExists(article_type.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(article_type);
        }

        // GET: Article_type/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article_type = await db.Article_types
                .SingleOrDefaultAsync(m => m.Id == id);
            if (article_type == null)
            {
                return NotFound();
            }

            return View(article_type);
        }

        // POST: Article_type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article_type = await db.Article_types.SingleOrDefaultAsync(m => m.Id == id);
            db.Article_types.Remove(article_type);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Article_typeExists(int id)
        {
            return db.Article_types.Any(e => e.Id == id);
        }
    }
}
