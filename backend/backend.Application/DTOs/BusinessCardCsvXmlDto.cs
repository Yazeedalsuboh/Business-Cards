using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace backend.Application.DTOs
{

  [XmlRoot("BusinessCardCsvXmlDtos")]
  public class BusinessCardCsvXmlDtoList
  {
    [XmlElement("BusinessCardCsvXmlDto")]
    public List<BusinessCardCsvXmlDto> Items { get; set; }
  }

  public class BusinessCardCsvXmlDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        [XmlIgnore]
        public DateOnly DateOfBirth { get; set; }

        [XmlElement("DateOfBirth")]
        public string DateOfBirthString
        {
            get => DateOfBirth.ToString("yyyy-MM-dd");
            set => DateOfBirth = DateOnly.Parse(value);
        }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
