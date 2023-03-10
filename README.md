Monolith architecture and MVC were used in the project. 
AutoFac Library is used for AOP. 
FluentValidation library was used for validation. Entity Framwork Core ORM is used. 
JWT is included in the project. 
The TaskDb.bak file is in the DataAccess Folder. 
Don't forget to change the database configuration in the Web Api appsettings.json file. 
Authorization has been made in the Delete User method. The deletion process is performed only with the admin user. You can add users from the register method.
OperationClaim and UserOperationClaim tables have been added and users can be assigned privileges.

Note:
The admin user is assigned admin authority.

Warning:
admin information;
Email: admin@gmail.com
password: admin


Project Configurations;

DbContext Configuration:
<img width="951" alt="DBContex_Configuration" src="https://user-images.githubusercontent.com/72544552/224307654-02828dbf-b034-45da-b37f-c463eb491145.png">

UIPresantation Configuration:
</br>
Warning: 
</br>
Local http address generated when WebAPI is run
<img width="946" alt="WebUI_Configuration" src="https://user-images.githubusercontent.com/72544552/224307937-bdba9f95-5b8f-4131-8fb6-e75d9af08d9a.png">

Running the Project:
</br>
1-Right click on the project </br>
2-Set Startup Projects

<img width="390" alt="Project_Debug" src="https://user-images.githubusercontent.com/72544552/224310406-21c98ffe-6c66-4400-aae6-9cd4d3fd6401.png">
