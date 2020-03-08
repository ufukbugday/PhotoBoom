using System;
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
	    private readonly IWebHostEnvironment hostEnvironment;


        public GalleryController(IPhotoService photoService, IWebHostEnvironment hostEnvironment)
        {
	        this.photoService = photoService;
	        this.hostEnvironment = hostEnvironment;
        }

        // GET: Gallery
        public ViewResult Index()
        {
	        var photos = photoService.GetAll();
            var model = photos.Select(photo => new PhotoListViewModel {Id = photo.Id, Title = photo.Title, Tag = photo.Tag, PhotoPath = photo.PhotoPath}).ToList();

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
        public IActionResult Create([Bind("Id,Title,Tag,Photo")] PhotoCreateViewModel model)
        {
	        if (ModelState.IsValid)
	        {
		        string uniqueFileName = ProcessUploadedFile(model);

		        var newPhoto = new Photo
		        {
			        Title = model.Title,
			        Tag = model.Tag,
			        PhotoPath = uniqueFileName
		        };

		        photoService.Add(newPhoto);
		        return RedirectToAction("Details", new { id = newPhoto.Id });
	        }

	        return View();
        }

        // GET: Gallery/Edit/5
        public ViewResult Edit(int? id)
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

            var model = new PhotoEditViewModel
            {
	            Id = photo.Id,
	            Title = photo.Title,
	            Tag = photo.Tag,
	            ExistingPhotoPath = photo.PhotoPath
            };
            return View(model);
        }

        // POST: Gallery/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Tag,Photo")] PhotoEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
	                var photo = photoService.GetById(model.Id);
	                photo.Title = model.Title;
	                photo.Tag = model.Tag;
	                if (model.Photo != null)
                    {
	                    if (model.ExistingPhotoPath != null)
	                    {
		                    string filePath = Path.Combine(hostEnvironment.WebRootPath,
			                    "images", model.ExistingPhotoPath);
		                    System.IO.File.Delete(filePath);
	                    }
	                    photo.PhotoPath = ProcessUploadedFile(model);

                    }
                    photoService.Update(photo);
                }
                catch (DbUpdateConcurrencyException)
                {
	                if (!PhotoExists(model.Id))
                    {
                        return NotFound();
                    }

	                throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

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
	            Tag = photo.Tag
            };
            return View(model);
        }

        // POST: Gallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
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
    }
}
