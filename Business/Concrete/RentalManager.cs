using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
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
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;
        private readonly ICarService _carService;
        private readonly IFindeksService _findeksService;

        public RentalManager(IRentalDal rentalDal, ICarService carService, IFindeksService findeksService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _findeksService = findeksService;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(
                WillLeasedCarAvailable(rental.CarId), 
                IfCheckFindeksScore(rental));

            if (result != null)
            {
                return result;
            }

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.AddRentalMessage);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.DeleteRentalMessage);
        }

        public IDataResult<Rental> Get(int id)
        {
            Rental rental = _rentalDal.Get(p => p.Id == id);
            if (rental == null)
            {
                return new ErrorDataResult<Rental>(Messages.GetErrorRentalMessage);
            }
            return new SuccessDataResult<Rental>(rental, Messages.GetSuccessRentalMessage);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            List<Rental> rentals = _rentalDal.GetAll();
            if (rentals.Count == 0)
            {
                return new ErrorDataResult<List<Rental>>(Messages.GetErrorRentalMessage);
            }
            return new SuccessDataResult<List<Rental>>(rentals, Messages.GetSuccessRentalMessage);
        }

        public IDataResult<List<RentalDetailDto>> GetAllRentalDetails()
        {
            List<RentalDetailDto> rentalDetailDtos = _rentalDal.GetAllRentalDetails();
            if (rentalDetailDtos.Count > 0)
                return new SuccessDataResult<List<RentalDetailDto>>(rentalDetailDtos, Messages.GetSuccessRentalMessage);
            else
                return new ErrorDataResult<List<RentalDetailDto>>(Messages.GetErrorRentalMessage);
        }

        public IDataResult<List<RentalDetailDto>> GetAllUndeliveredRentalDetails()
        {
            List<RentalDetailDto> rentalDetailDtos = _rentalDal.GetAllRentalDetails(p => p.ReturnDate == null);
            if (rentalDetailDtos.Count > 0)
                return new SuccessDataResult<List<RentalDetailDto>>(rentalDetailDtos, Messages.GetSuccessRentalMessage);
            else
                return new ErrorDataResult<List<RentalDetailDto>>(Messages.GetErrorRentalMessage);
        }

        public IDataResult<List<RentalDetailDto>> GetAllDeliveredRentalDetails()
        {
            List<RentalDetailDto> rentalDetailDtos = _rentalDal.GetAllRentalDetails(p => p.ReturnDate != null);
            if (rentalDetailDtos.Count > 0)
                return new SuccessDataResult<List<RentalDetailDto>>(rentalDetailDtos, Messages.GetSuccessRentalMessage);
            else
                return new ErrorDataResult<List<RentalDetailDto>>(Messages.GetErrorRentalMessage);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.EditRentalMessage);
        }

        public IResult DeliverTheCar(Rental rental)
        {
            var result = BusinessRules.Run(CanARentalCarBeReturned(rental.Id));
            if (result != null)
            {
                return result;
            }
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.CarDeliverTheCar);
        }

        #region RentalManager Business Rules

        private IResult WillLeasedCarAvailable(int carId)
        {
            if (_rentalDal.Get(p => p.CarId == carId && p.ReturnDate == null) != null)
                return new ErrorResult(Messages.CarNotAvaible);
            else
                return new SuccessResult();
        }

        private IResult CanARentalCarBeReturned(int carId)
        {
            if (_rentalDal.Get(p => p.CarId == carId && p.ReturnDate == null) == null)
                return new ErrorResult(Messages.CarNotAvaible);
            else
                return new SuccessResult();
        }

        private IResult IfCheckFindeksScore(Rental rental)
        {
            var car = _carService.Get(rental.CarId);
            var findeks = _findeksService.GetFindeksScore(rental.CustomerId);

            if (car.Success && findeks.Success)
            {
                if (car.Data.FindeksScore < findeks.Data)
                {
                    return new SuccessResult(Messages.FindeksPointsSufficient);
                }
                return new ErrorResult(Messages.FindeksPointsInsufficient);
            }
            return new ErrorResult(Messages.GetErrorRentalMessage);
        }

        #endregion RentalManager Business Rules
    }
}
