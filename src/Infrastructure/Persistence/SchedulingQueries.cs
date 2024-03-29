﻿namespace DentallApp.Infrastructure.Persistence;

/// <inheritdoc cref="ISchedulingQueries" />
public class SchedulingQueries(DbContext context) : ISchedulingQueries
{
    public async Task<List<SchedulingResponse>> GetDentalServicesAsync()
    {
        var treatments = await context.Set<GeneralTreatment>()
            .Where(treatment => treatment.SpecificTreatments.Any())
            .Select(treatment => new SchedulingResponse
            {
                Title = treatment.Name,
                Value = treatment.Id.ToString()
            })
            .AsNoTracking()
            .ToListAsync();

        return treatments;
    }

    public async Task<List<SchedulingResponse>> GetDentistsAsync(int officeId, int specialtyId)
    {
        var dentists = await 
            (from employee in context.Set<Employee>()
             join person in context.Set<Person>() on employee.PersonId equals person.Id
             join office in context.Set<Office>() on employee.OfficeId equals office.Id
             join employeeSpecialty in context.Set<EmployeeSpecialty>()
              on employee.Id equals employeeSpecialty.EmployeeId into employeeSpecialties
             from employeeSpecialty in employeeSpecialties.DefaultIfEmpty()
             join userRole in context.Set<UserRole>() on employee.UserId equals userRole.UserId
             where employee.IsActive() &&
                   employee.OfficeId == officeId &&
                   userRole.RoleId == (int)Role.Predefined.Dentist &&
                   employee.EmployeeSchedules.Any() &&
                   (employeeSpecialty.SpecialtyId == specialtyId || HasNoSpecialties(employee))
             select new SchedulingResponse
             {
                 Title = person.FullName,
                 Value = employee.Id.ToString()
             })
             .IgnoreQueryFilters()
             .AsNoTracking()
             .ToListAsync();

        return dentists;
    }

    [Decompile]
    private static bool HasNoSpecialties(Employee employee)
    {
        return !employee.EmployeeSpecialties.Any();
    }

    public async Task<List<SchedulingResponse>> GetOfficesAsync()
    {
        var offices = await context.Set<Office>()
            .Where(office => office.OfficeSchedules.Any())
            .Select(office => new SchedulingResponse
            {
                Title = office.Name,
                Value = office.Id.ToString()
            })
            .AsNoTracking()
            .ToListAsync();

        return offices;
    }

    public async Task<List<SchedulingResponse>> GetPatientsAsync(AuthenticatedUser user)
    { 
        var choices = await context.Set<Dependent>()
            .Where(dependent => dependent.UserId == user.UserId)
            .Select(dependent => new SchedulingResponse
            {
                Title = dependent.Person.FullName + " / " + dependent.Kinship.Name,
                Value = dependent.Person.Id.ToString()
            })
            .AsNoTracking()
            .ToListAsync();
        
        choices.Insert(0, new SchedulingResponse
        {
            Title = user.FullName + " / " + KinshipName.Default,
            Value = user.PersonId.ToString()
        });

        return choices;
    }
}
