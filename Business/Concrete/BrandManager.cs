using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand brand)
        {
            try
            {
                _brandDal.Add(brand);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public IResult Delete(Brand brand)
        {
            try
            {
                _brandDal.Delete(brand);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public IDataResult<Brand> Get(int id)
        {
            Brand brand;
            try
            {
                brand = _brandDal.Get(b => b.Id == id);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<Brand>(exception.Message);
            }
            return new SuccessDataResult<Brand>(brand);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            List<Brand> brands;
            try
            {
                brands = _brandDal.GetAll();
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<Brand>>(exception.Message);
            }
            return new SuccessDataResult<List<Brand>>(brands);
        }

        public IResult Update(Brand brand)
        {
            Brand oldBrand;
            try
            {
                oldBrand = _brandDal.Get(b => b.Id == brand.Id);
                if (oldBrand == null)
                {
                    return new ErrorResult("Brand not found!");
                }
                oldBrand.Name = brand.Name != default ? brand.Name : oldBrand.Name;
                _brandDal.Update(oldBrand);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }
    }
}