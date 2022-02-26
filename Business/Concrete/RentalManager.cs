using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
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
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {
            FluentValidationTool.Validate(new RentalValidator(), rental);

            try
            {
                var result = _rentalDal.Get(r => r.CarId == rental.CarId && r.ReturnDate == null);
                if(result != null)
                {
                    return new ErrorResult("The car is not available for rental");
                }
                _rentalDal.Add(rental);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public IResult Delete(Rental rental)
        {
            try
            {
                _rentalDal.Delete(rental);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public IDataResult<Rental> Get(int id)
        {
            Rental rental;
            try
            {
                rental = _rentalDal.Get(r => r.Id == id);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<Rental>(exception.Message);
            }
            return new SuccessDataResult<Rental>(rental);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            List<Rental> rentals;
            try
            {
                rentals = _rentalDal.GetAll();
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<Rental>>(exception.Message);
            }
            return new SuccessDataResult<List<Rental>>(rentals);
        }

        public IResult Update(Rental rental)
        {
            FluentValidationTool.Validate(new RentalValidator(), rental);

            Rental oldRental;
            try
            {
                oldRental = _rentalDal.Get(r => r.Id == rental.Id);
                if(oldRental == null)
                {
                    return new ErrorResult("Rental not found");
                }

                oldRental.CarId = rental.CarId != default ? rental.CarId : oldRental.CarId;
                oldRental.CustomerId = rental.CustomerId != default ? rental.CustomerId : oldRental.CustomerId;
                oldRental.RentDate = rental.RentDate != default ? rental.RentDate : oldRental.RentDate;
                oldRental.ReturnDate = rental.ReturnDate != default ? rental.ReturnDate : oldRental.ReturnDate;

                _rentalDal.Update(oldRental);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }
    }
}
