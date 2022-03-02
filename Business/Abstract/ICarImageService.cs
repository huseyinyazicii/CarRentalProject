using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IDataResult<List<CarImage>> GetAll();

        IDataResult<CarImage> Get(int id);

        IResult Add(CarImagesOperationDto carImagesOperationDto);

        IResult Update(CarImagesOperationDto carImagesOperationDto);

        IResult Delete(CarImage carImage);

        IDataResult<List<CarImage>> GetAllByCarId(int carId);
    }
}
