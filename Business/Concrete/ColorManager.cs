using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public void Add(Color color)
        {
            _colorDal.Add(color);
        }

        public void Delete(Color color)
        {
            _colorDal.Delete(color);
        }

        public Color Get(int id)
        {
            var result = _colorDal.Get(c => c.Id == id);
            return result;
        }

        public List<Color> GetAll()
        {
            var result = _colorDal.GetAll();
            return result;
        }

        public void Update(Color color)
        {
            var oldColor = _colorDal.Get(c => c.Id == color.Id);

            if(oldColor == null)
            {
                throw new InvalidOperationException();
            }

            oldColor.Name = color.Name != default ? color.Name : oldColor.Name;

            _colorDal.Update(oldColor);
        }
    }
}
