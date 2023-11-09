using System.Xml.Serialization;
using PPM.Model;

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

        using (TextWriter projectWriter = new StreamWriter(projectFile))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Project>));
            serializer.Serialize(projectWriter, ProjectRepository.projectList);
        }

        using (TextWriter employeeWriter = new StreamWriter(employeeFile))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));
            serializer.Serialize(employeeWriter, EmployeeRepository.employeeList);
        }
        
        using (TextWriter roleWriter = new StreamWriter(roleFile))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Role>));
            serializer.Serialize(roleWriter, RolesRepository.rolesList);
        }

        using (TextWriter employeeToProjectWriter = new StreamWriter(employeeToProjectFile))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<EmployeeProject>));
            serializer.Serialize(employeeToProjectWriter, EmployeeProjectRepository.employeeProjectList);
        }

    }
}
