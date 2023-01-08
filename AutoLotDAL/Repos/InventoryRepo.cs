using AutoLotDALModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotDAL.Repos
{
    public class InventoryRepo:BaseRepo<Inventory>
    {
        public override List<Inventory> GetAll() => Context.Inventory.OrderBy(x => x.PetName).ToList();
        


    }
}
