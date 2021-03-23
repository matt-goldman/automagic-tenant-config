using MedMan.Application.Common.Mappings;
using MedMan.Application.Prescriptions.Common;
using MedMan.Domain.Entities;
using System;

namespace MedMan.Application.Administrations.Common
{
    public class AdministrationDto : PrescriptionDto, IMapFrom<Administration>
    {
        public DateTime TimeGiven { get; set; }
    }
}
