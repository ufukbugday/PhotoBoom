using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoBoom.Business;
using PhotoBoom.Endpoint.ViewModel;
using PhotoBoom.Entity;

namespace PhotoBoom.Endpoint.Controllers
{
    public class GalleryController : Controller
    {
	    private readonly IPhotoService photoService;
	    private readonly ITagService tagService;
	    private readonly IWebHostEnvironment hostEnvironment;


        public GalleryController(IPhotoService photoService, IWebHostEnvironment hostEnvironment, ITagService tagService)
        {
	        this.photoService = photoService;
	        this.hostEnvironment = hostEnvironment;
	        this.tagService = tagService;
        }

        // GET: Gallery
        public ViewResult Index()
        {
	        var photos = photoService.GetAll();
            var model = photos.Select(photo => new PhotoListViewModel
	        {
					Id = photo.Id, 
					Title = photo.Title, 
					TagStr = GetTags(photo), 
					PhotoPath = photo.PhotoPath
	        }).ToList();

            return View(model);
        }

        // GET: Gallery/Details/5
        public ViewResult Details(int? id)
        {
	        if (id == null)
	        {
		        Response.StatusCode = 404;
		        return View("NotFound", null);
	        }

            var photo = photoService.GetById(id.GetValueOrDefault());
            if (photo == null)
            {
	            Response.StatusCode = 404;
	            return View("NotFound", id);
            }
            var photoDetailsViewModel = new PhotoDetailsViewModel
            {
	            Photo = photo,
                TagStr = GetTags(photo),
	            PageTitle = "Photo Details"
            };
            return View(photoDetailsViewModel);
        }

        // GET: Gallery/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gallery/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,TagStr,Photo")] PhotoCreateViewModel model)
        {
	        if (ModelState.IsValid)
	        {
		        var uniqueFileName = ProcessUploadedFile(model);

		        #region Add Photo

		        var newPhoto = new Photo
		        {
			        Title = model.Title,
			        PhotoPath = uniqueFileName
		        };
		        photoService.Add(newPhoto);

		        #endregion

		        var tags = model.TagStr.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
		        foreach (var tag in tags)
		        {
			        var newTag = new Tag
			        {
				        Name = tag,
				        PhotoId = newPhoto.Id
                    };
			        tagService.Add(newTag);

		        }
                return RedirectToAction("Details", new { id = newPhoto.Id });
	        }

	        return View();
        }

        //// GET: Gallery/Edit/5
        //public ViewResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
	       //     Response.StatusCode = 404;
	       //     return View("NotFound", null);
        //    }

        //    var photo = photoService.GetById(id.GetValueOrDefault());

        //    if (photo == null)
        //    {
	       //     Response.StatusCode = 404;
	       //     return View("NotFound", id);
        //    }

        //    var model = new PhotoEditViewModel
        //    {
	       //     Id = photo.Id,
	       //     Title = photo.Title,
	       //     //Tags = photo.Tags,
	       //     ExistingPhotoPath = photo.PhotoPath
        //    };
        //    return View(model);
        //}

        //// POST: Gallery/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int id, [Bind("Id,Title,TagStr,Photo")] PhotoEditViewModel model)
        //{
        //    if (id != model.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
	       //         var photo = photoService.GetById(model.Id);
        //            photo.Title = model.Title;
	       //         if (model.Photo != null)
        //            {
	       //             if (model.ExistingPhotoPath != null)
	       //             {
		      //              string filePath = Path.Combine(hostEnvironment.WebRootPath,
			     //               "images", model.ExistingPhotoPath);
		      //              System.IO.File.Delete(filePath);
	       //             }
	       //             photo.PhotoPath = ProcessUploadedFile(model);

        //            }
        //            photoService.Update(photo);
                    
        //            var tags = tagService.GetByPhotoId(model.Id);
        //            foreach (var tag in tags)
        //            {
	       //             tagService.Update(tag);
        //            }

        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
	       //         if (!PhotoExists(model.Id))
        //            {
        //                return NotFound();
        //            }

	       //         throw;
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(model);
        //}

        // GET: Gallery/Delete/5
        public ViewResult Delete(int? id)
        {
            if (id == null)
            {
	            Response.StatusCode = 404;
	            return View("NotFound", null);
            }

            var photo = photoService.GetById(id.GetValueOrDefault());
            if (photo == null)
            {
	            Response.StatusCode = 404;
	            return View("NotFound", null);
            }

            var model = new PhotoDeleteViewModel
            {
	            Id = photo.Id,
	            Title = photo.Title,
	            ExistingPhotoPath = photo.PhotoPath,
            };
            return View(model);
        }

        // POST: Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
	        var tags = tagService.GetByPhotoId(id);
	        foreach (var tag in tags)
	        {
		        tagService.Delete(tag.Id);

            }
            photoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoExists(int id)
        {
	        return photoService.GetById(id) != null;

        }

        private string ProcessUploadedFile(PhotoCreateViewModel model)
        {
	        string uniqueFileName = null;
	        if (model.Photo != null)
	        {
		        string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
		        uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
		        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
		        using var fileStream = new FileStream(filePath, FileMode.Create);
		        model.Photo.CopyTo(fileStream);
	        }

	        return uniqueFileName;
        }
        private string GetTags(Photo photo)
        {
	        return string.Join(',', (tagService.GetByPhotoId(photo.Id)).Select(c => $"#{c.Name}").ToList());
        }
    }
}
