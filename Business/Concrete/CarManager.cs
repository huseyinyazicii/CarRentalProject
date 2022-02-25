using Business.Abstract;
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
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car car)
        {
            if (car.Description.Length < 2 || car.DailyPrice < 0)
            {
                return new ErrorResult("Validation Error");
            }

            try
            {
                _carDal.Add(car);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }

        public IResult Delete(Car car)
        {
            try
            {
                _carDal.Delete(car);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public IDataResult<Car> Get(int id)
        {
            Car car;
            try
            {
                car = _carDal.Get(c => c.Id == id);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<Car>(exception.Message);
            }
            return new SuccessDataResult<Car>(car);
        }

        public IDataResult<List<Car>> GetAll()
        {
            List<Car> cars;
            try
            {
                cars = _carDal.GetAll();
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<Car>>(exception.Message);
            }
            return new SuccessDataResult<List<Car>>(cars);
        }

        public IDataResult<CarDetailDto> GetCarDetail(int carId)
        {
            CarDetailDto car;
            try
            {
                car = _carDal.GetCarDetail(carId);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<CarDetailDto>(exception.Message);
            }
            return new SuccessDataResult<CarDetailDto>(car);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            List<Car> cars;
            try
            {
                cars = _carDal.GetAll(c => c.BrandId == brandId);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<Car>>(exception.Message);
            }
            return new SuccessDataResult<List<Car>>(cars);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            List<Car> cars;
            try
            {
                cars = _carDal.GetAll(c => c.ColorId == colorId);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<Car>>(exception.Message);
            }
            return new SuccessDataResult<List<Car>>(cars);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetail()
        {
            List<CarDetailDto> cars;
            try
            {
                cars = _carDal.GetCarsDetail();
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<CarDetailDto>>(exception.Message);
            }
            return new SuccessDataResult<List<CarDetailDto>>(cars);
        }

        public IResult Update(Car car)
        {
            Car oldCar;
            try
            {
                oldCar = _carDal.Get(c => c.Id == car.Id);

                if (oldCar == null)
                {
                    return new ErrorResult("Car not found");
                }

                oldCar.BrandId = car.BrandId != default ? car.BrandId : oldCar.BrandId;
                oldCar.ColorId = car.ColorId != default ? car.ColorId : oldCar.ColorId;
                oldCar.DailyPrice = car.DailyPrice != default ? car.DailyPrice : oldCar.DailyPrice;
                oldCar.Description = car.Description != default ? car.Description : oldCar.Description;
                oldCar.ModelYear = car.ModelYear != default ? car.ModelYear : oldCar.ModelYear;

                _carDal.Update(oldCar);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }
    }
}
