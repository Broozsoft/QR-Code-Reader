using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qr_reader.Models;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace qr_reader.Controllers
{
    public class ResultsController : Controller
    {
        private readonly DbEntity _context;

        public ResultsController(DbEntity context)
        {
            _context = context;
        }


        // GET: Results
        public async Task<IActionResult> Index()
        {
            var ttt = _context.Tests.Include(blog => blog.Article_type).ToList();
            var tests = _context.Tests.FirstOrDefault().Article_type;
            var tests2 = _context.Tests.FirstOrDefault();
            var dbEntity = _context.Results.Include(r => r.Test);
            return View(await dbEntity.ToListAsync());
        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Test)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        public IActionResult SearchQrCode(string QrCode)
        {
            string code = QrCode;
            int serialNumber=0;
            Array codeCharArray = code.ToCharArray();
            int lastIndex = codeCharArray.Length;
            int start = 0;

            List<string> codes = new List<string>();

            for (int i = 0; i < lastIndex; i++)
            {
                int last = i;
                if (code[i].ToString() == " " || ++last == lastIndex)
                {
                    int len = i - start;
                    string part = code.Substring(start, ++len);
                    codes.Add(part);
                    start = i;
                }
            }

            if (codes != null)
            {
                serialNumber =Int32.Parse(codes.ElementAt(2));
            }

            DbEntity db = new DbEntity();

            var unit = _context.Units.Where(unitObj => unitObj.SerialNumber == serialNumber).FirstOrDefault();

            var article = _context.Articles.Where(articleObj => articleObj.Id == unit.ArticleId).FirstOrDefault();
            var test = _context.Tests.Where(tests => tests.Article_typeId == article.Article_typeId).FirstOrDefault();
            
            //var test = _context.Tests.Find(1);
            XDocument xml = new XDocument();
            xml = XDocument.Parse(test.Property);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(test.Property);
            XmlNode rootNode = doc.DocumentElement;
            XmlNodeList name = rootNode.ChildNodes;
            var nodes = (from xl in xml.Elements() select xl).ToList();
            foreach (XmlNode item in name)
            {
                item.LocalName.ToString();
            }
            ViewBag.testName = rootNode.Name;
            ViewBag.nodes = name;
            return PartialView();
        }
        // GET: Results/Create
        public IActionResult AddResult(List<string> codes)
        {
            if (codes != null)
            {
                var serialNumber = codes.ElementAt(0);
            }

            var test = _context.Tests.Find(1);
            XDocument xml = new XDocument();
            xml = XDocument.Parse(test.Property);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(test.Property);
            XmlNode rootNode = doc.DocumentElement;
            XmlNodeList name =rootNode.ChildNodes;
            var nodes = (from xl in xml.Elements() select xl).ToList();
            foreach (XmlNode item in name)
            {
                item.LocalName.ToString();
            }
            ViewBag.testName = rootNode.Name;
            ViewBag.nodes = name;
            return View();
        }

        public IActionResult DisplayResultForm()
        {
            return View();
        }
        [HttpPost]
        // GET: Results/Create
        public IActionResult AddResult([Bind("Id,result,testId")] Result result, string data)
        {
            var test = _context.Tests.Find(1);
            XDocument xml = new XDocument();
            xml = XDocument.Parse(test.Property);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(test.Property);
            XmlNode rootNode = doc.DocumentElement;
            XmlNodeList name = rootNode.ChildNodes;
            var nodes = (from xl in xml.Elements() select xl).ToList();
            foreach (XmlNode item in name)
            {
                item.LocalName.ToString();
            }
            ViewBag.testName = rootNode.Name;
            ViewBag.nodes = name;
            return RedirectToAction("AddResult");
        }

        // GET: Results/Create
        public IActionResult Create()
        {

            ViewData["testId"] = new SelectList(_context.Tests, "Id", "Id");
            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,result,testId")] Result result)
        {
            if (ModelState.IsValid)
            {
                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["testId"] = new SelectList(_context.Tests, "Id", "Id", result.TestId);
            return View(result);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results.SingleOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            ViewData["testId"] = new SelectList(_context.Tests, "Id", "Id", result.TestId);
            return View(result);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,result,testId")] Result result)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.Id))
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
            ViewData["testId"] = new SelectList(_context.Tests, "Id", "Id", result.TestId);
            return View(result);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results
                .Include(r => r.Test)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _context.Results.SingleOrDefaultAsync(m => m.Id == id);
            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.Id == id);
        }
    }
}
