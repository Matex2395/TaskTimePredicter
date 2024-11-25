using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskTimePredicter.Data;
using TaskTimePredicter.Models;

namespace TaskTimePredicter.Controllers
{
    [Authorize]
    public class QuestsController : Controller
    {
        private readonly AppDbContext _context;

        public QuestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Quests
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Quests.Include(q => q.Category).Include(q => q.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Quests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests
                .Include(q => q.Category)
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.QuestId == id);
            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        // GET: Quests/Create
        public IActionResult Create()
        {
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
            return View();
        }

        // POST: Quests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestId,QuestName,EstimatedTime,ActualTime,QuestState,CreationDate,UserId,SubcategoryId,ProjectId")] Quest quest)
        {
            if (ModelState.IsValid)
            {
                //Validación 'CreatedAt' != Nulo ni vacío
                if (quest.CreationDate == default)
                {
                    quest.CreationDate = DateOnly.FromDateTime(DateTime.Now);
                }

                //Búsqueda de Usuario correspondiente por UserId
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim != null)
                {
                    quest.UserId = int.Parse(userIdClaim);
                    quest.User = await _context.Users
                        .FirstOrDefaultAsync(u => u.UserId == quest.UserId.Value);
                }
                //Búsqueda de Subcategoría correspondiente por SubcategoryId
                if (quest.SubcategoryId.HasValue)
                {
                    var subcategory = await _context.Subcategories
                        .FirstOrDefaultAsync(s => s.SubcategoryId == quest.SubcategoryId.Value);
                    if (subcategory != null)
                    {
                        quest.CategoryId = subcategory.CategoryId;
                        quest.Category = await _context.Categories
                            .FirstOrDefaultAsync(c => c.CategoryId == quest.CategoryId);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Subcategoría no válida.";
                        ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
                        ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryName");
                        return View(quest);
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Seleccione una Subcategoría para Asociar";
                    ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
                    ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryName");
                    return View(quest);
                }
                if (quest.ProjectId.HasValue)
                {
                    quest.Project = await _context.Projects
                        .FirstOrDefaultAsync(p => p.ProjectId == quest.ProjectId.Value);
                }
                else
                {
                    TempData["ErrorMessage"] = "Seleccione un Proyecto para Asociar";
                    ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
                    ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryName");
                    return View(quest);
                }

                _context.Add(quest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryName");
            return View(quest);
        }

        // GET: Quests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryName");
            return View(quest);
        }

        // POST: Quests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestId,QuestName,EstimatedTime,ActualTime,QuestState,CreationDate,UserId,SubcategoryId,ProjectId")] Quest quest)
        {
            if (id != quest.QuestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var prevQuest = await _context.Quests.FirstOrDefaultAsync(d => d.QuestId == quest.QuestId);
                    quest.CreationDate = prevQuest.CreationDate;
                    //Búsqueda de Usuario correspondiente por UserId
                    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (userIdClaim != null)
                    {
                        quest.UserId = int.Parse(userIdClaim);
                        quest.User = await _context.Users
                            .FirstOrDefaultAsync(u => u.UserId == quest.UserId.Value);
                    }
                    //Búsqueda de Subcategoría correspondiente por SubcategoryId
                    if (quest.SubcategoryId.HasValue)
                    {
                        var subcategory = await _context.Subcategories
                            .FirstOrDefaultAsync(s => s.SubcategoryId == quest.SubcategoryId.Value);
                        if (subcategory != null)
                        {
                            quest.CategoryId = subcategory.CategoryId;
                            quest.Category = await _context.Categories
                                .FirstOrDefaultAsync(c => c.CategoryId == quest.CategoryId);
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Subcategoría no válida.";
                            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
                            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryName");
                            return View(quest);
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Seleccione una Subcategoría para Asociar";
                        ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
                        ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryName");
                        return View(quest);
                    }
                    if (quest.ProjectId.HasValue)
                    {
                        quest.Project = await _context.Projects
                            .FirstOrDefaultAsync(p => p.ProjectId == quest.ProjectId.Value);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Seleccione un Proyecto para Asociar";
                        ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
                        ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryName");
                        return View(quest);
                    }
                    _context.Update(quest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestExists(quest.QuestId))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectName");
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategories, "SubcategoryId", "SubcategoryName");
            return View(quest);
        }

        // GET: Quests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await _context.Quests
                .Include(q => q.Category)
                .Include(q => q.User)
                .FirstOrDefaultAsync(m => m.QuestId == id);
            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        // POST: Quests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quest = await _context.Quests.FindAsync(id);
            if (quest != null)
            {
                _context.Quests.Remove(quest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestExists(int id)
        {
            return _context.Quests.Any(e => e.QuestId == id);
        }
    }
}
