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
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> Get(int id);
        IResult Add(Rental rental);
        IResult Delete(Rental rental);
        IResult Update(Rental rental);

        /// <summary>
        /// Aracı teslim al.
        /// </summary>
        IResult DeliverTheCar(Rental rental);

        /// <summary>
        /// Kiralanan ve kiralanmayan tüm araçların detaylı listesidir.
        /// </summary>
        IDataResult<List<RentalDetailDto>> GetAllRentalDetails();

        /// <summary>
        /// Teslim alınmayan tüm araçların detaylı listesidir.
        /// </summary>
        IDataResult<List<RentalDetailDto>> GetAllUndeliveredRentalDetails();

        /// <summary>
        /// Teslim alınan tüm araçların detaylı listesidir.
        /// </summary>
        IDataResult<List<RentalDetailDto>> GetAllDeliveredRentalDetails();
    }
}
