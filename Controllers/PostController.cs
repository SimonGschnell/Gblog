using Gblog.Context;
using Gblog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Markdig;
using Gblog.ViewModels.PostViewModels;
using Microsoft.AspNetCore.Hosting;

namespace Gblog.Controllers
{
    public class PostController : Controller
    {
        private readonly PostContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostController(PostContext context, IWebHostEnvironment webHostEnvironment) {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        async public Task<IActionResult> Index()
        {
            var posts = await _context.Post.ToListAsync();
            var markdown_posts = posts.Select(post => { post.Content = Markdown.ToHtml(post.Content!); return post; });
            return View(markdown_posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var data = new CreatePostVM { Post = new Post(), Image = null };
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<IActionResult> Create(CreatePostVM data)
        {
            if (ModelState.IsValid) 
            {
                if(data.Post.InsertDate == null)
                {
                    data.Post.InsertDate = DateTime.Now;
                }
                // save image in wwwroot and save fileName in db
                if (data.Image != null && data.Image.Length > 0)
                {
                    var filename = Path.GetFileName(data.Image.FileName);
                    data.Post.Image = filename;
                    var folder_path = Path.Combine(_webHostEnvironment.WebRootPath, "images/post_images");
                    var file_path = Path.Combine(folder_path, filename);
                    // return View with validation error if the filename already exists
                    if (System.IO.File.Exists(file_path))
                    {
                        ModelState.AddModelError("Image", "The Filename already exists. Please rename the file!");
                        return View(data);
                    }
                    else 
                    { 
                        using (Stream file = new FileStream(file_path, FileMode.Create))
                        {
                            await data.Image.CopyToAsync(file);
                        }
                    }
                    
                }
                _context.Add(data.Post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(data);
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
