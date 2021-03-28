using MedMan.Application.Common.Mappings;
using MedMan.Application.Prescriptions.Queries.Common;
using MedMan.Domain.Entities;
using System;

namespace MedMan.Application.Administrations.Queries.Common
{
    public class AdministrationDto : PrescriptionDto, IMapFrom<Administration>
    {
        public DateTime TimeGiven { get; set; }
    }
}
