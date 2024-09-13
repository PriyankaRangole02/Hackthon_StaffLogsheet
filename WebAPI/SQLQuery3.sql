-- Insert sample data into the LogSheets table
INSERT INTO LogSheets (UserId, StartTime, EndTime, LogType, CourseId, SubjectId, Topic, VerifiedBy, ApprovedBy, Status)
VALUES 
(3, '2024-08-08 09:00:00', '2024-08-08 11:00:00', 'Lecture', 1, 3, 'Introduction to SQL', 2, 1, 'Approved'),
(4, '2024-08-08 12:00:00', '2024-08-08 13:30:00', 'Lab', 1, 3, 'Hands-on SQL Practice', 2, NULL, 'Verified'),
(5, '2024-08-09 10:00:00', '2024-08-09 11:00:00', 'Workshop', 1, 3, 'Advanced SQL Techniques', NULL, NULL, 'Pending');
