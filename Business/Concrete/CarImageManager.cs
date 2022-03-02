using Business.Abstract;
using Business.Constants;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImagesOperationDto carImagesOperationDto)
        {
            if (_carImageDal.GetAll(p => p.CarID == carImagesOperationDto.Id).Count > 4)
            {
                return new ErrorResult(Messages.AboveImageAddingLimit);
            }

            foreach (var file in carImagesOperationDto.Images)
            {
                _carImageDal.Add(new CarImage
                {
                    CarID = carImagesOperationDto.CarId,
                    Date = DateTime.Now,
                    ImagePath = FileProcessHelper.Upload(DefaultNameOrPath.ImageDirectory, file).Data
                });
            }

            return new SuccessResult(Messages.AddCarImageMessage);
        }

        public IResult Delete(CarImage carImage)
        {
            var imageData = _carImageDal.Get(p => p.Id == carImage.Id);
            FileProcessHelper.Delete(imageData.ImagePath);
            _carImageDal.Delete(imageData);
            return new SuccessResult(Messages.DeleteCarImageMessage);
        }

        public IDataResult<CarImage> Get(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.Id == id));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            var getAllbyCarIdResult = _carImageDal.GetAll(p => p.CarID == carId);

            if (getAllbyCarIdResult.Count == 0)
            {
                return new SuccessDataResult<List<CarImage>>(new List<CarImage>
                {
                    new CarImage
                    {
                        Id = -1,
                        CarID = carId,
                        Date = DateTime.MinValue,
                        ImagePath = DefaultNameOrPath.NoImagePath
                    }
                });
            }

            return new SuccessDataResult<List<CarImage>>(getAllbyCarIdResult);
        }

        // ????
        public IResult Update(CarImagesOperationDto carImagesOperationDto)
        {
            foreach (var file in carImagesOperationDto.Images)
            {
                if (_carImageDal.Get(p => p.Id == carImagesOperationDto.Id) == null)
                {
                    return new ErrorResult(Messages.CarImageNotFound);
                }

                if (_carImageDal.GetAll(p => p.CarID == carImagesOperationDto.CarId).Count > 4)
                {
                    return new ErrorResult(Messages.AboveImageAddingLimit);
                }

                FileProcessHelper.Delete(_carImageDal.Get(p => p.Id == carImagesOperationDto.Id).ImagePath);

                _carImageDal.Update(new CarImage
                {
                    Id = carImagesOperationDto.Id,
                    CarID = carImagesOperationDto.CarId,
                    ImagePath = FileProcessHelper.Upload(DefaultNameOrPath.ImageDirectory, file).Data
                });
            }

            return new SuccessResult(Messages.EditCarImageMessage);
        }
    }
}
