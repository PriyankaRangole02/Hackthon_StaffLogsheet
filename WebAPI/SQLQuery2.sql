-- Insert sample data into the LogSheets table
INSERT INTO LogSheets (UserId, StartTime, EndTime, LogType, CourseId, SubjectId, Topic, VerifiedBy, ApprovedBy, Status)
VALUES 
(10, '2024-08-08 09:00:00', '2024-08-08 11:00:00', 'Lecture', 99, 3, 'Introduction to SQL', 2, 3, 'Approved'),
(11, '2024-08-08 12:00:00', '2024-08-08 13:30:00', 'Lab', 100, 4, 'Hands-on SQL Practice', 3, NULL, 'Verified'),
(12, '2024-08-09 10:00:00', '2024-08-09 11:00:00', 'Workshop', 101, 5, 'Advanced SQL Techniques', NULL, NULL, 'Pending');
