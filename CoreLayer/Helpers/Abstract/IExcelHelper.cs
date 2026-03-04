using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Helpers.Abstract
{
	public interface IExcelHelper
	{
		// T: Herhangi bir sınıf olabilir (BlogDto, YorumDto vs.)
		byte[] ExportToExcel<T>(List<T> list, string sheetName) where T : class;
	}
}
