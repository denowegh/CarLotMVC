using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AutoLotDAL.Models.Models.MetaData
{
    /*В то время как дополнительные аннотации данных можно добавлять к самим классам моделей, зачастую полезно помещать
     * их в отдельный класс, чтобы предохранить целостность классов моделей.*/
    public class InventoryMetaData
    {
        /*Многие аннотации данных, применяемые инфраструктурой Entity Framework, также используются механизмом представлений
         * MVC для визуализации разметки и проверки достоверности. Существуют дополнительные аннотации данных,
         * которые игнорируются EF, но применяются для MVC, такие как аннотации данных Display. Аннотации данных 
         * Display сообщают MVC имя, подлежащее использованию вместо имени свойства, когда в представлении
         * применяется вспомогательный метод HTML под названием DisplayName () или DisplayNameFor(). Синтаксис
         * прост, как иллюстрируется в показанном ниже примере, где отображаемое имя изменяется с PetName (имя
         * свойства) на более читабельное Pet Name*/
        [Display(Name = "Pet Name")]
        public string PetName;

    }
}
