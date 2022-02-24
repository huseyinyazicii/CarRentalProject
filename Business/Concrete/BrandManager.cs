using Business.Abstract;
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

        public void Add(Brand brand)
        {
            _brandDal.Add(brand);
        }

        public void Delete(Brand brand)
        {
            _brandDal.Delete(brand);
        }

        public Brand Get(int id)
        {
            var result = _brandDal.Get(b => b.Id == id);
            return result;
        }

        public List<Brand> GetAll()
        {
            var result = _brandDal.GetAll();
            return result;
        }

        public void Update(Brand brand)
        {
            var oldBrand = _brandDal.Get(b => b.Id == brand.Id);

            if(oldBrand == null)
            {
                throw new InvalidOperationException();
            }

            oldBrand.Name = brand.Name != default ? brand.Name : oldBrand.Name;

            _brandDal.Update(oldBrand);
        }
    }
}