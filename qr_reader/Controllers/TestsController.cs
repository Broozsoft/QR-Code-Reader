using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qr_reader.Models;
using System.Xml.Linq;
using System.Text.RegularExpressions;


namespace qr_reader.Controllers
{
    public class TestsController : Controller
    {
        private readonly DbEntity _context;

        public TestsController(DbEntity context)
        {
            _context = context;
        }

        // GET: Tests
        public async Task<IActionResult> Index()
        {
            var dbEntity = _context.Tests.Include(t => t.Article_type);
            return View(await dbEntity.ToListAsync());
        }

        // GET: Tests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .Include(t => t.Article_type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: Tests/Create
        public IActionResult Create()
        {
            ViewData["Article_typeId"] = new SelectList(_context.Article_types, "Id","Name");
            return View();
        }



        // POST: Tests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(string property, string testName, int Articletype/*[Bind("Id,Name,Property,Datetime,Article_typeId")] Test test*/)
            
        {
            Test test = new Test();
            if (property!=null&&testName!=null&&Articletype!=0)
            {
            testName = testName.Replace(" ", "");
            List<string> formArray = new List<string>();
            formArray = property.Split("&").Skip(2).ToList();
            List<string> PropertyList = new List<string>();
            int n = formArray.Count;

            for (int i = 0; i < n; i++)
            {
                if (i == n - 1)
                {
                        string w = formArray[i];
                        w = w.Remove(w.Length - 1);
                        PropertyList.Add(w.Replace("+", "").Replace("/","-").Substring(10));
                    }
                    else { PropertyList.Add(formArray[i].Replace("+", "").Replace("/", "-").Substring(10)); }
            }
            XElement xmlDocument = new XElement(testName, PropertyList.Select(i => new XElement(i, i)));
            //xmlDocument.Save(@"C:\Users\Z003VN4R\source\repos\qr_reader Code\qr_reader\qr_reader\qr_reader\Data.xml");

            
            test.Datetime = DateTime.Now;
            test.Article_type = await _context.Article_types.SingleOrDefaultAsync(m => m.Id == Articletype);
            test.Name = testName;
            test.Property = xmlDocument.ToString();
            _context.Add(test);
             await _context.SaveChangesAsync();

               
                return RedirectToAction(nameof(Index));
            }
            ViewData["Article_typeId"] = new SelectList(_context.Article_types, "Id", "Id", test.Article_typeId);
            
            return View(test);
        }
        //public JsonResult CheckTestNameAvalability(string TestName)
        //{
        //    System.Threading.Thread.Sleep(200);
        //    var SearchData = _context.Tests.Where(x => x.Name == TestName).ToList();
        //    int c = SearchData.Count();

        //    if (SearchData != null)
        //    {
        //        return Json(SearchData);
        //    }
        //    else
        //    {
        //        return Json(0);
        //    }

        //}
        public IActionResult CheckTestNameAvalability(string TestName)
        {

            var SearchData = _context.Tests.Where(x => x.Name == TestName).Include(g=>g.Article_type).ToList();
            int c = SearchData.Count();
            if (c != 0)
            {
                return PartialView(SearchData);
            }
            else
            {
                return Json(0);
            }
        }



        // GET: Tests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests.SingleOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }
            ViewData["Article_typeId"] = new SelectList(_context.Article_types, "Id", "Id", test.Article_typeId);
            return View(test);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Property,Datetime,Article_typeId")] Test test)
        {
            if (id != test.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.Id))
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
            ViewData["Article_typeId"] = new SelectList(_context.Article_types, "Id", "Id", test.Article_typeId);
            return View(test);
        }

        // GET: Tests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .Include(t => t.Article_type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var test = await _context.Tests.SingleOrDefaultAsync(m => m.Id == id);
            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestExists(int id)
        {
            return _context.Tests.Any(e => e.Id == id);
        }
    }
}
