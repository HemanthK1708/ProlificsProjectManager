using System.Xml.Serialization;
using PPM.Model;
using PPM.DAL;

namespace PPM.Domain;

public class SerializeData
{
    public static void SaveState()
    {
        string projectFile =
            @"C:\Users\HChilamkurthi\Documents\Dotnet_Assignment\ProlificsProjectManager\PPM.XML\ProjectData.xml";
        string roleFile =
            @"C:\Users\HChilamkurthi\Documents\Dotnet_Assignment\ProlificsProjectManager\PPM.XML\RoleData.xml";
        string employeeFile =
            @"C:\Users\HChilamkurthi\Documents\Dotnet_Assignment\ProlificsProjectManager\PPM.XML\EmployeeData.xml";
        string employeeToProjectFile =
            @"C:\Users\HChilamkurthi\Documents\Dotnet_Assignment\ProlificsProjectManager\PPM.XML\EmployeeToProjectData.xml";

        ProjectDAL projectDAL = new();
        EmployeeDAL employeeDAL = new();
        RoleDAL roleDAL = new();
        EmployeeProjectDAL employeeProjectDAL = new();

        using (TextWriter projectWriter = new StreamWriter(projectFile))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Project>));
            projectDAL.ViewAll();
            serializer.Serialize(projectWriter, ProjectDAL.projectList);
        }

        using (TextWriter employeeWriter = new StreamWriter(employeeFile))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
            employeeDAL.ViewAll();
            serializer.Serialize(employeeWriter, EmployeeDAL.employeeList);
        }

        using (TextWriter roleWriter = new StreamWriter(roleFile))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Role>));
            roleDAL.ViewAll();
            serializer.Serialize(roleWriter, RoleDAL.rolesList);
        }

        using (TextWriter employeeToProjectWriter = new StreamWriter(employeeToProjectFile))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<EmployeeProject>));
            employeeProjectDAL.ViewAll();
            serializer.Serialize(employeeToProjectWriter, EmployeeProjectDAL.employeeProjectList);
        }

    }
}
