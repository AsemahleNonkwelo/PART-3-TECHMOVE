using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechMovePOE.API.Data;
using TechMovePOE.API.Models;

namespace TechMovePOE.Controllers
{
    public class ContractController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ContractController(
            AppDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Contract
        public async Task<IActionResult> Index(string status)
        {
            var contracts = _context.Contracts
                .Include(c => c.Client)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                contracts = contracts.Where(c => c.Status == status);
            }

            return View(await contracts.ToListAsync());
        }

        // GET: Contract/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.ContractId == id);

            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // GET: Contract/Create
        public IActionResult Create()
        {
            ViewBag.ClientId = new SelectList(
                _context.Clients,
                "ClientId",
                "Name");

            return View();
        }

        // POST: Contract/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ContractId,ClientId,StartDate,EndDate,Status,ServiceLevel")]
            Contract contract)
        {
            if (contract.AgreementFile != null)
            {
                var extension =
                    Path.GetExtension(contract.AgreementFile.FileName);

                if (extension.ToLower() != ".pdf")
                {
                    ModelState.AddModelError(
                        "",
                        "Only PDF files are allowed.");

                    ViewBag.ClientId = new SelectList(
                        _context.Clients,
                        "ClientId",
                        "Name",
                        contract.ClientId);

                    return View(contract);
                }

                string uploadsFolder =
                    Path.Combine(
                        _environment.WebRootPath,
                        "contracts");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName =
                    Guid.NewGuid().ToString() + ".pdf";

                string filePath =
                    Path.Combine(
                        uploadsFolder,
                        uniqueFileName);

                using (var stream =
                    new FileStream(filePath, FileMode.Create))
                {
                    await contract.AgreementFile.CopyToAsync(stream);
                }

                contract.AgreementFilePath =
                    "/contracts/" + uniqueFileName;
            }

            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClientId = new SelectList(
                _context.Clients,
                "ClientId",
                "Name",
                contract.ClientId);

            return View(contract);
        }

        // GET: Contract/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract =
                await _context.Contracts.FindAsync(id);

            if (contract == null)
            {
                return NotFound();
            }

            ViewBag.ClientId = new SelectList(
                _context.Clients,
                "ClientId",
                "Name",
                contract.ClientId);

            return View(contract);
        }

        // POST: Contract/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("ContractId,ClientId,StartDate,EndDate,Status,ServiceLevel,AgreementFilePath")]
            Contract contract)
        {
            if (id != contract.ContractId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.ContractId))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClientId = new SelectList(
                _context.Clients,
                "ClientId",
                "Name",
                contract.ClientId);

            return View(contract);
        }

        // GET: Contract/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.ContractId == id);

            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract =
                await _context.Contracts.FindAsync(id);

            if (contract != null)
            {
                _context.Contracts.Remove(contract);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(
                e => e.ContractId == id);
        }
    }
}