using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult(Messages.AddColorMessage);
        }

        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new SuccessResult(Messages.DeleteColorMessage);
        }

        public IDataResult<Color> Get(int id)
        {
            Color color = _colorDal.Get(c => c.Id == id);
            if(color == null)
            {
                return new ErrorDataResult<Color>(Messages.GetErrorColorMessage);
            }
            return new SuccessDataResult<Color>(color, Messages.GetSuccessColorMessage);
        }

        public IDataResult<List<Color>> GetAll()
        {
            List<Color> colors = _colorDal.GetAll();
            if (colors == null)
            {
                return new ErrorDataResult<List<Color>>(Messages.GetErrorColorMessage);
            }
            return new SuccessDataResult<List<Color>>(colors);
        }

        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.EditColorMessage);
        }
    }
}
