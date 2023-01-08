using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
using AutoLotDALModels.Models;
using AutoLotDAL.Models.Models.MetaData;

namespace AutoLotDALModels.Models
{
    /*Файлы метаданных не являются файлами полных определений классов; они используются только для загрузки атрибутов
     * в другие файлы. Следовательно, добавлять к такому свойству синтаксис get/set не придется и фактически это
     * делаться не должно. Может возникнуть вопрос; как инфраструктура узнает, что указанный класс предоставляет
     * атрибуты для класса Inventory? В настоящий момент никак. К классу Inventory необходимо добавить атрибут уровня
     * класса, чтобы инфраструктуре стало известно, что класс Inventory содержит дополнительные атрибуты для нее.*/
    [MetadataType(typeof(InventoryMetaData))]
    public partial class Inventory : EntityBase
    {
        public override string ToString()
        {
            return $"{ this.PetName ?? "**No Name**"} \t is a {this.Color} \t {this.Make} \twith ID {this.Id}.";
        }
        /*
         * Атрибут [NotMapped] сообщает инфраструктуре EF о том, что поле является 
         * свойством, относящимся только к .NET.*/
        [NotMapped]
        public string MakeColor => Color +" "+Make;


    }
}
