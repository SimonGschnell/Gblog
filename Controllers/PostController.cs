using Gblog.Context;
using Gblog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Markdig;

namespace Gblog.Controllers
{
    public class PostController : Controller
    {
        private readonly PostContext _context;

        public PostController(PostContext context) {
            _context = context;
        }

        async public Task<IActionResult> Index()
        {
            var posts = await _context.Post.ToListAsync();
            var markdown_posts = posts.Select(post => { post.Content = Markdown.ToHtml(post.Content!); return post; });
            return View(markdown_posts);
        }

        public IActionResult Create()
        {
            var empty_post = new Post();
            return View(empty_post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<IActionResult> Create([Bind("InsertDate,Title,Content")] Post post)
        {
            if (ModelState.IsValid) 
            {
                if(post.InsertDate == null)
                {
                    post.InsertDate = DateTime.Now;
                }
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<IActionResult> Delete(int id)
        {

            var post = await _context.Post.FirstOrDefaultAsync(post => post.ID == id);
            if (post == null)
            {
                return NotFound();
            }
            _context.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        async public Task<IActionResult> Detail(int id)
        {

            var post = await _context.Post.FirstOrDefaultAsync(post => post.ID == id);
            if (post == null)
            {
                return NotFound();
            }
            post.Content = Markdown.ToHtml(post.Content!);

            return View(post);
        }
    }
}
