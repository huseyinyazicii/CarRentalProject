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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IResult Add(Color color)
        {
            try
            {
                _colorDal.Add(color);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public IResult Delete(Color color)
        {
            try
            {
                _colorDal.Delete(color);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

        public IDataResult<Color> Get(int id)
        {
            Color color;
            try
            {
                color = _colorDal.Get(c => c.Id == id);
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<Color>(exception.Message);
            }
            return new SuccessDataResult<Color>(color);
        }

        public IDataResult<List<Color>> GetAll()
        {
            List<Color> colors;
            try
            {
                colors = _colorDal.GetAll();
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<Color>>(exception.Message);
            }
            return new SuccessDataResult<List<Color>>(colors);
        }

        public IResult Update(Color color)
        {
            Color oldColor;
            try
            {
                oldColor = _colorDal.Get(c => c.Id == color.Id);
                if (oldColor == null)
                {
                    return new ErrorResult("Color don't found");
                }
                oldColor.Name = color.Name != default ? color.Name : oldColor.Name;
                _colorDal.Update(oldColor);
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }
    }
}
