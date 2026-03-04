using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class BlogValidator: AbstractValidator<Description>
    {
        public BlogValidator() 
        {
         RuleFor(x=>x.DescriptionTitle).NotEmpty().WithMessage("Blog başlığı boş olamaz");
         RuleFor(x=>x.DescriptionTitle).MinimumLength(10).WithMessage("Blog başlığı 10 karakterden fazla olmalıdır");
         RuleFor(x=>x.DescriptionTitle).MaximumLength(50).WithMessage("Blog başlığı 50 karakterden fazla olamaz");
         RuleFor(x=>x.DescriptionContent).NotEmpty().WithMessage("Blog içeriği boş olamaz");
         
         
           

        }
    
    
    }
}
