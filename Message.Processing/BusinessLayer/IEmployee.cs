using ClassLibrary1.DataAccessLayer;

namespace ClassLibrary1.BusinessLayer;

public interface IEmployee
{
    RoleLevel Role { get; }

    Identification Identification { get; }

    string Name { get; }

    string Surname { get; }

    int Id { get; }

    void AddEmployee(IEmployee newEmployee);

    void DeleteEmployee(IEmployee toDelete);

    List<IEmployee> GetAll();

    void Work();
}