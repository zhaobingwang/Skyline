SELECT * FROM Users;
SELECT * FROM Roles;
SELECT * FROM UserRoleMappings;
SELECT * FROM Menus;
SELECT * FROM Permissions;
SELECT * FROM RolePermissionMappings;


DELETE FROM UserRoleMappings;
DELETE FROM RolePermissionMappings;
DELETE FROM Roles;
DELETE FROM Users;
DELETE FROM Permissions;
DELETE FROM Menus;