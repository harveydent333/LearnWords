using LearnWords.Entities;
using LearnWords.Extensions;
using LearnWords.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LearnWords.Controllers;

public class BlockController : Controller
{
    private readonly DataContext _db;

    public BlockController(DataContext db)
    {
        _db = db;
    }

    private Dictionary<int, TimeSpan> _periods = new()
    {
        {2, new TimeSpan(0,30,0)},
        {3, new TimeSpan(2,0,30)},
        {4, new TimeSpan(1,0,0,0)},
        {5, new TimeSpan(14,0,0,0)},
        {6, new TimeSpan(31,0,0,0)},
        {7, new TimeSpan(90,0,0,0)},
        {8, new TimeSpan(180,0,0,0)},
        {9, new TimeSpan(360,0,0,0)},
    };

    [HttpGet("Blocks")]
    public async Task<IActionResult> Blocks()
    {
        var blocks = await _db.BlockOfWords.AsNoTracking()
            .Include(b => b.Words)
            .ToListAsync();

        return View("Blocks", blocks);
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(long id)
    {
        var blockWords = await _db.BlockOfWords.AsNoTracking()
            .Where(b => b.Id == id)
            .Include(b => b.Words)
            .ThenInclude(bw => bw.Word)
            .Select(b =>
                new BlockWordsModel
                {
                    Id = b.Id,
                    Created = b.Created,
                    RepeatCount = b.RepeatCount,
                    NextRepeat = b.NextRepeat,
                    Words = b.Words.Select(w => w.Word).ToList()
                })
            .FirstOrDefaultAsync();

        return View("DetailsBlock", blockWords);
    }

    [HttpPost("CreateNewBlock")]
    public async Task<IActionResult> CreateNewBlock(NewBlockModel model)
    {
        var block = new BlockOfWords
        {
            Created = DateTime.Now,
            NextRepeat = DateTime.Now,
            RepeatCount = 1
        };

        var words = await _db.Words
            .Where(w => w.IsNew)
            .Take(model.NewWordsCount)
            .ToListAsync();

        foreach (var word in words)
        {
            var wordBlock = new WordBlock
            {
                BlockOfWords = block,
                Word = word
            };

            word.IsNew = false;

            await _db.WordBlocks.AddAsync(wordBlock);
        }

        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Blocks));
    }

    [HttpGet("Learn{id}")]
    public IActionResult Learn(int id)
    {
        return View("LearnBlock", id);
    }

    [HttpGet("GetPartsOfSpeech")]
    public IActionResult GetPartsOfSpeech()
    {
        return Ok(Enum
            .GetValues(typeof(PartsOfSpeech))
            .Cast<PartsOfSpeech>()
            .Select(psm => new
            {
                Value = Convert.ToInt32(psm),
                Name = psm.GetDisplayName()
            }));
    }

    [HttpGet("GetLearnData/{id}")]
    public async Task<IActionResult> GetLearnData(long id)
    {
        var blockWords = await _db.BlockOfWords.AsNoTracking()
            .Where(b => b.Id == id)
            .Include(b => b.Words)
            .ThenInclude(bw => bw.Word)
            .Select(b =>
                new BlockWordsModel
                {
                    Id = b.Id,
                    Words = b.Words.Select(w => w.Word).ToList()
                })
            .FirstOrDefaultAsync();

        return Ok(blockWords);
    }

    [HttpPost("SubmitBlock")]
    public async Task<IActionResult> SubmitBlock(BlockOfWords block)
    {
        var dbBlock = await _db.BlockOfWords
            .FirstOrDefaultAsync(bw => bw.Id == block.Id);

        dbBlock.RepeatCount++;

        if (_periods.Count > dbBlock.RepeatCount)
        {
            dbBlock.NextRepeat = DateTime.Now.Add(_periods[dbBlock.RepeatCount]);
        }

        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Blocks));
    }
}

