using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [SecuredOperation("admin,brand.add")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            _brandDal.Add(brand);
            return new SuccessResult(Messages.AddBrandMessage);
        }

        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.DeleteBrandMessage);
        }

        public IDataResult<Brand> Get(int id)
        {
            Brand brand = _brandDal.Get(b => b.Id == id);
            if(brand == null)
            {
                return new ErrorDataResult<Brand>(Messages.GetErrorBrandMessage);
            }
            return new SuccessDataResult<Brand>(brand, Messages.GetSuccessBrandMessage);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            List<Brand> brands = _brandDal.GetAll();
            if(brands == null)
            {
                return new ErrorDataResult<List<Brand>>(Messages.GetErrorBrandMessage);
            }
            return new SuccessDataResult<List<Brand>>(brands, Messages.GetSuccessBrandMessage);
        }

        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult(Messages.EditBrandMessage);

            //Brand oldBrand = _brandDal.Get(b => b.Id == brand.Id);
            //if (oldBrand == null)
            //{
            //    return new ErrorResult("Brand not found!");
            //}
            //oldBrand.Name = brand.Name != default ? brand.Name : oldBrand.Name;
            //_brandDal.Update(oldBrand);
        }
    }
}