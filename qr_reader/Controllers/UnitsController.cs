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
    public class UnitsController : Controller
    {
        private readonly DbEntity _context;

        public UnitsController(DbEntity context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult FindUnit()
        {


            return View();
        }
        [HttpPost]
        public IActionResult FindUnit(string readerString)
        {
            if (readerString!=null|| readerString.Count()!=0)
            {
                List<string> readerList = readerString.Split(' ').ToList();

                int unitSerialNumber = int.Parse(readerList[2]);
                Unit unit = _context.Units.First(c => c.SerialNumber == unitSerialNumber);
                //return View(unit);
                return RedirectToAction("Details", new { id = unit.Id });
            }
            else return View();
        }
        // GET: Units
        public async Task<IActionResult> Index()
        {
            var dbEntity = _context.Units.Include(u => u.Article).Include(u => u.Order);
            return View(await dbEntity.ToListAsync());
        }

        // GET: Units/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .Include(u => u.Article)
                .Include(u => u.Order)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: Units/Create
        public IActionResult Create()
        {
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Id");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SerialNumber,ArticleId,OrderId")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Id", unit.ArticleId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", unit.OrderId);
            return View(unit);
        }

        // GET: Units/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Units.SingleOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Id", unit.ArticleId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", unit.OrderId);
            return View(unit);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SerialNumber,ArticleId,OrderId")] Unit unit)
        {
            if (id != unit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitExists(unit.Id))
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
            ViewData["ArticleId"] = new SelectList(_context.Articles, "Id", "Id", unit.ArticleId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", unit.OrderId);
            return View(unit);
        }

        // GET: Units/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .Include(u => u.Article)
                .Include(u => u.Order)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unit = await _context.Units.SingleOrDefaultAsync(m => m.Id == id);
            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitExists(int id)
        {
            return _context.Units.Any(e => e.Id == id);
        }
    }
}
