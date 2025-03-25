using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Domain.Entities;
using System.Threading.Tasks;

[Route("UserProfiles")]
public class UserProfilesController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserProfilesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var profiles = await _context.UserProfiles.Include(p => p.User).AsNoTracking().ToListAsync();
        return View(profiles);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Users = new SelectList(await _context.Users.ToListAsync(), "IdUser", "Name");
        return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nickname,PhoneNumber,Resume,IdUser")] UserProfile userProfile)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Users = new SelectList(await _context.Users.ToListAsync(), "IdUser", "Name", userProfile.IdUser);
            return View(userProfile);
        }

        try
        {
            _context.Add(userProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError("", "Ошибка при сохранении профиля. Проверьте данные.");
            ViewBag.Users = new SelectList(await _context.Users.ToListAsync(), "IdUser", "Name", userProfile.IdUser);
            return View(userProfile);
        }
    }

    [HttpGet("Edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var profile = await _context.UserProfiles.FindAsync(id);
        if (profile == null) return NotFound();

        ViewBag.Users = new SelectList(await _context.Users.ToListAsync(), "IdUser", "Name", profile.IdUser);
        return View(profile);
    }

    [HttpPost("Edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("IdUP,Nickname,PhoneNumber,Resume,IdUser")] UserProfile userProfile)
    {
        if (id != userProfile.IdUP) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(userProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.UserProfiles.Any(e => e.IdUP == id)) return NotFound();
                throw;
            }
        }

        ViewBag.Users = new SelectList(await _context.Users.ToListAsync(), "IdUser", "Name", userProfile.IdUser);
        return View(userProfile);
    }

    [HttpGet("Delete/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var profile = await _context.UserProfiles
            .Include(p => p.User)
            .FirstOrDefaultAsync(m => m.IdUP == id);
        if (profile == null) return NotFound();

        return View(profile);
    }

    [HttpPost("DeleteConfirmed/{id:int}")] 
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userProfile = await _context.UserProfiles.FindAsync(id);
        if (userProfile == null) return NotFound();

        _context.UserProfiles.Remove(userProfile);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
