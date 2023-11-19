namespace API.Dtos
{
    public class P_EmployeeDto
    {
        public string Name { get; set; } 
    
        public string LastName1 { get; set; } 
    
        public string LastName2 { get; set; } 
    
        public string Extension { get; set; } 
    
        public string Email { get; set; } 
    
        public int BossId { get; set; }
    
        public string JobTitle { get; set; } 
    
    }

    public class EmployeeOfficeDto
    {
        public string Name { get; set; } 
    
        public string LastName1 { get; set; } 
    
        public string JobTitle { get; set; } 
        public EssencialOfficeDto Office {get;set;}    
    }
    
    public class EmployeeNameDto
    {
        public string Name { get; set; } 
    }

        public class EmployeeBossDto
    {
        public string Name { get; set; } 
    
        public string LastName1 { get; set; } 
    
        public string LastName2 { get; set; } 
    
        public string Extension { get; set; } 
    
        public string Email { get; set; } 
    
        public EmployeeNameDto Boss { get; set; }
    
        public string JobTitle { get; set; } 
    
    }
}