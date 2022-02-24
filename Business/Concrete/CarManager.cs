using Business.Abstract;
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

        public void Add(Car car)
        {
            if(car.Description.Length < 2 || car.DailyPrice < 0)
            {
                return;
            }
            _carDal.Add(car);
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }

        public Car Get(int id)
        {
            var result = _carDal.Get(c => c.Id == id);
            return result;
        }

        public List<Car> GetAll()
        {
            var result = _carDal.GetAll();
            return result;
        }

        public CarDetailDto GetCarDetail(int carId)
        {
            var result = _carDal.GetCarDetail(carId);
            return result;
        }

        public List<Car> GetCarsByBrandId(int brandId)
        {
            var result = _carDal.GetAll(c => c.BrandId == brandId);
            return result;
        }

        public List<Car> GetCarsByColorId(int colorId)
        {
            var result = _carDal.GetAll(c => c.ColorId == colorId);
            return result;
        }

        public List<CarDetailDto> GetCarsDetail()
        {
            var result = _carDal.GetCarsDetail();
            return result;
        }

        public void Update(Car car)
        {
            var oldCar = _carDal.Get(c => c.Id == car.Id);

            if(oldCar == null)
            {
                throw new InvalidOperationException("Car is not found!");
            }

            oldCar.BrandId = car.BrandId != default ? car.BrandId : oldCar.BrandId;
            oldCar.ColorId = car.ColorId != default ? car.ColorId : oldCar.ColorId;
            oldCar.DailyPrice = car.DailyPrice != default ? car.DailyPrice : oldCar.DailyPrice;
            oldCar.Description = car.Description != default ? car.Description : oldCar.Description;
            oldCar.ModelYear = car.ModelYear != default ? car.ModelYear : oldCar.ModelYear;

            _carDal.Update(oldCar);
        }
    }
}
