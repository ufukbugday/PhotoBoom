using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using PhotoBoom.Business;
using PhotoBoom.Endpoint.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PhotoBoom.DataAccess;
using PhotoBoom.Endpoint.ViewModel;


namespace PhotoBoom.XUnitTests.Controller
{
	public class GalleryControllerShould
	{
		private readonly Mock<IPhotoService> photoServiceMock;
		private readonly Mock<IWebHostEnvironment> webHostEnvironmentMock;
		private readonly GalleryController sut;

		public GalleryControllerShould()
		{
			photoServiceMock = new Mock<IPhotoService>();
			webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
			sut = new GalleryController(photoServiceMock.Object, webHostEnvironmentMock.Object);
		}

		[Fact]
		public void ReturnViewForIndex()
		{
			IActionResult result = sut.Index();

			Assert.IsType<ViewResult>(result);
		}

		[Fact]
		public void ReturnViewWhenInvalidModelState()
		{
			sut.ModelState.AddModelError("x", "Test Error");

			var viewModel = new PhotoCreateViewModel
			{
				Title = "Title Title Title Title Title Title Title Title Title Title Title Title Title Title Title Title Title Title Title "
			};

			IActionResult result = sut.Create(viewModel);

			ViewResult viewResult = Assert.IsType<ViewResult>(result);

			var model = Assert.IsType<PhotoCreateViewModel>(viewResult.Model);

			Assert.Equal(viewModel.Title, model.Title);
		}
    }
}
