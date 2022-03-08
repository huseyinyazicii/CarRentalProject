using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
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
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.AddCustomerMessage);
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult(Messages.DeleteCustomerMessage);
        }

        public IDataResult<Customer> Get(int id)
        {
            Customer customer = _customerDal.Get(c => c.Id == id);
            if(customer == null)
            {
                return new ErrorDataResult<Customer>(Messages.GetErrorCustomerMessage);
            }
            return new SuccessDataResult<Customer>(customer, Messages.GetSuccessCustomerMessage);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            List<Customer> customers = _customerDal.GetAll();
            if (customers == null)
            {
                return new ErrorDataResult<List<Customer>>(Messages.GetErrorCustomerMessage);
            }
            return new SuccessDataResult<List<Customer>>(customers, Messages.GetSuccessCustomerMessage);
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
        {
            var customers = _customerDal.GetCustomerDetailDto();
            return new SuccessDataResult<List<CustomerDetailDto>>(customers, Messages.GetSuccessCustomerMessage);
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            try
            {
                _customerDal.Update(customer);
                return new SuccessResult(Messages.EditCustomerMessage);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.ErrorCustomerFKMessage);
            }
        }
    }
}
