using LearnWords.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnWords.Controllers
{
    public class WordController : Controller
    {
        private readonly DataContext _db;

        public WordController(DataContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var words = await _db.Words.AsNoTracking()
                .OrderByDescending(w => w.Version)
                .ToListAsync();

            ViewBag.AllCount = await _db.Words.CountAsync();
            ViewBag.NewWords = await _db.Words.CountAsync(w => w.IsNew);

            return View("Index", words);
        }

        [HttpGet("Words/{partOfSpeech?}")]
        public async Task<IActionResult> Words(int? partOfSpeech)
        {
            var queryable = _db.Words.AsNoTracking();

            if (partOfSpeech.HasValue)
            {
                var part = (PartsOfSpeech)(Enum
                    .GetValues(typeof(PartsOfSpeech))
                    .GetValue(partOfSpeech.Value) ?? throw new InvalidOperationException());

                queryable = queryable.Where(w => w.PartOfSpeech == part);
            }

            ViewBag.AllCount = await _db.Words.CountAsync();
            ViewBag.NewWords = await _db.Words.CountAsync(w => w.IsNew);

            return View("Words", await queryable.OrderByDescending(w => w.Version).ToListAsync());
        }

        [HttpGet("CreateWord")]
        public IActionResult CreateWord()
        {
            return View("CreateWord");
        }

        [HttpPost("CreateWord")]
        public async Task<IActionResult> CreateWord(Word word)
        {
            word.English = word.English.Trim();
            word.Russian = word.Russian.Trim();
            await _db.Words.AddAsync(word);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("UpdateWord/{id}")]
        public async Task<IActionResult> UpdateWord(int id)
        {
            var word = await _db.Words.AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == id);

            return View("UpdateWord", word);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(Word word)
        {
            var dbWord = await _db.Words
                .FirstOrDefaultAsync(w => w.Id == word.Id);

            dbWord.Russian = word.Russian.Trim();
            dbWord.English = word.English.Trim();
            dbWord.PartOfSpeech = word.PartOfSpeech;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost("DeleteWord")]
        public async Task<IActionResult> DeleteWord(int id)
        {
            var word = await _db.Words.AsNoTracking()
                .FirstAsync(w => w.Id == id);

            _db.Words.Remove(word);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
