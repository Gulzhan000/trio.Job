using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Entities;

public class UserProfilesController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserProfilesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: UserProfiles
    public async Task<IActionResult> Index()
    {
        return View(await _context.UserProfiles.ToListAsync());
    }

    // GET: UserProfiles/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(m => m.Id == id);
        if (userProfile == null)
            return NotFound();

        return View(userProfile);
    }

    // GET: UserProfiles/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: UserProfiles/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,PhoneNumber,Resume,Description,DateOfBirth")] UserProfile userProfile)
    {
        if (ModelState.IsValid)
        {
            _context.Add(userProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(userProfile);
    }

    // GET: UserProfiles/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var userProfile = await _context.UserProfiles.FindAsync(id);
        if (userProfile == null)
            return NotFound();

        return View(userProfile);
    }

    // POST: UserProfiles/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,PhoneNumber,Resume,Description,DateOfBirth")] UserProfile userProfile)
    {
        if (id != userProfile.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(userProfile);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.UserProfiles.Any(e => e.Id == userProfile.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(userProfile);
    }

    // GET: UserProfiles/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(m => m.Id == id);
        if (userProfile == null)
            return NotFound();

        return View(userProfile);
    }

    // POST: UserProfiles/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userProfile = await _context.UserProfiles.FindAsync(id);
        _context.UserProfiles.Remove(userProfile);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
