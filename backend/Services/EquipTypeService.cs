using backend.Data;
using backend.Models;

namespace backend.Services.equipTypes;

public class EquipTypeService {
    private readonly AppDbContext _context;
    public EquipTypeService(AppDbContext context){
        _context = context;
    }

    public List<EquipType> GetAllEquipTypes(){
        return _context.EquipmentTypes.ToList();
    }

    public EquipType? GetEquipTypeById(int id){
        return _context.EquipmentTypes.FirstOrDefault(e => e.Id == id);
    }

    public EquipType CreateEquipType(string name){
        var equipType = new EquipType{Name = name};
        _context.EquipmentTypes.Add(equipType);
        _context.SaveChanges();
        return equipType;
    }

    public EquipType? UpdateEquipType(int id, string name){
        var equipType = _context.EquipmentTypes.FirstOrDefault(e => e.Id == id);
        if(equipType == null) return null;
        equipType.Name = name;
        _context.SaveChanges();
        return equipType;
    }

    public bool DeleteEquipType(int id){
        var equipType = _context.EquipmentTypes.FirstOrDefault(e => e.Id == id);
        if(equipType == null) return false;
        _context.EquipmentTypes.Remove(equipType);
        _context.SaveChanges();
        return true;
    }
}