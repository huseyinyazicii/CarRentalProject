using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
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
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly ICarImageService _carImageService;

        public CarManager(ICarDal carDal, ICarImageService carImageService)
        {
            _carDal = carDal;
            _carImageService = carImageService;
        }

        [CacheRemoveAspect("Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.AddCarMessage);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.DeleteCarMessage);
        }

        public IDataResult<Car> Get(int id)
        {
            Car car = _carDal.Get(c => c.Id == id);
            if(car == null)
            {
                return new ErrorDataResult<Car>(Messages.GetErrorCarMessage);
            }
            return new SuccessDataResult<Car>(car, Messages.GetSuccessCarMessage);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            List<Car> cars = _carDal.GetAll();
            if (cars == null)
            {
                return new ErrorDataResult<List<Car>>(Messages.GetErrorCarMessage);
            }
            return new SuccessDataResult<List<Car>>(cars, Messages.GetSuccessCarMessage);
        }

        public IDataResult<CarDetailAndImagesDto> GetCarDetailAndImagesDto(int carId)
        {
            var result = _carDal.GetCarDetail(carId);
            var imageResult = _carImageService.GetAllByCarId(carId);

            if (result == null || imageResult.Success == false)
            {
                return new ErrorDataResult<CarDetailAndImagesDto>(Messages.GetErrorCarMessage);
            }

            var carDetailAndImagesDto = new CarDetailAndImagesDto
            {
                Car = result,
                CarImages = imageResult.Data
            };

            return new SuccessDataResult<CarDetailAndImagesDto>(carDetailAndImagesDto, Messages.GetSuccessCarMessage);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetail()
        {
            List<CarDetailDto> cars = _carDal.GetCarsDetail();
            if(cars == null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.GetErrorCarMessage);
            }
            return new SuccessDataResult<List<CarDetailDto>>(cars, Messages.GetSuccessCarMessage);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailByBrandId(int brandId)
        {
            List<CarDetailDto> carDetails = _carDal.GetCarsDetail(p => p.BrandId == brandId);
            if (carDetails == null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.GetErrorCarMessage);
            }
            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.GetErrorCarMessage);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailByBrandIdAndColorId(int brandId, int colorId)
        {
            List<CarDetailDto> carDetails = _carDal.GetCarsDetail(p => p.BrandId == brandId && p.ColorId == colorId);
            if (carDetails == null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.GetErrorCarMessage);
            }
            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.GetErrorCarMessage);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailByColorId(int colorId)
        {
            List<CarDetailDto> carDetails = _carDal.GetCarsDetail(p => p.ColorId == colorId);
            if (carDetails == null)
            {
                return new ErrorDataResult<List<CarDetailDto>>(Messages.GetErrorCarMessage);
            }
            return new SuccessDataResult<List<CarDetailDto>>(carDetails, Messages.GetErrorCarMessage);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            FluentValidationTool.Validate(new CarValidator(), car);

            _carDal.Update(car);
            return new SuccessResult(Messages.EditCarMessage);

            //Car oldCar = _carDal.Get(c => c.Id == car.Id);

            //if (oldCar == null)
            //{
            //    return new ErrorResult("Car not found");
            //}

            //oldCar.BrandId = car.BrandId != default ? car.BrandId : oldCar.BrandId;
            //oldCar.ColorId = car.ColorId != default ? car.ColorId : oldCar.ColorId;
            //oldCar.DailyPrice = car.DailyPrice != default ? car.DailyPrice : oldCar.DailyPrice;
            //oldCar.Description = car.Description != default ? car.Description : oldCar.Description;
            //oldCar.ModelYear = car.ModelYear != default ? car.ModelYear : oldCar.ModelYear;

            //_carDal.Update(oldCar);
        }
    }
}
