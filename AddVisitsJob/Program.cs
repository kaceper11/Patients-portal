using System;
using System.Collections.Generic;
using System.Linq;
using PatientsPortalModel;
using PatientsPortalModel.Entities;


namespace AddVisitsJob
{
    class Program
    {
        static void Main(string[] args)
        {
            int addedVisits = 0;
            foreach (var doctor in Doctors)
            {
                foreach (var date in GetWorkDaysForDoctor(doctor.Id))
                {
                    if (CheckIfWeekDay((DateTime)date))
                    {
                        foreach (var time in GetVisitHoursForDay(doctor.Id, (DateTime)date))
                        {
                            Visit visit = new Visit()
                            {
                                UserId = null,
                                DoctorId = doctor.Id,
                                Specialization = doctor.Specialization,
                                IsReserved = false,
                                VisitDate = date,
                                VisitTime = time,
                                CreatedDate = DateTime.Now,
                                CreatedBy = "AddVisitsJob"
                            };

                            if (CheckIfVisitExist(visit))
                            {
                                Console.WriteLine("Skipping existing visit with DoctorId {0}, Visit Date {1} and Visit Time {2}",
                                visit.DoctorId, visit.VisitDate, visit.VisitTime);
                                Console.WriteLine("Going to next one...");
                            }
                            else

                            {
                                Console.WriteLine("Adding new visit for Doctor with Id = {0}, Visit Date {1} and Visit Time {2}",
                                    visit.DoctorId, visit.VisitDate, visit.VisitTime);
                                db.Visits.Add(visit);
                                db.SaveChanges();
                                addedVisits += 1;
                               
                                Console.WriteLine("Added new visit for Doctor with Id = {0}, Visit Date {1} and Visit Time {2}",
                                    visit.DoctorId, visit.VisitDate, visit.VisitTime);
                            }
                        }                     
                    }

 
                }
            }
            Console.WriteLine("Added {0} visits for {1} doctors.", addedVisits,  db.Doctors.Count());
            Console.WriteLine("Finished job execution.");

        }

        private static PatientsPortalContext db = new PatientsPortalContext();

        static List<Doctor> Doctors = db.Doctors.ToList();

        //If visit exists in database - return true
        private static bool CheckIfVisitExist(Visit visit)
        {
            var dbVisit = (from m in db.Visits
                where (m.DoctorId == visit.DoctorId &&  m.VisitDate == visit.VisitDate 
                                                    && m.VisitTime == visit.VisitTime)
                select m);

            if (dbVisit.Any())
            {
                return true;
            }
            return false;
        }

        private static bool CheckIfWeekDay(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
            {
                return false;
            }
            return true;
        }


        private static List<DateTime?> GetWorkDaysForDoctor(int doctorId)
        {
            var listDates = (from w in db.WorkHours
                             where w.DoctorId == doctorId
                             select w.WorkDate).ToList();

            return listDates;
        }

        private static List<int> GetVisitHoursForDay(int doctorId, DateTime date)
        {
            List<int> visitHours = new List<int>();

            for (int i = (int)GetStarWorkHoursForDay(doctorId, date); i <= GetEndWorkHoursForDay(doctorId, date); i+=15)
            {
                visitHours.Add(i);
            }

            return visitHours;
        }


        private static int? GetStarWorkHoursForDay(int doctorId, DateTime date)
        {

            int startWorkTime = (from w in db.WorkHours
                where w.DoctorId == doctorId && w.WorkDate == date
                select w.StartTime).SingleOrDefault();

            return startWorkTime;
           
        }

        private static int? GetEndWorkHoursForDay(int doctorId, DateTime date)
        {         
            int endWorkTime = (from w in db.WorkHours
                where w.DoctorId == doctorId && w.WorkDate == date
                select w.EndTime).SingleOrDefault();

            return endWorkTime;         
        }

    }
}
