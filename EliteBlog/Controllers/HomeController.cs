using AutoMapper;
using EliteBlog.Domain.Interfaces;
using EliteBlog.Domain.Models;
using EliteBlog.Models;
using EliteBlog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EliteBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string SearchString, Guid CategoryID)
        {

            var databasePosts = await _unitOfWork.Posts.GetAllPosts();

            if (!String.IsNullOrEmpty(SearchString))
            {
                databasePosts = databasePosts.Where(s => s.Title.Contains(SearchString)
                                       || s.Text.Contains(SearchString));
                ViewData["CurrentStringFilter"] = SearchString;
            }

            if(CategoryID != Guid.Empty)
            {
                databasePosts = databasePosts.Where(p => p.Categories.Any(c => c.CategoryID == CategoryID));
                var filteredCategory = await _unitOfWork.Categories.Get(CategoryID);
                ViewData["CurrentCategoryFilter"] = filteredCategory;
            }

            var posts = _mapper.Map<List<PostViewModel>>(databasePosts);
            foreach(var post in posts)
            {
                post.Categories = _mapper.Map<IList<CategoryViewModel>>(await _unitOfWork.Categories.GetCategoriesByPostID(post.ID));
            }
            
            return View(posts);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            
            var mainPost = await _unitOfWork.Posts.GetPostWithComments(id);
            var mainComments = await _unitOfWork.Comments.GetCommentsByPostID(mainPost.ID);
            var mainCategories = await _unitOfWork.Categories.GetCategoriesByPostID(mainPost.ID);
            if (mainPost == null)
            {
                return NotFound();
            }
            var postVM = _mapper.Map<PostDetailViewModel>(mainPost);
            var commentsVM = _mapper.Map<IList<CommentViewModel>>(mainComments);
            var categoriesVM = _mapper.Map<IList<CategoryViewModel>>(mainCategories);
            postVM.Categories = categoriesVM;
            postVM.Comments = commentsVM;
         
            return View(postVM);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: Posts/Create
        public async Task<IActionResult> Create()
        {
            var user = HttpContext.User;
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

                var path = Path.Combine(this._webHostEnvironment.WebRootPath, "images/uploads", Photo.FileName);
                var stream = new FileStream(path, FileMode.Create);
                await Photo.CopyToAsync(stream);
                post.Photo = Photo.FileName;

                post.ID = Guid.NewGuid();

                var postCategories = new List<PostCategory>();

                foreach (var categoryID in Categories)
                {
                    var newPostCategory = new PostCategory()
                    {
                        CategoryID = Guid.Parse(categoryID),
                        PostID = post.ID
                    };
                    postCategories.Add(newPostCategory);
                }

                if (postCategories.Count > 0)
                {
                    post.Categories = postCategories;
                }

                var user = await _userManager.GetUserAsync(HttpContext.User);
                post.CreatedBy = user.Id;

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

            var postCategories = await _unitOfWork.Categories.GetCategoriesByPostID(post.ID);
            var dataBaseCategories = await _unitOfWork.Categories.GetAll();

            var postEditVM = new PostEditViewModel()
            {
                Post = _mapper.Map<PostViewModel>(post),
                Categories = _mapper.Map<List<SelectListItem>>(dataBaseCategories),
            };

            return View(postEditVM);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Post")] PostEditViewModel postEditVM, string[] Categories, IFormFile Photo)

        {
            if (id != postEditVM.Post.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var postVM = postEditVM.Post;
                    var post = await _unitOfWork.Posts.Get(id);

                    if(Photo != null)
                    {
                        var supportedTypes = new[] { "jpg", "jpeg", "png", "JPG", "JPEG", "PNG" };
                        var fileExt = System.IO.Path.GetExtension(Photo.FileName).Substring(1);
                        if (!supportedTypes.Contains(fileExt))
                        {
                            ModelState.AddModelError(string.Empty, "File Type Is InValid - Only Upload jpg/jpeg/png Files");
                            return View();
                        }

                        var path = Path.Combine(this._webHostEnvironment.WebRootPath, "images/uploads", Photo.FileName);
                        var stream = new FileStream(path, FileMode.Create);
                        await Photo.CopyToAsync(stream);
                        post.Photo = Photo.FileName;
                    }

                    
                    post.Title = postVM.Title;
                    post.Text = postVM.Text;

                    var postCategories = new List<PostCategory>();
                    var presentPostCategories = await _unitOfWork.PostCategories.GetPostCategoriesByPostID(post.ID);

                    foreach (var categoryID in Categories)
                    {
                        var newPostCategory = new PostCategory()
                        {
                            CategoryID = Guid.Parse(categoryID),
                            PostID = post.ID
                        };
                        postCategories.Add(newPostCategory);
                    }
                    postCategories.GroupBy(x => x.PostID).Distinct().ToList();
                    post.Categories = postCategories;

                    _unitOfWork.Posts.Update(post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var postExist = await PostExists(postEditVM.Post.ID);
                    if (!postExist)
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
            ViewData["CategoryID"] = new SelectList(await _unitOfWork.Categories.GetAll(), "ID", "Title", postEditVM.Post.Categories);
            return View(postEditVM);
        }

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

        private async Task<bool> PostExists(Guid id)
        {
            var post = await _unitOfWork.Posts.Get(id);
            if(post == null)
            {
                return false;
            }
            return true;
        }

        public async Task<IActionResult> AddComment(Comment Comment)
        {
            if (ModelState.IsValid)
            {

                if (Comment.PostID == Guid.Empty)
                {
                    return NotFound();
                }

                var post = await _unitOfWork.Posts.Get(Comment.PostID);

                if (post == null)
                {
                    return NotFound();
                }

                var user = await _userManager.GetUserAsync(HttpContext.User);
                Comment.CreatedBy = user.Id;

                Comment.ID = Guid.NewGuid();

                await _unitOfWork.Comments.Add(Comment);

                var AddedComment = await _unitOfWork.Comments.Get(Comment.ID);
                var AddedCommentVM = _mapper.Map<CommentViewModel>(AddedComment);

                AddedCommentVM.CreatedAt = AddedComment.CreatedAt.ToShortTimeString() + ", " + AddedComment.CreatedAt.ToLongDateString();

                return Json(new { 
                    success = true,
                    data = AddedCommentVM
                });
            }
            return Json(new { success = false });
        }

        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAll();
                var categoriesVM = _mapper.Map<IList<CategoryViewModel>>(categories);
                return Json(new { success = true, data = categoriesVM });
            }
            catch(Exception error)
            {
                return Json(new { success = false, error = error.Message });
            }
        }

    }
}
