using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer)
        {
            try
            {
                _customerDal.Add(customer);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public IResult Delete(Customer customer)
        {
            try
            {
                _customerDal.Delete(customer);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public IDataResult<Customer> Get(int id)
        {
            Customer customer;
            try
            {
                customer = _customerDal.Get(c => c.Id == id);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<Customer>(exception.Message);
            }
            return new SuccessDataResult<Customer>(customer);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            List<Customer> customers;
            try
            {
                customers = _customerDal.GetAll();
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<Customer>>(exception.Message);
            }
            return new SuccessDataResult<List<Customer>>(customers);
        }

        public IResult Update(Customer customer)
        {
            Customer oldCustomer;
            try
            {
                oldCustomer = _customerDal.Get(c => c.Id == customer.Id);
                if(oldCustomer == null)
                {
                    return new ErrorResult("Customer not found");
                }

                oldCustomer.UserId = customer.UserId != default ? customer.UserId : oldCustomer.UserId;
                oldCustomer.CompanyName = customer.CompanyName != default ? customer.CompanyName : oldCustomer.CompanyName;

                _customerDal.Update(oldCustomer);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }
    }
}
