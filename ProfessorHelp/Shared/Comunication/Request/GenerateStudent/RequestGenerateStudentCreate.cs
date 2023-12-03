﻿using ProfessorHelp.Models.Enum;

namespace ProfessorHelp.Shared.Comunication.Request.GenerateStudent;

public class RequestGenerateStudentCreate
{
    public string First_Name { get; set; } = string.Empty;
    public string Last_Name { get; set; } = string.Empty; 

    public Sex Sex { get; set; }
}
