CREATE OR REPLACE FUNCTION proc_GetDoctorsBySpeciality(spciality VARCHAR(20))
RETURNS TABLE(id INT,dname TEXT,yoe REAL)
AS 
$$
BEGIN
   RETURN QUERY
   		 SELECT DISTINCT "Id","Name","YearsOfExperience" 
		 FROM public."Doctors"
   		 WHERE "Id" IN(
			SELECT "DoctorId" 
			FROM public."DoctorSpecialities" 
			WHERE "SpecialityId" IN(
				SELECT "Id"
				FROM public."Specialities"
				WHERE "Name"=spciality
			)
		);
END;
$$
LANGUAGE PLPGSQL;



-- DROP FUNCTION proc_GetDoctorsBySpeciality(spciality VARCHAR(20));

SELECT * 
FROM proc_GetDoctorsBySpeciality('Cardiology');

