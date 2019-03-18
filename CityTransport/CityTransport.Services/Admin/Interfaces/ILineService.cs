namespace CityTransport.Services.Admin.Interfaces
{
    using CityTransport.Common.Models;
    using CityTransport.Common.Models.Admin.InputModels.Line;
    using CityTransport.Common.Models.Admin.ViewModels.Line;
    using System.Collections.Generic;

    public interface ILineService
    {
        ICollection<LineViewModel> Lines();

        BaseModel CreateLine(CreateLineInputModel model);

        EditLineInputModel GetEditLineModel(int? id);

        BaseModel EditLine(EditLineInputModel model);
    }
}
