-- name: GetTotalScheduledAppointments
SELECT 
CONCAT(p.names, ' ', p.last_names) AS DentistName,
o.name AS OfficeName,
COUNT(*) AS Total
FROM appointment AS a
INNER JOIN employee AS e ON e.id = a.dentist_id
INNER JOIN person AS p ON p.id = e.person_id
INNER JOIN office AS o ON o.id = a.office_id
WHERE (a.appointment_status_id = @Scheduled) AND
	  (a.date >= @From AND a.date <= @To) AND
	  (a.office_id = @OfficeId OR @OfficeId = 0)
GROUP BY a.dentist_id
ORDER BY Total DESC