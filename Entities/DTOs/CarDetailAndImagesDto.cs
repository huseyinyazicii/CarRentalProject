using Core.Entities.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Entities.DTOs
{
    public class CarDetailAndImagesDto : IDto
    {
        public CarDetailDto Car { get; set; }
        public List<CarImage> CarImages { get; set; }
    }
}
