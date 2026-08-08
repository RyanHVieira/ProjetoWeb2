using backend.Services.equipTypes;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("equipTypes")]
public class EquipmentTypeController : ControllerBase {
     private readonly EquipTypeService _equipTypeService;
     public EquipmentTypeController(EquipTypeService equipTypeService){
        _equipTypeService = equipTypeService;
    }

}
