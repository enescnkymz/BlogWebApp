using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateAccessLayer.Abstract
{
    public interface IDescriptionDal : IGenericDal<Description>
    {

        List<Description> GetDescriptionsWithCategory();
        Description GetDescriptionWithCategory(int id);

    }
}
