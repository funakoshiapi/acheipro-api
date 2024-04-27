using System;
using System.ComponentModel.Design;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using Shared.DataTransferObjects;
using static System.Net.WebRequestMethods;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Service
{
	public class CompanyImageService : ICompanyImageService
	{
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _environment;


        public CompanyImageService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IWebHostEnvironment environment)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
            _environment = environment;
		}

        public async Task AddImageModel(Guid companyId, CompanyImageDto image, bool trackChanges)
        {
            var companyEntity = await CheckIfCompanyExists(image.CompanyId, trackChanges);

            var imageEntity = _mapper.Map<CompanyImage>(image);

            _repository.CompanyImage.AddImage(imageEntity);

            await _repository.SaveAsync();
        }

       public async Task<CompanyImageDto> GetImageName(Guid companyId, bool trackChanges)
        {
            await CheckIfCompanyExists(companyId, trackChanges);

            var imageEntity = await _repository.CompanyImage.GetCompanyImage(companyId, trackChanges).ConfigureAwait(false);

            // WILL BE MODIFIED LATER

            var imageDto = _mapper.Map<CompanyImageDto>(imageEntity);

            return imageDto;


        }

        public async Task UpdateImage(CompanyImageDto model, bool trackChanges)
        {
            var company = await CheckIfCompanyExists(model.CompanyId, trackChanges);

            var imageEntity = _repository.CompanyImage.GetCompanyImage(model.CompanyId, trackChanges).Result;

            imageEntity.ImageName = model.ImageName;

            await _repository.SaveAsync();
        }


        public async Task<Company> CheckIfCompanyExists(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);

            if (company is null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            return company;

        }

        public async Task<Tuple<int, string>> SaveImageToResources(IFormFile imageFile)
        {
            try
            {
                var contentPath = this._environment.ContentRootPath;
                // path = "c://projects/productminiapi/uploads" ,not exactly something like that
                var path = Path.Combine(contentPath, "Resources");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Check the allowed extenstions
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));

                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                // we are trying to create a unique filename here
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);

                await imageFile.CopyToAsync(stream);
                stream.Close();

                //Image img = Image.FromStream(stream);
                

                //Bitmap b = new Bitmap(img);

                //Image i = ResizeImage(b, new Size(300, 300));

               // i.Save(fileWithPath.Replace(newFileName, newFileName));

                return new Tuple<int, string>(1, newFileName);
            }

            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");
            }
        }

        private static Image ResizeImage(Image imgToResize, Size size)
        {
            // Get the image current width
            int sourceWidth = imgToResize.Width;
            // Get the image current height
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            // Calculate width and height with new desired size
            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            nPercent = Math.Min(nPercentW, nPercentH);
            // New Width and Height
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
    }
}

