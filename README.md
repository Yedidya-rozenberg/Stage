# Stage - Online Course Platform project

# Introduction:
Hi, Project tester! My name is Yedidya Rozenberg, A student in HackerU company. Before I will start explaining about this project ,I'd like you'd take Some notes about the progress it made.
1.	I wrote the project at the same time as work and family, and unplanned events such as moving house, corona and death in the family. In the end, a lot of what I planned went into the server side only, into an unfinished version (which you can find on git) or not at all. I tried to demonstrate, even without fully implementing, different abilities in the final project. I hope to develop it into a full-sized app in the future.
2.	The grade is less important to me. The main thing for me is learning and development. I would appreciate any comments that would allow me to learn from her how to do it better.
 # About This Project
This project aims to demonstrate an online learning system. It is built to demonstrate knowledge and application of the fundamentals and principles of full stack programming - C#, SQL, EF, and Angular.
The project allows logging in as a student or lecturer, access to the list of courses, with various search options. From there there is a transition to the course itself, according to the access permissions, and from there to the course units.
When logging in as a lecturer, it is possible to perform CRUD operations on the course units and the course details. (Note - when connecting from a small device it is impossible to perform these actions).
# Installation - IMPORTANT
•	Download zip
•	Open folder
•	Open terminal and write "npm install"
•	After installation write "cd Stage/StageApp/API" and then "dotnet run"
•	Open another terminal and write "cd Stage/StageApp /client " and then "ng serve"
•	Now the project is activated and full of seed data.
•	All demo user's password is Pa$$word. Teacher name example "Vera". Student name example "Webb".
•	Enjoy 
# Database
The information is stored in a SQLite database. The server sets up the database and communicates with it using the EF code first method. The database includes the following entities:
•	Users - including teacher and student type users, with TPH inheritance.
•	Courses
•	Units
•	Photos - are linked in a one-to-one relationship to courses or users, and contain a link to an external API address where the image is hosted.
The database includes one-to-one relationships (photo to user/course), one-to-many (courses to lecturer) and many-to-many (students to courses).
# Server side
The server side is built on the Unit of Work design pattern, in order to make it easier to make changes to the code in the future, and to prevent conflicts between the various repositories in accessing the database. 
In addition, all repositories and services include interfaces that define their operations - which makes it possible to produce different implementations if necessary.
**Server side Included:**
•	Extensions
•	MiddleWare
•	Paginations (Helpers folder)
•	AutoMapper (Helpers folder)
•	Cloudinary Service
•	SSL + Certificate
•	Repositories that allow CRUD operations to be performed on the various tables.
# API
Note! The controllers were tested in Postman. Not all capabilities have been realized on the client side.
Includes the following controllers:
•	Account:
o	 Register.
o	 login.
•	Courses:
o	 get courses - Allows filtering by various parameters, including by lecturer name (using JOIN).
o	Get course.
o	Create course
o	Update course.
o	Delete course
o	Register to course.
o	Unregister to course
o	Get students of course.
•	Units:
o	Get units of course.
o	Get unit (full unit).
o	Create unit.
o	Delete unit.
o	Update unit.
•	Users:
o   Getting "My Profile"
o	Update user.
o	Add photo (using Cloudinary).
o	Delete photo (using Cloudinary).
# Client Side Components:
•	Nav - Always close to the top of the page. Contains a login form and the site's motto. In connected mode includes a navigation menu (currently only "My courses") and the user's name and photo, with a drop-down menu that includes the option to disconnect.
•	Home - A brief explanation of the website and 3 sample courses. "Register" button.
•	Register - Dynamic registration form, including validations.
•	Footer - Adjacent to the bottom of the page. including my contact details.
•	Course-card - When using the course list view. Contains course name, description, teacher and picture. Clicking on it takes you, according to permission, to the course page.
•	Courses - including a search panel according to my courses, teacher's name, and page size.
•	Course - includes the details of the course and the list of units included in it. The lecturer has a sidebar that allows him to perform CRUD operations on the course and the units. A student cannot enter a course if it is disabled.
•	Unit - shows the study unit. For a lecturer, edit mode is available.
•	Not found - includes a button back to the main page.
**Client side included**
•	Interceptors
•	Guards
•	Shared module
•	Resolver
•	Spinner
•	Toaster
•	error management







