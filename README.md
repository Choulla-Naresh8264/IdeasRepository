# IdeasRepository
NOTE: Сonnection string is configured to use the MS SQL Server 2012 Standard Edition

### Overview and authorization/authentication system
The Ideas Repository project bases on the ASP.NET MVC 5 platform with .NET Framework 4.5.2 targeted version. I choose it because it includes an Identity authentication and authorization system that give more flexibility in projecting database structure in comparison with outdated Membership system. One of the major advantages of Identity is that it supports Entity Framework code-first approach and it was being interesting for me to get more familiar with it.
### High-level architecture
Project high-level architecture is based on 3-tier model that consists of presentation layer, business logic layer and data access layer. It allows to make class structure more clearly and extensible.
### ORM
I used Entity Framework code-first approach for creating additional entities in the database and building relations between them. Also default database initializer was being overriding for filling the database with test values on application start. It’s makes the debugging process more convenient and clear. For updating database models was been creates a migration classes that allow up- and downgrade database and easily track changes with the possibility of change.
### IoC container
To Implement IoC container for dependency injection mechanism I used Ninject library. It makes easier to test the code and develop complex logic structures.
### Common tools
As database server was being used the MS SQL Server 2012 Standard Edition, Visual Studio 2015 Community – as IDE.

