using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EliteBlog.Domain.Models;
using EliteBlog.Repository;
using EliteBlog.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using EliteBlog.Models.ViewModels;
using AutoMapper;

namespace EliteBlog.Controllers
{
    public class PostsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;


        public PostsController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var posts = _unitOfWork.Posts.GetAll();
            return View(await posts);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var post = await _unitOfWork.Posts
                .Get(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public async Task<IActionResult> Create()
        {
            var dataBaseCategories = await _unitOfWork.Categories.GetAll();
            var categoriesSelectList = _mapper.Map<List<SelectListItem>>(dataBaseCategories);

            ViewData["Categories"] = categoriesSelectList;
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Text")] Post post, string[] Categories, IFormFile Photo)
        {
            if (ModelState.IsValid)
            {
                var supportedTypes = new[] { "jpg", "jpeg", "png", "JPG", "JPEG", "PNG" };
                var fileExt = System.IO.Path.GetExtension(Photo.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    ModelState.AddModelError(string.Empty, "File Type Is InValid - Only Upload jpg/jpeg/png Files");
                    return View();
                }

                var path = Path.Combine(this._webHostEnvironment.WebRootPath, "images", Photo.FileName);
                var stream = new FileStream(path, FileMode.Create);
                await Photo.CopyToAsync(stream);
                post.Photo = Photo.FileName;

                post.ID = Guid.NewGuid();

                var postCategories = new List<PostCategory>();

                foreach(var categoryID in Categories)
                {
                    var newPostCategory = new PostCategory()
                    {
                        CategoryID = Guid.Parse(categoryID),
                        PostID = post.ID
                    };
                    postCategories.Add(newPostCategory);
                }

                if(postCategories.Count > 0)
                {
                    post.Categories = postCategories;
                }

                await _unitOfWork.Posts.Add(post);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(await _unitOfWork.Categories.GetAll(), "ID", "Title", post.Categories);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var post = await _unitOfWork.Posts.Get(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(await _unitOfWork.Categories.GetAll(), "ID", "Title", post.Categories);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Text,CategoryID,ID")] Post post)
        {
            if (id != post.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Posts.Update(post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.ID))
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
            ViewData["CategoryID"] = new SelectList(await _unitOfWork.Categories.GetAll(), "ID", "Title", post.Categories);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var post = await _unitOfWork.Posts.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _unitOfWork.Posts.Get(id);

            _unitOfWork.Posts.Delete(post);

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(Guid id)
        {
            var posts =  _unitOfWork.Posts.GetAll();
            
            return false;
        }
    }
}
