-- Insert sample data into the UserRole table
SET IDENTITY_INSERT roles ON;
INSERT INTO roles(RoleId, RoleName)
VALUES 
(1, 'COCO'),
(2, 'ModuleRouter'),
(3, 'Faculty');

-- Insert sample data into the User table
INSERT INTO [User] (Email, UserName, Password, MobileNo, RoleId)
VALUES 
('pratik.com', 'JohnDoe', 'password123', '1234567890', 1),  -- Admin role
('jyoti.com', 'JaneSmith', 'securepass', '0987654321', 2),  -- Instructor role
('priya.com', 'AliceJones', 'mypassword', '1122334455', 3);  -- Student role

