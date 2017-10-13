# Plugin Architecture via Abp and Angular4+
This repository works on building a plugin architecture using ASP.NET boilerplate framework and angular 4+.

### Prerequisite
1. Setup the project accordingly according to the [ASP.NET Boilerplate Documentation](https://aspnetboilerplate.com/Pages/Documents/Zero/Startup-Template-Angular)
2. Install [Yarn](https://yarnpkg.com/en/)
3. Has Powershell available.

### Quick Start
#### Website without Plugin
##### Run the Host
    1. Navigate to ``/aspnet-core`` and open ``Todo.MainProject.sln``
    2. Set ``/src/Todo.MainProject.Web.Host`` as StartUp project
    3. ****[First time only]** Open ``Package Manager Console`` and run ``Update-Database`` command to create your database (ensure that Default project is selected as ``Todo.MainProject.EntityFrameworkCore`` in the Package Manager Console window).
    4. Build & Run.
##### Run the UI
    1. Navigate to ``/angular`` and open a ``powershell``
    2. ****[First time only]** Run ``Yarn``
    3. Run ``ng build --prod``, then Run ``dotnet run``
    4. Open your browser and navigate to [http://localhost:4200](http://localhost:4200)
#### Website with Plugin
1. Setup the project once: Ran ``Update-Database`` and ``Yarn``
2. Run ``addPlugin.ps1`` located in the root level.
3. Now [Run the Host](README.md#Run-the-Host) then [Run the UI](README.md#Run-the-Host)

### How it works
1. The backend (Host) plugin architecture system made use of the ASP.NET Boilerplate template.
2. The frontend (UI) plugin is developed using multiple angular4+ applications. When the plugin is done, we need to compile and build the UI to get the minified js, html, css and assets files. Then, put the plugin-ui into the main ui wwwroot folder and use a hosting-server to run the UI. Then, voila, we can now use the plugin.

### FAQ
1. Can we use ``yarn start`` or ``npm start`` instead of running ``ng build`` and ``dotnet run``?
    * Running ``yarn start`` or ``npm start`` would not see the plugin, so, if you want to see the plugin, then no.

### Acknowledgement
Special thanks to [Aspnetboilerplate](https://github.com/aspnetboilerplate/aspnetboilerplate) for their amazing work!


If you are interested in the template, please see: [Aspnetboilerplate Template Readme](./Template_readme.md)