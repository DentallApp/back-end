-- name: GetTotalAppointments
SELECT 
COUNT(*) AS Total,
SUM(case when appointment_status_id = @Assisted then 1 else 0 END) 
    AS TotalAppointmentsAssisted,
SUM(case when appointment_status_id = @NotAssisted then 1 else 0 END) 
    AS TotalAppointmentsNotAssisted,
SUM(case when appointment_status_id = @Canceled AND is_cancelled_by_employee = 1 then 1 else 0 END) 
    AS TotalAppointmentsCancelledByEmployee,
SUM(case when appointment_status_id = @Canceled AND is_cancelled_by_employee = 0 then 1 else 0 END) 
    AS TotalAppointmentsCancelledByPatient
FROM appointment AS a
WHERE (a.appointment_status_id <> @Scheduled) AND
      (a.date >= @From AND a.date <= @To) AND
      (a.office_id = @OfficeId OR @OfficeId = 0) AND
      (a.dentist_id = @DentistId OR @DentistId = 0)